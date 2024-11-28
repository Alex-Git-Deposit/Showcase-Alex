namespace Tetris
{
    partial class Main_Window
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SpeedIncreaseComboBox = new ComboBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Start_Button = new Button();
            End_Button = new Button();
            Pause_Button = new Button();
            Speed_Start = new ComboBox();
            Current_Score = new RichTextBox();
            Score_Board = new Button();
            Highscore_Display = new RichTextBox();
            HS_Textbox = new TextBox();
            Textname = new TextBox();
            Store_Box = new RichTextBox();
            Next_Box = new RichTextBox();
            flow_store = new FlowLayoutPanel();
            flow_Next = new FlowLayoutPanel();
            Level_Display = new RichTextBox();
            SuspendLayout();
            // 
            // SpeedIncreaseComboBox
            // 
            SpeedIncreaseComboBox.Font = new Font("Segoe UI", 18F);
            SpeedIncreaseComboBox.FormattingEnabled = true;
            SpeedIncreaseComboBox.ImeMode = ImeMode.On;
            SpeedIncreaseComboBox.Items.AddRange(new object[] { "Fixed", "Slow", "Medium", "High", "Max" });
            SpeedIncreaseComboBox.Location = new Point(21, 44);
            SpeedIncreaseComboBox.Name = "SpeedIncreaseComboBox";
            SpeedIncreaseComboBox.Size = new Size(220, 40);
            SpeedIncreaseComboBox.TabIndex = 5;
            SpeedIncreaseComboBox.Text = "Tempo steigern ->";
            SpeedIncreaseComboBox.SelectedIndexChanged += Feld_comboBox1_SelectedIndexChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ActiveCaptionText;
            flowLayoutPanel1.Location = new Point(390, 44);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(205, 500);
            flowLayoutPanel1.TabIndex = 6;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // Start_Button
            // 
            Start_Button.Font = new Font("Segoe UI", 20F);
            Start_Button.Location = new Point(21, 252);
            Start_Button.Name = "Start_Button";
            Start_Button.Size = new Size(185, 63);
            Start_Button.TabIndex = 7;
            Start_Button.Text = "START";
            Start_Button.UseVisualStyleBackColor = true;
            Start_Button.Click += Start_button_Click;
            // 
            // End_Button
            // 
            End_Button.Location = new Point(21, 507);
            End_Button.Name = "End_Button";
            End_Button.Size = new Size(90, 37);
            End_Button.TabIndex = 8;
            End_Button.Text = "Beenden";
            End_Button.UseVisualStyleBackColor = true;
            End_Button.Click += End_button_Click;
            // 
            // Pause_Button
            // 
            Pause_Button.Location = new Point(21, 353);
            Pause_Button.Name = "Pause_Button";
            Pause_Button.Size = new Size(128, 37);
            Pause_Button.TabIndex = 9;
            Pause_Button.Text = "Pause";
            Pause_Button.UseVisualStyleBackColor = true;
            Pause_Button.Click += Pause_button_Click;
            // 
            // Speed_Start
            // 
            Speed_Start.Font = new Font("Segoe UI", 16F);
            Speed_Start.FormattingEnabled = true;
            Speed_Start.ItemHeight = 30;
            Speed_Start.Items.AddRange(new object[] { "Tempo x 0", "Tempo x 1", "Tempo x 2", "Tempo x 4", "Tempo x 8", "Tempo x 10" });
            Speed_Start.Location = new Point(21, 119);
            Speed_Start.Name = "Speed_Start";
            Speed_Start.Size = new Size(185, 38);
            Speed_Start.TabIndex = 10;
            Speed_Start.Text = "Tempo Beginn ->";
            Speed_Start.SelectedIndexChanged += Tempo_comboBox2_SelectedIndexChanged;
            // 
            // Current_Score
            // 
            Current_Score.Font = new Font("Segoe UI", 15F);
            Current_Score.Location = new Point(247, 7);
            Current_Score.Name = "Current_Score";
            Current_Score.ReadOnly = true;
            Current_Score.Size = new Size(106, 31);
            Current_Score.TabIndex = 11;
            Current_Score.Text = "Score:";
            Current_Score.KeyDown += Current_Score_KeyDown;
            // 
            // Score_Board
            // 
            Score_Board.Location = new Point(21, 418);
            Score_Board.Name = "Score_Board";
            Score_Board.Size = new Size(132, 34);
            Score_Board.TabIndex = 12;
            Score_Board.Text = "Highscores";
            Score_Board.UseVisualStyleBackColor = true;
            Score_Board.Click += Highscores_button_Click;
            // 
            // Highscore_Display
            // 
            Highscore_Display.Font = new Font("Segoe UI", 15F);
            Highscore_Display.Location = new Point(475, 4);
            Highscore_Display.Name = "Highscore_Display";
            Highscore_Display.ReadOnly = true;
            Highscore_Display.Size = new Size(272, 31);
            Highscore_Display.TabIndex = 13;
            Highscore_Display.Text = "Highscore: ";
            // 
            // HS_Textbox
            // 
            HS_Textbox.Location = new Point(0, 0);
            HS_Textbox.Name = "HS_Textbox";
            HS_Textbox.Size = new Size(100, 23);
            HS_Textbox.TabIndex = 0;
            // 
            // Textname
            // 
            Textname.Location = new Point(0, 0);
            Textname.Name = "Textname";
            Textname.Size = new Size(100, 23);
            Textname.TabIndex = 0;
            // 
            // Store_Box
            // 
            Store_Box.Font = new Font("Segoe UI", 15F);
            Store_Box.Location = new Point(249, 126);
            Store_Box.Name = "Store_Box";
            Store_Box.ReadOnly = true;
            Store_Box.Size = new Size(75, 31);
            Store_Box.TabIndex = 14;
            Store_Box.Text = "Store:";
            Store_Box.TextChanged += richTextBox1_TextChanged;
            // 
            // Next_Box
            // 
            Next_Box.Font = new Font("Segoe UI", 15F);
            Next_Box.Location = new Point(672, 120);
            Next_Box.Name = "Next_Box";
            Next_Box.ReadOnly = true;
            Next_Box.Size = new Size(75, 31);
            Next_Box.TabIndex = 15;
            Next_Box.Text = "Next:";
            // 
            // flow_store
            // 
            flow_store.BackColor = SystemColors.ActiveCaptionText;
            flow_store.Location = new Point(247, 163);
            flow_store.Name = "flow_store";
            flow_store.Size = new Size(77, 72);
            flow_store.TabIndex = 16;
            flow_store.Paint += flow_store_Paint;
            // 
            // flow_Next
            // 
            flow_Next.BackColor = SystemColors.ActiveCaptionText;
            flow_Next.Location = new Point(670, 163);
            flow_Next.Name = "flow_Next";
            flow_Next.Size = new Size(77, 72);
            flow_Next.TabIndex = 17;
            flow_Next.Paint += flow_Next_Paint;
            // 
            // Level_Display
            // 
            Level_Display.Font = new Font("Segoe UI", 15F);
            Level_Display.Location = new Point(247, 53);
            Level_Display.Name = "Level_Display";
            Level_Display.ReadOnly = true;
            Level_Display.Size = new Size(106, 31);
            Level_Display.TabIndex = 18;
            Level_Display.Text = "Level: 0";
            // 
            // Main_Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(785, 568);
            Controls.Add(Level_Display);
            Controls.Add(flow_Next);
            Controls.Add(flow_store);
            Controls.Add(Next_Box);
            Controls.Add(Store_Box);
            Controls.Add(Highscore_Display);
            Controls.Add(Score_Board);
            Controls.Add(Current_Score);
            Controls.Add(Speed_Start);
            Controls.Add(Pause_Button);
            Controls.Add(End_Button);
            Controls.Add(Start_Button);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(SpeedIncreaseComboBox);
            Name = "Main_Window";
            Text = "Tetris";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox SpeedIncreaseComboBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button Start_Button;
        private Button End_Button;
        private Button Pause_Button;
        private ComboBox Speed_Start;
        private RichTextBox Current_Score;
        private Button Score_Board;
        private RichTextBox Highscore_Display;
        public TextBox HS_Textbox;
        private TextBox Textname;
        private RichTextBox Store_Box;
        private RichTextBox Next_Box;
        private FlowLayoutPanel flow_store;
        private FlowLayoutPanel flow_Next;
        private RichTextBox Level_Display;
    }
}
