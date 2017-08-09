namespace ACNetClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.open = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.connect = new System.Windows.Forms.Button();
            this.statusclient = new System.Windows.Forms.ListBox();
            this.artist_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.author_textbox = new System.Windows.Forms.TextBox();
            this.listallsong = new System.Windows.Forms.ListView();
            this.sharenhac = new System.Windows.Forms.Button();
            this.refreshlistSong = new System.Windows.Forms.Button();
            this.download = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textsearch = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(7, 60);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(94, 23);
            this.open.TabIndex = 0;
            this.open.Text = "Open";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(7, 89);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(395, 86);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(235, 31);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(89, 23);
            this.connect.TabIndex = 4;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // statusclient
            // 
            this.statusclient.FormattingEnabled = true;
            this.statusclient.Location = new System.Drawing.Point(7, 312);
            this.statusclient.Name = "statusclient";
            this.statusclient.Size = new System.Drawing.Size(276, 95);
            this.statusclient.TabIndex = 5;
            // 
            // artist_textbox
            // 
            this.artist_textbox.Location = new System.Drawing.Point(449, 86);
            this.artist_textbox.Name = "artist_textbox";
            this.artist_textbox.Size = new System.Drawing.Size(100, 20);
            this.artist_textbox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Artist:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Author:";
            // 
            // author_textbox
            // 
            this.author_textbox.Location = new System.Drawing.Point(452, 124);
            this.author_textbox.Name = "author_textbox";
            this.author_textbox.Size = new System.Drawing.Size(100, 20);
            this.author_textbox.TabIndex = 9;
            // 
            // listallsong
            // 
            this.listallsong.Location = new System.Drawing.Point(7, 181);
            this.listallsong.Name = "listallsong";
            this.listallsong.Size = new System.Drawing.Size(395, 128);
            this.listallsong.TabIndex = 11;
            this.listallsong.UseCompatibleStateImageBehavior = false;
            // 
            // sharenhac
            // 
            this.sharenhac.Location = new System.Drawing.Point(143, 60);
            this.sharenhac.Name = "sharenhac";
            this.sharenhac.Size = new System.Drawing.Size(105, 23);
            this.sharenhac.TabIndex = 13;
            this.sharenhac.Text = "Share";
            this.sharenhac.UseVisualStyleBackColor = true;
            this.sharenhac.Click += new System.EventHandler(this.sharenhac_Click);
            // 
            // refreshlistSong
            // 
            this.refreshlistSong.Location = new System.Drawing.Point(289, 60);
            this.refreshlistSong.Name = "refreshlistSong";
            this.refreshlistSong.Size = new System.Drawing.Size(113, 23);
            this.refreshlistSong.TabIndex = 14;
            this.refreshlistSong.Text = "Refresh songlist";
            this.refreshlistSong.UseVisualStyleBackColor = true;
            this.refreshlistSong.Click += new System.EventHandler(this.refreshlistSong_Click_1);
            // 
            // download
            // 
            this.download.Location = new System.Drawing.Point(316, 351);
            this.download.Name = "download";
            this.download.Size = new System.Drawing.Size(75, 23);
            this.download.TabIndex = 15;
            this.download.Text = "Download";
            this.download.UseVisualStyleBackColor = true;
            this.download.Click += new System.EventHandler(this.download_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(411, 351);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textsearch
            // 
            this.textsearch.Location = new System.Drawing.Point(416, 260);
            this.textsearch.Name = "textsearch";
            this.textsearch.Size = new System.Drawing.Size(195, 20);
            this.textsearch.TabIndex = 19;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(552, 222);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(59, 23);
            this.search.TabIndex = 20;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Title",
            "Artist"});
            this.comboBox1.Location = new System.Drawing.Point(416, 222);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 21;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(633, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.configToolStripMenuItem.Text = "&Config";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(68, 33);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(146, 20);
            this.txtIp.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "IP server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 415);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.sharenhac);
            this.Controls.Add(this.open);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.search);
            this.Controls.Add(this.textsearch);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.download);
            this.Controls.Add(this.refreshlistSong);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.listallsong);
            this.Controls.Add(this.author_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.artist_textbox);
            this.Controls.Add(this.statusclient);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.connect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ACNetClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button open;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.ListBox statusclient;
        private System.Windows.Forms.TextBox artist_textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox author_textbox;
        private System.Windows.Forms.ListView listallsong;
        private System.Windows.Forms.Button sharenhac;
        private System.Windows.Forms.Button refreshlistSong;
        private System.Windows.Forms.Button download;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textsearch;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label3;
    }
}

