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

namespace SilverGold
{
    public partial class PartyInformation : Form
    {
        #region Declare Variable

        OleDbConnection con;
        OleDbTransaction Tran = null;
        List<OpeningMCXEntity> OpeningMCXList = new List<OpeningMCXEntity>();
        List<OpeningOtherEntity> OpeningOtherList = new List<OpeningOtherEntity>();
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(CommanHelper.NumericCheck);
        DataGridViewColumn colLimit = new DataGridViewTextBoxColumn();

        CalendarColumn dtpDateFrom_CreditPeriod = new CalendarColumn();
        CalendarColumn dtpDateTo_CreditPeriod = new CalendarColumn();
        DataGridViewComboBoxColumn col_RateRevise_CreditPeriod = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_Matltype_CreditPeriod = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_Product_CreditPeriod = new DataGridViewComboBoxColumn();
        DataGridViewColumn col_Westage_CreditPeriod = new DataGridViewTextBoxColumn();
        DataGridViewColumn col_Amount_CreditPeriod = new DataGridViewTextBoxColumn();
        DataGridViewColumn col_Days_CreditPeriod = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn col_TranType_CreditPeriod = new DataGridViewComboBoxColumn();

        CalendarColumn dtpDateFrom_LabourRate = new CalendarColumn();
        CalendarColumn dtpDateTo_LabourRate = new CalendarColumn();
        DataGridViewComboBoxColumn col_WtPcs_LabourRate = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_Cate_LabourRate = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_Product_LabourRate = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_FineAmnt_LabourRate = new DataGridViewComboBoxColumn();
        DataGridViewColumn col_LRate_LabourRate = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn col_PType_LabourRate = new DataGridViewComboBoxColumn();

        CalendarColumn dtpDateFrom_GhattakList = new CalendarColumn();
        CalendarColumn dtpDateTo_GhattakList = new CalendarColumn();
        DataGridViewComboBoxColumn col_WtPcs_GhattakList = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_Cate_GhattakList = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_Product_GhattakList = new DataGridViewComboBoxColumn();        
        DataGridViewColumn col_Ghattk_GhattakList = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn col_PType_GhattakList = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_JN_GhattakList = new DataGridViewComboBoxColumn();

        #endregion
        public PartyInformation()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate2(dataGridView1);
            CommanHelper.ChangeGridFormate2(dataGridView2);
            CommanHelper.ChangeGridFormate(dataGridViewCreditPeriod);
            CommanHelper.ChangeGridFormate(dataGridView_LabourRate);
            CommanHelper.ChangeGridFormate(dataGridView_GhattakList);
            BindCreditLimitOpeningColumn();
            BindCreditPeriod();
        }

        #region Helper

        private void BindCreditLimitOpeningColumn()
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dataGridView2.Columns.Add(col1);

