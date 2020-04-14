using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace Student
{
    public partial class FrmTeachings : Form
    {
        public FrmTeachings()
        {
            InitializeComponent();
        }
        public static string IP = "";// "172.169.1.137";
        [return: MarshalAs(UnmanagedType.Bool)]        // khai báo khóa chuột và bàn phím
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void BlockInput([In, MarshalAs(UnmanagedType.Bool)]bool fBlockIt);   // khóa chuột và bàn phím
        private void frmTeachings_Load(object sender, EventArgs e)
        {
            FrmMain.StopListen();
            Start();
            //Download source code mien phi tai Sharecode.vn
        }
        ScreenCapture.ScreenCapture obj;
        TcpChannel channel;
        void Start()
        {

            string URI = "Tcp://" + IP + ":6601/MyCaptureScreenServer";

            obj = new ScreenCapture.ScreenCapture();
            channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);
            obj = (ScreenCapture.ScreenCapture)Activator.GetObject(typeof(ScreenCapture.ScreenCapture), URI);

            Runtime.Enabled = true;
        }
        void Stop()
        {
            try
            {               
                Runtime.Enabled = false;
                ChannelServices.UnregisterChannel(channel);
                FrmMain.startListen();
                this.Close();

            }
            catch { FrmMain.startListen(); }
        }
        public void KillCtrlAltDelete()
        {
            RegistryKey regkey;
            string keyValueInt = "1";
            string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableTaskMgr", keyValueInt);
                regkey.Close();
            }
            catch { }
        }
        private void Runtime_Tick(object sender, EventArgs e)
        {
            try
            {
                BlockInput(true);    // khóa chuột và bàn phím
                //KillCtrlAltDelete();
                string URI = "Tcp://" + IP + ":6601/MyCaptureScreenServer";

                byte[] buff = obj.GetDesktopBitmapBytes(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                byte[] tmp = ScreenCapture.QuickLZ.decompress(buff);
                MemoryStream ms = new MemoryStream(tmp);
                pteTeaching.Image = Image.FromStream(ms);
            }
            catch
            {

                Stop();
            }
        }
        public void EnableCTRLALTDEL()    // mở CTRL ALT DEL
        {
            try
            {
                string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);
            }
            catch
            {
            }
        }
        private void frmTeaching_FormClosing(object sender, FormClosingEventArgs e)
        {            
            Stop();
            BlockInput(false);     // mở khóa chuột và bàn phím
            EnableCTRLALTDEL();
        }       
       
        //
    }
}
