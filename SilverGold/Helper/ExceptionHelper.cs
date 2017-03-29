using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Helper
{
    public static class ExceptionHelper
    {
      
        public static int LineNumber(this Exception e)
        {
            int linenum = 0;
            try
            {
                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));
            }
            catch
            {
            }
            return linenum;

        }

        public static void LogFile(string sExceptionName, string sEventName, string sControlName, int nErrorLineNo, string sFormName)
        {
            string LogPath = Application.StartupPath + "\\";
            string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            string filepath = LogPath + filename;

            StreamWriter log;
            if (!File.Exists(filepath))
            {
                log = new StreamWriter(filepath,true);
            }
            else
            {
                log = File.AppendText(filepath);
               
            }
            log.WriteLine("Data Time:" + DateTime.Now + " | Exception Name:" + sExceptionName + " | Event Name:" + sEventName + " | Control Name:" + sControlName + " | Error Line No.:" + nErrorLineNo + " | Form Name:" + sFormName);
            log.WriteLine("-----------------------------END-------------------------------" + DateTime.Now);
            log.Close();
        }

    }
}
