using SilverGold.Comman;
using SilverGold.Entity;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class PartyInformation : Form
    {
        #region Declare Variable

        OleDbConnection con;
        List<OpeningEntity> OpeningList = new List<OpeningEntity>();
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(CommanHelper.NumericCheck);
        DataGridViewColumn colLimit = new DataGridViewTextBoxColumn();
        #endregion
        public PartyInformation()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate2(dataGridView1);
            CommanHelper.ChangeGridFormate2(dataGridView2);
            BindCreditLimitOpeningColumn();
        }

        #region Helper

        private void BindCreditLimitOpeningColumn()
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dataGridView2.Columns.Add(col1);

            colLimit.DataPropertyName = "Limit";
            colLimit.HeaderText = "Limit";
            colLimit.Name = "Limit";
            dataGridView2.Columns.Add(colLimit);
        }

        private void BindOpeningMCXColumn()
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Weight";
            col2.HeaderText = "Weight";
            col2.Name = "Weight";
            dataGridView1.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "Closing";
            col3.HeaderText = "Closing";
            col3.Name = "Closing";
            dataGridView1.Columns.Add(col3);

            DataGridViewComboBoxColumn col4 = new DataGridViewComboBoxColumn();
            col4.DataPropertyName = "DrCr";
            col4.HeaderText = "DrCr";
            col4.Name = "DrCr";
            col4.Items.Add("SELL");
            col4.Items.Add("PURCHASE");
            col4.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col4);
        }

        private void BindOpeningOtherColumn()
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "Name";
            col1.HeaderText = "Name";
            col1.Name = "Name";
            col1.ReadOnly = true;
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Amount";
            col2.HeaderText = "Amount";
            col2.Name = "Amount";
            dataGridView1.Columns.Add(col2);

            DataGridViewComboBoxColumn col3 = new DataGridViewComboBoxColumn();
            col3.DataPropertyName = "DrCr";
            col3.HeaderText = "DrCr";
            col3.Name = "DrCr";
            col3.Items.Add("DEBIT");
            col3.Items.Add("CREDIT");
            col3.FlatStyle = FlatStyle.Popup;
            dataGridView1.Columns.Add(col3);
        }

        #endregion



        private void PartyInformation_Load(object sender, EventArgs e)
        {
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            con = new OleDbConnection();

            BindOpeningOtherColumn();

            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

            CommanHelper.FillCreditLimitOpening(dataGridView2);
            CommanHelper.BindPartyCategory(cmbCategory);
            for (int i = 0; i <= 365; i++)
            {
                cmbDays.Items.Add(i);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbtype.Text.Trim() == "PARTY")
                {
                    cmbBullion.Visible = true;
                    grpBoxWithCreditLimit.Visible = true;
                    cmbLot.SelectedIndex = -1;
                    cmbLot.Enabled = false;
                    cmbLot.Visible = false;
                    lblLot.Visible = false;
                    lblLotGenerateIn.Visible = false;
                    Panel_LotGenerate.Visible = false;
                }
                else
                {
                    chkWithCreditLimit.Checked = false;
                    cmbDays.Text = "";
                    cmbBullion.Visible = false;
                    cmbShowtrail.Text = "YES";
                    grpBoxWithCreditLimit.Visible = false;
                    groBoxCreditPeriod.Visible = false;
                    lblLot.Visible = true;
                    cmbLot.Enabled = true;
                    cmbLot.Visible = true;
                    lblLotGenerateIn.Visible = true;
                    Panel_LotGenerate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbBullion_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            if (cmbBullion.Text.Trim().ToUpper() == "MCX")
            {
                BindOpeningMCXColumn();
                OpeningList = CommanHelper.BindMCXDefaultOpening();
                dataGridView1.DataSource = OpeningList.ToList();
            }
            else
            {
                BindOpeningOtherColumn();
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == colLimit.Index)
            {
                e.Control.KeyPress -= NumericCheckHandler;
                e.Control.KeyPress += NumericCheckHandler;
            }
        }

        private void chkWithCreditLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithCreditLimit.Checked == true)
            {
                groBoxCreditPeriod.Visible = true;
                dataGridView2.Visible = true;
            }
            else
            {
                groBoxCreditPeriod.Visible = false;
                dataGridView2.Visible = false;
            }
        }

        private void cmbDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbDays.Text != "0") && (cmbDays.Text != ""))
            {
                label19.Visible = true;
                rateupdate_radio.Visible = true;
                rateupdate_radio_N.Visible = true;

            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
