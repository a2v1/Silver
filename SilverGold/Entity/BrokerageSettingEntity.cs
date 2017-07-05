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
    class BrokerageSettingEntity
    {
        CalendarColumn dtpDateFrom_Brok = new CalendarColumn();
        CalendarColumn dtpDateTo_Brok = new CalendarColumn();
        public DataGridViewComboBoxColumn col_BrokType_Brok = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Product_Brok = new DataGridViewComboBoxColumn();
        public DataGridViewColumn col_BrokRate_Brok = new DataGridViewTextBoxColumn();
        public DataGridViewComboBoxColumn col_TranType_Brok = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_LotSet_Brok = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_PType_Brok = new DataGridViewComboBoxColumn();

        public void BindBrokerageList(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dtpDateFrom_Brok.DataPropertyName = "DateFrom";
            dtpDateFrom_Brok.HeaderText = "DateFrom";
            dtpDateFrom_Brok.Name = "DateFrom";
            dgv.Columns.Add(dtpDateFrom_Brok);

            dtpDateTo_Brok.DataPropertyName = "DateTo";
            dtpDateTo_Brok.HeaderText = "DateTo";
            dtpDateTo_Brok.Name = "DateTo";
            dgv.Columns.Add(dtpDateTo_Brok);

            col_BrokType_Brok.DataPropertyName = "BrokerageType";
            col_BrokType_Brok.HeaderText = "BrokType";
            col_BrokType_Brok.Name = "BrokerageType";
            col_BrokType_Brok.Items.Add("LotWise");
            col_BrokType_Brok.Items.Add("Turnover");
            col_BrokType_Brok.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_BrokType_Brok);

            col_Product_Brok.DataPropertyName = "Product";
            col_Product_Brok.HeaderText = "Product";
            col_Product_Brok.Name = "Product";
            col_Product_Brok.Items.Add("SILVER");
            col_Product_Brok.Items.Add("SILVERM");
            col_Product_Brok.Items.Add("GOLD");
            col_Product_Brok.Items.Add("GOLDM");
            col_Product_Brok.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Product_Brok);

            col_BrokRate_Brok.DataPropertyName = "BrokerageRate";
            col_BrokRate_Brok.HeaderText = "BrokRate";
            col_BrokRate_Brok.Name = "BrokerageRate";
            dgv.Columns.Add(col_BrokRate_Brok);

            col_TranType_Brok.DataPropertyName = "TranType";
            col_TranType_Brok.HeaderText = "TranType";
            col_TranType_Brok.Name = "TranType";
            col_TranType_Brok.Items.Add("JAMA");
            col_TranType_Brok.Items.Add("NAAM");
            col_TranType_Brok.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_TranType_Brok);

            col_LotSet_Brok.DataPropertyName = "LotSet";
            col_LotSet_Brok.HeaderText = "LotSet";
            col_LotSet_Brok.Name = "LotSet";
            col_LotSet_Brok.Items.Add("30");
            col_LotSet_Brok.Items.Add(".100");
            col_LotSet_Brok.Items.Add("1.000");
            col_LotSet_Brok.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_LotSet_Brok);

            col_PType_Brok.DataPropertyName = "PayType";
            col_PType_Brok.HeaderText = "P Type";
            col_PType_Brok.Name = "PayType";
            col_PType_Brok.Items.Add("PURCHASE");
            col_PType_Brok.Items.Add("SELL");
            col_PType_Brok.Items.Add("BOTH");
            col_PType_Brok.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_PType_Brok);

            var _FDate = Conversion.ConToDT(CommanHelper.FDate);
            var _TDate = Conversion.ConToDT(CommanHelper.TDate);

            dgv.Rows[0].Cells[0].Value = _FDate;
            dgv.Rows[0].Cells[1].Value = _TDate;
        }

    }

    public static class BrokerageSettingFactory
    {
        public static void Insert(String _PartyName, DateTime _DateFrom, DateTime _DateTo, String _BrokerageType, String _Category, String _Product, Decimal _BrokerageRate, String _TranType, Decimal _LotSet, String _PayType, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO BrokerageSetting(PartyName,DateFrom,DateTo,BrokerageType,Category,Product,BrokerageRate,TranType,LotSet,PayType,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@BrokerageType,@Category,@Product,@BrokerageRate,@TranType,@LotSet,@PayType,@Company,@UserId)";
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
            cmdInsert.Parameters.AddWithValue("@BrokerageType", _BrokerageType);
            cmdInsert.Parameters.AddWithValue("@Category", "");
            cmdInsert.Parameters.AddWithValue("@Product", _Product);
            cmdInsert.Parameters.AddWithValue("@BrokerageRate", _BrokerageRate);
            cmdInsert.Parameters.AddWithValue("@TranType", _TranType);
            cmdInsert.Parameters.AddWithValue("@LotSet", _LotSet);
            cmdInsert.Parameters.AddWithValue("@PayType", _PayType);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
