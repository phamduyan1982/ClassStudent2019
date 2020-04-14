using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Management;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Student
{
    public partial class FrmMain : Form
    {
        ClassTool ct = new ClassTool();   //
        public FrmMain()
        {
            InitializeComponent();
            this.ControlBox = false;   // khoa nut thoat Form
        }
        public static bool chat = false;
        //   https://nosomovo.xyz/2017/09/26/khoi-dong-ung-dung-cung-windows-trong-c/
        RegistryKey My_app_key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);   // khởi động cùng win           
        //
        public delegate void FileRecievedEventHandler(object source, string fileName);
        public event FileRecievedEventHandler NewFileRecieved;
        //        

        #region Gửi dữ liệu từ máy sinh viên
        //  gửi File về máy giáo viên
        string FileName;

        FileStream fs1;
        public bool IsFileUsedbyAnotherProcess(string fileName)
        {
            bool kt = false;

            try
            {
                fs1 = new FileStream(txtFile.Text, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch
            {

                kt = true;
            }
            fs1.Close();
            return kt;

        }
        /// <summary>
        /// Lấy về dung lượng của một file;
        /// </summary>
        /// <param name="parth"></param>
        /// <returns></returns>
        long size = 0;
        void FileSize(string parth)
        {
            try
            {
                string fileName = parth;
                FileInfo f2 = new FileInfo(fileName);
                size = f2.Length;
            }
            catch
            {
                MessageBox.Show("Đường dẫn không đúng", "Quản lý phòng máy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //
        # endregion

        #region Nhận dữ liệu từ máy giáo viên
        NetworkStream myns;
        TcpListener mytcpl;
        Socket mysocket;
        Thread myth;
        BinaryReader bb;
        /// <summary>
        /// Kiểm tra xem thư mục này đã tồn tại hay chưa
        /// Nếu chưa tồn tại thì tạo một thư mục mới
        /// </summary>
        /// <param name="strPath"></param>
        public void CreateFolder(string strPath)
        {

            try
            {
                if (Directory.Exists(strPath) == false)
                {

                    Directory.CreateDirectory(strPath);
                }
            }
            catch { }
        }

        /// <summary>
        /// Nhận dữ liệu
        /// </summary>
        void File_Receiver()
        {
            mytcpl = new TcpListener(3047);
            mytcpl.Start();
            mysocket = mytcpl.AcceptSocket();
            myns = new NetworkStream(mysocket);
            BinaryFormatter br = new BinaryFormatter();
            object op;
            op = br.Deserialize(myns); // Deserialize the Object from Stream
            bb = new BinaryReader(myns);
            byte[] buffer = bb.ReadBytes(10000000);
            //Kiểm tra file đã tồn tại chưa, nếu tồn tại rồi thì xóa file đó đi
            if (File.Exists(@"E:\PTMS\DuLieu\" + (string)op))
            {
                File.Delete(@"E:\PTMS\DuLieu\" + (string)op);
            }
            CreateFolder(@"E:\PTMS");
            CreateFolder(@"E:\PTMS\DuLieu");

            FileStream fss = new FileStream(@"E:\PTMS\DuLieu\" + (string)op, FileMode.CreateNew, FileAccess.Write);
            notifyIcon1.BalloonTipText = "Bạn đang nhận được một file dữ liệu từ giáo viên ";
            notifyIcon1.BalloonTipTitle = "PTMS sinh viên ";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(2000);
            fss.Write(buffer, 0, buffer.Length);
            //StreamWriter sw = new StreamWriter(myns);
            //sw.WriteLine("Nhận dữ liệu thành công");
            //sw.Flush();
            //sw.Close();
            fss.Close();
            mytcpl.Stop();

            Process.Start(@"E:\PTMS\DuLieu\");
            //listBox1.Items.Add("Successfully Saved to: " + textBox1.Text + (string)op);
            if (mysocket.Connected == true)
            {
                while (true)
                {
                    File_Receiver();
                }
            }
        }
        #endregion

        #region Trao đổi thông tin với giáo viên
        public static Socket client;
        public static Socket newsock;
        public static IPEndPoint iep = new IPEndPoint(IPAddress.Any, 3046);
        private Thread receiver;
        private static byte[] data = new byte[1024];
        public static bool inform = false;
        /// <summary>
        /// Nhận dữ liệu từ máy giáo viên 
        /// </summary>
        public void ReceiveData()
        {

            try
            {
                int recv;
                string stringData;
                while (true)
                {
                    recv = client.Receive(data);
                    stringData = UnicodeEncoding.UTF8.GetString(data, 0, recv);
                    if (stringData == "Bye")
                    {
                        break;
                    }

                    //frmClient.file = stringData + "," + (frmClient.count + 1).ToString();

                    if (stringData == "Buzz")
                    {
                        System.Media.SoundPlayer sp = new System.Media.SoundPlayer("Buzz.wav");
                        sp.Play();
                    }
                }
                newsock.Close();
                client.Close();
                Listen();
            }
            catch
            {
            }
            return;
        }
        /// <summary>
        /// Khi có một kết nối đến thì sẽ tạo một luồng để giao tiếp với kết nối đó
        /// </summary>
        /// <param name="iar"></param>
        void AcceptConn(IAsyncResult iar)
        {
            Socket oldserver = (Socket)iar.AsyncState;
            client = oldserver.EndAccept(iar);
            receiver = new Thread(new ThreadStart(ReceiveData));
            receiver.Start();
        }
        /// <summary>
        /// Lắng nghe kết nối từ phía giáo viên
        /// </summary>
        public void Listen()
        {
            newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            newsock.Bind(iep);
            newsock.Listen(5);
            newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
        }
        #endregion

        #region Nhận các yêu cầu về shutdown,loff,reset máy...
        public enum ShutDown
        {
            LogOff = 0,
            Shutdown = 1,
            Reboot = 2,
            ForcedLogOff = 4,
            ForcedShutdown = 5,
            ForcedReboot = 6,
            PowerOff = 8,
            ForcedPowerOff = 12
        }
        Thread trlisten;
        IPAddress ipAddress;
        //public static bool ktr = false;
        private void ListenToServer()
        {
            bool LISTENING = false;
            int port = 63000;
            //' PORT ADDRESS
            ///'''''''' making socket tcpList ''''''''''''''''
            TcpListener tcpList = new TcpListener(port);
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
                    string receivedData = sr.ReadLine();

                    sr = null;
                    if (receivedData == "###SHUTDOWN###")
                    {
                        shutDown(ShutDown.ForcedShutdown);
                    }
                    if (receivedData == "###REBOOT###")
                    {
                        shutDown(ShutDown.ForcedReboot);
                    }

                    if (receivedData == "###LOGOFF###")
                    {
                        shutDown(ShutDown.ForcedLogOff);
                    }

                    if (receivedData.IndexOf("##giangbai##") >= 0)
                    {
                        notifyIcon1.BalloonTipText = "Hệ thống đang bật chế độ giảng bài!\nSau 5s mọi thao tác với chuột và bàn phím đều bị vô hiệu hóa !!!";
                        notifyIcon1.BalloonTipTitle = "PTMS Student";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon1.ShowBalloonTip(1000);
                        Thread.Sleep(5000);
                        string[] mang = receivedData.Split(' ');
                        FrmTeachings.IP = mang[0];
                        //FrmTeachings.IP = "172.169.1.137"; 
                        FrmTeachings a = new FrmTeachings();
                        a.ShowDialog();
                    }

                    string returnedData = "###OK###";
                    //& " From Server"
                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(returnedData);
                    sw.Flush();
                    sr = null;
                    sw.Close();
                    ns.Close();
                    //tcpCli.Close();
                    break;
                }
                tcpList.Stop();
                trlisten = new Thread(ListenToServer);
                trlisten.Start();
            }
            catch (Exception ex)
            {
                //error
                LISTENING = false;
            }
        }
        public void shutDown(ShutDown flag)
        {
            ManagementBaseObject outParam = null;
            ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            ManagementBaseObject inParams = sysOS.GetMethodParameters("Win32Shutdown");
            // pass the flag of 0 = System Shutdown
            inParams["Flags"] = flag;
            inParams["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
            {
                outParam = manObj.InvokeMethod("Win32Shutdown", inParams, null);
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Bắt đầu lắng nghe tắt máy, reset;
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            ipAddress = addr.First<IPAddress>();
            trlisten = new Thread(ListenToServer);
            trlisten.Start();
            //Bắt đầu lằng nghe gửi tài liệu
            myth = new Thread(new System.Threading.ThreadStart(File_Receiver)); // Start Thread Session
            myth.Start();
            Listen();
            timer1.Enabled = false;
            timer1.Dispose();
        }

        #region code phan remoting
        public static ScreenCapture.ScreenCapture myScreen = null;
        public static ObjRef objServer = null;
        public static TcpChannel Channel = null;
        public static void StopListen()
        {
            if (objServer != null)
            { RemotingServices.Unmarshal(objServer); }
            if (myScreen != null)
                RemotingServices.Disconnect(myScreen);
            if (Channel != null)
                ChannelServices.UnregisterChannel(Channel);
            myScreen = null;
            Channel = null;
            objServer = null;

        }
        public static void startListen()
        {
            StopListen();
            try
            {
                Channel = new TcpChannel(6600);
                ChannelServices.RegisterChannel(Channel, false);
                myScreen = new ScreenCapture.ScreenCapture();
                objServer = RemotingServices.Marshal(myScreen);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(ScreenCapture.ScreenCapture), "MyCaptureScreenServer", WellKnownObjectMode.Singleton);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        private int phut, giay;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            startListen();
            //ConnectPort();

            //10/09/2018
            //http://www.ayobamiadewole.com/Blog/A-Client-Server-File-Sharing-Application
            txtHost.Text = "8080";
            btnListen_Click(null, null);

            this.NewFileRecieved += new FileRecievedEventHandler(FrmMain_NewFileRecieved);
            // Form1_NewFileRecieved

            phut = 01; giay = 60;
            timer2.Enabled = true;
            timer2_Tick(sender, e);

        }
        #region nút lện gửi dữ liệu về máy giáo viên   
        private void btnSend_Click(object sender, EventArgs e)
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

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
                FileInfo TheFile = new FileInfo(txtFile.Text); // Get The File Name
                FileName = TheFile.Name;
                FileSize(txtFile.Text);
                if (size > 0)
                {
                    prgFile.Maximum = (int)size;
                    prgFile.Minimum = 0;
                    prgFile.Value = 0;

                    if (size <= 100000000)
                    {
                        if (IsFileUsedbyAnotherProcess(txtFile.Text) == false)
                        {

                            try
                            {
                                lblMessage.Text = "Đang trong quá trình gửi, vui lòng chờ trong giây lát";
                                FileStream fs = new FileStream(txtFile.Text, FileMode.Open);
                                byte[] buffer = new byte[fs.Length];
                                int len = (int)fs.Length;
                                fs.Read(buffer, 0, len);
                                fs.Close();
                                BinaryFormatter br = new BinaryFormatter();
                                TcpClient myclient = new TcpClient(PhongIP, 7000);   //  frmMain.ip
                                NetworkStream myns = myclient.GetStream();
                                br.Serialize(myns, FileName);
                                BinaryWriter mysw = new BinaryWriter(myns);
                                mysw.Write(buffer);
                                mysw.Close();
                                myns.Close();
                                myclient.Close();
                                prgFile.Value = prgFile.Maximum;
                                lblMessage.Text = "Đã gửi thành công";
                                MessageBox.Show("Đã gửi thành công", "Thông báo");
                                //this.Close();
                            }
                            catch
                            {
                                lblMessage.Text = "Không kết nối tới máy giáo viên, Thử lại sau";
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

        #endregion

        #region code phần nhận dữ liệu từ server
        private void FrmMain_NewFileRecieved(object sender, string
       fileName)
        {
            this.BeginInvoke(
            new Action(
            delegate ()
            {
                MessageBox.Show("New File Recieved\n" + fileName);
                System.Diagnostics.Process.Start("explorer", @"e:\");
            }));
        }
        private void btnListen_Click(object sender, EventArgs e)
        {
            int port = int.Parse(txtHost.Text);
            Task.Factory.StartNew(() => HandleIncomingFile(port));
        }

        public void HandleIncomingFile(int port)
        {
            try
            {
                TcpListener tcpListener = new TcpListener(port);
                tcpListener.Start();
                while (true)
                {
                    Socket handlerSocket = tcpListener.AcceptSocket();
                    if (handlerSocket.Connected)
                    {
                        string fileName = string.Empty;
                        NetworkStream networkStream = new NetworkStream

                         (handlerSocket);
                        int thisRead = 0;
                        int blockSize = 1024;
                        Byte[] dataByte = new Byte[blockSize];
                        lock (this)
                        {
                            string folderPath = @"e:\";
                            handlerSocket.Receive(dataByte);
                            int fileNameLen = BitConverter.ToInt32(dataByte,

                             0);
                            fileName = Encoding.ASCII.GetString(dataByte, 4,

                             fileNameLen);
                            Stream fileStream = File.OpenWrite(folderPath +

                             fileName);
                            fileStream.Write(dataByte, 4 + fileNameLen, (

                             1024 - (4 + fileNameLen)));
                            while (true)
                            {
                                thisRead = networkStream.Read(dataByte, 0,

                                 blockSize);
                                fileStream.Write(dataByte, 0, thisRead);
                                if (thisRead == 0)
                                    break;
                            }
                            fileStream.Close();

                        }
                        if (NewFileRecieved != null)
                        {
                            NewFileRecieved(this, fileName);
                        }
                        handlerSocket = null;
                    }
                }

            }
            catch
            {

            }
        }
        #endregion

        private void btnHide_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
        }
        private void Listen8080()
        {
            int port = int.Parse("8080");
            Task.Factory.StartNew(() => HandleIncomingFile(port));

        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void MiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void RunWindowItem_Click(object sender, EventArgs e)
        {
            // https://nosomovo.xyz/2017/09/26/khoi-dong-ung-dung-cung-windows-trong-c/
            My_app_key.SetValue("Student.exe", "\"" + Application.ExecutablePath.ToString() + "\"");   // khởi động cùng win
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            frmChatRoom frmC = new frmChatRoom();
            frmC.Show();
        }

        private void btnChatServer_Click(object sender, EventArgs e)
        {
            frmClient frmC = new frmClient();
            frmC.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            giay = giay - 1;
            if (giay >= 0)
            {
                p.Text = " " + phut.ToString() + " : " + giay.ToString();
            }
            else
            {
                phut = phut - 1;
                giay = 59;
                p.Text = " " + phut.ToString() + " : " + giay.ToString();
            }
            if (phut == 0 && giay == 0)
            {
                Listen8080(); // bật cổng 8080  17/09/2018
                timer2.Enabled = false;
                btnHide_Click(sender, e);

            }
        }
    }
}
