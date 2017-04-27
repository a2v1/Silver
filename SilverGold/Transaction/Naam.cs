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
    public partial class Naam : Form
    {
        #region Declare Variables
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;

        List<JamaNaamEntity> JamaNaamList = new List<JamaNaamEntity>();
        List<OpeningOtherEntity> oOpeningOtherEntity = new List<OpeningOtherEntity>();
        List<TunchPendingEntity> TunchPendingList = new List<TunchPendingEntity>();
        DataGridView.HitTestInfo hti;
        Decimal _Old_westage = 0;
        Decimal _Old_labour = 0;
        #endregion
        public Naam()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            CommanHelper.ChangeGridFormate2(dataGridView2);
        }


        #region Mapper



        private void Cal_Amount()
        {
            Decimal Pcs, Amount, Weight, LabourRs;
            Pcs = 0;
            Weight = 0;
            Amount = 0;
            LabourRs = 0;
            Weight = Conversion.ConToDec(txtweight.Text);
            Pcs = Conversion.ConToDec(txtpcs.Text);
            LabourRs = Conversion.ConToDec(txtlabourrs.Text);
            if (Pcs > 0)
            {
                Amount = System.Math.Round((Pcs * LabourRs), 0);
            }
            else
            {
                Amount = System.Math.Round((Weight * LabourRs), 0);
            }
            if (Amount > 0)
            {
                txtamount.Text = Amount.ToString();
            }
            else
            {
                txtamount.Text = "";
            }
        }

        private void Cal_Fine()
        {
            Decimal Weight, Tunch1, Tunch2, Fine, Westage, mTunch;
            Weight = 0;
            Tunch1 = 0;
            Tunch2 = 0;
            Westage = 0;
            mTunch = 0;
            Weight = Conversion.ConToDec6(txtweight.Text);
            Tunch1 = Conversion.ConToDec6(txttunch1.Text);
            Tunch2 = Conversion.ConToDec6(txttunch2.Text);
            Westage = Conversion.ConToDec6(txtwestage.Text);
            if (Tunch1 > 0)
            {
                mTunch = Tunch1;
            }
            if (Tunch2 > 0)
            {
                mTunch = Tunch2;
            }
            if (Tunch1 > 0 && Tunch2 > 0)
            {
                mTunch = (Tunch1 + Tunch2) / 2;
            }
            if (CommanHelper.CheckGram_Metal(cmbproduct.Text.Trim()) == true)
            {
                Fine = System.Math.Round(((mTunch + Westage) * Weight) / 100, 6);
            }
            else
            {
                Fine = System.Math.Round(((mTunch + Westage) * Weight) / 100, 3);
            }

            if (Fine > 0)
            {
                txtfine.Text = Fine.ToString();
            }
            else
            {
                txtfine.Text = "";
            }
        }
        public void AutoCode(string str1, string str2)
        {
            txtbillno.Text = str1 + CommanHelper.Pro_AutoCode("PartyTran", "BillNo", "TranType", str2);
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
            cmbPartyName_PriseList.Text = "";
            cmbProduct_PriceList.Text = "";
            dtpFrom.Text = DateTime.Now.ToString();
            dtpTo.Text = DateTime.Now.ToString();
            dataGridView2.DataSource = "";
        }

        #endregion

        private void Naam_Load(object sender, EventArgs e)
        {

            lblTunchPending.Text = "";
            this.CancelButton = btnClose;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            con = new OleDbConnection();
            objCon = new ConnectionClass();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

            JamaNaamEntity oJamaNaamEntity = new JamaNaamEntity();
            oJamaNaamEntity.BindGridColumn(dataGridView1);
            oJamaNaamEntity.SetCreditLimitGridView_ColumnWith(dataGridView1);

            _Clear();
            oOpeningOtherEntity = CommanHelper.OpeningOther();
            cmbCategory.DataSource = oOpeningOtherEntity;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.SelectedIndex = -1;

            CommanHelper.ComboBoxItem(cmbGroup, "Product", "Distinct(PGroup)");
            CommanHelper.ComboBoxItem(cmbPopUp, "PartyTran", "Distinct(BillNo)", "TranType", "GG");
            CommanHelper.GetParty(Cmbparty, "PARTY");
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grpPriceList.Visible = false;
                txtweight.Clear();
                txtpcs.Clear();
                txttunch1.Clear();
                txttunch2.Clear();
                txtwestage.Clear();
                txtfine.Clear();
                txtamount.Clear();
                txtlabourrs.Clear();
                cmbproduct.Text = "";
                CommanHelper.GetProductCategory_GroupWise(cmbproduct, cmbCategory.Text.Trim(), cmbGroup.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Cmbparty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblTunchPending.Text = "";
                txtweight.Clear();
                txtpcs.Clear();
                txttunch1.Clear();
                txttunch2.Clear();
                txtwestage.Clear();
                txtfine.Clear();
                txtamount.Clear();
                txtlabourrs.Clear();
                grpPriceList.Visible = false;
                CommanHelper.GetProductCategory_GroupWise(cmbproduct, cmbCategory.Text.Trim(), cmbGroup.Text.Trim());

                if (CommanHelper.CheckTunchPending(Cmbparty.Text.Trim(), Conversion.GetDateStr(dtp1.Text.Trim())) == true)
                {
                    lblTunchPending.Text = "(Tunchpending)";
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
            { CommanHelper.GetProductCategory_GroupWise(cmbproduct, cmbCategory.Text.Trim(), cmbGroup.Text.Trim()); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbproduct.Text.Trim() != "")
                {
                    txttunch1.Text = Math.Round(Conversion.ConToDec(CommanHelper.GetColumnValue("Tunch", "Product", "ProductName", cmbproduct.Text.Trim())), 2).ToString();

                    txtwestage.Clear();

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand("Select Westage,LabourRs From PriceList Where sno=(Select Max(sno) From PriceList Where PartyName='" + Cmbparty.Text + "' And Product='" + cmbproduct.Text + "' and TranType='GR')", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        _Old_westage = Conversion.ConToDec(dr[0].ToString());
                        _Old_labour = Conversion.ConToDec(dr[1].ToString());

                        if (_Old_westage != 0)
                            txtwestage.Text = _Old_westage.ToString();
                        else
                            txtwestage.Text = "";

                        if (_Old_labour != 0)
                            txtlabourrs.Text = _Old_labour.ToString();
                        else
                            txtlabourrs.Text = "";
                    }
                    con.Close();
                }
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
            { Cal_Amount(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch1_TextChanged(object sender, EventArgs e)
        {
            try
            { Cal_Fine(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txttunch2_TextChanged(object sender, EventArgs e)
        {
            try
            { Cal_Fine(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtwestage_TextChanged(object sender, EventArgs e)
        {
            try
            { Cal_Fine(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtlabourrs_TextChanged(object sender, EventArgs e)
        {
            try
            { Cal_Amount(); }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
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
            { this.Close(); }
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

        private void cmbPopUp_SelectedIndexChanged(object sender, EventArgs e)
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
            { panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))); }
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
                panel9.BackColor = Color.Red;
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
                    Cmbparty.Focus();
                }
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
                panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Cmbparty_Enter(object sender, EventArgs e)
        {
            try
            {
                Cmbparty.BackColor = Color.Aqua;
                panel10.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Cmbparty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbGroup.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Cmbparty_Leave(object sender, EventArgs e)
        {
            try
            {
                Cmbparty.BackColor = Color.White;
                panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
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
                cmbGroup.BackColor = Color.Aqua;
                Grpanel.BackColor = Color.Red;
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
                    cmbproduct.Focus();
                }
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
                Grpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                cmbGroup.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbproduct_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbproduct.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbproduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbproduct.Text.Trim() == "")
                    {
                        btnSave.Focus();
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

        private void cmbproduct_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbproduct.BackColor = Color.White;
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
                txtpcs.TabStop = true;
                txttunch1.TabStop = true;
                txttunch2.TabStop = true;

                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtdescription.TabStop = true;
                txtweight.BackColor = Color.Aqua;
                this.txtweight.SelectAll();
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
                //if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                //{
                //}
                //else
                //{
                //    e.Handled = e.KeyChar != (char)Keys.Back;
                //}


                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }

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

        private void txtweight_Leave(object sender, EventArgs e)
        {
            try
            {
                txtweight.BackColor = Color.White;
                if (txtweight.Text == "")
                {
                    txtweight.TabStop = false;
                }
                else
                {
                    txtweight.TabStop = true;
                }
                if (txtweight.Text != "")
                {
                    if (CommanHelper.CheckGram_Metal(cmbproduct.Text.Trim()) == true)
                    {
                        decimal finep = Conversion.ConToDec6(txtweight.Text);
                        txtweight.Text = String.Format("{0:0.000000}", finep);
                    }
                    else
                    {
                        decimal finep = Conversion.ConToDec6(txtweight.Text);
                        txtweight.Text = String.Format("{0:0.000}", finep);
                    }
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
                txttunch1.TabStop = true;
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtdescription.TabStop = true;
                txtpcs.BackColor = Color.Aqua;
                this.txtpcs.SelectAll();
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
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                {
                }
                else
                {
                    e.Handled = e.KeyChar != (char)Keys.Back;
                }
                if (e.KeyChar == 13)
                {
                    txttunch1.Focus();
                }
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
                if (txtpcs.Text == "")
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

        private void txttunch1_Enter(object sender, EventArgs e)
        {
            try
            {
                txttunch1.BackColor = Color.Aqua;
                this.txttunch1.SelectAll();
                txttunch2.TabStop = true;
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtdescription.TabStop = true;
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
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                {
                }
                else
                {
                    e.Handled = e.KeyChar != (char)Keys.Back;
                }
                if (e.KeyChar == 13)
                {
                    txttunch2.Focus();
                }
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
                if (txttunch1.Text == "")
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

        private void txttunch2_Enter(object sender, EventArgs e)
        {
            try
            {
                txttunch2.BackColor = Color.Aqua;
                this.txttunch2.SelectAll();
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtdescription.TabStop = true;
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
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                {
                }
                else
                {
                    e.Handled = e.KeyChar != (char)Keys.Back;
                }
                if (e.KeyChar == 13)
                {
                    txtwestage.Focus();
                }
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
                if (txttunch2.Text == "")
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

        private void txtwestage_Enter(object sender, EventArgs e)
        {
            try
            {
                txtwestage.BackColor = Color.Aqua;
                this.txtwestage.SelectAll();
                txtwestage.TabStop = true;
                txtlabourrs.TabStop = true;
                txtdescription.TabStop = true;
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
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                {
                }
                else
                {
                    e.Handled = e.KeyChar != (char)Keys.Back;
                }
                if (e.KeyChar == 13)
                {
                    txtlabourrs.Focus();
                }
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
                if (txtwestage.Text == "")
                {
                    txtwestage.TabStop = false;
                }
                else
                {
                    txtwestage.TabStop = true;

                }
                if (txtwestage.Text != "")
                {
                    if (CommanHelper.CheckGram_Metal(cmbproduct.Text.Trim()) == true)
                    {
                        decimal finep = Conversion.ConToDec6(txtwestage.Text);
                        txtwestage.Text = String.Format("{0:0.000000}", finep);
                    }
                    else
                    {
                        decimal finep = Conversion.ConToDec6(txtwestage.Text);
                        txtwestage.Text = String.Format("{0:0.000}", finep);
                    }
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
                txtdescription.TabStop = true;
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
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                {
                }
                else
                {
                    e.Handled = e.KeyChar != (char)Keys.Back;
                }
                if (e.KeyChar == 13)
                {
                    txtdescription.Focus();
                }
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
                if (txtlabourrs.Text == "")
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

        private void txtdescription_Enter(object sender, EventArgs e)
        {
            try
            {
                txtdescription.BackColor = Color.Aqua;
                txtdescription.SelectAll();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtdescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    btnOk.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtdescription_Leave(object sender, EventArgs e)
        {
            try
            {
                txtdescription.BackColor = Color.White;
                if (txtdescription.Text == "")
                {
                    txtdescription.TabStop = false;
                }
                else
                {
                    txtdescription.TabStop = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
