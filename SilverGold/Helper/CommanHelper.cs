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
                    OleDbCommand cmd = new OleDbCommand("Select MetalCategory,MetalName,WieghtType,KachchiFine,Sno,CompanyName,UserId From Metal", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MetalEntity oMetal = new MetalEntity();
                        oMetal.MetalName = dr["MetalName"].ToString();
                        oMetal.MetalCategory = dr["MetalCategory"].ToString();
                        oMetal.WieghtType = dr["WieghtType"].ToString();
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


        public static List<MetalEntity> GetCompanyMetal()
        {
            List<MetalEntity> MetalList = new List<MetalEntity>();
            
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select MetalCategory,[Metal.MetalName] AS MetalName,WieghtType,KachchiFine,Amount_Weight,DrCr,[CompanyOpening.CompanyName] AS CompanyName,[CompanyOpening.UserId] AS UserId,[Metal.Sno] AS Sno  from Metal LEFT OUTER JOIN CompanyOpening ON Metal.MetalName  = CompanyOpening.MetalName", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MetalEntity oMetal = new MetalEntity();
                        oMetal.MetalName = dr["MetalName"].ToString().Trim();
                        oMetal.MetalCategory = dr["MetalCategory"].ToString().Trim();
                        oMetal.WieghtType = dr["WieghtType"].ToString().Trim();
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
    }
}