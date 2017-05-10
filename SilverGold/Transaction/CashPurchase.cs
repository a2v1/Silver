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
    public partial class CashPurchase : Form
    {
        #region Declare Variable
        List<CashPurchaseSaleEntity> CashPurchaseSaleList = new List<CashPurchaseSaleEntity>();
        List<TunchPendingEntity> TunchPendingList = new List<TunchPendingEntity>();
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        DataGridView.HitTestInfo hti;
        int Row_No = -1;
        Decimal _Old_westage = 0;
        Decimal _Old_labour = 0;
        String _Tunch_pending_YN = "";
        String _Tunch_Update = "";
        int _TunchSno = -1;
        String _Tunch1LastValue = "";
        String _Tunch2LastValue = "";
        public static int _Flage_TunchPending_CR = 0;
        public static int _TunchSno_TunchPending_CR = 0;

        Boolean _Ratecut_Check = false;

        #endregion
        public CashPurchase()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
        }
        #region Mapper

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

        public void Cal_Amount()
        {
            try
            {
                String _Wt_Type = "";
                Double _Pcs = 0; Double _Amount = 0; Double _Weight = 0; Double _LabourRs = 0;

                _Wt_Type = CommanHelper.GetColumnValue("WeightType", "Metal", "MetalName", "");

                _Weight = Conversion.ConTodob(txtweight.Text);
                _Pcs = Conversion.ConTodob(txtpcs.Text);
                _LabourRs = Conversion.ConTodob(txtlabourrs.Text);

                if (_Pcs > 0)
                    _Amount = System.Math.Round((_Pcs * _LabourRs), 0);
                else
                    _Amount = System.Math.Round((_Weight * _LabourRs), 0);

                if (_Amount > 0)
                    txtamount.Text = _Amount.ToString();
                else
                    txtamount.Text = "";

                Double _Fine1 = 0; Double _Bhaav1 = 0; Double _Amount1 = 0; Double _Mcxrate = 0; Double _Labourrs = 0;
                _Labourrs = Conversion.ConTodob(txtamount.Text);
                _Mcxrate = Conversion.ConTodob(txtPmcx.Text);
                _Fine1 = Conversion.ConTodob(txtBalance.Text);
                _Bhaav1 = Conversion.ConTodob(txtPhazir.Text);
                //_Amount1 = Conversion.ConTodob(txtamount.Text);

                if (_Wt_Type == "GRMS")
                    _Amount1 = ((_Mcxrate + _Bhaav1) * _Fine1) * 100 + _Labourrs;
                else
                    _Amount1 = ((_Mcxrate + _Bhaav1) * _Fine1) + _Labourrs;


                txtGamt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(_Amount1.ToString())));
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }


        public void Cal_Fine()
        {
            if (cmbProduct.Text.Trim().ToUpper() != "KF")
            {
                Double _Weight = 0; Double _Tunch1 = 0; Double _Tunch2 = 0; Double _Fine = 0; Double _Westage = 0; Double _mTunch = 0;

                _Weight = Conversion.ConTodob(txtweight.Text);
                _Tunch1 = Conversion.ConTodob(txttunch1.Text);
                _Tunch2 = Conversion.ConTodob(txttunch2.Text);
                _Westage = Conversion.ConTodob(txtwestage.Text);

                if (_Tunch1 > 0)
                {
                    _mTunch = _Tunch1;
                }
                if (_Tunch2 > 0)
                {
                    _mTunch = _Tunch2;
                }
                if (_Tunch1 > 0 && _Tunch2 > 0)
                {
                    _mTunch = (_Tunch1 + _Tunch2) / 2;
                }
                if (CommanHelper.CheckGram_Metal(cmbProduct.Text.Trim()))
                {
                    _Fine = System.Math.Round(((_mTunch + _Westage) * _Weight) / 100, 6);
                }
                else
                {
                    _Fine = System.Math.Round(((_mTunch + _Westage) * _Weight) / 100, 3);
                }

                if (_Fine > 0)
                {
                    if (CommanHelper.CheckGram_Metal(cmbProduct.Text.Trim()))
                    {
                        txtfine.Text = String.Format("{0:0.000000}", Conversion.ConTodob(_Fine.ToString()));
                    }
                    else
                    {
                        txtfine.Text = String.Format("{0:0.000}", Conversion.ConTodob(_Fine.ToString()));
                    }
                }
            }
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
            dtpgrp2From.Text = DateTime.Now.ToString();
            dtpgrp2To.Text = DateTime.Now.ToString();
            dataGridView2.DataSource = "";
        }


        private void Total()
        {
            try
            {
                lblTotalAmount.Text = "";
                lblTotalFine.Text = "";
                lblTotalWeight.Text = "";
                lblTotalPcs.Text = "";
                if ((CommanHelper.SumRow1(dataGridView1, 10) + CommanHelper.SumRow1(dataGridView1, 9)) > 0)
                {
                    lblTotalAmount.Text = CommanHelper.SumRow1(dataGridView1, 11).ToString();
                }

                if (CommanHelper.SumRow1(dataGridView1, 8) != 0)
                {
                    int count_row = 0;
                    Decimal sum_col, col1;
                    sum_col = 0;
                    string sum_colR = "";

                    col1 = 0;

                    count_row = dataGridView1.Rows.Count;
                    for (int i = 0; i < count_row; i++)
                    {
                        if ((dataGridView1.Rows[i].Cells[1].Value.ToString() != "RETURN Fine") && (dataGridView1.Rows[i].Cells[1].Value.ToString().ToUpper() != "DISCOUNT AMT") && (dataGridView1.Rows[i].Cells[1].Value.ToString().ToUpper() != "DISCOUNT FINE") && (dataGridView1.Rows[i].Cells[1].Value.ToString() != "RATE CUT"))
                        {
                            col1 = Conversion.ConToDec(dataGridView1.Rows[i].Cells[8].Value);
                            sum_col = col1 + sum_col;
                        }
                        if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "RETURN Fine")
                        {
                            col1 = Conversion.ConToDec(dataGridView1.Rows[i].Cells[8].Value);
                            sum_colR = " (T) / " + col1.ToString() + " (R)";
                        }
                    }

                    string temp = String.Format("{0:0.000}", sum_col.ToString());
                    string temp2 = temp.ToString() + sum_colR.ToString();
                    textBox1.Text = temp2;
                }

                if (CommanHelper.SumRow1(dataGridView1, 2) > 0)
                {
                    if (CommanHelper.CheckGram_Metal(cmbProduct.Text.Trim()))
                    {
                        lblTotalWeight.Text = String.Format("{0:0.000000}", CommanHelper.SumRow1(dataGridView1, 2).ToString());
                    }
                    else
                    {
                        lblTotalWeight.Text = String.Format("{0:0.000}", CommanHelper.SumRow1(dataGridView1, 2).ToString());
                    }
                }
                if (CommanHelper.SumRow1(dataGridView1, 3) > 0)
                {
                    lblTotalPcs.Text = CommanHelper.SumRow1(dataGridView1, 3).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion


        private void CashPurchase_Load(object sender, EventArgs e)
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

            Ratecut_off();
            _Clear();
            CommanHelper.BindMetalCategory(cmbCategory);
            CommanHelper.GetCashParty(cmbParty, "CASH PURCHASE");
            CommanHelper.ComboBoxItem(cmbGroup, "Product", "Distinct(PGroup)");
            cmbGroup.Items.Add("Metal");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

            }
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
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtp1_Enter(object sender, EventArgs e)
        {
            try
            {
                panel11.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtp1_Leave(object sender, EventArgs e)
        {
            try
            {
                panel11.BackColor = Color.Transparent;
            }
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
            {
                panel5.BackColor = Color.Red;
            }
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
                txtweight.Focus();
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
                if (e.KeyChar == 13) txttunch1.Focus();
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
                if (e.KeyChar == 13) txtnarration.Focus();
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

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbProduct.Text = "";
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtweight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cal_Amount();
                Cal_Fine();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtpcs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cal_Fine();
                Cal_Amount();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cal_Fine();
                Cal_Amount();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cal_Fine();
                Cal_Amount();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtwestage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cal_Fine();
                Cal_Amount();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtlabourrs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cal_Fine();
                Cal_Amount();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }



        private void txtfine_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                CommanHelper.IsNumericTextBox(txtfine, e);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                _Tunch_pending_YN = "";

                String _Category = "";
                String _PartyName = "";
                String _PartyCategory = "";
                _Category = cmbCategory.Text;
                _PartyName = cmbParty.Text.Trim();
                _PartyCategory = CommanHelper.GetColumnValue("Category", "PartyDetails", "PartyName", cmbParty.Text.Trim());

                Double _Weight = Conversion.ConTodob(txtweight.Text);
                if (_Weight == 0)
                {
                    MessageBox.Show("Please Enter Weight", "CASH(RECIEVE)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtweight.Focus();
                    return;
                }
                if (txttunch1.Text == "" && txttunch2.Text == "" && txtwestage.Text == "")
                {
                    if (cmbProduct.Text.ToUpper() != "KF")
                    {
                        MessageBox.Show("Please Enter Tunch/Westage", "CASH(RECIEVE)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txttunch1.Focus();
                        return;
                    }
                }
                if (cmbParty.Text.Trim() == "")
                {
                    MessageBox.Show("Plz Select A Party", "CASH(RECIEVE)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbParty.Focus();
                    return;
                }
                if (cmbProduct.Text.Trim() == "")
                {
                    cmbProduct.Focus();
                    return;
                }
                if (_TunchSno == -1)
                {
                    if (CashPurchaseSaleList.Count() == 0)
                    {
                        _TunchSno = CommanHelper.Get_Tunch_Sl_No("CR");
                    }
                    else
                    {
                        _TunchSno = CashPurchaseSaleList.Max(x => x.TunchSno) + 1;
                        if (_TunchSno < CommanHelper.Get_Tunch_Sl_No("CR"))
                        {
                            _TunchSno = CommanHelper.Get_Tunch_Sl_No("CR");
                        }
                    }
                }
                if (txtbillno.Text == "")
                {
                    txtbillno.Text = 'C' + CommanHelper.Pro_AutoCode("PartyTran", "BillNo", "TranType", "CR");
                }
                if (Row_No != -1)
                {
                    //----Update Cash Transaction Data

                    var result = (from r in CashPurchaseSaleList where r.Sno == Row_No select r).SingleOrDefault();
                    result.PGroup = cmbGroup.Text.Trim();
                    result.Product = cmbProduct.Text.Trim();
                    result.Weight = Conversion.ConToDec(txtweight.Text.Trim());
                    result.Pcs = Conversion.ConToDec(txtpcs.Text.Trim());
                    result.Tunch1 = Conversion.ConToDec(txttunch1.Text.Trim());
                    result.Tunch2 = Conversion.ConToDec(txttunch2.Text.Trim());
                    result.Westage = Conversion.ConToDec(txtwestage.Text.Trim());
                    result.LaboursRate = Conversion.ConToDec(txtlabourrs.Text.Trim());
                    result.Fine = Conversion.ConToDec(txtfine.Text.Trim());
                    result.Amount = Conversion.ConToDec(txtamount.Text.Trim());
                    if (txtnarration.Text.Trim() == "")
                        result.Narration = "CASH PURCHASE";
                    else
                        result.Narration = txtnarration.Text.Trim();

                    result.GivingType = cmbgivingtype.Text;
                    result.TunchSno = _TunchSno;


                    if (_Tunch_Update != "U")
                    {
                        if (_Tunch_pending_YN == "Y")
                        {
                            //---  Update TunchPending Data
                            var uTunchPending = TunchPendingList.Where(x => x.TunchSno == _TunchSno && x.BillNo == txtbillno.Text.Trim()).FirstOrDefault();
                            uTunchPending.BillNo = txtbillno.Text.Trim();
                            uTunchPending.TrDate = Conversion.GetDateStr(dtp1.Text.Trim());
                            uTunchPending.PartyCate = _PartyCategory;
                            uTunchPending.PartyName = _PartyName;
                            uTunchPending.Category = _Category;
                            uTunchPending.Product = cmbProduct.Text.Trim();
                            uTunchPending.Weight = Conversion.ConToDec(txtweight.Text.Trim());
                            uTunchPending.TunchValue1 = Conversion.ConToDec(txttunch1.Text.Trim());
                            uTunchPending.TunchValue2 = Conversion.ConToDec(txttunch2.Text.Trim());

                            if (_Flage_TunchPending_CR == 1)
                            {
                                var update = TunchPendingList.Where(x => x.TunchSno == _TunchSno_TunchPending_CR).FirstOrDefault();
                                update.Tunch1 = "N";

                                if (TunchPendingList.Where(x => x.TunchSno == _TunchSno_TunchPending_CR).FirstOrDefault().Tunch2 == "Y")
                                {
                                    if (txttunch2.Text.Trim() != "")
                                    {
                                        var updateTunch2 = TunchPendingList.Where(x => x.TunchSno == _TunchSno_TunchPending_CR).FirstOrDefault();
                                        updateTunch2.Tunch2 = "N";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Do You Want change Updated Tunch ?", "CASH(RECIEVE)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            txttunch1.Text = _Tunch1LastValue;
                            txttunch2.Text = _Tunch2LastValue;
                        }
                    }
                }
                else
                {
                    String _Narr = "";
                    if (txtnarration.Text.Trim() == "") _Narr = "CASH PURCHASE"; else _Narr = txtnarration.Text.Trim();
                    var max = 0;
                    if (CashPurchaseSaleList.Count > 0)
                    {
                        max = CashPurchaseSaleList.Max(x => x.Sno) + 1;
                    }

                    CashPurchaseSaleEntity oCashPurchaseSaleEntity = new CashPurchaseSaleEntity();
                    oCashPurchaseSaleEntity.AddCashPurchaseSale(cmbGroup.Text.Trim(), cmbProduct.Text.Trim(), Conversion.ConToDec(txtweight.Text.Trim()), Conversion.ConToDec(txtpcs.Text.Trim()), Conversion.ConToDec(txttunch1.Text.Trim()), Conversion.ConToDec(txttunch2.Text.Trim()), Conversion.ConToDec(txtwestage.Text.Trim()), Conversion.ConToDec(txtlabourrs.Text.Trim()), Conversion.ConToDec(txtfine.Text.Trim()), 0, 0, Conversion.ConToDec6(txtamount.Text.Trim()), _Narr, cmbgivingtype.Text.Trim(), _TunchSno, max);
                    CashPurchaseSaleList.Add(oCashPurchaseSaleEntity);
                    if (_Tunch_pending_YN == "Y")
                    {
                        TunchPendingEntity oTunchPendingEntity = new TunchPendingEntity();
                        oTunchPendingEntity.AddTunchPending(txtbillno.Text.Trim(), Conversion.GetDateStr(dtp1.Text.Trim()), _PartyCategory, _PartyName, _Category, cmbProduct.Text.Trim(), Conversion.ConToDec(txtweight.Text.Trim()), Conversion.ConToDec(txttunch1.Text.Trim()), Conversion.ConToDec(txttunch2.Text.Trim()), "Y", "Y", "CR", _TunchSno, CommanHelper.CompName.ToString(), CommanHelper.UserId.ToString());
                        TunchPendingList.Add(oTunchPendingEntity);
                    }
                }




                if (_Tunch_Update != "U")
                {
                    if (_Tunch_pending_YN == "Y")
                    {
                        if (cmbPopUp.Text == "")
                        {
                            if (_Flage_TunchPending_CR == 0)
                            {
                                if (_TunchSno != 0)
                                {
                                    DialogResult result;
                                    if ((_Tunch_Update == "P1") || (_Tunch_Update == ""))
                                    {
                                        result = MessageBox.Show("Do You Want Tunch Pending 2", "CASH(RECIEVE)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    }
                                    else
                                    {
                                        result = MessageBox.Show("Do You Want Tunch Pending 2", "CASH(RECIEVE)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                    }

                                    if (result == DialogResult.Yes)
                                    {
                                        if (_TunchSno != 0)
                                        {
                                            TunchPendingList.Where(x => x.TunchSno == _TunchSno && x.BillNo == txtbillno.Text.Trim()).FirstOrDefault().Tunch2 = "Y";
                                        }
                                    }
                                    else
                                    {
                                        if (_TunchSno != 0)
                                        {
                                            TunchPendingList.Where(x => x.TunchSno == _TunchSno && x.BillNo == txtbillno.Text.Trim()).FirstOrDefault().Tunch2 = "";
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (_Flage_TunchPending_CR == 0)
                            {
                                if (_TunchSno != 0)
                                {
                                    DialogResult result;
                                    if ((_Tunch_Update == "P1") || (_Tunch_Update == ""))
                                    {
                                        result = MessageBox.Show("Do You Want Tunch Pending 2", "CASH(RECIEVE)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    }
                                    else
                                    {
                                        result = MessageBox.Show("Do You Want Tunch Pending 2", "CASH(RECIEVE)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                    }
                                    if (result == DialogResult.Yes)
                                    {
                                        if (_TunchSno != 0)
                                        {
                                            TunchPendingList.Where(x => x.TunchSno == _TunchSno && x.BillNo == txtbillno.Text.Trim()).FirstOrDefault().Tunch2 = "Y";
                                        }
                                    }
                                    else
                                    {
                                        if (_TunchSno != 0)
                                        {
                                            TunchPendingList.Where(x => x.TunchSno == _TunchSno && x.BillNo == txtbillno.Text.Trim()).FirstOrDefault().Tunch2 = "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                dataGridView1.DataSource = CashPurchaseSaleList.ToList();

                Total();
                _TunchSno = -1;
                Row_No = -1;
                cmbProduct.SelectedIndex = -1;
                cmbProduct.Text = "";
                txtweight.Clear();
                txtpcs.Clear();
                txttunch1.Clear();
                txttunch2.Clear();
                txtwestage.Clear();
                txtlabourrs.Clear();
                txtfine.Clear();
                txtamount.Clear();
                txtnarration.Clear();

                txtweight.TabStop = true;
                txtpcs.TabStop = true;
                txttunch1.TabStop = true;
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtnarration.TabStop = true;
                cmbProduct.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _Ratecut_Check = false;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgivingtype_Enter(object sender, EventArgs e)
        {
            try
            {
                panel6.BackColor = Color.Red;
            }
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

        private void cmbgivingtype_Leave(object sender, EventArgs e)
        {
            try
            {
                panel6.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
