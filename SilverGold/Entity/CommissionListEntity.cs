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
    class CommissionListEntity
    {
        CalendarColumn dtpDateFrom_CommList = new CalendarColumn();
        CalendarColumn dtpDateTo_CommList = new CalendarColumn();
        public DataGridViewComboBoxColumn col_WtPcs_CommList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Cate_CommList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Product_CommList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_FineAmt_CommList = new DataGridViewComboBoxColumn();
        public DataGridViewColumn col_Com_CommList = new DataGridViewTextBoxColumn();
        public DataGridViewComboBoxColumn col_PType_CommList = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_JN_CommList = new DataGridViewComboBoxColumn();

        public void BindCommissionList(DataGridView dgv, String _Category)
        {
            dgv.Columns.Clear();
            dtpDateFrom_CommList.DataPropertyName = "DateFrom";
            dtpDateFrom_CommList.HeaderText = "DateFrom";
            dtpDateFrom_CommList.Name = "DateFrom";
            dgv.Columns.Add(dtpDateFrom_CommList);

            dtpDateTo_CommList.DataPropertyName = "DateTo";
            dtpDateTo_CommList.HeaderText = "DateTo";
            dtpDateTo_CommList.Name = "DateTo";
            dgv.Columns.Add(dtpDateTo_CommList);

            col_Cate_CommList.DataPropertyName = "Category";
            col_Cate_CommList.HeaderText = "Category";
            col_Cate_CommList.Name = "Category";
            if (_Category == "" || _Category == "COMMON")
            {
                col_Cate_CommList.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
            }
            else
            {
                col_Cate_CommList.DataSource = CommanHelper.GetProduct().Where(r => r.Category == _Category).Select(x => x.Category).Distinct().ToList();
            }
            col_Cate_CommList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Cate_CommList);

            col_WtPcs_CommList.DataPropertyName = "WeightPcs";
            col_WtPcs_CommList.HeaderText = "WT/PCS";
            col_WtPcs_CommList.Name = "WeightPcs";
            col_WtPcs_CommList.Items.Clear();
            col_WtPcs_CommList.Items.Add("WEIGHT");
            col_WtPcs_CommList.Items.Add("PCS");
            col_WtPcs_CommList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_WtPcs_CommList);

            col_Product_CommList.DataPropertyName = "Product";
            col_Product_CommList.HeaderText = "Product";
            col_Product_CommList.Name = "Product";

            List<MetalEntity> MetalList = new List<MetalEntity>();
            MetalList = CommanHelper.GetCompanyMetal().ToList();

            if (_Category == "" || _Category == "COMMON" || _Category == "MIX METAL")
            {
                foreach (var list in CommanHelper.GetProduct().Distinct().ToList())
                {
                    col_Product_CommList.Items.Add(list.ProductName.ToString());
                }

                foreach (var list in MetalList)
                {
                    if (list.UserId.ToString() != "" && list.MetalName != "CASH")
                    {
                        col_Product_CommList.Items.Add(list.MetalName.ToString());
                    }
                }
            }
            else
            {
                foreach (var list in CommanHelper.GetProduct().Where(r => r.Category == _Category).Distinct().ToList())
                {
                    col_Product_CommList.Items.Add(list.ProductName.ToString());
                }
                foreach (var list in MetalList)
                {
                    if (list.UserId.ToString() != "" && list.MetalName != "CASH" && list.MetalCategory.ToString().Trim() == _Category.Trim())
                    {
                        col_Product_CommList.Items.Add(list.MetalName.ToString());
                    }
                }
            }



            col_Product_CommList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_Product_CommList);


            col_FineAmt_CommList.DataPropertyName = "Fine_Amount";
            col_FineAmt_CommList.HeaderText = "FINE/AMT";
            col_FineAmt_CommList.Name = "Fine_Amount";
            col_FineAmt_CommList.Items.Clear();
            col_FineAmt_CommList.Items.Add("AMOUNT");
            col_FineAmt_CommList.Items.Add("FINE");
            col_FineAmt_CommList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_FineAmt_CommList);

            col_Com_CommList.DataPropertyName = "BrokerageRate";
            col_Com_CommList.HeaderText = "Rate";
            col_Com_CommList.Name = "BrokerageRate";
            dgv.Columns.Add(col_Com_CommList);

            col_PType_CommList.DataPropertyName = "PayType";
            col_PType_CommList.HeaderText = "PayType";
            col_PType_CommList.Name = "PayType";
            col_PType_CommList.Items.Clear();
            col_PType_CommList.Items.Add("GIVING");
            col_PType_CommList.Items.Add("RECIEVING");
            col_PType_CommList.Items.Add("BOTH");
            col_PType_CommList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_PType_CommList);

            col_JN_CommList.DataPropertyName = "JamaNaam";
            col_JN_CommList.HeaderText = "J/N";
            col_JN_CommList.Name = "JamaNaam";
            col_JN_CommList.Items.Clear();
            col_JN_CommList.Items.Add("J");
            col_JN_CommList.Items.Add("N");
            col_JN_CommList.FlatStyle = FlatStyle.Popup;
            dgv.Columns.Add(col_JN_CommList);

            var _FDate = Conversion.ConToDT(CommanHelper.FDate);
            var _TDate = Conversion.ConToDT(CommanHelper.TDate);

            dgv.Rows[0].Cells[0].Value = _FDate;
            dgv.Rows[0].Cells[1].Value = _TDate;
        }

    }

    public static class CommissionListFactory
    {
        public static void Insert(String _PartyName, DateTime _DateFrom, DateTime _DateTo, String _WeightPcs, String _Category, String _Product, String _Fine_Amount, Decimal _BrokerageRate, String _PayType, String _JamaNaam, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO CommissionList(PartyName,DateFrom,DateTo,WeightPcs,Category,Product,Fine_Amount,BrokerageRate,PayType,JamaNaam,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@WeightPcs,@Category,@Product,@Fine_Amount,@BrokerageRate,@PayType,@JamaNaam,@Company,@UserId)";
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
            cmdInsert.Parameters.AddWithValue("@Fine_Amount", _Fine_Amount);
            cmdInsert.Parameters.AddWithValue("@BrokerageRate", _BrokerageRate);
            cmdInsert.Parameters.AddWithValue("@PayType", _PayType);
            cmdInsert.Parameters.AddWithValue("@JamaNaam", _JamaNaam);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
