using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
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

namespace SilverGold.Transaction
{
    public partial class Jama : Form
    {
        #region Declare Variables
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        List<JamaNaamEntity> JamaNaamList = new List<JamaNaamEntity>();
        List<OpeningOtherEntity> oOpeningOtherEntity = new List<OpeningOtherEntity>();
        List<TunchPendingEntity> TunchPendingList = new List<TunchPendingEntity>();
        DataGridView.HitTestInfo hti;
        int Row_No = -1;

        Decimal _Old_westage = 0;
        Decimal _Old_labour = 0;
        String _Tunch_pending_YN = "";
        String _Tunch_Update = "";
        int _TunchSno = -1;
        String _Tunch1LastValue = "";
        String _Tunch2LastValue = "";

        #endregion

        public Jama()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            BindGridColumn();
        }


        #region Mapper

        private void BindGridColumn()
        {

            DataGridViewColumn col_Group = new DataGridViewTextBoxColumn();
            col_Group.DataPropertyName = "PGroup";
            col_Group.HeaderText = "PGroup";
            col_Group.Name = "PGroup";
            dataGridView1.Columns.Add(col_Group);

            DataGridViewColumn col_Product = new DataGridViewTextBoxColumn();
            col_Product.DataPropertyName = "Product";
            col_Product.HeaderText = "Product";
            col_Product.Name = "Product";
            dataGridView1.Columns.Add(col_Product);

            DataGridViewColumn col_Weight = new DataGridViewTextBoxColumn();
            col_Weight.DataPropertyName = "Weight";
            col_Weight.HeaderText = "Weight";
            col_Weight.Name = "Weight";
            dataGridView1.Columns.Add(col_Weight);

            DataGridViewColumn col_Pcs = new DataGridViewTextBoxColumn();
            col_Pcs.DataPropertyName = "Pcs";
            col_Pcs.HeaderText = "Pcs";
            col_Pcs.Name = "Pcs";
            dataGridView1.Columns.Add(col_Pcs);

            DataGridViewColumn col_Tunch1 = new DataGridViewTextBoxColumn();
            col_Tunch1.DataPropertyName = "Tunch1";
            col_Tunch1.HeaderText = "Tunch";
            col_Tunch1.Name = "Tunch1";
            dataGridView1.Columns.Add(col_Tunch1);

            DataGridViewColumn col_Tunch2 = new DataGridViewTextBoxColumn();
            col_Tunch2.DataPropertyName = "Tunch2";
            col_Tunch2.HeaderText = "Tunch2";
            col_Tunch2.Name = "Tunch2";
            dataGridView1.Columns.Add(col_Tunch2);

            DataGridViewColumn col_Westage = new DataGridViewTextBoxColumn();
            col_Westage.DataPropertyName = "Westage";
            col_Westage.HeaderText = "Westage";
            col_Westage.Name = "Westage";
            dataGridView1.Columns.Add(col_Westage);

            DataGridViewColumn col_LabourFine = new DataGridViewTextBoxColumn();
            col_LabourFine.DataPropertyName = "LaboursRate";
            col_LabourFine.HeaderText = "LaboursFine";
            col_LabourFine.Name = "LaboursRate";
            dataGridView1.Columns.Add(col_LabourFine);

            DataGridViewColumn col_Fine = new DataGridViewTextBoxColumn();
            col_Fine.DataPropertyName = "Fine";
            col_Fine.HeaderText = "Fine";
            col_Fine.Name = "Fine";
            dataGridView1.Columns.Add(col_Fine);

            DataGridViewColumn col_Amount = new DataGridViewTextBoxColumn();
            col_Amount.DataPropertyName = "Amount";
            col_Amount.HeaderText = "Amount";
            col_Amount.Name = "Amount";
            dataGridView1.Columns.Add(col_Amount);

            DataGridViewColumn col_Narration = new DataGridViewTextBoxColumn();
            col_Narration.DataPropertyName = "Narration";
            col_Narration.HeaderText = "Narration";
            col_Narration.Name = "Narration";
            dataGridView1.Columns.Add(col_Narration);

            DataGridViewColumn col_TunchSno = new DataGridViewTextBoxColumn();
            col_TunchSno.DataPropertyName = "TunchSno";
            col_TunchSno.HeaderText = "TunchSno";
            col_TunchSno.Name = "TunchSno";
            col_TunchSno.Visible = false;
            dataGridView1.Columns.Add(col_TunchSno);

            DataGridViewColumn col_Sno = new DataGridViewTextBoxColumn();
            col_Sno.DataPropertyName = "Sno";
            col_Sno.HeaderText = "Sno";
            col_Sno.Name = "Sno";
            col_Sno.Visible = false;
            dataGridView1.Columns.Add(col_Sno);


        }

