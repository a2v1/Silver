using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    class ReturnMetalEntity
    {
        public string BillNo { get; set; }
        public DateTime TrDate { get; set; }
        public string MetalCate { get; set; }
        public string MetalName { get; set; }
        public decimal Fine { get; set; }
        public decimal Premium { get; set; }
        public decimal FinePrem { get; set; }
        public string Narration { get; set; }
        public string WeightRate { get; set; }
        public string GrossNet { get; set; }
        public int Sno { get; set; }
    }


    public static class ReturnMetalFactory
    {
        public static void BindReturnMetalColumn(DataGridView dgv)
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "MetalName";
            col1.HeaderText = "Product";
            col1.Name = "MetalName";
            dgv.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Fine";
            col2.HeaderText = "Fine";
            col2.Name = "Fine";
            dgv.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "Premium";
            col3.HeaderText = "Premium";
            col3.Name = "Premium";
            dgv.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "FinePrem";
            col4.HeaderText = "FinePrem";
            col4.Name = "FinePrem";
            dgv.Columns.Add(col4);

            DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "Narration";
            col5.HeaderText = "Narration";
            col5.Name = "Narration";
            dgv.Columns.Add(col5);

            DataGridViewColumn col6 = new DataGridViewTextBoxColumn();
            col6.DataPropertyName = "WeightRate";
            col6.HeaderText = "Wt/Rs";
            col6.Name = "WeightRate";
            dgv.Columns.Add(col6);

            DataGridViewColumn col7 = new DataGridViewTextBoxColumn();
            col7.DataPropertyName = "GrossNet";
            col7.HeaderText = "Gr/Net";
            col7.Name = "GrossNet";
            dgv.Columns.Add(col7);

            DataGridViewColumn col8 = new DataGridViewTextBoxColumn();
            col8.DataPropertyName = "Sno";
            col8.HeaderText = "Sno";
            col8.Name = "Sno";
            col8.Visible = false;
            dgv.Columns.Add(col8);
        }
    }
}
