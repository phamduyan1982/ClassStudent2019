using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Net.NetworkInformation;
using System.Collections;
using System.Windows.Forms;

namespace PracticeTeachingManagementSystem_Student
{
    public class clsSystem
    {
        //Gửi dữ liệu cho server

        public void NoRun()
        {

            string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey, true);
            key.SetValue("NoRun", "1", RegistryValueKind.DWord);
        }
        public void DisNoRun()
        {
            try
            {
                string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey, true);
                key.DeleteValue("NoRun");
            }
            catch { }
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
            catch 
            {
            }
        }


        public  void EnableCTRLALTDEL()
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

        [DllImport("user32", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);
        [DllImport("user32", EntryPoint = "UnhookWindowsHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int UnhookWindowsHookEx(int hHook);
        private delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("user32", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        private const int WH_KEYBOARD_LL = 13;

        /*code needed to disable start menu*/
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

        private bool Ping(string hostname)
        {
            bool kt = false;
            Ping ping = new Ping();
            PingReply reply = ping.Send(hostname);
            if (reply.Status == IPStatus.Success)
                kt = true;
            return kt;
        }

        // Importing Windows API library - Phan tat, reset, ngu dong may Client
        [DllImport("user32.dll")]
        public static extern void LockWorkStation();
        [DllImport("user32.dll")]
        public static extern int ExitWindowsEx(int uFlags, int dwReason);

        public void Hibernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
        }
        public void Standby()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }
        public void Shutdown()
        {
            ExitWindowsEx(1, 0);
        }
        public void Reboot()
        {
            ExitWindowsEx(2, 0);
        }
        public void LogOff()
        {
            ExitWindowsEx(0, 0);
        }
        public void LockComp()
        {
            LockWorkStation();
        }
    }
}
