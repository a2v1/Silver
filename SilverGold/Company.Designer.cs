using SilverGold.Comman;
using SilverGold.Helper;
namespace SilverGold
{
    partial class Company
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new SilverGold.Comman.GRIDVIEWCUSTOM1();
            this.txtFinancialYear = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblFine = new System.Windows.Forms.Label();
            this.lblsno = new System.Windows.Forms.Label();
            this.lbltotalweight = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbUType = new System.Windows.Forms.ComboBox();
            this.txtRePassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnupdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblKFCate = new System.Windows.Forms.Label();
            this.lblKFName = new System.Windows.Forms.Label();
            this.dataGridView2 = new SilverGold.Comman.GRIDVIEWCUSTOM1();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenu_Create = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuI_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_OK = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenu_CompanyDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.txtFinancialYear);
            this.groupBox1.Controls.Add(this.txtCompanyName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 290);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(680, 196);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // txtFinancialYear
            // 
            this.txtFinancialYear.Location = new System.Drawing.Point(153, 47);
            this.txtFinancialYear.MaxLength = 8;
            this.txtFinancialYear.Name = "txtFinancialYear";
            this.txtFinancialYear.Size = new System.Drawing.Size(220, 20);
            this.txtFinancialYear.TabIndex = 8;
            this.txtFinancialYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFinancialYear_KeyPress);
            this.txtFinancialYear.Leave += new System.EventHandler(this.txtFinancialYear_Leave);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(153, 19);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(221, 20);
            this.txtCompanyName.TabIndex = 7;
            this.txtCompanyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompanyName_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(48, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "(Ex.20052006)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Financial Year";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company Name";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1277, 34);
            this.panel2.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(15, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Create Company";
            // 
            // lblWeight
            // 
            this.lblWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.Location = new System.Drawing.Point(858, 256);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(60, 16);
            this.lblWeight.TabIndex = 36;
            this.lblWeight.Text = "lblWeight";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWeight.Visible = false;
            // 
            // lblFine
            // 
            this.lblFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFine.Location = new System.Drawing.Point(1084, 256);
            this.lblFine.Name = "lblFine";
            this.lblFine.Size = new System.Drawing.Size(62, 13);
            this.lblFine.TabIndex = 35;
            this.lblFine.Text = "Fine";
            this.lblFine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFine.Visible = false;
            // 
            // lblsno
            // 
            this.lblsno.AutoSize = true;
            this.lblsno.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsno.Location = new System.Drawing.Point(817, 257);
            this.lblsno.Name = "lblsno";
            this.lblsno.Size = new System.Drawing.Size(28, 15);
            this.lblsno.TabIndex = 34;
            this.lblsno.Text = "sno";
            this.lblsno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblsno.Visible = false;
            // 
            // lbltotalweight
            // 
            this.lbltotalweight.AutoSize = true;
            this.lbltotalweight.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalweight.Location = new System.Drawing.Point(756, 257);
            this.lbltotalweight.Name = "lbltotalweight";
            this.lbltotalweight.Size = new System.Drawing.Size(59, 15);
            this.lbltotalweight.TabIndex = 33;
            this.lbltotalweight.Text = "Total Qty:";
            this.lbltotalweight.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(1071, 284);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 32);
            this.btnOK.TabIndex = 37;
            this.btnOK.Text = "OK(F8)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 386);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(157, 16);
            this.label9.TabIndex = 38;
            this.label9.Text = "Company Details (F2)";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(29, 407);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(690, 117);
            this.dataGridView3.TabIndex = 39;
            this.dataGridView3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView3_KeyPress);
            this.dataGridView3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView3_MouseClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbUType);
            this.groupBox3.Controls.Add(this.txtRePassword);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUserId);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(759, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(306, 135);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Set Password";
            this.groupBox3.Visible = false;
            // 
            // cmbUType
            // 
            this.cmbUType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUType.FormattingEnabled = true;
            this.cmbUType.Items.AddRange(new object[] {
            "ADMIN",
            "USER",
            "DOMAIN"});
            this.cmbUType.Location = new System.Drawing.Point(121, 91);
            this.cmbUType.Name = "cmbUType";
            this.cmbUType.Size = new System.Drawing.Size(121, 23);
            this.cmbUType.TabIndex = 3;
            this.cmbUType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUType_KeyPress);
            // 
            // txtRePassword
            // 
            this.txtRePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRePassword.Location = new System.Drawing.Point(121, 66);
            this.txtRePassword.Name = "txtRePassword";
            this.txtRePassword.PasswordChar = '*';
            this.txtRePassword.Size = new System.Drawing.Size(163, 21);
            this.txtRePassword.TabIndex = 2;
            this.txtRePassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRePassword_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(121, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(163, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUserId
            // 
            this.txtUserId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserId.Location = new System.Drawing.Point(121, 18);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(163, 21);
            this.txtUserId.TabIndex = 0;
            this.txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserId_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "User Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Varify Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Password:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "User Id:";
            // 
            // btnrefresh
            // 
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(207, 349);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(80, 28);
            this.btnrefresh.TabIndex = 43;
            this.btnrefresh.Text = "Refresh(F5)";
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdate.Location = new System.Drawing.Point(117, 349);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(84, 28);
            this.btnupdate.TabIndex = 42;
            this.btnupdate.Text = "Update(F12)";
            this.btnupdate.UseVisualStyleBackColor = true;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(293, 349);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 28);
            this.btnExit.TabIndex = 44;
            this.btnExit.Text = "Exit (Esc)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(30, 349);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(81, 28);
            this.btnCreate.TabIndex = 41;
            this.btnCreate.Text = "Create (F9)";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblKFCate
            // 
            this.lblKFCate.AutoSize = true;
            this.lblKFCate.ForeColor = System.Drawing.Color.Red;
            this.lblKFCate.Location = new System.Drawing.Point(752, 50);
            this.lblKFCate.Name = "lblKFCate";
            this.lblKFCate.Size = new System.Drawing.Size(41, 13);
            this.lblKFCate.TabIndex = 45;
            this.lblKFCate.Text = "label10";
            // 
            // lblKFName
            // 
            this.lblKFName.AutoSize = true;
            this.lblKFName.ForeColor = System.Drawing.Color.Red;
            this.lblKFName.Location = new System.Drawing.Point(877, 50);
            this.lblKFName.Name = "lblKFName";
            this.lblKFName.Size = new System.Drawing.Size(41, 13);
            this.lblKFName.TabIndex = 46;
            this.lblKFName.Text = "label11";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(752, 72);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(394, 181);
            this.dataGridView2.TabIndex = 17;
            this.dataGridView2.Visible = false;
            this.dataGridView2.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValidated);
            this.dataGridView2.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView2_RowsRemoved);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_Create,
            this.toolStripMenu_Update,
            this.toolStripMenuI_Refresh,
            this.toolStripMenu_OK,
            this.toolStripMenu_CompanyDetails});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1277, 24);
            this.menuStrip1.TabIndex = 47;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenu_Create
            // 
            this.toolStripMenu_Create.Name = "toolStripMenu_Create";
            this.toolStripMenu_Create.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.toolStripMenu_Create.Size = new System.Drawing.Size(31, 20);
            this.toolStripMenu_Create.Text = "    ";
            // 
            // toolStripMenu_Update
            // 
            this.toolStripMenu_Update.Name = "toolStripMenu_Update";
            this.toolStripMenu_Update.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.toolStripMenu_Update.Size = new System.Drawing.Size(31, 20);
            this.toolStripMenu_Update.Text = "    ";
            // 
            // toolStripMenuI_Refresh
            // 
            this.toolStripMenuI_Refresh.Name = "toolStripMenuI_Refresh";
            this.toolStripMenuI_Refresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.toolStripMenuI_Refresh.Size = new System.Drawing.Size(28, 20);
            this.toolStripMenuI_Refresh.Text = "   ";
            // 
            // toolStripMenu_OK
            // 
            this.toolStripMenu_OK.Name = "toolStripMenu_OK";
            this.toolStripMenu_OK.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.toolStripMenu_OK.Size = new System.Drawing.Size(25, 20);
            this.toolStripMenu_OK.Text = "  ";
            // 
            // toolStripMenu_CompanyDetails
            // 
            this.toolStripMenu_CompanyDetails.Name = "toolStripMenu_CompanyDetails";
            this.toolStripMenu_CompanyDetails.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.toolStripMenu_CompanyDetails.Size = new System.Drawing.Size(25, 20);
            this.toolStripMenu_CompanyDetails.Text = "  ";
            this.toolStripMenu_CompanyDetails.Click += new System.EventHandler(this.toolStripMenu_CompanyDetails_Click);
            // 
            // Company
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 539);
            this.Controls.Add(this.lblKFName);
            this.Controls.Add(this.lblKFCate);
            this.Controls.Add(this.btnrefresh);
            this.Controls.Add(this.btnupdate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.lblFine);
            this.Controls.Add(this.lblsno);
            this.Controls.Add(this.lbltotalweight);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Company";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Company";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Company_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFinancialYear;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblFine;
        private System.Windows.Forms.Label lblsno;
        private System.Windows.Forms.Label lbltotalweight;
        public System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbUType;
        private System.Windows.Forms.TextBox txtRePassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnupdate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCreate;
        private GRIDVIEWCUSTOM1 dataGridView1;
        private GRIDVIEWCUSTOM1 dataGridView2;
        private System.Windows.Forms.Label lblKFCate;
        private System.Windows.Forms.Label lblKFName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Create;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Update;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuI_Refresh;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_OK;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_CompanyDetails;
    }
}