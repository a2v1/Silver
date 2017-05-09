using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class CashPurchaseSaleEntity
    {
        public string PGroup { get; set; }
        public string Product { get; set; }
        public decimal Weight { get; set; }
        public decimal Pcs { get; set; }
        public decimal Tunch1 { get; set; }
        public decimal Tunch2 { get; set; }
        public decimal Westage { get; set; }
        public decimal LaboursRate { get; set; }
        public decimal Fine { get; set; }
        public decimal Mcx { get; set; }
        public decimal Hazir { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public string GivingType { get; set; }
        public int Sno { get; set; }


        public void BindGridColumn(DataGridView grd)
        {

            DataGridViewColumn col_Group = new DataGridViewTextBoxColumn();
            col_Group.DataPropertyName = "PGroup";
            col_Group.HeaderText = "PGroup";
            col_Group.Name = "PGroup";
            grd.Columns.Add(col_Group);

            DataGridViewColumn col_Product = new DataGridViewTextBoxColumn();
            col_Product.DataPropertyName = "Product";
            col_Product.HeaderText = "Product";
            col_Product.Name = "Product";
            grd.Columns.Add(col_Product);

            DataGridViewColumn col_Weight = new DataGridViewTextBoxColumn();
            col_Weight.DataPropertyName = "Weight";
            col_Weight.HeaderText = "Weight";
            col_Weight.Name = "Weight";
            grd.Columns.Add(col_Weight);

            DataGridViewColumn col_Pcs = new DataGridViewTextBoxColumn();
            col_Pcs.DataPropertyName = "Pcs";
            col_Pcs.HeaderText = "Pcs";
            col_Pcs.Name = "Pcs";
            grd.Columns.Add(col_Pcs);

            DataGridViewColumn col_Tunch1 = new DataGridViewTextBoxColumn();
            col_Tunch1.DataPropertyName = "Tunch1";
            col_Tunch1.HeaderText = "Tunch";
            col_Tunch1.Name = "Tunch1";
            grd.Columns.Add(col_Tunch1);

            DataGridViewColumn col_Tunch2 = new DataGridViewTextBoxColumn();
            col_Tunch2.DataPropertyName = "Tunch2";
            col_Tunch2.HeaderText = "Tunch2";
            col_Tunch2.Name = "Tunch2";
            grd.Columns.Add(col_Tunch2);

            DataGridViewColumn col_Westage = new DataGridViewTextBoxColumn();
            col_Westage.DataPropertyName = "Westage";
            col_Westage.HeaderText = "Westage";
            col_Westage.Name = "Westage";
            grd.Columns.Add(col_Westage);

            DataGridViewColumn col_LabourFine = new DataGridViewTextBoxColumn();
            col_LabourFine.DataPropertyName = "LaboursRate";
            col_LabourFine.HeaderText = "LaboursFine";
            col_LabourFine.Name = "LaboursRate";
            grd.Columns.Add(col_LabourFine);

            DataGridViewColumn col_Fine = new DataGridViewTextBoxColumn();
            col_Fine.DataPropertyName = "Fine";
            col_Fine.HeaderText = "Fine";
            col_Fine.Name = "Fine";
            grd.Columns.Add(col_Fine);

            DataGridViewColumn col_Mcx = new DataGridViewTextBoxColumn();
            col_Mcx.DataPropertyName = "Mcx";
            col_Mcx.HeaderText = "MCX";
            col_Mcx.Name = "Mcx";
            grd.Columns.Add(col_Mcx);

            DataGridViewColumn col_Hazir = new DataGridViewTextBoxColumn();
            col_Hazir.DataPropertyName = "Hazir";
            col_Hazir.HeaderText = "Hazir";
            col_Hazir.Name = "Hazir";
            grd.Columns.Add(col_Hazir);

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

            DataGridViewColumn col_TunchSno = new DataGridViewTextBoxColumn();
            col_TunchSno.DataPropertyName = "GivingType";
            col_TunchSno.HeaderText = "Type";
            col_TunchSno.Name = "GivingType";
            grd.Columns.Add(col_TunchSno);

            DataGridViewColumn col_Sno = new DataGridViewTextBoxColumn();
            col_Sno.DataPropertyName = "Sno";
            col_Sno.HeaderText = "Sno";
            col_Sno.Name = "Sno";
            col_Sno.Visible = false;
            grd.Columns.Add(col_Sno);
        }

        public void SetCreditLimitGridView_ColumnWith(DataGridView grd)
        {
            grd.Columns["PGroup"].Width = 45;
            grd.Columns["Product"].Width = 100;
            grd.Columns["Weight"].Width = 70;
            grd.Columns["Pcs"].Width = 43;
            grd.Columns["Tunch1"].Width = 50;
            grd.Columns["Tunch2"].Width = 50;
            grd.Columns["Westage"].Width = 60;
            grd.Columns["LaboursRate"].Width = 130;
            grd.Columns["Fine"].Width = 75;
            grd.Columns["Mcx"].Width = 75;
            grd.Columns["Hazir"].Width = 75;
            grd.Columns["Amount"].Width = 100;
            grd.Columns["Narration"].Width = 100;
            grd.Columns["GivingType"].Width = 50;
        }
    }
}
