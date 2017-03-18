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

        public static List<Metal> GetMetalCate()
        {
            List<Metal> MetalList = new List<Metal>();
            ConnectionClass objCon = new ConnectionClass();
            try
            {
                using (OleDbConnection con = new OleDbConnection(objCon._CONSTR()))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select MetalCategory,MetalName,WieghtType,KachchiFine,Sno From Metal", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Metal oMetal = new Metal();
                        oMetal.MetalName = dr["MetalName"].ToString();
                        oMetal.MetalCategory = dr["MetalCategory"].ToString();
                        oMetal.WieghtType = dr["WieghtType"].ToString();
                        oMetal.KachchiFine = dr["KachchiFine"].ToString();
                        oMetal.Sno = Conversion.ConToInt(dr["Sno"].ToString());
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
    }
}
