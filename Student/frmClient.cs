using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
namespace Student
{
    public partial class frmClient : Form
    {
        ClassTool ct = new ClassTool();
        public frmClient()
        {
            InitializeComponent();
        }

        public static bool close = false;
        public static string file = ",0";
        public static int count = 0;
        private static byte[] data = new byte[1024];

        Thread trSendMessage;
        string information;
        /// <summary>
        /// Gửi thông tin tới máy cần gửi
        /// </summary>
        public void SendMessage()
        {
            int port = 6400;
            try
            {
                TcpClient tcpCli = new TcpClient(IPsverver(), port);
                NetworkStream ns = tcpCli.GetStream();
                //Gửi thông tin tới các thành viên trong nhóm
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(information);
                sw.Flush();
                sw.Close();
                ns.Close();
                trSendMessage.Abort();
            }
            catch
            { trSendMessage.Abort(); }

        }

        void SendDL(string x)
        {
            try
            {
                byte[] message = UnicodeEncoding.UTF8.GetBytes(x);
                FrmMain.client.Send(message);
            }
            catch
            { }
        }
        private void frmClient_Load(object sender, EventArgs e)
        {
            foreach (FontFamily item in FontFamily.Families)
            {
                cboFont.Items.Add(item.Name);
            }
            cboFont.Text = "Arial";
            for (int i = 1; i <= 32; i++)
            {
                cboSize.Items.Add(i);
            }
            Program.chat = false;
            cboSize.Text = "8";
            close = false;
            FrmMain.inform = true;
        }

        
        private string IPsverver()
        {
            string Gr = "", PhongIP = "";
            Gr = ct.GetworkgroupPC();
            if (Gr == "P601")
                PhongIP = "10.0.0.9";
            if (Gr == "P602")
                PhongIP = "10.0.0.70";
            if (Gr == "P603")
                PhongIP = "10.0.0.120";
            if (Gr == "P604")
                PhongIP = "10.0.0.160";
            return PhongIP;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (FrmMain.client.Connected == true)
                {
                    string x = "";
                    if (txtSend.Text != "")
                    {
                        x = x + SystemInformation.ComputerName + " : " + txtSend.Text;      //  frmJoinGroup.FullName
                        lbtShow.AppendText("\n");
                        lbtShow.AppendText(SystemInformation.ComputerName + ": ");       //  frmJoinGroup.FullName
                        ct.AppendText(lbtShow, txtSend.Text, colorDialog1.Color, cboFont.Text, float.Parse(cboSize.Text), ttbB, ttbI, ttbU);
                        SendDL(x);
                    }
                    txtSend.Text = "";
                }
                else
                {

                    string x = "chatlan,";
                    String strHostName = Dns.GetHostName();
                    IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
                    foreach (IPAddress ipaddress in iphostentry.AddressList)
                    {
                        if (ipaddress.AddressFamily.ToString() == "InterNetwork")
                            x += ipaddress.ToString();
                        break;
                    }
                    information = x + "," + ct.GetworkgroupPC() + "," + SystemInformation.ComputerName + "," + txtSend.Text;     //  frmJoinGroup.RoomName        frmJoinGroup.FullName
                    trSendMessage = new Thread(SendMessage);
                    trSendMessage.Start();
                    lbtShow.AppendText("\n");
                    lbtShow.AppendText(SystemInformation.ComputerName + ": ");       //    frmJoinGroup.FullName
                    ct.AppendText(lbtShow, txtSend.Text, colorDialog1.Color, cboFont.Text, float.Parse(cboSize.Text), ttbB, ttbI, ttbU);
                    txtSend.Text = "";
                    //Thread.Sleep(2000);
                    //if (frmMain.client.Connected == false)
                    //{
                    //    lbtShow.AppendText("\n");
                    //    lbtShow.AppendText("Không kết nối tới giang viên");
                    //}
                }
            }
            catch
            {
                lbtShow.AppendText("\n");
                lbtShow.AppendText("Không kết nối tới giang viên");
            }

        }
        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                FrmMain.chat = false;
                file = ",0";
                count = 0;
                close = true;
                FrmMain.inform = false;
            }
            catch
            { }
        }

        private void txtSend_TextChanged(object sender, EventArgs e)
        {
            if (txtSend.Text == "")
            { btnSend.Enabled = false; }
            else
            {
                btnSend.Enabled = true;
            }
            txtSend.SelectionFont = new Font(cboFont.Text, int.Parse(cboSize.Text), ct.KieuFont(ttbB, ttbI, ttbU));
            txtSend.Focus();
            txtSend.SelectionColor = colorDialog1.Color;
        }
        #region các chức năng phụ
        /// <summary>
        /// Gửi thông nhận được để hiển thị
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] mang = file.Split(',');

            if (count < int.Parse(mang[1]))
            {
                count++;
                if (mang[0] == "Buzz")
                {
                    //this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;
                    lbtShow.AppendText("\n");
                    ct.AppendText(lbtShow, "Buzz", Color.Red, "Arial", 14, ttbB, ttbI, ttbU);
                }
                else
                {
                    //this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;
                    lbtShow.AppendText("\n");
                    lbtShow.AppendText(mang[0]);
                }

            }
        }

        private void ttbColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSend.SelectionColor = colorDialog1.Color;
                txtSend.Focus();
            }
        }

        private void ttbBuzz_Click(object sender, EventArgs e)
        {
            txtSend.Text = "Buzz";
            SendDL(txtSend.Text);
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer("Buzz.wav");
            sp.Play();
            lbtShow.AppendText("\n");
            ct.AppendText(lbtShow, "Buzz", Color.Red, "Arial", 14, ttbB, ttbI, ttbU);
            txtSend.Text = "";
        }

        private void ttbB_Click(object sender, EventArgs e)
        {

            if (ttbB.Checked == false)
            {
                ttbB.Checked = true;
            }

            else
            {
                ttbB.Checked = false;
            }
            txtSend.Focus();

        }

        private void ttbU_Click(object sender, EventArgs e)
        {
            txtSend.Focus();
            if (ttbU.Checked == false)
            {
                ttbU.Checked = true;
            }
            else
                ttbU.Checked = false;

        }
        private void ttbI_Click(object sender, EventArgs e)
        {
            txtSend.Focus();
            if (ttbI.Checked == false)
            {
                ttbI.Checked = true;
            }
            else
                ttbI.Checked = false;
        }
        #endregion
        string FileName;
        private void ttbFile_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            FileInfo TheFile = new FileInfo(filename); // Get The File Name
            FileName = TheFile.Name;
            ct.FileSize(filename);
            if (ct.size > 0)
            {

                if (ct.size <= 100000000)
                {
                    if (ct.IsFileUsedbyAnotherProcess(filename) == false)
                    {
                        try
                        {
                            FileStream fs = new FileStream(filename, FileMode.Open);
                            byte[] buffer = new byte[fs.Length];
                            int len = (int)fs.Length;
                            fs.Read(buffer, 0, len);
                            fs.Close();
                            BinaryFormatter br = new BinaryFormatter();
                            TcpClient myclient = new TcpClient(IPsverver(), 7000);       // frmMain.ip
                            NetworkStream myns = myclient.GetStream();
                            br.Serialize(myns, FileName);
                            BinaryWriter mysw = new BinaryWriter(myns);
                            mysw.Write(buffer);
                            mysw.Close();
                            myns.Close();
                            myclient.Close();
                            lbtShow.AppendText("\n");
                            ct.AppendText(lbtShow, "Bạn đã gửi thành công" + ":" + FileName, Color.Blue, "Arial", 14, ttbB, ttbI, ttbU);
                        }
                        catch
                        {
                            MessageBox.Show("Không kết nối tới máy giáo viên", "Quản lý phòng máy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File bạn cần gửi đang được mở bởi một chương trình khác", "Quản lý phòng máy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Quá dung lượng cho phép (100Mb)", "Quản lý phòng máy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }
    }
}
