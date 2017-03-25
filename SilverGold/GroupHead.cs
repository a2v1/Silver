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
    public partial class GroupHead : Form
    {
        #region Declare Variables
        OleDbConnection con;
        OleDbTransaction Tran = null;
        List<GroupHeadEntity> GroupHeadList = new List<GroupHeadEntity>();

        #endregion
        public GroupHead()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            BindColumn();
        }

        private void GroupHead_Load(object sender, EventArgs e)
        {
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            this.CancelButton = btnExit;
            this.toolStripMenu_Save.Click += new EventHandler(btnSave_Click);
            this.toolStripMenu_Refresh.Click += new EventHandler(btnRefresh_Click);
            this.toolStripMenu_Delete.Click += new EventHandler(btnDelete_Click);

            con = new OleDbConnection();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
            CommanHelper.ComboBoxItem(CMBPOPUP, "GroupHead", "Distinct(GroupHead)");
        }

        #region Helper

        private void BindColumn()
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "SubGroup";
            col1.HeaderText = "SubHead";
            col1.Name = "SubGroup";
            dataGridView1.Columns.Add(col1);
            
            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "Sno";
            col2.HeaderText = "Sno";
            col2.Name = "Sno";
            col2.Visible = false;
            dataGridView1.Columns.Add(col2);
        }

        private void ClearControl()
        {
            CommanHelper.ComboBoxItem(CMBPOPUP, "GroupHead", "Distinct(GroupHead)");
            Tran = null;
            txtGroupHead.Clear();
            dataGridView1.Rows.Clear();
            CMBPOPUP.SelectedIndex = 0;
            txtGroupHead.Focus();
        }
      
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupHead.Text.Trim() == "")
                {
                    txtGroupHead.Focus();
                    return;
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                Tran = con.BeginTransaction();

                OleDbCommand cmd = new OleDbCommand("Delete From GroupHead Where GroupHead = '" + txtGroupHead.Text.Trim() + "' And Company = '" + CommanHelper.CompName.Trim() + "'", con, Tran);
                cmd.ExecuteNonQuery();

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if ((dr.Cells[0].Value ?? (object)"").ToString() != "")
                    {
                        cmd.CommandText = "INSERT INTO GroupHead(GroupHead,SubGroup,Company,UserId)VALUES('" + txtGroupHead.Text.Trim() + "','" + dr.Cells[0].Value.ToString().Trim() + "','" + CommanHelper.CompName.Trim() + "','" + CommanHelper.UserId.Trim() + "')";
                        cmd.ExecuteNonQuery();
                    }
                }
                Tran.Commit();
                ClearControl();

                MessageBox.Show("Data Successfully Inserted..", "Group Head", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Tran.Rollback();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do You Want To Delete The Data", "Group Head", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    Tran = con.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand("Delete From GroupHead Where GroupHead='" + CommanHelper.CompName.Trim() + "'", con, Tran);
                    cmd.ExecuteNonQuery();
                    Tran.Commit();
                    ClearControl();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Tran.Rollback();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
       

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Are you want to delete the data", "Group Head", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void CMBPOPUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CMBPOPUP.Text.Trim() != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand("Select GroupHead,SubGroup,Company from GroupHead Where GroupHead = '" + CMBPOPUP.Text.Trim() + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    int _Sno = 0;
                    dataGridView1.Rows.Clear();
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[_Sno].Cells[0].Value = dr["SubGroup"].ToString(); _Sno++;
                    }
                    con.Close();
                    txtGroupHead.Text = CMBPOPUP.Text.Trim();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripMenu_PopUp_Click(object sender, EventArgs e)
        {
            CMBPOPUP.Focus();
        }
    }
}
