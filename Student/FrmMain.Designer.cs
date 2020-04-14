namespace Student
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.prgFile = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mởToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MiniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.RunWindowItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnListen = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnHide = new System.Windows.Forms.Button();
            this.p = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChat = new System.Windows.Forms.Button();
            this.btnChatServer = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(76, 19);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(93, 20);
            this.txtFile.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(185, 19);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 24);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Gửi File";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Đường dẫn :";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(11, 78);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(128, 13);
            this.lblMessage.TabIndex = 39;
            this.lblMessage.Text = "Hãy chọn file bạn cần gửi";
            // 
            // prgFile
            // 
            this.prgFile.Location = new System.Drawing.Point(7, 48);
            this.prgFile.Name = "prgFile";
            this.prgFile.Size = new System.Drawing.Size(253, 23);
            this.prgFile.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(380, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "VER 30.04.2019";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Hệ thống sinh viên";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mởToolStripMenuItem,
            this.MiniToolStripMenuItem,
            this.tsmiExit,
            this.RunWindowItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 92);
            // 
            // mởToolStripMenuItem
            // 
            this.mởToolStripMenuItem.Name = "mởToolStripMenuItem";
            this.mởToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.mởToolStripMenuItem.Text = "Mở";
            this.mởToolStripMenuItem.Click += new System.EventHandler(this.mởToolStripMenuItem_Click);
            // 
            // MiniToolStripMenuItem
            // 
            this.MiniToolStripMenuItem.CheckOnClick = true;
            this.MiniToolStripMenuItem.Name = "MiniToolStripMenuItem";
            this.MiniToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.MiniToolStripMenuItem.Text = "Thu nhỏ";
            this.MiniToolStripMenuItem.Click += new System.EventHandler(this.MiniToolStripMenuItem_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(178, 22);
            this.tsmiExit.Text = "Thoát";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // RunWindowItem
            // 
            this.RunWindowItem.Name = "RunWindowItem";
            this.RunWindowItem.Size = new System.Drawing.Size(178, 22);
            this.RunWindowItem.Text = "Chạy cùng Window";
            this.RunWindowItem.Click += new System.EventHandler(this.RunWindowItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(107, 14);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(52, 23);
            this.btnListen.TabIndex = 7;
            this.btnListen.Text = "Start";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(46, 15);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(53, 20);
            this.txtHost.TabIndex = 6;
            this.txtHost.Text = "8080";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Port";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnListen);
            this.groupBox3.Controls.Add(this.txtHost);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(295, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 42);
            this.groupBox3.TabIndex = 95;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Share Server";
            // 
            // btnHide
            // 
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHide.Location = new System.Drawing.Point(410, 75);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(50, 19);
            this.btnHide.TabIndex = 96;
            this.btnHide.Text = "Hide";
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // p
            // 
            this.p.Location = new System.Drawing.Point(308, 102);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(66, 18);
            this.p.TabIndex = 97;
            this.p.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFile);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.prgFile);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 102);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send File";
            // 
            // btnChat
            // 
            this.btnChat.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChat.Location = new System.Drawing.Point(294, 76);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(50, 20);
            this.btnChat.TabIndex = 99;
            this.btnChat.Text = "Chat R";
            this.btnChat.UseVisualStyleBackColor = false;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnChatServer
            // 
            this.btnChatServer.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnChatServer.Location = new System.Drawing.Point(350, 76);
            this.btnChatServer.Name = "btnChatServer";
            this.btnChatServer.Size = new System.Drawing.Size(50, 20);
            this.btnChatServer.TabIndex = 100;
            this.btnChatServer.Text = "Chat S";
            this.btnChatServer.UseVisualStyleBackColor = false;
            this.btnChatServer.Click += new System.EventHandler(this.btnChatServer_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 128);
            this.Controls.Add(this.btnChatServer);
            this.Controls.Add(this.btnChat);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.p);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hỗ trợ giảng dạy";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ProgressBar prgFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MiniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.Button btnHide;
        internal System.Windows.Forms.Label p;
        private System.Windows.Forms.ToolStripMenuItem mởToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem RunWindowItem;
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.Button btnChatServer;
    }
}

