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
    public partial class Items : Form
    {
        #region Declare Variables
        OleDbConnection con;
        ConnectionClass objCon;
        OleDbTransaction Tran = null;
        DataGridView.HitTestInfo hti;
        int _Sno = 0;
        List<MetalEntity> MetalList = new List<MetalEntity>();

        #endregion
        public Items()
        {
            InitializeComponent();
            CommanHelper.ChangeGridFormate(dataGridView1);
            Bind();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            con = new OleDbConnection();
            objCon = new ConnectionClass();
            this.toolStripMenu_Save.Click += new EventHandler(btnSave_Click);
            this.toolStripMenu_Refresh.Click += new EventHandler(btnRefresh_Click);
            this.toolStripMenu_Delete.Click += new EventHandler(btnDelete_Click);
            if (CommanHelper.CompName != "" && CommanHelper.Com_DB_PATH != "" && CommanHelper.Com_DB_NAME != "")
            {
                con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");
            }
            else
            {
                con.ConnectionString = objCon._CONSTR();
            }
            this.CancelButton = btnExit;
            cmbKF.SelectedIndex = 0;
            if (CommanHelper.CompName != "" && CommanHelper.Com_DB_PATH != "" && CommanHelper.Com_DB_NAME != "")
            {
                CommanHelper.GetMetalCategory(cmbMetalCat);
                CommanHelper.GetWeightType(cmbWeightType);
            }
            else
            {
                CommanHelper.GetMetalCate_Account(cmbMetalCat);
                CommanHelper.GetWeightType_Account(cmbWeightType);
            }
            cmbMetalCat.SelectedIndex = -1;
            BindMetal();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Tran = null;
                if (cmbMetalCat.Text.Trim() == "")
                {
                    cmbMetalCat.Focus();
                    return;
                }
               
                if (cmbKF.Text.Trim() == "")
                {
                    cmbKF.Focus();
                    return;
                }

                if (_Sno == 0)
                {
                    if (CheckItemExist(cmbMetalCat.Text.Trim(), txtMetalName.Text.Trim()) == true)
                    {
                        MessageBox.Show("Already Exist Metal.", "Metal", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtMetalName.Focus();
                        return;
                    }
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    Tran = con.BeginTransaction();
                }
                OleDbCommand cmd = new OleDbCommand("", con, Tran);
                if (_Sno > 0)
                {
                    cmd.CommandText = "UPDATE Metal SET MetalCategory='" + cmbMetalCat.Text.Trim() + "',MetalName='" + txtMetalName.Text.Trim() + "',WeightType='" + cmbWeightType.Text.Trim() + "',KachchiFine='" + cmbKF.Text.Trim() + "' WHERE Sno = " + _Sno + "";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "INSERT INTO Metal(MetalCategory,MetalName,WeightType,KachchiFine,CompanyName,UserId)VALUES('" + cmbMetalCat.Text.Trim() + "','" + txtMetalName.Text.Trim() + "','" + cmbWeightType.Text.Trim() + "','" + cmbKF.Text.Trim() + "','" + CommanHelper.CompName + "','" + CommanHelper.UserId + "')";
                    cmd.ExecuteNonQuery();
                }
                Tran.Commit();
                con.Close();
                ClearControl();
                BindMetal();

            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                Tran.Rollback();
            }
        }

        #region Helper

        private Boolean CheckItemExist(String _MetalCategory, String _MetalName)
        {
            Boolean _Check = false;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select * from Metal Where MetalCategory = '" + _MetalCategory + "' and MetalName = '" + _MetalName + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                _Check = true;
            }
            con.Close();

            return _Check;
        }

        private void ClearControl()
        {

            if (CommanHelper.CompName != "" && CommanHelper.Com_DB_PATH != "" && CommanHelper.Com_DB_NAME != "")
            {
                CommanHelper.GetMetalCategory(cmbMetalCat);
                CommanHelper.GetWeightType(cmbWeightType);
            }
            else
            {
                CommanHelper.GetMetalCate_Account(cmbMetalCat);
                CommanHelper.GetWeightType_Account(cmbWeightType);
            }
            cmbMetalCat.SelectedIndex = -1;
            cmbKF.Text = "";
            cmbMetalCat.Text = "";
            txtMetalName.Clear();
            cmbWeightType.SelectedIndex = -1;
            cmbWeightType.Text = "";
            _Sno = 0;
        }

        private void Bind()
        {
            DataGridViewColumn col1 = new DataGridViewTextBoxColumn();
            col1.DataPropertyName = "MetalCategory";
            col1.HeaderText = "Category";
            col1.Name = "MetalCategory";
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn();
            col2.DataPropertyName = "MetalName";
            col2.HeaderText = "Name";
            col2.Name = "MetalName";
            dataGridView1.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn();
            col3.DataPropertyName = "WeightType";
            col3.HeaderText = "WeightType";
            col3.Name = "WeightType";
            dataGridView1.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewTextBoxColumn();
            col4.DataPropertyName = "KachchiFine";
            col4.HeaderText = "KF";
            col4.Name = "KachchiFine";
            dataGridView1.Columns.Add(col4);

            DataGridViewColumn col5 = new DataGridViewTextBoxColumn();
            col5.DataPropertyName = "Sno";
            col5.HeaderText = "Sno";
            col5.Name = "Sno";
            col5.Visible = false;
            dataGridView1.Columns.Add(col5);

        }


        private void BindMetal()
        {
            MetalList.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand("Select MetalCategory,MetalName,WeightType,KachchiFine,Sno From Metal Order By MetalCategory ASC", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MetalEntity oMetal = new MetalEntity();
                oMetal.MetalCategory = dr["MetalCategory"].ToString();
                oMetal.MetalName = dr["MetalName"].ToString();              
                oMetal.WeightType = dr["WeightType"].ToString();
                oMetal.KachchiFine = dr["KachchiFine"].ToString();
                oMetal.Sno = Conversion.ConToInt(dr["Sno"].ToString());
                MetalList.Add(oMetal);
            }
            dataGridView1.DataSource = MetalList.Select(x => new { x.MetalCategory, x.MetalName, x.WeightType, x.KachchiFine, x.Sno }).ToList();
            con.Close();
        }

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Sno > 0)
                {
                    if (MessageBox.Show("Do You Want To Delete Data", "Item", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        OleDbCommand cmd = new OleDbCommand("Delete From Metal Where Sno =" + _Sno + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ClearControl();
                        BindMetal();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControl();
                BindMetal();
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                hti = dataGridView1.HitTest(e.X, e.Y);
                if (hti.RowIndex >= 0)
                {
                    cmbMetalCat.Text = (dataGridView1.Rows[hti.RowIndex].Cells[0].Value??(object)"").ToString();
                    txtMetalName.Text = (dataGridView1.Rows[hti.RowIndex].Cells[1].Value ?? (object)"").ToString();
                    cmbWeightType.Text = (dataGridView1.Rows[hti.RowIndex].Cells[2].Value ?? (object)"").ToString();
                    cmbKF.Text = (dataGridView1.Rows[hti.RowIndex].Cells[3].Value ?? (object)"").ToString();
                    _Sno = Conversion.ConToInt((dataGridView1.Rows[hti.RowIndex].Cells[4].Value ?? (object)"").ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbMetalCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
            if (e.KeyChar == 13)
            {
                txtMetalName.Focus();
            }
        }

        private void txtMetalName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbWeightType.Focus();
            }
        }

        private void cmbWeightType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
            if (e.KeyChar == 13)
            {
                cmbKF.Focus();
            }
        }

        private void cmbKF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSave.Focus();
            }
        }

        private void cmbMetalCat_Enter(object sender, EventArgs e)
        {
            cmbMetalCat.BackColor = Color.Cyan;
        }

        private void cmbMetalCat_Leave(object sender, EventArgs e)
        {
            cmbMetalCat.BackColor = Color.White;
        }

        private void txtMetalName_Enter(object sender, EventArgs e)
        {
            txtMetalName.BackColor = Color.Cyan;
        }

        private void txtMetalName_Leave(object sender, EventArgs e)
        {
            txtMetalName.BackColor = Color.White;
        }

        private void cmbWeightType_Enter(object sender, EventArgs e)
        {
            cmbWeightType.BackColor = Color.Cyan;
        }

        private void cmbWeightType_Leave(object sender, EventArgs e)
        {
            cmbWeightType.BackColor = Color.White;
        }

        private void cmbKF_Enter(object sender, EventArgs e)
        {
            cmbKF.BackColor = Color.Cyan;
        }

        private void cmbKF_Leave(object sender, EventArgs e)
        {
            cmbKF.BackColor = Color.White;
        }
    }
}
