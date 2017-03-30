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

namespace SilverGold
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
        String _MetalCateCellValue = "";
        String _MetalNameCellValue = "";
        List<CompanyEntity> companyEntity = new List<CompanyEntity>();
        List<MetalEntity> MetalList = new List<MetalEntity>();
        DataGridViewComboBoxColumn col_MCate = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn col_MNane = new DataGridViewComboBoxColumn();
        DataGridViewColumn col_Amt = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn col_Weight = new DataGridViewComboBoxColumn();
        

        DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
        DataGridViewColumn col6 = new DataGridViewTextBoxColumn();

        DataGridView.HitTestInfo hti;
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);


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
            col_MCate.HeaderText = "MetalCate";
            col_MCate.Name = "MetalCategory";
            col_MCate.FlatStyle = FlatStyle.Popup;
            col_MCate.DataSource = CommanHelper.GetMetalCate().Select(x => x.MetalCategory).Distinct().ToList();
            dataGridView1.Columns.Add(col_MCate);


            col_MNane.DataPropertyName = "MetalName";
            col_MNane.HeaderText = "MetalName";
            col_MNane.Name = "MetalName";
            col_MNane.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col_MNane);


            col_Weight.DataPropertyName = "WieghtType";
            col_Weight.HeaderText = "Wt/Type";
            col_Weight.Name = "WieghtType";
            col_Weight.MaxDropDownItems = 4;
            col_Weight.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col_Weight);

            DataGridViewComboBoxColumn col_KF = new DataGridViewComboBoxColumn();
            col_KF.DataPropertyName = "KachchiFine";
            col_KF.HeaderText = "KF";
            col_KF.Name = "KachchiFine";
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
                col_DrCr.Items.Add("CREDIT");
                col_DrCr.Items.Add("DEBIT");                
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
                OleDbCommand cmd = new OleDbCommand("SELECT CompanyName,DateFrom,DateTo,FinancialYear,DatabasePath,DataBaseName FROM Company", con);
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

                dataGridView3.DataSource = companyEntity.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearControl()
        {
            F_Update = 0;
            txtCompanyName.Clear();
            txtFinancialYear.Clear();
            dataGridView1.Rows.Clear();
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
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
            btnCreate.Enabled = false;
            btnupdate.Enabled = true;
            txtCompanyName.Text = CommanHelper.CompName.ToString();
            txtFinancialYear.Text = CommanHelper._FinancialYear.ToString();
            MetalList.Clear();
            MetalList = CommanHelper.GetCompanyMetal().ToList();

            int Snu = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (var item in MetalList.Where(x => x.CompanyName == _CompName.Trim()).ToList())
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[Snu].Cells[0].Value = Convert.ToString(item.MetalCategory);
                DataGridViewComboBoxCell cmbCatName = (DataGridViewComboBoxCell)dataGridView1.Rows[Snu].Cells[1];
                cmbCatName.Dispose();
                cmbCatName.DataSource = MetalList.Where(x => x.MetalCategory == Convert.ToString(item.MetalCategory).Trim()).Select(r => r.MetalName).Distinct().ToList();
                dataGridView1.Rows[Snu].Cells[1].Value = Convert.ToString(item.MetalName);

                DataGridViewComboBoxCell cmbWeigth = (DataGridViewComboBoxCell)dataGridView1.Rows[Snu].Cells[2];
                cmbWeigth.Dispose();
                cmbWeigth.DataSource = MetalList.Where(x => x.MetalCategory == Convert.ToString(item.MetalCategory).Trim() && x.MetalName == Convert.ToString(item.MetalName)).Select(r => r.WieghtType).Distinct().ToList();
                dataGridView1.Rows[Snu].Cells[2].Value = Convert.ToString(item.WieghtType);

                dataGridView1.Rows[Snu].Cells[3].Value = Convert.ToString(item.KachchiFine);
                dataGridView1.Rows[Snu].Cells[4].Value = Convert.ToString(item.AmountWeight);
                dataGridView1.Rows[Snu].Cells[5].Value = Convert.ToString(item.DrCr);
                Snu++;
            }

            OleDbDataAdapter da = new OleDbDataAdapter("Select PaatNo,Weight,Tunch1,Tunch2,Fine,Sno from KfDetails Where TranType = 'CKF'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns["Sno"].Visible = false;

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

            MetalList = CommanHelper.GetMetalCate().ToList();
           

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

                foreach (var item in dirName)
                {
                    if (txtCompanyName.Text.Trim() == item.Name.ToString())
                    {
                        MessageBox.Show("Company Name Allready Exists.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCompanyName.Focus();
                        return;
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
                    cmd.CommandText = "INSERT INTO Company(CompanyName,DateFrom,DateTo,FinancialYear,DatabasePath,DataBaseName)VALUES('" + txtCompanyName.Text.Trim() + "','" + _FYFrom + "','" + _FYTo + "','" + txtFinancialYear.Text.Trim() + "','" + _DPath + "','" + _DName + "')";
                    cmd.ExecuteNonQuery();

                    //--------Insert Users Id Password
                    cmd.CommandText = "INSERT INTO USERS(UserId,Pwd,UserType,Company)VALUES('" + txtUserId.Text.Trim() + "','" + txtPassword.Text.Trim() + "','" + cmbUType.Text.Trim() + "','" + CommanHelper.CompName + "')";
                    cmd.ExecuteNonQuery();

                    //----------Company Opening Information
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        String MetalCat = "";
                        String MetalName = "";
                        String WeightType = "";
                        String KF = "";
                        Decimal Amt_Weight = 0;

                        String DrCr = "";
                        Decimal _Credit = 0;
                        Decimal _Debit = 0;
                        MetalCat = (dataGridView1.Rows[i].Cells[0].Value ?? (object)"").ToString();
                        MetalName = (dataGridView1.Rows[i].Cells[1].Value ?? (object)"").ToString();
                        WeightType = (dataGridView1.Rows[i].Cells[2].Value ?? (object)"").ToString();
                        KF = (dataGridView1.Rows[i].Cells[3].Value ?? (object)"").ToString();

                        if (dataGridView1.Rows[i].Cells[4].Value != null)
                        {
                            Amt_Weight = Conversion.ConToDec6(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        }
                        DrCr = Conversion.ConToStr(dataGridView1.Rows[i].Cells[5].Value);
                        if (DrCr.Trim() == "CREDIT")
                        {
                            _Credit = Amt_Weight;
                        }
                        if (DrCr.Trim() == "DEBIT")
                        {
                            _Debit = Amt_Weight;
                        }

                        if (DrCr != "")
                        {
                            cmd.CommandText = "INSERT INTO CompanyOpening(MetalName,Amount_Weight,DrCr,CompanyName,UserId)VALUES('" + MetalName + "','" + Amt_Weight + "','" + DrCr + "','" + CommanHelper.CompName + "','" + txtUserId.Text.Trim() + "')";
                            cmd.ExecuteNonQuery();

                            //--------Insert Data In PartyTran Table
                            cmd.CommandText = "INSERT INTO PartyTran(TrDate,PartyName,MetalCategory,MetalName,Debit,Credit,Weight,TranType)VALUES('" + _FYFrom + "','" + txtCompanyName.Text.Trim() + "','" + MetalCat + "','" + MetalName + "','" + _Debit + "','" + _Credit + "','" + Amt_Weight + "','CO')";
                            cmd.ExecuteNonQuery();
                        }

                        //---------Insert Data Metal
                        if (MetalCat != "" && MetalName != "" && WeightType != "" && KF != "")
                        {
                            Boolean CheckMetalExist = false;
                            cmd.CommandText = "Select * From Metal  Where MetalCategory='" + MetalCat + "' And MetalName = '" + MetalName + "' And WieghtType = '" + WeightType + "' And KachchiFine = '" + KF + "'";
                            OleDbDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                CheckMetalExist = true;
                            }
                            dr.Close();

                            if (CheckMetalExist == false)
                            {
                                cmd.CommandText = "INSERT INTO Metal(MetalCategory,MetalName,WieghtType,KachchiFine,CompanyName,UserId)VALUES('" + MetalCat + "','" + MetalName + "','" + WeightType + "','" + KF + "','" + CommanHelper.CompName + "','" + txtUserId.Text.Trim() + "')";
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText = "UPDATE Metal SET CompanyName = '" + CommanHelper.CompName + "',UserId = '" + txtUserId.Text.Trim() + "', MetalCategory='" + MetalCat + "' , MetalName = '" + MetalName + "' , WieghtType = '" + WeightType + "' , KachchiFine = '" + KF + "' Where MetalCategory='" + MetalCat + "' And MetalName = '" + MetalName + "' And WieghtType = '" + WeightType + "' And KachchiFine = '" + KF + "'";
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }



                    //-----Insert KF details
                    for (int k = 0; k < dataGridView2.Rows.Count - 1; k++)
                    {
                        String strPaatNo = "";
                        Decimal weight = 0;
                        Decimal Tunch1 = 0;
                        Decimal Tunch2 = 0;
                        Decimal Fine = 0;

                        strPaatNo = (dataGridView2.Rows[k].Cells[0].Value ?? (object)"").ToString();
                        weight = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[1].Value ?? (object)"").ToString());
                        Tunch1 = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[2].Value ?? (object)"").ToString());
                        Tunch2 = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[3].Value ?? (object)"").ToString());
                        Fine = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[4].Value ?? (object)"").ToString());
                        if (weight > 0)
                        {
                            cmd.CommandText = "INSERT INTO KfDetails(MetalCategory,MetalName,PaatNo,Weight,Tunch1,Tunch2,Fine,TranType,Company,UserId)VALUES('" + lblKFCate.Text.Trim() + "','" + lblKFName.Text.Trim() + "','" + strPaatNo + "','" + weight + "','" + Tunch1 + "','" + Tunch2 + "','" + Fine + "','CKF','" + CommanHelper.CompName + "','" + txtUserId.Text.Trim() + "')";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    Tran.Commit();
                    con2.Close();

                    MessageBox.Show("Company Created Successfully.", "Company", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("Do U Want To Login Into New Created Company ?", "Company", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Master.objMaster.Hide();
                        Login oLogin = new Login(); oLogin.Show();
                        oLogin.txtUserId.Focus();
                    }
                    ClearControl();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                Tran.Rollback();
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

                cmd.CommandText = "Delete From Metal";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From KfDetails Where TranType='CKF' And Company = '" + CommanHelper.CompName + "'";
                cmd.ExecuteNonQuery();

                //----------Company Opening Information
                foreach (var item in MetalList)
                {
                    String MetalCat = "";
                    String MetalName = "";
                    String WeightType = "";
                    String KF = "";
                    Decimal Amt_Weight = 0;

                    String DrCr = "";
                    Decimal _Credit = 0;
                    Decimal _Debit = 0;

                    MetalCat = (item.MetalCategory ?? (object)"").ToString();
                    MetalName = (item.MetalName ?? (object)"").ToString();
                    WeightType = (item.WieghtType ?? (object)"").ToString();
                    KF = (item.KachchiFine ?? (object)"").ToString();

                    Amt_Weight = Conversion.ConToDec6(item.AmountWeight.ToString());
                    DrCr = item.DrCr.ToString();
                    if (DrCr.Trim() == "CREDIT")
                    {
                        _Credit = Amt_Weight;
                    }
                    if (DrCr.Trim() == "DEBIT")
                    {
                        _Debit = Amt_Weight;
                    }

                    if (DrCr != "")
                    {
                        cmd.CommandText = "INSERT INTO CompanyOpening(MetalName,Amount_Weight,DrCr,CompanyName,UserId)VALUES('" + MetalName + "','" + Amt_Weight + "','" + DrCr + "','" + CommanHelper.CompName + "','" + txtUserId.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();

                        //--------Insert Data In PartyTran Table
                        cmd.CommandText = "INSERT INTO PartyTran(TrDate,PartyName,MetalCategory,MetalName,Debit,Credit,Weight,TranType)VALUES('" + _FYFrom + "','" + txtCompanyName.Text.Trim() + "','" + MetalCat + "','" + MetalName + "','" + _Debit + "','" + _Credit + "','" + Amt_Weight + "','CO')";
                        cmd.ExecuteNonQuery();
                    }

                    //---------Insert Data Metal
                    if (MetalCat != "" && MetalName != "" && KF != "")
                    {
                        cmd.CommandText = "INSERT INTO Metal(MetalCategory,MetalName,WieghtType,KachchiFine,CompanyName)VALUES('" + MetalCat + "','" + MetalName + "','" + WeightType + "','" + KF + "','" + (item.CompanyName ?? (object)"").ToString() + "')";
                        cmd.ExecuteNonQuery();

                    }
                }

                //-----Insert KF details
                for (int k = 0; k < dataGridView2.Rows.Count - 1; k++)
                {
                    String strPaatNo = "";
                    Decimal weight = 0;
                    Decimal Tunch1 = 0;
                    Decimal Tunch2 = 0;
                    Decimal Fine = 0;

                    strPaatNo = (dataGridView2.Rows[k].Cells[0].Value ?? (object)"").ToString();
                    weight = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[1].Value ?? (object)"").ToString());
                    Tunch1 = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[2].Value ?? (object)"").ToString());
                    Tunch2 = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[3].Value ?? (object)"").ToString());
                    Fine = Conversion.ConToDec6((dataGridView2.Rows[k].Cells[4].Value ?? (object)"").ToString());
                    if (weight > 0)
                    {
                        cmd.CommandText = "INSERT INTO KfDetails(MetalCategory,MetalName,PaatNo,Weight,Tunch1,Tunch2,Fine,TranType,Company,UserId)VALUES('" + lblKFCate.Text.Trim() + "','" + lblKFName.Text.Trim() + "','" + strPaatNo + "','" + weight + "','" + Tunch1 + "','" + Tunch2 + "','" + Fine + "','CKF','" + CommanHelper.CompName + "','" + txtUserId.Text.Trim() + "')";
                        cmd.ExecuteNonQuery();
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

        private void txtFinancialYear_KeyPress(object sender, KeyPressEventArgs e)
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
                    }
                    dataGridView1.Focus();
                }
            }
            else
            {
                e.Handled = true;
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
                    }
                }
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
                if (dataGridView1.Rows.Count - 1 == dataGridView1.CurrentCell.RowIndex && e.ColumnIndex == 1)
                {
                    if ((dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString() == "" && (dataGridView1.CurrentRow.Cells[1].Value ?? (object)"").ToString() == "")
                    {
                        if (F_Update == 1)
                        {
                            btnupdate.Focus();
                        }
                        else
                        {
                            btnCreate.Focus();
                        }
                    }
                }
                
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
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            int_keyvalue = e.KeyValue;
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    //----------Check KF Exist Than Delete Opening Data From KF Table
                    if (MetalList.Where(x => x.MetalName == dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim() && x.KachchiFine == "Y").Any())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        OleDbCommand cmd = new OleDbCommand("Delete From KfDetails Where TranType = 'CKF'", con);
                        cmd.ExecuteNonQuery();
                        dataGridView2.DataSource = null;
                        dataGridView2.Rows.Clear();
                    }

                    //--------Delete Data from Metal List
                    var result = (from r in MetalList
                                  where r.MetalCategory == (dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString() &&
                                      r.MetalName == (dataGridView1.CurrentRow.Cells[1].Value ?? (object)"").ToString()
                                  select r).SingleOrDefault();
                    if (result != null)
                        MetalList.Remove(result);

                  
                }
                catch (Exception ex)
                {
                    ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
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
                            tWeight.DataSource = MetalList.Where(r => r.MetalName == _MetalNameCellValue.Trim()).OrderBy(z => z.WieghtType).Select(x => x.WieghtType).Distinct().ToList();

                            if (MetalList.Where(r => r.MetalName == _MetalNameCellValue.Trim() && r.KachchiFine == "Y").Select(x => x.KachchiFine).Distinct().Any())
                            {
                                DataGridViewComboBoxCell tKF = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[3];
                                tKF.DataSource = MetalList.Where(r => r.MetalName == _MetalNameCellValue.Trim()).Select(x => x.KachchiFine).Distinct().ToList();
                              //  tKF.Value = 0;                       
                            }
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
                txtPassword.Focus();
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

        private static void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = e.KeyChar.ToString().ToUpper();
            char[] ch = str.ToCharArray();
            e.KeyChar = ch[0];

        }
        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (s != null && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                e.Handled = s.Text.Contains(e.KeyChar);
            }
            else
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == col2.Index || dataGridView2.CurrentCell.ColumnIndex == col3.Index ||
                dataGridView2.CurrentCell.ColumnIndex == col4.Index || dataGridView2.CurrentCell.ColumnIndex == col5.Index || 
                dataGridView2.CurrentCell.ColumnIndex == col6.Index)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //-------------Metal Category

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
                            oMetal.WieghtType = "";
                            oMetal.Sno = max;
                            oMetal.CompanyName = txtCompanyName.Text.Trim();
                            MetalList.Add(oMetal);
                        }
                    }

                    _MetalCateCellValue = e.FormattedValue.ToString();
                    DataGridViewComboBoxCell t1 = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[0];
                    t1.DataSource = MetalList.Select(r => r.MetalCategory).Distinct().ToList();
                    dataGridView1.CurrentRow.Cells[0].Value = e.FormattedValue.ToString();
                }

                //-------------Metal Name
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
                                    var max = 0;
                                    if (result.Count > 0) { max = MetalList.Max(x => x.Sno) + 1; }
                                    MetalEntity oMetal = new MetalEntity();
                                    oMetal.MetalCategory = _MetalCateCellValue;
                                    oMetal.MetalName = e.FormattedValue.ToString();
                                    oMetal.WieghtType = "";
                                    oMetal.Sno = max;
                                    oMetal.CompanyName = txtCompanyName.Text.Trim(); MetalList.Add(oMetal);
                                }
                                else
                                {
                                    if (item.MetalName == "")
                                    {
                                        var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                        update.MetalName = e.FormattedValue.ToString();
                                        update.KachchiFine = "N";
                                    }
                                }
                            }

                            _MetalNameCellValue = e.FormattedValue.ToString();
                            DataGridViewComboBoxCell t2 = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[1];
                            t2.DataSource = MetalList.Where(x => x.MetalCategory == _MetalCateCellValue).Select(r => r.MetalName).Distinct().ToList();
                            dataGridView1.CurrentRow.Cells[1].Value = e.FormattedValue.ToString();
                        }
                    }

                }

                //-------------Metal Weight Type
                if (dataGridView1.CurrentCellAddress.X == col_Weight.DisplayIndex)
                {
                    if (!col_Weight.Items.Contains(e.FormattedValue) && e.FormattedValue.ToString() != "")
                    {
                        if (dataGridView1.CurrentRow.Cells[1].Value != null)
                        {
                            var result = (from r in MetalList where r.MetalName == _MetalNameCellValue.Trim() select r).ToList();
                            foreach (var item in result)
                            {
                                if (!MetalList.Where(x => x.MetalName == _MetalNameCellValue.Trim() && x.WieghtType == e.FormattedValue.ToString()).Any())
                                {
                                    var max = 0;
                                    if (result.Count > 0) { max = MetalList.Max(x => x.Sno) + 1; }
                                    MetalEntity oMetal = new MetalEntity();
                                    oMetal.MetalCategory = "";
                                    oMetal.MetalName = _MetalNameCellValue;
                                    oMetal.WieghtType = e.FormattedValue.ToString();
                                    oMetal.Sno = max;
                                    oMetal.CompanyName = txtCompanyName.Text.Trim();
                                    MetalList.Add(oMetal);
                                }
                                else
                                {
                                    if (item.WieghtType == "")
                                    {
                                        var update = (from r in MetalList where r.Sno == item.Sno select r).FirstOrDefault();
                                        update.WieghtType = e.FormattedValue.ToString();
                                        update.KachchiFine = "N";
                                    }
                                }
                            }

                            _MetalNameCellValue = e.FormattedValue.ToString();
                            DataGridViewComboBoxCell tWeight = (DataGridViewComboBoxCell)dataGridView1.CurrentRow.Cells[2];
                            tWeight.DataSource = MetalList.Select(r => r.WieghtType).Distinct().ToList();
                            dataGridView1.CurrentRow.Cells[2].Value = e.FormattedValue.ToString();
                        }
                    }

                }
                if (dataGridView1.CurrentRow.Cells[5].Value == null)
                {
                    dataGridView1.CurrentRow.Cells[5].Value = "CREDIT";
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
                    if ((MetalList.Where(x => x.MetalName == dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim() && x.KachchiFine == "Y").Any()) && e.ColumnIndex == 4)
                    {
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
                        dataGridView2.Focus();
                        Total();
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
                if ((dataGridView1.CurrentRow.Cells[0].Value ?? (object)"").ToString().ToUpper() == "CASH" && e.ColumnIndex == 2)
                {
                    dataGridView1.CurrentCell.ReadOnly = true;
                }
                if (e.ColumnIndex == 1)
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
                        if (MetalList.Where(x => x.MetalName == _MetalNameCellValue.Trim() && x.WieghtType != "").Select(r => r.WieghtType).Distinct().Any())
                        {
                            cmbWeight.DataSource = MetalList.Where(x => x.MetalName == _MetalNameCellValue.Trim()).Select(r => r.WieghtType).Distinct().ToList();
                        }
                        else
                        {
                            cmbWeight.DataSource = MetalList.OrderBy(z => z.WieghtType).Select(r => r.WieghtType).Distinct().ToList();
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
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (CommanHelper.CompName.Trim().ToString() == dataGridView3.CurrentRow.Cells[0].Value.ToString().Trim())
                {
                    _CompName = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                    BindCompany();
                }
            }
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Total();
        }

        private void toolStripMenu_CompanyDetails_Click(object sender, EventArgs e)
        {
            dataGridView3.Focus();
        }

    }
}