            colLimit.DataPropertyName = "Limit";
            colLimit.HeaderText = "Limit";
            colLimit.Name = "Limit";
            dataGridView2.Columns.Add(colLimit);
        }

        private void BindCreditPeriod()
        {
            dtpDateFrom_CreditPeriod.DataPropertyName = "DateFrom";
            dtpDateFrom_CreditPeriod.HeaderText = "DateFrom";
            dtpDateFrom_CreditPeriod.Name = "DateFrom";
            dataGridViewCreditPeriod.Columns.Add(dtpDateFrom_CreditPeriod);

            dtpDateTo_CreditPeriod.DataPropertyName = "DateTo";
            dtpDateTo_CreditPeriod.HeaderText = "DateTo";
            dtpDateTo_CreditPeriod.Name = "DateTo";
            dataGridViewCreditPeriod.Columns.Add(dtpDateTo_CreditPeriod);

            col_RateRevise_CreditPeriod.DataPropertyName = "RateRevised";
            col_RateRevise_CreditPeriod.HeaderText = "Rate Revised";
            col_RateRevise_CreditPeriod.Name = "RateRevised";
            col_RateRevise_CreditPeriod.Items.Add("AMOUNT");
            col_RateRevise_CreditPeriod.Items.Add("WESTAGE");
            col_RateRevise_CreditPeriod.Items.Add("BOTH");
            col_RateRevise_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dataGridViewCreditPeriod.Columns.Add(col_RateRevise_CreditPeriod);

            col_Matltype_CreditPeriod.DataPropertyName = "Category";
            col_Matltype_CreditPeriod.HeaderText = "Category";
            col_Matltype_CreditPeriod.Name = "Category";
            col_Matltype_CreditPeriod.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
            col_Matltype_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dataGridViewCreditPeriod.Columns.Add(col_Matltype_CreditPeriod);

            col_Product_CreditPeriod.DataPropertyName = "Product";
            col_Product_CreditPeriod.HeaderText = "Product";
            col_Product_CreditPeriod.Name = "Product";
            col_Product_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dataGridViewCreditPeriod.Columns.Add(col_Product_CreditPeriod);

            col_Westage_CreditPeriod.DataPropertyName = "Westage";
            col_Westage_CreditPeriod.HeaderText = "Westage";
            col_Westage_CreditPeriod.Name = "Westage";
            dataGridViewCreditPeriod.Columns.Add(col_Westage_CreditPeriod);

            col_Amount_CreditPeriod.DataPropertyName = "AmountWeight";
            col_Amount_CreditPeriod.HeaderText = "Amount";
            col_Amount_CreditPeriod.Name = "AmountWeight";
            dataGridViewCreditPeriod.Columns.Add(col_Amount_CreditPeriod);

            col_TranType_CreditPeriod.DataPropertyName = "Tran_Type";
            col_TranType_CreditPeriod.HeaderText = "TranType";
            col_TranType_CreditPeriod.Name = "Tran_Type";
            col_TranType_CreditPeriod.Items.Add("JAMA");
            col_TranType_CreditPeriod.Items.Add("NAAM");
            col_TranType_CreditPeriod.Items.Add("BOTH");
            col_TranType_CreditPeriod.FlatStyle = FlatStyle.Popup;
            dataGridViewCreditPeriod.Columns.Add(col_TranType_CreditPeriod);

            col_Days_CreditPeriod.DataPropertyName = "Days";
            col_Days_CreditPeriod.HeaderText = "Days";
            col_Days_CreditPeriod.Name = "Days";
            dataGridViewCreditPeriod.Columns.Add(col_Days_CreditPeriod);
        }

        private void BindLabourRate()
        {
            dtpDateFrom_LabourRate.DataPropertyName = "DateFrom";
            dtpDateFrom_LabourRate.HeaderText = "DateFrom";
            dtpDateFrom_LabourRate.Name = "DateFrom";
            dataGridView_LabourRate.Columns.Add(dtpDateFrom_LabourRate);

            dtpDateTo_LabourRate.DataPropertyName = "DateTo";
            dtpDateTo_LabourRate.HeaderText = "DateTo";
            dtpDateTo_LabourRate.Name = "DateTo";
            dataGridView_LabourRate.Columns.Add(dtpDateTo_LabourRate);

            col_WtPcs_LabourRate.DataPropertyName = "WeightPcs";
            col_WtPcs_LabourRate.HeaderText = "WT/PCS";
            col_WtPcs_LabourRate.Name = "WeightPcs";
            col_WtPcs_LabourRate.Items.Add("WEIGHT");
            col_WtPcs_LabourRate.Items.Add("PCS");
            col_WtPcs_LabourRate.FlatStyle = FlatStyle.Popup;
            dataGridView_LabourRate.Columns.Add(col_WtPcs_LabourRate);

            col_Cate_LabourRate.DataPropertyName = "Category";
            col_Cate_LabourRate.HeaderText = "Category";
            col_Cate_LabourRate.Name = "Category";
            col_Cate_LabourRate.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
            col_Cate_LabourRate.FlatStyle = FlatStyle.Popup;
            dataGridView_LabourRate.Columns.Add(col_Cate_LabourRate);

            col_Product_LabourRate.DataPropertyName = "Product";
            col_Product_LabourRate.HeaderText = "Product";
            col_Product_LabourRate.Name = "Product";
            col_Product_LabourRate.FlatStyle = FlatStyle.Popup;
            dataGridView_LabourRate.Columns.Add(col_Product_LabourRate);

            col_FineAmnt_LabourRate.DataPropertyName = "Fine_Amount";
            col_FineAmnt_LabourRate.HeaderText = "FINE/AMT";
            col_FineAmnt_LabourRate.Name = "Fine_Amount";
            col_FineAmnt_LabourRate.FlatStyle = FlatStyle.Popup;
            dataGridView_LabourRate.Columns.Add(col_FineAmnt_LabourRate);

            col_LRate_LabourRate.DataPropertyName = "LabourRate";
            col_LRate_LabourRate.HeaderText = "LRate";
            col_LRate_LabourRate.Name = "LabourRate";
            dataGridView_LabourRate.Columns.Add(col_LRate_LabourRate);

            col_PType_LabourRate.DataPropertyName = "PayRate";
            col_PType_LabourRate.HeaderText = "Pay Type";
            col_PType_LabourRate.Name = "PayRate";
            col_PType_LabourRate.Items.Add("GIVING");
            col_PType_LabourRate.Items.Add("RECIEVING");
            col_PType_LabourRate.Items.Add("COMMON");
            col_PType_LabourRate.FlatStyle = FlatStyle.Popup;
            dataGridView_LabourRate.Columns.Add(col_PType_LabourRate);
        }

        private void BindGhattakList()
        {
            dtpDateFrom_GhattakList.DataPropertyName = "DateFrom";
            dtpDateFrom_GhattakList.HeaderText = "DateFrom";
            dtpDateFrom_GhattakList.Name = "DateFrom";
            dataGridView_GhattakList.Columns.Add(dtpDateFrom_GhattakList);

            dtpDateTo_GhattakList.DataPropertyName = "DateTo";
            dtpDateTo_GhattakList.HeaderText = "DateTo";
            dtpDateTo_GhattakList.Name = "DateTo";
            dataGridView_GhattakList.Columns.Add(dtpDateTo_GhattakList);

            col_WtPcs_GhattakList.DataPropertyName = "WeightPcs";
            col_WtPcs_GhattakList.HeaderText = "WT/PCS";
            col_WtPcs_GhattakList.Name = "WeightPcs";
            col_WtPcs_GhattakList.Items.Add("WEIGHT");
            col_WtPcs_GhattakList.Items.Add("PCS");
            col_WtPcs_GhattakList.FlatStyle = FlatStyle.Popup;
            dataGridView_GhattakList.Columns.Add(col_WtPcs_GhattakList);

            col_Cate_GhattakList.DataPropertyName = "Category";
            col_Cate_GhattakList.HeaderText = "Category";
            col_Cate_GhattakList.Name = "Category";
            col_Cate_GhattakList.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
            col_Cate_GhattakList.FlatStyle = FlatStyle.Popup;
            dataGridView_GhattakList.Columns.Add(col_Cate_GhattakList);

            col_Product_GhattakList.DataPropertyName = "Product";
            col_Product_GhattakList.HeaderText = "Product";
            col_Product_GhattakList.Name = "Product";
            col_Product_GhattakList.FlatStyle = FlatStyle.Popup;
            dataGridView_GhattakList.Columns.Add(col_Product_GhattakList);

            col_Ghattk_GhattakList.DataPropertyName = "Ghattak";
            col_Ghattk_GhattakList.HeaderText = "Ghattak";
            col_Ghattk_GhattakList.Name = "Ghattak";
            dataGridView_GhattakList.Columns.Add(col_Ghattk_GhattakList);

            col_PType_GhattakList.DataPropertyName = "PayType";
            col_PType_GhattakList.HeaderText = "PayType";
            col_PType_GhattakList.Name = "PayType";
            col_PType_GhattakList.Items.Add("GIVING");
            col_PType_GhattakList.Items.Add("RECIEVING");
            col_PType_GhattakList.Items.Add("COMMON");
            col_PType_GhattakList.FlatStyle = FlatStyle.Popup;
            dataGridView_GhattakList.Columns.Add(col_PType_GhattakList);

            col_JN_GhattakList.DataPropertyName = "Jama_Naam";
            col_JN_GhattakList.HeaderText = "J/N";
            col_JN_GhattakList.Name = "Jama_Naam";
            col_JN_GhattakList.Items.Add("JAMA");
            col_JN_GhattakList.Items.Add("NAAM");
            col_JN_GhattakList.FlatStyle = FlatStyle.Popup;
            dataGridView_GhattakList.Columns.Add(col_JN_GhattakList);
        }


        private void SetCreditLimitGridView_ColumnWith()
        {
            dataGridViewCreditPeriod.Columns["DateFrom"].Width = 80;
            dataGridViewCreditPeriod.Columns["DateTo"].Width = 80;
            dataGridViewCreditPeriod.Columns["RateRevised"].Width = 90;
            dataGridViewCreditPeriod.Columns["Category"].Width = 100;
            dataGridViewCreditPeriod.Columns["Product"].Width = 130;
            dataGridViewCreditPeriod.Columns["Westage"].Width = 60;
            dataGridViewCreditPeriod.Columns["AmountWeight"].Width = 60;
            dataGridViewCreditPeriod.Columns["Tran_Type"].Width = 80;
            dataGridViewCreditPeriod.Columns["Days"].Width = 50;
        }


        private void SetLabourRateGridView_ColumnWith()
        {
            dataGridView_LabourRate.Columns["WeightPcs"].Width = 50;
            dataGridView_LabourRate.Columns["Category"].Width = 50;
            dataGridView_LabourRate.Columns["Product"].Width = 100;
        }

        private void SetGhattakListGridView_ColumnWith()
        {
            dataGridView_GhattakList.Columns["WeightPcs"].Width = 60;
            dataGridView_GhattakList.Columns["Category"].Width = 70;
            dataGridView_GhattakList.Columns["Product"].Width = 130;
        }

        private void BindOpeningMCXColumn()
        {
            dataGridView1.Columns.Clear();
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Weight";
            col2.Name = "Weight";
            dataGridView1.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "Closing";
            col3.HeaderText = "Closing";
            col3.Name = "Closing";
            dataGridView1.Columns.Add(col3);

            DataGridViewComboBoxColumn col4 = new DataGridViewComboBoxColumn();
            col4.DataPropertyName = "DrCr";
            col4.HeaderText = "DrCr";
            col4.Name = "DrCr";
            col4.Items.Add("SELL");
            col4.Items.Add("PURCHASE");
            col4.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col4);
        }

        private void BindOpeningOtherColumn()
        {
            dataGridView1.Columns.Clear();
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Amount";
            col2.HeaderText = "Amount";
            col2.Name = "Amount";
            dataGridView1.Columns.Add(col2);

            DataGridViewComboBoxColumn col3 = new DataGridViewComboBoxColumn();
            col3.DataPropertyName = "DrCr";
            col3.HeaderText = "DrCr";
            col3.Name = "DrCr";
            col3.Items.Add("DEBIT");
            col3.Items.Add("CREDIT");
            col3.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col3);
        }

        private void ClearControl()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            BindOpeningOtherColumn();

            cmbPopUp.SelectedIndex = -1;
            CommanHelper.BindPartyName(cmbPopUp);
            cmbtype.SelectedIndex = -1;
            cmbtype.Text = "";
            cmbCategory.SelectedIndex = -1;
            cmbCategory.Text = "";
            txtpartyname.Clear();
            cmbBullion.SelectedIndex = -1;
            txtaddress.Clear();
            txtemailid.Clear();
            txtcontactno.Clear();
            cmbgrouphead.SelectedIndex = -1;
            cmbsubhead.SelectedIndex = -1;
            cmbIntroducer.SelectedIndex = -1;
            cmbShowtrail.SelectedIndex = -1;
            chkWithCreditLimit.Checked = false;
            cmbLot.SelectedIndex = -1;
            txtoprs.Clear();
            cmbrs.SelectedIndex = -1;
            cmb_gen_type.SelectedIndex = -1;

            dataGridViewCreditPeriod.DataSource = null;
            dataGridViewCreditPeriod.Rows.Clear();

            dataGridView_LabourRate.DataSource = null;
            dataGridView_LabourRate.Rows.Clear();
            groupBox_LabourRate.Visible = false;
            dataGridView_LabourRate.Visible = false;

            dataGridView_GhattakList.DataSource = null;
            dataGridView_GhattakList.Rows.Clear();
            dataGridView_GhattakList.Visible = false;
            groupBox_GhattakList.Visible = false;
        }

        private void GetPartyDetails(String strPartyName)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand("Select Type,Category,PartyName,PartyType,Address,Email,ContactNo,GroupHead,SubGroup,IntroducerName,ShowInTrail,WithCreditPeriod,Lot,LotGenerate From PartyDetails Where PartyName = '" + strPartyName + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmbtype.SelectedIndex = -1;
                cmbCategory.SelectedIndex = -1;
                txtpartyname.Clear();
                cmbBullion.SelectedIndex = -1;
                txtaddress.Clear();
                txtemailid.Clear();
                txtcontactno.Clear();
                cmbgrouphead.SelectedIndex = -1;
                cmbsubhead.SelectedIndex = -1;
                cmbIntroducer.SelectedIndex = -1;
                cmbShowtrail.SelectedIndex = -1;
                cmbLot.SelectedIndex = -1;
                cmb_gen_type.SelectedIndex = -1;
                chkWithCreditLimit.Checked = false;
                if (dr.Read())
                {
                    cmbtype.Text = dr["Type"].ToString();
                    cmbCategory.Text = dr["Category"].ToString();
                    txtpartyname.Text = dr["PartyName"].ToString();
                    cmbBullion.Text = dr["PartyType"].ToString();
                    txtaddress.Text = dr["Address"].ToString();
                    txtemailid.Text = dr["Email"].ToString();
                    txtcontactno.Text = dr["ContactNo"].ToString();
                    cmbgrouphead.Text = dr["GroupHead"].ToString();
                    cmbsubhead.Text = dr["SubGroup"].ToString();
                    cmbIntroducer.Text = dr["IntroducerName"].ToString();
                    cmbShowtrail.Text = dr["ShowInTrail"].ToString();
                    if (dr["WithCreditPeriod"].ToString().Trim().ToUpper() == "YES")
                    {
                        chkWithCreditLimit.Checked = true;
                    }
                    cmbLot.Text = dr["Lot"].ToString();
                    cmb_gen_type.Text = dr["LotGenerate"].ToString();

                }
                dr.Close();

                cmd.CommandText = "Select Amount_Weight,DrCr from PartyOpening Where PartyName = '" + strPartyName + "' AND ItemName = 'CASH'";
                dr = cmd.ExecuteReader();
                txtoprs.Clear();
                cmbrs.SelectedIndex = -1;
                if (dr.Read())
                {
                    txtoprs.Text = dr["Amount_Weight"].ToString();
                    cmbrs.Text = dr["DrCr"].ToString();
                }
                dr.Close();

                if (chkWithCreditLimit.Checked == true)
                {
                    cmd.CommandText = "Select CreditPeriod,RateUpdate From CreditLimit Where PartyName = '" + strPartyName + "'";
                    dr = cmd.ExecuteReader();
                    cmbDays.SelectedIndex = -1;
                    if (dr.Read())
                    {
                        cmbDays.Text = dr["CreditPeriod"].ToString();
                        if (dr["RateUpdate"].ToString().Trim().ToUpper() == "NO")
                        {
                            rateupdate_radio_N.Checked = true;
                        }
                        else
                        {
                            rateupdate_radio.Checked = true;
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        #endregion



        private void PartyInformation_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnexit;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            this.toolStripMenu_Save.Click += new EventHandler(btnsave_Click);
            this.toolStripMenu_Delete.Click += new EventHandler(btndelete_Click);
            this.toolStripMenu_Refersh.Click += new EventHandler(btnrefresh_Click);
            this.toolStripMenu_Report.Click += new EventHandler(btnReport_Click);

            con = new OleDbConnection();
            BindOpeningOtherColumn();

            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
            CommanHelper.BindPartyName(cmbPopUp);
            CommanHelper.BindPartyName(cmbIntroducer);


            CommanHelper.FillCreditLimitOpening(dataGridView2);
            CommanHelper.BindPartyCategory(cmbCategory);
            for (int i = 0; i <= 365; i++)
            {
                cmbDays.Items.Add(i);
            }

            CommanHelper.ComboBoxItem(cmbgrouphead, "GroupHead", "Distinct(GroupHead)");
            SetCreditLimitGridView_ColumnWith();
            BindLabourRate();
            BindGhattakList();
            SetLabourRateGridView_ColumnWith();
            SetGhattakListGridView_ColumnWith();
            cmbtype.Focus();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select Party/Worker", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbtype.Focus();
                    return;
                }
                if (cmbShowtrail.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select From Show In Trial", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbShowtrail.Focus();
                    return;
                }
                if (txtpartyname.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter The PartyName", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpartyname.Focus();
                    return;
                }
                if (txtoprs.Text != "" && cmbrs.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select Credit/Debit", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbrs.Focus();
                    return;
                }
                if ((cmbtype.Text.ToString() == "WORKER") && (cmbLot.Text.ToString() == "YES"))
                {
                    if (cmb_gen_type.Text.Trim() == "")
                    {
                        MessageBox.Show("Plz Select Lot Genrate Type!!!", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmb_gen_type.Focus();
                        return;

                    }
                }

                if (cmbPopUp.Text.Trim() == "")
                {
                    if (CommanHelper.AlreadyExistParty(txtpartyname.Text.Trim()) == true)
                    {
                        MessageBox.Show("Party Already Exist.", "Party Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtpartyname.Focus();
                        return;
                    }
                }
                String _PartyName = "";
                String strWithCreditLimit = "";
                String rate_revised = "";
                if (chkWithCreditLimit.Checked == true) { strWithCreditLimit = "YES"; }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                if (cmbPopUp.Text.Trim() == "")
                {
                    _PartyName = txtpartyname.Text.Trim();
                }
                else
                {
                    _PartyName = cmbPopUp.Text.Trim();
                }

                con.Open();
                Tran = con.BeginTransaction();
                OleDbCommand cmd = new OleDbCommand("", con, Tran);

                cmd.CommandText = "Delete From PartyDetails Where PartyName = '" + _PartyName + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete From PartyOpening Where PartyName = '" + _PartyName + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete From CreditLimit Where PartyName = '" + _PartyName + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete From PartyTran Where PartyName = '" + _PartyName + "' And TranType = 'O'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete From CreditPeriod Where PartyName = '" + _PartyName + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete From LaboursRate Where PartyName = '" + _PartyName + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete From GhattakList Where PartyName = '" + _PartyName + "'";
                cmd.ExecuteNonQuery();


                #region Insert PartyDetails

                cmd.CommandText = "INSERT INTO PartyDetails(Type,Category,PartyName,PartyType,Address,Email,ContactNo,GroupHead,SubGroup,IntroducerName,ShowInTrail,WithCreditPeriod,Lot,LotGenerate,Company,UserId)VALUES" +
                    "(@Type,@Category,@PartyName,@PartyType,@Address,@Email,@ContactNo,@GroupHead,@SubGroup,@IntroducerName,@ShowInTrail,@WithCreditPeriod,@Lot,@LotGenerate,@Company,@UserId)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Type", cmbtype.Text.Trim());
                cmd.Parameters.AddWithValue("@Category", cmbCategory.Text.Trim());
                cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                cmd.Parameters.AddWithValue("@PartyType", cmbBullion.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtaddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtemailid.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNo", txtcontactno.Text.Trim());
                cmd.Parameters.AddWithValue("@GroupHead", cmbgrouphead.Text.Trim());
                cmd.Parameters.AddWithValue("@SubGroup", cmbsubhead.Text.Trim());
                cmd.Parameters.AddWithValue("@IntroducerName", cmbIntroducer.Text.Trim());
                cmd.Parameters.AddWithValue("@ShowInTrail", cmbShowtrail.Text.Trim());
                cmd.Parameters.AddWithValue("@WithCreditPeriod", strWithCreditLimit);
                cmd.Parameters.AddWithValue("@Lot", cmbLot.Text.Trim());
                cmd.Parameters.AddWithValue("@LotGenerate", cmb_gen_type.Text.Trim());
                cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                cmd.ExecuteNonQuery();

                #endregion

                #region Insert Cash Opening

                ///---------------Insert Cash Opening
                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO PartyOpening(PartyName,ItemName,Amount_Weight,DrCr,Company,UserId)VALUES(@PartyName,@ItemName,@Amount_Weight,@DrCr,@Company,@UserId)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemName", "CASH");
                cmd.Parameters.AddWithValue("@ClosingRate", Conversion.ConToDec6(txtoprs.Text.ToString().Trim()));
                cmd.Parameters.AddWithValue("@DrCr", cmbrs.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                cmd.ExecuteNonQuery();

                #endregion


                if (cmbtype.Text.Trim().ToUpper() == "PARTY")
                {
                    #region Insert Credit Limit

                    //--------Insert Credit Limit
                    if (chkWithCreditLimit.Checked == true)
                    {
                        if (rateupdate_radio.Checked == true)
                        {
                            rate_revised = "YES";
                        }
                        if (rateupdate_radio_N.Checked == true)
                        {
                            rate_revised = "NO";
                        }
                        foreach (DataGridViewRow dr in dataGridView2.Rows)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO CreditLimit(PartyName,CreditPeriod,RateUpdate,ItemName,ItemLimit,Company,UserId)VALUES(@PartyName,@CreditPeriod,@RateUpdate,@ItemName,@ItemLimit,@Company,@UserId)";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                            cmd.Parameters.AddWithValue("@CreditPeriod", cmbDays.Text.Trim());
                            cmd.Parameters.AddWithValue("@RateUpdate", rate_revised.Trim());
                            cmd.Parameters.AddWithValue("@ItemName", dr.Cells[0].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@ItemLimit", Conversion.ConToDec6((dr.Cells[1].Value ?? (object)"").ToString().Trim()));
                            cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                            cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                            cmd.ExecuteNonQuery();

                        }

                        foreach (DataGridViewRow dr in dataGridViewCreditPeriod.Rows)
                        {
                            DateTime _DateFrom = DateTime.Now;
                            DateTime _DateTo = DateTime.Now;
                            String _RateRevised = "";
                            String _Category = "";
                            String _Product = "";
                            Decimal _Westage = 0;
                            Decimal _Amount = 0;
                            String _Tran_Type = "";
                            Int32 _Days = 0;

                            _DateFrom = Conversion.ConToDT((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                            _DateTo = Conversion.ConToDT((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                            _RateRevised = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                            _Category = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                            _Product = (dr.Cells[4].Value ?? (object)"").ToString().Trim();
                            _Westage = Conversion.ConToDec6((dr.Cells[5].Value ?? (object)"").ToString().Trim());
                            _Amount = Conversion.ConToDec6((dr.Cells[6].Value ?? (object)"").ToString().Trim());
                            _Tran_Type = (dr.Cells[7].Value ?? (object)"").ToString().Trim();
                            _Days = Conversion.ConToInt((dr.Cells[8].Value ?? (object)"").ToString().Trim());

                            if (_RateRevised != "" && _Product != "" && _Westage != 0 && _Amount != 0 && _Tran_Type != "" && _Days != 0)
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandText = "INSERT INTO CreditPeriod(PartyName,DateFrom,DateTo,RateRevised,Category,Product,Westage,Amount,Tran_Type,Days,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@RateRevised,@Category,@Product,@Westage,@Amount,@Tran_Type,@Days,@Company,@UserId)";
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                                cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                                cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                                cmd.Parameters.AddWithValue("@RateRevised", _RateRevised);
                                cmd.Parameters.AddWithValue("@Category", _Category);
                                cmd.Parameters.AddWithValue("@Product", _Product);
                                cmd.Parameters.AddWithValue("@Westage", _Westage);
                                cmd.Parameters.AddWithValue("@Amount", _Amount);
                                cmd.Parameters.AddWithValue("@Tran_Type", _Tran_Type);
                                cmd.Parameters.AddWithValue("@Days", _Days);
                                cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                                cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Insert Labour Rate

                    foreach (DataGridViewRow dr in dataGridView_LabourRate.Rows)
                    {
                        DateTime _DateFrom = DateTime.Now;
                        DateTime _DateTo = DateTime.Now;
                        String _WeightPcs = "";
                        String _Category = "";
                        String _Product = "";
                        String _Fine_Amount = "";
                        Decimal _LaboursRate = 0;
                        String _PayType = "";

                        _DateFrom = Conversion.ConToDT((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                        _DateTo = Conversion.ConToDT((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                        _WeightPcs = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                        _Category = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                        _Product = (dr.Cells[4].Value ?? (object)"").ToString().Trim();
                        _Fine_Amount = (dr.Cells[5].Value ?? (object)"").ToString().Trim();
                        _LaboursRate = Conversion.ConToDec6((dr.Cells[6].Value ?? (object)"").ToString().Trim());
                        _PayType = (dr.Cells[7].Value ?? (object)"").ToString().Trim();

                        if (_WeightPcs != "" && _Fine_Amount != "" && _LaboursRate != 0 && _PayType != "")
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO LaboursRate(PartyName,DateFrom,DateTo,WeightPcs,Category,Product,Fine_Amount,LaboursRate,PayType,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@WeightPcs,@Category,@Product,@Fine_Amount,@LaboursRate,@PayType,@Company,@UserId)";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                            cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                            cmd.Parameters.AddWithValue("@WeightPcs", _WeightPcs);
                            cmd.Parameters.AddWithValue("@Category", _Category);
                            cmd.Parameters.AddWithValue("@Product", _Product);
                            cmd.Parameters.AddWithValue("@Fine_Amount", _Fine_Amount);
                            cmd.Parameters.AddWithValue("@LaboursRate", _LaboursRate);
                            cmd.Parameters.AddWithValue("@PayType", _PayType);
                            cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                            cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    #endregion

                    #region Insert Ghattak List

                    foreach (DataGridViewRow dr in dataGridView_GhattakList.Rows)
                    {
                        DateTime _DateFrom = DateTime.Now;
                        DateTime _DateTo = DateTime.Now;
                        String _WeightPcs = "";
                        String _Category = "";
                        String _Product = "";
                        Decimal _Ghattak = 0;
                        String _Jama_Naam = "";
                        String _PayType = "";

                        _DateFrom = Conversion.ConToDT((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                        _DateTo = Conversion.ConToDT((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                        _WeightPcs = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                        _Category = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                        _Product = (dr.Cells[4].Value ?? (object)"").ToString().Trim();
                        _Ghattak =  Conversion.ConToDec6((dr.Cells[5].Value ?? (object)"").ToString().Trim());
                        _Jama_Naam = (dr.Cells[6].Value ?? (object)"").ToString().Trim();
                        _PayType = (dr.Cells[7].Value ?? (object)"").ToString().Trim();

                        if (_WeightPcs != "" && _Ghattak != 0 && _Jama_Naam != "" && _PayType != "")
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO GhattakList(PartyName,DateFrom,DateTo,WeightPcs,Category,Product,Ghattak,PayType,Jama_Naam,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@WeightPcs,@Category,@Product,@Ghattak,@PayType,@Jama_Naam,@Company,@UserId)";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                            cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                            cmd.Parameters.AddWithValue("@WeightPcs", _WeightPcs);
                            cmd.Parameters.AddWithValue("@Category", _Category);
                            cmd.Parameters.AddWithValue("@Product", _Product);
                            cmd.Parameters.AddWithValue("@Ghattak", _Ghattak);
                            cmd.Parameters.AddWithValue("@PayType", _Jama_Naam);
                            cmd.Parameters.AddWithValue("@Jama_Naam", _PayType);
                            cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                            cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }

                    #endregion


                }


                #region Insert Party Opening

                //----------Insert Party Opening

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {

                    if (cmbBullion.Text.Trim().ToUpper() == "MCX" || cmbtype.Text.Trim().ToUpper() == "WORKER")
                    {
                        Decimal _Sell = 0;
                        Decimal _Purchase = 0;
                        Decimal _Weight = 0;
                        String _MetalCategory = "";
                        Decimal _MCXRate = 0;

                        _MetalCategory = dr.Cells[0].Value.ToString().Trim();
                        _MCXRate = Conversion.ConToDec6(dr.Cells[2].Value.ToString().Trim());

                        if ((dr.Cells[3].Value ?? (object)"").ToString().Trim() == "SELL")
                        {
                            _Sell = Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim());
                            _Weight = -Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim());
                        }
                        if ((dr.Cells[3].Value ?? (object)"").ToString().Trim() == "PURCHASE")
                        {
                            _Purchase = Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim());
                            _Weight = Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim());
                        }

                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO PartyOpening(PartyName,ItemName,Amount_Weight,ClosingRate,DrCr,Company,UserId)VALUES(@PartyName,@ItemName,@Amount_Weight,@ClosingRate,@DrCr,@Company,@UserId)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItemName", dr.Cells[0].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Amount_Weight", Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim()));
                        cmd.Parameters.AddWithValue("@ClosingRate", Conversion.ConToDec6(dr.Cells[2].Value.ToString().Trim()));
                        cmd.Parameters.AddWithValue("@DrCr", (dr.Cells[3].Value ?? (object)"").ToString().Trim());
                        cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                        cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                        cmd.ExecuteNonQuery();

                        //-----------Insert Opening In PartyTran
                        if ((dr.Cells[3].Value ?? (object)"").ToString().Trim() != "")
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO PartyTran(TrDate,Category,PartyName,MetalCategory,MetalName,Debit,Credit,Weight,MCXRate,TranType,ContCode,Narration,Company,UserId)VALUES('" + DateTime.Now.ToString("MM/dd/yyy") + "','" + cmbCategory.Text.Trim() + "','" + txtpartyname.Text.Trim() + "','" + _MetalCategory + "','" + _MetalCategory + "','" + _Sell + "','" + _Purchase + "','" + _Weight + "','" + _MCXRate + "','O','" + CommanHelper.CompName.ToString() + "','PARTY OPENING','" + CommanHelper.CompName.ToString() + "','" + CommanHelper.UserId.ToString() + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Decimal _Debit = 0;
                        Decimal _Credit = 0;

                        if ((dr.Cells[2].Value ?? (object)"").ToString().Trim() == "DEBIT")
                        {
                            _Debit = Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim());
                        }
                        if ((dr.Cells[2].Value ?? (object)"").ToString().Trim() == "CREDIT")
                        {
                            _Credit = Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim());
                        }

                        cmd.Parameters.Clear();
                        cmd.CommandText = "INSERT INTO PartyOpening(PartyName,ItemName,Amount_Weight,DrCr,Company,UserId)VALUES(@PartyName,@ItemName,@Amount_Weight,@DrCr,@Company,@UserId)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PartyName", txtpartyname.Text.Trim());
                        cmd.Parameters.AddWithValue("@ItemName", dr.Cells[0].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@ClosingRate", Conversion.ConToDec6(dr.Cells[1].Value.ToString().Trim()));
                        cmd.Parameters.AddWithValue("@DrCr", (dr.Cells[2].Value ?? (object)"").ToString().Trim());
                        cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
                        cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
                        cmd.ExecuteNonQuery();

                        //-----------Insert Opening In PartyTran
                        if ((dr.Cells[2].Value ?? (object)"").ToString().Trim() != "")
                        {
                            cmd.CommandText = "INSERT INTO PartyTran(TrDate,Category,PartyName,MetalCategory,MetalName,Debit,Credit,TranType,ContCode,Narration,Company,UserId)VALUES('" + DateTime.Now.ToString("MM/dd/yyy") + "','" + cmbCategory.Text.Trim() + "','" + txtpartyname.Text.Trim() + "','" + dr.Cells[0].Value.ToString().Trim() + "','" + dr.Cells[0].Value.ToString().Trim() + "','" + _Debit + "','" + _Credit + "','O','" + CommanHelper.CompName.ToString() + "','PARTY OPENING','" + CommanHelper.CompName.ToString() + "','" + CommanHelper.UserId.ToString() + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                #endregion

                Tran.Commit();
                con.Close();

                MessageBox.Show("Data SuccessFully Updated", "Party Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                if (cmbtype.Text.Trim() == "PARTY")
                {
                    BindOpeningOtherColumn();
                    OpeningOtherList = CommanHelper.OpeningOther();
                    dataGridView1.DataSource = OpeningOtherList.ToList();
                    cmbBullion.Visible = true;
                    grpBoxWithCreditLimit.Visible = true;
                    cmbLot.SelectedIndex = -1;
                    cmbLot.Enabled = false;
                    cmbLot.Visible = false;
                    lblLot.Visible = false;
                    lblLotGenerateIn.Visible = false;
                    Panel_LotGenerate.Visible = false;
                }
                else
                {
                    BindOpeningMCXColumn();
                    OpeningOtherList = CommanHelper.OpeningOther();
                    dataGridView1.DataSource = CommanHelper.BindMCXDefaultOpening().ToList();
                    chkWithCreditLimit.Checked = false;
                    cmbDays.Text = "";
                    cmbBullion.Visible = false;
                    cmbShowtrail.Text = "YES";
                    grpBoxWithCreditLimit.Visible = false;
                    groBoxCreditPeriod.Visible = false;
                    lblLot.Visible = true;
                    cmbLot.Enabled = true;
                    cmbLot.Visible = true;
                    //lblLotGenerateIn.Visible = true;
                    //Panel_LotGenerate.Visible = true;
                    cmbgrouphead.Text = "LABOUR JOB";
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbBullion_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            if (cmbBullion.Text.Trim().ToUpper() == "MCX")
            {
                BindOpeningMCXColumn();
                OpeningMCXList = CommanHelper.BindMCXDefaultOpening();
                dataGridView1.DataSource = OpeningMCXList.ToList();
            }
            else
            {
                BindOpeningOtherColumn();
                OpeningOtherList = CommanHelper.OpeningOther();
                dataGridView1.DataSource = OpeningOtherList.ToList();
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == colLimit.Index)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
        }

        private void chkWithCreditLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithCreditLimit.Checked == true)
            {
                groBoxCreditPeriod.Visible = true;
                dataGridView2.Visible = true;
            }
            else
            {
                groBoxCreditPeriod.Visible = false;
                dataGridView2.Visible = false;
            }
        }

        private void cmbDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbDays.Text != "0") && (cmbDays.Text != ""))
            {
                label19.Visible = true;
                rateupdate_radio.Visible = true;
                rateupdate_radio_N.Visible = true;

            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbgrouphead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbgrouphead.Text.Trim() != "")
            {
                cmbsubhead.Text = "";
                CommanHelper.ComboBoxItem(cmbsubhead, "GroupHead", "Distinct(SubGroup)", "GroupHead", cmbgrouphead.Text);
            }
        }

        private void cmbtype_Enter(object sender, EventArgs e)
        {
            cmbtype.BackColor = Color.Aqua;
        }

        private void cmbtype_Leave(object sender, EventArgs e)
        {
            cmbtype.BackColor = Color.White;
        }


        private void cmbLot_Enter(object sender, EventArgs e)
        {
            plnlot.BackColor = Color.Red;
        }

        private void cmbLot_Leave(object sender, EventArgs e)
        {
            plnlot.BackColor = Color.Transparent;
        }

        private void cmb_gen_type_Enter(object sender, EventArgs e)
        {
            Panel_LotGenerate.BackColor = Color.Red;
        }

        private void cmb_gen_type_Leave(object sender, EventArgs e)
        {
            Panel_LotGenerate.BackColor = Color.Transparent;
        }

        private void cmbDays_Enter(object sender, EventArgs e)
        {
            cmbDays.BackColor = Color.Cyan;
        }

        private void cmbDays_Leave(object sender, EventArgs e)
        {
            cmbDays.BackColor = Color.White;
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            Panel_Category.BackColor = Color.Red;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            Panel_Category.BackColor = Color.Transparent;
        }

        private void txtpartyname_Enter(object sender, EventArgs e)
        {
            txtpartyname.BackColor = Color.Cyan;
        }
        private void txtpartyname_Leave(object sender, EventArgs e)
        {
            txtpartyname.BackColor = Color.White;
        }
        private void cmbBullion_Enter(object sender, EventArgs e)
        {
            Panel_McxBullion.BackColor = Color.Red;
        }

        private void cmbBullion_Leave(object sender, EventArgs e)
        {
            Panel_McxBullion.BackColor = Color.Transparent;
        }
        private void txtaddress_Enter(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.Cyan;
        }

        private void txtaddress_Leave(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.White;
        }

        private void txtcontactno_Enter(object sender, EventArgs e)
        {
            txtcontactno.BackColor = Color.Cyan;
        }

        private void txtcontactno_Leave(object sender, EventArgs e)
        {
            txtcontactno.BackColor = Color.White;
        }

        private void txtemailid_Enter(object sender, EventArgs e)
        {
            txtemailid.BackColor = Color.Cyan;
        }

        private void txtemailid_Leave(object sender, EventArgs e)
        {
            txtemailid.BackColor = Color.White;
        }

        private void cmbgrouphead_Enter(object sender, EventArgs e)
        {
            cmbgrouphead.BackColor = Color.Cyan;
        }

        private void cmbgrouphead_Leave(object sender, EventArgs e)
        {
            cmbgrouphead.BackColor = Color.White;
        }

        private void txtoprs_Enter(object sender, EventArgs e)
        {
            txtoprs.BackColor = Color.Cyan;
        }

        private void txtoprs_Leave(object sender, EventArgs e)
        {
            txtoprs.BackColor = Color.White;
        }

        private void cmbrs_Enter(object sender, EventArgs e)
        {
            plncmbrs.BackColor = Color.Red;
        }

        private void cmbrs_Leave(object sender, EventArgs e)
        {
            plncmbrs.BackColor = Color.Transparent;
        }

        private void cmbsubhead_Enter(object sender, EventArgs e)
        {
            cmbsubhead.BackColor = Color.Cyan;
        }

        private void cmbsubhead_Leave(object sender, EventArgs e)
        {
            cmbsubhead.BackColor = Color.White;
        }

        private void cmbIntroducer_Enter(object sender, EventArgs e)
        {
            cmbIntroducer.BackColor = Color.Cyan;
        }

        private void cmbIntroducer_Leave(object sender, EventArgs e)
        {
            cmbIntroducer.BackColor = Color.White;
        }

        private void txtopnarr_Enter(object sender, EventArgs e)
        {
            txtopnarr.BackColor = Color.Cyan;
        }

        private void txtopnarr_Leave(object sender, EventArgs e)
        {
            txtopnarr.BackColor = Color.White;
        }

        private void txtbanklimit_Enter(object sender, EventArgs e)
        {
            txtbanklimit.BackColor = Color.Cyan;
        }

        private void txtbanklimit_Leave(object sender, EventArgs e)
        {
            txtbanklimit.BackColor = Color.White;
        }

        private void cmbtype_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                cmbShowtrail.Focus();
            }
        }

        private void cmbLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if ((cmbLot.Text.Trim().ToUpper() == "YES") && (cmb_gen_type.Enabled == true))
                    {

                        cmb_gen_type.Focus();
                        if (cmbPopUp.Text.Trim() == "")
                        {
                            cmb_gen_type.SelectedIndex = -1;
                        }

                    }
                    else
                    {
                        txtpartyname.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmb_gen_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmb_gen_type.Text.Trim() != "")
                    {
                        cmbCategory.Focus();
                    }
                    else
                    {
                        cmb_gen_type.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtpartyname.Focus();
            }
        }

        private void txtpartyname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbPopUp.Text.Trim() == "")
                    {
                        if (CommanHelper.AlreadyExistParty(txtpartyname.Text.Trim()) == true)
                        {
                            MessageBox.Show("Party Already Exist.", "Party Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtpartyname.Focus();
                            return;
                        }
                    }
                    if (cmbPopUp.Text != "")
                    {
                        if (cmbtype.Text.Trim() == "WORKER")
                        {
                            dataGridView_LabourRate.Focus();
                            groupBox_LabourRate.Visible = true;
                        }
                        else
                        {
                            cmbBullion.Focus();
                        }
                    }
                    else
                    {
                        if (cmbtype.Text.Trim() == "WORKER")
                        {
                            groupBox_LabourRate.Visible = true;
                            dataGridView_LabourRate.Visible = true;
                            dataGridView_LabourRate.Focus();
                        }
                        else
                        {
                            cmbBullion.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbBullion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbBullion.Text.Trim().ToUpper() == "MCX")
                    {
                        //mcxbrokbox.Visible = true;
                        //c1TrueDBGrid5.Focus();
                    }
                    else
                    {
                        txtaddress.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtcontactno.Focus();
            }
        }

        private void txtcontactno_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)(e.KeyChar) >= 48 && (int)(e.KeyChar) <= 57 || (int)(e.KeyChar) == 8 || (int)(e.KeyChar) == 13)
                {
                    e.Handled = false;
                    if (e.KeyChar == 13)
                    {
                        txtemailid.Focus();
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtemailid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbgrouphead.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrouphead_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbsubhead.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbsubhead_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbIntroducer.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbIntroducer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbIntroducer.Text != "")
                    {
                        //if (ds_Introducer1.Tables[0].Rows.Count != 0)
                        //{
                        //    Panel1.Visible = true;
                        //}
                        //else
                        //{
                        //    Panel1.Visible = true;
                        //    c1TrueDBGrid4.Visible = true;
                        //    ds_Introducer1.Clear();
                        //    oleDbDataAdapter8.SelectCommand.CommandText = "select * from introducer where client_code='" + txtpartyname.Text.Trim() + "'";
                        //    oleDbDataAdapter8.Fill(ds_Introducer1);
                        //}
                        //c1TrueDBGrid4.Focus();
                        //c1TrueDBGrid4.Row = 0;
                        //c1TrueDBGrid4.Col = 0;
                    }
                    else
                    {

                        txtoprs.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtoprs_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (((int)(e.KeyChar) >= 48 && (int)(e.KeyChar) <= 57) || (int)(e.KeyChar) == 8 || (int)(e.KeyChar) == 13)
                {
                    e.Handled = false;
                    if (e.KeyChar == 13)
                    {
                        cmbrs.Focus();
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbrs_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbtype.Text.Trim().ToUpper() == "PARTY")
                    {
                        chkWithCreditLimit.Focus();
                    }
                    else
                    {
                        btnsave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void chkWithCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (chkWithCreditLimit.Checked == true)
                    {
                        cmbDays.Focus();
                    }
                    else
                    {
                        btnsave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (rateupdate_radio.Checked == true)
                    {
                        rateupdate_radio.Focus();
                    }
                    else if (rateupdate_radio_N.Checked == true)
                    {
                        rateupdate_radio_N.Focus();
                    }
                    else
                    {
                        rateupdate_radio.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cmbPopUp_Enter(object sender, EventArgs e)
        {
            cmbPopUp.BackColor = Color.Cyan;
        }

        private void cmbPopUp_Leave(object sender, EventArgs e)
        {
            cmbPopUp.BackColor = Color.White;
        }

        private void cmbPopUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbtype.Focus();
                }
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
                if (cmbPopUp.Text.Trim() != "")
                {
                    GetPartyDetails(cmbPopUp.Text.Trim());

                    if (cmbBullion.Text.Trim().ToUpper() == "MCX" || cmbtype.Text.Trim().ToUpper() == "WORKER")
                    {
                        dataGridView1.DataSource = null;
                        BindOpeningMCXColumn();
                        dataGridView1.DataSource = CommanHelper.GetPartyOpeningMCX(cmbPopUp.Text.Trim());
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = CommanHelper.GetPartyOpening(cmbPopUp.Text.Trim());
                    }
                    if (chkWithCreditLimit.Checked == true)
                    {
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = CommanHelper.GetCreditLimit(cmbPopUp.Text.Trim());
                    }
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand("Select Format(DateFrom,'DD/MM/YYYY') AS DateFrom,Format(DateTo,'DD/MM/YYYY') AS DateTo,RateRevised,Category,Product,Round(Westage,3) AS Westage,Round(Amount,3) AS Amount,Tran_Type,Days From CreditPeriod Where PartyName = '" + cmbPopUp.Text.Trim() + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    int _Sno = 0;
                    dataGridViewCreditPeriod.Rows.Clear();
                    while (dr.Read())
                    {
                        dataGridViewCreditPeriod.Rows.Add();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[0].Value = dr["DateFrom"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[1].Value = dr["DateTo"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[2].Value = dr["RateRevised"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[3].Value = dr["Category"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[4].Value = dr["Product"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[5].Value = dr["Westage"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[6].Value = dr["Amount"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[7].Value = dr["Tran_Type"].ToString();
                        dataGridViewCreditPeriod.Rows[_Sno].Cells[8].Value = dr["Days"].ToString();

                        _Sno++;
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbLot_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLot.Text.Trim().ToUpper() == "NO")
                {
                    lblLotGenerateIn.Visible = false;
                    cmb_gen_type.Visible = false;
                    Panel_LotGenerate.Visible = false;
                }
                else
                {
                    lblLotGenerateIn.Visible = true;
                    cmb_gen_type.Visible = true;
                    Panel_LotGenerate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridViewCreditPeriod_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void cmbShowtrail_Enter(object sender, EventArgs e)
        {
            panel_ShowInTrail.BackColor = Color.Red;
        }

        private void cmbShowtrail_Leave(object sender, EventArgs e)
        {
            panel_ShowInTrail.BackColor = Color.Transparent;
        }

        private void cmbShowtrail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbtype.Text == "WORKER")
                    {
                        cmbLot.Focus();
                    }
                    else
                    {
                        txtpartyname.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        private void dataGridViewCreditPeriod_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                dataGridViewCreditPeriod.Rows[e.RowIndex].Cells["DateFrom"].Value = Conversion.ConToDT(CommanHelper.FDate);
                dataGridViewCreditPeriod.Rows[e.RowIndex].Cells["DateTo"].Value = Conversion.ConToDT(CommanHelper.TDate);
            }
            catch (Exception ex)
            {
                // ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridViewCreditPeriod_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dataGridViewCreditPeriod.CurrentCellAddress.X == col_Matltype_CreditPeriod.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridViewCreditPeriod.CurrentRow.Cells[4];
                    if (e.FormattedValue.ToString() != "")
                    {
                        CommanHelper.GetProductCategoryWise(cmbProduct, e.FormattedValue.ToString());
                    }
                    else
                    {
                        CommanHelper.GetProduct(cmbProduct);
                    }

                }
                if (e.ColumnIndex == 8)
                {
                    if (e.FormattedValue.ToString() == "")
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridViewCreditPeriod_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridViewCreditPeriod.CurrentCell.ColumnIndex == col_Westage_CreditPeriod.Index ||
                    dataGridViewCreditPeriod.CurrentCell.ColumnIndex == col_Amount_CreditPeriod.Index ||
                    dataGridViewCreditPeriod.CurrentCell.ColumnIndex == col_Days_CreditPeriod.Index)
                {
                    e.Control.KeyPress -= NumericCheckHandler;
                    e.Control.KeyPress += NumericCheckHandler;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void rateupdate_radio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rateupdate_radio.Checked == true)
            {
                if (e.KeyChar == 13)
                {
                    dataGridViewCreditPeriod.Focus();
                }
            }
        }

        private void toolStripMenu_PopUp_Click(object sender, EventArgs e)
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

        private void rateupdate_radio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rateupdate_radio.Checked == true)
                {
                    rateupdate_radio_N.Checked = false;
                    dataGridViewCreditPeriod.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void rateupdate_radio_N_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rateupdate_radio_N.Checked == true)
                {
                    rateupdate_radio.Checked = false;
                    dataGridViewCreditPeriod.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_LabourRate_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView_LabourRate_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dataGridView_LabourRate.CurrentCellAddress.X == col_WtPcs_LabourRate.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbFineAmt = (DataGridViewComboBoxCell)dataGridView_LabourRate.CurrentRow.Cells[5];
                    cmbFineAmt.Items.Clear();
                    if (e.FormattedValue.ToString().Trim().ToUpper() == "PCS")
                    {
                        cmbFineAmt.Items.Add("FINE");
                        dataGridView_LabourRate.CurrentRow.Cells[5].Value = "FINE";
                    }
                    else
                    {
                        cmbFineAmt.Items.Add("AMOUNT");
                        cmbFineAmt.Items.Add("FINE");
                        dataGridView_LabourRate.CurrentRow.Cells[5].Value = "AMOUNT";
                    }
                }
                if (dataGridView_LabourRate.CurrentCellAddress.X == col_Cate_LabourRate.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_LabourRate.CurrentRow.Cells[4];
                    if (e.FormattedValue.ToString() != "")
                    {
                        CommanHelper.GetProductCategoryWise(cmbProduct, e.FormattedValue.ToString());
                    }
                    else
                    {
                        CommanHelper.GetProduct(cmbProduct);
                   }
                }
                if (e.ColumnIndex == 7)
                {
                    if (e.FormattedValue.ToString() == "")
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_LabourRate_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView_LabourRate.CurrentCell.ColumnIndex == col_LRate_LabourRate.Index)
                {
                    e.Control.KeyPress -= NumericCheckHandler;
                    e.Control.KeyPress += NumericCheckHandler;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_GhattakList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView_GhattakList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView_GhattakList.CurrentCell.ColumnIndex == col_Ghattk_GhattakList.Index)
                {
                    e.Control.KeyPress -= NumericCheckHandler;
                    e.Control.KeyPress += NumericCheckHandler;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_GhattakList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dataGridView_GhattakList.CurrentCellAddress.X == col_Cate_GhattakList.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_GhattakList.CurrentRow.Cells[4];
                    if (e.FormattedValue.ToString() != "")
                    {
                        CommanHelper.GetProductCategoryWise(cmbProduct, e.FormattedValue.ToString());
                    }
                    else
                    {
                        CommanHelper.GetProduct(cmbProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_LabourRate_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((dataGridView_LabourRate.Rows[e.RowIndex].Cells[2].Value ?? (object)"").ToString() == "" && (dataGridView_LabourRate.Rows[e.RowIndex].Cells[3].Value ?? (object)"").ToString() == "")
                {
                    groupBox_GhattakList.Visible = true;
                    dataGridView_GhattakList.Visible = true;
                    this.dataGridView_GhattakList.CurrentCell = this.dataGridView_GhattakList[0, 0];
                    dataGridView_GhattakList.Focus();
                }
                else
                {
                    groupBox_GhattakList.Visible = false;
                    dataGridView_GhattakList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_GhattakList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((dataGridView_GhattakList.Rows[e.RowIndex].Cells[2].Value ?? (object)"").ToString() == "" && (dataGridView_GhattakList.Rows[e.RowIndex].Cells[3].Value ?? (object)"").ToString() == "")
                {
                    txtaddress.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
