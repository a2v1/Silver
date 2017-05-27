using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class RecieptPaymentVoucherEntity
    {
        public String Product { get; set; }
        public decimal Fine { get; set; }
        public decimal Premium { get; set; }
        public decimal FinePremium { get; set; }
        public decimal Amount { get; set; }
        public String Narration { get; set; }
        public String WeightRate { get; set; }
        public String GrossNet { get; set; }
        public int Sno { get; set; }

        public void AddRecieptPaymentVoucher(String _Product, decimal _Fine, decimal _Premium, decimal _FinePremium, decimal _Amount, String _Narration, String _WeightRate, String _GrossNet)
        {
            Product = _Product;
            Fine = _Fine;
            Premium = _Premium;
            FinePremium = _FinePremium;
            Amount = _Amount;
            Narration = _Narration;
            WeightRate = _WeightRate;
            GrossNet = _GrossNet;
        }

        public void BindGridColumn(DataGridView grd)
        {
            DataGridViewColumn col_Product = new DataGridViewTextBoxColumn();
            col_Product.DataPropertyName = "Product";
            col_Product.HeaderText = "Product";
            col_Product.Name = "Product";
            grd.Columns.Add(col_Product);

            DataGridViewColumn col_Fine = new DataGridViewTextBoxColumn();
            col_Fine.DataPropertyName = "Fine";
            col_Fine.HeaderText = "Fine";
            col_Fine.Name = "Fine";
            grd.Columns.Add(col_Fine);

            DataGridViewColumn col_Premium = new DataGridViewTextBoxColumn();
            col_Premium.DataPropertyName = "Premium";
            col_Premium.HeaderText = "Premium";
            col_Premium.Name = "Premium";
            grd.Columns.Add(col_Premium);

            DataGridViewColumn col_FinePremium = new DataGridViewTextBoxColumn();
            col_FinePremium.DataPropertyName = "FinePremium";
            col_FinePremium.HeaderText = "FinePremium";
            col_FinePremium.Name = "FinePremium";
            grd.Columns.Add(col_FinePremium);

            DataGridViewColumn col_Amount = new DataGridViewTextBoxColumn();
            col_Amount.DataPropertyName = "Amount";
            col_Amount.HeaderText = "Amount";
            col_Amount.Name = "Amount";
            grd.Columns.Add(col_Amount);

            DataGridViewColumn col_Narration = new DataGridViewTextBoxColumn();
            col_Narration.DataPropertyName = "Narration";
            col_Narration.HeaderText = "Narration";
            col_Narration.Name = "Narration";
            grd.Columns.Add(col_Narration);

            DataGridViewColumn col_WeightRate = new DataGridViewTextBoxColumn();
            col_WeightRate.DataPropertyName = "WeightRate";
            col_WeightRate.HeaderText = "Rs/Wt";
            col_WeightRate.Name = "WeightRate";
            grd.Columns.Add(col_WeightRate);

            DataGridViewColumn col_GrossNet = new DataGridViewTextBoxColumn();
            col_GrossNet.DataPropertyName = "GrossNet";
            col_GrossNet.HeaderText = "Gr/Net";
            col_GrossNet.Name = "GrossNet";
            grd.Columns.Add(col_GrossNet);


            DataGridViewColumn col_Sno = new DataGridViewTextBoxColumn();
            col_Sno.DataPropertyName = "Sno";
            col_Sno.HeaderText = "Sno";
            col_Sno.Name = "Sno";
            col_Sno.Visible = false;
            grd.Columns.Add(col_Sno);
        }


        public void SetGridView_ColumnWith(DataGridView grd)
        {
            grd.Columns["Product"].Width = 50;
            grd.Columns["Fine"].Width = 25;
            grd.Columns["Premium"].Width = 25;
            grd.Columns["FinePremium"].Width = 25;
            grd.Columns["Amount"].Width = 25;
            grd.Columns["Narration"].Width = 65;
            grd.Columns["WeightRate"].Width = 25;
            grd.Columns["GrossNet"].Width = 25;

            grd.Columns["Fine"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Premium"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["FinePremium"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grd.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

    }
}
