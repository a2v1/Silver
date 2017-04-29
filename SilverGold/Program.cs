using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SilverGold
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Boolean CheckCompanyDir = false;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var directoryInfo = new System.IO.DirectoryInfo(Application.StartupPath);
                var dirName = directoryInfo.GetDirectories();


                for (int i = 0; i < dirName.Count(); i++)
                {
                    var mainDir = dirName[i].GetDirectories();
                    FileInfo[] Files;
                    foreach (var item in mainDir)
                    {
                        DirectoryInfo d = new DirectoryInfo(item.FullName);
                        Files = d.GetFiles("*.mdb");
                        foreach (FileInfo file in Files)
                        {
                            CheckCompanyDir = true;
                        }
                    }
                }
                if (CheckCompanyDir == true)
                {
                    Application.Run(new CompanyDetails());
               
                }
                else
                {
                    if (MessageBox.Show("Please Create New Company ?", "Company", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Application.Run(new Master());
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
