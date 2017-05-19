using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class KFEntity
    {
        public string BillNo { get; set; }
        public DateTime TrDate { get; set; }
        public string MetalCategory { get; set; }
        public string MetalName { get; set; }
        public string PaatNo { get; set; }
        public decimal Weight { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Fine { get; set; }
        public string TranType { get; set; }
        public string Narration { get; set; }
        public int Sno { get; set; }
        public string YN { get; set; }
        public int KF_Sno { get; set; }
        public DateTime KF_DateR { get; set; }
        public DateTime KF_DateP { get; set; }
    }


    class KFOpeningEntity
    {
        public string MetalCategory { get; set; }
        public string MetalName { get; set; }
        public string PaatNo { get; set; }
        public decimal Weight { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Fine { get; set; }
        public int Sno { get; set; }
     
    }


    public static class KFFactory
    {

        public static void Insert(String _MetalCategory, String _MetalName, String _PaatNo, Decimal _Weight, Decimal _Tunch1, Decimal _Tunch2, Decimal _Fine, String _TranType, String _YN, DateTime _KF_DateR, String _Company, String _UserId, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO KfDetails(MetalCategory,MetalName,PaatNo,Weight,Tunch1,Tunch2,Fine,TranType,YN,KF_DateR,Company,UserId)VALUES(@MetalCategory,@MetalName,@PaatNo,@Weight,@Tunch1,@Tunch2,@Fine,@TranType,@YN,@KF_DateR,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@MetalCategory", _MetalCategory);
            cmdInsert.Parameters.AddWithValue("@MetalName", _MetalName);
            cmdInsert.Parameters.AddWithValue("@PaatNo", _PaatNo);
            cmdInsert.Parameters.AddWithValue("@Weight", _Weight);
            cmdInsert.Parameters.AddWithValue("@Tunch1", _Tunch1);
            cmdInsert.Parameters.AddWithValue("@Tunch2", _Tunch2);
            cmdInsert.Parameters.AddWithValue("@Fine", _Fine);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@YN", _YN);
            cmdInsert.Parameters.AddWithValue("@KF_DateR", _KF_DateR);
            cmdInsert.Parameters.AddWithValue("@Company", _Company);
            cmdInsert.Parameters.AddWithValue("@UserId", _UserId);
            cmdInsert.ExecuteNonQuery();
        }

        public static void BindKFColumn(DataGridView dgv)
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "PaatNo";
            col1.HeaderText = "PaatNo";
            col1.Name = "PaatNo";
            dgv.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Weight";
            col2.Name = "Weight";
            dgv.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "Tunch1";
            col3.HeaderText = "Tunch1";
            col3.Name = "Tunch1";
            dgv.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "Tunch2";
            col4.HeaderText = "Tunch2";
            col4.Name = "Tunch2";
            col4.DefaultCellStyle.Format = "N2";
            dgv.Columns.Add(col4);

            DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "Fine";
            col5.HeaderText = "Fine";
            col5.Name = "Fine";
            dgv.Columns.Add(col5);

            DataGridViewColumn col6 = new DataGridViewTextBoxColumn();
            col6.DataPropertyName = "Sno";
            col6.HeaderText = "Sno";
            col6.Name = "Sno";
            col6.Visible = false;
            dgv.Columns.Add(col6);
        }

        public static void BindKFColumnCheckBox(DataGridView dgv)
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "PaatNo";
            col1.HeaderText = "PaatNo";
            col1.Name = "PaatNo";
            dgv.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Weight";
            col2.Name = "Weight";
            dgv.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "Tunch1";
            col3.HeaderText = "Tunch1";
            col3.Name = "Tunch1";
            dgv.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "Tunch2";
            col4.HeaderText = "Tunch2";
            col4.Name = "Tunch2";
            col4.DefaultCellStyle.Format = "N2";
            dgv.Columns.Add(col4);

            DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "Fine";
            col5.HeaderText = "Fine";
            col5.Name = "Fine";
            dgv.Columns.Add(col5);

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            dgv.Columns.Add(chk);
            chk.HeaderText = "YN";
            chk.Name = "YN";
            
            DataGridViewColumn col6 = new DataGridViewTextBoxColumn();
            col6.DataPropertyName = "Sno";
            col6.HeaderText = "Sno";
            col6.Name = "Sno";
            col6.Visible = false;
            dgv.Columns.Add(col6);
        }

        public static void SetKF_ColumnWidth(DataGridView grd)
        {
            grd.Columns["Weight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Tunch1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Tunch2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Fine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}
