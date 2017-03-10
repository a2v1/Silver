using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SilverGold.Comman
{
    class ConnectionClass
    {
        //public string CONSTR()
        //{
        //    return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DSOURCE() + "" + "\\" + FUNCTIONCLASS.Com_DB_PATH + "\\" + FUNCTIONCLASS.Com_DB_NAME + ".mdb ;Jet OLEDB:Database Password=Hello@12345XZ435";
        //}
        public string _CONSTR()
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DSOURCE() + "\\" + DATABASE() + ".mdb;Jet OLEDB:Database Password=Hello@12345XZ435";
        }

        public string DSOURCE()
        {
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DSOURCE.dll", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string str1;
            str1 = "";
            while (!sr.EndOfStream)
            {
                str1 = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            return str1;
        }
        public string DATABASE()
        {
            FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DATABASE.dll", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string str;
            str = "";
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            return str;
        }

     
    }
}