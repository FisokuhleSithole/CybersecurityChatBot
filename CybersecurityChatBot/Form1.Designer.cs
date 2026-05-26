namespace CybersecurityChatBot
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstChat;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstChat = new System.Windows.Forms.ListBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lstChat
            this.lstChat.BackColor = System.Drawing.Color.FromArgb(30, 30, 45);
            this.lstChat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstChat.ForeColor = System.Drawing.Color.White;
            this.lstChat.FormattingEnabled = true;
            this.lstChat.ItemHeight = 20;
            this.lstChat.Location = new System.Drawing.Point(12, 12);
            this.lstChat.Size = new System.Drawing.Size(560, 344);
            this.lstChat.TabIndex = 0;

            // txtUserInput
            this.txtUserInput.BackColor = System.Drawing.Color.FromArgb(50, 50, 65);
            this.txtUserInput.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserInput.ForeColor = System.Drawing.Color.White;
            this.txtUserInput.Location = new System.Drawing.Point(12, 370);
            this.txtUserInput.Size = new System.Drawing.Size(440, 27);
            this.txtUserInput.TabIndex = 1;
            this.txtUserInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserInput_KeyPress);

            // btnSend
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(458, 368);
            this.btnSend.Size = new System.Drawing.Size(50, 30);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // btnClear
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(80, 80, 95);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(514, 368);
            this.btnClear.Size = new System.Drawing.Size(58, 30);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 35);
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.lstChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cybersecurity Awareness Chatbot";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}