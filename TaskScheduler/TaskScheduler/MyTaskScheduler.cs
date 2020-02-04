/*
 *  © 2018, Alexander Moshan, All Rights Reserved.
 *  Scheduler of all types of functions. Persistent function saving "IF" the function is serializable.
 *  Version 1.0
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace TaskScheduler
{
    /// <summary>
    /// Scheduler static class that schedule all kinds of function, saving them for the long term as well, if they are serializable
    /// 
    /// How to use:
    /// 
    /// Start the scheduler:
    /// MyTaskScheduler.startScheduling();    -->>   will start the scheduling, message boxes will appear if no saved files exist.
    /// 
    /// Add task to schedule:
    /// MyTaskScheduler.addTask(TaskDate, TaskName, TaskRepetition, TaskFunction());  -->> message boxes will appear if the function non-serializable and wont be saved for the long term.
    /// 
    /// Stop the scheduler:
    /// MyTaskScheduler.stopScheduling();  -->> will stop the scheduling, all the saved functions will remain and be rescheduled when scheduler starts again.
    /// 
    /// Deleting tasks:
    /// Current GUI doesnt have task deleting, check the task list file for your task name and delete the file with the same name.
    /// The scheduler does delete tasks that are non-repetitive or has broken serialization file.
    /// Will appear in scheduler v2.
    /// </summary>
    static class MyTaskScheduler
    {
        #region Class variables
        [Serializable]
        public delegate void MyFunction();

        /// <summary>
        /// Repetition enum - how often does the task repeat.
        /// </summary>
        public enum Repetition { NEVER, MINUTELY, HOURLY, DAILY, WEEKLY, MONTHLY, YEARLY};

        /// <summary>
        /// sorted list to sort the tasks by their execution time
        /// </summary>
        private static SortedList<DateTime, JobToDo> jobsQueue = null;

        private static Timer timer;

        /// <summary>
        /// Determine if the scheduler is working or not - saves try/catch
        /// </summary>
        private static bool scheduling = false;

        /// <summary>
        /// lock for the jobs queue
        /// </summary>
        private static Object jobsQueueLock = new Object();

        private const string FOLDER_NAME = "SchedFolder";
        //extension dnt = do not touch (obviously made up by me, can be encrypted so people would no mess with it, but not in the current version/task).
        private const string TASK_LIST_FILE_NAME = FOLDER_NAME + "/TaskList.dnt";  //task list build : "TaskTime\tTaskRepetition\tTaskName";
        private const int ONE_HOUR_IN_MILISEC = 3600000; // 1000 * 60 * 60
        #endregion

        #region My private tasks class: JobToDo
        /// <summary>
        /// class that holds the job to be done - private and none other than scheduler class should know about it, for now.
        /// </summary>
        [Serializable]
        private class JobToDo
        {
            public MyFunction myFunction { get; }
            public Repetition myRepetition { get; set; }
            public string myName { get; set; }
            public bool serializable { get; set; }

            /// <summary>
            /// Two constructors to add a job.
            /// </summary>
            /// <param name="myName"></param>
            /// <param name="myRepetition"></param>
            /// <param name="myFunction"></param>
            public JobToDo(string myName, Repetition myRepetition, MyFunction myFunction)
            {
                this.myName = myName;
                this.myRepetition = myRepetition;
                this.myFunction = myFunction;
                this.serializable = false;
            }
            public JobToDo(string myName, Repetition myRepetition, MyFunction myFunction, bool serializable) : this(myName, myRepetition, myFunction)
            {
                this.serializable = serializable;
            }

            /// <summary>
            /// executing the task that is given to be executed.
            /// </summary>
            public void doMyJob()
            {
                try
                {   //try executing the given function.
                    myFunction();
                    System.Windows.Forms.MessageBox.Show("Finished executing the job: " + myName);
                }
                catch(Exception e)
                {
                    //if the function doesnt handle exceptions and fails, ill handle it for them.
                    System.Windows.Forms.MessageBox.Show("Failed to execute the job: " + myName + "\r\nError: " + e.Message);
                }
            }   
        }
        #endregion

        #region Scheduler functions
        /// <summary>
        /// Starts the scheduler, initialize the jobqueue, load saved task list and start the scheduling.
        /// </summary>
        public static void startScheduling()
        {
            if (!scheduling)
            {
                //create my folder if it doesnt exists yet
                Directory.CreateDirectory(FOLDER_NAME);                
                jobsQueue = new SortedList<DateTime, JobToDo>();
                loadTaskList();
                timer = new Timer(_ => executeTask(), null, 0, Timeout.Infinite);
                scheduling = true;
            }
        }

        /// <summary>
        /// Stops the scheduler - do when exit the program.
        /// </summary>
        public static void stopScheduling()
        {
            if (scheduling)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
                scheduling = false;
            }
        }

        /// <summary>
        /// Adding a new task, must meet the requierment or exception will be thrown (handled in the Gui).
        /// </summary>
        /// <param name="startingTaskTime">First time the task should start - must be above DateTime.Now</param>
        /// <param name="taskName">String of the task name, doesnt really matter here just for show in the list</param>
        /// <param name="taskRepetition">How often does the task repeat</param>
        /// <param name="taskFunction">What is the actual function to execute</param>
        public static void addTask(DateTime startingTaskTime, string taskName, Repetition taskRepetition, MyFunction taskFunction)
        {
            if(!scheduling)
            {
                throw new InvalidOperationException("Cant add tasks before starting the scheduler");
            }
            if(startingTaskTime == null)
            {
                throw new ArgumentException("Parameter cannot be null", "startingTaskTime");
            }
            if (taskName == null)
            {
                throw new ArgumentException("Parameter cannot be null", "taskName");
            }
            if (taskFunction == null)
            {
                throw new ArgumentException("Parameter cannot be null", "taskFunction");
            }
            if (startingTaskTime < DateTime.Now)
            {
                throw new ArgumentException("Parameter cannot be past time", "startingTaskTime");
            }
            //stop the timer
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            //try to add an element
            bool finishedAdding = false;
            while (!finishedAdding)
            {
                try
                {   //try adding the job to the queue
                    lock (jobsQueueLock)
                    {
                        jobsQueue.Add(startingTaskTime, new JobToDo(taskName, taskRepetition, taskFunction));
                    }
                    finishedAdding = true;
                }
                catch(Exception e) //it is a problem if the job owner spams all his tasks to be executed at the exact same time untill the milisec
                {
                    //exceptoion might be caused if the Key already exists. therefore we will add a milisec untill new key untill we find the next free key.
                    startingTaskTime = startingTaskTime.AddMilliseconds(1);
                }
            }
            //try serializing the function and saving it
            saveTask(startingTaskTime);
            //continue timer
            timer.Change(0, Timeout.Infinite);
        }

        /// <summary>
        /// The schedulers main function that executes all the jobs by their executing time.
        /// </summary>
        private static void executeTask()
        {
            long nextTaskTime;
            try
            {
                KeyValuePair<DateTime, JobToDo> earliestJob = jobsQueue.First(); //get the earliest job in the list
                nextTaskTime = (long)(earliestJob.Key - DateTime.Now).TotalMilliseconds;

                if (nextTaskTime <= 0) //if time has come/passed to do the first job, do it.
                {
                    //do the job on a different thread
                    Thread jobToDo = new Thread(new ThreadStart(earliestJob.Value.doMyJob));
                    jobToDo.Start();
                    //remove the job from the list
                    lock (jobsQueueLock)
                    {
                        jobsQueue.Remove(earliestJob.Key);
                    }
                    //add the job again if it has repetition. 
                    if (earliestJob.Value.myRepetition != Repetition.NEVER)
                    {
                        DateTime oldTaskTime = earliestJob.Key;
                        DateTime newTaskTime = new DateTime();
                        bool finishedAdding = false;
                        while (!finishedAdding)
                        {
                            try
                            {   
                                switch (earliestJob.Value.myRepetition) //add cases here if you want to add other repetition spans.
                                {
                                    case Repetition.MINUTELY: newTaskTime = oldTaskTime.AddMinutes(1); break;
                                    case Repetition.HOURLY: newTaskTime = oldTaskTime.AddHours(1); break;
                                    case Repetition.DAILY: newTaskTime = oldTaskTime.AddDays(1); break;
                                    case Repetition.WEEKLY: newTaskTime = oldTaskTime.AddDays(7); break;
                                    case Repetition.MONTHLY: newTaskTime = oldTaskTime.AddMonths(1); break;
                                    case Repetition.YEARLY: newTaskTime = oldTaskTime.AddYears(1); break;
                                }
                                //try adding the job to the queue
                                lock (jobsQueueLock)
                                {
                                    jobsQueue.Add(newTaskTime, earliestJob.Value);
                                }
                                finishedAdding = true;
                            }
                            catch (Exception e) //it is a problem if the job owner spams all his tasks to be executed at the exact same time untill the milisec
                            {
                                //exceptoion might be caused if the Key already exists. therefore we will add a milisec untill new key untill we find the next free key.
                                newTaskTime = newTaskTime.AddMilliseconds(1);
                            } 
                        }
                        //only update the files if the job is serializable, else dont even bother as it is not saved.
                        if (earliestJob.Value.serializable)
                        {
                            updateTask(oldTaskTime, newTaskTime);
                        }
                    }
                    else //it task does not repeat, delete its file.
                    {
                        deleteTask(earliestJob.Key);
                    }
                    nextTaskTime = (long)(jobsQueue.First().Key - DateTime.Now).TotalMilliseconds;
                    if(nextTaskTime < 0) //if 2 tasks are very close 1 after the other - must not set the timer thread on negative value
                    {
                        nextTaskTime = 0;
                    }
                }
            }
            catch (InvalidOperationException ioe) //if there are no jobs to do - set timer to sleep for 1 hour
            {
                nextTaskTime = ONE_HOUR_IN_MILISEC;
            }
            catch (ArgumentNullException ane) //the job queue isnt initiazted yet -> an issue because it shall be started when program starts start.
            {
                System.Windows.Forms.MessageBox.Show("Jobs queue caused an error: " + ane.Message + "\r\nRetrying to load saved list.");
                jobsQueue = new SortedList<DateTime, JobToDo>();
                loadTaskList();
                nextTaskTime = 0;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Execution error: " + e.Message);
                nextTaskTime = ONE_HOUR_IN_MILISEC;
            }

            //set the timer
            try
            {
                timer.Change(nextTaskTime, Timeout.Infinite); //try setting the timer on the earliest job
            }
            catch (Exception e) //max time length is 2^32 therefore i cannot set timer for longer than 24 days, hence ill set it on max possible length
            {
                timer.Change(int.MaxValue, Timeout.Infinite);
            }         
        }

        /// <summary>
        /// Delete the task that does not repeat itself.
        /// </summary>
        /// <param name="taskTime"></param>
        private static void deleteTask(DateTime taskTime)
        {
            try
            {
                File.Delete(Path.Combine(FOLDER_NAME, taskTime.Ticks.ToString() + ".bin"));
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Failed to delete task function file: " + taskTime.Ticks.ToString() + ".bin" + "\r\nError: " + e.Message);
            }
            saveTaskList();
        }

        /// <summary>
        /// Update task execution file
        /// </summary>
        /// <param name="oldTaskTime"></param>
        /// <param name="newTaskTime"></param>
        private static void updateTask(DateTime oldTaskTime, DateTime newTaskTime)
        {
            //check if the new task time is different from the old, might be loading a file that is not old yet.
            if(oldTaskTime != newTaskTime)
            {
                //try changing the file name, if it doesnt succeed because of already existing files, just delete the old and create new.
                bool saveFileList = true;
                try
                {
                    File.Move(Path.Combine(FOLDER_NAME, oldTaskTime.Ticks.ToString() + ".bin"), Path.Combine(FOLDER_NAME, newTaskTime.Ticks.ToString() + ".bin"));
                }
                catch (Exception e)
                {
                    // delete the old file and create a new one.
                    try
                    {
                        File.Delete(oldTaskTime.Ticks.ToString() + ".bin");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Failed to delete task function file: " + oldTaskTime.Ticks.ToString() + ".bin" + "\r\nError: " + ex.Message);
                    }
                    saveFileList = false;
                    saveTask(newTaskTime);
                }
                if (saveFileList) //if no errors has occured then we save the list, else "saveTask" will already save the list.
                {
                    saveTaskList();
                }
            }
        }

        /// <summary>
        /// Saves the task for the long term serializing it into a file.
        /// </summary>
        /// <param name="taskDT">the execution time of the task</param>
        private static void saveTask(DateTime taskDT)
        {
            bool success = false;
            FileStream fs = null;
            BinaryFormatter formatter = new BinaryFormatter();

            string fileName = taskDT.Ticks.ToString();
            try
            {
                fs = new FileStream(Path.Combine(FOLDER_NAME, fileName + ".bin"), FileMode.Create);
                MyFunction myFunc;
                lock (jobsQueueLock)
                {
                    myFunc = jobsQueue[taskDT].myFunction;
                }
                formatter.Serialize(fs, new SerializeDelegate(myFunc));
                success = true;
            }
            catch (SerializationException se) //serialization failed - delete the created file.
            {
                fs.Close();
                try
                {
                    File.Delete(Path.Combine(FOLDER_NAME, fileName + ".bin")); //try deleting the file if it was created but failed to serialize
                }
                catch (Exception e) //failed to delete the saved file for some reason
                {                           
                    System.Windows.Forms.MessageBox.Show("Failed to delete file: " + fileName + ".bin" + "\r\nError: " + e.Message);
                }
                System.Windows.Forms.MessageBox.Show("Added function is non-serializable!\r\nError: " + se.Message);
                return;
            }
            catch (Exception e) //file stream opening failed
            {
                System.Windows.Forms.MessageBox.Show("Failed to save task list.\r\nError: " + e.Message);
            }                       
            if (fs != null) //close the file stream if it was opened
            {
                fs.Close();
            }
            lock (jobsQueueLock) //define if the function is serializable
            {
                if (success)
                {
                    jobsQueue[taskDT].serializable = true;
                }
                else
                {
                    jobsQueue[taskDT].serializable = false;
                }
            }
            if (success) //save new task list only if i succeeded to serialize it
            {
                saveTaskList(); 
            }

        }

        /// <summary>
        /// Saves the task list into a file
        /// </summary>
        private static void saveTaskList()
        {
            int counter = 0;
            string[] taskList;
            lock (jobsQueueLock)
            {
                taskList = new string[jobsQueue.Count];
                //for each task in the queue, save the details inside a file
                foreach (KeyValuePair<DateTime, JobToDo> task in jobsQueue)
                {
                    if (!task.Value.serializable) //only saving serializable funtions
                    {
                        continue;
                    }
                    string taskLine = task.Key.Ticks.ToString() + "\t" + task.Value.myRepetition + "\t" + task.Value.myName;
                    taskList[counter++] = taskLine;
                }
            }
            try
            {               
                File.WriteAllLines(TASK_LIST_FILE_NAME, taskList);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Failed to save tasks list.\r\nError: " + e.Message);
            }
        }

        /// <summary>
        /// Loads the written task list to the jobs queue.
        /// Exception thrown if some files been invalid/changed or if the list file was deleted.
        /// </summary>
        private static void loadTaskList()
        {           
            //update the task list if any errors has occured. use bool over IO functoins
            bool errors = false;
            string[] taskList = null;
            try
            {
                //clean all the empty array elements that were not serializable;
                taskList = File.ReadAllLines(TASK_LIST_FILE_NAME).Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
            }
            catch(Exception excep)
            {
                System.Windows.Forms.MessageBox.Show("Failed to load task list file.\r\nError: " + excep.Message);
                return;
            }
                    
            FileStream fs = null;
            BinaryFormatter formatter = new BinaryFormatter();
            int lineNumber = 1; //lines counter incase there are issues parsing the TaskListFile

            foreach(string taskInfo in taskList)
            {
                string[] taskDetails = taskInfo.Split('\t'); //[0] TaskTime, [1] TaskRepetition, [2] TaskName
                if(taskDetails.Length > 2)
                {
                    //try deserializing the functions files
                    try
                    {                                                  //TaskTime
                        fs = new FileStream(Path.Combine(FOLDER_NAME, taskDetails[0] + ".bin"), FileMode.Open);
                        SerializeDelegate sd2 = (SerializeDelegate)formatter.Deserialize(fs);
                        MyFunction myfunc = (MyFunction)sd2.Delegate;
                        fs.Close();
                        Repetition repetition;
                                //TaskRepetition
                        switch (taskDetails[1]) //add cases here if you want to add other repetition spans.
                        {
                            case "MINUTELY": repetition = Repetition.MINUTELY; break;
                            case "HOURLY": repetition = Repetition.HOURLY; break;
                            case "DAILY": repetition = Repetition.DAILY; break;
                            case "WEEKLY": repetition = Repetition.WEEKLY; break;
                            case "MONTHLY": repetition = Repetition.MONTHLY; break;
                            case "YEARLY": repetition = Repetition.YEARLY; break;
                            default: repetition = Repetition.NEVER; break;
                        }
                                                                //TaskTime
                        DateTime newTaskTime = getDateFromString(taskDetails[0], repetition);
                        bool finishedAdding = false;
                        while (!finishedAdding)
                        {
                            try
                            {   //try adding the job to the queue
                                lock (jobsQueueLock)
                                {                                       //TaskName
                                    jobsQueue.Add(newTaskTime, new JobToDo(taskDetails[2], repetition, myfunc, true));
                                }
                                finishedAdding = true;
                            }
                            catch (Exception e) //it is a problem if the job owner spams all his tasks to be executed at the exact same time untill the milisec
                            {
                                //exceptoion might be caused if the Key already exists. therefore we will add a milisec untill new key untill we find the next free key.
                                newTaskTime = newTaskTime.AddMilliseconds(1);
                            }
                        }
                        //update the task execution files that are no longer relevant.           
                        //TaskTime string
                        updateTask(new DateTime(long.Parse(taskDetails[0].Trim())), newTaskTime);
                    }
                    catch (SerializationException se) //reached here if the deserializing failed.
                    {
                        System.Windows.Forms.MessageBox.Show("Failed to deserialize.\r\nError: " + se.Message);
                        try
                        {
                            errors = true;
                            fs.Close(); //TaskTime
                            File.Delete(Path.Combine(FOLDER_NAME,taskDetails[0] + ".bin")); //try deleting the file of the function -> not necessary if you want to keep old version serialized files.
                            continue;
                        }
                        catch (Exception e)
                        {                                                                   //TaskTime
                            System.Windows.Forms.MessageBox.Show("Failed to delete file: " + taskDetails[0] + ".bin" + "\r\nError: " + e.Message);
                        }                       
                    }
                    catch (ArgumentException ae)
                    {
                        //Reached here if there was a function to be executed once only and its time passed already.
                        try
                        {
                            errors = true;
                            fs.Close(); //TaskTime
                            File.Delete(Path.Combine(FOLDER_NAME, taskDetails[0] + ".bin")); //try deleting the file of the function
                            continue;
                        }
                        catch (Exception e)
                        {                                                                    //TaskTime
                            System.Windows.Forms.MessageBox.Show("Failed to delete file: " + taskDetails[0] + ".bin" + "\r\nError: " + e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        errors = true;
                        System.Windows.Forms.MessageBox.Show("Failed to load task file.\r\nError: " + e.Message);
                    }
                }
                else //parsing gave wrong result -> the line have been changed manualy from outside.
                {
                    errors = true;
                    System.Windows.Forms.MessageBox.Show("Failed to parse file list at line: " + lineNumber);
                }
                lineNumber++;
            }
            if (errors) //if the task list have changed, 
            {
                saveTaskList();
            }
        }

        /// <summary>
        /// Get the next correct DateTime of the task depending on its repetition.
        /// </summary>
        /// <param name="dateString"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        private static DateTime getDateFromString(string dateString, Repetition repetition)
        {
            DateTime newTaskDate = new DateTime(long.Parse(dateString.Trim()));
            DateTime currentDate = DateTime.Now;
            int daysApart = (currentDate - newTaskDate).Days;
            int hoursApart = (currentDate - newTaskDate).Hours;
            int minutesApart = (currentDate - newTaskDate).Minutes;
            //maybe i loaded a function that time has already passed, i need to find its new time.
            //worst case this loop will be executed few times, if the amount i added was not enough -> hence the MAX value of a year/month
            while (newTaskDate < currentDate)
            {
                switch (repetition) //add cases here if you want to add other repetition spans. 
                {
                    case Repetition.MINUTELY: newTaskDate = newTaskDate.AddMinutes(minutesApart + 1); break;
                    case Repetition.HOURLY: newTaskDate = newTaskDate.AddHours(hoursApart + 1); break;
                    case Repetition.DAILY: newTaskDate = newTaskDate.AddDays(daysApart + 1); break;
                    case Repetition.WEEKLY: newTaskDate = newTaskDate.AddDays(daysApart / 7 + 1); break;
                    case Repetition.MONTHLY: newTaskDate = newTaskDate.AddMonths(daysApart / 31 + 1); break;
                    case Repetition.YEARLY: newTaskDate = newTaskDate.AddYears(daysApart / 366 + 1); break;
                    default: throw new ArgumentException("Past time non repeating functions cant be executed.", "repetition");
                }
            }
            return newTaskDate;
        }
        #endregion

        #region GUI functions - not really needed, just showing the scheduler in the gui - should not be included unless scheduler needs GUI
        /// <summary>
        /// For UI purpose only, getting all the tasks that are in the scheduler
        /// </summary>
        /// <returns></returns>
        public static string[] getTaskList()
        {
            string[] taskList;
            if (scheduling)
            {
                int counter = 0;              
                lock (jobsQueueLock)
                {
                    taskList = new string[jobsQueue.Count];
                    foreach (KeyValuePair<DateTime, JobToDo> pair in jobsQueue)
                    {
                        string task = pair.Key.ToString() + " ; " + pair.Value.myRepetition + " ; " + pair.Value.myName;
                        taskList[counter++] = task;
                    }
                }
            }
            else
            {
                taskList = new string[0];
            }
            return taskList;
        }
        #endregion

        #region Helper class to serialize all serializable functions
        /// <summary>
        /// ★COPYRIGHT / LICENSING★
        /// All rights belong to their respective owners.
        /// Anonymous Method Serialization by Fredrik Norén, 12 Feb 2009
        /// https://www.codeproject.com/Articles/33345/Anonymous-Method-Serialization
        /// This article, along with any associated source code and files, is licensed under The Code Project Open License (CPOL)
        /// </summary>
        [Serializable]
        private class SerializeDelegate : ISerializable
        {
            internal SerializeDelegate(Delegate delegate_)
            {
                this.delegate_ = delegate_;
            }

            internal SerializeDelegate(SerializationInfo info, StreamingContext context)
            {
                Type delType = (Type)info.GetValue("delegateType", typeof(Type));

                //If it's a "simple" delegate we just read it straight off
                if (info.GetBoolean("isSerializable"))
                    this.delegate_ = (Delegate)info.GetValue("delegate", delType);

                //otherwise, we need to read its anonymous class
                else
                {
                    MethodInfo method = (MethodInfo)info.GetValue("method", typeof(MethodInfo));

                    AnonymousClassWrapper w =
                        (AnonymousClassWrapper)info.GetValue
                    ("class", typeof(AnonymousClassWrapper));

                    delegate_ = Delegate.CreateDelegate(delType, w.obj, method);
                }
            }

            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("delegateType", delegate_.GetType());

                //If it's an "simple" delegate we can serialize it directly
                if ((delegate_.Target == null ||
                    delegate_.Method.DeclaringType
                        .GetCustomAttributes(typeof(SerializableAttribute), false).Length > 0) &&
                    delegate_ != null)
                {
                    info.AddValue("isSerializable", true);
                    info.AddValue("delegate", delegate_);
                }

                //otherwise, serialize anonymous class
                else
                {
                    info.AddValue("isSerializable", false);
                    info.AddValue("method", delegate_.Method);
                    info.AddValue("class",
                        new AnonymousClassWrapper
                (delegate_.Method.DeclaringType, delegate_.Target));
                }
            }

            public Delegate Delegate { get { return delegate_; } }

            Delegate delegate_;

            [Serializable]
            class AnonymousClassWrapper : ISerializable
            {
                internal AnonymousClassWrapper(Type bclass, object bobject)
                {
                    this.type = bclass;
                    this.obj = bobject;
                }

                internal AnonymousClassWrapper(SerializationInfo info, StreamingContext context)
                {
                    Type classType = (Type)info.GetValue("classType", typeof(Type));
                    obj = Activator.CreateInstance(classType);

                    foreach (FieldInfo field in classType.GetFields())
                    {
                        //If the field is a delegate
                        if (typeof(Delegate).IsAssignableFrom(field.FieldType))
                            field.SetValue(obj,
                                ((SerializeDelegate)info.GetValue
                        (field.Name, typeof(SerializeDelegate)))
                                    .Delegate);
                        //If the field is an anonymous class
                        else if (!field.FieldType.IsSerializable)
                            field.SetValue(obj,
                                ((AnonymousClassWrapper)info.GetValue
                        (field.Name, typeof(AnonymousClassWrapper)))
                                    .obj);
                        //otherwise
                        else
                            field.SetValue(obj, info.GetValue(field.Name, field.FieldType));
                    }
                }

                void ISerializable.GetObjectData
                (SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("classType", type);

                    foreach (FieldInfo field in type.GetFields())
                    {
                        //See corresponding comments above
                        if (typeof(Delegate).IsAssignableFrom(field.FieldType))
                            info.AddValue(field.Name, new SerializeDelegate
                            ((Delegate)field.GetValue(obj)));
                        else if (!field.FieldType.IsSerializable)
                            info.AddValue(field.Name, new AnonymousClassWrapper
                        (field.FieldType, field.GetValue(obj)));
                        else
                            info.AddValue(field.Name, field.GetValue(obj));
                    }
                }

                public Type type;
                public object obj;
            }
        }
        #endregion
    }
}
