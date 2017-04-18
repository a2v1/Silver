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
            col_Amount.DataPropertyName = "LaboursAmount";
            col_Amount.HeaderText = "Amount";
            col_Amount.Name = "LaboursAmount";
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
            dataGridView1.Columns["LaboursAmount"].Width = 65;

            this.dataGridView1.Columns["Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Pcs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Tunch1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Tunch2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Westage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["LaboursRate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Fine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["LaboursAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            Weight =0;
            Tunch1 = 0;
            Tunch2 = 0;
            Westage = 0;
            mTunch = 0;
            Weight = Conversion.ConToDec6(txtweight.Text) ;
            Tunch1 = Conversion.ConToDec6(txttunch1.Text) ;
            Tunch2 = Conversion.ConToDec6(txttunch2.Text) ;
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

            oOpeningOtherEntity = CommanHelper.OpeningOther();
            cmbCategory.DataSource = oOpeningOtherEntity;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.SelectedIndex = -1;

            CommanHelper.ComboBoxItem(cmbGroup, "Product", "Distinct(PGroup)");
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
                if (Row_No != 0)
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
                    result.LaboursAmount = Conversion.ConToDec6(txtamount.Text.Trim());
                    result.Narration = txtdescription.Text.Trim();
                }
                else
                {
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
                    oJamaNaamEntity.LaboursAmount = Conversion.ConToDec6(txtamount.Text.Trim());
                    oJamaNaamEntity.Narration = txtdescription.Text.Trim();
                    JamaNaamList.Add(oJamaNaamEntity);
                }
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
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
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

        private void cmbPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
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
                 txtweight.Focus();
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
                    txtweight.Text = dataGridView1.Rows[hti.RowIndex].Cells[3].Value.ToString();
                    txtpcs.Text = dataGridView1.Rows[hti.RowIndex].Cells[4].Value.ToString();
                    txttunch1.Text = dataGridView1.Rows[hti.RowIndex].Cells[5].Value.ToString();
                    txttunch2.Text = dataGridView1.Rows[hti.RowIndex].Cells[6].Value.ToString();
                    txtwestage.Text = dataGridView1.Rows[hti.RowIndex].Cells[7].Value.ToString();
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
    }
}
