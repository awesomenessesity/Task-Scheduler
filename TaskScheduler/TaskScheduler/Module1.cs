using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    [Serializable]
    class Module1
    {
        public string fileString = "";
        public Module1()
        {
        }

        private void writeToFile(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
        }

        private void readFromFile(string filePath)
        {
            fileString = File.ReadAllText(filePath);
            System.Windows.Forms.MessageBox.Show("File Read: " + fileString);
        }

        public void scheduleFileWritting(DateTime dt, string taskName, MyTaskScheduler.Repetition rp, string filePath, string text)
        {
            MyTaskScheduler.addTask(dt, taskName, rp, () => writeToFile(filePath, text));
        }

        public void scheduleFileReading(DateTime dt, string taskName, MyTaskScheduler.Repetition rp, string filePath)
        {
            MyTaskScheduler.addTask(dt, taskName, rp, () => readFromFile(filePath));
        }

        public void scheduleFileWrittingFromInsideTheClass(DateTime dt, string taskName, MyTaskScheduler.Repetition rp, string filePath)
        {
            MyTaskScheduler.addTask(dt, taskName, rp, () => writeToFile(filePath, fileString));
        }
    }
}
