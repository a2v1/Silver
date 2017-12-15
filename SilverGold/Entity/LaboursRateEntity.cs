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
    class LaboursRateEntity
    {
        public CalendarColumn dtpDateFrom_LabourRate = new CalendarColumn();
        public CalendarColumn dtpDateTo_LabourRate = new CalendarColumn();
        public DataGridViewComboBoxColumn col_WtPcs_LabourRate = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Cate_LabourRate = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_Product_LabourRate = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_FineAmnt_LabourRate = new DataGridViewComboBoxColumn();
        public DataGridViewTextBoxColumn col_LRate_LabourRate = new DataGridViewTextBoxColumn();
        public DataGridViewComboBoxColumn col_PType_LabourRate = new DataGridViewComboBoxColumn();
        public DataGridViewComboBoxColumn col_JN_LabourRate = new DataGridViewComboBoxColumn();

        public void BindLabourRate(DataGridView dgv, String _Category)
        {
            try
            {
                dgv.Columns.Clear();

                dtpDateFrom_LabourRate.DataPropertyName = "DateFrom";
                dtpDateFrom_LabourRate.HeaderText = "DateFrom";
                dtpDateFrom_LabourRate.Name = "DateFrom";
                dgv.Columns.Add(dtpDateFrom_LabourRate);

                dtpDateTo_LabourRate.DataPropertyName = "DateTo";
                dtpDateTo_LabourRate.HeaderText = "DateTo";
                dtpDateTo_LabourRate.Name = "DateTo";
                dgv.Columns.Add(dtpDateTo_LabourRate);


                col_WtPcs_LabourRate.DataPropertyName = "WeightPcs";
                col_WtPcs_LabourRate.HeaderText = "WT/PCS";
                col_WtPcs_LabourRate.Name = "WeightPcs";
                col_WtPcs_LabourRate.Items.Clear();
                col_WtPcs_LabourRate.Items.Add("WEIGHT");
                col_WtPcs_LabourRate.Items.Add("PCS");
                col_WtPcs_LabourRate.FlatStyle = FlatStyle.Popup;
                dgv.Columns.Add(col_WtPcs_LabourRate);

                col_Cate_LabourRate.DataPropertyName = "Category";
                col_Cate_LabourRate.HeaderText = "Category";
                col_Cate_LabourRate.Name = "Category";

                if (_Category == "" || _Category == "COMMON")
                {
                    col_Cate_LabourRate.DataSource = CommanHelper.GetProduct().Select(x => x.Category).Distinct().ToList();
                }
                else
                {
                    col_Cate_LabourRate.DataSource = CommanHelper.GetProduct().Where(r => r.Category == _Category).Select(x => x.Category).Distinct().ToList();
                }

                col_Cate_LabourRate.FlatStyle = FlatStyle.Popup;
                dgv.Columns.Add(col_Cate_LabourRate);

                col_Product_LabourRate.DataPropertyName = "Product";
                col_Product_LabourRate.HeaderText = "Product";
                col_Product_LabourRate.Name = "Product";
                if (_Category == "" || _Category == "COMMON")
                {
                    col_Product_LabourRate.DataSource = CommanHelper.GetProduct().Select(x => x.ProductName).Distinct().ToList();
                }
                else
                {
                    col_Product_LabourRate.DataSource = CommanHelper.GetProduct().Where(r => r.Category == _Category).Select(x => x.ProductName).Distinct().ToList();
                }
                col_Product_LabourRate.FlatStyle = FlatStyle.Popup;
                dgv.Columns.Add(col_Product_LabourRate);

                col_FineAmnt_LabourRate.DataPropertyName = "Fine_Amount";
                col_FineAmnt_LabourRate.HeaderText = "FINE/AMT";
                col_FineAmnt_LabourRate.Name = "Fine_Amount";
                col_FineAmnt_LabourRate.Items.Clear();
                col_FineAmnt_LabourRate.Items.Add("AMOUNT");
                col_FineAmnt_LabourRate.Items.Add("FINE");
                col_FineAmnt_LabourRate.FlatStyle = FlatStyle.Popup;
                dgv.Columns.Add(col_FineAmnt_LabourRate);

                col_LRate_LabourRate.DataPropertyName = "LabourRate";
                col_LRate_LabourRate.HeaderText = "LRate";
                col_LRate_LabourRate.Name = "LabourRate";                
                dgv.Columns.Add(col_LRate_LabourRate);

                col_PType_LabourRate.DataPropertyName = "PayRate";
                col_PType_LabourRate.HeaderText = "Pay Type";
                col_PType_LabourRate.Name = "PayRate";
                col_PType_LabourRate.Items.Clear();
                col_PType_LabourRate.Items.Add("GIVING");
                col_PType_LabourRate.Items.Add("RECIEVING");
                col_PType_LabourRate.Items.Add("COMMON");
                col_PType_LabourRate.FlatStyle = FlatStyle.Popup;
                dgv.Columns.Add(col_PType_LabourRate);

                col_JN_LabourRate.DataPropertyName = "JamaNaam";
                col_JN_LabourRate.HeaderText = "J/N";
                col_JN_LabourRate.Name = "JamaNaam";
                col_JN_LabourRate.Items.Clear();
                col_JN_LabourRate.Items.Add("J");
                col_JN_LabourRate.Items.Add("N");
                col_JN_LabourRate.FlatStyle = FlatStyle.Popup;
                dgv.Columns.Add(col_JN_LabourRate);

                var _FDate = Conversion.ConToDT(CommanHelper.FDate);
                var _TDate = Conversion.ConToDT(CommanHelper.TDate);

                dgv.Rows[0].Cells[0].Value = _FDate;
                dgv.Rows[0].Cells[1].Value = _TDate;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }

    public static class LaboursRateFactory
    {
        public static void Insert(String _PartyName, DateTime _DateFrom, DateTime _DateTo, String _WeightPcs, String _Category, String _Product, String _Fine_Amount, Decimal _LaboursRate, String _PayType, String _JamaNaam, OleDbConnection _Con, OleDbTransaction _Tran)
        {
            string strInsert = null;
            OleDbCommand cmdInsert = new OleDbCommand();
            strInsert = "INSERT INTO LaboursRate(PartyName,DateFrom,DateTo,WeightPcs,Category,Product,Fine_Amount,LaboursRate,PayType,JamaNaam,Company,UserId)VALUES(@PartyName,@DateFrom,@DateTo,@WeightPcs,@Category,@Product,@Fine_Amount,@LaboursRate,@PayType,@JamaNaam,@Company,@UserId)";
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
            cmdInsert.Parameters.AddWithValue("@LaboursRate", _LaboursRate);
            cmdInsert.Parameters.AddWithValue("@PayType", _PayType);
            cmdInsert.Parameters.AddWithValue("@JamaNaam", _JamaNaam);
            cmdInsert.Parameters.AddWithValue("@Company", CommanHelper.CompName.ToString());
            cmdInsert.Parameters.AddWithValue("@UserId", CommanHelper.UserId.ToString());
            cmdInsert.ExecuteNonQuery();
        }
    }
}
