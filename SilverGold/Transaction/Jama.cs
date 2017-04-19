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
    public partial class Jama : Form
    {
        #region Declare Variables
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        List<JamaNaamEntity> JamaNaamList = new List<JamaNaamEntity>();
        List<OpeningOtherEntity> oOpeningOtherEntity = new List<OpeningOtherEntity>();
        DataGridView.HitTestInfo hti;
        int Row_No = -1;
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
            dataGridView1.Columns["Product"].Width = 110;
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
            _Clear();
            CommanHelper.ComboBoxItem(cmbPopUp, "PartyTran", "Distinct(BillNo)", "TranType", "GR");
            JamaNaamList.Clear();
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

        #endregion

        private void Jama_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            SetCreditLimitGridView_ColumnWith();

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
                CommanHelper.GetProductCategory_GroupWise(cmbproduct, cmbCategory.Text.Trim(), cmbGroup.Text.Trim());
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
                    txttunch1.Text = Math.Round(Conversion.ConToDec(CommanHelper.GetProductValue("Tunch", cmbproduct.Text.Trim())), 2).ToString();
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

                if (Row_No != -1)
                {
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
                }
                else
                {
                    if (cmbPopUp.Text.Trim() == "")
                    {
                        if (txtbillno.Text == "")
                        {
                            txtbillno.Text = 'J' + CommanHelper.Pro_AutoCode("PartyTran", "BillNo", "TranType", "GR");
                        }
                    }
                    var max = 0;
                    if (JamaNaamList.Count > 0)
                    {
                        max = JamaNaamList.Max(x => x.Sno) + 1;
                    }

                    JamaNaamEntity oJamaNaamEntity = new JamaNaamEntity();
                    oJamaNaamEntity.PGroup = cmbGroup.Text.Trim();
                    oJamaNaamEntity.Product = cmbproduct.Text.Trim();
                    oJamaNaamEntity.Weight = Conversion.ConToDec(txtweight.Text.Trim());
                    oJamaNaamEntity.Pcs = Conversion.ConToDec(txtpcs.Text.Trim());
                    oJamaNaamEntity.Tunch1 = Conversion.ConToDec(txttunch1.Text.Trim());
                    oJamaNaamEntity.Tunch2 = Conversion.ConToDec(txttunch2.Text.Trim());
                    oJamaNaamEntity.Westage = Conversion.ConToDec(txtwestage.Text.Trim());
                    oJamaNaamEntity.LaboursRate = Conversion.ConToDec(txtlabourrs.Text.Trim());
                    oJamaNaamEntity.Fine = Conversion.ConToDec(txtfine.Text.Trim());
                    oJamaNaamEntity.Amount = Conversion.ConToDec6(txtamount.Text.Trim());
                    oJamaNaamEntity.Narration = txtdescription.Text.Trim();
                    oJamaNaamEntity.Sno= max;
                    JamaNaamList.Add(oJamaNaamEntity);
                }
                dataGridView1.DataSource = JamaNaamList.ToList();

                Total();
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
                String _Category = "";
                String _PartyName = "";

                _Category = cmbCategory.Text;
                _PartyName = Cmbparty.Text.Trim();
                JamaNaamEntity oJamaNaamEntity = new JamaNaamEntity();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                Tran = con.BeginTransaction();

                OleDbCommand cmd = new OleDbCommand("Delete From PartyTran Where BillNo = '" + txtbillno.Text.Trim() + "'", con, Tran);
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


                    oJamaNaamEntity.InsertJamaNaam(txtbillno.Text.Trim(), Conversion.ConToDT(dtp1.Text), _Category, _PartyName, _PGroup, _Product, _Weight, _Pcs, _Tunch1, _Tunch2, _Westage, _LabourFine, 0, _Fine, _Amount, _Narration, "GR", this.FindForm().Name, CommanHelper.CompName.ToString(), CommanHelper.UserId.ToString(), con, Tran);

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
                OleDbCommand cmd = new OleDbCommand("Select BillNo,TrDate,MetalCategory,PartyName,PGroup, Product, Weight, Pcs, Tunch1, Tunch2, Westage, LaboursRate,Credit, Amount, Narration,Sno From PartyTran Where TranType = 'GR' And BillNo = '" + _BillNo + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();

                JamaNaamList.Clear();
               
                while(dr.Read())
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
                    oJamaNaamEntity.Amount = Conversion.ConToDec6(dr["Amount"].ToString());
                    oJamaNaamEntity.Narration = dr["Narration"].ToString();
                    oJamaNaamEntity.Sno = Conversion.ConToInt(dr["Sno"].ToString()); 
                    JamaNaamList.Add(oJamaNaamEntity);
                }
                dataGridView1.DataSource = JamaNaamList.ToList();
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
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '£')
                {
                }
                else
                {
                    e.Handled = e.KeyChar != (char)Keys.Back;
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
                    cmbGroup.Text = dataGridView1.Rows[hti.RowIndex].Cells[0].Value.ToString();
                    cmbproduct.Text = dataGridView1.Rows[hti.RowIndex].Cells[1].Value.ToString();
                    txtweight.Text = dataGridView1.Rows[hti.RowIndex].Cells[2].Value.ToString();
                    txtpcs.Text = dataGridView1.Rows[hti.RowIndex].Cells[3].Value.ToString();
                    txttunch1.Text = dataGridView1.Rows[hti.RowIndex].Cells[4].Value.ToString();
                    txttunch2.Text = dataGridView1.Rows[hti.RowIndex].Cells[5].Value.ToString();
                    txtwestage.Text = dataGridView1.Rows[hti.RowIndex].Cells[6].Value.ToString();
                    txtlabourrs.Text = dataGridView1.Rows[hti.RowIndex].Cells[7].Value.ToString();
                    txtfine.Text = dataGridView1.Rows[hti.RowIndex].Cells[8].Value.ToString();
                    txtamount.Text = dataGridView1.Rows[hti.RowIndex].Cells[9].Value.ToString();
                    txtdescription.Text = dataGridView1.Rows[hti.RowIndex].Cells[10].Value.ToString();
                    Row_No = Convert.ToInt32(dataGridView1.Rows[hti.RowIndex].Cells[11].Value.ToString());
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
                    cmbGroup.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    cmbproduct.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtweight.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtpcs.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    txttunch1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txttunch2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtwestage.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txtlabourrs.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtfine.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    txtamount.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    txtdescription.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                    Row_No = Convert.ToInt32(dataGridView1.CurrentRow.Cells[11].Value.ToString());

                    cmbGroup.Focus();
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
                    int SNo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[11].Value.ToString());
                    var result = (from r in JamaNaamList where r.Sno == SNo select r).SingleOrDefault();
                    if (result != null)
                        JamaNaamList.Remove(result);

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
    }
}
