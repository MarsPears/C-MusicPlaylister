namespace MusicPlaylister_Server
{
    partial class ServerForm
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
            this.labelIPFeedback = new System.Windows.Forms.Label();
            this.labelHostFeedback = new System.Windows.Forms.Label();
            this.buttonHostOn = new System.Windows.Forms.Button();
            this.buttonHostOff = new System.Windows.Forms.Button();
            this.richTextBoxFeedback = new System.Windows.Forms.RichTextBox();
            this.labelFeedbackTitle = new System.Windows.Forms.Label();
            this.labelSong = new System.Windows.Forms.Label();
            this.labelUsers = new System.Windows.Forms.Label();
            this.buttonNewUser = new System.Windows.Forms.Button();
            this.buttonRemoveUser = new System.Windows.Forms.Button();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.listBoxSongs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelIPFeedback
            // 
            this.labelIPFeedback.AutoSize = true;
            this.labelIPFeedback.Location = new System.Drawing.Point(13, 13);
            this.labelIPFeedback.Name = "labelIPFeedback";
            this.labelIPFeedback.Size = new System.Drawing.Size(111, 13);
            this.labelIPFeedback.TabIndex = 0;
            this.labelIPFeedback.Text = "*IPFEEDBACKHERE*";
            // 
            // labelHostFeedback
            // 
            this.labelHostFeedback.AutoSize = true;
            this.labelHostFeedback.Location = new System.Drawing.Point(13, 35);
            this.labelHostFeedback.Name = "labelHostFeedback";
            this.labelHostFeedback.Size = new System.Drawing.Size(66, 13);
            this.labelHostFeedback.TabIndex = 1;
            this.labelHostFeedback.Text = "Hosting : Off";
            // 
            // buttonHostOn
            // 
            this.buttonHostOn.Location = new System.Drawing.Point(85, 30);
            this.buttonHostOn.Name = "buttonHostOn";
            this.buttonHostOn.Size = new System.Drawing.Size(39, 23);
            this.buttonHostOn.TabIndex = 2;
            this.buttonHostOn.Text = "Host";
            this.buttonHostOn.UseVisualStyleBackColor = true;
            this.buttonHostOn.Click += new System.EventHandler(this.buttonHostOn_Click);
            // 
            // buttonHostOff
            // 
            this.buttonHostOff.Enabled = false;
            this.buttonHostOff.Location = new System.Drawing.Point(130, 30);
            this.buttonHostOff.Name = "buttonHostOff";
            this.buttonHostOff.Size = new System.Drawing.Size(38, 23);
            this.buttonHostOff.TabIndex = 3;
            this.buttonHostOff.Text = "Stop";
            this.buttonHostOff.UseVisualStyleBackColor = true;
            // 
            // richTextBoxFeedback
            // 
            this.richTextBoxFeedback.Enabled = false;
            this.richTextBoxFeedback.Location = new System.Drawing.Point(14, 78);
            this.richTextBoxFeedback.Name = "richTextBoxFeedback";
            this.richTextBoxFeedback.Size = new System.Drawing.Size(400, 150);
            this.richTextBoxFeedback.TabIndex = 4;
            this.richTextBoxFeedback.Text = "";
            // 
            // labelFeedbackTitle
            // 
            this.labelFeedbackTitle.AutoSize = true;
            this.labelFeedbackTitle.Location = new System.Drawing.Point(179, 62);
            this.labelFeedbackTitle.Name = "labelFeedbackTitle";
            this.labelFeedbackTitle.Size = new System.Drawing.Size(55, 13);
            this.labelFeedbackTitle.TabIndex = 5;
            this.labelFeedbackTitle.Text = "Feedback";
            // 
            // labelSong
            // 
            this.labelSong.AutoSize = true;
            this.labelSong.Location = new System.Drawing.Point(94, 242);
            this.labelSong.Name = "labelSong";
            this.labelSong.Size = new System.Drawing.Size(37, 13);
            this.labelSong.TabIndex = 8;
            this.labelSong.Text = "Songs";
            // 
            // labelUsers
            // 
            this.labelUsers.AutoSize = true;
            this.labelUsers.Location = new System.Drawing.Point(300, 242);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(34, 13);
            this.labelUsers.TabIndex = 11;
            this.labelUsers.Text = "Users";
            // 
            // buttonNewUser
            // 
            this.buttonNewUser.Location = new System.Drawing.Point(252, 377);
            this.buttonNewUser.Name = "buttonNewUser";
            this.buttonNewUser.Size = new System.Drawing.Size(132, 23);
            this.buttonNewUser.TabIndex = 12;
            this.buttonNewUser.Text = "New User";
            this.buttonNewUser.UseVisualStyleBackColor = true;
            this.buttonNewUser.Click += new System.EventHandler(this.buttonNewUser_Click);
            // 
            // buttonRemoveUser
            // 
            this.buttonRemoveUser.Location = new System.Drawing.Point(252, 406);
            this.buttonRemoveUser.Name = "buttonRemoveUser";
            this.buttonRemoveUser.Size = new System.Drawing.Size(132, 23);
            this.buttonRemoveUser.TabIndex = 13;
            this.buttonRemoveUser.Text = "Remove User";
            this.buttonRemoveUser.UseVisualStyleBackColor = true;
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(221, 258);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(193, 95);
            this.listBoxUsers.TabIndex = 14;
            // 
            // listBoxSongs
            // 
            this.listBoxSongs.FormattingEnabled = true;
            this.listBoxSongs.Location = new System.Drawing.Point(14, 258);
            this.listBoxSongs.Name = "listBoxSongs";
            this.listBoxSongs.Size = new System.Drawing.Size(194, 186);
            this.listBoxSongs.TabIndex = 15;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 451);
            this.Controls.Add(this.listBoxSongs);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.buttonRemoveUser);
            this.Controls.Add(this.buttonNewUser);
            this.Controls.Add(this.labelUsers);
            this.Controls.Add(this.labelSong);
            this.Controls.Add(this.labelFeedbackTitle);
            this.Controls.Add(this.richTextBoxFeedback);
            this.Controls.Add(this.buttonHostOff);
            this.Controls.Add(this.buttonHostOn);
            this.Controls.Add(this.labelHostFeedback);
            this.Controls.Add(this.labelIPFeedback);
            this.Name = "ServerForm";
            this.Text = "SERVER-MusicPlaylister";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerForm_FormClosed);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIPFeedback;
        private System.Windows.Forms.Label labelHostFeedback;
        private System.Windows.Forms.Button buttonHostOn;
        private System.Windows.Forms.Button buttonHostOff;
        private System.Windows.Forms.RichTextBox richTextBoxFeedback;
        private System.Windows.Forms.Label labelFeedbackTitle;
        private System.Windows.Forms.Label labelSong;
        private System.Windows.Forms.Label labelUsers;
        private System.Windows.Forms.Button buttonNewUser;
        private System.Windows.Forms.Button buttonRemoveUser;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.ListBox listBoxSongs;
    }
}

