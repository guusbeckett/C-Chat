namespace ClientApp
{
    partial class Form2
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelUserNameQuestion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Location = new System.Drawing.Point(9, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 20);
            this.textBox1.TabIndex = 0;
            // 
            // labelUserNameQuestion
            // 
            this.labelUserNameQuestion.AutoSize = true;
            this.labelUserNameQuestion.Location = new System.Drawing.Point(8, 19);
            this.labelUserNameQuestion.Name = "labelUserNameQuestion";
            this.labelUserNameQuestion.Size = new System.Drawing.Size(125, 13);
            this.labelUserNameQuestion.TabIndex = 1;
            this.labelUserNameQuestion.Text = "Vul je gebruikersnaam in:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vul het server IP in:";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(9, 97);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxServerIP.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 141);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUserNameQuestion);
            this.Controls.Add(this.textBox1);
            this.Name = "Form2";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelUserNameQuestion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxServerIP;
    }
}