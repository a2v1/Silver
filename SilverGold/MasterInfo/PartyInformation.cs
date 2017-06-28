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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SilverGold.MasterInfo
{
    public partial class PartyInformation : Form
    {
        #region Declare Variable

        OleDbConnection con;
        OleDbTransaction Tran = null;
        List<OpeningMCXEntity> OpeningMCXList = new List<OpeningMCXEntity>();
        List<OpeningOtherEntity> OpeningOtherList = new List<OpeningOtherEntity>();
        GhattakListEntity oGhattakListEntity = new GhattakListEntity();
        CreditPeriodEntity oCreditPeriodEntity = new CreditPeriodEntity();
        LaboursRateEntity oLaboursRateEntity = new LaboursRateEntity();
        CommissionListEntity oCommissionListEntity = new CommissionListEntity();
        BrokerageSettingEntity oBrokerageSettingEntity = new BrokerageSettingEntity();
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(CommanHelper.NumericCheck);
        DataGridViewColumn colLimit = new DataGridViewTextBoxColumn();
        CalendarColumn dtpOpeningDate = new CalendarColumn();
        CalendarColumn dtpOpening = new CalendarColumn();
        DataGridViewComboBoxColumn col_ProductItem = new DataGridViewComboBoxColumn();

        List<ProductEntity> ProductList = new List<ProductEntity>();
        public static String _Category = "";
        public static int _ProductFlage = 0;
        public static String _ProductName = "";
        int _ProductCreate_GridFlage = 0;

        DateTime _From = Conversion.ConToDT(CommanHelper.FDate);
        DateTime _To = Conversion.ConToDT(CommanHelper.TDate);

        #endregion

        public PartyInformation()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            CommanHelper.ChangeGridFormate2(dataGridView2);
            CommanHelper.ChangeGridFormate(dataGridViewCreditPeriod);
            CommanHelper.ChangeGridFormate(dataGridView_LabourRate);
            CommanHelper.ChangeGridFormate(dataGridView_GhattakList);
            CommanHelper.ChangeGridFormate(dataGridView_Commission);
            CommanHelper.ChangeGridFormate(dataGridView_BrokerageSetting);
            dataGridView1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridView2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        #region Helper


        private void BindCreditLimitOpeningColumn()
        {
            dataGridView2.Columns.Clear();

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

            DataGridViewComboBoxColumn col3 = new DataGridViewComboBoxColumn();
            col3.DataPropertyName = "JN";
            col3.HeaderText = "J/N";
            col3.Name = "JN";
            col3.Items.Clear();
            col3.Items.Add("JAMA");
            col3.Items.Add("NAAM");
            col3.FlatStyle = FlatStyle.Popup;
            dataGridView2.Columns.Add(col3);
        }

        private void AdjustCreditLimitColumnOrder()
        {
            dataGridView2.Columns["Name"].DisplayIndex = 0;
            dataGridView2.Columns["Limit"].DisplayIndex = 1;
            dataGridView2.Columns["JN"].DisplayIndex = 2;
        }

        private void SetCreditLimitGridView_ColumnWith()
        {
            dataGridViewCreditPeriod.Columns["DateFrom"].Width = 80;
            dataGridViewCreditPeriod.Columns["DateTo"].Width = 80;
            dataGridViewCreditPeriod.Columns["RateRevised"].Width = 90;
            dataGridViewCreditPeriod.Columns["Category"].Width = 80;
            dataGridViewCreditPeriod.Columns["Product"].Width = 130;
            dataGridViewCreditPeriod.Columns["Westage"].Width = 60;
            dataGridViewCreditPeriod.Columns["AmountWeight"].Width = 60;
            dataGridViewCreditPeriod.Columns["Tran_Type"].Width = 100;
            dataGridViewCreditPeriod.Columns["Days"].Width = 50;
        }

        private void SetLabourRateGridView_ColumnWith()
        {
            dataGridView_LabourRate.Columns["WeightPcs"].Width = 50;
            dataGridView_LabourRate.Columns["Category"].Width = 55;
            dataGridView_LabourRate.Columns["Product"].Width = 100;
            this.dataGridView_LabourRate.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SetGhattakListGridView_ColumnWith()
        {
            dataGridView_GhattakList.Columns["WeightPcs"].Width = 60;
            dataGridView_GhattakList.Columns["Category"].Width = 55;
            dataGridView_GhattakList.Columns["Product"].Width = 100;
            dataGridView_GhattakList.Columns["Ghattak"].Width = 50;
            dataGridView_GhattakList.Columns["PayType"].Width = 75;
            this.dataGridView_GhattakList.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SetCommissionListGridView_ColumnWith()
        {
            dataGridView_Commission.Columns["WeightPcs"].Width = 50;
            dataGridView_Commission.Columns["Category"].Width = 50;
            dataGridView_Commission.Columns["Product"].Width = 110;
            this.dataGridView_Commission.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SetBrokerageListGridView_ColumnWith()
        {
            dataGridView_BrokerageSetting.Columns["Product"].Width = 110;
            this.dataGridView_BrokerageSetting.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView_BrokerageSetting.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void BindOpeningMCXColumn()
        {
            dataGridView1.Columns.Clear();
            this.dataGridView1.RowHeadersVisible = false;
            dtpOpeningDate.DataPropertyName = "OpeningDate";
            dtpOpeningDate.HeaderText = "Date(Op)";
            dtpOpeningDate.Name = "OpeningDate";
            dataGridView1.Columns.Add(dtpOpeningDate);


            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Item";
            col1.HeaderText = "Item";
            col1.Name = "Item";
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Wt/Amt";
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

            DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "Narration";
            col5.HeaderText = "Narration";
            col5.Name = "Narration";
            dataGridView1.Columns.Add(col5);
        }

        private void BindOpeningOtherColumn()
        {
            dataGridView1.Columns.Clear();
            this.dataGridView1.RowHeadersVisible = false;
            dtpOpeningDate.DataPropertyName = "OpeningDate";
            dtpOpeningDate.HeaderText = "Date(Op)";
            dtpOpeningDate.Name = "OpeningDate";
            dataGridView1.Columns.Add(dtpOpeningDate);


            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Item";
            col1.HeaderText = "Item";
            col1.Name = "Item";
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Wt/Amt";
            col2.Name = "Weight";
            dataGridView1.Columns.Add(col2);

            DataGridViewComboBoxColumn col3 = new DataGridViewComboBoxColumn();
            col3.DataPropertyName = "DrCr";
            col3.HeaderText = "J/N";
            col3.Name = "DrCr";
            col3.Items.Add("J");
            col3.Items.Add("N");
            col3.FlatStyle = FlatStyle.Popup;
            //col3.dropdownsty
            dataGridView1.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "Narration";
            col4.HeaderText = "Narration";
            col4.Name = "Narration";
            dataGridView1.Columns.Add(col4);

        }

        private void BindWorkerColumn(String _Category)
        {
            try
            {
                if (_Category == "COMMON") { _Category = ""; }
                this.dataGridView1.Columns.Clear();
                this.dataGridView1.RowHeadersVisible = true;
                dtpOpening.DataPropertyName = "OpeningDate";
                dtpOpening.HeaderText = "Date(Op)";
                dtpOpening.Name = "OpeningDate";
                dataGridView1.Columns.Add(dtpOpening);

                col_ProductItem.DataPropertyName = "Item";
                col_ProductItem.HeaderText = "Item";
                col_ProductItem.Name = "Item";
                if (_Category == "")
                {
                    CommanHelper.GetProduct_Worker(col_ProductItem);
                }
                else
                {
                    CommanHelper.GetProductCategoryWise_Worker(col_ProductItem, _Category);
                }
                col_ProductItem.FlatStyle = FlatStyle.Popup;
                dataGridView1.Columns.Add(col_ProductItem);

                DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
                col2.DataPropertyName = "Weight";
                col2.HeaderText = "Wt/Amt";
                col2.Name = "Weight";
                dataGridView1.Columns.Add(col2);

                DataGridViewComboBoxColumn col3 = new DataGridViewComboBoxColumn();
                col3.DataPropertyName = "DrCr";
                col3.HeaderText = "J/N";
                col3.Name = "DrCr";
                col3.Items.Add("J");
                col3.Items.Add("N");
                col3.FlatStyle = FlatStyle.Popup;
                dataGridView1.Columns.Add(col3);

                DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
                col4.DataPropertyName = "Narration";
                col4.HeaderText = "Narration";
                col4.Name = "Narration";
                dataGridView1.Columns.Add(col4);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }


        private void AdjustWorkerColumnOrder()
        {
            dataGridView1.Columns["OpeningDate"].DisplayIndex = 0;
            dataGridView1.Columns["Item"].DisplayIndex = 1;
            dataGridView1.Columns["Weight"].DisplayIndex = 2;
            dataGridView1.Columns["DrCr"].DisplayIndex = 3;
            dataGridView1.Columns["Narration"].DisplayIndex = 4;
        }

        private void SetWorkerGridView_ColumnWith()
        {
            dataGridView1.Columns["OpeningDate"].Width = 80;
            dataGridView1.Columns["Item"].Width = 100;
            dataGridView1.Columns["Weight"].Width = 70;
            dataGridView1.Columns["DrCr"].Width = 40;
            dataGridView1.Columns["Narration"].Width = 90;
        }

        private void ClearControl()
        {
            cmbtype.SelectedIndex = -1;
            cmbtype.Text = "";
            cmbCategory.SelectedIndex = -1;
            cmbCategory.Text = "";
            txtPartyName.Clear();
            cmbBullion.SelectedIndex = -1;
            txtaddress.Clear();
            txtemailid.Clear();
            txtcontactno.Clear();
            cmbgrouphead.SelectedIndex = -1;
            cmbsubhead.SelectedIndex = -1;
            cmbIntroducer.SelectedIndex = -1;
            cmbIntroducer.Text = "";
            cmbShowtrail.Text = "YES";
            chkWithCreditLimit.Checked = false;
            cmbLot.SelectedIndex = -1;
            rateupdate_radio.Checked = false;
            rateupdate_radio_N.Checked = false;
            cmb_gen_type.SelectedIndex = -1;
            txtBankCredit.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridViewCreditPeriod.Visible = false;
            BindCreditLimitOpeningColumn();
            AdjustCreditLimitColumnOrder();
            grpPartyCrditL.Visible = false;
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            BindOpeningOtherColumn();

            cmbPopUp.SelectedIndex = -1;
            CommanHelper.BindPartyName(cmbPopUp);
            CommanHelper.GetIntroducer(cmbIntroducer, "");

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

            dataGridView_BrokerageSetting.DataSource = null;
            dataGridView_BrokerageSetting.Rows.Clear();
            groupBox_BrokerageSetting.Visible = false;

            dataGridView_Commission.DataSource = null;
            dataGridView_Commission.Rows.Clear();
            groupBox_CommissionList.Visible = false;
            lblBankCredit.Visible = false;
            txtBankCredit.Visible = false;
            cmbtype.Focus();
        }

        private void GetPartyDetails(String strPartyName)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand("Select Type,Category,PartyName,PartyType,Address,Email,ContactNo,GroupHead,SubGroup,IntroducerName,ShowInTrail,WithCreditPeriod,CreditPeriod,RateUpdate,Lot,LotGenerate,BankCredit From PartyDetails Where PartyName = '" + strPartyName + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmbtype.Text = "";
                cmbCategory.SelectedIndex = -1;
                txtPartyName.Clear();
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
                txtBankCredit.Clear();
                txtBankCredit.Visible = false;
                lblBankCredit.Visible = false;
                cmbDays.SelectedIndex = -1;
                if (dr.Read())
                {
                    cmbtype.Text = dr["Type"].ToString();
                    cmbCategory.Text = dr["Category"].ToString();
                    txtPartyName.Text = dr["PartyName"].ToString();
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
                    if (dr["SubGroup"].ToString().Trim() == "BANK OD/CC")
                    {
                        txtBankCredit.Visible = true;
                        lblBankCredit.Visible = true;
                        txtBankCredit.Text = dr["BankCredit"].ToString();
                    }

                    cmbDays.Text = dr["CreditPeriod"].ToString();
                    if (dr["RateUpdate"].ToString().Trim().ToUpper() == "NO")
                    {
                        rateupdate_radio_N.Checked = true;
                    }
                    else if (dr["RateUpdate"].ToString().Trim().ToUpper() == "YES")
                    {
                        rateupdate_radio.Checked = true;
                    }
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TabIndex_Default()
        {
            panel_Type.TabIndex = 0;
            cmbtype.TabIndex = 1;
            Panel_ShowInTrail.TabIndex = 503;
            cmbShowtrail.TabIndex = 504;
            Panel_lot.TabIndex = 505;
            cmbLot.TabIndex = 506;
            Panel_LotGenerate.TabIndex = 507;
            cmb_gen_type.TabIndex = 508;
            Panel_Category.TabIndex = 509;
            cmbCategory.TabIndex = 510;
            txtPartyName.TabIndex = 511;
            Panel_McxBullion.TabIndex = 512;
            cmbBullion.TabIndex = 513;
            txtaddress.TabIndex = 514;
            txtcontactno.TabIndex = 515;
            txtemailid.TabIndex = 516;
            cmbgrouphead.TabIndex = 517;
            cmbsubhead.TabIndex = 518;
            cmbIntroducer.TabIndex = 519;
            dataGridView1.TabIndex = 520;
            groupBox_CommissionList.TabIndex = 521;
            dataGridView_Commission.TabIndex = 522;
            groupBox_LabourRate.TabIndex = 523;
            dataGridView_LabourRate.TabIndex = 524;
            groupBox_GhattakList.TabIndex = 525;
            dataGridView_GhattakList.TabIndex = 526;
            groupBox_BrokerageSetting.TabIndex = 527;
            dataGridView_BrokerageSetting.TabIndex = 528;
            grpPartyCrditL.TabIndex = 529;
            dataGridView2.TabIndex = 530;
            grpBoxWithCreditLimit.TabIndex = 531;
            chkWithCreditLimit.TabIndex = 532;
            groBoxCreditPeriod.TabIndex = 533;
            cmbDays.TabIndex = 534;
            rateupdate_radio.TabIndex = 535;
            rateupdate_radio_N.TabIndex = 536;
            dataGridViewCreditPeriod.TabIndex = 537;
            groupBox2.TabIndex = 538;
            btnSave.TabIndex = 539;
            btnDelete.TabIndex = 540;
            btnRefresh.TabIndex = 541;
            btnReport.TabIndex = 542;
            btnExit.TabIndex = 543;
            groupBox3.TabIndex = 544;
            cmbPopUp.TabIndex = 545;
            groupBox4.TabIndex = 546;
            groupBox6.TabIndex = 547;
            ckall.TabIndex = 548;
            ckparty.TabIndex = 549;
            ckworker.TabIndex = 550;
            groupBox5.TabIndex = 551;
            rbtnwithopbal.TabIndex = 552;
            rbtnwithoutopbal.TabIndex = 553;
            btnShow.TabIndex = 554;


        }


        private void TabStopSetting_False()
        {
            panel_Type.TabStop = true;
            cmbtype.TabStop = true;
            Panel_ShowInTrail.TabStop = false;
            cmbShowtrail.TabStop = false;
            Panel_lot.TabStop = false;
            cmbLot.TabStop = false;
            Panel_LotGenerate.TabStop = false;
            cmb_gen_type.TabStop = false;
            Panel_Category.TabStop = false;
            cmbCategory.TabStop = false;
            txtPartyName.TabStop = false;
            Panel_McxBullion.TabStop = false;
            cmbBullion.TabStop = false;
            txtaddress.TabStop = false;
            txtcontactno.TabStop = false;
            txtemailid.TabStop = false;
            cmbgrouphead.TabStop = false;
            cmbsubhead.TabStop = false;
            cmbIntroducer.TabStop = false;
            dataGridView1.TabStop = false;
            groupBox_CommissionList.TabStop = false;
            dataGridView_Commission.TabStop = false;
            groupBox_LabourRate.TabStop = false;
            dataGridView_LabourRate.TabStop = false;
            groupBox_GhattakList.TabStop = false;
            dataGridView_GhattakList.TabStop = false;
            groupBox_BrokerageSetting.TabStop = false;
            dataGridView_BrokerageSetting.TabStop = false;
            grpPartyCrditL.TabStop = false;
            dataGridView2.TabStop = false;
            grpBoxWithCreditLimit.TabStop = false;
            chkWithCreditLimit.TabStop = false;
            groBoxCreditPeriod.TabStop = false;
            cmbDays.TabStop = false;
            rateupdate_radio.TabStop = false;
            rateupdate_radio_N.TabStop = false;
            dataGridViewCreditPeriod.TabStop = false;
            groupBox2.TabStop = false;
            btnSave.TabStop = false;
            btnDelete.TabStop = false;
            btnRefresh.TabStop = false;
            btnReport.TabStop = false;
            btnExit.TabStop = false;
            groupBox3.TabStop = false;
            cmbPopUp.TabStop = false;
            groupBox4.TabStop = false;
            groupBox6.TabStop = false;
            ckall.TabStop = false;
            ckparty.TabStop = false;
            ckworker.TabStop = false;
            groupBox5.TabStop = false;
            rbtnwithopbal.TabStop = false;
            rbtnwithoutopbal.TabStop = false;
            btnShow.TabStop = false;
        }

        #endregion



        private void PartyInformation_Load(object sender, EventArgs e)
        {
            TabIndex_Default();
            TabStopSetting_False();
            this.CancelButton = btnExit;
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            this.toolStripMenu_Save.Click += new EventHandler(btnsave_Click);
            this.toolStripMenu_Delete.Click += new EventHandler(btndelete_Click);
            this.toolStripMenu_Refersh.Click += new EventHandler(btnrefresh_Click);
            this.toolStripMenu_Report.Click += new EventHandler(btnReport_Click);

            con = new OleDbConnection();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

            BindOpeningOtherColumn();
            BindCreditLimitOpeningColumn();
            AdjustCreditLimitColumnOrder();
            oCreditPeriodEntity.BindCreditPeriod(dataGridViewCreditPeriod, "");
            oGhattakListEntity.BindGhattakList(dataGridView_GhattakList, "");
            oLaboursRateEntity.BindLabourRate(dataGridView_LabourRate, "");
            oCommissionListEntity.BindCommissionList(dataGridView_Commission, "");
            oBrokerageSettingEntity.BindBrokerageList(dataGridView_BrokerageSetting);
            CommanHelper.BindPartyName(cmbPopUp);
            CommanHelper.GetIntroducer(cmbIntroducer, "");

            cmbShowtrail.Text = "YES";
            CommanHelper.BindMetalCategory(cmbCategory);
            cmbCategory.Items.Add("COMMON");
            for (int i = 0; i <= 365; i++)
            {
                cmbDays.Items.Add(i);
            }

            CommanHelper.ComboBoxItem(cmbgrouphead, "GroupHead", "Distinct(GroupHead)");
            SetCreditLimitGridView_ColumnWith();

            SetLabourRateGridView_ColumnWith();
            SetGhattakListGridView_ColumnWith();
            SetCommissionListGridView_ColumnWith();
            SetBrokerageListGridView_ColumnWith();
            SetWorkerGridView_ColumnWith();

            this.dataGridViewCreditPeriod.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewCreditPeriod.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewCreditPeriod.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            cmbtype.Focus();

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
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

                if (cmbgrouphead.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select Group head", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbgrouphead.Focus();
                    return;
                }

                if (cmbsubhead.Text.Trim() == "")
                {
                    MessageBox.Show("Please Select Sub Group head", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbsubhead.Focus();
                    return;
                }
                if (txtPartyName.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter The PartyName", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPartyName.Focus();
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
                    if (CommanHelper.AlreadyExistParty(txtPartyName.Text.Trim()) == true)
                    {
                        MessageBox.Show("Party Already Exist.", "Party Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtPartyName.Focus();
                        return;
                    }
                } DateTime _OpeningDate = Conversion.ConToDT(DateTime.Now.ToString("MM/dd/yyyy"));
                String _PartyName = "";
                String strWithCreditLimit = "";
                int _CreditPeriod = 0;
                String rate_revised = "";
                if (chkWithCreditLimit.Checked == true) { strWithCreditLimit = "YES"; }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                if (cmbPopUp.Text.Trim() == "")
                {
                    _PartyName = txtPartyName.Text.Trim();
                }
                else
                {
                    _PartyName = cmbPopUp.Text.Trim();
                }

                if (cmbDays.Text.Trim() != "")
                {
                    _CreditPeriod = Conversion.ConToInt(cmbDays.Text.Trim());
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



                #region Insert PartyDetails
                if (rateupdate_radio.Checked == true)
                {
                    rate_revised = "YES";
                }
                if (rateupdate_radio_N.Checked == true)
                {
                    rate_revised = "NO";
                }
                PartyInformationFactory.Insert(cmbtype.Text.Trim(), cmbCategory.Text.Trim(), txtPartyName.Text.Trim(), cmbBullion.Text.Trim(), txtaddress.Text.Trim(), txtemailid.Text.Trim(), txtcontactno.Text.Trim(), cmbgrouphead.Text.Trim(), cmbsubhead.Text.Trim(), cmbIntroducer.Text.Trim(), cmbShowtrail.Text.Trim(), strWithCreditLimit, _CreditPeriod, rate_revised.Trim(), cmbLot.Text.Trim(), cmb_gen_type.Text.Trim(), Conversion.ConToDec(txtBankCredit.Text.Trim()), con, Tran);

                #endregion



                if (cmbtype.Text.Trim().ToUpper() == "PARTY")
                {
                    #region Insert Credit Limit
                    foreach (DataGridViewRow dr in dataGridView2.Rows)
                    {
                        CreditLimitFactory.Insert(txtPartyName.Text.Trim(), dr.Cells[0].Value.ToString().Trim(), Conversion.ConToDec6((dr.Cells[1].Value ?? (object)"").ToString().Trim()), (dr.Cells[2].Value ?? (object)"").ToString().Trim(), con, Tran);
                    }

                    //--------Insert Credit Limit And Credit Period                    
                    if (chkWithCreditLimit.Checked == true)
                    {
                        if(rateupdate_radio.Checked==true){
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

                            _DateFrom = Conversion.GetDateStr((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                            _DateTo = Conversion.GetDateStr((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                            _RateRevised = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                            _Category = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                            _Product = (dr.Cells[4].Value ?? (object)"").ToString().Trim();
                            _Westage = Conversion.ConToDec6((dr.Cells[5].Value ?? (object)"").ToString().Trim());
                            _Amount = Conversion.ConToDec6((dr.Cells[6].Value ?? (object)"").ToString().Trim());
                            _Tran_Type = (dr.Cells[7].Value ?? (object)"").ToString().Trim();
                            _Days = Conversion.ConToInt((dr.Cells[8].Value ?? (object)"").ToString().Trim());

                            if (_RateRevised != "" && _Product != "" && _Westage != 0 && _Amount != 0 && _Tran_Type != "" && _Days != 0)
                            {
                                CreditPeriodFactory.Insert(txtPartyName.Text.Trim(), _DateFrom, _DateTo, _RateRevised, _Category, _Product, _Westage, _Amount, _Tran_Type, _Days, con, Tran);
                            }
                        }
                            }
                    }
                    #endregion

                    #region Insert Brokerage Setting

                    cmd.CommandText = "Delete From BrokerageSetting Where PartyName = '" + _PartyName + "'";
                    cmd.ExecuteNonQuery();

                    if (cmbBullion.Text.Trim().ToUpper() == "MCX")
                    {
                        foreach (DataGridViewRow dr in dataGridView_BrokerageSetting.Rows)
                        {

                            DateTime _DateFrom = DateTime.Now;
                            DateTime _DateTo = DateTime.Now;
                            String _BrokType = "";
                            String _Product = "";
                            Decimal _BrokRate = 0;
                            String _TranType = "";
                            Decimal _LotSet = 0;
                            String _PType = "";

                            _DateFrom = Conversion.GetDateStr((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                            _DateTo = Conversion.GetDateStr((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                            _BrokType = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                            _Product = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                            _BrokRate = Conversion.ConToDec6((dr.Cells[4].Value ?? (object)"").ToString().Trim());
                            _TranType = (dr.Cells[5].Value ?? (object)"").ToString().Trim();
                            _LotSet = Conversion.ConToDec6((dr.Cells[6].Value ?? (object)"").ToString().Trim());
                            _PType = (dr.Cells[7].Value ?? (object)"").ToString().Trim();

                            if (_BrokType != "" && _Product != "" && _BrokRate != 0 && _TranType != "" && _LotSet != 0 && _PType != "")
                            {
                                BrokerageSettingFactory.Insert(txtPartyName.Text.Trim(), _DateFrom, _DateTo, _BrokType, "", _Product, _BrokRate, _TranType, _LotSet, _PType, con, Tran);
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Insert Labour Rate

                    cmd.CommandText = "Delete From LaboursRate Where PartyName = '" + _PartyName + "'";
                    cmd.ExecuteNonQuery();

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
                        String _JN = "";

                        var _date = (dr.Cells[0].Value ?? (object)"");
                        _DateFrom = Conversion.ConToDT(_date);
                        _DateFrom = Conversion.ConToDT((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                        _DateTo = Conversion.ConToDT((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                        _WeightPcs = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                        _Category = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                        _Product = (dr.Cells[4].Value ?? (object)"").ToString().Trim();
                        _Fine_Amount = (dr.Cells[5].Value ?? (object)"").ToString().Trim();
                        _LaboursRate = Conversion.ConToDec6((dr.Cells[6].Value ?? (object)"").ToString().Trim());
                        _PayType = (dr.Cells[7].Value ?? (object)"").ToString().Trim();
                        _JN = (dr.Cells[8].Value ?? (object)"").ToString().Trim();

                        if (_WeightPcs != "" && _Fine_Amount != "" && _LaboursRate != 0 && _PayType != "" && _JN != "")
                        {
                            LaboursRateFactory.Insert(txtPartyName.Text.Trim(), _DateFrom, _DateTo, _WeightPcs, _Category, _Product, _Fine_Amount, _LaboursRate, _PayType, _JN, con, Tran);
                        }
                    }
                    #endregion

                    #region Insert Ghattak List

                    cmd.CommandText = "Delete from GhattakList Where PartyName = '" + _PartyName + "'";
                    cmd.ExecuteNonQuery();

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
                        _Ghattak = Conversion.ConToDec6((dr.Cells[5].Value ?? (object)"").ToString().Trim());
                        _Jama_Naam = (dr.Cells[6].Value ?? (object)"").ToString().Trim();
                        _PayType = (dr.Cells[7].Value ?? (object)"").ToString().Trim();

                        if (_WeightPcs != "" && _Ghattak != 0 && _Jama_Naam != "" && _PayType != "")
                        {
                            GhattakListFactory.Insert(txtPartyName.Text.Trim(), _DateFrom, _DateTo, _WeightPcs, _Category, _Product, _Ghattak, _PayType, _Jama_Naam, con, Tran);
                        }
                    }

                    #endregion
                }

                if (cmbIntroducer.Text.Trim() != "")
                {
                    #region Insert Commission List

                    cmd.CommandText = "Delete From CommissionList Where PartyName = '" + _PartyName + "'";
                    cmd.ExecuteNonQuery();

                    foreach (DataGridViewRow dr in dataGridView_Commission.Rows)
                    {
                        DateTime _DateFrom = DateTime.Now;
                        DateTime _DateTo = DateTime.Now;
                        String _WeightPcs = "";
                        String _Category = "";
                        String _Product = "";
                        String _Fine_Amount = "";
                        Decimal _BrokerageRate = 0;
                        String _PayType = "";
                        String _JN = "";

                        _DateFrom = Conversion.ConToDT((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                        _DateTo = Conversion.ConToDT((dr.Cells[1].Value ?? (object)"").ToString().Trim());
                        _WeightPcs = (dr.Cells[2].Value ?? (object)"").ToString().Trim();
                        _Category = (dr.Cells[3].Value ?? (object)"").ToString().Trim();
                        _Product = (dr.Cells[4].Value ?? (object)"").ToString().Trim();
                        _Fine_Amount = (dr.Cells[5].Value ?? (object)"").ToString().Trim();
                        _BrokerageRate = Conversion.ConToDec6((dr.Cells[6].Value ?? (object)"").ToString().Trim());
                        _PayType = (dr.Cells[7].Value ?? (object)"").ToString().Trim();
                        _JN = (dr.Cells[8].Value ?? (object)"").ToString().Trim();

                        if (_WeightPcs != "" && _Product != "" && _Fine_Amount != "" && _BrokerageRate != 0 && _JN != "" && _PayType != "")
                        {
                            CommissionListFactory.Insert(txtPartyName.Text.Trim(), _DateFrom, _DateTo, _WeightPcs, _Category, _Product, _Fine_Amount, _BrokerageRate, _PayType, _JN, con, Tran);
                        }
                    }


                    #endregion
                }

                #region Insert Party Opening

                //----------Insert Party Opening

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    String _MetalCategory = "";
                    Decimal _Weight = 0;
                    _OpeningDate = DateTime.Now;
                    _OpeningDate = Conversion.ConToDT((dr.Cells[0].Value ?? (object)"").ToString().Trim());
                    String _Narration = "";
                    String _JN = "";
                    if (cmbBullion.Text.Trim().ToUpper() == "MCX")
                    {

                        Decimal _Sell = 0;
                        Decimal _Purchase = 0;
                        _MetalCategory = "";
                        Decimal _OpeningWeight = 0;
                        Decimal _MCXRate = 0;
                        _MetalCategory = dr.Cells["Item"].Value.ToString().Trim();
                        _OpeningWeight = Conversion.ConToDec6(dr.Cells["Weight"].Value.ToString().Trim());
                        _MCXRate = Conversion.ConToDec6(dr.Cells["Closing"].Value.ToString().Trim());
                        _JN = (dr.Cells["DrCr"].Value ?? (object)"").ToString().Trim();
                        _Narration = (dr.Cells["Narration"].Value ?? (object)"").ToString().Trim();
                        if (_JN.Trim() == "SELL")
                        {
                            _Sell = Conversion.ConToDec6(dr.Cells[2].Value.ToString().Trim());
                            _Weight = -Conversion.ConToDec6(dr.Cells[2].Value.ToString().Trim());
                        }
                        if (_JN.Trim() == "PURCHASE")
                        {
                            _Purchase = Conversion.ConToDec6(dr.Cells[2].Value.ToString().Trim());
                            _Weight = Conversion.ConToDec6(dr.Cells[2].Value.ToString().Trim());
                        }

                        //-----------Insert Opening In PartyTran

                        PartyOpeningFactory.Insert(txtPartyName.Text.Trim(), _OpeningDate, _MetalCategory, _OpeningWeight, _MCXRate, _JN, _Narration, con, Tran);
                        if ((dr.Cells[3].Value ?? (object)"").ToString().Trim() != "")
                        {
                            PartyTranFactory.InsertPartyInformation(DateTime.Now.ToString("MM/dd/yyy"), cmbCategory.Text.Trim(), txtPartyName.Text.Trim(), _MetalCategory, _MetalCategory, _Sell, _Purchase, _Weight, _MCXRate, "O", CommanHelper.CompName.ToString(), "PARTY OPENING", con, Tran);
                        }
                    }
                    else
                    {
                        _MetalCategory = "";
                        Decimal _Debit = 0;
                        Decimal _Credit = 0;
                        _MetalCategory = (dr.Cells["Item"].Value ?? (object)"").ToString().Trim();
                        _Weight = Conversion.ConToDec6((dr.Cells["Weight"].Value ?? (object)"").ToString().Trim());
                        _JN = (dr.Cells["DrCr"].Value ?? (object)"").ToString().Trim();
                        _Narration = (dr.Cells["Narration"].Value ?? (object)"").ToString().Trim();
                        if (_JN.Trim() == "N")
                        {
                            _Debit = Conversion.ConToDec6((dr.Cells[2].Value ?? (object)"").ToString().Trim());
                        }
                        if (_JN.Trim() == "J")
                        {
                            _Credit = Conversion.ConToDec6((dr.Cells[2].Value ?? (object)"").ToString().Trim());
                        }

                        //-----------Insert Opening In PartyTran
                        if (_MetalCategory != "" && _Weight > 0 && _JN != "")
                        {
                            if (cmbtype.Text.Trim() == "WORKER")
                            {
                                PartyOpeningFactory.Insert(txtPartyName.Text.Trim(), _OpeningDate, _MetalCategory, _Weight, 0, _JN.Trim(), _Narration, con, Tran);
                            }
                            else
                            {
                                PartyOpeningFactory.Insert(txtPartyName.Text.Trim(), _OpeningDate, _MetalCategory, _Weight, 0, _JN.Trim(), _Narration, con, Tran);
                            }
                        }

                        if (_MetalCategory != "" && _Weight > 0)
                        {
                            PartyTranFactory.InsertPartyInformation(DateTime.Now.ToString("MM/dd/yyy"), cmbCategory.Text.Trim(), txtPartyName.Text.Trim(), _MetalCategory, _MetalCategory, _Debit, _Credit, 0, 0, "O", CommanHelper.CompName.ToString(), "PARTY OPENING", con, Tran);
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
                if (MessageBox.Show("Do You Want To Delete The Data", "Party Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Boolean _ValidParty = false;
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    Tran = con.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand("", con, Tran);

                    cmd.CommandText = "Select * From PartyTran Where PartyName = '" + cmbPopUp.Text.Trim() + "' And TranType <> 'O'";
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        _ValidParty = true;
                    }
                    dr.Close();
                    if (_ValidParty == true)
                    {
                        MessageBox.Show("U Can't Delete The Party. Transaction Exist.", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Tran.Commit();
                        con.Close();
                        return;
                    }
                    else
                    {
                        cmd.CommandText = "Delete From PartyDetails Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From PartyOpening Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From CreditLimit Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From PartyTran Where PartyName = '" + cmbPopUp.Text.Trim() + "' And TranType = 'O'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From CreditPeriod Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From CommissionList Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete from GhattakList Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From LaboursRate Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Delete From BrokerageSetting Where PartyName = '" + cmbPopUp.Text.Trim() + "'";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data SuccessFully Deleted", "Party Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Tran.Commit();
                        con.Close();
                        ClearControl();
                        cmbtype.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Tran.Rollback();
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
                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting


                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                #endregion

                #region Set Tab StopValue


                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                }
                else
                {
                    Panel_Category.TabStop = true;
                    cmbCategory.TabStop = true;
                }
                #endregion

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                cmbBullion.Items.Clear();
                cmbBullion.Text = "";
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.Visible = true;
                    dataGridView_LabourRate.Visible = true;
                    cmbBullion.Visible = false;
                    BindWorkerColumn("");
                    AdjustWorkerColumnOrder();
                    SetWorkerGridView_ColumnWith();
                    chkWithCreditLimit.Checked = false;
                    cmbDays.Text = "";
                    cmbShowtrail.Text = "YES";
                    groupBox_BrokerageSetting.Visible = false;
                    groBoxCreditPeriod.Visible = false;
                    lblLot.Visible = true;
                    cmbLot.Enabled = true;
                    cmbLot.Visible = true;
                    grpPartyCrditL.Visible = false;
                    cmbgrouphead.Text = "LABOUR JOB";
                    grpBoxWithCreditLimit.Visible = false;
                    groBoxCreditPeriod.Visible = false;
                }
                else
                {
                    groupBox_LabourRate.Visible = false;
                    dataGridView_LabourRate.Visible = false;
                    groupBox_GhattakList.Visible = false;
                    cmbBullion.Visible = true;
                    grpPartyCrditL.Visible = true;
                    this.grpPartyCrditL.Size = new System.Drawing.Size(215, 140);
                    this.dataGridView2.Size = new System.Drawing.Size(200, 120);
                    this.grpPartyCrditL.Location = new System.Drawing.Point(692, 34);
                    cmbLot.SelectedIndex = -1;
                    cmbLot.Enabled = false;
                    cmbLot.Visible = false;
                    lblLot.Visible = false;
                    lblLotGenerateIn.Visible = false;
                    Panel_LotGenerate.Visible = false;
                    if (cmbtype.Text.Trim() == "PARTY")
                    {
                        cmbBullion.Items.Add("MCX");
                    }
                    else
                    {
                        cmbBullion.Items.Add("BULLION");
                        cmbBullion.Items.Add("MCX");
                    }
                    grpBoxWithCreditLimit.Visible = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.Visible = true;
                    }
                    else
                    { groBoxCreditPeriod.Visible = false; }
                    BindOpeningOtherColumn();
                    SetWorkerGridView_ColumnWith();
                    OpeningOtherList = CommanHelper.OpeningOther();
                    if (cmbCategory.Text.Trim() == "COMMON")
                    {
                        cmbgrouphead.Text = "";
                        cmbsubhead.Items.Clear();
                        cmbsubhead.Text = "";
                        dataGridView1.DataSource = OpeningOtherList.ToList();
                        CommanHelper.FillCreditLimitOpening(dataGridView2, "COMMON");
                    }
                    else
                    {
                        cmbgrouphead.Text = "SUNDRY DEBITORS/CREDITORS";
                        cmbsubhead.Text = "SUNDRY DEBITORS/CREDITORS";
                        var _str = new string[] { cmbCategory.Text.Trim().ToUpper(), "CASH" };
                        dataGridView1.DataSource = OpeningOtherList.Where(x => _str.Contains(x.Item)).ToList();
                        CommanHelper.FillCreditLimitOpening(dataGridView2, cmbCategory.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbBullion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_BrokerageSetting.Visible = false;
                    dataGridView_BrokerageSetting.Visible = false;
                }
                if (cmbtype.Text.Trim() != "WORKER")
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    if (cmbBullion.Text.Trim().ToUpper() == "MCX")
                    {
                        this.groupBox_BrokerageSetting.Location = new System.Drawing.Point(692, 175);
                        groupBox_BrokerageSetting.Visible = true;
                        dataGridView_BrokerageSetting.Visible = true;
                        BindOpeningMCXColumn();
                        OpeningMCXList = CommanHelper.BindMCXDefaultOpening();
                        dataGridView1.DataSource = OpeningMCXList.ToList();
                    }
                    else
                    {
                        groupBox_BrokerageSetting.Visible = false;
                        dataGridView_BrokerageSetting.Visible = false;

                        BindOpeningOtherColumn();
                        SetWorkerGridView_ColumnWith();
                        OpeningOtherList = CommanHelper.OpeningOther();
                        if (cmbCategory.Text.Trim() == "COMMON")
                        {
                            dataGridView1.DataSource = OpeningOtherList;
                        }
                        else
                        {
                            var _str = new string[] { cmbCategory.Text.Trim().ToUpper(), "CASH" };
                            dataGridView1.DataSource = OpeningOtherList.Where(x => _str.Contains(x.Item)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
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
            try
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void chkWithCreditLimit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkWithCreditLimit.Checked == true)
                {
                    if (rateupdate_radio.Checked == true)
                    {
                        dataGridViewCreditPeriod.Visible = true;
                    }
                    groBoxCreditPeriod.Visible = true;
                }
                else
                {
                    dataGridViewCreditPeriod.Visible = false;
                    groBoxCreditPeriod.Visible = false;
                }

                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    dataGridView1.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabStop = true;
                        cmbDays.TabStop = true;
                        rateupdate_radio.TabStop = true;
                        rateupdate_radio_N.TabStop = true;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cmbDays.Text != "0") && (cmbDays.Text != ""))
                {
                    label19.Visible = true;
                    rateupdate_radio.Visible = true;
                    rateupdate_radio_N.Visible = true;
                }

                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    dataGridView1.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabStop = true;
                        cmbDays.TabStop = true;
                        rateupdate_radio.TabStop = true;
                        rateupdate_radio_N.TabStop = true;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (cmbBullion.Text.Trim() != "MCX")
                {
                    oCommissionListEntity.BindCommissionList(dataGridView_Commission, cmbCategory.Text.Trim());
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        BindWorkerColumn(cmbCategory.Text.Trim());
                        AdjustWorkerColumnOrder();
                        SetWorkerGridView_ColumnWith();
                        oLaboursRateEntity.BindLabourRate(dataGridView_LabourRate, cmbCategory.Text.Trim());
                        oGhattakListEntity.BindGhattakList(dataGridView_GhattakList, cmbCategory.Text.Trim());
                    }

                    if (cmbtype.Text.Trim() == "PARTY" || cmbtype.Text.Trim() == "OTHER")
                    {
                        BindOpeningOtherColumn();
                        SetWorkerGridView_ColumnWith();
                        if (cmbCategory.Text.Trim() == "COMMON")
                        {
                            dataGridView1.DataSource = OpeningOtherList.ToList();
                        }
                        else
                        {
                            var _str = new string[] { cmbCategory.Text.Trim().ToUpper(), "CASH" };
                            dataGridView1.DataSource = OpeningOtherList.Where(x => _str.Contains(x.Item)).ToList();
                        }
                        oCreditPeriodEntity.BindCreditPeriod(dataGridViewCreditPeriod, cmbCategory.Text.Trim());
                    }
                }
                if (cmbBullion.Text.Trim() == "MCX" || cmbBullion.Text.Trim() == "")
                {
                    if (cmbtype.Text.Trim() == "PARTY" || cmbtype.Text.Trim() == "OTHER")
                    {
                        BindCreditLimitOpeningColumn();
                        AdjustCreditLimitColumnOrder();
                        OpeningOtherList = CommanHelper.OpeningOther();
                        if (cmbCategory.Text.Trim() == "COMMON")
                        {
                            CommanHelper.FillCreditLimitOpening(dataGridView2, "COMMON");
                        }
                        else
                        {
                            CommanHelper.FillCreditLimitOpening(dataGridView2, cmbCategory.Text.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbgrouphead_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbgrouphead.Text.Trim() != "")
                {
                    cmbsubhead.Text = "";
                    CommanHelper.ComboBoxItem(cmbsubhead, "GroupHead", "Distinct(SubGroup)", "GroupHead", cmbgrouphead.Text);
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbtype_Enter(object sender, EventArgs e)
        {
            try
            {

                panel_Type.BackColor = Color.Red;
                cmbtype.BackColor = Color.Aqua;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbtype_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!cmbtype.Items.Contains(cmbtype.Text.Trim()) || cmbtype.Text.Trim() == "")
                {
                    cmbtype.Focus(); return;
                }

                //Control Back Color
                panel_Type.BackColor = Color.Transparent;
                cmbtype.BackColor = Color.White;

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbLot_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbLot.BackColor = Color.Cyan;
                Panel_lot.BackColor = Color.Red;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbLot_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbLot.Text.Trim() == "") { cmbLot.Focus(); return; }

                //Control Back Color
                cmbLot.BackColor = Color.White;
                Panel_lot.BackColor = Color.Transparent;

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmb_gen_type_Enter(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting
                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                Panel_lot.TabIndex = 4;
                cmbLot.TabIndex = 5;
                Panel_LotGenerate.TabIndex = 6;
                cmb_gen_type.TabIndex = 7;
                if (cmb_gen_type.Text.Trim() != "")
                {
                    Panel_Category.TabIndex = 8;
                    cmbCategory.TabIndex = 9;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_lot.TabStop = true;
                cmbLot.TabStop = true;
                Panel_LotGenerate.TabStop = true;
                cmb_gen_type.TabStop = true;

                if (cmb_gen_type.Text.Trim() != "")
                {
                    Panel_Category.TabStop = true;
                    cmbCategory.TabStop = true;
                }

                #endregion

                cmb_gen_type.BackColor = Color.Cyan;
                Panel_LotGenerate.BackColor = Color.Red;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmb_gen_type_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmb_gen_type.Text.Trim() == "") { cmb_gen_type.Focus(); return; }

                //Set Control Backcolor
                cmb_gen_type.BackColor = Color.White;
                Panel_LotGenerate.BackColor = Color.Transparent;

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbDays_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbDays.BackColor = Color.Cyan;

                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    dataGridView1.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabStop = true;
                        cmbDays.TabStop = true;
                        rateupdate_radio.TabStop = true;
                        rateupdate_radio_N.TabStop = true;
                    }
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbDays_Leave(object sender, EventArgs e)
        {
            cmbDays.BackColor = Color.White;
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbCategory.BackColor = Color.Cyan;
                Panel_Category.BackColor = Color.Red;

                //Set Tab Setting 
                TabIndex_Default();
                TabStopSetting_False();


                #region Tab Index Setting
                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;

                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }


                txtPartyName.TabStop = true;
                Panel_McxBullion.TabStop = true;
                cmbBullion.TabStop = true;
                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbBullion.Text.Trim() != "MCX")
                {
                    if (cmbCategory.Text.Trim() != "")
                    {
                        if (!cmbCategory.Items.Contains(cmbCategory.Text.Trim()))
                        { cmbCategory.Focus(); return; }
                    }
                    if (cmbPopUp.Text.Trim() == "")
                    {
                        if (cmbtype.Text.Trim() != "WORKER")
                        {
                            BindOpeningOtherColumn();
                            SetWorkerGridView_ColumnWith();
                            OpeningOtherList = CommanHelper.OpeningOther();
                            if (cmbCategory.Text.Trim() == "COMMON")
                            {
                                dataGridView1.DataSource = OpeningOtherList.ToList();
                                CommanHelper.FillCreditLimitOpening(dataGridView2, "COMMON");
                            }
                            else
                            {
                                var _str = new string[] { cmbCategory.Text.Trim().ToUpper(), "CASH" };
                                dataGridView1.DataSource = OpeningOtherList.Where(x => _str.Contains(x.Item)).ToList();
                                CommanHelper.FillCreditLimitOpening(dataGridView2, cmbCategory.Text.Trim());
                            }
                        }
                    }
                }

                //Set Control Backcolor
                cmbCategory.BackColor = Color.White;
                Panel_Category.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtpartyname_Enter(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting 
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;

                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                else
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                #endregion


                //Set Control BackColor
                txtPartyName.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtpartyname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPartyName.Text.Trim() == "") { txtPartyName.Focus(); return; }
                txtPartyName.Text = CommanHelper.UpperCaseFirstCharacter(txtPartyName.Text.Trim());

                //Set Control Backcolor
                txtPartyName.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        private void cmbBullion_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbBullion.BackColor = Color.Cyan;
                Panel_McxBullion.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbBullion_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbBullion.Text.Trim() != "")
                {
                    if (!cmbBullion.Items.Contains(cmbBullion.Text.Trim()))
                    { cmbBullion.Focus(); return; }
                }
                if (cmbPopUp.Text.Trim() == "")
                {
                    if (cmbBullion.Text.Trim() == "")
                    {
                        BindOpeningOtherColumn();
                        SetWorkerGridView_ColumnWith();
                        OpeningOtherList = CommanHelper.OpeningOther();
                        if (cmbCategory.Text.Trim() == "COMMON")
                        {
                            dataGridView1.DataSource = OpeningOtherList.ToList();
                            CommanHelper.FillCreditLimitOpening(dataGridView2, "COMMON");
                        }
                        else
                        {
                            var _str = new string[] { cmbCategory.Text.Trim().ToUpper(), "CASH" };
                            dataGridView1.DataSource = OpeningOtherList.Where(x => _str.Contains(x.Item)).ToList();
                            CommanHelper.FillCreditLimitOpening(dataGridView2, cmbCategory.Text.Trim());
                        }
                    }
                }

                //Set Control Backcolor
                cmbBullion.BackColor = Color.White;
                Panel_McxBullion.BackColor = Color.Transparent;

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        private void txtaddress_Enter(object sender, EventArgs e)
        {
            txtaddress.BackColor = Color.Cyan;
        }

        private void txtaddress_Leave(object sender, EventArgs e)
        {
            //Set BackColor
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
            try
            {
                if (cmbgrouphead.Text.Trim() == "") { cmbgrouphead.Focus(); return; }
                cmbgrouphead.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbsubhead_Enter(object sender, EventArgs e)
        {
            cmbsubhead.BackColor = Color.Cyan;
        }

        private void cmbsubhead_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbsubhead.Text.Trim() == "") { cmbsubhead.Focus(); return; }
                cmbsubhead.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbIntroducer_Enter(object sender, EventArgs e)
        {
            try
            {
                CommanHelper.GetIntroducer(cmbIntroducer, cmbPopUp.Text.Trim());

                //Set Control Back Color
                cmbIntroducer.BackColor = Color.Cyan;

                //Set Tab Setting 
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    {
                        groupBox_LabourRate.TabIndex = dataGridView_Commission.TabIndex + 1;
                    }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }

                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                else
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                else
                {
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        groupBox_LabourRate.TabStop = true;
                        dataGridView_LabourRate.TabStop = true;
                    }
                    else
                    {
                        if (cmbBullion.Text.Trim() == "MCX")
                        {
                            groupBox_BrokerageSetting.TabStop = true;
                            dataGridView_BrokerageSetting.TabStop = true;
                        }
                        else
                        {
                            grpPartyCrditL.TabStop = true;
                            dataGridView2.TabStop = true;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbIntroducer_Leave(object sender, EventArgs e)
        {
            try
            {
                
                if (cmbIntroducer.Text.Trim() != "")
                {
                    if (!cmbIntroducer.Items.Contains(cmbIntroducer.Text.Trim()))
                    { cmbIntroducer.Focus(); return; }

                    groupBox_CommissionList.Visible = true;
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        this.groupBox_CommissionList.Location = new System.Drawing.Point(692, 320);
                    }
                    else
                    {
                        if (cmbBullion.Text.Trim() == "MCX")
                        {
                            this.groupBox_CommissionList.Location = new System.Drawing.Point(692, 340);
                        }
                        else
                        {
                            this.groupBox_CommissionList.Location = new System.Drawing.Point(692, 175);
                        }
                    }
                }
                if (cmbBullion.Text.Trim() != "MCX")
                {
                    this.groupBox_BrokerageSetting.Visible = false;
                }

                //Set Control Backcolor
                cmbIntroducer.BackColor = Color.White;


            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtBankCredit_Enter(object sender, EventArgs e)
        {
            try
            {
                txtBankCredit.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtBankCredit_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBankCredit.BackColor = Color.White;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbtype_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    cmbShowtrail.Focus();
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
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
                        cmbCategory.Focus();
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
                    txtPartyName.Focus();
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void txtpartyname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbPopUp.Text.Trim() == "")
                    {
                        if (CommanHelper.AlreadyExistParty(txtPartyName.Text.Trim()) == true)
                        {
                            MessageBox.Show("Party Already Exist.", "Party Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtPartyName.Focus();
                            return;
                        }
                    }
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        txtaddress.Focus();
                    }
                    else
                    {
                        cmbBullion.Focus();
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
                    txtaddress.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    txtcontactno.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
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
                groupBox_CommissionList.Visible = false;
                if (e.KeyChar == 13)
                {
                    if (cmbIntroducer.Text != "")
                    {
                        groupBox_CommissionList.Visible = true;
                        if (this.dataGridView_Commission.RowCount > 0)
                        {
                            this.dataGridView_Commission.CurrentCell = this.dataGridView_Commission[0, 0];
                        }
                        dataGridView_Commission.Focus();
                    }
                    else
                    {
                        if (cmbtype.Text.Trim() == "WORKER")
                        {
                            groupBox_LabourRate.Visible = true;
                            if (this.dataGridView_LabourRate.RowCount > 0)
                            {
                                this.dataGridView_LabourRate.CurrentCell = this.dataGridView_LabourRate[1, 0];
                                this.dataGridView_LabourRate.Focus();
                            }

                        }
                        else
                        {
                            if (cmbBullion.Text.Trim() == "MCX")
                            {
                                if (this.dataGridView_BrokerageSetting.RowCount > 0)
                                {
                                    this.dataGridView_BrokerageSetting.CurrentCell = this.dataGridView_BrokerageSetting[1, 0];
                                    this.dataGridView_BrokerageSetting.Focus();
                                }
                            }
                            else
                            {
                                if (this.dataGridView2.RowCount > 0)
                                {
                                    this.dataGridView2.CurrentCell = this.dataGridView2[1, 0];
                                    this.dataGridView2.Focus();
                                }
                                else
                                {
                                    if (this.dataGridView1.RowCount > 0)
                                    {
                                        this.dataGridView1.CurrentCell = this.dataGridView1[1, 0];
                                    }
                                    this.dataGridView1.Focus();
                                }
                            }
                        }
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
                        cmbDays.Focus();
                    else if (txtBankCredit.Visible == true)
                        txtBankCredit.Focus();
                    else
                        btnSave.Focus();
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

        private void cmbPopUp_Enter(object sender, EventArgs e)
        {
            try
            {
                //Set Control Backcolor
                cmbPopUp.BackColor = Color.Cyan;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbPopUp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbPopUp.Text.Trim() != "")
                {
                    if (!cmbPopUp.Items.Contains(cmbPopUp.Text.Trim()))
                    { cmbPopUp.Focus(); return; }

                }
                cmbPopUp.BackColor = Color.White;
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
                    rateupdate_radio.Checked = false;
                    dataGridViewCreditPeriod.DataSource = null;
                    dataGridViewCreditPeriod.Rows.Clear();
                    dataGridViewCreditPeriod.Visible = false;

                    groupBox_LabourRate.Visible = false;
                    dataGridView_LabourRate.DataSource = null;
                    dataGridView_LabourRate.Rows.Clear();
                    dataGridView_LabourRate.Visible = false;

                    dataGridView_GhattakList.DataSource = null;
                    dataGridView_GhattakList.Rows.Clear();
                    groupBox_GhattakList.Visible = false;

                    dataGridView_BrokerageSetting.DataSource = null;
                    dataGridView_BrokerageSetting.Rows.Clear();
                    groupBox_BrokerageSetting.Visible = false;

                    dataGridView_Commission.DataSource = null;
                    dataGridView_Commission.Rows.Clear();
                    groupBox_CommissionList.Visible = false;

                    GetPartyDetails(cmbPopUp.Text.Trim());

                    if (cmbBullion.Text.Trim().ToUpper() == "MCX")
                    {
                        dataGridView1.DataSource = null;
                        BindOpeningMCXColumn();
                        dataGridView1.DataSource = CommanHelper.GetPartyOpeningMCX(cmbPopUp.Text.Trim());

                        groupBox_BrokerageSetting.Visible = true;
                        this.groupBox_BrokerageSetting.Location = new System.Drawing.Point(692, 175);
                        GetBrokerage(cmbPopUp.Text.Trim());
                    }
                    else if (cmbtype.Text.Trim().ToUpper() == "WORKER")
                    {
                        dataGridView1.DataSource = null;
                        BindWorkerColumn(cmbCategory.Text.Trim());
                        AdjustWorkerColumnOrder();
                        SetWorkerGridView_ColumnWith();
                        int Snu = 0;
                        OpeningOtherList = CommanHelper.GetPartyOpening(cmbPopUp.Text.Trim());
                        foreach (var item in OpeningOtherList)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[Snu].Cells[0].Value = Convert.ToString(item.OpeningDate);
                            dataGridView1.Rows[Snu].Cells[1].Value = Convert.ToString(item.Item);
                            dataGridView1.Rows[Snu].Cells[2].Value = Convert.ToString(item.Weight);
                            dataGridView1.Rows[Snu].Cells[3].Value = Convert.ToString(item.DrCr);
                            dataGridView1.Rows[Snu].Cells[4].Value = Convert.ToString(item.Narration);
                            Snu++;
                        }
                        groupBox_LabourRate.Visible = true;
                        groupBox_GhattakList.Visible = true;

                        GetLabroursRate(cmbPopUp.Text.Trim());
                        GetGhattakList(cmbPopUp.Text.Trim());
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        BindOpeningOtherColumn();
                        SetWorkerGridView_ColumnWith();
                        dataGridView1.DataSource = CommanHelper.GetPartyOpening(cmbPopUp.Text.Trim());
                    }

                    if (rateupdate_radio.Checked == true)
                    {
                        GetCreditPeriod(cmbPopUp.Text.Trim());
                    }

                    if (cmbIntroducer.Text.Trim() != "")
                    {
                        groupBox_CommissionList.Visible = true;
                        if (cmbtype.Text.Trim() == "WORKER")
                        {
                            this.groupBox_CommissionList.Location = new System.Drawing.Point(692, 320);
                        }
                        else
                        {
                            if (cmbBullion.Text.Trim() == "MCX")
                            {
                                this.groupBox_CommissionList.Location = new System.Drawing.Point(692, 340);
                            }
                            else
                            { this.groupBox_CommissionList.Location = new System.Drawing.Point(692, 175); }
                        }
                        GetCommission(cmbPopUp.Text.Trim());
                    }

                    dataGridView2.DataSource = null;
                    BindCreditLimitOpeningColumn();
                    AdjustCreditLimitColumnOrder();
                    var _atr = CommanHelper.GetCreditLimit(cmbPopUp.Text.Trim());
                    dataGridView2.DataSource = CommanHelper.GetCreditLimit(cmbPopUp.Text.Trim());
                    this.dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.ClearSelection();
                    dataGridView2.ClearSelection();

                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void GetLabroursRate(String _StrPartyName)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Format(DateFrom,'MM/dd/yyyy') AS DateFrom,Format(DateTo,'MM/dd/YYYY') AS DateTo,WeightPcs,Category,Product,Fine_Amount,ROUND(LaboursRate,2) AS LaboursRate,PayType,JamaNaam From LaboursRate Where PartyName = '" + _StrPartyName + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            int _Sno = 0;
            dataGridView_LabourRate.Visible = true;
            while (dr.Read())
            {
                dataGridView_LabourRate.Rows.Add();
                dataGridView_LabourRate.Rows[_Sno].Cells[0].Value = Conversion.ConToDT(dr["DateFrom"].ToString());
                dataGridView_LabourRate.Rows[_Sno].Cells[1].Value = Conversion.ConToDT(dr["DateTo"].ToString());
                dataGridView_LabourRate.Rows[_Sno].Cells[2].Value = dr["WeightPcs"].ToString();
                DataGridViewComboBoxCell cmbCat = (DataGridViewComboBoxCell)dataGridView_LabourRate.Rows[_Sno].Cells[3];
                cmbCat.Dispose();
                if (dr["WeightPcs"].ToString().Trim() == "PCS")
                {
                    this.dataGridView_LabourRate[4, _Sno] = new DataGridViewTextBoxCell();
                }
                else
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_LabourRate.Rows[_Sno].Cells[4];
                    cmbProduct.Dispose();
                    if (dr["Category"].ToString().Trim() == "" || dr["Category"].ToString().Trim() == "COMMON")
                    {
                        cmbProduct.DataSource = CommanHelper.GetProduct().Select(x => x.ProductName).Distinct().ToList();
                    }
                    else
                    {
                        cmbProduct.DataSource = CommanHelper.GetProduct().Where(r => r.Category == dr["Category"].ToString().Trim()).Select(x => x.ProductName).Distinct().ToList();
                    }
                }
                if (dr["Category"].ToString().Trim() == "" || dr["Category"].ToString().Trim() == "COMMON")
                {
                    cmbCat.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
                }
                else
                {
                    cmbCat.DataSource = CommanHelper.GetProduct().Where(r => r.Category == dr["Category"].ToString().Trim()).Select(x => x.Category).Distinct().ToList();
                }
                dataGridView_LabourRate.Rows[_Sno].Cells[3].Value = dr["Category"].ToString();
                dataGridView_LabourRate.Rows[_Sno].Cells[4].Value = dr["Product"].ToString();
                dataGridView_LabourRate.Rows[_Sno].Cells[5].Value = dr["Fine_Amount"].ToString();
                dataGridView_LabourRate.Rows[_Sno].Cells[6].Value = dr["LaboursRate"].ToString();
                dataGridView_LabourRate.Rows[_Sno].Cells[7].Value = dr["PayType"].ToString();
                dataGridView_LabourRate.Rows[_Sno].Cells[8].Value = dr["JamaNaam"].ToString();
                _Sno++;
            }
            con.Close();
        }

        private void GetGhattakList(String _StrPartyName)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Format(DateFrom,'MM/dd/yyyy') AS DateFrom,Format(DateTo,'MM/dd/yyyy') AS DateTo,WeightPcs,Category,Product,ROUND(Ghattak,2) AS Ghattak,PayType,Jama_Naam From GhattakList Where PartyName = '" + _StrPartyName + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            int _Sno = 0;
            dataGridView_GhattakList.Rows.Clear();
            dataGridView_GhattakList.Visible = true;
            while (dr.Read())
            {
                dataGridView_GhattakList.Rows.Add();
                dataGridView_GhattakList.Rows[_Sno].Cells[0].Value = Conversion.ConToDT(dr["DateFrom"].ToString());
                dataGridView_GhattakList.Rows[_Sno].Cells[1].Value = Conversion.ConToDT(dr["DateTo"].ToString());
                dataGridView_GhattakList.Rows[_Sno].Cells[2].Value = dr["WeightPcs"].ToString();
                DataGridViewComboBoxCell cmbCat = (DataGridViewComboBoxCell)dataGridView_GhattakList.Rows[_Sno].Cells[3];
                cmbCat.Dispose();
                if (dr["WeightPcs"].ToString().Trim() == "PCS")
                {
                    this.dataGridView_GhattakList[4, _Sno] = new DataGridViewTextBoxCell();
                }
                else
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_GhattakList.Rows[_Sno].Cells[4];
                    cmbProduct.Dispose();
                    if (dr["Category"].ToString().Trim() == "" || dr["Category"].ToString().Trim() == "COMMON")
                    {
                        cmbProduct.DataSource = CommanHelper.GetProduct().Select(x => x.ProductName).Distinct().ToList();
                    }
                    else
                    {
                        cmbProduct.DataSource = CommanHelper.GetProduct().Where(r => r.Category == dr["Category"].ToString().Trim()).Select(x => x.ProductName).Distinct().ToList();
                    }
                }
                if (dr["Category"].ToString().Trim() == "" || dr["Category"].ToString().Trim() == "COMMON")
                {
                    cmbCat.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
                }
                else
                {
                    cmbCat.DataSource = CommanHelper.GetProduct().Where(r => r.Category == dr["Category"].ToString().Trim()).Select(x => x.Category).Distinct().ToList();
                }
                dataGridView_GhattakList.Rows[_Sno].Cells[3].Value = dr["Category"].ToString();
                dataGridView_GhattakList.Rows[_Sno].Cells[4].Value = dr["Product"].ToString();
                dataGridView_GhattakList.Rows[_Sno].Cells[5].Value = dr["Ghattak"].ToString();
                dataGridView_GhattakList.Rows[_Sno].Cells[6].Value = dr["PayType"].ToString();
                dataGridView_GhattakList.Rows[_Sno].Cells[7].Value = dr["Jama_Naam"].ToString();

                _Sno++;
            }
            con.Close();
        }

        private void GetCreditPeriod(String _StrPartyName)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Format(DateFrom,'MM/dd/yyyy') AS DateFrom,Format(DateTo,'MM/dd/yyyy') AS DateTo,RateRevised,Category,Product,Round(Westage,3) AS Westage,Round(Amount,3) AS Amount,Tran_Type,Days From CreditPeriod Where PartyName = '" + _StrPartyName + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            int _Sno = 0;
            dataGridViewCreditPeriod.Rows.Clear();
            dataGridViewCreditPeriod.Visible = true;
            while (dr.Read())
            {
                dataGridViewCreditPeriod.Rows.Add();
                dataGridViewCreditPeriod.Rows[_Sno].Cells[0].Value = Conversion.ConToDT(dr["DateFrom"].ToString());
                dataGridViewCreditPeriod.Rows[_Sno].Cells[1].Value = Conversion.ConToDT(dr["DateTo"].ToString());
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

        private void GetBrokerage(String _StrPartyName)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Format(DateFrom,'MM/dd/yyyy') AS DateFrom,Format(DateTo,'MM/dd/yyyy') AS DateTo,BrokerageType,Category,Product,BrokerageRate,TranType,LotSet,PayType,Company,UserId From BrokerageSetting Where PartyName = '" + _StrPartyName + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            int _Sno = 0;
            dataGridView_BrokerageSetting.Rows.Clear();
            while (dr.Read())
            {
                dataGridView_BrokerageSetting.Rows.Add();
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[0].Value = Conversion.ConToDT(dr["DateFrom"].ToString());
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[1].Value = Conversion.ConToDT(dr["DateTo"].ToString());
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[2].Value = dr["BrokerageType"].ToString();
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[3].Value = dr["Product"].ToString();
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[4].Value = dr["BrokerageRate"].ToString();
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[5].Value = dr["TranType"].ToString();
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[6].Value = dr["LotSet"].ToString();
                dataGridView_BrokerageSetting.Rows[_Sno].Cells[7].Value = dr["PayType"].ToString();
                _Sno++;
            }
            con.Close();
        }


        private void GetCommission(String _StrPartyName)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select Format(DateFrom,'MM/dd/yyyy') AS DateFrom,Format(DateTo,'MM/dd/yyyy') AS DateTo,WeightPcs,Category,Product,Fine_Amount,ROUND(BrokerageRate,2) AS BrokerageRate,PayType,JamaNaam,Company,UserId From CommissionList Where PartyName = '" + _StrPartyName + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            int _Sno = 0;
            dataGridView_Commission.Rows.Clear();
            dataGridView_Commission.Rows[0].Cells[0].Value = Conversion.ConToDT(CommanHelper.FDate);
            dataGridView_Commission.Rows[0].Cells[1].Value = Conversion.ConToDT(CommanHelper.TDate);
            while (dr.Read())
            {
                dataGridView_Commission.Rows.Add();
                dataGridView_Commission.Rows[_Sno].Cells[0].Value = Conversion.ConToDT(dr["DateFrom"].ToString());
                dataGridView_Commission.Rows[_Sno].Cells[1].Value = Conversion.ConToDT(dr["DateTo"].ToString());
                dataGridView_Commission.Rows[_Sno].Cells[2].Value = dr["WeightPcs"].ToString();

                DataGridViewComboBoxCell cmbCat = (DataGridViewComboBoxCell)dataGridView_Commission.Rows[_Sno].Cells[3];
                cmbCat.Dispose();
                if (dr["WeightPcs"].ToString().Trim() == "PCS")
                {
                    this.dataGridView_Commission[4, _Sno] = new DataGridViewTextBoxCell();
                }
                else
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_Commission.Rows[_Sno].Cells[4];
                    cmbProduct.Dispose();
                    if (cmbCategory.Text.Trim() == "" || cmbCategory.Text.Trim() == "COMMON")
                    {
                        cmbProduct.DataSource = CommanHelper.GetProduct().Select(x => x.ProductName).Distinct().ToList();
                    }
                    else
                    {
                        cmbProduct.DataSource = CommanHelper.GetProduct().Where(r => r.Category == dr["Category"].ToString().Trim()).Select(x => x.ProductName).Distinct().ToList();
                    }
                }
                if (cmbCategory.Text == "" || cmbCategory.Text == "COMMON")
                {
                    cmbCat.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
                }
                else
                {
                    cmbCat.DataSource = CommanHelper.GetProduct().Where(r => r.Category == cmbCategory.Text.Trim()).Select(x => x.Category).Distinct().ToList();
                }
                dataGridView_Commission.Rows[_Sno].Cells[3].Value = dr["Category"].ToString();
                dataGridView_Commission.Rows[_Sno].Cells[4].Value = dr["Product"].ToString();
                dataGridView_Commission.Rows[_Sno].Cells[5].Value = dr["Fine_Amount"].ToString();
                dataGridView_Commission.Rows[_Sno].Cells[6].Value = dr["BrokerageRate"].ToString();
                dataGridView_Commission.Rows[_Sno].Cells[7].Value = dr["PayType"].ToString();
                dataGridView_Commission.Rows[_Sno].Cells[8].Value = dr["JamaNaam"].ToString();
                _Sno++;
            }
            con.Close();
        }

        private void cmbLot_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                Panel_lot.TabIndex = 4;
                cmbLot.TabIndex = 5;
                if (cmbLot.Text.Trim() == "YES")
                {
                    Panel_LotGenerate.TabIndex = 6;
                    cmb_gen_type.TabIndex = 7;
                }
                if (cmbLot.Text.Trim() == "NO")
                {

                    Panel_Category.TabIndex = 6;
                    cmbCategory.TabIndex = 7;
                }


                #endregion

                #region Set Tab StopValue


                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_lot.TabStop = true;
                cmbLot.TabStop = true;
                if (cmbLot.Text.Trim() == "YES")
                {
                    Panel_LotGenerate.TabStop = true;
                    cmb_gen_type.TabStop = true;
                }
                if (cmbLot.Text.Trim() == "NO")
                {
                    Panel_Category.TabStop = true;
                    cmbCategory.TabStop = true;
                }
                #endregion


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
            cmbShowtrail.BackColor = Color.Cyan;
            Panel_ShowInTrail.BackColor = Color.Red;
        }

        private void cmbShowtrail_Leave(object sender, EventArgs e)
        {
            //Control Back Color
            cmbShowtrail.BackColor = Color.White;
            Panel_ShowInTrail.BackColor = Color.Transparent;
        }

        private void cmbShowtrail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (cmbtype.Text.Trim() != "")
                    {
                        if (cmbtype.Text == "PARTY" || cmbtype.Text.Trim() == "OTHER")
                            cmbCategory.Focus();
                        else
                            cmbLot.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void dataGridViewCreditPeriod_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dataGridViewCreditPeriod.CurrentCellAddress.X == oCreditPeriodEntity.col_Matltype_CreditPeriod.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridViewCreditPeriod.CurrentRow.Cells[4];
                    if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                        CommanHelper.GetProduct(cmbProduct);
                    else
                        CommanHelper.GetProductCategoryWise(cmbProduct, (dataGridViewCreditPeriod.Rows[e.RowIndex].Cells[3].Value ?? (object)"").ToString().Trim());
                }
                if (dataGridViewCreditPeriod.CurrentCellAddress.X == oCreditPeriodEntity.col_Matltype_CreditPeriod.DisplayIndex)
                {
                    if (dataGridViewCreditPeriod.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                    {
                        DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridViewCreditPeriod.CurrentRow.Cells[4];
                        cmbProduct.FlatStyle = FlatStyle.Popup;
                        if ((cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "") && (e.FormattedValue ?? (object)"").ToString() == "")
                        {
                            CommanHelper.GetProduct(cmbProduct);
                        }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(cmbProduct, (e.FormattedValue ?? (object)"").ToString());
                        }
                    }
                }
                if (e.ColumnIndex == 8)
                {
                    if (e.FormattedValue.ToString() == "")
                        e.Cancel = true;
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
                if (dataGridViewCreditPeriod.CurrentCell.ColumnIndex == oCreditPeriodEntity.col_Westage_CreditPeriod.Index || dataGridViewCreditPeriod.CurrentCell.ColumnIndex == oCreditPeriodEntity.col_Amount_CreditPeriod.Index || dataGridViewCreditPeriod.CurrentCell.ColumnIndex == oCreditPeriodEntity.col_Days_CreditPeriod.Index)
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
            try
            {
                if (rateupdate_radio.Checked == true)
                {
                    if (e.KeyChar == 13)
                    {
                        this.dataGridViewCreditPeriod.CurrentCell = this.dataGridViewCreditPeriod[0, 0];
                        dataGridViewCreditPeriod.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
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
                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                    if (rateupdate_radio.Checked == true)
                    {
                        dataGridViewCreditPeriod.TabIndex = rateupdate_radio_N.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    dataGridView1.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabStop = true;
                        cmbDays.TabStop = true;
                        rateupdate_radio.TabStop = true;
                        rateupdate_radio_N.TabStop = true;
                    }
                    if (rateupdate_radio.Checked == true)
                    {
                        dataGridViewCreditPeriod.TabStop = true;
                    }
                }

                #endregion
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
                if (dataGridView_LabourRate.CurrentCellAddress.X == oLaboursRateEntity.col_WtPcs_LabourRate.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbFineAmt = (DataGridViewComboBoxCell)dataGridView_LabourRate.CurrentRow.Cells[5];
                    cmbFineAmt.Items.Clear();
                    int _RowIndex = dataGridView_LabourRate.CurrentCell.RowIndex;
                    if (e.FormattedValue.ToString().Trim().ToUpper() == "PCS")
                    {
                        cmbFineAmt.Items.Add("AMOUNT");
                        dataGridView_LabourRate.CurrentRow.Cells[5].Value = "AMOUNT";
                        if (dataGridView_LabourRate.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                        {
                            this.dataGridView_LabourRate[4, _RowIndex] = new DataGridViewTextBoxCell();
                        }
                    }
                    else
                    {
                        cmbFineAmt.Items.Add("AMOUNT");
                        cmbFineAmt.Items.Add("FINE");
                        dataGridView_LabourRate.CurrentRow.Cells[5].Value = "AMOUNT";
                        if (dataGridView_LabourRate.CurrentRow.Cells[4].GetType().Name == "DataGridViewTextBoxCell")
                        {
                            this.dataGridView_LabourRate[4, _RowIndex] = new DataGridViewComboBoxCell();
                            DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_LabourRate.CurrentRow.Cells[4];
                            cmbProduct.FlatStyle = FlatStyle.Popup;

                            if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                            {
                                CommanHelper.GetProduct(cmbProduct);
                            }
                            else
                            {
                                CommanHelper.GetProductCategoryWise(cmbProduct, (dataGridView_LabourRate.CurrentRow.Cells[3].Value ?? (object)"").ToString().Trim());
                            }
                        }

                    }
                }
                if (dataGridView_LabourRate.CurrentCellAddress.X == oLaboursRateEntity.col_Cate_LabourRate.DisplayIndex)
                {
                    if (dataGridView_LabourRate.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                    {
                        DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_LabourRate.CurrentRow.Cells[4];
                        cmbProduct.FlatStyle = FlatStyle.Popup;

                        if ((cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "") && (e.FormattedValue ?? (object)"").ToString() == "")
                        {
                            CommanHelper.GetProduct(cmbProduct);
                        }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(cmbProduct, (e.FormattedValue ?? (object)"").ToString());
                        }
                    }
                }
                if (e.ColumnIndex == 8)
                    if ((dataGridView_LabourRate.Rows[e.RowIndex].Cells[2].Value ?? (object)"").ToString() != "" &&
                        (dataGridView_LabourRate.Rows[e.RowIndex].Cells[4].Value ?? (object)"").ToString() != "" &&
                        Conversion.ConToDec((dataGridView_LabourRate.Rows[e.RowIndex].Cells[6].Value ?? (object)"").ToString()) != 0 &&
                        (dataGridView_LabourRate.Rows[e.RowIndex].Cells[7].Value ?? (object)"").ToString() != "")
                    {
                        if (e.FormattedValue.ToString() == "")
                            e.Cancel = true;
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
                if (dataGridView_LabourRate.CurrentCell.ColumnIndex == oLaboursRateEntity.col_LRate_LabourRate.Index)
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
                if (dataGridView_GhattakList.CurrentCell.ColumnIndex == oGhattakListEntity.col_Ghattk_GhattakList.Index)
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
                if (dataGridView_GhattakList.CurrentCellAddress.X == oGhattakListEntity.col_WtPcs_GhattakList.DisplayIndex)
                {
                    int _RowIndex = dataGridView_GhattakList.CurrentCell.RowIndex;
                    if (e.FormattedValue.ToString().Trim().ToUpper() == "PCS")
                    {
                        if (dataGridView_GhattakList.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                        {
                            this.dataGridView_GhattakList[4, _RowIndex] = new DataGridViewTextBoxCell();
                        }
                    }
                    else
                    {
                        if (dataGridView_GhattakList.CurrentRow.Cells[4].GetType().Name == "DataGridViewTextBoxCell")
                        {
                            this.dataGridView_GhattakList[4, _RowIndex] = new DataGridViewComboBoxCell();
                            DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_GhattakList.CurrentRow.Cells[4];
                            cmbProduct.FlatStyle = FlatStyle.Popup;

                            if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                            {
                                CommanHelper.GetProduct(cmbProduct);
                            }
                            else
                            {
                                CommanHelper.GetProductCategoryWise(cmbProduct, (dataGridView_GhattakList.CurrentRow.Cells[3].Value ?? (object)"").ToString().Trim());
                            }
                        }
                    }
                }
                if (dataGridView_GhattakList.CurrentCellAddress.X == oGhattakListEntity.col_Cate_GhattakList.DisplayIndex)
                {
                    if (dataGridView_GhattakList.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                    {
                        DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_GhattakList.CurrentRow.Cells[4];
                        cmbProduct.FlatStyle = FlatStyle.Popup;

                        if ((cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "") && (e.FormattedValue ?? (object)"").ToString() == "")
                        {
                            CommanHelper.GetProduct(cmbProduct);
                        }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(cmbProduct, (e.FormattedValue ?? (object)"").ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void dataGridView_Commission_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView_Commission_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dataGridView_Commission.CurrentCellAddress.X == oCommissionListEntity.col_WtPcs_CommList.DisplayIndex)
                {
                    DataGridViewComboBoxCell cmbFineAmt = (DataGridViewComboBoxCell)dataGridView_Commission.CurrentRow.Cells[5];
                    cmbFineAmt.Items.Clear();
                    int _RowIndex = dataGridView_Commission.CurrentCell.RowIndex;
                    if (e.FormattedValue.ToString().Trim().ToUpper() == "PCS")
                    {
                        cmbFineAmt.Items.Add("AMOUNT");
                        dataGridView_Commission.CurrentRow.Cells[5].Value = "AMOUNT";
                        if (dataGridView_Commission.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                        {
                            this.dataGridView_Commission[4, _RowIndex] = new DataGridViewTextBoxCell();
                        }
                    }
                    else
                    {
                        cmbFineAmt.Items.Add("AMOUNT");
                        cmbFineAmt.Items.Add("FINE");
                        dataGridView_Commission.CurrentRow.Cells[5].Value = "AMOUNT";

                        if (dataGridView_Commission.CurrentRow.Cells[4].GetType().Name == "DataGridViewTextBoxCell")
                        {
                            this.dataGridView_Commission[4, _RowIndex] = new DataGridViewComboBoxCell();
                            DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_Commission.CurrentRow.Cells[4];
                            cmbProduct.FlatStyle = FlatStyle.Popup;

                            if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                            {
                                CommanHelper.GetProduct(cmbProduct);
                            }
                            else
                            {
                                CommanHelper.GetProductCategoryWise(cmbProduct, (dataGridView_Commission.CurrentRow.Cells[3].Value ?? (object)"").ToString().Trim());
                            }
                        }
                    }
                }
                if (dataGridView_Commission.CurrentCellAddress.X == oCommissionListEntity.col_Cate_CommList.DisplayIndex)
                {
                    if (dataGridView_Commission.CurrentRow.Cells[4].GetType().Name == "DataGridViewComboBoxCell")
                    {
                        DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView_Commission.CurrentRow.Cells[4];
                        cmbProduct.FlatStyle = FlatStyle.Popup;

                        if ((cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "") && (e.FormattedValue ?? (object)"").ToString() == "")
                        {
                            CommanHelper.GetProduct(cmbProduct);
                        }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(cmbProduct, (e.FormattedValue ?? (object)"").ToString());
                        }
                    }
                }
                if (e.ColumnIndex == 8)
                {
                    if ((dataGridView_Commission.Rows[e.RowIndex].Cells[2].Value ?? (object)"").ToString() != "" &&
                          Conversion.ConToDec((dataGridView_Commission.Rows[e.RowIndex].Cells[6].Value ?? (object)"").ToString()) != 0 &&
                          (dataGridView_Commission.Rows[e.RowIndex].Cells[7].Value ?? (object)"").ToString() != "")
                    {
                        if (e.FormattedValue.ToString() == "")
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void dataGridView_Commission_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView_Commission.CurrentCell.ColumnIndex == oCommissionListEntity.col_Com_CommList.Index)
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

        private void dataGridView_BrokerageSetting_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView_BrokerageSetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView_BrokerageSetting.CurrentCell.ColumnIndex == oBrokerageSettingEntity.col_BrokRate_Brok.Index)
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


        private void txtopnarr_KeyPress(object sender, KeyPressEventArgs e)
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
                        btnSave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void rateupdate_radio_Enter(object sender, EventArgs e)
        {
            rateupdate_radio.BackColor = Color.Red;
        }

        private void rateupdate_radio_Leave(object sender, EventArgs e)
        {
            rateupdate_radio.BackColor = Color.Transparent;
        }

        private void rateupdate_radio_N_Enter(object sender, EventArgs e)
        {
            rateupdate_radio_N.BackColor = Color.Red;
        }

        private void rateupdate_radio_N_Leave(object sender, EventArgs e)
        {
            rateupdate_radio_N.BackColor = Color.Transparent;
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                string headerText =
                    dataGridView2.Columns[e.ColumnIndex].HeaderText;
                //if (e.RowIndex == this.dataGridView2.Rows.Count - 1 && e.ColumnIndex == 2)
                //{
                //    dataGridView2.ClearSelection();
                //    this.dataGridView1.CurrentCell = this.dataGridView1[1, 0];
                //    dataGridView1.Focus();
                //    //dataGridView2.Rows[e.RowIndex].ErrorText = "the value must be a non-negative integer";
                //}


            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (this.dataGridView1.CurrentCellAddress.X == col_ProductItem.DisplayIndex)
                    {
                        DataGridViewComboBoxCell cmbProduct = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[1];
                        cmbProduct.FlatStyle = FlatStyle.Popup;
                        if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                            CommanHelper.GetProduct_Worker(cmbProduct);
                        else
                            CommanHelper.GetProductCategoryWise_Worker(cmbProduct, cmbCategory.Text.Trim());
                        dataGridView1.CurrentRow.Cells[1].Value = e.FormattedValue.ToString();
                    }

                    // Check Duplicate Entry
                    if (e.ColumnIndex == 1)
                    {
                        for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                        {
                            if (dataGridView1.Rows[row].Cells[1].Value != null && row != e.RowIndex && dataGridView1.Rows[row].Cells[1].Value.Equals(e.FormattedValue))
                            {
                                MessageBox.Show(e.FormattedValue + " Already in the grid !", "Worker Opening", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                e.Cancel = true;
                            }
                        }
                    }
                    if (e.ColumnIndex == 3)
                    {
                        if ((dataGridView1.Rows[e.RowIndex].Cells[1].Value ?? (object)"").ToString() == "" && Conversion.ConToDec((dataGridView1.Rows[e.RowIndex].Cells[2].Value ?? (object)"").ToString()) == 0 && (dataGridView1.Rows[e.RowIndex].Cells[3].Value ?? (object)"").ToString() == "")
                        {
                            this.dataGridView1.ClearSelection();
                            btnSave.Focus();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void cmbsubhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblBankCredit.Visible = false;
                txtBankCredit.Visible = false;
                if (cmbgrouphead.Text.Trim() == "CURRENT ASSETS" && cmbsubhead.Text.Trim() == "BANK OD/CC")
                {
                    lblBankCredit.Visible = true;
                    txtBankCredit.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void rateupdate_radio_N_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (rateupdate_radio_N.Checked == true)
                {
                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridViewCreditPeriod_Enter(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                    if (rateupdate_radio.Checked == true)
                    {
                        dataGridViewCreditPeriod.TabIndex = rateupdate_radio_N.TabIndex + 1;
                    }
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                else
                {
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        groupBox_LabourRate.TabStop = true;
                        dataGridView_LabourRate.TabStop = true;
                        groupBox_GhattakList.TabStop = true;
                        dataGridView_GhattakList.TabStop = true;
                        dataGridView1.TabStop = true;
                        btnSave.TabStop = true;
                    }
                    else
                    {
                        if (cmbBullion.Text.Trim() == "MCX")
                        {
                            groupBox_BrokerageSetting.TabStop = true;
                            dataGridView_BrokerageSetting.TabStop = true;
                        }
                        grpPartyCrditL.TabStop = true;
                        dataGridView2.TabStop = true;
                        dataGridView1.TabStop = true;
                        grpBoxWithCreditLimit.TabStop = true;
                        chkWithCreditLimit.TabStop = true;
                        if (chkWithCreditLimit.Checked == true)
                        {
                            groBoxCreditPeriod.TabStop = true;
                            cmbDays.TabStop = true;
                            rateupdate_radio.TabStop = true;
                            rateupdate_radio_N.TabStop = true;
                        }
                        if (rateupdate_radio.Checked == true)
                        {
                            dataGridViewCreditPeriod.TabStop = true;
                        }
                        btnSave.TabStop = true;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void chkWithCreditLimit_Enter(object sender, EventArgs e)
        {
            try
            {
                chkWithCreditLimit.BackColor = Color.Red;

                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                    btnSave.TabIndex = chkWithCreditLimit.TabIndex + 1;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    dataGridView1.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabStop = true;
                        cmbDays.TabStop = true;
                        rateupdate_radio.TabStop = true;
                        rateupdate_radio_N.TabStop = true;
                    }
                    btnSave.TabStop = true;
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void chkWithCreditLimit_Leave(object sender, EventArgs e)
        {
            try
            {
                chkWithCreditLimit.BackColor = Color.Transparent;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbBullion.Text.Trim().ToUpper() == "MCX")
                {
                    groupBox_BrokerageSetting.Visible = true;
                    this.groupBox_BrokerageSetting.Location = new System.Drawing.Point(692, 175);
                }
                else
                {
                    groupBox_BrokerageSetting.Visible = false;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    dataGridView1.ClearSelection();
                    groupBox_LabourRate.Visible = true;
                    dataGridView_LabourRate.Visible = true;
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void toolStripMenuItem_Product_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (this.dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        _ProductCreate_GridFlage = 5;
                    }
                    if (this.dataGridView_LabourRate.CurrentCell.ColumnIndex == 4)
                    {
                        _ProductCreate_GridFlage = 2;
                    }
                    if (this.dataGridView_GhattakList.CurrentCell.ColumnIndex == 4)
                    {
                        _ProductCreate_GridFlage = 3;
                    }
                }
                if (cmbtype.Text.Trim() == "PARTY")
                {
                    if (this.dataGridViewCreditPeriod.CurrentCell.ColumnIndex == 4)
                    {
                        _ProductCreate_GridFlage = 1;
                    }
                    if (this.dataGridView_BrokerageSetting.CurrentCell.ColumnIndex == 4)
                    {
                        _ProductCreate_GridFlage = 4;
                    }
                }
                if (_ProductCreate_GridFlage != 0)
                {
                    _Category = cmbCategory.Text.Trim();
                    _ProductFlage = 1;
                    ProductDetails oProductDetails = new ProductDetails();
                    oProductDetails.MdiParent = Master.objMaster;
                    oProductDetails.Show();
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void PartyInformation_Activated(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (_ProductName != "")
                    {
                        DataGridViewComboBoxCell t2 = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[1];
                        if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                            CommanHelper.GetProduct_Worker(t2);
                        else
                            CommanHelper.GetProductCategoryWise_Worker(t2, cmbCategory.Text.Trim());


                        DataGridViewComboBoxCell col_Product_LabourRate = (DataGridViewComboBoxCell)dataGridView_LabourRate.CurrentRow.Cells[4];
                        if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                        { CommanHelper.GetProduct(col_Product_LabourRate); }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(col_Product_LabourRate, cmbCategory.Text.Trim());
                        }

                        DataGridViewComboBoxCell col_Product_GhattakList = (DataGridViewComboBoxCell)dataGridView_GhattakList.CurrentRow.Cells[4];
                        if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                        {
                            CommanHelper.GetProduct(col_Product_GhattakList);
                        }
                        else
                        { CommanHelper.GetProductCategoryWise(col_Product_GhattakList, cmbCategory.Text.Trim()); }

                        if (_ProductCreate_GridFlage == 2)
                        {
                            dataGridView_LabourRate.CurrentRow.Cells[4].Value = _ProductName;
                        }
                        if (_ProductCreate_GridFlage == 3)
                        {
                            dataGridView_GhattakList.CurrentRow.Cells[4].Value = _ProductName;
                        }

                        if (_ProductCreate_GridFlage == 5)
                        {
                            dataGridView1.CurrentRow.Cells[1].Value = _ProductName;
                            dataGridView1.Focus(); return;
                        }
                    }
                }
                if (cmbtype.Text.Trim() == "PARTY")
                {
                    if (_ProductName != "")
                    {
                        DataGridViewComboBoxCell col_Product_CreditPeriod = (DataGridViewComboBoxCell)dataGridViewCreditPeriod.CurrentRow.Cells[4];
                        if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                        { CommanHelper.GetProduct(col_Product_CreditPeriod); }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(col_Product_CreditPeriod, cmbCategory.Text.Trim());
                        }
                        DataGridViewComboBoxCell col_Product_Commission = (DataGridViewComboBoxCell)dataGridView_Commission.CurrentRow.Cells[4];
                        if (cmbCategory.Text.Trim() == "COMMON" || cmbCategory.Text.Trim() == "")
                        { CommanHelper.GetProduct(col_Product_Commission); }
                        else
                        {
                            CommanHelper.GetProductCategoryWise(col_Product_Commission, cmbCategory.Text.Trim());
                        }
                        if (_ProductCreate_GridFlage == 1)
                        {
                            dataGridViewCreditPeriod.CurrentRow.Cells[4].Value = _ProductName;
                        }
                        if (_ProductCreate_GridFlage == 4)
                        {
                            dataGridView_Commission.CurrentRow.Cells[4].Value = _ProductName;
                        }
                    }
                }
                _ProductCreate_GridFlage = 0;
                _ProductName = "";
                _ProductFlage = 0;
                _Category = "";
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }


        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    dataGridView1.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (dataGridView1.CurrentCellAddress.X == 1)
                    {
                        ComboBox cb = e.Control as ComboBox;
                        if (cb != null)
                        {
                            cb.DropDownStyle = ComboBoxStyle.DropDown;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtBankCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) btnSave.Focus();
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_GhattakList_Enter(object sender, EventArgs e)
        {
            try
            {
                groupBox_GhattakList.BackColor = Color.Silver;


                //Set Tab Setting 
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;

                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    {
                        groupBox_LabourRate.TabIndex = dataGridView_Commission.TabIndex + 1;
                    }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;
                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        { groupBox_BrokerageSetting.TabIndex = dataGridView_Commission.TabIndex + 1; }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        } dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                else
                {
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        groupBox_LabourRate.TabStop = true;
                        dataGridView_LabourRate.TabStop = true;
                        groupBox_GhattakList.TabStop = true;
                        dataGridView_GhattakList.TabStop = true;
                        dataGridView1.TabStop = true;
                    }
                    else
                    {
                        if (cmbBullion.Text.Trim() == "MCX")
                        {
                            groupBox_BrokerageSetting.TabStop = true;
                            dataGridView_BrokerageSetting.TabStop = true;
                        }
                        else
                        {
                            grpPartyCrditL.TabStop = true;
                            dataGridView2.TabStop = true;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_LabourRate_Enter(object sender, EventArgs e)
        {
            try
            {
                groupBox_LabourRate.BackColor = Color.Silver;

                //Set Tab Setting 
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;

                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = dataGridView_Commission.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        { groupBox_BrokerageSetting.TabIndex = dataGridView_Commission.TabIndex + 1; }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        { grpPartyCrditL.TabIndex = dataGridView_Commission.TabIndex + 1; }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    else
                    {
                        grpPartyCrditL.TabStop = true;
                        dataGridView2.TabStop = true;
                    }
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }


        private void cmb_gen_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                Panel_lot.TabIndex = 4;
                cmbLot.TabIndex = 5;
                Panel_LotGenerate.TabIndex = 6;
                cmb_gen_type.TabIndex = 7;
                if (cmb_gen_type.Text.Trim() != "")
                {
                    Panel_Category.TabIndex = 8;
                    cmbCategory.TabIndex = 9;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_lot.TabStop = true;
                cmbLot.TabStop = true;
                Panel_LotGenerate.TabStop = true;
                cmb_gen_type.TabStop = true;

                if (cmb_gen_type.Text.Trim() != "")
                {
                    Panel_Category.TabStop = true;
                    cmbCategory.TabStop = true;
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void cmbIntroducer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting 
                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;

                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    {
                        groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                        dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        groupBox_LabourRate.TabIndex = dataGridView_Commission.TabIndex + 1;
                    }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                            dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                            groupBox_BrokerageSetting.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                            dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                            grpPartyCrditL.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                }


                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    else
                    {
                        grpPartyCrditL.TabStop = true;
                        dataGridView2.TabStop = true;
                    }
                }

                #endregion

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_LabourRate_Leave(object sender, EventArgs e)
        {
            try
            {
                groupBox_LabourRate.BackColor = Color.Transparent;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_GhattakList_Leave(object sender, EventArgs e)
        {
            try
            {
                groupBox_GhattakList.BackColor = Color.Transparent;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_Commission_Enter(object sender, EventArgs e)
        {
            try
            {
                groupBox_CommissionList.BackColor = Color.Silver;

                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    {
                        groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1;
                    }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }

                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;
                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = dataGridView_Commission.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }

                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                }



                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                else
                {
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        groupBox_LabourRate.TabStop = true;
                        dataGridView_LabourRate.TabStop = true;
                        groupBox_GhattakList.TabStop = true;
                        dataGridView_GhattakList.TabStop = true;
                        dataGridView1.TabStop = true;
                        btnSave.TabStop = true;
                    }
                    else
                    {
                        if (cmbBullion.Text.Trim() == "MCX")
                        {
                            groupBox_BrokerageSetting.TabStop = true;
                            dataGridView_BrokerageSetting.TabStop = true;
                            grpPartyCrditL.TabStop = true;
                            dataGridView2.TabStop = true;
                        }
                        else
                        {
                            grpPartyCrditL.TabStop = true;
                            dataGridView2.TabStop = true;
                        }
                    }
                }

                #endregion

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_Commission_Leave(object sender, EventArgs e)
        {
            try
            {
                groupBox_CommissionList.BackColor = Color.Transparent;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_BrokerageSetting_Enter(object sender, EventArgs e)
        {
            try
            {
                groupBox_BrokerageSetting.BackColor = Color.Silver;

                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }

                if (cmbBullion.Text.Trim() == "MCX")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                }
                else
                {
                    if (cmbIntroducer.Text != "")
                    {
                        grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                    }
                    else
                    {
                        grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                else
                {

                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    else
                    {
                        grpPartyCrditL.TabStop = true;
                        dataGridView2.TabStop = true;
                    }
                }

                #endregion

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_BrokerageSetting_Leave(object sender, EventArgs e)
        {
            try
            {
                groupBox_BrokerageSetting.BackColor = Color.Transparent;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            try
            {
                grpPartyCrditL.BackColor = Color.Silver;
                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        { groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                        dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    }
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                else
                {
                    if (cmbtype.Text.Trim() == "WORKER")
                    {
                        groupBox_LabourRate.TabStop = true;
                        dataGridView_LabourRate.TabStop = true;
                        groupBox_GhattakList.TabStop = true;
                        dataGridView_GhattakList.TabStop = true;
                        dataGridView1.TabStop = true;
                        btnSave.TabStop = true;
                    }
                    else
                    {
                        if (cmbBullion.Text.Trim() == "MCX")
                        {
                            groupBox_BrokerageSetting.TabStop = true;
                            dataGridView_BrokerageSetting.TabStop = true;
                        }
                        else
                        {
                            grpPartyCrditL.TabStop = true;
                            dataGridView2.TabStop = true;
                        }
                    }
                }

                #endregion

            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView2_Leave(object sender, EventArgs e)
        {
            try
            {
                grpPartyCrditL.BackColor = Color.Transparent;
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    {
                        groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1;
                    }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;
                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {
            try
            {
                //Set Tab Setting 

                TabIndex_Default();
                TabStopSetting_False();

                #region Tab Index Setting

                Panel_ShowInTrail.TabIndex = 2;
                cmbShowtrail.TabIndex = 3;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabIndex = 4;
                    cmbLot.TabIndex = 5;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabIndex = 6;
                        cmb_gen_type.TabIndex = 7;
                        Panel_Category.TabIndex = 8;
                        cmbCategory.TabIndex = 9;
                    }
                    if (cmbLot.Text.Trim() == "NO")
                    {
                        Panel_Category.TabIndex = 6;
                        cmbCategory.TabIndex = 7;
                    }
                }
                else
                {
                    Panel_Category.TabIndex = 4;
                    cmbCategory.TabIndex = 5;
                }

                int _TabIndex = 0;
                txtPartyName.TabIndex = cmbCategory.TabIndex + 1;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    _TabIndex = txtPartyName.TabIndex + 1;
                }
                else
                {
                    Panel_McxBullion.TabIndex = txtPartyName.TabIndex + 1;
                    cmbBullion.TabIndex = Panel_McxBullion.TabIndex + 1;
                    _TabIndex = cmbBullion.TabIndex + 1;
                }
                txtaddress.TabIndex = _TabIndex;
                txtcontactno.TabIndex = txtaddress.TabIndex + 1;
                txtemailid.TabIndex = txtcontactno.TabIndex + 1;
                cmbgrouphead.TabIndex = txtemailid.TabIndex + 1;
                cmbsubhead.TabIndex = cmbgrouphead.TabIndex + 1;
                cmbIntroducer.TabIndex = cmbsubhead.TabIndex + 1;
                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabIndex = cmbIntroducer.TabIndex + 1;
                    dataGridView_Commission.TabIndex = groupBox_CommissionList.TabIndex + 1;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    if (cmbIntroducer.Text != "")
                    { groupBox_LabourRate.TabIndex = groupBox_CommissionList.TabIndex + 1; }
                    else
                    {
                        groupBox_LabourRate.TabIndex = cmbIntroducer.TabIndex + 1;
                    }
                    dataGridView_LabourRate.TabIndex = groupBox_LabourRate.TabIndex + 1;

                    groupBox_GhattakList.TabIndex = dataGridView_LabourRate.TabIndex + 1;
                    dataGridView_GhattakList.TabIndex = groupBox_GhattakList.TabIndex + 1;

                    dataGridView1.TabIndex = dataGridView_GhattakList.TabIndex + 1;
                    btnSave.TabIndex = dataGridView1.TabIndex + 1;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            groupBox_BrokerageSetting.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            groupBox_BrokerageSetting.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView_BrokerageSetting.TabIndex = groupBox_BrokerageSetting.TabIndex + 1;
                        grpPartyCrditL.TabIndex = dataGridView_BrokerageSetting.TabIndex + 1;
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    else
                    {
                        if (cmbIntroducer.Text != "")
                        {
                            grpPartyCrditL.TabIndex = groupBox_CommissionList.TabIndex + 1;
                        }
                        else
                        {
                            grpPartyCrditL.TabIndex = cmbIntroducer.TabIndex + 1;
                        }
                        dataGridView2.TabIndex = grpPartyCrditL.TabIndex + 1;
                    }
                    dataGridView1.TabIndex = dataGridView2.TabIndex + 1;
                    grpBoxWithCreditLimit.TabIndex = dataGridView1.TabIndex + 1;
                    chkWithCreditLimit.TabIndex = grpBoxWithCreditLimit.TabIndex + 1;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabIndex = chkWithCreditLimit.TabIndex + 1;
                        cmbDays.TabIndex = groBoxCreditPeriod.TabIndex + 1;
                        rateupdate_radio.TabIndex = cmbDays.TabIndex + 1;
                        rateupdate_radio_N.TabIndex = rateupdate_radio.TabIndex + 1;
                    }
                    btnSave.TabIndex = chkWithCreditLimit.TabIndex + 1;
                }

                #endregion

                #region Set Tab StopValue

                Panel_ShowInTrail.TabStop = true;
                cmbShowtrail.TabStop = true;
                Panel_Category.TabStop = true;
                cmbCategory.TabStop = true;

                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_lot.TabStop = true;
                    cmbLot.TabStop = true;
                    if (cmbLot.Text.Trim() == "YES")
                    {
                        Panel_LotGenerate.TabStop = true;
                        cmb_gen_type.TabStop = true;
                    }
                }
                txtPartyName.TabStop = true;
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    Panel_McxBullion.TabStop = true;
                    cmbBullion.TabStop = true;
                }
                txtaddress.TabStop = true;
                txtcontactno.TabStop = true;
                txtemailid.TabStop = true;
                cmbgrouphead.TabStop = true;
                cmbsubhead.TabStop = true;
                cmbIntroducer.TabStop = true;

                if (cmbIntroducer.Text != "")
                {
                    groupBox_CommissionList.TabStop = true;
                    dataGridView_Commission.TabStop = true;
                }
                if (cmbtype.Text.Trim() == "WORKER")
                {
                    groupBox_LabourRate.TabStop = true;
                    dataGridView_LabourRate.TabStop = true;
                    groupBox_GhattakList.TabStop = true;
                    dataGridView_GhattakList.TabStop = true;
                    dataGridView1.TabStop = true;
                    btnSave.TabStop = true;
                }
                else
                {
                    if (cmbBullion.Text.Trim() == "MCX")
                    {
                        groupBox_BrokerageSetting.TabStop = true;
                        dataGridView_BrokerageSetting.TabStop = true;
                    }
                    grpPartyCrditL.TabStop = true;
                    dataGridView2.TabStop = true;
                    dataGridView1.TabStop = true;
                    grpBoxWithCreditLimit.TabStop = true;
                    chkWithCreditLimit.TabStop = true;
                    if (chkWithCreditLimit.Checked == true)
                    {
                        groBoxCreditPeriod.TabStop = true;
                        cmbDays.TabStop = true;
                        rateupdate_radio.TabStop = true;
                        rateupdate_radio_N.TabStop = true;
                    }
                    btnSave.TabStop = true;
                }

                #endregion
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    int iColumn = dataGridView2.CurrentCell.ColumnIndex;
                    int iRow = dataGridView2.CurrentCell.RowIndex;
                    if (iColumn == dataGridView2.ColumnCount - 1)
                    {
                        if (dataGridView2.RowCount > (iRow + 1))
                        {
                            dataGridView2.CurrentCell = dataGridView2[1, iRow + 1];
                        }
                        else
                        {
                            if (this.dataGridView1.RowCount > 0)
                            {
                                this.dataGridView1.CurrentCell = this.dataGridView1[1, 0];
                            }
                            this.dataGridView1.Focus();
                        }
                    }
                    else
                        dataGridView2.CurrentCell = dataGridView2[iColumn + 1, iRow];
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cmbtype.Text.Trim() != "WORKER")
                    {
                        e.SuppressKeyPress = true;
                        int iColumn = dataGridView1.CurrentCell.ColumnIndex;
                        int iRow = dataGridView1.CurrentCell.RowIndex;
                        if (iColumn == dataGridView1.ColumnCount - 1)
                        {
                            if (dataGridView1.RowCount > (iRow + 1))
                            {
                                dataGridView1.CurrentCell = dataGridView1[1, iRow + 1];
                            }
                            else
                            {
                                this.dataGridView1.ClearSelection();
                                chkWithCreditLimit.Focus();
                            }
                        }
                        else
                            dataGridView1.CurrentCell = dataGridView1[iColumn + 1, iRow];
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_BrokerageSetting_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dataGridView_BrokerageSetting.CurrentCell.ColumnIndex == 4)
                    {
                        if ((dataGridView_BrokerageSetting.CurrentRow.Cells[2].Value ?? (object)"").ToString() == "" && (dataGridView_BrokerageSetting.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                        {
                            this.dataGridView_BrokerageSetting.ClearSelection();
                            if (this.dataGridView2.RowCount > 0)
                            {
                                this.dataGridView2.CurrentCell = this.dataGridView2[1, 0];
                            }
                            this.dataGridView2.Focus();
                        }
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }

        }

        private void dataGridView_LabourRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.dataGridView_LabourRate.CurrentCell.ColumnIndex == 4)
                    {
                        if ((dataGridView_LabourRate.CurrentRow.Cells[2].Value ?? (object)"").ToString() == "" && (dataGridView_LabourRate.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "" && (dataGridView_LabourRate.CurrentRow.Cells[4].Value ?? (object)"").ToString() == "")
                        {
                            groupBox_GhattakList.Visible = true;
                            dataGridView_GhattakList.Visible = true;
                            this.dataGridView_LabourRate.ClearSelection();
                            this.dataGridView_GhattakList.CurrentCell = this.dataGridView_GhattakList[0, 0];
                            dataGridView_GhattakList.Focus();
                        }
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_GhattakList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.dataGridView_GhattakList.CurrentCell.ColumnIndex == 4)
                    {
                        if ((dataGridView_GhattakList.CurrentRow.Cells[2].Value ?? (object)"").ToString() == "" && (dataGridView_GhattakList.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "" && (dataGridView_GhattakList.CurrentRow.Cells[4].Value ?? (object)"").ToString() == "")
                        {
                            this.dataGridView_GhattakList.ClearSelection();
                            if (this.dataGridView1.RowCount > 0)
                            {
                                this.dataGridView1.CurrentCell = this.dataGridView1[1, 0];
                            }

                            this.dataGridView1.Focus();
                        }
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_Commission_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.dataGridView_Commission.CurrentCell.ColumnIndex == 3)
                    {
                        if ((dataGridView_Commission.CurrentRow.Cells[2].Value ?? (object)"").ToString() == "" && (dataGridView_Commission.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                        {
                            this.dataGridView_Commission.ClearSelection();
                            if (cmbtype.Text.Trim() == "WORKER")
                            {
                                groupBox_LabourRate.Visible = true;
                                if (this.dataGridView_LabourRate.RowCount > 0)
                                {
                                    this.dataGridView_LabourRate.CurrentCell = this.dataGridView_LabourRate[0, 0];
                                }
                                this.dataGridView_LabourRate.Focus();
                            }
                            else
                            {
                                if (cmbBullion.Text.Trim() == "MCX")
                                {
                                    if (this.dataGridView_BrokerageSetting.RowCount > 0)
                                    {
                                        this.dataGridView_BrokerageSetting.CurrentCell = this.dataGridView_BrokerageSetting[1, 0];
                                    }
                                    this.dataGridView_BrokerageSetting.Focus();
                                }
                                else
                                {
                                    if (this.dataGridView2.RowCount > 0)
                                    {
                                        this.dataGridView2.CurrentCell = this.dataGridView2[1, 0];
                                    }
                                    dataGridView2.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridViewCreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.dataGridViewCreditPeriod.CurrentCell.ColumnIndex == 4)
                    {
                        if ((dataGridViewCreditPeriod.CurrentRow.Cells[2].Value ?? (object)"").ToString() == "" && (dataGridViewCreditPeriod.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                        {
                            if (txtBankCredit.Visible == true)
                                txtBankCredit.Focus();
                            else
                                btnSave.Focus();
                        }
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_LabourRate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (dataGridView_LabourRate.Rows.Count > 1)
                    {
                        var _NextDateFrom = Conversion.ConToDT((dataGridView_LabourRate.Rows[e.RowIndex].Cells["DateTo"].Value ?? (object)DateTime.Now).ToString()).AddDays(1);
                        dataGridView_LabourRate.Rows[e.RowIndex + 1].Cells["DateFrom"].Value = _NextDateFrom;
                        dataGridView_LabourRate.Rows[e.RowIndex + 1].Cells["DateTo"].Value = Conversion.ConToDT(CommanHelper.TDate);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_GhattakList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (e.RowIndex > 0)
                    {
                        var _NextDateFrom = Conversion.ConToDT((dataGridView_GhattakList.Rows[e.RowIndex].Cells["DateTo"].Value ?? (object)DateTime.Now).ToString()).AddDays(1);
                        dataGridView_GhattakList.Rows[e.RowIndex + 1].Cells["DateFrom"].Value = _NextDateFrom;
                        dataGridView_GhattakList.Rows[e.RowIndex + 1].Cells["DateTo"].Value = Conversion.ConToDT(CommanHelper.TDate);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView_Commission_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (dataGridView_Commission.Rows.Count > 1)
                    {
                        var _NextDateFrom = Conversion.ConToDT((dataGridView_Commission.Rows[e.RowIndex].Cells["DateTo"].Value ?? (object)DateTime.Now).ToString()).AddDays(1);
                        dataGridView_Commission.Rows[e.RowIndex + 1].Cells["DateFrom"].Value = _NextDateFrom;
                        dataGridView_Commission.Rows[e.RowIndex + 1].Cells["DateTo"].Value = Conversion.ConToDT(CommanHelper.TDate);
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridView_BrokerageSetting_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (dataGridView_BrokerageSetting.Rows.Count > 1)
                    {
                        var _NextDateFrom = Conversion.ConToDT((dataGridView_BrokerageSetting.Rows[e.RowIndex].Cells["DateTo"].Value ?? (object)DateTime.Now).ToString()).AddDays(1);
                        dataGridView_BrokerageSetting.Rows[e.RowIndex + 1].Cells["DateFrom"].Value = _NextDateFrom;
                        dataGridView_BrokerageSetting.Rows[e.RowIndex + 1].Cells["DateTo"].Value = Conversion.ConToDT(CommanHelper.TDate);
                    }
                }
            }
            catch (Exception ex) { ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name); }
        }

        private void dataGridViewCreditPeriod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (dataGridView_LabourRate.Rows.Count > 1)
                    {
                        var _NextDateFrom = Conversion.ConToDT((dataGridViewCreditPeriod.Rows[e.RowIndex - 1].Cells["DateTo"].Value ?? (object)DateTime.Now).ToString()).AddDays(1);
                        dataGridViewCreditPeriod.Rows[e.RowIndex + 1].Cells["DateFrom"].Value = _NextDateFrom;
                        dataGridViewCreditPeriod.Rows[e.RowIndex + 1].Cells["DateTo"].Value = Conversion.ConToDT(CommanHelper.TDate);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
