using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TaskScheduler
{
    [Serializable]
    public partial class Form1 : Form
    {
        private string fileWrittingPath;
        private string fileReadingPath;
        private Module1 md;

        public Form1()
        {
            InitializeComponent();
            tbTaskChat.Text = "";
            datePicker.CustomFormat = "dd/MM/yyyy";
            timePicker.CustomFormat = "HH:mm:ss";
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.NEVER);
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.MINUTELY);
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.HOURLY);
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.DAILY);
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.WEEKLY);
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.MONTHLY);
            cbRepetition.Items.Add(MyTaskScheduler.Repetition.YEARLY);
            cbRepetition.SelectedIndex = 0;

            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FileName = "Example.txt";
            fileWrittingPath = "Example.txt";

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FileName = "Example.txt";
            fileReadingPath = "Example.txt";

            md = new Module1();

            MyTaskScheduler.startScheduling();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime taskDate = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            string textToWrite = tbStringToTaskChat.Text;
            string taskName = tbTaskName.Text;
            if (taskName.Equals(""))
            {
                taskName = "Default Task Name (You didnt choose one)";
            }
            MyTaskScheduler.Repetition repetition = (MyTaskScheduler.Repetition)cbRepetition.SelectedItem;
            try
            {
                MyTaskScheduler.addTask(taskDate, taskName, repetition, () => appendTextToChat(textToWrite));
                MessageBox.Show("Task Added");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error adding the task: " + exp.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            DateTime taskDate = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            string textToWrite = tbTextToFile.Text;
            string taskName = tbTaskName.Text;
            if (taskName.Equals(""))
            {
                taskName = "Default Task Name (You didnt choose one)";
            }
            MyTaskScheduler.Repetition repetition = (MyTaskScheduler.Repetition)cbRepetition.SelectedItem;
            string filePath = fileWrittingPath;
            try
            {
                md.scheduleFileWritting(taskDate, taskName, repetition, filePath, textToWrite);
                MessageBox.Show("Task Added");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error adding the task: " + exp.Message);
            }
            
        }

        private void btnSfromFile_Click(object sender, EventArgs e)
        {   
            DateTime taskDate = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            MyTaskScheduler.Repetition repetition = (MyTaskScheduler.Repetition)cbRepetition.SelectedItem;
            string filePath = fileReadingPath;
            string taskName = tbTaskName.Text;
            if (taskName.Equals(""))
            {
                taskName = "Default Task Name (You didnt choose one)";
            }
            try
            {
                md.scheduleFileReading(taskDate, taskName, repetition, filePath);
                MessageBox.Show("Task Added");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error adding the task: " + exp.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listOfTask.Items.Clear();
            listOfTask.Items.AddRange(MyTaskScheduler.getTaskList());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyTaskScheduler.stopScheduling();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = Path.GetFileName(fileWrittingPath);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileWrittingPath = saveFileDialog1.FileName;
                tbFileWName.Text = Path.GetFileName(fileWrittingPath);
                btnStoFile.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = Path.GetFileName(fileReadingPath);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileReadingPath = openFileDialog1.FileName;
                tbFileRName.Text = Path.GetFileName(fileReadingPath);
                btnSfromFile.Enabled = true;
            }
        }

        public void appendTextToChat(string text)
        {
            Invoke((MethodInvoker)(() => tbTaskChat.Text += text + "\r\n"));
        }

    }
}
