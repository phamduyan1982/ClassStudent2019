using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;
using System.Management.Instrumentation;
namespace ScreenCapture
{
    public class ScreenCapture : System.MarshalByRefObject
    {
        //Lấy ra cửa sổ của desktop
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();
        /// <summary>
        /// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
        /// chuyển khối bit dữ liệu màu sắc tương ứng trong 1 hình chữ nhật của các điểm ảnh từ một thiết bị nguồn đến một thiết bị đích
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="nXDest"></param>
        /// <param name="nYDest"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="nXSrc"></param>
        /// <param name="nYSrc"></param>
        /// <param name="dwRop"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest, // handle to destination device context
            int nXDest, // x-coord of destination upper-left corner
            int nYDest, // y-coord of destination upper-left corner
            int nWidth, // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc, // handle to source device context
            int nXSrc, // x-coordinate of source upper-left corner
            int nYSrc, // y-coordinate of source upper-left corner
            System.Int32 dwRop // raster operation code
            );

        private const Int32 SRCCOPY = 0xCC0020;
        /// <summary>
        ///
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        /// <summary>
        /// lấy ra kích thước ảnh hiển thị trên màn hình
        /// </summary>
        /// <returns></returns>
        public Size GetDesktopBitmapSize()
        {
            return new Size(GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN));
        }

        private const uint MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        private const uint MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
        private const uint MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000; /* absolute move */
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint INPUT_MOUSE = 0;
        private const uint INPUT_KEYBOARD = 1;

        struct MOUSE_INPUT
        {
            public uint dx;
            public uint dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public uint dwExtraInfo;
        }

        struct KEYBD_INPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public uint dwExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public uint type;

            // union
            [FieldOffset(4)]
            public MOUSE_INPUT mi;

            [FieldOffset(4)]
            public KEYBD_INPUT ki;
        }
        /// <summary>
        /// lấy đầu vào là các thao tác của chuột và bàn phím để thay đổi màn hình
        /// </summary>
        /// <param name="nInputs">The number input structures in the array</param>
        /// <param name="input"></param>
        /// <param name="cbSize"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern uint SendInput(
            uint nInputs,     // count of input events
            ref INPUT input,
            int cbSize        // size of structure
            );
        /// <summary>
        /// nhấn hoạc thả chuột
        /// </summary>
        /// <param name="Press"></param>
        /// <param name="Left"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void PressOrReleaseMouseButton(bool Press, bool Left, int X, int Y)
        {
            INPUT input = new INPUT();
            input.type = INPUT_MOUSE;
            input.mi.dx = (uint)X;
            input.mi.dy = (uint)Y;
            input.mi.mouseData = 0;
            input.mi.dwFlags = 0;
            input.mi.time = 0;
            input.mi.dwExtraInfo = 0;

            if (Left)
            {
                input.mi.dwFlags = Press ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_LEFTUP;
            }
            else
            {
                input.mi.dwFlags = Press ? MOUSEEVENTF_RIGHTDOWN : MOUSEEVENTF_RIGHTUP;
            }

            SendInput(1, ref input, Marshal.SizeOf(input));
        }
        /// <summary>
        /// cài đặt vị trí của chuột
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);

        public void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public void SendKeystroke(byte VirtualKeyCode, byte ScanCode, bool KeyDown, bool ExtendedKey)
        {
            INPUT input = new INPUT();

            input.type = INPUT_KEYBOARD;
            input.ki.wVk = VirtualKeyCode;
            input.ki.wScan = ScanCode;
            input.ki.dwExtraInfo = 0;
            input.ki.time = 0;

            if (!KeyDown)
            {
                input.ki.dwFlags |= KEYEVENTF_KEYUP;
            }

            if (ExtendedKey)
            {
                input.ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
            }

            SendInput(1, ref input, Marshal.SizeOf(input));
        }
        /// <summary>
        /// Resize ảnh về kích thước màn hình hiển thị 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="dWidth"> Với form Remote thì dwidth là chiều rộng của giảng viên(màn hình hiển thị ảnh)</param>
        /// <param name="dHeight"></param>
        /// <returns></returns>
        Bitmap ImageSize(Bitmap bmp, int dWidth, int dHeight)
        {

            Bitmap tmp = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage((Image)tmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage((Image)bmp, 0, 0, dWidth, dHeight);
            g.Dispose();
            return tmp;
        }
        ///// <summary>
        ///// lấy về kích thước của màn hình mà mình muốn xem 
        ///// </summary>
        ///// <returns></returns>
        //public Size GetSize()
        //{
        //   //int width = Screen.PrimaryScreen.Bounds.Width;
        //   //int height = Screen.PrimaryScreen.Bounds.Height;
        //   return GetDesktopBitmapSize();
        //}
        public byte[] GetDesktopBitmapBytes(int dWidth, int dHeight)
        {
            Size DesktopBitmapSize = GetDesktopBitmapSize();
            Graphics Graphic = Graphics.FromHwnd(GetDesktopWindow());
            Bitmap MemImage = new Bitmap(DesktopBitmapSize.Width, DesktopBitmapSize.Height, Graphic);
            Graphics MemGraphic = Graphics.FromImage(MemImage);
            IntPtr dc1 = Graphic.GetHdc();
            IntPtr dc2 = MemGraphic.GetHdc();
            BitBlt(dc2, 0, 0, DesktopBitmapSize.Width, DesktopBitmapSize.Height, dc1, 0, 0, SRCCOPY);
            Graphic.ReleaseHdc(dc1);
            MemGraphic.ReleaseHdc(dc2);
            Graphic.Dispose();
            MemGraphic.Dispose();

            //Graphics g = System.Drawing.Graphics.FromImage(MemImage);
            //System.Windows.Forms.Cursor cur = System.Windows.Forms.Cursors.Arrow;
            //cur.Draw(g, new Rectangle(System.Windows.Forms.Cursor.Position.X - 10, System.Windows.Forms.Cursor.Position.Y - 10, cur.Size.Width, cur.Size.Height));
            Bitmap tmp = ImageSize(MemImage, dWidth, dHeight);
            MemImage.Dispose();
            MemoryStream ms = new MemoryStream();
            tmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] tmp1 = QuickLZ.compress(ms.GetBuffer(), 1);
            return tmp1;
        }
        public byte[] UCGetDesktopBitmapBytes()
        {
            Size DesktopBitmapSize = GetDesktopBitmapSize();
            Graphics Graphic = Graphics.FromHwnd(GetDesktopWindow());
            Bitmap MemImage = new Bitmap(DesktopBitmapSize.Width, DesktopBitmapSize.Height, Graphic);

            Graphics MemGraphic = Graphics.FromImage(MemImage);
            IntPtr dc1 = Graphic.GetHdc();
            IntPtr dc2 = MemGraphic.GetHdc();
            BitBlt(dc2, 0, 0, DesktopBitmapSize.Width, DesktopBitmapSize.Height, dc1, 0, 0, SRCCOPY);
            Graphic.ReleaseHdc(dc1);
            MemGraphic.ReleaseHdc(dc2);
            Graphic.Dispose();
            MemGraphic.Dispose();

            //Graphics g = System.Drawing.Graphics.FromImage(MemImage);
            //System.Windows.Forms.Cursor cur = System.Windows.Forms.Cursors.Arrow;
            //cur.Draw(g, new Rectangle(System.Windows.Forms.Cursor.Position.X - 10, System.Windows.Forms.Cursor.Position.Y - 10, cur.Size.Width, cur.Size.Height));

            MemoryStream ms = new MemoryStream();
            MemImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] tmp1 = QuickLZ.compress(ms.GetBuffer(), 1);
            return tmp1;
        }


        /// <summary>
        /// Lấy ra cấu hình máy tính
        /// </summary>
        /// <returns></returns>
        public string GetConfig()
        {
            string strCDRom = "", strKeyBoard = "", strRam = "", strHDD = "", strProcesscor = "", strBIOS = "";
            //lấy ra thông số của CDRom
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select Name from Win32_CDROMDrive");
            foreach (ManagementObject cdRom in searcher.Get())
            {
                strCDRom = cdRom.GetPropertyValue("Name").ToString();
            }

            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("Select Name from Win32_Processor");
            foreach (ManagementObject processor in searcher1.Get())
            {
                strProcesscor = processor.GetPropertyValue("Name").ToString();
            }
            //Bàn phím
            ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("Select Name from  Win32_Keyboard");
            foreach (ManagementObject tmp in searcher2.Get())
            {
                strKeyBoard = tmp.GetPropertyValue("Name").ToString();
            }
            //Ổ cứng HDD
            ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("Select * from  Win32_DiskDrive");
            foreach (ManagementObject hdd in searcher3.Get())
            {
                string s = hdd.GetPropertyValue("Size").ToString();
                double size = double.Parse(s);
                double sizeHDD = Math.Round(size / 1000000000, 1);
                strHDD = hdd.GetPropertyValue("Caption").ToString() + " " + sizeHDD + "GB";
            }
            //Ram
            ManagementObjectSearcher searcher4 = new ManagementObjectSearcher("Select * from Win32_MemoryArray");
            foreach (ManagementObject ram in searcher4.Get())
            {
                string s = ram.GetPropertyValue("EndingAddress").ToString();
                double size = double.Parse(s);//Lấy ra giá trị
                double sizeRam = Math.Round(size / 1024 / 1024, 1);//Chia để được giá trị 2.5 GB
                strRam = sizeRam.ToString() + "GB";
            }
            //Bios
            ManagementObjectSearcher searcher5 = new ManagementObjectSearcher("Select Name from  Win32_BIOS");
            foreach (ManagementObject tmp in searcher5.Get())
            {
                strBIOS = tmp.GetPropertyValue("Name").ToString();
            }
            string strConfig = @"" + strProcesscor + "," + strRam + "," + strCDRom + "," + strHDD + "," + strBIOS + "," + strKeyBoard;
            return strConfig;
        }
        public string[,] GetConfigA()
        {
            string[,] cauhinh = new string[6, 4];
            //0 ma, 1 ten, 2 hang, 3 cau hinh
            //cpu
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (ManagementObject processor in searcher1.Get())
            {
                cauhinh[0, 0] = processor.GetPropertyValue("ProcessorID").ToString();
                cauhinh[0, 1] = "CPU";
                cauhinh[0, 2] = processor.GetPropertyValue("Manufacturer").ToString();
                cauhinh[0, 3] = processor.GetPropertyValue("Name").ToString();
            }
            //Ram
            ManagementObjectSearcher searcher4 = new ManagementObjectSearcher("Select * from Win32_MemoryArray");
            foreach (ManagementObject ram in searcher4.Get())
            {
                string s = ram.GetPropertyValue("EndingAddress").ToString();
                double size = double.Parse(s);//Lấy ra giá trị 
                double sizeRam = Math.Round(size / 1024 / 1024, 1);//Chia để được giá trị 2.5 GB

                cauhinh[1, 0] = ram.GetPropertyValue("EndingAddress").ToString();
                cauhinh[1, 1] = "Ram";
                cauhinh[1, 2] = ram.GetPropertyValue("EndingAddress").ToString();
                cauhinh[1, 3] = sizeRam.ToString() + "GB";
            }
            //Bios
            ManagementObjectSearcher searcher5 = new ManagementObjectSearcher("Select Name from  Win32_BIOS");
            foreach (ManagementObject tmp in searcher5.Get())
            {
                cauhinh[2, 0] = tmp.GetPropertyValue("Name").ToString();
                cauhinh[2, 1] = "Bios";
                cauhinh[2, 2] = tmp.GetPropertyValue("Name").ToString();
                cauhinh[2, 3] = tmp.GetPropertyValue("Name").ToString();
            }
            //Ổ cứng HDD
            ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("Select * from  Win32_DiskDrive");
            foreach (ManagementObject hdd in searcher3.Get())
            {
                string s = hdd.GetPropertyValue("Size").ToString();
                double size = double.Parse(s);
                double sizeHDD = Math.Round(size / 1000000000, 0);

                //Phan nay bi loi 10-5
               // cauhinh[3, 0] = hdd.GetPropertyValue("SerialNumber").ToString().Trim();
                cauhinh[3, 1] = "HDD";
                cauhinh[3, 2] = hdd.GetPropertyValue("Manufacturer").ToString();
                cauhinh[3, 3] = hdd.GetPropertyValue("InterfaceType").ToString() + " " + sizeHDD + "GB";
            }
            //lấy ra thông số của CDRom
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select Name from Win32_CDROMDrive");
            foreach (ManagementObject cdRom in searcher.Get())
            {
                cauhinh[4, 0] = cdRom.GetPropertyValue("Name").ToString().Trim(); ;
                cauhinh[4, 1] = "CDRom";
                cauhinh[4, 2] = cdRom.GetPropertyValue("Name").ToString();
                cauhinh[4, 3] = cdRom.GetPropertyValue("Name").ToString();
            }


            //Bàn phím
            ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("Select * from  Win32_Keyboard");
            foreach (ManagementObject tmp in searcher2.Get())
            {
                cauhinh[5, 0] = tmp.GetPropertyValue("DeviceID").ToString();
                cauhinh[5, 1] = "Keyboard";
                cauhinh[5, 2] = "";
                cauhinh[5, 3] = tmp.GetPropertyValue("Name").ToString();
            }
            return cauhinh;
        }
        #region Lock keyboard and Mouse
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
        public void EnableCTRLALTDEL()
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
        /// <summary>
        /// khoa ban phim
        /// </summary>
        /// <param name="fBlockIt"></param>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void BlockInput([In, MarshalAs(UnmanagedType.Bool)]bool fBlockIt);
        public void KeyboardandMouse(bool allow)
        {
            BlockInput(allow);
        }
        [DllImport("user32", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);

        [DllImport("user32", EntryPoint = "UnhookWindowsHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int UnhookWindowsHookEx(int hHook);
        private delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("user32", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        private const int WH_KEYBOARD_LL = 13;

        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        private struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        private static int intLLKey;

        private int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;

            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key,
                    blnEat = ((lParam.vkCode == 9) && (lParam.flags == 32)) || ((lParam.vkCode == 27) && (lParam.flags == 32)) || ((lParam.vkCode == 27) && (lParam.flags == 0)) || ((lParam.vkCode == 91) && (lParam.flags == 1)) || ((lParam.vkCode == 92) && (lParam.flags == 1)) || ((lParam.vkCode == 73) && (lParam.flags == 0));
                    break;
            }

            if (blnEat == true)
            {
                return 1;
            }
            else
            {
                return CallNextHookEx(0, nCode, wParam, ref lParam);
            }
        }
        public void KillStartMenu()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);
        }

        public void ShowStartMenu()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_SHOW);
        }

        public void Kill_Key_Sys()
        {
            intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelKeyboardProc, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
        }

        #endregion
        #region Tắt máy: Shutdown, reboot, standby, logoff,
        public void ExitWindow(int Flag)
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();
            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");
            // Flag 1 means we want to shut down the system
            mboShutdownParams["Flags"] = Flag.ToString();
            mboShutdownParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
            }
        }
        public void Logoff()
        {
            ExitWindow(0);
        }
        public void Shutdown()
        {
            ExitWindow(1);
        }
        public void Standby()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }
        public void Reboot()
        {
            ExitWindow(2);
        }
        public void Hibernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
        }
        [DllImport("user32.dll")]
        public static extern void LockWorkStation();
        public void LockComputer()
        {
            LockWorkStation();
        }
        #endregion
    }
}
