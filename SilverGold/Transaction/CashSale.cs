using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Transaction
{
    public partial class CashSale : Form
    {
        #region Declare Variable
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        DataGridView.HitTestInfo hti;

        Boolean _Ratecut_Check = false;
        #endregion

        public CashSale()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            CommanHelper.ChangeGridFormate(dataGridView3);
            CommanHelper.ChangeGridFormate(dataGridView4);
            CommanHelper.ChangeGridFormate2(dataGridView5);
        }

        #region Helper

        private void Ratecut_on()
        {
            lblGFine.Visible = true;
            txtGfine.Visible = true;
            lblPfine.Visible = true;
            txtPfine.Visible = true;
            lblPamt.Visible = true;
            txtPamt.Visible = true;
            lblBal.Visible = true;
            txtBalance.Visible = true;
            lblmcx.Visible = true;
            txtPmcx.Visible = true;
            lblhazir.Visible = true;
            txtPhazir.Visible = true;
            lblGamt.Visible = true;
            txtGamt.Visible = true;
            cmbPtype.Visible = true;
            btnPOK.Visible = true;
            lblPfine.Visible = true;
            lbltype.Visible = true;
            disSil.Visible = true;
            txtDisRs.Visible = true;
            Rscr.Visible = true;
            Rsdr.Visible = true;
            sildr.Visible = true;
            silcr.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            label9.Visible = true;
            label10.Visible = true;

        }

        private void Ratecut_off()
        {
            lblGFine.Visible = false;
            txtGfine.Visible = false;
            // lblfine.Visible = false;
            txtPfine.Visible = false;
            lblPamt.Visible = false;
            txtPamt.Visible = false;
            lblBal.Visible = false;
            txtBalance.Visible = false;
            lblmcx.Visible = false;
            txtPmcx.Visible = false;
            lblhazir.Visible = false;
            txtPhazir.Visible = false;
            lblGamt.Visible = false;
            txtGamt.Visible = false;
            cmbPtype.Visible = false;
            btnPOK.Visible = false;
            lblPfine.Visible = false;
            lbltype.Visible = false;
            disSil.Visible = false;
            txtDisRs.Visible = false;
            Rscr.Visible = false;
            Rsdr.Visible = false;
            sildr.Visible = false;
            silcr.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            txtPmcx.Clear();
            txtPhazir.Clear();
            txtGamt.Clear();
            txtBalance.Clear();
            cmbPtype.SelectedIndex = -1;
            txtGfine.Clear();
            txtPfine.Clear();
            txtPamt.Clear();
            disSil.Clear();
            txtDisRs.Clear();


        }

        private void BindItems()
        {
            try
            {
                if (cmbGroup.Text.Trim() == "Metal" || cmbGroup.Text.Trim() == "")
                {
                    if (cmbCategory.Text.Trim() != "")
                    {
                        CommanHelper.BindMetalName(cmbProduct, cmbCategory.Text.Trim());
                    }
                }
                else
                {
                    CommanHelper.GetProductCategory_GroupWise(cmbProduct, cmbCategory.Text.Trim(), cmbGroup.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void KF_Visible_ON()
        {
            dataGridView3.Visible = true;
            lblsno.Visible = true;
            lbltqty.Visible = true;
            lblweight.Visible = true;
            lblfine.Visible = true;
            btnKfOK.Visible = true;
        }

        private void KF_Visible_OFF()
        {
            dataGridView3.Visible = false;
            lblsno.Visible = false;
            lbltqty.Visible = false;
            lblweight.Visible = false;
            lblfine.Visible = false;
            btnKfOK.Visible = false;

        }

        private void Return_Visible_ON()
        {
            cmbProductR.Visible = true;
            txtFineR.Visible = true;
            txtPremiumR.Visible = true;
            txtPremiumValueR.Visible = true;
            txtdiscription.Visible = true;

        }

        private void Return_Visible_OFF()
        {
            cmbProductR.Visible = false;
            txtFineR.Visible = false;
            txtPremiumR.Visible = false;
            txtPremiumValueR.Visible = false;
            txtdiscription.Visible = false;
            btnOKR.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            dataGridView4.Visible = false;
            lbltotFineP.Visible = false;
            lblPreFineP.Visible = false;
            lblPreAmtP.Visible = false;

            btnReturnOK.Visible = false;
            lbltotFineP.Text = "";
            lblPreFineP.Text = "";
            lblPreAmtP.Text = "";

            txtFineR.Clear();
            txtPremiumR.Clear();
            txtPremiumValueR.Clear();
            txtdiscription.Clear();
            cmbProductR.Text = "";
            Net.Checked = false;
            Gross.Checked = false;
            Rs.Checked = false;
            Wt.Checked = false;
        }

        private void _Clear()
        {
            lblTotalAmount.Text = "";
            lblTotalFine.Text = "";
            lblTotalWeight.Text = "";
            lblTotalPcs.Text = "";
        }


        private void PriceList_Clear()
        {
            cmbgrp2partyname.Text = "";
            cmbgrp2product.Text = "";
            dtpFrom.Text = DateTime.Now.ToString();
            dtpTo.Text = DateTime.Now.ToString();
            dataGridView2.DataSource = "";
        }

        #endregion

        private void CashSale_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            con = new OleDbConnection();
            objCon = new ConnectionClass();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");


            CashPurchaseSaleEntity oCashPurchaseSaleEntity = new CashPurchaseSaleEntity();
            oCashPurchaseSaleEntity.BindGridColumn(dataGridView1);
            oCashPurchaseSaleEntity.SetCreditLimitGridView_ColumnWith(dataGridView1);
            KFFactory.BindKFColumn(dataGridView3);
            KFFactory.SetKF_ColumnWidth(dataGridView3);
            ReturnMetalFactory.BindReturnMetalColumn(dataGridView4);
            KFFactory.BindKFColumnCheckBox(dataGridView5);

            Ratecut_off();
            _Clear();
            KF_Visible_OFF();
            CommanHelper.BindMetalCategory(cmbCategory);
            CommanHelper.GetCashParty(cmbParty, "CASH SALE");
            CommanHelper.ComboBoxItem(cmbGroup, "Product", "Distinct(PGroup)");
            cmbGroup.Items.Add("Metal");
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtp1_Enter(object sender, EventArgs e)
        {
            try
            { panel11.BackColor = Color.Red; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void dtp1_Leave(object sender, EventArgs e)
        {
            try
            { panel11.BackColor = Color.Transparent; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbCategory.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbCategory.BackColor = Color.Aqua;
                panel7.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbCategory.BackColor = Color.White;
                panel7.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbCategory.Text.Trim() != "")
                    {
                        cmbParty.Focus();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbParty_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbParty.BackColor = Color.Aqua;
                panel9.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbParty_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbParty.BackColor = Color.White;
                panel9.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbParty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbParty.Text.Trim() != "")
                    {
                        cmbGroup.Focus();
                        cmbGroup.BackColor = Color.Aqua;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbGroup_Enter(object sender, EventArgs e)
        {
            try
            { panel5.BackColor = Color.Red; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            try
            {
                panel5.BackColor = Color.Transparent;
                cmbGroup.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbParty.Text.Trim() != "")
                    {
                        cmbProduct.Focus();
                        cmbProduct.BackColor = Color.Aqua;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProduct_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbProduct.BackColor = Color.Aqua;
                //FUNCTIONCLASS.Category_text1 = cmbCategory.Text;
                //FUNCTIONCLASS.flag_product = 2;


                txtweight.TabStop = true;
                txtpcs.TabStop = true;
                txttunch1.TabStop = true;
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
                BindItems();

                panel12.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                panel12.BackColor = Color.Transparent;
                cmbProduct.BackColor = Color.White;
                if (CommanHelper.GetColumnValue("KachchiFine", "Metal", "MetalName", cmbProduct.Text.Trim().ToUpper()) == "YES")
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("Select PaatNo,Weight,Tunch1,Tunch2,Fine,Sno from KfDetails Where  BillNo='" + txtbillno.Text + "' And  TranType = 'RCF'", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dataGridView3.DataSource = ds.Tables[0];
                        dataGridView3.Columns["Sno"].Visible = false;
                    }
                    this.dataGridView3.CurrentCell = this.dataGridView3[0, 0];
                    KF_Visible_ON();
                    dataGridView3.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbProduct.Text.Trim() == "")
                    {
                        String _ForwardAction = "";
                        Object ac = _ForwardAction;
                        for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                        {
                            string _type = "";
                            if (dataGridView1.Rows[i].Cells[13].Value.ToString().Length <= 3)
                            {
                                _type = dataGridView1.Rows[i].Cells[13].Value.ToString();
                            }
                            else
                            {
                                _type = dataGridView1.Rows[i].Cells[13].Value.ToString().Substring(0, 3);
                            }
                            if (_type == "MET")
                            {
                                _ForwardAction = "WithReturn";
                            }
                        }
                        if (_ForwardAction == "WithReturn")
                        {
                            Return_Visible_ON();
                        }
                    }
                    else
                    {
                        txtweight.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtweight_Enter(object sender, EventArgs e)
        {
            try
            {
                txtweight.BackColor = Color.Aqua;
                this.txtweight.SelectAll();
                txtpcs.TabStop = true;
                txttunch1.TabStop = true;
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtweight_Leave(object sender, EventArgs e)
        {
            try
            {
                txtweight.BackColor = Color.White;
                txtpcs.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtweight_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtweight, e);
                if (e.KeyChar == 13)
                {
                    txtpcs.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtpcs_Enter(object sender, EventArgs e)
        {
            try
            {
                txtpcs.BackColor = Color.Aqua;
                this.txtpcs.SelectAll();
                txttunch1.TabStop = true;
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtpcs_Leave(object sender, EventArgs e)
        {
            try
            {
                txtpcs.BackColor = Color.White;
                if (txtpcs.Text.Trim() == "")
                {
                    txtpcs.TabStop = false;
                }
                else
                {
                    txtpcs.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtpcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtpcs, e);
                if (e.KeyChar == 13)
                    txttunch1.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch1_Enter(object sender, EventArgs e)
        {
            try
            {
                txttunch1.BackColor = Color.Aqua;
                this.txttunch1.SelectAll();
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch1_Leave(object sender, EventArgs e)
        {
            try
            {
                txttunch1.BackColor = Color.White;
                if (txttunch1.Text.Trim() == "")
                {
                    txttunch1.TabStop = false;
                }
                else
                {
                    txttunch1.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txttunch1, e);
                if (e.KeyChar == 13)
                {
                    if (txttunch1.Text.Trim() != "")
                    {
                        txttunch1.Text = String.Format("{0:0.00}", Conversion.ConTodob(txttunch1.Text.ToString()));
                    }
                    txttunch2.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch2_Enter(object sender, EventArgs e)
        {
            try
            {
                txttunch2.BackColor = Color.Aqua;
                this.txttunch2.SelectAll();
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch2_Leave(object sender, EventArgs e)
        {
            try
            {
                txttunch2.BackColor = Color.White;
                if (txttunch1.Text.Trim() == "" && txttunch2.Text.Trim() == "")
                {
                    //if ((com_metal == true) && (cmbproduct.Text.ToUpper() != "KF"))
                    //{
                    //    txttunch1.Text = "100";
                    //    txttunch2.Text = "100";
                    //}
                }
                if (txttunch2.Text.Trim() == "")
                {
                    txttunch2.TabStop = false;
                }
                else
                {
                    txttunch2.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txttunch2, e);
                if (e.KeyChar == 13)
                {
                    if (txttunch2.Text.Trim() != "")
                    {
                        txttunch2.Text = String.Format("{0:0.00}", Conversion.ConTodob(txttunch2.Text.ToString()));
                    }
                    txtwestage.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtwestage_Enter(object sender, EventArgs e)
        {
            try
            {
                txtwestage.BackColor = Color.Aqua;
                this.txtwestage.SelectAll();
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtwestage_Leave(object sender, EventArgs e)
        {
            try
            {
                txtwestage.BackColor = Color.White;
                if (txtwestage.Text.Trim() == "")
                {
                    txtwestage.TabStop = false;
                }
                else
                {
                    txtwestage.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtwestage_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtwestage, e);
                if (e.KeyChar == 13)
                {
                    if (txtwestage.Text.Trim() != "")
                    {
                        txtwestage.Text = String.Format("{0:0.00}", Conversion.ConTodob(txtwestage.Text.ToString()));
                    }
                    txtlabourrs.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtlabourrs_Enter(object sender, EventArgs e)
        {
            try
            {
                txtlabourrs.BackColor = Color.Aqua;
                this.txtlabourrs.SelectAll();
                txtnarration.TabStop = true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtlabourrs_Leave(object sender, EventArgs e)
        {
            try
            {
                txtlabourrs.BackColor = Color.White;
                txtfine.BackColor = Color.White;
                txtamount.BackColor = Color.White;
                if (txtlabourrs.Text.Trim() == "")
                {
                    txtlabourrs.TabStop = false;
                }
                else
                {
                    txtlabourrs.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtlabourrs_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtlabourrs, e);
                if (e.KeyChar == 13)
                    txtnarration.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtnarration_Enter(object sender, EventArgs e)
        {
            try
            {
                txtnarration.SelectAll();
                txtnarration.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtnarration_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtnarration.Text.Trim() == "")
                {
                    txtnarration.TabStop = false;
                }
                txtnarration.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void txtnarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (_Ratecut_Check == true)
                    {
                        btnPOK.Focus();
                    }
                    else
                    {
                        cmbgivingtype.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgivingtype_Enter(object sender, EventArgs e)
        {
            try
            { panel6.BackColor = Color.Red; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgivingtype_Leave(object sender, EventArgs e)
        {
            try
            { panel6.BackColor = Color.Transparent; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgivingtype_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    btnok.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnClosePriceL_Click(object sender, EventArgs e)
        {
            try
            {
                grpBoxPriceList.Visible = false;
                PriceList_Clear();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProductR_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbProductR.BackColor = Color.Aqua;
                CommanHelper.BindMetalName(cmbProductR, cmbCategory.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProductR_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbProductR.Text.Trim() != "")
                    {
                        if (CommanHelper.GetColumnValue("KachchiFine", "Metal", "MetalName", cmbProductR.Text.Trim().ToUpper()) == "YES")
                        {

                        }
                        else
                        {
                            txtFineR.Focus();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void cmbProductR_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbProductR.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtFineR_Enter(object sender, EventArgs e)
        {
            try
            {
                txtFineR.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtFineR_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtFineR, e);
                if (e.KeyChar == 13)
                {
                    if (Wt.Checked == true)
                    {
                        Wt.Focus();
                    }
                    else if (Rs.Checked == true)
                    {
                        Rs.Focus();
                    }
                    else
                    {
                        Wt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtFineR_Leave(object sender, EventArgs e)
        {
            try
            {
                string _wt_type = CommanHelper.GetColumnValue("WeightType", "Metal", "MetalName", cmbProductR.Text.Trim());
                txtFineR.BackColor = Color.White;
                if (txtFineR.Text != "")
                {
                    Decimal finep = Conversion.ConToDec6(txtFineR.Text);
                    if (_wt_type == "GRMS")
                    {
                        txtFineR.Text = String.Format("{0:0.000000}", finep);
                    }
                    else
                    {
                        txtFineR.Text = String.Format("{0:0.000}", finep);
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtPremiumR_Enter(object sender, EventArgs e)
        {
            try
            {
                txtPremiumR.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtPremiumR_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtPremiumR, e);
                if (e.KeyChar == 13)
                {
                    txtdiscription.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtPremiumR_Leave(object sender, EventArgs e)
        {
            try
            {
                string _wt_type = CommanHelper.GetColumnValue("WeightType", "Metal", "MetalName", cmbProductR.Text.Trim());
                txtPremiumR.BackColor = Color.White;
                if (txtPremiumR.Text != "")
                {
                    if (Wt.Checked == true)
                    {
                        decimal finep = Conversion.ConToDec6(txtPremiumR.Text);
                        if (_wt_type == "GRMS")
                        {
                            txtPremiumR.Text = String.Format("{0:0.000000}", finep);
                        }
                        else
                        {
                            txtPremiumR.Text = String.Format("{0:0.000}", finep);
                        }
                    }
                    else
                    {
                        Decimal finep = Conversion.ConToDec6(txtPremiumR.Text);
                        txtPremiumR.Text = finep.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Gross_Enter(object sender, EventArgs e)
        {
            try
            {
                Gross.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Gross_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (Wt.Checked == true)
                    {
                        Wt.Focus();
                    }
                    else if (Rs.Checked == true)
                    {
                        Rs.Focus();
                    }
                    else
                    {
                        Wt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Gross_Leave(object sender, EventArgs e)
        {
            try
            {
                Gross.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Net_Enter(object sender, EventArgs e)
        {
            try
            {
                Net.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Net_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (Wt.Checked == true)
                    {
                        Wt.Focus();
                    }
                    else if (Rs.Checked == true)
                    {
                        Rs.Focus();
                    }
                    else
                    {
                        Wt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Net_Leave(object sender, EventArgs e)
        {
            try
            {
                Net.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Wt_Enter(object sender, EventArgs e)
        {
            try
            {
                Wt.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Wt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    txtPremiumR.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Wt_Leave(object sender, EventArgs e)
        {
            try
            {
                Wt.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Rs_Enter(object sender, EventArgs e)
        {
            try
            {
                Rs.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Rs_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    txtPremiumR.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Rs_Leave(object sender, EventArgs e)
        {
            try
            {
                Rs.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtdiscription_Enter(object sender, EventArgs e)
        {
            try
            {
                txtdiscription.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtdiscription_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    btnOKR.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtdiscription_Leave(object sender, EventArgs e)
        {
            try
            {
                txtdiscription.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrp2partyname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbgrp2partyname.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrp2partyname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                    cmbgrp2product.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrp2partyname_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbgrp2partyname.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrp2product_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbgrp2product.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrp2product_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                    dtpFrom.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrp2product_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbgrp2product.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtpFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                    dtpTo.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnOKR_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnReturnOK_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnKfOK_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPopUp_Enter(object sender, EventArgs e)
        {
            try
            {
                plnpopup.BackColor = Color.RosyBrown;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPopUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { if (e.KeyChar == 13)dtp1.Focus(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPopUp_Leave(object sender, EventArgs e)
        {
            try { plnpopup.BackColor = Color.Transparent; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            try { panel10.BackColor = Color.RosyBrown; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try { if (e.KeyChar == 13) { listBox1.Visible = true; listBox1.Focus(); } }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            try { panel10.BackColor = Color.Transparent; }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
