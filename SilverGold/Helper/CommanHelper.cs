using SilverGold.Comman;
using SilverGold.Entity;
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

namespace SilverGold.Helper
{
    static class CommanHelper
    {
        public static int FormX = 0;
        public static int FormY = 0;
        public static string CompName = "";
        public static string Com_DB_PATH = "";
        public static string Com_DB_NAME = "";
        public static string _FinancialYear = "";

        public static string FDate = "";
        public static string TDate = "";
        public static string UserId = "";
        public static string Password = "";
        public static int F_TunchPending = 0;

        public static string _CompName_ChangeComapny = "";
        public static string _Com_DB_PATH_ChangeComapny = "";
        public static string _Com_DB_NAME_ChangeComapny = "";
        public static string _FinancialYear_ChangeComapny = "";


        public static List<CompanyLoginEntity> CompanyLogin = new List<CompanyLoginEntity>();

        public static void IsNumericTextBox(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        public static string UpperCaseFirstCharacter(this string input)
        {
            return Regex.Replace(input.ToLower(), @"(?<=\b(?:mc|mac)?)[a-zA-Z](?<!'s\b)", m => m.Value.ToUpper()); ;
        }

        public static void ChangeGridFormate(DataGridView grd)
        {

            grd.EnableHeadersVisualStyles = false;
            grd.ColumnHeadersDefaultCellStyle.BackColor = Color.Wheat;
            grd.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            grd.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            grd.RowHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            // Change GridLine Color
            grd.GridColor = Color.Blue;
            // Change Grid Border Style
            grd.BorderStyle = BorderStyle.Fixed3D;
        }
        public static void ChangeGridFormate2(DataGridView grd)
        {
            grd.DefaultCellStyle.Font = new Font("Calibri", 9.0f, FontStyle.Regular);
            grd.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Regular);
            grd.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood;
            grd.EnableHeadersVisualStyles = false;
            grd.RowHeadersVisible = false;

            grd.BackgroundColor = Color.White;
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }



        public static decimal SumRow(DataGridView dgt, int col)
        {
            int count_row = 0;
            decimal sum_col, col1;
            sum_col = 0;
            col1 = 0;
            try
            {
                count_row = dgt.Rows.Count;
                for (int i = 0; i < count_row - 1; i++)
                {
                    col1 = Conversion.ConToDec5(dgt.Rows[i].Cells[col].Value.ToString());
                    sum_col = sum_col + col1;
                }
            }
            catch (Exception ee)
            {

            }
            return sum_col;
        }


        public static decimal SumRow1(DataGridView dgt, int col)
        {
            int count_row = 0;
            decimal sum_col, col1;
            sum_col = 0;
            col1 = 0;
            try
            {
                count_row = dgt.Rows.Count;
                for (int i = 0; i < count_row; i++)
                {
                    col1 = Conversion.ConToDec5(dgt.Rows[i].Cells[col].Value.ToString());
                    sum_col = sum_col + col1;
                }
            }
            catch (Exception ee)
            {

            }
            return sum_col;
        }

        public static decimal SumRowCustumDataGridView(GRIDVIEWCUSTOM1 dgt, int col)
        {
            int count_row = 0;
            decimal sum_col, col1;
            sum_col = 0;
            col1 = 0;
            try
            {
                count_row = dgt.Rows.Count;
                for (int i = 0; i < count_row - 1; i++)
                {
                    col1 = Conversion.ConToDec5(dgt.Rows[i].Cells[col].Value.ToString());
                    sum_col = sum_col + col1;
                }
            }
            catch (Exception ee)
            {

            }
            return sum_col;
        }

