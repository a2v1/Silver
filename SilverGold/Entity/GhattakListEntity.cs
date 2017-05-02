using SilverGold.Comman;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class GhattakListEntity
    {

        public DataGridViewComboBoxColumn col_WtPcs_GhattakList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Cate_GhattakList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Product_GhattakList = new DataGridViewComboBoxColumn();
        public DataGridViewColumn col_Ghattk_GhattakList = new DataGridViewTextBoxColumn();
        public DataGridViewComboBoxColumn col_PType_GhattakList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_JN_GhattakList = new DataGridViewComboBoxColumn();
        CalendarColumn dtpDateFrom = new CalendarColumn();
        CalendarColumn dtpDateTo = new CalendarColumn();

        public void BindGhattakList(DataGridView dgv)
        {
            dtpDateFrom.DataPropertyName = "DateFrom";
            dtpDateFrom.HeaderText = "DateFrom";
            dtpDateFrom.Name = "DateFrom";
            dgv.Columns.Add(dtpDateFrom);

            dtpDateTo.DataPropertyName = "DateTo";
            dtpDateTo.HeaderText = "DateTo";
            dtpDateTo.Name = "DateTo";
            dgv.Columns.Add(dtpDateTo);

            col_WtPcs_GhattakList.DataPropertyName = "WeightPcs";
            col_WtPcs_GhattakList.HeaderText = "WT/PCS";
            col_WtPcs_GhattakList.Name = "WeightPcs";
            col_WtPcs_GhattakList.Items.Add("WEIGHT");
            col_WtPcs_GhattakList.Items.Add("PCS");
            col_WtPcs_GhattakList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_WtPcs_GhattakList);

            col_Cate_GhattakList.DataPropertyName = "Category";
            col_Cate_GhattakList.HeaderText = "Category";
            col_Cate_GhattakList.Name = "Category";
            col_Cate_GhattakList.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
            col_Cate_GhattakList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Cate_GhattakList);

            col_Product_GhattakList.DataPropertyName = "Product";
            col_Product_GhattakList.HeaderText = "Product";
            col_Product_GhattakList.Name = "Product";
            col_Product_GhattakList.DataSource = CommanHelper.GetProduct().Select(x => x.ProductName).Distinct().ToList();
            col_Product_GhattakList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Product_GhattakList);

            col_Ghattk_GhattakList.DataPropertyName = "Ghattak";
            col_Ghattk_GhattakList.HeaderText = "Ghattak";
            col_Ghattk_GhattakList.Name = "Ghattak";
            dgv.Columns.Add(col_Ghattk_GhattakList);

            col_PType_GhattakList.DataPropertyName = "PayType";
            col_PType_GhattakList.HeaderText = "PayType";
            col_PType_GhattakList.Name = "PayType";
            col_PType_GhattakList.Items.Add("GIVING");
            col_PType_GhattakList.Items.Add("RECIEVING");
            col_PType_GhattakList.Items.Add("COMMON");
            col_PType_GhattakList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_PType_GhattakList);

            col_JN_GhattakList.DataPropertyName = "Jama_Naam";
            col_JN_GhattakList.HeaderText = "J/N";
            col_JN_GhattakList.Name = "Jama_Naam";
            col_JN_GhattakList.Items.Add("JAMA");
            col_JN_GhattakList.Items.Add("NAAM");
            col_JN_GhattakList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_JN_GhattakList);
        }


    }

    public static class GhattakListFactory
    {

        public static void Insert(String _PartyName, DateTime _DateFrom, DateTime _DateTo, String _WeightPcs, String _Category, String _Product, Decimal _Ghattak, String _PayType, String _Jama_Naam, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO GhattakList(PartyName,DateFrom,DateTo,WeightPcs,Category,Product,Ghattak,PayType,Jama_Naam,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@WeightPcs,@Category,@Product,@Ghattak,@PayType,@Jama_Naam,@Company,@UserId)";
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Open();
                _Tran = _Con.BeginTransaction();
            }

            cmdInsert = new OleDbCommand(strInsert, _Con, _Tran);
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.Parameters.AddWithValue("@PartyName", _PartyName);
            cmdInsert.Parameters.AddWithValue("@DateFrom", _DateFrom);
            cmdInsert.Parameters.AddWithValue("@DateTo", _DateTo);
            cmdInsert.Parameters.AddWithValue("@WeightPcs", _WeightPcs);
            cmdInsert.Parameters.AddWithValue("@Category", _Category);
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@Ghattak", _Ghattak);
            cmdInsert.Parameters.AddWithValue("@PayType", _PayType);
            cmdInsert.Parameters.AddWithValue("@Jama_Naam", _Jama_Naam);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
