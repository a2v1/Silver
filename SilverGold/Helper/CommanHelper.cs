using SilverGold.Comman;
using SilverGold.Entity;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Helper
{
    class CommanHelper
    {
        public static int FormX = 0;
        public static int FormY = 0;
        public static string CompName = "";
        public static string Com_DB_PATH = "";
        public static string Com_DB_NAME = "";

        public static string FDate = "";
        public static string TDate = "";
        public static string _FinancialYear = "";
        public static string UserId = "";
        public static string Password = "";

        public static List<CompanyLoginEntity> CompanyLogin = new List<CompanyLoginEntity>();

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
            grd.DefaultCellStyle.Font = new Font("Calibri", 10.25f, FontStyle.Regular);
            grd.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Regular);
            grd.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood;
            grd.EnableHeadersVisualStyles = false;
            grd.RowHeadersVisible = false;

            grd.BackgroundColor = Color.White;
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

        public static List<MetalEntity> GetMetalCate()
        {
            List<MetalEntity> MetalList = new List<MetalEntity>();
            ConnectionClass objCon = new ConnectionClass();
            try
            {
                using (OleDbConnection con = new OleDbConnection(objCon._CONSTR()))
                {
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
                    OleDbCommand cmd = new OleDbCommand("Select MetalCategory,[Metal.MetalName] AS MetalName,WeightType,KachchiFine,Amount_Weight,DrCr,[CompanyOpening.CompanyName] AS CompanyName,[CompanyOpening.UserId] AS UserId,[Metal.Sno] AS Sno  from Metal LEFT OUTER JOIN CompanyOpening ON Metal.MetalName  = CompanyOpening.MetalName ORDER BY MetalCategory ASC", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MetalEntity oMetal = new MetalEntity();
                        oMetal.MetalName = dr["MetalName"].ToString().Trim();
                        oMetal.MetalCategory = dr["MetalCategory"].ToString().Trim();
                        oMetal.WeightType = dr["WeightType"].ToString().Trim();
                        oMetal.KachchiFine = dr["KachchiFine"].ToString().Trim();
                        oMetal.DrCr = dr["DrCr"].ToString().Trim();
                        oMetal.AmountWeight = Conversion.ConToDec6(dr["Amount_Weight"].ToString()); 
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
                    Name = "SILVER",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity  
                {
                    Name = "SILVERM",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity 
                {
                    Name = "GOLD",
                    Weight = 0,
                    Closing = 0,
                    DrCr = ""
                });
                OpeningMCXList.Add(new OpeningMCXEntity 
                {
                    Name = "GOLDM",
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

        public static void FillCreditLimitOpening(DataGridView gdv)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalCategory) from Metal", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    gdv.Rows.Clear();
                    int Sno = 0;
                    while (dr.Read())
                    {
                        gdv.Rows.Add();
                        gdv.Rows[Sno].Cells[0].Value = dr[0].ToString();
                        Sno++;
                    }
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
                    OleDbCommand cmd = new OleDbCommand("Select Distinct(MetalCategory) from Metal  Where MetalCategory <> 'CASH'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();                  
                    while (dr.Read())
                    {
                        OpeningOtherEntity oOpeningOtherEntity = new OpeningOtherEntity();
                        oOpeningOtherEntity.Name = dr[0].ToString();
                        oOpeningOtherEntity.Amount = 0;
                        oOpeningOtherEntity.DrCr = "";
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
            catch (Exception exrr)
            {

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
            catch (Exception exrr)
            {

            }
        }

        public static void BindPartyCategory(ComboBox cmb)
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
                    cmb.Items.Add("OTHER");
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
                OleDbCommand cmd = new OleDbCommand("Select ItemName,Amount_Weight,ClosingRate,DrCr From PartyOpening Where PartyName = '" + strPartyName + "' And ItemName <> 'CASH'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OpeningMCXEntity oOpeningMCXEntity = new OpeningMCXEntity();
                    oOpeningMCXEntity.Name = dr["ItemName"].ToString();
                    oOpeningMCXEntity.Weight = Conversion.ConToDec6(dr["Amount_Weight"].ToString());
                    oOpeningMCXEntity.Closing = Conversion.ConToDec(dr["ClosingRate"].ToString());
                    oOpeningMCXEntity.DrCr = dr["DrCr"].ToString();
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
                OleDbCommand cmd = new OleDbCommand("Select ItemName,Amount_Weight,DrCr From PartyOpening Where PartyName = '" + strPartyName + "'  And ItemName <> 'CASH'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    OpeningOtherEntity oOpeningOtherEntity = new OpeningOtherEntity();
                    oOpeningOtherEntity.Name = dr["ItemName"].ToString();
                    oOpeningOtherEntity.Amount = Conversion.ConToDec6(dr["Amount_Weight"].ToString());
                    oOpeningOtherEntity.DrCr = dr["DrCr"].ToString();
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
                OleDbCommand cmd = new OleDbCommand("Select ItemName,ItemLimit from CreditLimit Where PartyName = '" + strPartyName + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreditLimitOpeningEntity oCreditLimitOpeningEntity = new CreditLimitOpeningEntity();
                    oCreditLimitOpeningEntity.Name = dr["ItemName"].ToString();
                    oCreditLimitOpeningEntity.Limit = Conversion.ConToDec6(dr["ItemLimit"].ToString());
                    CreditLimitOpening.Add(oCreditLimitOpeningEntity);
                }
                con.Close();
            }

            return CreditLimitOpening;
        }

        public static List<Product> GetProduct()
        {
            List<Product> ProductList = new List<Product>();
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select Category,Unit,Weight_Packet,ProductName,SubGroup,PGroup,Opening,Pcs,Tunch,Westage,LabourRate,Fine,Amount,RawDefine,OpenDate,Narration,Company,UserId From Product", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Product oProduct = new Product(dr["Category"].ToString(), dr["Unit"].ToString(), Conversion.ConToDec6(dr["Weight_Packet"].ToString()), dr["ProductName"].ToString(), dr["SubGroup"].ToString(), dr["PGroup"].ToString(), Conversion.ConToDec6(dr["Opening"].ToString()), Conversion.ConToDec6(dr["Pcs"].ToString()), Conversion.ConToDec6(dr["Tunch"].ToString()), Conversion.ConToDec6(dr["Westage"].ToString()), Conversion.ConToDec6(dr["LabourRate"].ToString()), Conversion.ConToDec6(dr["Fine"].ToString()), Conversion.ConToDec6(dr["Amount"].ToString()), dr["RawDefine"].ToString(), Conversion.ConToDT(dr["OpenDate"].ToString()), dr["Narration"].ToString(), dr["Company"].ToString(), dr["UserId"].ToString());
                    ProductList.Add(oProduct);
                }
                Product _Product = new Product("","",0, "ALL PRODUCT","","",0,0,0, 0, 0, 0, 0, "", Conversion.ConToDT(""), "", "", "");
                ProductList.Insert(0,_Product);
                
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
                cmb.Items.Add("ALL PRODUCT");
                while (dr.Read())
                {
                    cmb.Items.Add(dr["ProductName"].ToString().Trim());
                }     
                
                con.Close();
            }
        }

        public static void GetProductCategoryWise(DataGridViewComboBoxCell cmb,String _Category)
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

        public static void GetProductCategoryWise(ComboBox cmb, String _Category )
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

        public static void GetProductCategory_GroupWise(ComboBox cmb, String _Category,String _Group)
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

        public static void GetParty(ComboBox cmb,String _Type)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select PartyName From PartyDetails Where Type = '" + _Type + "' ORDER BY PartyName ASC", con);
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

        public static String GetProductValue(String _ColName , String _ProductName)
        {
            String _Str = "";
            using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("Select " + _ColName + " From Product Where ProductName = '" + _ProductName + "'", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
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
                    CreditPeriodEntity oCreditPeriodEntity = new CreditPeriodEntity(dr["PartyName"].ToString(), Conversion.ConToDT(dr["DateFrom"].ToString()), Conversion.ConToDT(dr["DateTo"].ToString()),
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

    }
}