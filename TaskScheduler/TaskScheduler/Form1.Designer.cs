namespace TaskScheduler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStoChat = new System.Windows.Forms.Button();
            this.tbTaskChat = new System.Windows.Forms.TextBox();
            this.btnStoFile = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listOfTask = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRepetition = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSfromFile = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFileRName = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbTextToFile = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFileWName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbStringToTaskChat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblTaskName = new System.Windows.Forms.Label();
            this.tbTaskName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStoChat
            // 
            this.btnStoChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoChat.Location = new System.Drawing.Point(486, 19);
            this.btnStoChat.Name = "btnStoChat";
            this.btnStoChat.Size = new System.Drawing.Size(157, 80);
            this.btnStoChat.TabIndex = 0;
            this.btnStoChat.Text = "Add New Task (Not Serializable)";
            this.btnStoChat.UseVisualStyleBackColor = true;
            this.btnStoChat.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbTaskChat
            // 
            this.tbTaskChat.BackColor = System.Drawing.SystemColors.Window;
            this.tbTaskChat.Location = new System.Drawing.Point(250, 19);
            this.tbTaskChat.Multiline = true;
            this.tbTaskChat.Name = "tbTaskChat";
            this.tbTaskChat.ReadOnly = true;
            this.tbTaskChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTaskChat.Size = new System.Drawing.Size(230, 80);
            this.tbTaskChat.TabIndex = 1;
            // 
            // btnStoFile
            // 
            this.btnStoFile.Enabled = false;
            this.btnStoFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoFile.Location = new System.Drawing.Point(486, 19);
            this.btnStoFile.Name = "btnStoFile";
            this.btnStoFile.Size = new System.Drawing.Size(157, 71);
            this.btnStoFile.TabIndex = 2;
            this.btnStoFile.Text = "Add New Task";
            this.btnStoFile.UseVisualStyleBackColor = true;
            this.btnStoFile.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(6, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(643, 33);
            this.button3.TabIndex = 3;
            this.button3.Text = "show current tasks list";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listOfTask
            // 
            this.listOfTask.FormattingEnabled = true;
            this.listOfTask.HorizontalScrollbar = true;
            this.listOfTask.Location = new System.Drawing.Point(6, 32);
            this.listOfTask.Name = "listOfTask";
            this.listOfTask.Size = new System.Drawing.Size(643, 108);
            this.listOfTask.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Task Chat:";
            // 
            // datePicker
            // 
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePicker.Location = new System.Drawing.Point(12, 47);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(116, 20);
            this.datePicker.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.listOfTask);
            this.groupBox1.Location = new System.Drawing.Point(12, 412);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 187);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Task List";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblTaskName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbTaskName);
            this.groupBox2.Controls.Add(this.cbRepetition);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.timePicker);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.datePicker);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(661, 394);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add New Tasks";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(275, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Task Repetition:";
            // 
            // cbRepetition
            // 
            this.cbRepetition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbRepetition.FormattingEnabled = true;
            this.cbRepetition.Location = new System.Drawing.Point(278, 47);
            this.cbRepetition.Name = "cbRepetition";
            this.cbRepetition.Size = new System.Drawing.Size(122, 21);
            this.cbRepetition.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(122, -3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(497, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "Choose Date and Time before clicking on adding a new Task!";
            // 
            // timePicker
            // 
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker.Location = new System.Drawing.Point(151, 47);
            this.timePicker.Name = "timePicker";
            this.timePicker.ShowUpDown = true;
            this.timePicker.Size = new System.Drawing.Size(108, 20);
            this.timePicker.TabIndex = 24;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.btnSfromFile);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.tbFileRName);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Location = new System.Drawing.Point(6, 306);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(650, 85);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Read String from a File:";
            // 
            // btnSfromFile
            // 
            this.btnSfromFile.Enabled = false;
            this.btnSfromFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSfromFile.Location = new System.Drawing.Point(486, 13);
            this.btnSfromFile.Name = "btnSfromFile";
            this.btnSfromFile.Size = new System.Drawing.Size(158, 64);
            this.btnSfromFile.TabIndex = 16;
            this.btnSfromFile.Text = "Add New Task";
            this.btnSfromFile.UseVisualStyleBackColor = true;
            this.btnSfromFile.Click += new System.EventHandler(this.btnSfromFile_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(247, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "File Name:";
            // 
            // tbFileRName
            // 
            this.tbFileRName.BackColor = System.Drawing.SystemColors.Window;
            this.tbFileRName.Location = new System.Drawing.Point(250, 20);
            this.tbFileRName.Name = "tbFileRName";
            this.tbFileRName.ReadOnly = true;
            this.tbFileRName.Size = new System.Drawing.Size(230, 20);
            this.tbFileRName.TabIndex = 18;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(250, 54);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(230, 23);
            this.button5.TabIndex = 19;
            this.button5.Text = "Choose a file...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbTextToFile);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.tbFileWName);
            this.groupBox4.Controls.Add(this.btnStoFile);
            this.groupBox4.Location = new System.Drawing.Point(6, 201);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(650, 99);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Write String to File Task:";
            // 
            // tbTextToFile
            // 
            this.tbTextToFile.Location = new System.Drawing.Point(6, 19);
            this.tbTextToFile.Multiline = true;
            this.tbTextToFile.Name = "tbTextToFile";
            this.tbTextToFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTextToFile.Size = new System.Drawing.Size(230, 71);
            this.tbTextToFile.TabIndex = 12;
            this.tbTextToFile.Text = "Enter Text Here";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(250, 64);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(230, 26);
            this.button4.TabIndex = 15;
            this.button4.Text = "Choose a file...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(247, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "File Name:";
            // 
            // tbFileWName
            // 
            this.tbFileWName.BackColor = System.Drawing.SystemColors.Window;
            this.tbFileWName.Location = new System.Drawing.Point(250, 19);
            this.tbFileWName.Name = "tbFileWName";
            this.tbFileWName.ReadOnly = true;
            this.tbFileWName.Size = new System.Drawing.Size(230, 20);
            this.tbFileWName.TabIndex = 14;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbTaskChat);
            this.groupBox3.Controls.Add(this.tbStringToTaskChat);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnStoChat);
            this.groupBox3.Location = new System.Drawing.Point(6, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(650, 108);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Write String to Task Chat:";
            // 
            // tbStringToTaskChat
            // 
            this.tbStringToTaskChat.Location = new System.Drawing.Point(6, 19);
            this.tbStringToTaskChat.Multiline = true;
            this.tbStringToTaskChat.Name = "tbStringToTaskChat";
            this.tbStringToTaskChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbStringToTaskChat.Size = new System.Drawing.Size(230, 80);
            this.tbStringToTaskChat.TabIndex = 10;
            this.tbStringToTaskChat.Text = "Enter Text Here";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Task Date:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblTaskName
            // 
            this.lblTaskName.AutoSize = true;
            this.lblTaskName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskName.Location = new System.Drawing.Point(417, 31);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(75, 13);
            this.lblTaskName.TabIndex = 20;
            this.lblTaskName.Text = "Task Name:";
            // 
            // tbTaskName
            // 
            this.tbTaskName.Location = new System.Drawing.Point(420, 48);
            this.tbTaskName.Name = "tbTaskName";
            this.tbTaskName.Size = new System.Drawing.Size(236, 20);
            this.tbTaskName.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(148, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Task Time:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(9, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 57);
            this.textBox1.TabIndex = 21;
            this.textBox1.Text = "The content of the file will be displayed inside a MessageBox that will pop up on" +
    " the screen.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "TaskDate  ;  TaskRepetition  ;  TaskName";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 603);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStoChat;
        private System.Windows.Forms.TextBox tbTaskChat;
        private System.Windows.Forms.Button btnStoFile;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listOfTask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStringToTaskChat;
        private System.Windows.Forms.TextBox tbTextToFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tbFileRName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tbFileWName;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSfromFile;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbRepetition;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.TextBox tbTaskName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}

