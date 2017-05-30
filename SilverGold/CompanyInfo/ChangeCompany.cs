using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.CompanyInfo
{
    public partial class ChangeCompany : Form
    {
        #region Declare Variable

        List<CompanyDetailsEntity> CompanyDetailsList = new List<CompanyDetailsEntity>();

        #endregion
        public ChangeCompany()
        {
            InitializeComponent();
            CommanHelper._CompName_ChangeComapny = "";
            CommanHelper._FinancialYear_ChangeComapny = "";
            CommanHelper._Com_DB_PATH_ChangeComapny = "";
            CommanHelper._Com_DB_NAME_ChangeComapny = "";
        }

        private void ChangeCompany_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;

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
                                if (CommanHelper.CompName != dirName[i].ToString() && CommanHelper._FinancialYear != mainDir[j].ToString())
                                {
                                    CompanyDetailsEntity oCompanyDetailsEntity = new CompanyDetailsEntity();
                                    oCompanyDetailsEntity.AddCompanyDetails(_DisplayName, dirName[i].ToString(), Path.GetFileNameWithoutExtension(file.Name), mainDir[j].ToString(), dirName[i].ToString() + "\\" + d.ToString().Split('\\').Last(), Sno);
                                    CompanyDetailsList.Add(oCompanyDetailsEntity);
                                }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Mapper

        private void Fun_ChangeCompany()
        {
            String _CompanyName = "";
            if (listBox1.Text.ToString().Trim() != "")
            {
                _CompanyName = listBox1.Text.ToString();
                var result = CompanyDetailsList.Where(x => x.Sno == Conversion.ConToInt(listBox1.SelectedValue.ToString())).SingleOrDefault();

                if ((MessageBox.Show("Do U Want To Access Company " + _CompanyName + " ?", "Change Company", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    CommanHelper._CompName_ChangeComapny = result.CompanyName;
                    CommanHelper._FinancialYear_ChangeComapny = result.FinancialYear.ToString();
                    CommanHelper._Com_DB_PATH_ChangeComapny = result.DataBasePath.ToString();
                    CommanHelper._Com_DB_NAME_ChangeComapny = result.DataBaseName.ToString();

                    this.Hide();
                    Login oLogin = new Login();
                    oLogin.ShowDialog();
                    
                }
            }
        }

        #endregion

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Fun_ChangeCompany();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
             
                if (e.KeyChar == 13)
                {
                    Fun_ChangeCompany();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
