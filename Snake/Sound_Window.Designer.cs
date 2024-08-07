namespace Snake
{
    partial class Sound_Window
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
            OnOff_Switch = new Button();
            Sound_Close = new Button();
            Loop_All = new Button();
            Sound_01 = new Button();
            Sound_02 = new Button();
            Sound_03 = new Button();
            SuspendLayout();
            // 
            // OnOff_Switch
            // 
            OnOff_Switch.Location = new Point(12, 12);
            OnOff_Switch.Name = "OnOff_Switch";
            OnOff_Switch.Size = new Size(126, 48);
            OnOff_Switch.TabIndex = 1;
            OnOff_Switch.Text = "Turn Sound OFF";
            OnOff_Switch.UseVisualStyleBackColor = true;
            /*OnOff_Switch.Click += OnOff_Switch_Click;*/
            // 
            // Sound_Close
            // 
            Sound_Close.DialogResult = DialogResult.OK;
            Sound_Close.Location = new Point(452, 150);
            Sound_Close.Name = "Sound_Close";
            Sound_Close.Size = new Size(103, 36);
            Sound_Close.TabIndex = 2;
            Sound_Close.Text = "OK";
            Sound_Close.UseVisualStyleBackColor = true;
            /*Sound_Close.Click += Sound_Close_Click;*/
            // 
            // Loop_All
            // 
            Loop_All.Location = new Point(14, 88);
            Loop_All.Name = "Loop_All";
            Loop_All.Size = new Size(124, 48);
            Loop_All.TabIndex = 3;
            Loop_All.Text = "Loop All";
            Loop_All.UseVisualStyleBackColor = true;
            /*Loop_All.Click += Loop_All_Click;*/
            // 
            // Sound_01
            // 
            Sound_01.Location = new Point(173, 12);
            Sound_01.Name = "Sound_01";
            Sound_01.Size = new Size(120, 48);
            Sound_01.TabIndex = 4;
            Sound_01.Text = "Sound 01";
            Sound_01.UseVisualStyleBackColor = true;
            /*Sound_01.Click += Sound_01_Click;*/
            // 
            // Sound_02
            // 
            Sound_02.Location = new Point(299, 12);
            Sound_02.Name = "Sound_02";
            Sound_02.Size = new Size(120, 48);
            Sound_02.TabIndex = 5;
            Sound_02.Text = "Sound 02";
            Sound_02.UseVisualStyleBackColor = true;
            /*Sound_02.Click += Sound_02_Click;*/
            // 
            // Sound_03
            // 
            Sound_03.Location = new Point(425, 12);
            Sound_03.Name = "Sound_03";
            Sound_03.Size = new Size(120, 48);
            Sound_03.TabIndex = 6;
            Sound_03.Text = "Sound 03";
            Sound_03.UseVisualStyleBackColor = true;
            /*Sound_03.Click += Sound_03_Click;*/
            // 
            // Sound_Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 198);
            Controls.Add(Sound_03);
            Controls.Add(Sound_02);
            Controls.Add(Sound_01);
            Controls.Add(Loop_All);
            Controls.Add(Sound_Close);
            Controls.Add(OnOff_Switch);
            Name = "Sound_Window";
            Text = "Sound";
            ResumeLayout(false);
        }

        #endregion
        private Button OnOff_Switch;
        private Button Sound_Close;
        private Button Loop_All;
        private Button Sound_01;
        private Button Sound_02;
        private Button Sound_03;
    }
}