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
    public partial class ProductDetails : Form
    {
        #region Declare Variable
        OleDbConnection con;
        OleDbTransaction Tran = null;
        List<OpeningOtherEntity> oOpeningOtherEntity = new List<OpeningOtherEntity>();
        String _ProductName = "";

        #endregion

        public ProductDetails()
        {
            InitializeComponent();
        }

        private void ProductDetails_Load(object sender, EventArgs e)
        {
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
            this.CancelButton = btnclose;

            con = new OleDbConnection();
            con.ConnectionString = ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb");

            oOpeningOtherEntity = CommanHelper.OpeningOther();            
            cmbcategory.DataSource = oOpeningOtherEntity;
            cmbcategory.DisplayMember = "Name";
            cmbcategory.SelectedIndex = -1;

            CommanHelper.ComboBoxItem(cmbsubgroup, "Product", "Distinct(SubGroup)");
            CommanHelper.ComboBoxItem(cmbPopUp, "Product", "Distinct(ProductName)");
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbcategory.Text.Trim() == "")
                {
                    cmbcategory.Focus();
                    return;
                }
                if (txtProductName.Text == "")
                {
                    txtProductName.Focus();
                    return;
                }
                if (cmbunit.Text.Trim() == "")
                {
                    cmbunit.Focus();
                    MessageBox.Show("Please Select Unit.", "Product Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmbPopUp.Text.Trim() != "")
                {
                    _ProductName = cmbPopUp.Text.Trim();
                }
                else
                {
                    _ProductName = txtProductName.Text.Trim();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                Tran = con.BeginTransaction();

                OleDbCommand cmd = new OleDbCommand("", con, Tran);

                cmd.CommandText = "Delete From Product Where ProductName ='" + txtProductName.Text.Trim() + "'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Delete From ProductGroup Where ProductGroup ='" + cmbgroup.Text.Trim() + "' AND ProductSubGroup = '" + cmbsubgroup.Text.Trim() + "'";
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO Product(Category,Unit,Weight_Packet,ProductName,SubGroup,PGroup,Opening,Pcs,Tunch,Westage,LabourRate,Fine,Amount,RawDefine,OpenDate,Narration,Company,UserId)VALUES(@Category,@Unit,@Weight_Packet,@ProductName,@SubGroup,@PGroup,@Opening,@Pcs,@Tunch,@Westage,@LabourRate,@Fine,@Amount,@RawDefine,@OpenDate,@Narration,@Company,@UserId)";                
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Category", cmbcategory.Text.Trim());
                cmd.Parameters.AddWithValue("@Unit", cmbunit.Text.Trim());
                cmd.Parameters.AddWithValue("@Weight_Packet", Conversion.ConToInt(txtwpkt.Text.Trim()));
                cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                cmd.Parameters.AddWithValue("@SubGroup", cmbsubgroup.Text.Trim());
                cmd.Parameters.AddWithValue("@PGroup", cmbgroup.Text.Trim());
                cmd.Parameters.AddWithValue("@Opening", Conversion.ConToDec6(txtopening.Text.Trim()));
                cmd.Parameters.AddWithValue("@Pcs", Conversion.ConToDec6(txtpcs.Text.Trim()));
                cmd.Parameters.AddWithValue("@Tunch", Conversion.ConToDec6(txttunch.Text.Trim()));
                cmd.Parameters.AddWithValue("@Westage", Conversion.ConToDec6(Txtwestage.Text.Trim()));
                cmd.Parameters.AddWithValue("@LabourRate", Conversion.ConToDec6(Txtlabour.Text.Trim()));
                cmd.Parameters.AddWithValue("@Fine", Conversion.ConToDec6(Txtfine.Text.Trim()));
                cmd.Parameters.AddWithValue("@Amount", Conversion.ConToDec6(Txtamount.Text.Trim()));
                cmd.Parameters.AddWithValue("@RawDefine", cmbRawDefine.Text.Trim());
                cmd.Parameters.AddWithValue("@OpenDate", Conversion.GetDateStr(dtpOpeningDate.Text.Trim()));
                cmd.Parameters.AddWithValue("@Narration", txtNarration.Text.Trim());
                cmd.Parameters.AddWithValue("@Company",CommanHelper.CompName.Trim());
                cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.Trim());
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = "INSERT INTO ProductGroup(ProductGroup,ProductSubGroup,Company,UserId)VALUES(@ProductGroup,@ProductSubGroup,@Company,@UserId)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductGroup", cmbgroup.Text.Trim());
                cmd.Parameters.AddWithValue("@ProductSubGroup", cmbsubgroup.Text.Trim());
                cmd.Parameters.AddWithValue("@Company", CommanHelper.CompName.Trim());
                cmd.Parameters.AddWithValue("@UserId", CommanHelper.UserId.Trim());
                cmd.ExecuteNonQuery();

                Tran.Commit();
                con.Close();
                MessageBox.Show("Data SuccessFully Updated", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Tran.Rollback();
            }
        }

        

        #region Helper

        private void GetProductDetails(String strProductNane)
        {
            try
            {
                if (strProductNane.Trim() != "")
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand("Select Category,Unit,Weight_Packet,ProductName,SubGroup,PGroup,Opening,Pcs,Tunch,Westage,LabourRate,Fine,Amount,RawDefine,OpenDate,Narration From Product Where ProductName = '" + strProductNane + "'", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    cmbcategory.SelectedIndex = -1;
                    txtProductName.Clear();
                    cmbunit.Text = "";
                    cmbsubgroup.Text = "";
                    cmbgroup.Text = "";
                    Txtamount.Clear();
                    Txtfine.Clear();
                    Txtlabour.Clear();
                    txtopening.Clear();
                    txtpcs.Clear();
                    txttunch.Clear();
                    Txtwestage.Clear();
                    txtwpkt.Clear();
                    txtNarration.Clear();
                    if (dr.Read())
                    {
                        cmbcategory.Text = dr["Category"].ToString();
                        cmbunit.Text = dr["Unit"].ToString();
                        if (Conversion.ConToDec6(dr["Weight_Packet"].ToString()) > 0)
                        {
                            txtwpkt.Text = dr["Weight_Packet"].ToString();
                        }
                        txtProductName.Text = dr["ProductName"].ToString();
                        cmbsubgroup.Text = dr["SubGroup"].ToString();
                        cmbgroup.Text = dr["PGroup"].ToString();
                        if (Conversion.ConToDec6(dr["Opening"].ToString()) > 0)
                        {
                            txtopening.Text = dr["Opening"].ToString();
                        }
                        if (Conversion.ConToDec6(dr["Pcs"].ToString()) > 0)
                        {
                            txtpcs.Text = dr["Pcs"].ToString();
                        }
                        if (Conversion.ConToDec6(dr["Tunch"].ToString()) > 0)
                        {
                            txttunch.Text = dr["Tunch"].ToString();
                        }
                        if (Conversion.ConToDec6(dr["Westage"].ToString()) > 0)
                        {
                            Txtwestage.Text = dr["Westage"].ToString();
                        }
                        if (Conversion.ConToDec6(dr["LabourRate"].ToString()) > 0)
                        {
                            Txtlabour.Text = dr["LabourRate"].ToString();
                        }
                        if (Conversion.ConToDec6(dr["Fine"].ToString()) > 0)
                        {
                            Txtfine.Text = dr["Fine"].ToString();
                        }
                        if (Conversion.ConToDec6(dr["Amount"].ToString()) > 0)
                        {
                            Txtamount.Text = dr["Amount"].ToString();
                        }
                        cmbRawDefine.Text = dr["RawDefine"].ToString();
                        // cmbcategory.Text = dr["OpenDate"].ToString();
                        txtNarration.Text = dr["Narration"].ToString();

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CalAmount_Gold()
        {
            Decimal Labour = 0;
            Decimal Opening = 0;
            Decimal Amount = 0;
            Decimal Pcs = 0;
            Labour = Conversion.ConToDec6(Txtlabour.Text);
            Opening = Conversion.ConToDec6(txtopening.Text);
            Pcs = Conversion.ConToDec6(txtpcs.Text);
            if (Pcs > 0)
            {
                Amount = System.Math.Round(Pcs * Labour);
                if (Amount == 0)
                {
                    Txtamount.Text = "";
                }
                else
                {
                    Txtamount.Text = Amount.ToString();
                }
            }
            else
            {
                Amount = System.Math.Round(((Opening * Labour) * 1000), 0);
                if (Amount == 0)
                {
                    Txtamount.Text = "";
                }
                else
                {
                    Txtamount.Text = Amount.ToString();
                }
            }
        }

        private void CallFine()
        {
            Decimal Tunch = 0;
            Decimal Opening = 0;
            Decimal Westage = 0;
            Decimal Fine = 0;
            Tunch = Conversion.ConToDec6(txttunch.Text);
            Opening = Conversion.ConToDec6(txtopening.Text);
            Westage = Conversion.ConToDec6(Txtwestage.Text);
            Fine = System.Math.Round(((Opening * (Westage + Tunch)) / 100), 3);
            if (Fine == 0)
            {
                Txtfine.Text = "";
            }
            else
            {
                Txtfine.Text = Fine.ToString();
            }
        }


        public void CallFine_Gold()
        {
            Decimal Tunch = 0;
            Decimal Opening = 0;
            Decimal Westage =0;
            Decimal Fine = 0;
            Tunch = Conversion.ConToDec6(txttunch.Text);
            Opening = Conversion.ConToDec6(txtopening.Text);
            Westage = Conversion.ConToDec6(Txtwestage.Text) ;
            Fine = System.Math.Round(((Opening * (Westage + Tunch)) / 100), 6);
            if (Fine == 0)
            {
                Txtfine.Text = "";
            }
            else
            {
                Txtfine.Text = Fine.ToString();
            }
        }

        public void CalAmount()
        {
            Decimal Labour = 0;
            Decimal Opening = 0;
            Decimal Amount = 0;
            Decimal Pcs = 0;
            Labour = Conversion.ConToDec6(Txtlabour.Text);
            Opening = Conversion.ConToDec6(txtopening.Text);
            Pcs = Conversion.ConToDec6(txtpcs.Text);

            if (Pcs > 0)
            {
                Amount = System.Math.Round(Pcs * Labour);
                if (Amount == 0)
                {
                    Txtamount.Text = "";
                }
                else
                {
                    Txtamount.Text = Amount.ToString();
                }
            }
            else
            {
                Amount = System.Math.Round(Opening * Labour);
                if (Amount == 0)
                {
                    Txtamount.Text = "";
                }
                else
                {
                    Txtamount.Text = Amount.ToString();
                }
            }
        }       

        private void ClearControls()
        {
            CommanHelper.ComboBoxItem(cmbPopUp, "Product", "Distinct(ProductName)");
            CommanHelper.ComboBoxItem(cmbsubgroup, "Product", "Distinct(SubGroup)");
            CommanHelper.ComboBoxItem(cmbgroup, "Product", "Distinct(PGroup)");
            cmbPopUp.SelectedIndex = -1;
            cmbPopUp.Text = "";
            cmbcategory.SelectedIndex = -1;
            cmbunit.SelectedIndex = -1;
            cmbsubgroup.SelectedIndex = -1;
            cmbgroup.SelectedIndex = -1;
            cmbsubgroup.Text = "";
            cmbgroup.Text = "";
            cmbRawDefine.SelectedIndex = -1;
            txtwpkt.Clear();
            txtProductName.Clear();
            txtopening.Clear();
            txtpcs.Clear();
            txttunch.Clear();
            Txtwestage.Clear();
            Txtlabour.Clear();
            Txtfine.Clear();
            Txtamount.Clear();
            txtNarration.Clear();
           // dtpOpeningDate.Text = DateTime.Now;
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void cmbcategory_Enter(object sender, EventArgs e)
        {
            cmbcategory.BackColor = Color.Aqua;
        }

        private void cmbcategory_Leave(object sender, EventArgs e)
        {
            cmbcategory.BackColor = Color.White;
        }

        private void cmbcategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) cmbunit.Focus();
        }

        private void cmbcategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbunit_Enter(object sender, EventArgs e)
        {
            cmbunit.BackColor = Color.Aqua;
        }

        private void cmbunit_Leave(object sender, EventArgs e)
        {
            cmbunit.BackColor = Color.White;
        }

        private void cmbunit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbunit.Text.Trim() == "PCS")
                {
                    txtwpkt.Focus();
                }
                else
                {
                    txtProductName.Focus();
                }
            }
        }

        private void txtwpkt_Enter(object sender, EventArgs e)
        {
            txtwpkt.BackColor = Color.Aqua;
        }

        private void txtwpkt_Leave(object sender, EventArgs e)
        {
            txtwpkt.BackColor = Color.White;
        }

        private void txtwpkt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtProductName.Focus();
            }
        }

        private void txtProductName_Enter(object sender, EventArgs e)
        {
            txtProductName.BackColor = Color.Aqua;
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            txtProductName.BackColor = Color.White;
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) cmbRawDefine.Focus();
        }

        private void cmbRawDefine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) cmbsubgroup.Focus();
        }

        private void cmbRawDefine_Enter(object sender, EventArgs e)
        {
            cmbRawDefine.BackColor = Color.Aqua;
        }

        private void cmbRawDefine_Leave(object sender, EventArgs e)
        {
            cmbRawDefine.BackColor = Color.White;
        }

        private void cmbsubgroup_Enter(object sender, EventArgs e)
        {
            cmbsubgroup.BackColor = Color.Aqua;
        }

        private void cmbsubgroup_Leave(object sender, EventArgs e)
        {
            cmbsubgroup.BackColor = Color.White;
        }

        private void cmbsubgroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbgroup.Focus();
            }
        }

        private void cmbgroup_Enter(object sender, EventArgs e)
        {
            cmbgroup.BackColor = Color.Aqua;
        }

        private void cmbgroup_Leave(object sender, EventArgs e)
        {
            cmbgroup.BackColor = Color.White;
        }

        private void cmbgroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (cmbunit.Text.Trim() == "PCS")
                {
                    txtpcs.Focus();
                }
                else
                {
                    txtopening.Focus();
                }
            }
        }

        private void txtopening_Enter(object sender, EventArgs e)
        {
            txtopening.BackColor = Color.Aqua;
            this.txtopening.SelectAll();
        }

        private void txtopening_Leave(object sender, EventArgs e)
        {
            txtopening.BackColor = Color.White;
        }

        private void txtopening_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtpcs.Focus();
            }
        }

        private void txtpcs_Enter(object sender, EventArgs e)
        {
            txtpcs.BackColor = Color.Aqua;
            this.txtpcs.SelectAll();
        }

        private void txtpcs_Leave(object sender, EventArgs e)
        {
            txtpcs.BackColor = Color.White;
        }

        private void txtpcs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txttunch.Focus();
            }
        }

        private void txttunch_Enter(object sender, EventArgs e)
        {
            txttunch.BackColor = Color.Aqua;
            this.txttunch.SelectAll();
        }

        private void txttunch_Leave(object sender, EventArgs e)
        {
            txttunch.BackColor = Color.White;
        }

        private void txttunch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Txtwestage.Focus();
            }
        }

        private void Txtwestage_Enter(object sender, EventArgs e)
        {
            Txtwestage.BackColor = Color.Aqua;
            this.Txtwestage.SelectAll();
        }

        private void Txtwestage_Leave(object sender, EventArgs e)
        {
            Txtwestage.BackColor = Color.White;
        }

        private void Txtwestage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Txtlabour.Focus();
            }
        }

        private void Txtlabour_Enter(object sender, EventArgs e)
        {
            Txtlabour.BackColor = Color.Aqua;
            this.Txtlabour.SelectAll();
        }

        private void Txtlabour_Leave(object sender, EventArgs e)
        {
            Txtlabour.BackColor = Color.White;
        }

        private void Txtlabour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNarration.Focus();
            }
        }

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.Cyan;
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.White;
        }

        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtProductName.Text == "")
                {
                    MessageBox.Show("Please Insert The Essential Information", "Product Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbcategory.Focus();
                }
                else
                {
                    btnsave.Focus();
                }
            }
        }

        private void cmbsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbgroup.SelectedIndex = -1;
            CommanHelper.ComboBoxItem(cmbgroup, "Product", "Distinct(PGroup)");
        }

        private void txtopening_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcategory.Text.Trim() == "GOLD")
                {
                    CalAmount_Gold();
                    CallFine_Gold();
                }
                else
                {
                    CalAmount();
                    CallFine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtpcs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbunit.Text.Trim() == "PCS")
                {
                    Decimal WtPkt = 0;
                    Decimal Pcs = 0;
                    Decimal Opening = 0;
                    WtPkt = Conversion.ConToDec6(txtwpkt.Text);
                    Pcs = Conversion.ConToDec6(txtpcs.Text);
                    Opening = WtPkt * Pcs;
                    if (Opening > 0)
                    {
                        txtopening.Text = Opening.ToString();
                    }
                    else
                    {
                        txtopening.Text = "";
                    }
                    if (cmbcategory.Text.Trim().ToUpper() == "GOLD")
                    {
                        CalAmount_Gold();
                    }
                    else
                    {
                        CalAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txttunch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcategory.Text.Trim().ToUpper() == "GOLD")
                {
                    CallFine_Gold();
                }
                else
                {
                    CallFine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Txtwestage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcategory.Text.Trim().ToUpper() == "GOLD")
                {
                    CallFine_Gold();
                }
                else
                {
                    CallFine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbPopUp_Enter(object sender, EventArgs e)
        {
            cmbPopUp.BackColor = Color.Cyan;
        }

        private void cmbPopUp_Leave(object sender, EventArgs e)
        {
            cmbPopUp.BackColor = Color.White;
        }

        private void cmbPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetProductDetails(cmbPopUp.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbPopUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmbcategory.Focus();
            }
        }


    }
}
