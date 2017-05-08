﻿using SilverGold.CompanyInfo;
using SilverGold.Helper;
using SilverGold.MasterInfo;
using SilverGold.Transaction;
using SilverGold.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class Master : Form
    {
        private int childFormNumber = 0;
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
        }


        private void MenuDesable()
        {
            masterInfoToolStripMenuItem.Enabled = false;
            transactionToolStripMenuItem.Enabled = false;
        }

        #endregion

        private void Master_Load(object sender, EventArgs e)
        {
            objMaster = this;
            CommanHelper.FormX = this.Width;
            CommanHelper.FormY = this.Height;

            if (CommanHelper.CompName == "")
            {
                MenuDesable();
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
    }
}
