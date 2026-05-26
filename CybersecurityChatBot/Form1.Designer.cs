namespace CybersecurityChatBot
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox txtChat;  // Changed from ListBox to RichTextBox
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblTitle;

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
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitle - Title Bar
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(0, 80, 150);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Size = new System.Drawing.Size(800, 60);
            this.lblTitle.Text = "  🔐 Cybersecurity Awareness Chatbot";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // txtChat - Chat Display Area (RichTextBox with word wrap)
            this.txtChat.BackColor = System.Drawing.Color.FromArgb(30, 30, 45);
            this.txtChat.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtChat.ForeColor = System.Drawing.Color.White;
            this.txtChat.Location = new System.Drawing.Point(15, 75);
            this.txtChat.Size = new System.Drawing.Size(770, 450);
            this.txtChat.TabIndex = 0;
            this.txtChat.ReadOnly = true;
            this.txtChat.WordWrap = true;  // This enables text wrapping!
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // txtUserInput - Input Box
            this.txtUserInput.BackColor = System.Drawing.Color.FromArgb(50, 50, 65);
            this.txtUserInput.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUserInput.ForeColor = System.Drawing.Color.White;
            this.txtUserInput.Location = new System.Drawing.Point(15, 540);
            this.txtUserInput.Size = new System.Drawing.Size(640, 35);
            this.txtUserInput.TabIndex = 1;
            this.txtUserInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserInput_KeyPress);

            // btnSend - Send Button
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(665, 538);
            this.btnSend.Size = new System.Drawing.Size(55, 38);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // btnClear - Clear Button
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(80, 80, 95);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(730, 538);
            this.btnClear.Size = new System.Drawing.Size(55, 38);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // Form1 - Main Window (Full Screen Ready)
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 35);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cybersecurity Awareness Chatbot";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}