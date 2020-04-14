using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Student
{
    public partial class frmChatRoom : Form
    {
        public frmChatRoom()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private string GetworkgroupPC()    // lấy ra workgroup
        {
            string Gr = "";
            // Getting information about worke group of computer
            ManagementObjectSearcher mosComputer = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject moComputer in mosComputer.Get())
            {
                if (moComputer["Workgroup"] != null)
                    //MessageBox.Show(moComputer["Workgroup"].ToString());
                    Gr = moComputer["Workgroup"].ToString();
            }
            return Gr;
        }
        void LoadListView()
        {
            string Gr = "";
            int j = 0, l = 0;
            Gr = GetworkgroupPC();
            if (Gr == "P601")
                j = 11; l = 59;
            if (Gr == "P602")
                j = 71; l = 119;
            if (Gr == "P603")
                j = 121; l = 156;
            if (Gr == "P604")
                j = 161; l = 198;
            //
            //DataTable dt = new DataTable();
            dt.Columns.Add("IP", typeof(string));
            dt.Columns.Add("ComputerNumber", typeof(string));
            Ping p = new Ping();
            PingReply r;
            string s;
            for (int i = j; i < l; i++)
            {
                s = "10.0.0." + i.ToString();
                r = p.Send(s, 500);
                if (r.Status == IPStatus.Success)
                {
                    DataRow _ravi = dt.NewRow();
                    _ravi["IP"] = "10.0.0." + i.ToString().Trim();
                    _ravi["ComputerNumber"] = "MÁY: " + i.ToString().Trim();
                    dt.Rows.Add(_ravi);
                }
            }

            //
            if (dt.Rows.Count > 0)
                GridIP.DataSource = dt;
            else
                MessageBox.Show("Không tìm thấy máy nào trong phòng","Thông báo");
        }
        //
        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        //

        string host;
        int port;
        Thread trSendMessage;
        /// <summary>
        /// Gửi thông tin tới máy cần gửi
        /// </summary>
        public void SendMessage()
        {

            try
            {
                TcpClient tcpCli = new TcpClient(host, port);
                NetworkStream ns = tcpCli.GetStream();
                //Gửi thông tin tới các thành viên trong nhóm
                StreamWriter sw = new StreamWriter(ns);
                string x = SystemInformation.ComputerName + ":" + infomation;    // frmJoinGroup.FullName
                sw.WriteLine(x);
                sw.Flush();
                sw.Close();
                ns.Close();
                trSendMessage.Abort();
            }
            catch
            { trSendMessage.Abort(); }

        }
        #region Lắng nghe các kết nối từ máy khác

        public static Thread trlisten;
        TcpListener tcpList;
        string receivedData = "";
        private void ListenToServer()
        {
            bool LISTENING = false;
            //IPAddress localhostAddress = ipAddress;
            string[] mang = GetLocalIPAddress().Split('.');//frmJoinGroup.IP.Split('.');
            port = 604 + int.Parse(mang[3]);   //frmJoinGroup.GroupID
            //' PORT ADDRESS
            ///'''''''' making socket tcpList ''''''''''''''''

            tcpList = new TcpListener(port);
            try
            {

                tcpList.Start();
                LISTENING = true;

                while (LISTENING)
                {
                    while (tcpList.Pending() == false & LISTENING == true)
                    {
                        // Yield the CPU for a while.
                        Thread.Sleep(10);
                    }
                    if (!LISTENING)
                        break; // TODO: might not be correct. Was : Exit Do

                    TcpClient tcpCli = tcpList.AcceptTcpClient();
                    //ListBox1.Items.Add("Data from " & "128.10.20.63")
                    NetworkStream ns = tcpCli.GetStream();
                    StreamReader sr = new StreamReader(ns);
                    ///'''''' get data from client '''''''''''''''
                    receivedData = sr.ReadLine();

                    sr.Close();
                    ns.Close();
                    tcpCli.Close();
                }
                tcpList.Stop();
            }
            catch (Exception ex)
            {
                //error
                LISTENING = false;
            }
        }

        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (receivedData != "")
            {
                lbtShow.AppendText("\n");
                lbtShow.AppendText(receivedData);
                receivedData = "";
            }
        }

        string infomation = "";
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSend.Text != "")
            {
                infomation = txtSend.Text;
                for (int i = 0; i < dt.Rows.Count; i++)    //  lviChat.Items.Count
                {
                    host = dt.Rows[i]["IP"].ToString();//lviChat.Items[i].SubItems[2].Text;
                    string[] mang = host.Split('.');
                    port = 604  + int.Parse(mang[3]); // frmJoinGroup.GroupID
                    trSendMessage = new Thread(SendMessage);
                    trSendMessage.Start();
                    Thread.Sleep(100);
                }
            }
            txtSend.Text = "";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //LoadListView();
        }

        private void frmChatRoom_Load(object sender, EventArgs e)
        {

            LoadListView();
            foreach (FontFamily item in FontFamily.Families)
            {
                cboFont.Items.Add(item.Name);
            }
            cboFont.Text = "Arial";
            for (int i = 1; i <= 32; i++)
            {
                cboSize.Items.Add(i);
            }
            cboSize.Text = "12";
            //Bắt đầu lắng nghe xem có máy nào kết nối đến
            trlisten = new Thread(ListenToServer);
            trlisten.Start();
        }

        private void frmChatRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {                
                tcpList.Stop();
                trlisten.Abort();
            }
            catch
            { }
        }

        #region Các chức năng phụ
        /// <summary>
        /// In đậm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// In nghiêng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttbI_Click(object sender, EventArgs e)
        {
            if (ttbI.Checked == false)
            {
                ttbI.Checked = true;
            }

            else
            {
                ttbI.Checked = false;
            }
            txtSend.Focus();

        }
        /// <summary>
        /// Ngạch chân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttbU_Click(object sender, EventArgs e)
        {
            if (ttbI.Checked == false)
            {
                ttbI.Checked = true;
            }

            else
            {
                ttbI.Checked = false;
            }
            txtSend.Focus();

        }
        /// <summary>
        /// Chọn màu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttbColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSend.SelectionColor = colorDialog1.Color;
                txtSend.Focus();
            }
        }
        /// <summary>
        /// Buzz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttbBuzz_Click_1(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Ẩn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ttbT_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Chọn kiểu chữ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFont_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtSend.Focus();
        }

        private void cboSize_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtSend.Focus();
        }

        

        private void ttbBuzz_Click_2(object sender, EventArgs e)
        {
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer("Buzz.wav");
            sp.Play();
        }

        #endregion

        private void btnF5_Click(object sender, EventArgs e)
        {
            LoadListView();
        }
    }
}