        private void SetCreditLimitGridView_ColumnWith()
        {
            dataGridView1.Columns["PGroup"].Width = 40;
            dataGridView1.Columns["Product"].Width = 105;
            dataGridView1.Columns["Weight"].Width = 55;
            dataGridView1.Columns["Pcs"].Width = 48;
            dataGridView1.Columns["Tunch1"].Width = 48;
            dataGridView1.Columns["Tunch2"].Width = 48;
            dataGridView1.Columns["Westage"].Width = 55;
            dataGridView1.Columns["LaboursRate"].Width = 60;
            dataGridView1.Columns["Fine"].Width = 60;
            dataGridView1.Columns["Amount"].Width = 65;

            this.dataGridView1.Columns["Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Pcs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Tunch1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Tunch2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Westage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["LaboursRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Fine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

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

        private void ClearControl()
        {
            lblTunchPending.Text = "";
            _Clear();
            CommanHelper.ComboBoxItem(cmbPopUp, "PartyTran", "Distinct(BillNo)", "TranType", "GR");
            JamaNaamList.Clear();
            TunchPendingList.Clear();
            grpPriceList.Visible = false;
            AutoCode("J", "GR");
            dtp1.Text = DateTime.Now.ToString();
            cmbCategory.SelectedIndex = -1;
            Cmbparty.SelectedIndex = -1;
            dataGridView1.DataSource = JamaNaamList.ToList();
            Row_No = -1;
            cmbGroup.SelectedIndex = -1;
            cmbGroup.Text = "";
            cmbproduct.Items.Clear();
            cmbproduct.Text = "";
            txtweight.Clear();
            txtpcs.Clear();
            txttunch1.Clear();
            txttunch2.Clear();
            txtwestage.Clear();
            txtlabourrs.Clear();
            txtfine.Clear();
            txtamount.Clear();
            txtdescription.Clear();
        }

        private void Total()
        {
            if (CommanHelper.SumRow1(dataGridView1, 9) > 0)
            {
                lblTotalAmount.Text = CommanHelper.SumRow1(dataGridView1, 9).ToString();
            }
            else
            {
                lblTotalAmount.Text = "";
            }
            if (CommanHelper.SumRow1(dataGridView1, 8) > 0)
            {
                lblTotalFine.Text = CommanHelper.SumRow1(dataGridView1, 8).ToString();
            }
            else
            {
                lblTotalFine.Text = "";
            }
            if (CommanHelper.SumRow1(dataGridView1, 2) > 0)
            {
                lblTotalWeight.Text = CommanHelper.SumRow1(dataGridView1, 2).ToString();
            }
            else
            {
                lblTotalWeight.Text = "";
            }
            if (CommanHelper.SumRow1(dataGridView1, 3) > 0)
            {
                lblTotalPcs.Text = CommanHelper.SumRow1(dataGridView1, 3).ToString();
            }
            else
            {
                lblTotalPcs.Text = "";
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
            cmbPartyName_PriseList.Text = "";
            cmbProduct_PriceList.Text = "";
            dtpFrom.Text = DateTime.Now.ToString();
            dtpTo.Text = DateTime.Now.ToString();
            dataGridView2.DataSource = "";
        }

        private void Remark_Tunch(String _BillNo)
        {
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                bool check_isvalid = false;
                bool check_T1valid = false;
                bool check_T2valid = false;
                bool check_tunch1 = false;
                bool check_tunch2 = false;

                int tunch_penslno = Convert.ToInt32(dataGridView1.Rows[i].Cells[11].Value.ToString());

                var result = TunchPendingList.Where(x => x.BillNo == _BillNo && x.TunchSno == tunch_penslno).SingleOrDefault();
                if (result.BillNo != null)
                {
                    check_isvalid = true;

                    if (result.Tunch1 != "")
                    {
                        if (result.Tunch1 == "N")
                        {
                            check_tunch1 = true;
                        }
                        else
                        {
                            check_tunch1 = false;
                        }
                        check_T1valid = true;
                    }
                    else
                    {
                        check_T1valid = false;
                    }

                    if (result.Tunch2 != "")
                    {
                        if (result.Tunch2 == "N")
                        {
                            check_tunch2 = true;
                        }
                        else
                        {
                            check_tunch2 = false;
                        }
                        check_T2valid = true;
                    }
                    else
                    {
                        check_T2valid = false;
                    }
                }

                if (check_isvalid == true)
                {
                    if ((check_tunch1 == true) && (check_tunch2 == true))
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = "U";
                    }

                    if ((check_tunch1 == false) && (check_tunch2 == true))
                    {
                        if (check_T1valid == true)
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "P1";
                        }
                        else
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "U";
                        }
                    }

                    if ((check_tunch1 == true) && (check_tunch2 == false))
                    {
                        if (check_T2valid == true)
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "P2";
                        }
                        else
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "U";
                        }
                    }
                    if ((check_tunch1 == false) && (check_tunch2 == false))
                    {
                        if ((check_T1valid == true) && (check_T2valid == true))
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "P1P2";
                        }
                        if ((check_T1valid == true) && (check_T2valid == false))
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "P1";
                        }
                        if ((check_T1valid == false) && (check_T2valid == true))
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "P2";
                        }
                        if ((check_T1valid == false) && (check_T2valid == false))
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = "";
                        }
                    }
                }
            }
        }

        private string Remark_Tunch1(string _BillNo, int _TunchSno)
        {
            var result = TunchPendingList.Where(x => x.BillNo == _BillNo && x.TunchSno == _TunchSno).SingleOrDefault();
            bool check_isvalid = false;
            bool check_T1valid = false;
            bool check_T2valid = false;
            bool check_tunch1 = false;
            bool check_tunch2 = false;
            string remark = "";
            if (result.BillNo != null)
            {
                check_isvalid = true;

                if (result.Tunch1 != "")
                {
                    if (result.Tunch1 == "N")
                    {
                        check_tunch1 = true;
                    }
                    else
                    {
                        check_tunch1 = false;
                    }
                    check_T1valid = true;
                }
                else
                {
                    check_T1valid = false;
                }

                if (result.Tunch2 != "")
                {
                    if (result.Tunch2 == "N")
                    {
                        check_tunch2 = true;
                    }
                    else
                    {
                        check_tunch2 = false;
                    }
                    check_T2valid = true;
                }
                else
                {
                    check_T2valid = false;
                }
            }

            if (check_isvalid == true)
            {
                if ((check_tunch1 == true) && (check_tunch2 == true))
                {
                    remark = "U";
                }

                if ((check_tunch1 == false) && (check_tunch2 == true))
                {
                    if (check_T1valid == true)
                    {
                        remark = "P1";
                    }
                    else
                    {
                        remark = "U";
                    }
                }

                if ((check_tunch1 == true) && (check_tunch2 == false))
                {
                    if (check_T2valid == true)
                    {
                        remark = "P2";
                    }
                    else
                    {
                        remark = "U";
                    }
                }
                if ((check_tunch1 == false) && (check_tunch2 == false))
                {
                    if ((check_T1valid == true) && (check_T2valid == true))
                    {
                        remark = "P1P2";
                    }
                    if ((check_T1valid == true) && (check_T2valid == false))
                    {
                        remark = "P1";
                    }
                    if ((check_T1valid == false) && (check_T2valid == true))
                    {
                        remark = "P2";
                    }
                    if ((check_T1valid == false) && (check_T2valid == false))
                    {
                        remark = "";
                    }
                }
            }
            return remark;
        }

        #endregion

        private void Jama_Load(object sender, EventArgs e)
        {
            lblTunchPending.Text = "";
            this.CancelButton = btnClose;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            SetCreditLimitGridView_ColumnWith();

            this.toolStripMenuItem_Save.Click += new EventHandler(btnSave_Click);
            this.toolStripMenuItem_Delete.Click += new EventHandler(btnDelete_Click);
            this.toolStripMenuItem_Refresh.Click += new EventHandler(btnRefresh_Click);
            this.toolStripMenuItem_Print.Click += new EventHandler(btnPrint_Click);

            con = new OleDbConnection();
            objCon = new ConnectionClass();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
            _Clear();
            oOpeningOtherEntity = CommanHelper.OpeningOther();
            cmbCategory.DataSource = oOpeningOtherEntity;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.SelectedIndex = -1;

            CommanHelper.ComboBoxItem(cmbGroup, "Product", "Distinct(PGroup)");
            CommanHelper.ComboBoxItem(cmbPopUp, "PartyTran", "Distinct(BillNo)", "TranType", "GR");
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
            {
                CommanHelper.GetProductCategory_GroupWise(cmbproduct, cmbCategory.Text.Trim(), cmbGroup.Text.Trim());
            }
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



        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {

                String _Category = "";
                String _PartyName = "";
                String _PartyCategory = "";
                _Category = cmbCategory.Text;
                _PartyName = Cmbparty.Text.Trim();
                _PartyCategory = CommanHelper.GetColumnValue("Category", "PartyDetails", "PartyName", Cmbparty.Text.Trim());
                if (CommanHelper.VarifiedValue("PartyDetails", "PartyName", "Type", "Party", Cmbparty.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid Party", "JAMA RECIEVING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cmbparty.Focus();
                    return;
                }


                if (CommanHelper.VarifiedValue("Product", "PGroup", cmbGroup.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter Valid Product Category", "JAMA RECIEVING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGroup.Focus();
                    return;
                }

                if (CommanHelper.VarifiedValue("Product", "ProductName", cmbproduct.Text.Trim()) == false)
                {
                    MessageBox.Show("Enter valid Product", "JAMA RECIEVING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbproduct.Focus();
                    return;
                }
                _Tunch_pending_YN = CommanHelper.GetColumnValue("RawDefine", "Product", "ProductName", cmbproduct.Text.Trim());

                if (_TunchSno == -1)
                {
                    if (TunchPendingList.Count() == 0)
                    {
                        _TunchSno = CommanHelper.Get_Tunch_Sl_No("GR");
                    }
                    else
                    {
                        _TunchSno = TunchPendingList.Max(x => x.TunchSno) + 1;
                        if (_TunchSno < CommanHelper.Get_Tunch_Sl_No("GR"))
                        {
                            _TunchSno = CommanHelper.Get_Tunch_Sl_No("GR");
                        }
                    }
                }
                if (txtbillno.Text == "")
                {
                    txtbillno.Text = 'J' + CommanHelper.Pro_AutoCode("PartyTran", "BillNo", "TranType", "GR");
                }


                if (Row_No != -1)
                {
                    //----Update JAMA Data
                    var result = (from r in JamaNaamList where r.Sno == Row_No select r).SingleOrDefault();
                    result.PGroup = cmbGroup.Text.Trim();
                    result.Product = cmbproduct.Text.Trim();
                    result.Weight = Conversion.ConToDec(txtweight.Text.Trim());
                    result.Pcs = Conversion.ConToDec(txtpcs.Text.Trim());
                    result.Tunch1 = Conversion.ConToDec(txttunch1.Text.Trim());
                    result.Tunch2 = Conversion.ConToDec(txttunch2.Text.Trim());
                    result.Westage = Conversion.ConToDec(txtwestage.Text.Trim());
                    result.LaboursRate = Conversion.ConToDec(txtlabourrs.Text.Trim());
                    result.Fine = Conversion.ConToDec(txtfine.Text.Trim());
                    result.Amount = Conversion.ConToDec6(txtamount.Text.Trim());
                    result.Narration = txtdescription.Text.Trim();
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
                            uTunchPending.Product = cmbproduct.Text.Trim();
                            uTunchPending.Weight = Conversion.ConToDec(txtweight.Text.Trim());
                            uTunchPending.TunchValue1 = Conversion.ConToDec(txttunch1.Text.Trim());
                            uTunchPending.TunchValue2 = Conversion.ConToDec(txttunch2.Text.Trim());
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Do You Want change Updated Tunch ?", "JAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            txttunch1.Text = _Tunch1LastValue;
                            txttunch2.Text = _Tunch2LastValue;
                        }
                    }
                }
                else
                {
                    var max = 0;
                    if (JamaNaamList.Count > 0)
                    {
                        max = JamaNaamList.Max(x => x.Sno) + 1;
                    }

                    JamaNaamEntity oJamaNaamEntity = new JamaNaamEntity();
                    oJamaNaamEntity.AddJamaNaam(cmbGroup.Text.Trim(), cmbproduct.Text.Trim(), Conversion.ConToDec(txtweight.Text.Trim()), Conversion.ConToDec(txtpcs.Text.Trim()), Conversion.ConToDec(txttunch1.Text.Trim()), Conversion.ConToDec(txttunch2.Text.Trim()), Conversion.ConToDec(txtwestage.Text.Trim()), Conversion.ConToDec(txtlabourrs.Text.Trim()), Conversion.ConToDec(txtfine.Text.Trim()), Conversion.ConToDec6(txtamount.Text.Trim()), txtdescription.Text.Trim(), _TunchSno, max);
                    JamaNaamList.Add(oJamaNaamEntity);

                    if (_Tunch_pending_YN == "Y")
                    {
                        TunchPendingEntity oTunchPendingEntity = new TunchPendingEntity();
                        oTunchPendingEntity.AddTunchPending(txtbillno.Text.Trim(), Conversion.GetDateStr(dtp1.Text.Trim()), _PartyCategory, _PartyName, _Category, cmbproduct.Text.Trim(), Conversion.ConToDec(txtweight.Text.Trim()), Conversion.ConToDec(txttunch1.Text.Trim()), Conversion.ConToDec(txttunch2.Text.Trim()), "Y", "", "GR", _TunchSno, CommanHelper.CompName.ToString(), CommanHelper.UserId.ToString());
                        TunchPendingList.Add(oTunchPendingEntity);
                    }
                }


                if (_Tunch_Update != "U")
                {
                    if (_Tunch_pending_YN == "Y")
                    {
                        if (cmbPopUp.Text == "")
                        {
                            if (_TunchSno != 0)
                            {
                                DialogResult result;
                                if ((_Tunch_Update == "P1") || (_Tunch_Update == ""))
                                {
                                    result = MessageBox.Show("Do You Want Tunch Pending 2", "JAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                }
                                else
                                {
                                    result = MessageBox.Show("Do You Want Tunch Pending 2", "JAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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
                        else
                        {
                            if (_TunchSno != 0)
                            {
                                DialogResult result;
                                if ((_Tunch_Update == "P1") || (_Tunch_Update == ""))
                                {
                                    result = MessageBox.Show("Do You Want Tunch Pending 2", "JAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                }
                                else
                                {
                                    result = MessageBox.Show("Do You Want Tunch Pending 2", "JAMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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

                dataGridView1.DataSource = JamaNaamList.ToList();
                Remark_Tunch(txtbillno.Text.Trim());
                Total();
                _TunchSno = -1;
                Row_No = -1;
                cmbproduct.SelectedIndex = -1;
                cmbproduct.Text = "";
                txtweight.Clear();
                txtpcs.Clear();
                txttunch1.Clear();
                txttunch2.Clear();
                txtwestage.Clear();
                txtlabourrs.Clear();
                txtfine.Clear();
                txtamount.Clear();
                txtdescription.Clear();
                cmbGroup.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.Text.Trim() == "")
                {
                    cmbCategory.Focus();
                    return;
                }

                if (Cmbparty.Text.Trim() == "")
                {
                    Cmbparty.Focus();
                    return;
                }
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Some Data is Missing", "JAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbproduct.Focus();
                    return;

                }

                _TunchSno = 0;
                String _Category = "";
                String _PartyName = "";
                String _PartyCategory = "";
                _Category = cmbCategory.Text;
                _PartyName = Cmbparty.Text.Trim();
                _PartyCategory = CommanHelper.GetColumnValue("Category", "PartyDetails", "PartyName", Cmbparty.Text.Trim());
                JamaNaamEntity oJamaNaamEntity = new JamaNaamEntity();
                PriceListEntity oPriceListEntity = new PriceListEntity();
                TunchPendingEntity oTunchPendingEntity = new TunchPendingEntity();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                Tran = con.BeginTransaction();

                OleDbCommand cmd = new OleDbCommand("Delete From PartyTran Where BillNo = '" + txtbillno.Text.Trim() + "'", con, Tran);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From TunchPending Where BillNo = '" + txtbillno.Text.Trim() + "'";
                cmd.ExecuteNonQuery();

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    String _PGroup = "";
                    String _Product = "";
                    Decimal _Weight = 0;
                    Decimal _Pcs = 0;
                    Decimal _Tunch1 = 0;
                    Decimal _Tunch2 = 0;
                    Decimal _Westage = 0;
                    Decimal _LabourFine = 0;
                    Decimal _Fine = 0;
                    Decimal _Amount = 0;
                    String _Narration = "";

                    _PGroup = dr.Cells[0].Value.ToString();
                    _Product = dr.Cells[1].Value.ToString();
                    _Weight = Conversion.ConToDec6(dr.Cells[2].Value.ToString());
                    _Pcs = Conversion.ConToDec6(dr.Cells[3].Value.ToString());
                    _Tunch1 = Conversion.ConToDec6(dr.Cells[4].Value.ToString());
                    _Tunch2 = Conversion.ConToDec6(dr.Cells[5].Value.ToString());
                    _Westage = Conversion.ConToDec6(dr.Cells[6].Value.ToString());
                    _LabourFine = Conversion.ConToDec6(dr.Cells[7].Value.ToString());
                    _Fine = Conversion.ConToDec6(dr.Cells[8].Value.ToString());
                    _Amount = Conversion.ConToDec6(dr.Cells[9].Value.ToString());
                    _Narration = dr.Cells[10].Value.ToString();
                    _TunchSno = Conversion.ConToInt(dr.Cells[11].Value.ToString());

                    oJamaNaamEntity.InsertJamaNaam(txtbillno.Text.Trim(), Conversion.ConToDT(dtp1.Text), _Category, _PartyCategory, _PartyName, _PGroup, _Product, _Weight, _Pcs, _Tunch1, _Tunch2, _Westage, _LabourFine, 0, _Fine, _Amount, _Narration, "GR", this.FindForm().Name, _TunchSno, CommanHelper.CompName.ToString(), CommanHelper.UserId.ToString(), con, Tran);
                    if (_Old_labour != _LabourFine || _Old_westage != _Westage)
                    {
                        oPriceListEntity.InsertPriceList(Conversion.ConToDT(dtp1.Text), _PartyCategory, _PartyName, _Category, _Product, _Westage, _LabourFine, "GR", CommanHelper.CompName.ToString(), CommanHelper.UserId.ToString(), con, Tran);
                    }
                }

                foreach (var item in TunchPendingList)
                {
                    oTunchPendingEntity.InsertTunchPending(item.BillNo, item.TrDate, item.PartyCate, item.PartyName, item.Category, item.Product, item.Weight, item.TunchValue1, item.TunchValue2, item.Tunch1, item.Tunch2, item.InvoiceType, item.TunchSno, item.Company, item.UserId, con, Tran);
                }

                Tran.Commit();
                con.Close();
                MessageBox.Show("Data Successfull Saved.....", "JAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                Tran.Rollback();
                con.Close();
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmbparty.Text.ToString() != "")
                {
                    if (MessageBox.Show("Do You Want To Delete Data", "JAMA", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Tran = null;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        Tran = con.BeginTransaction();
                        OleDbCommand cmd = new OleDbCommand("Delete From PartyTran Where BillNo = '" + txtbillno.Text.Trim() + "'", con, Tran);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "Delete From TunchPending Where BillNo = '" + txtbillno.Text.Trim() + "' AND InvoiceType = 'GR'";
                        cmd.ExecuteNonQuery();

                        Tran.Commit();
                        con.Close();
                        MessageBox.Show("Data SuccessFully Deleted", "JAMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearControl();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                Tran.Rollback();
                con.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
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

        private void cmbPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetDetails(cmbPopUp.Text.Trim());
                Total();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void GetDetails(String _BillNo)
        {
            if (_BillNo != "")
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand("Select BillNo,TrDate,MetalCategory,PartyName,PGroup, Product, Weight, Pcs, Tunch1, Tunch2, Westage, LaboursRate,Credit, Amount, Narration,TunchSno,Sno From PartyTran Where TranType = 'GR' And BillNo = '" + _BillNo + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                JamaNaamList.Clear();
                while (dr.Read())
                {
                    txtbillno.Text = dr["BillNo"].ToString();
                    dtp1.Text = dr["TrDate"].ToString();
                    cmbCategory.Text = dr["MetalCategory"].ToString();
                    Cmbparty.Text = dr["PartyName"].ToString();

                    JamaNaamEntity oJamaNaamEntity = new JamaNaamEntity();
                    oJamaNaamEntity.PGroup = dr["PGroup"].ToString();
                    oJamaNaamEntity.Product = dr["Product"].ToString();
                    oJamaNaamEntity.Weight = Conversion.ConToDec(dr["Weight"].ToString());
                    oJamaNaamEntity.Pcs = Conversion.ConToDec(dr["Pcs"].ToString());
                    oJamaNaamEntity.Tunch1 = Conversion.ConToDec(dr["Tunch1"].ToString());
                    oJamaNaamEntity.Tunch2 = Conversion.ConToDec(dr["Tunch2"].ToString());
                    oJamaNaamEntity.Westage = Conversion.ConToDec(dr["Westage"].ToString());
                    oJamaNaamEntity.LaboursRate = Conversion.ConToDec(dr["LaboursRate"].ToString());
                    oJamaNaamEntity.Fine = Conversion.ConToDec(dr["Credit"].ToString());
                    oJamaNaamEntity.Amount = Conversion.ConToDec(dr["Amount"].ToString());
                    oJamaNaamEntity.Narration = dr["Narration"].ToString();
                    oJamaNaamEntity.TunchSno = Conversion.ConToInt(dr["TunchSno"].ToString());
                    oJamaNaamEntity.Sno = Conversion.ConToInt(dr["Sno"].ToString());
                    JamaNaamList.Add(oJamaNaamEntity);
                }
                dr.Close();
                dataGridView1.DataSource = JamaNaamList.ToList();

                cmd.CommandText = "Select BillNo,TrDate,PartyCate,PartyName,Category,Product,Weight,TunchValue1,TunchValue2,Tunch1,Tunch2,InvoiceType,TunchSno,Company,UserId from TunchPending Where InvoiceType = 'GR' And BillNo = '" + _BillNo + "'";
                dr = cmd.ExecuteReader();
                TunchPendingList.Clear();
                while (dr.Read())
                {
                    TunchPendingEntity oTunchPendingEntity = new TunchPendingEntity();
                    oTunchPendingEntity.AddTunchPending(txtbillno.Text.Trim(), Conversion.GetDateStr(dtp1.Text.Trim()), dr["PartyCate"].ToString().Trim(), dr["PartyName"].ToString().Trim(), dr["Category"].ToString().Trim(), dr["Product"].ToString().Trim(), Conversion.ConToDec(dr["Weight"].ToString().Trim()), Conversion.ConToDec(dr["TunchValue1"].ToString().Trim()), Conversion.ConToDec(dr["TunchValue2"].ToString().Trim()), dr["Tunch1"].ToString().Trim(), dr["Tunch2"].ToString().Trim(), dr["InvoiceType"].ToString().Trim(), Conversion.ConToInt(dr["TunchSno"].ToString().Trim()), dr["Company"].ToString().Trim(), dr["UserId"].ToString().Trim());
                    TunchPendingList.Add(oTunchPendingEntity);
                }
                dr.Close();
                con.Close();

                Remark_Tunch(txtbillno.Text);
            }
        }



        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnOk_KeyPress(object sender, KeyPressEventArgs e)
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
                Cal_Amount();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                hti = dataGridView1.HitTest(e.X, e.Y);
                Row_No = hti.RowY;
                if (hti.RowIndex >= 0)
                {
                    _Tunch_Update = "";
                    cmbGroup.Text = dataGridView1.Rows[hti.RowIndex].Cells[0].Value.ToString();
                    cmbproduct.Text = dataGridView1.Rows[hti.RowIndex].Cells[1].Value.ToString();
                    txtweight.Text = dataGridView1.Rows[hti.RowIndex].Cells[2].Value.ToString();
                    txtpcs.Text = dataGridView1.Rows[hti.RowIndex].Cells[3].Value.ToString();
                    _Tunch1LastValue = txttunch1.Text = dataGridView1.Rows[hti.RowIndex].Cells[4].Value.ToString();
                    _Tunch2LastValue = txttunch2.Text = dataGridView1.Rows[hti.RowIndex].Cells[5].Value.ToString();
                    txtwestage.Text = dataGridView1.Rows[hti.RowIndex].Cells[6].Value.ToString();
                    txtlabourrs.Text = dataGridView1.Rows[hti.RowIndex].Cells[7].Value.ToString();
                    txtfine.Text = dataGridView1.Rows[hti.RowIndex].Cells[8].Value.ToString();
                    txtamount.Text = dataGridView1.Rows[hti.RowIndex].Cells[9].Value.ToString();
                    txtdescription.Text = dataGridView1.Rows[hti.RowIndex].Cells[10].Value.ToString();
                    _TunchSno = Convert.ToInt32(dataGridView1.CurrentRow.Cells[11].Value.ToString());
                    Row_No = Convert.ToInt32(dataGridView1.Rows[hti.RowIndex].Cells[12].Value.ToString());
                    _Tunch_Update = (dataGridView1.CurrentRow.HeaderCell.Value ?? (object)"").ToString();
                    dtp1.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    _Tunch_Update = "";
                    cmbGroup.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    cmbproduct.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtweight.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtpcs.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    _Tunch1LastValue = txttunch1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    _Tunch2LastValue = txttunch2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtwestage.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txtlabourrs.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtfine.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    txtamount.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    txtdescription.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                    _TunchSno = Convert.ToInt32(dataGridView1.CurrentRow.Cells[11].Value.ToString());
                    Row_No = Convert.ToInt32(dataGridView1.CurrentRow.Cells[12].Value.ToString());
                    _Tunch_Update = (dataGridView1.CurrentRow.HeaderCell.Value ?? (object)"").ToString();
                    dtp1.Focus();
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

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbCategory.BackColor = Color.White;
                panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
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

        private void Cmbparty_Leave(object sender, EventArgs e)
        {
            try
            {
                Cmbparty.BackColor = Color.White;
                panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
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

        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            try
            {
                Grpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
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

        private void cmbPopUp_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbPopUp.BackColor = Color.Aqua;
                pnlpopup.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPopUp_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbPopUp.BackColor = Color.White;
                pnlpopup.BackColor = Color.RosyBrown;
                pnlpopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPopUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    dataGridView1.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    int SNo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[12].Value.ToString());
                    int TunchSno = Convert.ToInt32(dataGridView1.CurrentRow.Cells[11].Value.ToString());
                    var result = (from r in JamaNaamList where r.Sno == SNo select r).SingleOrDefault();
                    if (result != null)
                        JamaNaamList.Remove(result);

                    var deleteTunch = (from x in TunchPendingList where x.TunchSno == TunchSno select x).SingleOrDefault();
                    if (deleteTunch != null)
                        TunchPendingList.Remove(deleteTunch);

                    dataGridView1.DataSource = JamaNaamList.ToList();
                    Total();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            try
            {
                listBox1.Visible = true;
                listBox1.Focus();
                listBox1.Items.Clear();

                new JamaNaamEntity().GetBillNo_ListBox(listBox1, Conversion.ConToDT(dateTimePicker1.Text), "GR", con);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {

                    ClearControl();
                    listBox1.Visible = true;
                    listBox1.Focus();
                    listBox1.Items.Clear();
                    new JamaNaamEntity().GetBillNo_ListBox(listBox1, Conversion.ConToDT(dateTimePicker1.Text), "GR", con);

                    listBox1.Focus();
                    if (listBox1.Items.Count > 0)
                    {
                        listBox1.SelectedIndex = 0;
                    }

                }
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
                    string _Billno = "";
                    _Billno = listBox1.SelectedItem.ToString().Substring(0, 5);
                    cmbPopUp.Text = _Billno;
                    GetDetails(_Billno);
                    Total();
                    dataGridView1.Focus();
                }
                listBox1.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string _Billno = "";

                _Billno = listBox1.SelectedItem.ToString().Substring(0, 5);
                cmbPopUp.Text = _Billno;
                GetDetails(_Billno);
                Total();

                listBox1.Visible = false;
                cmbproduct.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void toolStripMenuItem_PriceList_Click(object sender, EventArgs e)
        {
            try
            {
                PriceList_Clear();
                grpPriceList.Visible = true;
                CommanHelper.GetParty(cmbPartyName_PriseList, "PARTY");
                CommanHelper.ComboBoxItem(cmbProduct_PriceList, "Product", "ProductName");
                cmbPartyName_PriseList.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void toolStripMenuItem_PopUp_Click(object sender, EventArgs e)
        {
            try
            {
                cmbPopUp.Focus();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPartyName_PriseList_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbPartyName_PriseList.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPartyName_PriseList_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbPartyName_PriseList.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbPartyName_PriseList_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbPartyName_PriseList.Text.Trim() == "")
                    {
                        cmbPartyName_PriseList.Focus();
                        return;
                    }
                    else
                    {
                        cmbProduct_PriceList.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProduct_PriceList_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbProduct_PriceList.BackColor = Color.Aqua;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProduct_PriceList_Leave(object sender, EventArgs e)
        {
            try
            {
                cmbProduct_PriceList.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbProduct_PriceList_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbProduct_PriceList.Text.Trim() == "")
                    {
                        cmbProduct_PriceList.Focus();
                        return;
                    }
                    else
                    {
                        dtpFrom.Focus();
                    }
                }
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
                {
                    dtpTo.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnClose_PriceList_Click(object sender, EventArgs e)
        {
            try
            {
                grpPriceList.Visible = false;
                PriceList_Clear();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtpTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("Select format([TrDate],\"dd/MM/yyyy\") as [Date],Westage,LabourRs From PriceList Where PartyName='" + cmbPartyName_PriseList.Text + "' And Product='" + cmbProduct_PriceList.Text + "' And [TrDate]>= #" + Conversion.GetDateStr(dtpFrom.Text.Trim()) + "# And [TrDate]<= #" + Conversion.GetDateStr(dtpTo.Text.Trim()) + "#  and TranType='GR' Order by [TrDate] desc", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView2.DataSource = ds.Tables[0];
                    this.dataGridView2.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView2.Columns["Westage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dataGridView2.Columns["LabourRs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void lblTunchPending_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TunchPending oTunchPending = new TunchPending();
                oTunchPending.Show();

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
