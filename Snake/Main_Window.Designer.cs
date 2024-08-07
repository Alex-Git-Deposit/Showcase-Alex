namespace Snake
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
            FieldSizeComboBox = new ComboBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            Start_Button = new Button();
            End_Button = new Button();
            Pause_Button = new Button();
            Speed = new ComboBox();
            Current_Score = new RichTextBox();
            Score_Board = new Button();
            Highscore_Display = new RichTextBox();
            HS_Textbox = new TextBox();
            Textname = new TextBox();
            Sound_Pick = new Button();
            SuspendLayout();
            // 
            // FieldSizeComboBox
            // 
            FieldSizeComboBox.Font = new Font("Segoe UI", 18F);
            FieldSizeComboBox.FormattingEnabled = true;
            FieldSizeComboBox.Items.AddRange(new object[] { "20x20", "40x40", "60x60", "80x80", "100x100" });
            FieldSizeComboBox.Location = new Point(21, 44);
            FieldSizeComboBox.Name = "FieldSizeComboBox";
            FieldSizeComboBox.Size = new Size(185, 40);
            FieldSizeComboBox.TabIndex = 5;
            FieldSizeComboBox.Text = "Feld wählen ->";
            FieldSizeComboBox.SelectedIndexChanged += Feld_comboBox1_SelectedIndexChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.ActiveCaptionText;
            flowLayoutPanel1.Location = new Point(247, 44);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(500, 500);
            flowLayoutPanel1.TabIndex = 6;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            flowLayoutPanel1.Leave += flowLayoutPanel1_Leave;
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
            Pause_Button.Size = new Size(132, 37);
            Pause_Button.TabIndex = 9;
            Pause_Button.Text = "Pause";
            Pause_Button.UseVisualStyleBackColor = true;
            Pause_Button.Click += Pause_button_Click;
            // 
            // Speed
            // 
            Speed.Font = new Font("Segoe UI", 16F);
            Speed.FormattingEnabled = true;
            Speed.ItemHeight = 30;
            Speed.Items.AddRange(new object[] { "Tempo x 0", "Tempo x 1", "Tempo x 2", "Tempo x 4", "Tempo x 8", "Tempo x 10" });
            Speed.Location = new Point(21, 119);
            Speed.Name = "Speed";
            Speed.Size = new Size(185, 38);
            Speed.TabIndex = 10;
            Speed.Text = "Tempo wählen ->";
            Speed.SelectedIndexChanged += Tempo_comboBox2_SelectedIndexChanged;
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
            Current_Score.KeyPress += Current_Score_KeyPress;
            Current_Score.Leave += Current_Score_Leave;
            // 
            // Score_Board
            // 
            Score_Board.Location = new Point(21, 396);
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
            // Sound_Pick
            // 
            Sound_Pick.Location = new Point(21, 436);
            Sound_Pick.Name = "Sound_Pick";
            Sound_Pick.Size = new Size(90, 34);
            Sound_Pick.TabIndex = 14;
            Sound_Pick.Text = "Sound";
            Sound_Pick.UseVisualStyleBackColor = true;
            /*Sound_Pick.Click += Sound_Pick_Click;*/
            // 
            // Main_Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(785, 573);
            Controls.Add(Sound_Pick);
            Controls.Add(Highscore_Display);
            Controls.Add(Score_Board);
            Controls.Add(Current_Score);
            Controls.Add(Speed);
            Controls.Add(Pause_Button);
            Controls.Add(End_Button);
            Controls.Add(Start_Button);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(FieldSizeComboBox);
            Name = "Main_Window";
            Text = "Snake";
            Load += Form1_Load;
            KeyDown += Main_Window_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox FieldSizeComboBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button Start_Button;
        private Button End_Button;
        private Button Pause_Button;
        private ComboBox Speed;
        private RichTextBox Current_Score;
        private Button Score_Board;
        private RichTextBox Highscore_Display;
        public TextBox HS_Textbox;
        private TextBox Textname;
        private Button Sound_Pick;
    }
}
