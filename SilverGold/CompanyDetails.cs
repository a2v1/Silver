using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class CompanyDetails : Form
    {
        #region Declare Variable

        List<CompanyDetailsEntity> CompanyDetailsList = new List<CompanyDetailsEntity>();
        
        #endregion
        
        public CompanyDetails()
        {
            InitializeComponent();
        }

        private void CompanyDetails_Load(object sender, EventArgs e)
        {
            try
            {
               
                var directoryInfo = new System.IO.DirectoryInfo(Application.StartupPath);
                var dirName = directoryInfo.GetDirectories();


                for (int i = 0; i < dirName.Count(); i++)
                {
                    var mainDir = dirName[i].GetDirectories();
                    FileInfo[] Files;
                    int j = 0;
                    foreach (var item in mainDir)
                    {
                        DirectoryInfo d = new DirectoryInfo(item.FullName);
                        Files = d.GetFiles("*.mdb");
                        foreach (FileInfo file in Files)
                        {                            
                            if (mainDir[j].ToString().Trim().Length == 8)
                            {
                                String _DisplayName = dirName[i].ToString() + " (" + mainDir[j].ToString().Substring(0, 4) + "-" + mainDir[j].ToString().Substring(4, 4) + ")";
                                var Sno = 0;
                                if (CompanyDetailsList.Count > 0)
                                {
                                    Sno = CompanyDetailsList.Max(x => x.Sno) + 1;
                                }
                                CompanyDetailsEntity oCompanyDetailsEntity = new CompanyDetailsEntity();
                                oCompanyDetailsEntity.AddCompanyDetails(_DisplayName, dirName[i].ToString(), Path.GetFileNameWithoutExtension(file.Name), mainDir[j].ToString(), dirName[i].ToString() + "\\" + d.ToString().Split('\\').Last(), Sno);
                                CompanyDetailsList.Add(oCompanyDetailsEntity);
                            }
                        } j++;
                    }
                }
                if (CompanyDetailsList.Count > 0)
                {

                    listBox1.DataSource = CompanyDetailsList;
                    listBox1.DisplayMember = "DisplayName";
                    listBox1.ValueMember = "Sno";
                }

                listBox1.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void CompanyDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if ((listBox1.SelectedItem ?? (object)"").ToString() != "")
                    {
                        var _Sno = listBox1.SelectedValue;

                        var result = CompanyDetailsList.Where(x => x.Sno ==Conversion.ConToInt( _Sno)).FirstOrDefault();
                        CommanHelper.CompName = result.CompanyName;
                        CommanHelper._FinancialYear = result.FinancialYear.ToString();
                        CommanHelper.Com_DB_PATH = result.DataBasePath.ToString();
                        CommanHelper.Com_DB_NAME = result.DataBaseName.ToString();

                        Login oLogin = new Login();
                        oLogin.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((listBox1.SelectedItem ?? (object)"").ToString() != "")
                {
                    var _Sno = listBox1.SelectedValue;

                    var result = CompanyDetailsList.Where(x => x.Sno == Conversion.ConToInt(_Sno)).FirstOrDefault();
                    CommanHelper.CompName = result.CompanyName;
                    CommanHelper._FinancialYear = result.FinancialYear.ToString();
                    CommanHelper.Com_DB_PATH = result.DataBasePath.ToString();
                    CommanHelper.Com_DB_NAME = result.DataBaseName.ToString();

                    Login oLogin = new Login();
                    oLogin.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
