namespace Student
{
    partial class frmChatRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChatRoom));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridIP = new System.Windows.Forms.DataGridView();
            this.btnSend = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ttbB = new System.Windows.Forms.ToolStripButton();
            this.ttbI = new System.Windows.Forms.ToolStripButton();
            this.ttbU = new System.Windows.Forms.ToolStripButton();
            this.ttbColor = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.ttbImgare = new System.Windows.Forms.ToolStripButton();
            this.ttbT = new System.Windows.Forms.ToolStripButton();
            this.ttbBuzz = new System.Windows.Forms.ToolStripButton();
            this.cboFont = new System.Windows.Forms.ComboBox();
            this.cboSize = new System.Windows.Forms.ComboBox();
            this.lbtShow = new System.Windows.Forms.RichTextBox();
            this.txtSend = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnF5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridIP)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnF5);
            this.groupBox1.Controls.Add(this.GridIP);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Controls.Add(this.cboFont);
            this.groupBox1.Controls.Add(this.cboSize);
            this.groupBox1.Controls.Add(this.lbtShow);
            this.groupBox1.Controls.Add(this.txtSend);
            this.groupBox1.Location = new System.Drawing.Point(2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 397);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trao đổi thông tin nhóm";
            // 
            // GridIP
            // 
            this.GridIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridIP.Location = new System.Drawing.Point(302, 19);
            this.GridIP.Name = "GridIP";
            this.GridIP.Size = new System.Drawing.Size(181, 256);
            this.GridIP.TabIndex = 87;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(423, 340);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(60, 44);
            this.btnSend.TabIndex = 86;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ttbB,
            this.ttbI,
            this.ttbU,
            this.ttbColor});
            this.toolStrip1.Location = new System.Drawing.Point(9, 283);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(104, 25);
            this.toolStrip1.TabIndex = 84;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ttbB
            // 
            this.ttbB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ttbB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ttbB.Image = ((System.Drawing.Image)(resources.GetObject("ttbB.Image")));
            this.ttbB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbB.Name = "ttbB";
            this.ttbB.Size = new System.Drawing.Size(23, 22);
            this.ttbB.Text = "B";
            this.ttbB.Click += new System.EventHandler(this.ttbB_Click);
            // 
            // ttbI
            // 
            this.ttbI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ttbI.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ttbI.Image = ((System.Drawing.Image)(resources.GetObject("ttbI.Image")));
            this.ttbI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbI.Name = "ttbI";
            this.ttbI.Size = new System.Drawing.Size(23, 22);
            this.ttbI.Text = "I";
            this.ttbI.Click += new System.EventHandler(this.ttbI_Click);
            // 
            // ttbU
            // 
            this.ttbU.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ttbU.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ttbU.Image = ((System.Drawing.Image)(resources.GetObject("ttbU.Image")));
            this.ttbU.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbU.Name = "ttbU";
            this.ttbU.Size = new System.Drawing.Size(23, 22);
            this.ttbU.Text = "U";
            this.ttbU.Click += new System.EventHandler(this.ttbU_Click);
            // 
            // ttbColor
            // 
            this.ttbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ttbColor.Image = ((System.Drawing.Image)(resources.GetObject("ttbColor.Image")));
            this.ttbColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbColor.Name = "ttbColor";
            this.ttbColor.Size = new System.Drawing.Size(23, 22);
            this.ttbColor.Text = "Chọn màu";
            this.ttbColor.Click += new System.EventHandler(this.ttbColor_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ttbImgare,
            this.ttbT,
            this.ttbBuzz});
            this.toolStrip2.Location = new System.Drawing.Point(9, 308);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(81, 25);
            this.toolStrip2.TabIndex = 85;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // ttbImgare
            // 
            this.ttbImgare.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ttbImgare.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ttbImgare.Image = ((System.Drawing.Image)(resources.GetObject("ttbImgare.Image")));
            this.ttbImgare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbImgare.Name = "ttbImgare";
            this.ttbImgare.Size = new System.Drawing.Size(23, 22);
            this.ttbImgare.Text = "Trạng thái";
            this.ttbImgare.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // ttbT
            // 
            this.ttbT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ttbT.Font = new System.Drawing.Font("Snap ITC", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ttbT.Image = ((System.Drawing.Image)(resources.GetObject("ttbT.Image")));
            this.ttbT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbT.Name = "ttbT";
            this.ttbT.Size = new System.Drawing.Size(23, 22);
            this.ttbT.Text = "T";
            // 
            // ttbBuzz
            // 
            this.ttbBuzz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ttbBuzz.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ttbBuzz.Image = ((System.Drawing.Image)(resources.GetObject("ttbBuzz.Image")));
            this.ttbBuzz.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbBuzz.Name = "ttbBuzz";
            this.ttbBuzz.Size = new System.Drawing.Size(23, 22);
            this.ttbBuzz.Text = "Buzz";
            // 
            // cboFont
            // 
            this.cboFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFont.FormattingEnabled = true;
            this.cboFont.Location = new System.Drawing.Point(121, 283);
            this.cboFont.Name = "cboFont";
            this.cboFont.Size = new System.Drawing.Size(175, 21);
            this.cboFont.TabIndex = 82;
            // 
            // cboSize
            // 
            this.cboSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSize.FormattingEnabled = true;
            this.cboSize.Location = new System.Drawing.Point(301, 283);
            this.cboSize.Name = "cboSize";
            this.cboSize.Size = new System.Drawing.Size(41, 21);
            this.cboSize.TabIndex = 83;
            // 
            // lbtShow
            // 
            this.lbtShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtShow.Location = new System.Drawing.Point(11, 20);
            this.lbtShow.Name = "lbtShow";
            this.lbtShow.Size = new System.Drawing.Size(275, 255);
            this.lbtShow.TabIndex = 2;
            this.lbtShow.Text = "";
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.Location = new System.Drawing.Point(10, 337);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(395, 48);
            this.txtSend.TabIndex = 0;
            this.txtSend.Text = "";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 100000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnF5
            // 
            this.btnF5.Location = new System.Drawing.Point(345, 283);
            this.btnF5.Name = "btnF5";
            this.btnF5.Size = new System.Drawing.Size(50, 22);
            this.btnF5.TabIndex = 88;
            this.btnF5.Text = "F5";
            this.btnF5.UseVisualStyleBackColor = true;
            this.btnF5.Click += new System.EventHandler(this.btnF5_Click);
            // 
            // frmChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 404);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(513, 443);
            this.MinimumSize = new System.Drawing.Size(513, 443);
            this.Name = "frmChatRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmChatRoom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChatRoom_FormClosing);
            this.Load += new System.EventHandler(this.frmChatRoom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridIP)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ttbB;
        private System.Windows.Forms.ToolStripButton ttbI;
        private System.Windows.Forms.ToolStripButton ttbU;
        private System.Windows.Forms.ToolStripButton ttbColor;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton ttbImgare;
        private System.Windows.Forms.ToolStripButton ttbT;
        private System.Windows.Forms.ToolStripButton ttbBuzz;
        private System.Windows.Forms.ComboBox cboFont;
        private System.Windows.Forms.ComboBox cboSize;
        private System.Windows.Forms.RichTextBox lbtShow;
        private System.Windows.Forms.RichTextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.DataGridView GridIP;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnF5;
    }
}