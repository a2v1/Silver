using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.CompanyInfo
{
    public partial class Company : Form
    {
        #region Declare Variables
        OleDbConnection con;
        OleDbTransaction Tran = null;
        String _DPath = "";
        String _DName = "";
        String _CompName = "";
        int int_keyvalue = 0;
        int F_Update = 0;
        String _KfMetalCate = "";
        String _KfMetalName = "";
        String _MetalCateCellValue = "";
        String _MetalNameCellValue = "";
        Boolean _CheckCashExist = false;
        Boolean _Check = false;
        List<KFOpeningEntity> KFOpeningList = new List<KFOpeningEntity>();
        List<CompanyEntity> companyEntity = new List<CompanyEntity>();
        List<MetalEntity> MetalList = new List<MetalEntity>();
        DataGridViewComboBoxColumn col_MCate = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_MNane = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_KF = new DataGridViewComboBoxColumn();
        DataGridViewColumn col_Amt = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn col_Weight = new DataGridViewComboBoxColumn();


        DataGridViewColumn colMetalName = new DataGridViewTextBoxColumn();
        DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col6 = new DataGridViewTextBoxColumn();

        DataGridView.HitTestInfo hti;
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(CommanHelper.NumericCheck);


        #endregion

        public Company()
        {
            InitializeComponent();
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;

            CommanHelper.ChangeGridFormate(dataGridView1);
            CommanHelper.ChangeGridFormate(dataGridView2);
            CommanHelper.ChangeGridFormate(dataGridView3);
        }


        #region Helper

        private void BindCompanyOpeningColumn()
        {

            col_MCate.DataPropertyName = "MetalCategory";
            col_MCate.HeaderText = "Category";
            col_MCate.Name = "MetalCategory";
            col_MCate.FlatStyle = FlatStyle.Popup;
            col_MCate.DataSource = CommanHelper.GetMetalCate(F_Update).Select(x => x.MetalCategory).Distinct().ToList();
            dataGridView1.Columns.Add(col_MCate);


            col_MNane.DataPropertyName = "MetalName";
            col_MNane.HeaderText = "MetalName";
            col_MNane.Name = "MetalName";
            col_MNane.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col_MNane);


            col_Weight.DataPropertyName = "WeightType";
            col_Weight.HeaderText = "Wt/Type";
            col_Weight.Name = "WeightType";
            col_Weight.MaxDropDownItems = 4;
            col_Weight.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col_Weight);


            col_KF.DataPropertyName = "KachchiFine";
            col_KF.HeaderText = "KF";
            col_KF.Name = "KachchiFine";
            col_KF.Items.Clear();
            col_KF.Items.Add("NO");
            col_KF.Items.Add("YES");
            col_KF.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col_KF);

            col_Amt.DataPropertyName = "AmountWeight";
            col_Amt.HeaderText = "Amt/Weight";
            col_Amt.Name = "AmountWeight";
            col_Amt.DefaultCellStyle.Format = "N2";
            dataGridView1.Columns.Add(col_Amt);

            DataGridViewComboBoxColumn col_DrCr = new DataGridViewComboBoxColumn();
            {
                col_DrCr.DataPropertyName = "DrCr";
                col_DrCr.HeaderText = "DrCr";
                col_DrCr.Name = "DrCr";
                col_DrCr.Items.Add("JAMA");
                col_DrCr.Items.Add("NAAM");
                col_DrCr.FlatStyle = FlatStyle.Popup;
                dataGridView1.Columns.Add(col_DrCr);
            }
            DataGridViewColumn col_Sn = new DataGridViewTextBoxColumn();
            {
                col_Sn.DataPropertyName = "Sno";
                col_Sn.HeaderText = "Sno";
                col_Sn.Name = "Sno";
                col_Sn.Visible = false;
                dataGridView1.Columns.Add(col_Sn);
            }
        }

        private void BindKFColumn()
        {


            col1.DataPropertyName = "PaatNo";
            col1.HeaderText = "PaatNo";
            col1.Name = "PaatNo";
            dataGridView2.Columns.Add(col1);

            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Weight";
            col2.Name = "Weight";
            dataGridView2.Columns.Add(col2);

            col3.DataPropertyName = "Tunch1";
            col3.HeaderText = "Tunch1";
            col3.Name = "Tunch1";
            dataGridView2.Columns.Add(col3);

            col4.DataPropertyName = "Tunch2";
            col4.HeaderText = "Tunch2";
            col4.Name = "Tunch2";
            col4.DefaultCellStyle.Format = "N2";
            dataGridView2.Columns.Add(col4);

            col5.DataPropertyName = "Fine";
            col5.HeaderText = "Fine";
            col5.Name = "Fine";
            dataGridView2.Columns.Add(col5);

            col6.DataPropertyName = "Sno";
            col6.HeaderText = "Sno";
            col6.Name = "Sno";
            col6.Visible = false;
            dataGridView2.Columns.Add(col6);

            colMetalName.DataPropertyName = "MetalName";
            colMetalName.HeaderText = "MetalName";
            colMetalName.Name = "MetalName";
            colMetalName.Visible = false;
            dataGridView2.Columns.Add(colMetalName);
        }

        private void Total()
        {

            if (CommanHelper.SumRow(dataGridView2, 4) > 0)
            {
                lblFine.Text = CommanHelper.SumRow(dataGridView2, 4).ToString();
            }
            if (CommanHelper.SumRow(dataGridView2, 1) > 0)
            {
                lblWeight.Text = CommanHelper.SumRow(dataGridView2, 1).ToString();
            }

            int CountSno = 0;
            CountSno = dataGridView2.Rows.Count - 1;
            if (CountSno > 0)
            {
                lblsno.Text = CountSno.ToString();
            }
        }

        private void BindCompanyDetails()
        {
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT CompanyName,Format(DateFrom,'dd/MM/yyyy') AS DateFrom,Format(DateTo,'dd/MM/yyyy') AS DateTo,FinancialYear,DatabasePath,DataBaseName FROM Company", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    CompanyEntity oCompanyEntity = new CompanyEntity();
                    oCompanyEntity.CompanyName = dr["CompanyName"].ToString();
                    oCompanyEntity.DateFrom = dr["DateFrom"].ToString();
                    oCompanyEntity.DateTo = dr["DateTo"].ToString();
                    oCompanyEntity.FinancialYear = dr["FinancialYear"].ToString();
                    oCompanyEntity.DataBasePath = dr["DatabasePath"].ToString();
                    oCompanyEntity.DataBaseName = dr["DataBaseName"].ToString();
                    companyEntity.Add(oCompanyEntity);
                }
                con.Close();

                dataGridView3.DataSource = companyEntity.Select(x => new { x.CompanyName, x.DateFrom, x.DateTo, x.FinancialYear }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearControl()
        {
            txtUserId.Clear();
            txtPassword.Clear();
            txtRePassword.Clear();
            cmbUType.Text = "";
            KFOpeningList.Clear();
            txtCompanyName.ReadOnly = false;
            txtFinancialYear.ReadOnly = false;
            F_Update = 0;
            MetalList.Clear();
            MetalList = CommanHelper.GetMetalCate(F_Update).ToList();
            txtCompanyName.Clear();
            txtFinancialYear.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            BindCompanyOpeningColumn();
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            lbltotalweight.Visible = false;
            lblFine.Text = "";
            lblKFCate.Text = "";
            lblKFName.Text = "";
            lblsno.Text = "";
            lblWeight.Text = "";
            dataGridView2.Visible = false;
            btnOK.Visible = false;
            groupBox3.Visible = false;
            _CompName = "";
            btnCreate.Enabled = true;
            btnupdate.Enabled = false;
            txtCompanyName.Focus();
        }


        private void BindCompany()
        {
            F_Update = 1;
            MetalList.Clear();
            MetalList = CommanHelper.GetMetalCate(F_Update).ToList();
            btnCreate.Enabled = false;
            btnupdate.Enabled = true;
            txtCompanyName.Text = CommanHelper.CompName.ToString();
            txtCompanyName.ReadOnly = true;
            txtFinancialYear.Text = CommanHelper._FinancialYear.ToString();
            if (CommanHelper.CheckTransaction() == true)
            {
                txtFinancialYear.ReadOnly = true;
            }
            MetalList.Clear();
            MetalList = CommanHelper.GetCompanyMetal().ToList();

            int Snu = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            BindCompanyOpeningColumn();
            foreach (var item in MetalList.Where(x => x.CompanyName == _CompName.Trim()).ToList())
            {
                dataGridView1.Rows.Add();

                DataGridViewComboBoxCell cmbCat = (DataGridViewComboBoxCell)dataGridView1.Rows[Snu].Cells[1];
                cmbCat.Dispose();
                cmbCat.DataSource = MetalList.Select(r => r.MetalCategory).Distinct().ToList();
                dataGridView1.Rows[Snu].Cells[0].Value = Convert.ToString(item.MetalCategory);

                DataGridViewComboBoxCell cmbCatName = (DataGridViewComboBoxCell)dataGridView1.Rows[Snu].Cells[1];
                cmbCatName.Dispose();
                cmbCatName.DataSource = MetalList.Where(x => x.MetalCategory == Convert.ToString(item.MetalCategory).Trim()).Select(r => r.MetalName).Distinct().ToList();
                dataGridView1.Rows[Snu].Cells[1].Value = Convert.ToString(item.MetalName);

                DataGridViewComboBoxCell cmbWeigth = (DataGridViewComboBoxCell)dataGridView1.Rows[Snu].Cells[2];
                cmbWeigth.Dispose();
                cmbWeigth.DataSource = MetalList.Where(x => x.MetalCategory == Convert.ToString(item.MetalCategory).Trim() && x.MetalName == Convert.ToString(item.MetalName)).Select(r => r.WeightType).Distinct().ToList();
                dataGridView1.Rows[Snu].Cells[2].Value = Convert.ToString(item.WeightType);
                if (Convert.ToString(item.MetalCategory).Trim() != "CASH")
                {
                    dataGridView1.Rows[Snu].Cells[3].Value = Convert.ToString(item.KachchiFine);
                }
                dataGridView1.Rows[Snu].Cells[4].Value = Convert.ToString(item.Weight);
                dataGridView1.Rows[Snu].Cells[5].Value = Convert.ToString(item.DrCr);
                Snu++;
            }


            groupBox3.Visible = true;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select UserId,Pwd,UserType From Users", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtUserId.Text = dr["UserId"].ToString();
                txtRePassword.Text = txtPassword.Text = dr["Pwd"].ToString();
                cmbUType.Text = dr["UserType"].ToString();
            }
            dr.Close();

            cmd.CommandText = "Select MetalCategory,MetalName,PaatNo,Weight,Round(Tunch1,2) AS Tunch1,Round(Tunch2,2) AS Tunch2,Round(Fine,2) AS Fine,Sno from KfDetails Where TranType = 'CKF'";
            dr = cmd.ExecuteReader();
            KFOpeningList.Clear();
            while (dr.Read())
            {
                KFOpeningEntity oKFOpeningEntity = new KFOpeningEntity();
                oKFOpeningEntity.MetalCategory = dr["MetalCategory"].ToString();
                oKFOpeningEntity.MetalName = dr["MetalName"].ToString();
                oKFOpeningEntity.PaatNo = dr["PaatNo"].ToString();
                oKFOpeningEntity.Weight = Conversion.ConToDec6(dr["Weight"].ToString());
                oKFOpeningEntity.Tunch1 = Conversion.ConToDec6(dr["Tunch1"].ToString());
                oKFOpeningEntity.Tunch2 = Conversion.ConToDec6(dr["Tunch2"].ToString());
                oKFOpeningEntity.Fine = Conversion.ConToDec6(dr["Fine"].ToString());
                oKFOpeningEntity.Sno = Conversion.ConToInt(dr["Sno"].ToString());
                KFOpeningList.Add(oKFOpeningEntity);
            }
            dr.Close();


            con.Close();
            dataGridView1.Focus();

        }

        #endregion


        private void Company_Load(object sender, EventArgs e)
        {
            this.toolStripMenu_Create.Click += new EventHandler(btnCreate_Click);
            this.toolStripMenu_Update.Click += new EventHandler(btnupdate_Click);
            this.toolStripMenuI_Refresh.Click += new EventHandler(btnrefresh_Click);
            this.toolStripMenu_OK.Click += new EventHandler(btnOK_Click);

            lblKFCate.Text = "";
            lblKFName.Text = "";
            this.CancelButton = btnExit;
            con = new OleDbConnection();

            if (CommanHelper.CompName != "" && CommanHelper.Com_DB_PATH != "" && CommanHelper.Com_DB_NAME != "")
            {
                con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
                BindCompanyDetails();
            }

            BindCompanyOpeningColumn();
            BindKFColumn();

            MetalList = CommanHelper.GetMetalCate(F_Update).ToList();

            this.dataGridView1.Columns["AmountWeight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns["Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns["Tunch1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns["Tunch2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns["Fine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            btnupdate.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyName.Text.Trim() == "")
                {
                    txtCompanyName.Focus();
                    return;
                }
                if (txtFinancialYear.Text.Trim() == "")
                {
                    txtFinancialYear.Focus();
                    return;
                }

                groupBox3.Visible = true;

                if (txtUserId.Text.Trim() == "")
                {
                    txtUserId.Focus();
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    txtPassword.Focus();
                    return;
                }
                if (cmbUType.Text.Trim() == "")
                {
                    cmbUType.Focus();
                    return;
                }

                CommanHelper.CompName = txtCompanyName.Text;
                String _FYFrom = "04/01/" + txtFinancialYear.Text.Substring(0, 4);
                String _FYTo = "03/31/" + txtFinancialYear.Text.Substring(4, 4);
                var directoryInfo = new System.IO.DirectoryInfo(Application.StartupPath);
                var dirName = directoryInfo.GetDirectories();

                //foreach (var item in dirName)
                //{
                //    if (txtCompanyName.Text.Trim().ToUpper() == item.Name.ToString().ToUpper())
                //    {
                //        MessageBox.Show("Company Name Allready Exists.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        txtCompanyName.Focus();
                //        return;
                //    }
                //}

                for (int i = 0; i < dirName.Count(); i++)
                {
                    var mainDir = dirName[i].GetDirectories();
                    foreach (var item in mainDir)
                    {
                        DirectoryInfo d = new DirectoryInfo(item.FullName);
                        if ((txtCompanyName.Text.Trim().ToUpper() == dirName[i].Name.ToString().ToUpper()) && (txtFinancialYear.Text.Trim() == item.Name.ToString()))
                        {
                            MessageBox.Show("Company Name Allready Exists.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCompanyName.Focus();
                            return;
                        }
                    }
                }

                Directory.CreateDirectory(Application.StartupPath + "\\" + txtCompanyName.Text + "\\" + txtFinancialYear.Text);
                _DPath = txtCompanyName.Text + "\\" + txtFinancialYear.Text;

                File.Copy(Application.StartupPath + "\\" + "Account.mdb", Application.StartupPath + "\\" + txtCompanyName.Text + "\\" + txtFinancialYear.Text + "\\" + "" + txtCompanyName.Text + "(" + txtFinancialYear.Text + ")" + ".mdb");
                _DName = txtCompanyName.Text + "(" + txtFinancialYear.Text + ")";

                CommanHelper.Com_DB_PATH = Application.StartupPath + "\\" + _DPath;
                CommanHelper.Com_DB_NAME = _DName;


                using (OleDbConnection con2 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\" + _DPath + "\\" + _DName + ".mdb;Jet OLEDB:Database Password=Hello@12345XZ435"))
                {
                    con2.Open();
                    Tran = con2.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand("", con2, Tran);

                    //-------Insert Company Details
                    CompanyFactory.Insert(txtCompanyName.Text.Trim(), _FYFrom, _FYTo, txtFinancialYear.Text.Trim(), _DPath, _DName, con2, Tran);

                    //--------Insert Users Id Password
                    UserFactory.Insert(txtUserId.Text.Trim(), txtPassword.Text.Trim(), cmbUType.Text.Trim(), CommanHelper.CompName, con2, Tran);

                    //----------Company Opening Information
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        String _MetalCat = "";
                        String _MetalName = "";
                        String _WeightType = "";
                        String _KF = "";
                        Decimal _Amt_Weight = 0;
                        String _DrCr = "";
                        Decimal _Credit = 0;
                        Decimal _Debit = 0;
                        _MetalCat = (dataGridView1.Rows[i].Cells[0].Value ?? (object)"").ToString();
                        _MetalName = (dataGridView1.Rows[i].Cells[1].Value ?? (object)"").ToString();
                        _WeightType = (dataGridView1.Rows[i].Cells[2].Value ?? (object)"").ToString();
                        _KF = (dataGridView1.Rows[i].Cells[3].Value ?? (object)"").ToString();

                        if (dataGridView1.Rows[i].Cells[4].Value != null)
                        {
                            _Amt_Weight = Conversion.ConToDec6(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        }
                        _DrCr = Conversion.ConToStr(dataGridView1.Rows[i].Cells[5].Value);
                        if (_DrCr.Trim() == "JAMA")
                        {
                            _Credit = _Amt_Weight;
                        }
                        if (_DrCr.Trim() == "NAAM")
                        {
                            _Debit = _Amt_Weight;
                        }

                        if (_DrCr != "")
                        {
                            CompnayOpeningFactory.Insert(_MetalName, _Amt_Weight, _DrCr, CommanHelper.CompName, txtUserId.Text.Trim(), con2, Tran);

                            //--------Insert Data In PartyTran Table
                            cmd.CommandText = "INSERT INTO PartyTran(TrDate,PartyName,MetalCategory,MetalName,Debit,Credit,Weight,TranType)VALUES('" + _FYFrom + "','" + txtCompanyName.Text.Trim() + "','" + _MetalCat + "','" + _MetalName + "','" + _Debit + "','" + _Credit + "','" + _Amt_Weight + "','CO')";
                            cmd.ExecuteNonQuery();
                        }

                        //---------Insert Data Metal
                        if (_MetalCat != "" && _MetalName != "" && _KF != "")
                        {
                            Boolean CheckMetalExist = false;
                            Boolean _CheckDefaultMetal = false;
                            cmd.CommandText = "Select * From Metal  Where MetalCategory='" + _MetalCat + "' And MetalName = '" + _MetalName + "' And WeightType = '" + _WeightType + "' And KachchiFine = '" + _KF + "'";
                            OleDbDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                CheckMetalExist = true;
                                if (dr["UserId"].ToString().Trim() == "")
                                {
                                    _CheckDefaultMetal = true;
                                }
                            }
                            dr.Close();

                            if (CheckMetalExist == false)
                            {
                                MetalFactory.InsertMetal(_MetalCat, _MetalName, _WeightType, _KF, CommanHelper.CompName, txtUserId.Text.Trim(), con2, Tran);
                            }
                            else
                            {
                                if (_CheckDefaultMetal == false)
                                {
                                    MetalFactory.InsertMetal(_MetalCat, _MetalName, _WeightType, _KF, CommanHelper.CompName, txtUserId.Text.Trim(), con2, Tran);
                                }
                                else
                                {
                                    cmd.CommandText = "UPDATE Metal SET MetalCategory='" + _MetalCat + "' , MetalName = '" + _MetalName + "' , WeightType = '" + _WeightType + "' , KachchiFine = '" + _KF + "' Where MetalCategory='" + _MetalCat + "' And MetalName = '" + _MetalName + "' And WeightType = '" + _WeightType + "' And KachchiFine = '" + _KF + "'";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                    }



                    //-----Insert KF details
                    foreach (var item in KFOpeningList.ToList())
                    {
                        String _MetalCate = "";
                        String _MetalName = "";
                        String _StrPaatNo = "";
                        Decimal _Weight = 0;
                        Decimal _Tunch1 = 0;
                        Decimal _Tunch2 = 0;
                        Decimal _Fine = 0;

                        _MetalCate = (item.MetalCategory ?? (object)"").ToString();
                        _MetalName = (item.MetalName ?? (object)"").ToString();
                        _StrPaatNo = (item.PaatNo ?? (object)"").ToString();
                        _Weight = Conversion.ConToDec6((item.Weight.ToString() ?? (object)"").ToString());
                        _Tunch1 = Conversion.ConToDec6((item.Tunch1.ToString() ?? (object)"").ToString());
                        _Tunch2 = Conversion.ConToDec6((item.Tunch2.ToString() ?? (object)"").ToString());
                        _Fine = Conversion.ConToDec6((item.Fine.ToString() ?? (object)"").ToString());

                        if (_Weight > 0)
                        {
                            KFFactory.Insert(_MetalCate, _MetalName, _StrPaatNo, _Weight, _Tunch1, _Tunch2, _Fine, "CKF", "N", Conversion.ConToDT(_FYFrom), CommanHelper.CompName, txtUserId.Text.Trim(), con2, Tran);
                        }

                    }

                    Tran.Commit();
                    con2.Close();

                    MessageBox.Show("Company Created Successfully.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("Do U Want To Login Into New Created Company ?", "Company", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Master.objMaster.Hide();
                        CompanyDetails oCompanyDetails = new CompanyDetails(); oCompanyDetails.Show();
                        oCompanyDetails.listBox1.SelectedIndex = 0;
                    }
                    ClearControl();
                }
            }
            catch (Exception ex)
            {
                Tran.Rollback();
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                File.Delete(Application.StartupPath + "\\" + txtCompanyName.Text + "\\" + txtFinancialYear.Text + "\\" + txtCompanyName.Text + "(" + txtFinancialYear.Text + ")" + ".mdb");
                Directory.Delete(Application.StartupPath + "\\" + txtCompanyName.Text + "\\" + txtFinancialYear.Text);
                Directory.Delete(Application.StartupPath + "\\" + txtCompanyName.Text);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCompanyName.Text.Trim() == "")
                {
                    txtCompanyName.Focus();
                    return;
                }
                if (txtFinancialYear.Text.Trim() == "")
                {
                    txtFinancialYear.Focus();
                    return;
                }

                groupBox3.Visible = true;

                if (txtUserId.Text.Trim() == "")
                {
                    txtUserId.Focus();
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    txtPassword.Focus();
                    return;
                }
                if (cmbUType.Text.Trim() == "")
                {
                    cmbUType.Focus();
                    return;
                }

                CommanHelper.CompName = txtCompanyName.Text;
                String _FYFrom = "04/01/" + txtFinancialYear.Text.Substring(0, 4);
                String _FYTo = "03/31/" + txtFinancialYear.Text.Substring(4, 4);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                Tran = con.BeginTransaction();

                OleDbCommand cmd = new OleDbCommand("", con, Tran);

                //--------Insert Users Id Password
                cmd.CommandText = "UPDATE USERS SET UserId = '" + txtUserId.Text.Trim() + "',Pwd = '" + txtPassword.Text.Trim() + "',UserType = '" + cmbUType.Text.Trim() + "',Company = '" + CommanHelper.CompName + "'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From CompanyOpening";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From PartyTran Where TranType = 'CO'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From Metal Where CompanyName <> '' And UserId <> ''";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From KfDetails Where TranType='CKF' And Company = '" + CommanHelper.CompName + "'";
                cmd.ExecuteNonQuery();

                //----------Company Opening Information

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    String _MetalCat = "";
                    String _MetalName = "";
                    String _WeightType = "";
                    String _KF = "";
                    Decimal _Amt_Weight = 0;
                    String _DrCr = "";
                    Decimal _Credit = 0;
                    Decimal _Debit = 0;
                    _MetalCat = (dataGridView1.Rows[i].Cells[0].Value ?? (object)"").ToString();
                    _MetalName = (dataGridView1.Rows[i].Cells[1].Value ?? (object)"").ToString();
                    _WeightType = (dataGridView1.Rows[i].Cells[2].Value ?? (object)"").ToString();
                    _KF = (dataGridView1.Rows[i].Cells[3].Value ?? (object)"").ToString();

                    if (dataGridView1.Rows[i].Cells[4].Value != null)
                    {
                        _Amt_Weight = Conversion.ConToDec6(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    }
                    _DrCr = Conversion.ConToStr(dataGridView1.Rows[i].Cells[5].Value);
                    if (_DrCr.Trim() == "JAMA")
                    {
                        _Credit = _Amt_Weight;
                    }
                    if (_DrCr.Trim() == "NAAM")
                    {
                        _Debit = _Amt_Weight;
                    }

                    if (_DrCr != "")
                    {
                        CompnayOpeningFactory.Insert(_MetalName, _Amt_Weight, _DrCr, CommanHelper.CompName, txtUserId.Text.Trim(), con, Tran);

                        //--------Insert Data In PartyTran Table
                        cmd.CommandText = "INSERT INTO PartyTran(TrDate,PartyName,MetalCategory,MetalName,Debit,Credit,Weight,TranType)VALUES('" + _FYFrom + "','" + txtCompanyName.Text.Trim() + "','" + _MetalCat + "','" + _MetalName + "','" + _Debit + "','" + _Credit + "','" + _Amt_Weight + "','CO')";
                        cmd.ExecuteNonQuery();
                    }

                    //---------Insert Data Metal
                    if (_MetalCat != "" && _MetalName != "" && _KF != "")
                    {
                        Boolean CheckMetalExist = false;
                        Boolean _CheckDefaultMetal = false;
                        cmd.CommandText = "Select * From Metal  Where MetalCategory='" + _MetalCat + "' And MetalName = '" + _MetalName + "' And WeightType = '" + _WeightType + "' And KachchiFine = '" + _KF + "'";
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            CheckMetalExist = true;
                            if (dr["UserId"].ToString().Trim() == "")
                            {
                                _CheckDefaultMetal = true;
                            }
                        }
                        dr.Close();

                        if (CheckMetalExist == false)
                        {
                            MetalFactory.InsertMetal(_MetalCat, _MetalName, _WeightType, _KF, CommanHelper.CompName, txtUserId.Text.Trim(), con, Tran);
                        }
                        else
                        {
                            if (_CheckDefaultMetal == false)
                            {
                                MetalFactory.InsertMetal(_MetalCat, _MetalName, _WeightType, _KF, CommanHelper.CompName, txtUserId.Text.Trim(), con, Tran);
                            }
                            else
                            {
                                cmd.CommandText = "UPDATE Metal SET  MetalCategory='" + _MetalCat + "' , MetalName = '" + _MetalName + "' , WeightType = '" + _WeightType + "' , KachchiFine = '" + _KF + "' Where MetalCategory='" + _MetalCat + "' And MetalName = '" + _MetalName + "' And WeightType = '" + _WeightType + "' And KachchiFine = '" + _KF + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }


                //-----Insert KF details

                foreach (var item in KFOpeningList.ToList())
                {
                    String _MetalCate = "";
                    String _MetalName = "";
                    String _StrPaatNo = "";
                    Decimal _Weight = 0;
                    Decimal _Tunch1 = 0;
                    Decimal _Tunch2 = 0;
                    Decimal _Fine = 0;

                    _MetalCate = (item.MetalCategory ?? (object)"").ToString();
                    _MetalName = (item.MetalName ?? (object)"").ToString();
                    _StrPaatNo = (item.PaatNo ?? (object)"").ToString();
                    _Weight = Conversion.ConToDec6((item.Weight.ToString() ?? (object)"").ToString());
                    _Tunch1 = Conversion.ConToDec6((item.Tunch1.ToString() ?? (object)"").ToString());
                    _Tunch2 = Conversion.ConToDec6((item.Tunch2.ToString() ?? (object)"").ToString());
                    _Fine = Conversion.ConToDec6((item.Fine.ToString() ?? (object)"").ToString());

                    if (_Weight > 0)
                    {
                        KFFactory.Insert(_MetalCate, _MetalName, _StrPaatNo, _Weight, _Tunch1, _Tunch2, _Fine, "CKF", "N", Conversion.ConToDT(_FYFrom), CommanHelper.CompName, txtUserId.Text.Trim(), con, Tran);
                    }

                }
                Tran.Commit();
                con.Close();

                MessageBox.Show("Company Updated Successfully.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearControl();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                Tran.Rollback();
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

        private void btnExit_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void txtCompanyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (txtCompanyName.Text.Trim() != "")
                    {
                        txtFinancialYear.Focus();
                    }
                    else
                    {
                        txtCompanyName.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void DefaultCashOpening()
        {
            _CheckCashExist = false;
            for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
            {
                if ((dataGridView1.Rows[row].Cells[1].Value ?? (object)"").ToString() == "CASH")
                { _CheckCashExist = true; }
            }

            if (F_Update == 0)
            {
                if (_CheckCashExist == false)
                {
                    this.dataGridView1.Rows.Add("CASH", "CASH", "", "", "", "JAMA");

                }
                dataGridView1.Focus();
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[4];
                this.dataGridView1.CurrentCell.Value = "0";
                dataGridView1.CurrentCell.Selected = true;
            }
        }
        private void txtFinancialYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)(e.KeyChar) >= 48 && (int)(e.KeyChar) <= 57 || (int)(e.KeyChar) == 8 || (int)(e.KeyChar) == 13)
                {
                    e.Handled = false;
                    if (e.KeyChar == 13)
                    {
                        if (txtFinancialYear.Text.Length == 4)
                        {
                            int _FY = 0;
                            _FY = Convert.ToInt32(txtFinancialYear.Text);
                            _FY = _FY + 1;
                            txtFinancialYear.Text = txtFinancialYear.Text + _FY.ToString();
                            DefaultCashOpening();
                        }
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



        private void txtFinancialYear_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyName.Text.Trim() != "" && txtFinancialYear.Text.Trim() != "")
                {
                    if (txtFinancialYear.Text.Trim().Length == 4)
                    {
                        double year_add;
                        year_add = Convert.ToDouble(txtFinancialYear.Text.Trim());
                        year_add = year_add + 1;
                        txtFinancialYear.Text = txtFinancialYear.Text + year_add.ToString();
                        DefaultCashOpening();

                    }
                }
                txtFinancialYear.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count >= 0)
                {
                    foreach (var item in KFOpeningList.ToList())
                    {
                        if (item.MetalName == _KfMetalName)
                            KFOpeningList.Remove(item);
                    }
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        var _Sno = 0;
                        if (KFOpeningList.Count > 0)
                        {
                            _Sno = KFOpeningList.Max(x => x.Sno) + 1;
                        }
                        KFOpeningEntity oKFOpeningEntity = new KFOpeningEntity();
                        oKFOpeningEntity.MetalCategory = _KfMetalCate.ToString();
                        oKFOpeningEntity.MetalName = _KfMetalName.ToString();
                        oKFOpeningEntity.PaatNo = (dataGridView2.Rows[i].Cells[0].Value ?? (object)"").ToString();
                        oKFOpeningEntity.Weight = Conversion.ConToDec6((dataGridView2.Rows[i].Cells[1].Value ?? (object)"").ToString());
                        oKFOpeningEntity.Tunch1 = Conversion.ConToDec6((dataGridView2.Rows[i].Cells[2].Value ?? (object)"").ToString());
                        oKFOpeningEntity.Tunch2 = Conversion.ConToDec6((dataGridView2.Rows[i].Cells[3].Value ?? (object)"").ToString());
                        oKFOpeningEntity.Fine = Conversion.ConToDec6((dataGridView2.Rows[i].Cells[4].Value ?? (object)"").ToString());
                        oKFOpeningEntity.Sno = _Sno;
                        KFOpeningList.Add(oKFOpeningEntity);
                    }
                    dataGridView1.CurrentRow.Cells[4].Value = lblFine.Text;
                    dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[5];
                    dataGridView1.Focus();
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }



        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblKFCate.Visible = false;
                lblKFName.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            Double _WEIGHT, _FINE, _TUNCH1, _TUNCH2;
            _WEIGHT = 0;
            _TUNCH1 = 0;
            _FINE = 0;
            _TUNCH2 = 0;
            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 5)
            {
                try
                {
                    _WEIGHT = Conversion.ConTodob5(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
                    _TUNCH1 = Conversion.ConTodob5(dataGridView2.Rows[e.RowIndex].Cells[2].Value);
                    _TUNCH2 = Conversion.ConTodob5(dataGridView2.Rows[e.RowIndex].Cells[3].Value);
                    if (_TUNCH1 > 0 && _TUNCH2 == 0)
                    {
                        _FINE = System.Math.Round(((_WEIGHT * _TUNCH1) / 100), 3);
                    }
                    else if (_TUNCH2 > 0 && _TUNCH1 == 0)
                    {
                        _FINE = System.Math.Round(((_WEIGHT * _TUNCH2) / 100), 3);
                    }
                    else
                    {
                        _FINE = System.Math.Round(((_WEIGHT * ((_TUNCH1 + _TUNCH2) / 2)) / 100), 3);
                    }
                    if (_WEIGHT > 0)
                    {
                        dataGridView2.Rows[e.RowIndex].Cells[4].Value = _FINE.ToString();
                    }

                    Total();

                    if (dataGridView2.Rows.Count - 1 == dataGridView2.CurrentCell.RowIndex && e.ColumnIndex == 2 && int_keyvalue == 13)
                    {
                        int_keyvalue = 0;
                        if ((dataGridView2.CurrentRow.Cells[0].Value ?? (object)"").ToString() == "" && (dataGridView2.CurrentRow.Cells[1].Value ?? (object)"").ToString() == "")
                        {
                            btnOK.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
            }
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            int_keyvalue = e.KeyValue;
            if (e.KeyCode == Keys.Delete)
            {
                //--------Delete Data from Metal List
                var result = (from r in KFOpeningList
                              where r.Sno == Conversion.ConToInt((dataGridView2.CurrentRow.Cells[5].Value ?? (object)"0").ToString())
                              select r).SingleOrDefault();
                if (result != null)
                    KFOpeningList.Remove(result);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int_keyvalue = e.KeyValue;

                if (e.KeyCode == Keys.Delete)
                {
                    foreach (var item in MetalList.ToList())
                    {
                        if (item.MetalCategory == (dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString() && item.MetalName == (dataGridView1.CurrentRow.Cells[1].Value ?? (object)"").ToString() && item.CompanyName != "")
                            MetalList.Remove(item);
                    }

                    //----------Check KF Exist Than Delete Opening Data From KF Table              
                    foreach (var item in KFOpeningList.ToList())
                    {
                        if (item.MetalName == (dataGridView1.CurrentRow.Cells[1].Value ?? (object)"").ToString())
                            KFOpeningList.Remove(item);
                    }
                }


                if (e.KeyCode == Keys.Enter)
                {
                    if (dataGridView1.Rows.Count - 1 == dataGridView1.CurrentCell.RowIndex)
                    {
                        if (dataGridView1.CurrentCell.ColumnIndex == 1)
                        {
                            if ((dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString() == "" && (dataGridView1.CurrentRow.Cells[1].Value ?? (object)"").ToString() == "")
                            {
                                if (F_Update == 1)
                                {
                                    btnupdate.Focus();
                                }
                                else
                                { btnCreate.Focus(); }
                                dataGridView1.ClearSelection();
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


        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }


        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.IsCurrentCellDirty)
                {
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (dataGridView1.CurrentCell.ColumnIndex == 0)
                    {
                        if (dataGridView1.CurrentCell.Value != null)
                        {
                            DataGridViewComboBoxCell tName = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[1];
                            tName.DataSource = MetalList.Where(w => w.MetalCategory == _MetalCateCellValue.Trim()).Select(x => x.MetalName).Distinct().ToList();
                        }
                    }

                    if (dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        if (dataGridView1.CurrentCell.Value != null)
                        {
                            DataGridViewComboBoxCell tWeight = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[2];
                            tWeight.DataSource = MetalList.Where(r => r.MetalName == _MetalNameCellValue.Trim()).OrderBy(z => z.WeightType).Select(x => x.WeightType).Distinct().ToList();
                        }
                    }

                }
                dataGridView1.BeginEdit(true);
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                if (txtUserId.Text.Trim() == "")
                { txtUserId.Focus(); return; }
                else { txtPassword.Focus(); }
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRePassword.Focus();
            }
        }

        private void txtRePassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbUType.Focus();
            }
        }

        private void cmbUType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnCreate.Focus();
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == col_Amt.Index)
                {
                    e.Control.KeyPress -= NumericCheckHandler;
                    e.Control.KeyPress += NumericCheckHandler;
                }
                if (dataGridView1.CurrentCell.ColumnIndex.Equals(0) || dataGridView1.CurrentCell.ColumnIndex.Equals(1) || dataGridView1.CurrentCell.ColumnIndex.Equals(2))
                {
                    e.Control.KeyPress += Control_KeyPress; // Depending on your requirement you can register any key event for this.
                }
                if (dataGridView1.CurrentCellAddress.X == col_MCate.DisplayIndex || dataGridView1.CurrentCellAddress.X == col_MNane.DisplayIndex || dataGridView1.CurrentCellAddress.X == col_Weight.DisplayIndex)
                {
                    ComboBox cb = e.Control as ComboBox;
                    if (cb != null)
                    {
                        cb.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private static void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = e.KeyChar.ToString().ToUpper();
            char[] ch = str.ToCharArray();
            e.KeyChar = ch[0];

        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                //if (dataGridView2.CurrentCell.ColumnIndex > 0)
                //{
                //    e.Control.KeyPress -= NumericCheckHandler;
                //    e.Control.KeyPress += NumericCheckHandler;
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
                //-------------Metal Category
                #region -------------Metal Category

                if (dataGridView1.CurrentCellAddress.X == col_MCate.DisplayIndex)
                {
                    if (!col_MCate.Items.Contains(e.FormattedValue) && e.FormattedValue.ToString() != "")
                    {
                        if (!MetalList.Where(x => x.MetalCategory == e.FormattedValue.ToString().Trim()).Any())
                        {
                            var max = 0;
                            if (MetalList.Count > 0)
                            {
                                max = MetalList.Max(x => x.Sno) + 1;
                            }
                            MetalEntity oMetal = new MetalEntity();
                            oMetal.MetalCategory = e.FormattedValue.ToString();
                            oMetal.MetalName = "";
                            oMetal.WeightType = "";
                            oMetal.Sno = max;
                            oMetal.CompanyName = txtCompanyName.Text.Trim();
                            MetalList.Add(oMetal);
                            if ((dataGridView1.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                                dataGridView1.CurrentRow.Cells[3].Value = "NO";
                        }
                    }
                    _MetalCateCellValue = e.FormattedValue.ToString();
                    DataGridViewComboBoxCell t1 = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[0];
                    t1.DataSource = MetalList.Select(r => r.MetalCategory).Distinct().ToList();
                    dataGridView1.CurrentRow.Cells[0].Value = e.FormattedValue.ToString().Trim();

                    if (e.FormattedValue.ToString().ToUpper() != "CASH" && e.FormattedValue.ToString().Trim() != "")
                    {
                        DataGridViewComboBoxCell tWeight = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[2];
                        tWeight.DataSource = MetalList.Where(x => x.MetalCategory == e.FormattedValue.ToString()).Select(r => r.WeightType).Distinct().ToList();
                        dataGridView1.CurrentRow.Cells[2].Value = (MetalList.Where(x => x.MetalCategory == e.FormattedValue.ToString()).Select(r => r.WeightType).Distinct().FirstOrDefault() ?? (object)"").ToString();

                    }
                    if (e.FormattedValue.ToString().ToUpper() == "CASH")
                    {
                        dataGridView1.CurrentRow.Cells[2].Value = "";
                        dataGridView1.CurrentRow.Cells[3].Value = "";
                    }
                }
                #endregion

                //-------------Metal Name

                #region ----------Metal Name

                if (dataGridView1.CurrentCellAddress.X == col_MNane.DisplayIndex)
                {
                    if (!col_MNane.Items.Contains(e.FormattedValue) && e.FormattedValue.ToString() != "")
                    {
                        if (dataGridView1.CurrentRow.Cells[0].Value != null)
                        {
                            var result = (from r in MetalList where r.MetalCategory == _MetalCateCellValue.Trim() select r).ToList();
                            foreach (var item in result)
                            {
                                if (!MetalList.Where(x => x.MetalCategory == _MetalCateCellValue.Trim() && x.MetalName == e.FormattedValue.ToString()).Any())
                                {
                                    if (item.MetalCategory == "" && item.MetalName == "")
                                    {
                                        var max = 0;
                                        if (result.Count > 0) { max = MetalList.Max(x => x.Sno) + 1; }
                                        MetalEntity oMetal = new MetalEntity();
                                        oMetal.MetalCategory = _MetalCateCellValue;
                                        oMetal.MetalName = e.FormattedValue.ToString();
                                        oMetal.WeightType = "";
                                        oMetal.Sno = max;
                                        oMetal.CompanyName = txtCompanyName.Text.Trim();
                                        MetalList.Add(oMetal);
                                        if ((dataGridView1.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                                            dataGridView1.CurrentRow.Cells[3].Value = "NO";
                                    }
                                    else
                                    {
                                        if (item.MetalName == "")
                                        {
                                            var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                            update.MetalName = e.FormattedValue.ToString();
                                            update.KachchiFine = "NO";
                                        }
                                        else
                                        {
                                            var max = 0;
                                            if (result.Count > 0) { max = MetalList.Max(x => x.Sno) + 1; }
                                            MetalEntity oMetal = new MetalEntity();
                                            oMetal.MetalCategory = _MetalCateCellValue;
                                            oMetal.MetalName = e.FormattedValue.ToString();
                                            oMetal.WeightType = "";
                                            oMetal.Sno = max;
                                            oMetal.CompanyName = txtCompanyName.Text.Trim();
                                            MetalList.Add(oMetal);
                                            if ((dataGridView1.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                                                dataGridView1.CurrentRow.Cells[3].Value = "NO";
                                        }
                                    }
                                }
                                else
                                {
                                    if (item.MetalName == "")
                                    {
                                        var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                        update.MetalName = e.FormattedValue.ToString();
                                        update.KachchiFine = (dataGridView1.CurrentRow.Cells[3].Value ?? (object)"NO").ToString();
                                    }
                                    if (item.KachchiFine == "")
                                    {
                                        var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                        update.KachchiFine = (dataGridView1.CurrentRow.Cells[3].Value ?? (object)"NO").ToString();
                                    }
                                }
                            }

                            _MetalNameCellValue = e.FormattedValue.ToString();
                            DataGridViewComboBoxCell t2 = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[1];
                            t2.DataSource = MetalList.Where(x => x.MetalCategory == _MetalCateCellValue).Select(r => r.MetalName).Distinct().ToList();
                            dataGridView1.CurrentRow.Cells[1].Value = e.FormattedValue.ToString();

                            var _KF = MetalList.Where(x => x.MetalCategory == _MetalCateCellValue && x.MetalName == e.FormattedValue.ToString()).Select(r => r.KachchiFine).Distinct().FirstOrDefault();
                            if (e.FormattedValue.ToString().ToUpper() != "CASH" && e.FormattedValue.ToString().Trim() != "")
                            {
                                if ((_KF ?? (object)"").ToString() != "")
                                    dataGridView1.CurrentRow.Cells[3].Value = (_KF ?? (object)"").ToString();
                            }
                        }
                    }
                }

                #endregion

                //-------------Metal Weight Type

                #region -------------Metal Weight Type

                if (dataGridView1.CurrentCellAddress.X == col_Weight.DisplayIndex)
                {
                    if (!col_Weight.Items.Contains(e.FormattedValue) && e.FormattedValue.ToString() != "")
                    {
                        if (dataGridView1.CurrentRow.Cells[1].Value != null)
                        {
                            var result = (from r in MetalList where r.MetalName == _MetalNameCellValue.Trim() select r).ToList();
                            foreach (var item in result)
                            {
                                if (!MetalList.Where(x => x.MetalName == _MetalNameCellValue.Trim() && x.MetalCategory == (dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString()).Any())
                                {
                                    var max = 0;
                                    if (result.Count > 0) { max = MetalList.Max(x => x.Sno) + 1; }
                                    MetalEntity oMetal = new MetalEntity();
                                    oMetal.MetalCategory = "";
                                    oMetal.MetalName = _MetalNameCellValue;
                                    oMetal.WeightType = e.FormattedValue.ToString();
                                    oMetal.Sno = max;
                                    oMetal.CompanyName = txtCompanyName.Text.Trim();
                                    MetalList.Add(oMetal);
                                    if ((dataGridView1.CurrentRow.Cells[3].Value ?? (object)"").ToString() == "")
                                        dataGridView1.CurrentRow.Cells[3].Value = "NO";
                                }
                                else
                                {
                                    if (item.WeightType == "")
                                    {
                                        var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                        update.WeightType = e.FormattedValue.ToString();
                                        update.KachchiFine = (dataGridView1.CurrentRow.Cells[3].Value ?? (object)"NO").ToString();
                                    }
                                    if (item.KachchiFine == "")
                                    {
                                        var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                        update.KachchiFine = (dataGridView1.CurrentRow.Cells[3].Value ?? (object)"NO").ToString();
                                    }
                                }
                            }

                            _MetalNameCellValue = e.FormattedValue.ToString();
                            DataGridViewComboBoxCell tWeight = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[2];
                            tWeight.DataSource = MetalList.Select(r => r.WeightType).Distinct().ToList();
                            dataGridView1.CurrentRow.Cells[2].Value = e.FormattedValue.ToString();
                        }
                    }
                }

                #endregion

                //-------------Metal KF

                #region -------------Metal KF

                if (dataGridView1.CurrentCellAddress.X == col_KF.DisplayIndex)
                {
                    if (col_KF.Items.Contains(e.FormattedValue) && e.FormattedValue.ToString() != "")
                    {
                        if (dataGridView1.CurrentRow.Cells[1].Value != null)
                        {
                            var result = (from r in MetalList where r.MetalName == (dataGridView1.CurrentRow.Cells[1].Value ?? (object)"").ToString().Trim() select r).ToList();
                            foreach (var item in result)
                            {
                                var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                update.KachchiFine = (dataGridView1.CurrentRow.Cells[3].Value ?? (object)"NO").ToString();
                            }
                            dataGridView1.CurrentRow.Cells[3].Value = e.FormattedValue.ToString();
                        }
                    }
                }

                #endregion

                if (dataGridView1.CurrentRow.Cells[5].Value == null)
                {
                    dataGridView1.CurrentRow.Cells[5].Value = "JAMA";
                }

                if (e.ColumnIndex == 1)
                {
                    if (e.FormattedValue.ToString() == "")
                    {
                        if ((dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString() != "")
                            if ((from r in MetalList where r.MetalCategory == _MetalCateCellValue.Trim() select r).ToList().Count > 0)
                                e.Cancel = true;
                    }
                }

                _Check = false;
                for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                {
                    if (dataGridView1.Rows[row].Cells[1].Value != null && row != e.RowIndex &&
                        dataGridView1.Rows[row].Cells[1].Value.Equals(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                    {
                        MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[1].Value + " Already in the grid !", "Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        _Check = true;
                    }
                }

                if (e.ColumnIndex == 4)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null && (dataGridView1.Rows[e.RowIndex].Cells[0].Value ?? (object)"").ToString().Trim() != "CASH")
                    {
                        if ((dataGridView1.Rows[e.RowIndex].Cells[1].Value != null && (dataGridView1.Rows[e.RowIndex].Cells[1].Value ?? (object)"").ToString().Trim() != "")
                            && (dataGridView1.Rows[e.RowIndex].Cells[2].Value != null && (dataGridView1.Rows[e.RowIndex].Cells[2].Value ?? (object)"").ToString().Trim() != "")
                            && (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null && (dataGridView1.Rows[e.RowIndex].Cells[3].Value ?? (object)"").ToString().Trim() != ""))
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value == null || (dataGridView1.Rows[e.RowIndex].Cells[4].Value ?? (object)"").ToString().Trim() == "")
                            {
                                e.Cancel = true;
                            }
                        }
                    }
                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null && (dataGridView1.Rows[e.RowIndex].Cells[0].Value ?? (object)"").ToString().Trim() == "CASH")
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[4].Value == null || (dataGridView1.Rows[e.RowIndex].Cells[4].Value ?? (object)"").ToString().Trim() == "")
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CellValidating");
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[1].Value != null)
                {
                    if ((MetalList.Where(x => x.MetalName == dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim() && x.KachchiFine == "YES").Any()) && e.ColumnIndex == 4)
                    {
                        if (_Check == false)
                        {
                            _KfMetalName = "";
                            _KfMetalCate = "";

                            _KfMetalCate = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                            _KfMetalName = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();

                            lblKFCate.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                            lblKFName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();

                            btnOK.Visible = true;
                            lblWeight.Visible = true;
                            lblFine.Visible = true;
                            lbltotalweight.Visible = true;
                            lblsno.Visible = true;
                            dataGridView2.Visible = true;
                            lblFine.Text = "";
                            lblsno.Text = "";
                            lblWeight.Text = "";

                            DataTable dt = new DataTable();
                            var result = KFOpeningList.Where(x => x.MetalName == _KfMetalName).Select(s => new
                            {
                                s.PaatNo,
                                s.Weight,
                                s.Tunch1,
                                s.Tunch2,
                                s.Fine,
                                s.Sno,
                                s.MetalName
                            }).ToList();


                            dt = CommanHelper.ToDataTable(result);
                            if (KFOpeningList.Where(x => x.MetalName == _KfMetalName).Any())
                            {
                                dataGridView2.DataSource = dt;
                                Total();
                            }
                            else
                            {
                                dataGridView2.DataSource = dt;
                            }
                            dataGridView2.Columns["Sno"].Visible = false;
                            dataGridView2.Columns["MetalName"].Visible = false;

                            dataGridView2.Focus();
                            this.dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[0];
                            dataGridView2.CurrentCell.Selected = true;
                            dataGridView2.BeginEdit(true);
                        }
                    }
                    else
                    {
                        btnOK.Visible = false;
                        lblWeight.Visible = false;
                        lblFine.Visible = false;
                        lbltotalweight.Visible = false;
                        lblsno.Visible = false;
                        dataGridView2.Visible = false;
                        dataGridView2.Focus();
                    }
                }

                if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
                {
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    {
                        _MetalCateCellValue = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                        DataGridViewComboBoxCell cmbCatName = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[1];
                        cmbCatName.Dispose();
                        cmbCatName.DataSource = MetalList.Where(x => x.MetalCategory == _MetalCateCellValue.Trim()).Select(r => r.MetalName).Distinct().ToList();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (dataGridView1.CurrentRow.Cells[1].Value != null)
                    {
                        _MetalNameCellValue = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                        DataGridViewComboBoxCell cmbWeight = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[2];
                        cmbWeight.Dispose();
                        if (MetalList.Where(x => x.MetalName == _MetalNameCellValue.Trim() && x.WeightType != "").Select(r => r.WeightType).Distinct().Any())
                        {
                            cmbWeight.DataSource = MetalList.Where(x => x.MetalName == _MetalNameCellValue.Trim()).Select(r => r.WeightType).Distinct().ToList();
                        }
                        else
                        {
                            cmbWeight.DataSource = MetalList.OrderBy(z => z.WeightType).Select(r => r.WeightType).Distinct().ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cell Enter");
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                hti = dataGridView3.HitTest(e.X, e.Y);
                if (hti.RowIndex >= 0)
                {
                    btnCreate.Enabled = false;
                    _CompName = "";
                    _CompName = dataGridView3.Rows[hti.RowIndex].Cells[0].Value.ToString();
                    BindCompany();
                    dataGridView1.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView3_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (CommanHelper.CompName.Trim().ToString() == (dataGridView3.CurrentRow.Cells[0].Value ?? (object)"").ToString().Trim())
                    {
                        _CompName = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                        BindCompany();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                Total();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void toolStripMenu_CompanyDetails_Click(object sender, EventArgs e)
        {
            dataGridView3.Focus();
            dataGridView3.Rows[0].Selected = true;
        }




        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((dataGridView1.Rows[e.RowIndex].Cells[0].Value ?? (object)"").ToString().Trim() == "CASH")
                {
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Columns[2].ReadOnly = false;
                    dataGridView1.Columns[3].ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

            }
        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            try
            {
                txtUserId.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtUserId_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text.Trim() == "")
                {
                    txtUserId.Focus();
                }
                txtUserId.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            try
            {
                txtPassword.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text.Trim() == "")
                { txtPassword.Focus(); }
                txtPassword.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtRePassword_Enter(object sender, EventArgs e)
        {
            try
            {
                txtRePassword.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtRePassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRePassword.Text.Trim() == "")
                { txtRePassword.Focus(); }
                txtRePassword.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtCompanyName_Enter(object sender, EventArgs e)
        {
            try
            {
                txtCompanyName.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtCompanyName_Leave(object sender, EventArgs e)
        {
            try
            {
                txtCompanyName.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtFinancialYear_Enter(object sender, EventArgs e)
        {
            try
            {
                txtFinancialYear.BackColor = Color.Cyan;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridView3_Leave(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.ClearSelection();
                dataGridView3.CurrentCell = null;
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


    }
}
