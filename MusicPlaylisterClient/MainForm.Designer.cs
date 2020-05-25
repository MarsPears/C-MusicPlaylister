namespace MusicPlaylisterClient
{
    partial class FormClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClient));
            this.labelUserConnected = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.listBoxSongs = new System.Windows.Forms.ListBox();
            this.labelWorkingDirectory = new System.Windows.Forms.Label();
            this.labelPlaylists = new System.Windows.Forms.Label();
            this.groupBoxSongs = new System.Windows.Forms.GroupBox();
            this.buttonWDDownload = new System.Windows.Forms.Button();
            this.buttonWDDelete = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonWDSearch = new System.Windows.Forms.Button();
            this.buttonWDUpload = new System.Windows.Forms.Button();
            this.groupBoxPlaylist = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonPLNew = new System.Windows.Forms.Button();
            this.buttonPLSearch = new System.Windows.Forms.Button();
            this.buttonPLDelete = new System.Windows.Forms.Button();
            this.treeViewPlaylists = new System.Windows.Forms.TreeView();
            this.contextMenuStripPlaylist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playFirstSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playLastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atTheBegginingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beforeSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afterSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atTheEndOfPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSongs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.labelCurrentSong = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCurrentPlaylist = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelUserConnectedSet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.groupBoxSongs.SuspendLayout();
            this.groupBoxPlaylist.SuspendLayout();
            this.contextMenuStripPlaylist.SuspendLayout();
            this.contextMenuStripSongs.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelUserConnected
            // 
            this.labelUserConnected.AutoSize = true;
            this.labelUserConnected.Location = new System.Drawing.Point(338, 57);
            this.labelUserConnected.Name = "labelUserConnected";
            this.labelUserConnected.Size = new System.Drawing.Size(93, 13);
            this.labelUserConnected.TabIndex = 2;
            this.labelUserConnected.Text = "User Connected : ";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(13, 331);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(545, 51);
            this.axWindowsMediaPlayer1.TabIndex = 3;
            // 
            // listBoxSongs
            // 
            this.listBoxSongs.FormattingEnabled = true;
            this.listBoxSongs.Location = new System.Drawing.Point(10, 44);
            this.listBoxSongs.Name = "listBoxSongs";
            this.listBoxSongs.Size = new System.Drawing.Size(291, 212);
            this.listBoxSongs.TabIndex = 4;
            this.listBoxSongs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxSongs_MouseDown);
            // 
            // labelWorkingDirectory
            // 
            this.labelWorkingDirectory.AutoSize = true;
            this.labelWorkingDirectory.Location = new System.Drawing.Point(9, 28);
            this.labelWorkingDirectory.Name = "labelWorkingDirectory";
            this.labelWorkingDirectory.Size = new System.Drawing.Size(92, 13);
            this.labelWorkingDirectory.TabIndex = 5;
            this.labelWorkingDirectory.Text = "Working Directory";
            // 
            // labelPlaylists
            // 
            this.labelPlaylists.AutoSize = true;
            this.labelPlaylists.Location = new System.Drawing.Point(13, 404);
            this.labelPlaylists.Name = "labelPlaylists";
            this.labelPlaylists.Size = new System.Drawing.Size(69, 13);
            this.labelPlaylists.TabIndex = 7;
            this.labelPlaylists.Text = "Your Playlists";
            // 
            // groupBoxSongs
            // 
            this.groupBoxSongs.Controls.Add(this.buttonWDDownload);
            this.groupBoxSongs.Controls.Add(this.buttonWDDelete);
            this.groupBoxSongs.Controls.Add(this.textBox1);
            this.groupBoxSongs.Controls.Add(this.buttonWDSearch);
            this.groupBoxSongs.Controls.Add(this.buttonWDUpload);
            this.groupBoxSongs.Location = new System.Drawing.Point(307, 94);
            this.groupBoxSongs.Name = "groupBoxSongs";
            this.groupBoxSongs.Size = new System.Drawing.Size(248, 111);
            this.groupBoxSongs.TabIndex = 8;
            this.groupBoxSongs.TabStop = false;
            this.groupBoxSongs.Text = "Song Control";
            // 
            // buttonWDDownload
            // 
            this.buttonWDDownload.Location = new System.Drawing.Point(88, 28);
            this.buttonWDDownload.Name = "buttonWDDownload";
            this.buttonWDDownload.Size = new System.Drawing.Size(70, 23);
            this.buttonWDDownload.TabIndex = 5;
            this.buttonWDDownload.Text = "Download";
            this.buttonWDDownload.UseVisualStyleBackColor = true;
            this.buttonWDDownload.Click += new System.EventHandler(this.buttonWDDownload_Click);
            // 
            // buttonWDDelete
            // 
            this.buttonWDDelete.Location = new System.Drawing.Point(164, 28);
            this.buttonWDDelete.Name = "buttonWDDelete";
            this.buttonWDDelete.Size = new System.Drawing.Size(65, 23);
            this.buttonWDDelete.TabIndex = 4;
            this.buttonWDDelete.Text = "Delete";
            this.buttonWDDelete.UseVisualStyleBackColor = true;
            this.buttonWDDelete.Click += new System.EventHandler(this.buttonWDDelete_Click);
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox1.Location = new System.Drawing.Point(19, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 20);
            this.textBox1.TabIndex = 3;
            // 
            // buttonWDSearch
            // 
            this.buttonWDSearch.Location = new System.Drawing.Point(164, 68);
            this.buttonWDSearch.Name = "buttonWDSearch";
            this.buttonWDSearch.Size = new System.Drawing.Size(65, 23);
            this.buttonWDSearch.TabIndex = 2;
            this.buttonWDSearch.Text = "Search";
            this.buttonWDSearch.UseVisualStyleBackColor = true;
            this.buttonWDSearch.Click += new System.EventHandler(this.buttonWDSearch_Click);
            // 
            // buttonWDUpload
            // 
            this.buttonWDUpload.Location = new System.Drawing.Point(20, 28);
            this.buttonWDUpload.Name = "buttonWDUpload";
            this.buttonWDUpload.Size = new System.Drawing.Size(63, 23);
            this.buttonWDUpload.TabIndex = 0;
            this.buttonWDUpload.Text = "Upload";
            this.buttonWDUpload.UseVisualStyleBackColor = true;
            this.buttonWDUpload.Click += new System.EventHandler(this.buttonWDUpload_Click);
            // 
            // groupBoxPlaylist
            // 
            this.groupBoxPlaylist.Controls.Add(this.textBox2);
            this.groupBoxPlaylist.Controls.Add(this.buttonPLNew);
            this.groupBoxPlaylist.Controls.Add(this.buttonPLSearch);
            this.groupBoxPlaylist.Controls.Add(this.buttonPLDelete);
            this.groupBoxPlaylist.Location = new System.Drawing.Point(310, 404);
            this.groupBoxPlaylist.Name = "groupBoxPlaylist";
            this.groupBoxPlaylist.Size = new System.Drawing.Size(248, 137);
            this.groupBoxPlaylist.TabIndex = 9;
            this.groupBoxPlaylist.TabStop = false;
            this.groupBoxPlaylist.Text = "Playlist Control";
            // 
            // textBox2
            // 
            this.textBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.textBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox2.Location = new System.Drawing.Point(19, 82);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(139, 20);
            this.textBox2.TabIndex = 7;
            // 
            // buttonPLNew
            // 
            this.buttonPLNew.Location = new System.Drawing.Point(19, 40);
            this.buttonPLNew.Name = "buttonPLNew";
            this.buttonPLNew.Size = new System.Drawing.Size(102, 23);
            this.buttonPLNew.TabIndex = 4;
            this.buttonPLNew.Text = "New Playlist";
            this.buttonPLNew.UseVisualStyleBackColor = true;
            this.buttonPLNew.Click += new System.EventHandler(this.buttonPLNew_Click);
            // 
            // buttonPLSearch
            // 
            this.buttonPLSearch.Location = new System.Drawing.Point(164, 80);
            this.buttonPLSearch.Name = "buttonPLSearch";
            this.buttonPLSearch.Size = new System.Drawing.Size(65, 23);
            this.buttonPLSearch.TabIndex = 6;
            this.buttonPLSearch.Text = "Search";
            this.buttonPLSearch.UseVisualStyleBackColor = true;
            this.buttonPLSearch.Click += new System.EventHandler(this.buttonPLSearch_Click);
            // 
            // buttonPLDelete
            // 
            this.buttonPLDelete.Location = new System.Drawing.Point(127, 40);
            this.buttonPLDelete.Name = "buttonPLDelete";
            this.buttonPLDelete.Size = new System.Drawing.Size(102, 23);
            this.buttonPLDelete.TabIndex = 5;
            this.buttonPLDelete.Text = "Delete";
            this.buttonPLDelete.UseVisualStyleBackColor = true;
            this.buttonPLDelete.Click += new System.EventHandler(this.buttonPLDelete_Click);
            // 
            // treeViewPlaylists
            // 
            this.treeViewPlaylists.Location = new System.Drawing.Point(14, 421);
            this.treeViewPlaylists.Name = "treeViewPlaylists";
            this.treeViewPlaylists.Size = new System.Drawing.Size(291, 120);
            this.treeViewPlaylists.TabIndex = 10;
            this.treeViewPlaylists.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewPlaylists_NodeMouseDoubleClick);
            this.treeViewPlaylists.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewPlaylists_MouseDown);
            // 
            // contextMenuStripPlaylist
            // 
            this.contextMenuStripPlaylist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playFirstSongToolStripMenuItem,
            this.playSelectedToolStripMenuItem,
            this.playLastToolStripMenuItem,
            this.insertSongToolStripMenuItem});
            this.contextMenuStripPlaylist.Name = "contextMenuStripPlaylist";
            this.contextMenuStripPlaylist.Size = new System.Drawing.Size(179, 92);
            // 
            // playFirstSongToolStripMenuItem
            // 
            this.playFirstSongToolStripMenuItem.Name = "playFirstSongToolStripMenuItem";
            this.playFirstSongToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.playFirstSongToolStripMenuItem.Text = "Play first";
            this.playFirstSongToolStripMenuItem.Click += new System.EventHandler(this.playFirstSongToolStripMenuItem_Click);
            // 
            // playSelectedToolStripMenuItem
            // 
            this.playSelectedToolStripMenuItem.Name = "playSelectedToolStripMenuItem";
            this.playSelectedToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.playSelectedToolStripMenuItem.Text = "Play selected";
            this.playSelectedToolStripMenuItem.Click += new System.EventHandler(this.playSelectedToolStripMenuItem_Click);
            // 
            // playLastToolStripMenuItem
            // 
            this.playLastToolStripMenuItem.Name = "playLastToolStripMenuItem";
            this.playLastToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.playLastToolStripMenuItem.Text = "Play last";
            this.playLastToolStripMenuItem.Click += new System.EventHandler(this.playLastToolStripMenuItem_Click);
            // 
            // insertSongToolStripMenuItem
            // 
            this.insertSongToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atTheBegginingToolStripMenuItem,
            this.beforeSelectionToolStripMenuItem,
            this.afterSelectionToolStripMenuItem,
            this.atTheEndOfPlaylistToolStripMenuItem});
            this.insertSongToolStripMenuItem.Name = "insertSongToolStripMenuItem";
            this.insertSongToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.insertSongToolStripMenuItem.Text = "Insert selected song";
            // 
            // atTheBegginingToolStripMenuItem
            // 
            this.atTheBegginingToolStripMenuItem.Name = "atTheBegginingToolStripMenuItem";
            this.atTheBegginingToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.atTheBegginingToolStripMenuItem.Text = "At the beggining";
            this.atTheBegginingToolStripMenuItem.Click += new System.EventHandler(this.atTheBegginingToolStripMenuItem_Click);
            // 
            // beforeSelectionToolStripMenuItem
            // 
            this.beforeSelectionToolStripMenuItem.Name = "beforeSelectionToolStripMenuItem";
            this.beforeSelectionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.beforeSelectionToolStripMenuItem.Text = "Before selection";
            this.beforeSelectionToolStripMenuItem.Click += new System.EventHandler(this.beforeSelectionToolStripMenuItem_Click);
            // 
            // afterSelectionToolStripMenuItem
            // 
            this.afterSelectionToolStripMenuItem.Name = "afterSelectionToolStripMenuItem";
            this.afterSelectionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.afterSelectionToolStripMenuItem.Text = "After selection";
            this.afterSelectionToolStripMenuItem.Click += new System.EventHandler(this.afterSelectionToolStripMenuItem_Click);
            // 
            // atTheEndOfPlaylistToolStripMenuItem
            // 
            this.atTheEndOfPlaylistToolStripMenuItem.Name = "atTheEndOfPlaylistToolStripMenuItem";
            this.atTheEndOfPlaylistToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.atTheEndOfPlaylistToolStripMenuItem.Text = "At the end of playlist";
            this.atTheEndOfPlaylistToolStripMenuItem.Click += new System.EventHandler(this.atTheEndOfPlaylistToolStripMenuItem_Click);
            // 
            // contextMenuStripSongs
            // 
            this.contextMenuStripSongs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playSongToolStripMenuItem});
            this.contextMenuStripSongs.Name = "contextMenuStripSongs";
            this.contextMenuStripSongs.Size = new System.Drawing.Size(126, 26);
            // 
            // playSongToolStripMenuItem
            // 
            this.playSongToolStripMenuItem.Name = "playSongToolStripMenuItem";
            this.playSongToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.playSongToolStripMenuItem.Text = "Play song";
            this.playSongToolStripMenuItem.Click += new System.EventHandler(this.playSongToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.progressBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.progressBar1.Location = new System.Drawing.Point(279, 299);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar1.Size = new System.Drawing.Size(279, 40);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar2
            // 
            this.progressBar2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.progressBar2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.progressBar2.Location = new System.Drawing.Point(13, 299);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.progressBar2.RightToLeftLayout = true;
            this.progressBar2.Size = new System.Drawing.Size(269, 40);
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 13;
            // 
            // labelCurrentSong
            // 
            this.labelCurrentSong.AutoSize = true;
            this.labelCurrentSong.Location = new System.Drawing.Point(396, 273);
            this.labelCurrentSong.Name = "labelCurrentSong";
            this.labelCurrentSong.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentSong.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Current Song :";
            // 
            // labelCurrentPlaylist
            // 
            this.labelCurrentPlaylist.AutoSize = true;
            this.labelCurrentPlaylist.Location = new System.Drawing.Point(124, 273);
            this.labelCurrentPlaylist.Name = "labelCurrentPlaylist";
            this.labelCurrentPlaylist.Size = new System.Drawing.Size(0, 13);
            this.labelCurrentPlaylist.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Current Playlist :";
            // 
            // labelUserConnectedSet
            // 
            this.labelUserConnectedSet.AutoSize = true;
            this.labelUserConnectedSet.Location = new System.Drawing.Point(431, 57);
            this.labelUserConnectedSet.Name = "labelUserConnectedSet";
            this.labelUserConnectedSet.Size = new System.Drawing.Size(0, 13);
            this.labelUserConnectedSet.TabIndex = 18;
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 547);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.labelUserConnectedSet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCurrentSong);
            this.Controls.Add(this.labelCurrentPlaylist);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.treeViewPlaylists);
            this.Controls.Add(this.groupBoxPlaylist);
            this.Controls.Add(this.groupBoxSongs);
            this.Controls.Add(this.labelPlaylists);
            this.Controls.Add(this.labelWorkingDirectory);
            this.Controls.Add(this.listBoxSongs);
            this.Controls.Add(this.labelUserConnected);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "FormClient";
            this.Text = "MusicPlaylister";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClient_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.groupBoxSongs.ResumeLayout(false);
            this.groupBoxSongs.PerformLayout();
            this.groupBoxPlaylist.ResumeLayout(false);
            this.groupBoxPlaylist.PerformLayout();
            this.contextMenuStripPlaylist.ResumeLayout(false);
            this.contextMenuStripSongs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelUserConnected;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ListBox listBoxSongs;
        private System.Windows.Forms.Label labelWorkingDirectory;
        private System.Windows.Forms.Label labelPlaylists;
        private System.Windows.Forms.GroupBox groupBoxSongs;
        private System.Windows.Forms.GroupBox groupBoxPlaylist;
        private System.Windows.Forms.Button buttonWDDownload;
        private System.Windows.Forms.Button buttonWDDelete;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonWDSearch;
        private System.Windows.Forms.Button buttonWDUpload;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonPLNew;
        private System.Windows.Forms.Button buttonPLSearch;
        private System.Windows.Forms.Button buttonPLDelete;
        private System.Windows.Forms.TreeView treeViewPlaylists;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPlaylist;
        private System.Windows.Forms.ToolStripMenuItem playFirstSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playLastToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSongs;
        private System.Windows.Forms.ToolStripMenuItem playSongToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label labelCurrentSong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCurrentPlaylist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelUserConnectedSet;
        private System.Windows.Forms.ToolStripMenuItem insertSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atTheBegginingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beforeSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afterSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atTheEndOfPlaylistToolStripMenuItem;
    }
}

