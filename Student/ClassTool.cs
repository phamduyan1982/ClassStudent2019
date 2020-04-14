using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace Student
{
    class ClassTool
    {
       
        public void AppendText(RichTextBox box, string text, Color color, string font, float size, ToolStripButton B, ToolStripButton I, ToolStripButton U)
        {

            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {


                box.SelectionFont = new Font(font, size,KieuFont(B, I, U));
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clea
        }

        public string GetworkgroupPC()
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
        //public bool CheckEmptyTxt1(TextEdit txt, string report, ErrorProvider err)
        //{
        //    //Trả lại gia tri false neu o Text co du lieu
        //    if (txt.Text == "")
        //    {
        //        txt.Focus();
        //        err.SetError(txt, report);
        //        return true;
        //    }
        //    else return false;
        //}
        //public bool CheckLookupEdit(LookUpEdit txt, string report, ErrorProvider err)
        //{
        //    //Trả lại gia tri false neu o Text co du lieu
        //    if (txt.Text == "")
        //    {
        //        txt.Focus();
        //        err.SetError(txt, report);
        //        return true;
        //    }
        //    else return false;
        //}
        //public bool CheckEmptyCbo(ComboBoxEdit com, string report, ErrorProvider err)
        //{
        //    if (com.Text == "")
        //    {
        //        err.SetError(com, report);
        //        com.Focus();
        //        return true;
        //    }
        //    else return false;
        //}
        //public bool CheckEmptyDateEdit(DateEdit dtp, string report, ErrorProvider err)
        //{
        //    if (dtp.Text == "")
        //    {
        //        err.SetError(dtp, report);
        //        dtp.Focus();
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        /// <summary>
        /// Chuẩn xâu họ tên (viết hoa ,giãn cách giữa các chữ)
        /// </summary>
        /// <param name="xau"></param>
        /// <returns></returns>
        public string StandardStringName(string xau)
        {
            if (xau != "")
            {
                StringBuilder s = new StringBuilder(xau.Trim());
                while (s.ToString().IndexOf("  ") >= 0)
                {
                    s.Replace("  ", " ");
                }
                s[0] = s[0].ToString().ToUpper()[0];
                for (int i = 1; i < s.Length; i++)
                {
                    if ((s[i - 1] == ' ') && (s[i] != ' '))
                    {
                        s[i] = s[i].ToString().ToUpper()[0];
                    }
                    else
                        s[i] = s[i].ToString().ToLower()[0];
                }
                return s.ToString();
            }
            else
                return "";
        }

        /// <summary>
        /// Chuẩn xâu thường (viết hoa chữ đầu dòng ,giãn cách giữa các chữ)
        /// </summary>
        /// <param name="xau"></param>
        /// <returns></returns>
        public string StandardStringNormal(string xau)
        {
            //if (xau != "")
            //{
            //    StringBuilder s = new StringBuilder(xau.Trim());
            //    while (s.ToString().IndexOf("  ") >= 0)
            //    {
            //        s.Replace("  ", " ");
            //    }
            //    s[0] = s[0].ToString().ToUpper()[0];
            //    for (int i = 1; i < s.Length; i++)
            //    {
            //        s[i] = s[i].ToString().ToLower()[0];
            //    }
            //    return s.ToString();
            //}
            //else
            return "";
        }
       public FontStyle KieuFont(ToolStripButton ttbB,ToolStripButton ttbI,ToolStripButton ttbU)
        {

            
            FontStyle item = new FontStyle();
            if (ttbB.Checked)
            {
                item = item | FontStyle.Bold;
            }
            if (ttbU.Checked)
            {
                item = item | FontStyle.Underline;
            }
            if (ttbI.Checked)
            {
                item = item | FontStyle.Italic;
            }
            return item;
        }


        #region Gửi file
      
      
       FileStream fs1;
       public bool IsFileUsedbyAnotherProcess(string fileName)
       {
           bool kt = false;

           try
           {
               fs1 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
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
       public long size = 0;
       public void FileSize(string parth)
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
        #endregion
    }
}
