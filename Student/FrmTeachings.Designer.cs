namespace Student
{
    partial class FrmTeachings
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
            this.Runtime = new System.Windows.Forms.Timer(this.components);
            this.pteTeaching = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pteTeaching)).BeginInit();
            this.SuspendLayout();
            // 
            // Runtime
            // 
            this.Runtime.Interval = 30;
            this.Runtime.Tick += new System.EventHandler(this.Runtime_Tick);
            // 
            // pteTeaching
            // 
            this.pteTeaching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pteTeaching.Location = new System.Drawing.Point(0, 0);
            this.pteTeaching.Name = "pteTeaching";
            this.pteTeaching.Size = new System.Drawing.Size(800, 450);
            this.pteTeaching.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pteTeaching.TabIndex = 0;
            this.pteTeaching.TabStop = false;
            // 
            // FrmTeachings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pteTeaching);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTeachings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lắng nghe bài giảng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTeachings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pteTeaching)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer Runtime;
        private System.Windows.Forms.PictureBox pteTeaching;
    }
}