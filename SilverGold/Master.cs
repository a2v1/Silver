using SilverGold.Comman;
using SilverGold.CompanyInfo;
using SilverGold.Helper;
using SilverGold.MasterInfo;
using SilverGold.SecuritySystem;
using SilverGold.Transaction;
using SilverGold.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class Master : Form
    {
        //private int childFormNumber = 0;
        public static Master objMaster;
        public Master()
        {
            InitializeComponent();
        }

        //private void ShowNewForm(object sender, EventArgs e)
        //{
        //    Form childForm = new Form();
        //    childForm.MdiParent = this;
        //    childForm.Text = "Window " + childFormNumber++;
        //    childForm.Show();
        //}


        #region Helper

        private void MenuEnable()
        {
            masterInfoToolStripMenuItem.Enabled = true;
            transactionToolStripMenuItem.Enabled = true;
            reportToolStripMenuItem.Enabled = true;
            utilitiesToolStripMenuItem.Enabled = true;
            sToolStripMenuItem.Enabled = true;
        }


        private void MenuDesable()
        {
            masterInfoToolStripMenuItem.Enabled = false;
            transactionToolStripMenuItem.Enabled = false;
            reportToolStripMenuItem.Enabled = false;
            utilitiesToolStripMenuItem.Enabled = false;
            sToolStripMenuItem.Enabled = false;
        }

        #endregion

        private void Master_Load(object sender, EventArgs e)
        {
            if (CommanHelper._FinancialYear.ToString().Trim() != "")
            {
                this.Text = CommanHelper.CompName.ToString() + " (" + CommanHelper._FinancialYear.ToString().Substring(0, 4) + "-" + CommanHelper._FinancialYear.ToString().Substring(4, 4) + ")";
            }
            Master.objMaster = this;
            CommanHelper.FormX = this.Width;
            CommanHelper.FormY = this.Height;

            if (CommanHelper.CompName == "")
            {
                MenuDesable();
                Company oCompany = new Company();
                oCompany.MdiParent = this;
                oCompany.Show();
            }
            else
            {
                MenuEnable();
            }

            //foreach (Control ctl in this.Controls)
            //{
            //    if (ctl is MdiClient)
            //    {
            //        ctl.BackColor = Color.RosyBrown;
            //        break;
            //    }
            //}


            if (CommanHelper.CompName != "" && CommanHelper.UserId != "")
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    try
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("ALTER TABLE LaboursRate ADD JamaNaam Text(50)", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                    }
                    try
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("ALTER TABLE PartyDetails ADD Deleted int Default 0", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                    }

                    //try
                    //{
                    //    con.Open();
                    //    OleDbCommand cmd = new OleDbCommand("ALTER TABLE PartyTran ADD Hazir Decimal(14,6)", con);
                    //    cmd.ExecuteNonQuery();
                    //    con.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    con.Close();
                    //}

                    //try
                    //{
                    //    con.Open();
                    //    OleDbCommand cmd = new OleDbCommand("ALTER TABLE KFDetails ADD YN TEXT(50),KF_Sno INT,KF_DateR DateTime,KF_DateP DateTime", con);
                    //    cmd.ExecuteNonQuery();
                    //    con.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    con.Close();
                    //}

                    //try
                    //{
                    //    con.Open();
                    //    OleDbCommand cmd = new OleDbCommand("ALTER TABLE PartyTran ADD KF_Sno INT", con);
                    //    cmd.ExecuteNonQuery();
                    //    con.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    con.Close();
                    //}
                }
            }

        }

        private void Master_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void createCompToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Company oCompany = new Company();
                oCompany.MdiParent = this;
                oCompany.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void partyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PartyInformation oPartyInformation = new PartyInformation();
                oPartyInformation.MdiParent = this;
                oPartyInformation.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void productDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDetails oProductDetails = new ProductDetails();
                oProductDetails.MdiParent = this;
                oProductDetails.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void labourRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void ghattakListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void introducerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void groupHeadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GroupHead oGroupHead = new GroupHead();
                oGroupHead.MdiParent = this;
                oGroupHead.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void jamaRecievingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Jama oJama = new Jama();
                oJama.MdiParent = this;
                oJama.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void naamGivingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Naam oNaam = new Naam();
                oNaam.MdiParent = this;
                oNaam.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void changeCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeCompany oChangeCompany = new ChangeCompany();
                oChangeCompany.MdiParent = this;
                oChangeCompany.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Master_Click(object sender, EventArgs e)
        {
            try
            {
                Master f1 = new Master();
                f1.MdiParent = this.MdiParent;
                f1.Show();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManagement oUserManagement = new UserManagement();
            oUserManagement.MdiParent = this;
            oUserManagement.Show();
        }

        private void cashPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashPurchase oCashPurchase = new CashPurchase();
            oCashPurchase.MdiParent = this;
            oCashPurchase.Show();
        }

        private void cashSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashSale oCashSale = new CashSale();
            oCashSale.MdiParent = this;
            oCashSale.Show();
        }

        private void journalVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JournalVoucher oJournalVoucher = new JournalVoucher();
            oJournalVoucher.MdiParent = this;
            oJournalVoucher.Show();
        }

        private void recieptVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecieptVoucher oRecieptVoucher = new RecieptVoucher();
            oRecieptVoucher.MdiParent = this;
            oRecieptVoucher.Show();
        }

        private void paymentVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentVoucher oPaymentVoucher = new PaymentVoucher();
            oPaymentVoucher.MdiParent = this;
            oPaymentVoucher.Show();
        }

        private void setPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword oChangePassword = new ChangePassword();
            oChangePassword.MdiParent = this;
            oChangePassword.Show();
        }
    }
}
