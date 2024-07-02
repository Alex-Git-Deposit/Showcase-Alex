namespace P_Forms
{
    partial class SC
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 25F);
            button1.Location = new Point(21, 38);
            button1.Name = "button1";
            button1.Size = new Size(690, 101);
            button1.TabIndex = 0;
            button1.Text = "Strukturierter Code -->";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 25F);
            button2.Location = new Point(21, 167);
            button2.Name = "button2";
            button2.Size = new Size(690, 101);
            button2.TabIndex = 1;
            button2.Text = "Keine Abstürze!";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 25F);
            button3.Location = new Point(21, 291);
            button3.Name = "button3";
            button3.Size = new Size(690, 101);
            button3.TabIndex = 2;
            button3.Text = "Über 5.000 Zeilen";
            button3.UseVisualStyleBackColor = true;
            // 
            // SC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(743, 413);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "SC";
            Text = "SC";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
    }
}