        public static List<MetalEntity> GetMetalCate(int strFlage)
        {
            List<MetalEntity> MetalList = new List<MetalEntity>();
            ConnectionClass objCon = new ConnectionClass();
            try
            {
                OleDbConnection con = new OleDbConnection();
                if (strFlage == 0)
                {
                    con.ConnectionString = objCon._CONSTR();
                }
                else
                {
                    if (CompName != "" && UserId != "")
                    {
                        con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
                    }
                }
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select MetalCategory,MetalName,WeightType,KachchiFine,Sno,CompanyName,UserId From Metal ORDER BY MetalCategory ASC", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MetalEntity oMetal = new MetalEntity();
                    oMetal.MetalName = dr["MetalName"].ToString();
                    oMetal.MetalCategory = dr["MetalCategory"].ToString();
                    oMetal.WeightType = dr["WeightType"].ToString();
                    oMetal.KachchiFine = dr["KachchiFine"].ToString();
                    oMetal.Sno = Conversion.ConToInt(dr["Sno"].ToString());
                    oMetal.CompanyName = dr["CompanyName"].ToString();
                    oMetal.UserId = dr["UserId"].ToString();
                    MetalList.Add(oMetal);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            } return MetalList;
        }


        public static void GetMetalCate_Account(ComboBox cmb)
        {
            ConnectionClass objCon = new ConnectionClass();
            try
            {
                using (OleDbConnection con = new OleDbConnection(objCon._CONSTR()))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalCategory) From Metal", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString().Trim());
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static void GetWeightType_Account(ComboBox cmb)
        {

            ConnectionClass objCon = new ConnectionClass();
            try
            {
                using (OleDbConnection con = new OleDbConnection(objCon._CONSTR()))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(WeightType) from Metal Where WeightType <> ''", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString().Trim());
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static List<MetalEntity> GetCompanyMetal()
        {
            List<MetalEntity> MetalList = new List<MetalEntity>();

            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select MetalCategory,[Metal.MetalName] AS MetalName,WeightType,KachchiFine,Weight,DrCr,[CompanyOpening.CompanyName] AS CompanyName,[CompanyOpening.UserId] AS UserId,[Metal.Sno] AS Sno  from Metal LEFT OUTER JOIN CompanyOpening ON Metal.MetalName  = CompanyOpening.MetalName ORDER BY MetalCategory ASC", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MetalEntity oMetal = new MetalEntity();
                        oMetal.MetalName = dr["MetalName"].ToString().Trim();
                        oMetal.MetalCategory = dr["MetalCategory"].ToString().Trim();
                        oMetal.WeightType = dr["WeightType"].ToString().Trim();
                        oMetal.KachchiFine = dr["KachchiFine"].ToString().Trim();
                        oMetal.DrCr = dr["DrCr"].ToString().Trim();
                        if (dr["WeightType"].ToString().Trim() == "KG")
                        {
                            oMetal.Weight = System.Math.Round(Conversion.ConToDec6(dr["Weight"].ToString()), 3);
                        }
                        else if (dr["WeightType"].ToString().Trim() == "GRMS")
                        {
                            oMetal.Weight = System.Math.Round(Conversion.ConToDec6(dr["Weight"].ToString()), 6);
                        }
                        else
                        {
                            oMetal.Weight = System.Math.Round(Conversion.ConToDec6(dr["Weight"].ToString()), 0);
                        }
                        oMetal.Sno = Conversion.ConToInt(dr["Sno"].ToString());
                        oMetal.CompanyName = dr["CompanyName"].ToString().Trim();
                        oMetal.UserId = dr["UserId"].ToString().Trim();

                        MetalList.Add(oMetal);
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            } return MetalList;
        }

        public static void GetMetalCategory(ComboBox cmb)
        {

            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalCategory) from Metal", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString().Trim());
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public static void GetWeightType(ComboBox cmb)
        {

            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(WeightType) from Metal Where WeightType <> ''", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString().Trim());
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



        public static List<OpeningMCXEntity> BindMCXDefaultOpening()
        {
            List<OpeningMCXEntity> OpeningMCXList = new List<OpeningMCXEntity>();
            try
            {
                OpeningMCXList.Clear();
                OpeningMCXList.Add(new OpeningMCXEntity
                {
                    OpeningDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    Item = "CASH",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity
                {
                    OpeningDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    Item = "SILVER",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity
                {
                    OpeningDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    Item = "SILVERM",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity
                {
                    OpeningDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    Item = "GOLD",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity
                {
                    OpeningDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    Item = "GOLDM",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return OpeningMCXList;
        }

        public static void NumericCheck(object sender, KeyPressEventArgs e)
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

        public static void FillCreditLimitOpening(DataGridView gdv, String _str)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    String _Query = "";
                    if (_str != "")
                    {
                        if (_str == "COMMON")
                            _Query = "Select Distinct(MetalCategory) from Metal";
                        else
                            _Query = "Select Distinct(MetalCategory) from Metal Where MetalCategory IN ('" + _str + "','CASH')";
                    }
                    else
                    {
                        _Query = "Select Distinct(MetalCategory) from Metal Where MetalCategory=''";
                    }
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand(_Query, con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    List<CreditLimitOpeningEntity> CreditLimitOpeningList = new List<CreditLimitOpeningEntity>();
                    while (dr.Read())
                    {
                        CreditLimitOpeningEntity oCreditLimitOpeningEntity = new CreditLimitOpeningEntity();
                        oCreditLimitOpeningEntity.Name = dr[0].ToString();
                        oCreditLimitOpeningEntity.Limit = 0;
                        oCreditLimitOpeningEntity.JN = "JAMA";
                        CreditLimitOpeningList.Add(oCreditLimitOpeningEntity);
                    }
                    gdv.DataSource = CreditLimitOpeningList.ToList();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static List<OpeningOtherEntity> OpeningOther()
        {
            List<OpeningOtherEntity> OpeningOtherList = new List<OpeningOtherEntity>();
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalCategory) from CompanyOpening LEFT OUTER JOIN Metal  ON CompanyOpening.MetalName   =  Metal.MetalName  ORDER BY MetalCategory ASC", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        OpeningOtherEntity oOpeningOtherEntity = new OpeningOtherEntity();
                        oOpeningOtherEntity.OpeningDate = DateTime.Now.ToString("dd/MM/yyyy");
                        oOpeningOtherEntity.Item = dr[0].ToString();
                        oOpeningOtherEntity.Weight = 0;
                        oOpeningOtherEntity.DrCr = "";
                        oOpeningOtherEntity.Narration = "";
                        OpeningOtherList.Add(oOpeningOtherEntity);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return OpeningOtherList;
        }


        public static void ComboBoxItem(ComboBox cmb, string tabName, string columName)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("select " + columName + " from " + tabName + " ", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString());
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        public static void ComboBoxItem(ComboBox cmb, string tabName, string columName, string Fcolumn, string Fvalue)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    String STRCOND = "  where iif(isnull(" + Fcolumn + "),''," + Fcolumn + ")='" + Fvalue + "' ";
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("select " + columName + " from " + tabName + " " + STRCOND + "", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString());
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void BindMetalCategory(ComboBox cmb)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalCategory) From Metal Where MetalCategory <> 'CASH' ORDER BY MetalCategory ASC", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString());
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void BindMetalName(ComboBox cmb, String _Category)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalName) From Metal Where MetalCategory = '" + _Category + "' ORDER BY MetalName ASC", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString());
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public static void BindPartyName(ComboBox cmb)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(PartyName) From PartyDetails ORDER BY PartyName ASC", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmb.Items.Clear();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[0].ToString());
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public static Boolean AlreadyExistParty(String strPartyName)
        {
            bool existingparty = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();

                    OleDbCommand cmd = new OleDbCommand("Select * from PartyDetails where PartyName='" + strPartyName.Trim() + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        existingparty = true;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return existingparty;
        }

        public static Boolean CheckKF(String strMetalName)
        {
            Boolean _CheckKF = false;
            ConnectionClass objCon = new ConnectionClass();
            try
            {
                using (OleDbConnection con = new OleDbConnection(objCon._CONSTR()))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select KachchiFine From Metal Where MetalName='" + strMetalName + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["KachchiFine"].ToString().Trim() == "Y")
                        {
                            _CheckKF = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return _CheckKF;
        }

        public static string FilterCompany(string str1, string str2)
        {
            string s1, s2;
            s1 = "";
            s2 = "";
            int len = str1.Length;
            int i;

            for (i = 0; i < len; i++)
            {
                s1 = str1.Substring(i, 1);
                if (s1 == str2)
                {
                    s2 = str1.Substring(0, i);
                }
            }
            i = len + 1;
            return s2;
        }



        public static List<OpeningMCXEntity> GetPartyOpeningMCX(String strPartyName)
        {
            List<OpeningMCXEntity> OpeningMCX = new List<OpeningMCXEntity>();
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select FORMAT(OpeningDate,'dd/MM/yyyy') AS OpeningDate,ItemName,Weight,ClosingRate,DrCr,Narration From PartyOpening Where PartyName = '" + strPartyName + "'  ORDER BY ItemName ASC", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OpeningMCXEntity oOpeningMCXEntity = new OpeningMCXEntity();
                    oOpeningMCXEntity.OpeningDate = dr["OpeningDate"].ToString();
                    oOpeningMCXEntity.Item = dr["ItemName"].ToString();
                    oOpeningMCXEntity.Weight = Conversion.ConToDec6(dr["Weight"].ToString());
                    oOpeningMCXEntity.Closing = Conversion.ConToDec(dr["ClosingRate"].ToString());
                    oOpeningMCXEntity.DrCr = dr["DrCr"].ToString();
                    oOpeningMCXEntity.Narration = dr["Narration"].ToString();
                    OpeningMCX.Add(oOpeningMCXEntity);
                }
                con.Close();
            }

            return OpeningMCX;
        }

        public static List<OpeningOtherEntity> GetPartyOpening(String strPartyName)
        {
            List<OpeningOtherEntity> OpeningOther = new List<OpeningOtherEntity>();
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select FORMAT(OpeningDate,'dd/MM/yyyy') AS OpeningDate,ItemName,Weight,DrCr,Narration From PartyOpening Where PartyName = '" + strPartyName + "' ORDER BY ItemName ASC", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OpeningOtherEntity oOpeningOtherEntity = new OpeningOtherEntity();
                    oOpeningOtherEntity.OpeningDate = dr["OpeningDate"].ToString();
                    oOpeningOtherEntity.Item = dr["ItemName"].ToString();
                    if (dr["ItemName"].ToString().Trim() == "CASH") { oOpeningOtherEntity.Weight = Conversion.ConToDec(dr["Weight"].ToString()); }
                    else
                    {
                        oOpeningOtherEntity.Weight = Conversion.ConToDec6(dr["Weight"].ToString());
                    }
                    oOpeningOtherEntity.DrCr = dr["DrCr"].ToString();
                    oOpeningOtherEntity.Narration = dr["Narration"].ToString();
                    OpeningOther.Add(oOpeningOtherEntity);
                }
                con.Close();
            }

            return OpeningOther;
        }

        public static List<CreditLimitOpeningEntity> GetCreditLimit(String strPartyName)
        {
            List<CreditLimitOpeningEntity> CreditLimitOpening = new List<CreditLimitOpeningEntity>();
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ItemName,ItemLimit,JN from CreditLimit Where PartyName = '" + strPartyName + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreditLimitOpeningEntity oCreditLimitOpeningEntity = new CreditLimitOpeningEntity();
                    oCreditLimitOpeningEntity.Name = dr["ItemName"].ToString();
                    if (dr["ItemName"].ToString().Trim() == "CASH")
                    {
                        oCreditLimitOpeningEntity.Limit = Conversion.ConToDec(dr["ItemLimit"].ToString());
                    }
                    else
                    { oCreditLimitOpeningEntity.Limit = Conversion.ConToDec6(dr["ItemLimit"].ToString()); }
                    oCreditLimitOpeningEntity.JN = dr["JN"].ToString();
                    CreditLimitOpening.Add(oCreditLimitOpeningEntity);
                }
                con.Close();
            }

            return CreditLimitOpening;
        }

        public static List<ProductEntity> GetProduct()
        {
            List<ProductEntity> ProductList = new List<ProductEntity>();
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select Category,Unit,Weight_Packet,ProductName,SubGroup,PGroup,Opening,Pcs,Tunch,Westage,LabourRate,Fine,Amount,RawDefine,OpenDate,Narration,Company,UserId From Product", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                ProductEntity oProduct;
                while (dr.Read())
                {
                    oProduct = new ProductEntity();
                    oProduct.AddProductEntity(dr["Category"].ToString(), dr["Unit"].ToString(), Conversion.ConToDec6(dr["Weight_Packet"].ToString()), dr["ProductName"].ToString(), dr["SubGroup"].ToString(), dr["PGroup"].ToString(), Conversion.ConToDec6(dr["Opening"].ToString()), Conversion.ConToDec6(dr["Pcs"].ToString()), Conversion.ConToDec6(dr["Tunch"].ToString()), Conversion.ConToDec6(dr["Westage"].ToString()), Conversion.ConToDec6(dr["LabourRate"].ToString()), Conversion.ConToDec6(dr["Fine"].ToString()), Conversion.ConToDec6(dr["Amount"].ToString()), dr["RawDefine"].ToString(), Conversion.ConToDT(dr["OpenDate"].ToString()), dr["Narration"].ToString(), dr["Company"].ToString(), dr["UserId"].ToString());
                    ProductList.Add(oProduct);
                }
                oProduct = new ProductEntity();
                oProduct.AddProductEntity("", "", 0, "", "", "", 0, 0, 0, 0, 0, 0, 0, "", Conversion.ConToDT(""), "", "", "");
                ProductList.Insert(0, oProduct);

                con.Close();
            }
            return ProductList;
        }


        public static void GetProduct(DataGridViewComboBoxCell cmb)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.DataSource = null;
                cmb.Items.Clear();
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProductCategoryWise(DataGridViewComboBoxCell cmb, String _Category)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product Where Category = '" + _Category + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.DataSource = null;
                cmb.Items.Clear();
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProduct_Worker(DataGridViewComboBoxColumn cmb)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.DataSource = null;
                cmb.Items.Clear();
                cmb.Items.Add("CASH");
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProductCategoryWise_Worker(DataGridViewComboBoxColumn cmb, String _Category)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product Where Category = '" + _Category + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.DataSource = null;
                cmb.Items.Clear();
                cmb.Items.Add("CASH");
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProduct_Worker(DataGridViewComboBoxCell cmb)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.DataSource = null;
                cmb.Items.Clear();
                cmb.Items.Add("CASH");
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProductCategoryWise_Worker(DataGridViewComboBoxCell cmb, String _Category)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product Where Category = '" + _Category + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.DataSource = null;
                cmb.Items.Clear();
                cmb.Items.Add("CASH");
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProductCategoryWise(ComboBox cmb, String _Category)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product Where Category = '" + _Category + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.Items.Clear();
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetProductCategory_GroupWise(ComboBox cmb, String _Category, String _Group)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select ProductName From Product Where Category = '" + _Category + "' And PGroup = '" + _Group + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.Items.Clear();
                cmb.Text = "";
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetParty(ComboBox cmb, String _Type)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                string _strcheck1 = "";
                string _strcheck2 = "";

                if (_Type != "")
                {
                    _Type = " Type =  '" + _Type + "'";
                    if (_Type != "CASH PURCHAGE")
                    {
                        _strcheck1 = "  and PartyName <>'CASH PURCHASE'";
                    }

                }
                else
                {
                    if (_Type != "CASH PURCHAGE")
                    {
                        _strcheck1 = " PartyName <>'CASH PURCHASE'";
                    }
                }
                if (_Type != "CASH SALE")
                {
                    _strcheck2 = "  and  PartyName <>'CASH SALE'";
                }



                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select PartyName From PartyDetails Where " + _Type + " " + _strcheck1 + "  " + _strcheck2 + " ORDER BY PartyName ASC", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.Items.Clear();
                while (dr.Read())
                {
                    cmb.Items.Add(dr["PartyName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetIntroducer(ComboBox cmb, String _StrPartyName)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                String _Str = "";
                if (_StrPartyName != "")
                {
                    _StrPartyName = " ,'" + _StrPartyName + "'";
                }
                _Str = " Where PartyName NOT IN('CASH PURCHASE','CASH SALE' " + _StrPartyName + ")";



                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select PartyName From PartyDetails " + _Str + " ORDER BY PartyName ASC", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.Items.Clear();
                while (dr.Read())
                {
                    cmb.Items.Add(dr["PartyName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static void GetCashParty(ComboBox cmb, String _CashType)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select PartyName From PartyDetails Where SubGroup = '" + _CashType + "' ORDER BY PartyName ASC", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                cmb.Items.Clear();
                while (dr.Read())
                {
                    cmb.Items.Add(dr["PartyName"].ToString().Trim());
                }
                con.Close();
            }
        }

        public static Boolean CheckMetalName(String strValue, DataGridView dgrd)
        {
            Boolean _CheckValue = false;
            for (int row = 0; row < dgrd.Rows.Count; row++)
            {
                if (dgrd.Rows[row].Cells[1].Value != null && dgrd.Rows[row].Cells[1].Value.Equals(strValue.Trim()))
                {
                    _CheckValue = true;
                }
                else
                {
                    //Add To datagridview
                }
            }
            return _CheckValue;
        }

        public static String GetColumnValue(String _ColName, String _TableName, String _ColType, String _FValue)
        {
            String _Str = "";
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                String _StrWhere = "";
                _StrWhere = "Where " + _ColType + " = '" + _FValue + "'";
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select " + _ColName + " From " + _TableName + " " + _StrWhere + "", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _Str = dr[0].ToString();
                }
                con.Close();
            }
            return _Str;
        }



        public static List<CreditPeriodEntity> GetCreditPeriod(String _PartyName)
        {
            List<CreditPeriodEntity> CreditPeriodList = new List<CreditPeriodEntity>();
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select PartyName,DateFrom,DateTo,RateRevised,Category,Product,Westage,Amount,Tran_Type,Days,Company,UserId From CreditPeriod Where PartyName = '" + _PartyName + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreditPeriodEntity oCreditPeriodEntity = new CreditPeriodEntity();
                    oCreditPeriodEntity.CreditPeriodMapper(dr["PartyName"].ToString(), Conversion.ConToDT(dr["DateFrom"].ToString()), Conversion.ConToDT(dr["DateTo"].ToString()),
                            dr["RateRevised"].ToString(), dr["Category"].ToString(), dr["Product"].ToString(), Conversion.ConToDec6(dr["Westage"].ToString()),
                            Conversion.ConToDec6(dr["Amount"].ToString()), dr["Tran_Type"].ToString(), Conversion.ConToInt(dr["Days"].ToString()), dr["Company"].ToString(), dr["UserId"].ToString());
                    CreditPeriodList.Add(oCreditPeriodEntity);
                }
                con.Close();
            }
            return CreditPeriodList;
        }




        public static Boolean CheckGram_Metal(String _Str)
        {
            Boolean CheckGrams_MG = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd_CheckGrams = new OleDbCommand("Select * from Metal where MetalName = '" + _Str + "' AND WeightType='GRMS'", con);
                    OleDbDataReader dr_CheckGrams = cmd_CheckGrams.ExecuteReader();
                    if (dr_CheckGrams.Read())
                    {
                        CheckGrams_MG = true;
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return CheckGrams_MG;
        }


        public static Boolean VarifiedValue(String _TabName, String _ColumName, String _Fcolumn, String _Fvalue, string _Curvalue)
        {
            Boolean _Varified = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    string _StrWhere = "";
                    if (_Fvalue.Trim() != "")
                    {
                        _StrWhere = "  Where iif(isnull(" + _Fcolumn + "),''," + _Fcolumn + ")='" + _Fvalue + "'";
                    }

                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select " + _ColumName + " from " + _TabName + " " + _StrWhere + "", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (_Curvalue == dr[0].ToString().Trim())
                        {
                            _Varified = true;
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return _Varified;
        }




        public static Boolean VarifiedValue(String _TabName, String _ColumName, String _curtext)
        {
            bool _Varified = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    string _StrWhere = "";
                    if (_curtext.Trim() != "")
                    {
                        _StrWhere = "  Where iif(isnull(" + _ColumName + "),''," + _ColumName + ")='" + _curtext + "'";
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("select * from " + _TabName + " " + _StrWhere + "", con);
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            _Varified = true;
                        }
                        dr.Close();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return _Varified;
        }


        public static String Pro_AutoCode(string tabName, string colName, string Fcolumn, string Fvalue)
        {
            string str = "";
            string strwhere = "";
            if (Fcolumn.Trim().Length > 0)
            {
                strwhere = "  where  " + Fcolumn + "='" + Fvalue + "'";
            }
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("select iif(isnull(max(cint( right(" + colName + ",len(" + colName + ")-1)))),0,max(cint( right(" + colName + ",len(" + colName + ")-1))))+1 from " + tabName + "  " + strwhere + " and " + colName + "<> null", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                dr.Read();
                str = dr[0].ToString();
                con.Close();
                if (str.Length == 1)
                {
                    str = "000" + str;
                }
                if (str.Length == 2)
                {
                    str = "00" + str;
                }
                if (str.Length == 3)
                {
                    str = "0" + str;
                }
            }
            return str;

        }


        public static int Get_Tunch_Sl_No(String trn_type)
        {
            int _Tunch_Sl_No = 0;

            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select  iif(isnull(max(TunchSNo)),0,max(TunchSNo))+1 as Sl_No from PartyTran Where TranType='" + trn_type + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _Tunch_Sl_No = Conversion.ConToInt(dr[0].ToString());
                }
                dr.Close();
                con.Close();
            }

            return _Tunch_Sl_No;

        }

        public static Boolean CheckTunchPending(String _PartyName, DateTime _Date)
        {
            Boolean _TunchPendingExist = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select * from TunchPending Where PartyName='" + _PartyName + "' And TrDate <=#" + _Date + "#", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if ((dr["Tunch1"].ToString().Trim() == "Y") || (dr["Tunch2"].ToString().Trim() == "Y"))
                        {
                            _TunchPendingExist = true;
                        }
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return _TunchPendingExist;
        }

        public static Boolean CheckTransaction()
        {
            Boolean _Check = false;
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select * from PartyTran Where TranType IN ('GR','GG')", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        _Check = true;
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return _Check;
        }

        public static void ShowFineCash(DataGridView dgv, int _Col_FineRs, int _col_WeightRs, Label _lblFine, Label _lblRs)
        {
            String _fine_string = "";
            String _Amount_string = "";
            Decimal _Fine = 0;
            Decimal _Rs = 0;
            for (int i = 0; i <= dgv.RowCount - 1; i++)
            {
                if (dgv.Rows[i].Cells[_col_WeightRs].Value.ToString().ToUpper() == "WEIGHT")
                {
                    decimal S = Conversion.ConToDec6((dgv.Rows[i].Cells[_Col_FineRs].Value ?? (object)"").ToString());
                    _Fine = _Fine + S;
                }
                if (dgv.Rows[i].Cells[_col_WeightRs].Value.ToString().ToUpper() == "RUPEES")
                {
                    decimal S1 = Conversion.ConToDec6((dgv.Rows[i].Cells[_Col_FineRs].Value ?? (object)"").ToString());
                    _Rs = _Rs + S1;
                }
            }
            _fine_string = (String.Format("{0:0.000}", _Fine)) + "(F)";
            if (_Rs != 0)
            {
                _lblRs.Text = _Rs.ToString();
                _Amount_string = _Rs.ToString() + "(R)";
            }
            if (_Rs == 0)
            {
                _lblRs.Text = "";
            }

            if ((_Rs == 0) && (_Fine == 0))
            {
                _lblFine.Text = "";
            }
            if ((_Rs != 0) && (_Fine == 0))
            {
                _lblFine.Text = _Amount_string;
            }
            if ((_Rs == 0) && (_Fine != 0))
            {
                _lblFine.Text = _fine_string;
            }
            if ((_Rs != 0) && (_Fine != 0))
            {
                _lblFine.Text = _fine_string + "/" + _Amount_string;
            }
        }

    }
}