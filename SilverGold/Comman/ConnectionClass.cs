using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Comman
{
    class ConnectionClass
    {
        
        public string _CONSTR()
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\" + "Account.mdb;Jet OLEDB:Database Password=Hello@12345XZ435";
        }

        public static string LoginConString(String DSOURCE, String DATABASE)
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DSOURCE + "\\" + DATABASE + ";Jet OLEDB:Database Password=Hello@12345XZ435";
        }

       

     
    }
}