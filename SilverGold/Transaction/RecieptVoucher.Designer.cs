namespace SilverGold.Transaction
{
    partial class RecieptVoucher
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.panel11 = new System.Windows.Forms.Panel();
            this.cmbParty = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.net = new System.Windows.Forms.RadioButton();
            this.gross = new System.Windows.Forms.RadioButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.wt = new System.Windows.Forms.RadioButton();
            this.gram = new System.Windows.Forms.RadioButton();
            this.txtFinePremium = new System.Windows.Forms.TextBox();
            this.txtPremium = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtFine = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel13 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.plnpopup = new System.Windows.Forms.Panel();
            this.cmbPopUp = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblfinepremium = new System.Windows.Forms.Label();
            this.lbltotalfine = new System.Windows.Forms.Label();
            this.lbltotalamount = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnKfOK = new System.Windows.Forms.Button();
            this.lblfine = new System.Windows.Forms.Label();
            this.lblsno = new System.Windows.Forms.Label();
            this.lblweight = new System.Windows.Forms.Label();
            this.lbltqty = new System.Windows.Forms.Label();
            this.dataGridView2 = new SilverGold.Comman.GRIDVIEWCUSTOM1();
            this.lblcompanykf = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel13.SuspendLayout();
            this.plnpopup.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label34);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 34);
            this.panel1.TabIndex = 25;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label34.Location = new System.Drawing.Point(15, 7);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(132, 18);
            this.label34.TabIndex = 0;
            this.label34.Text = "Reciept Voucher";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.dtp);
            this.panel12.Location = new System.Drawing.Point(67, 50);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(103, 25);
            this.panel12.TabIndex = 44;
            // 
            // dtp
            // 
            this.dtp.CustomFormat = "dd/MM/yyyy";
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp.Location = new System.Drawing.Point(3, 2);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(98, 20);
            this.dtp.TabIndex = 0;
            this.dtp.Enter += new System.EventHandler(this.dtp_Enter);
            this.dtp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtp_KeyPress);
            this.dtp.Leave += new System.EventHandler(this.dtp_Leave);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.cmbParty);
            this.panel11.Location = new System.Drawing.Point(481, 49);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(215, 26);
            this.panel11.TabIndex = 45;
            // 
            // cmbParty
            // 
            this.cmbParty.FormattingEnabled = true;
            this.cmbParty.Location = new System.Drawing.Point(3, 3);
            this.cmbParty.Name = "cmbParty";
            this.cmbParty.Size = new System.Drawing.Size(209, 21);
            this.cmbParty.Sorted = true;
            this.cmbParty.TabIndex = 1;
            this.cmbParty.Enter += new System.EventHandler(this.cmbParty_Enter);
            this.cmbParty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbParty_KeyPress);
            this.cmbParty.Leave += new System.EventHandler(this.cmbParty_Leave);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.cmbProduct);
            this.panel9.Location = new System.Drawing.Point(21, 103);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(166, 26);
            this.panel9.TabIndex = 46;
            // 
            // cmbProduct
            // 
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(3, 3);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(162, 21);
            this.cmbProduct.TabIndex = 0;
            this.cmbProduct.TabStop = false;
            this.cmbProduct.Enter += new System.EventHandler(this.cmbProduct_Enter);
            this.cmbProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbProduct_KeyPress);
            this.cmbProduct.Leave += new System.EventHandler(this.cmbProduct_Leave);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.net);
            this.panel8.Controls.Add(this.gross);
            this.panel8.Location = new System.Drawing.Point(193, 80);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(97, 23);
            this.panel8.TabIndex = 56;
            // 
            // net
            // 
            this.net.AutoSize = true;
            this.net.Location = new System.Drawing.Point(52, 2);
            this.net.Name = "net";
            this.net.Size = new System.Drawing.Size(42, 17);
            this.net.TabIndex = 3;
            this.net.TabStop = true;
            this.net.Text = "Net";
            this.net.UseVisualStyleBackColor = true;
            this.net.Enter += new System.EventHandler(this.net_Enter);
            this.net.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.net_KeyPress);
            this.net.Leave += new System.EventHandler(this.net_Leave);
            // 
            // gross
            // 
            this.gross.AutoSize = true;
            this.gross.Location = new System.Drawing.Point(2, 2);
            this.gross.Name = "gross";
            this.gross.Size = new System.Drawing.Size(52, 17);
            this.gross.TabIndex = 2;
            this.gross.TabStop = true;
            this.gross.Text = "Gross";
            this.gross.UseVisualStyleBackColor = true;
            this.gross.Enter += new System.EventHandler(this.gross_Enter);
            this.gross.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gross_KeyPress);
            this.gross.Leave += new System.EventHandler(this.gross_Leave);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.wt);
            this.panel7.Controls.Add(this.gram);
            this.panel7.Location = new System.Drawing.Point(294, 80);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(93, 23);
            this.panel7.TabIndex = 55;
            // 
            // wt
            // 
            this.wt.AutoSize = true;
            this.wt.Location = new System.Drawing.Point(2, 1);
            this.wt.Name = "wt";
            this.wt.Size = new System.Drawing.Size(39, 17);
            this.wt.TabIndex = 1;
            this.wt.TabStop = true;
            this.wt.Text = "Wt";
            this.wt.UseVisualStyleBackColor = true;
            this.wt.Enter += new System.EventHandler(this.wt_Enter);
            this.wt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.wt_KeyPress);
            this.wt.Leave += new System.EventHandler(this.wt_Leave);
            // 
            // gram
            // 
            this.gram.AutoSize = true;
            this.gram.Location = new System.Drawing.Point(49, 1);
            this.gram.Name = "gram";
            this.gram.Size = new System.Drawing.Size(38, 17);
            this.gram.TabIndex = 0;
            this.gram.TabStop = true;
            this.gram.Text = "Rs";
            this.gram.UseVisualStyleBackColor = true;
            this.gram.Enter += new System.EventHandler(this.gram_Enter);
            this.gram.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gram_KeyPress);
            this.gram.Leave += new System.EventHandler(this.gram_Leave);
            // 
            // txtFinePremium
            // 
            this.txtFinePremium.Location = new System.Drawing.Point(315, 106);
            this.txtFinePremium.Name = "txtFinePremium";
            this.txtFinePremium.ReadOnly = true;
            this.txtFinePremium.Size = new System.Drawing.Size(63, 20);
            this.txtFinePremium.TabIndex = 49;
            this.txtFinePremium.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPremium
            // 
            this.txtPremium.Location = new System.Drawing.Point(255, 106);
            this.txtPremium.Name = "txtPremium";
            this.txtPremium.Size = new System.Drawing.Size(61, 20);
            this.txtPremium.TabIndex = 48;
            this.txtPremium.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPremium.Enter += new System.EventHandler(this.txtPremium_Enter);
            this.txtPremium.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPremium_KeyPress);
            this.txtPremium.Leave += new System.EventHandler(this.txtPremium_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nina", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(431, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 54;
            this.label3.Text = "Party:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nina", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 53;
            this.label2.Text = "Date:";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.YellowGreen;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(599, 105);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(34, 21);
            this.btnOk.TabIndex = 52;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtFine
            // 
            this.txtFine.Location = new System.Drawing.Point(189, 106);
            this.txtFine.Name = "txtFine";
            this.txtFine.Size = new System.Drawing.Size(66, 20);
            this.txtFine.TabIndex = 47;
            this.txtFine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFine.Enter += new System.EventHandler(this.txtFine_Enter);
            this.txtFine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFine_KeyPress);
            this.txtFine.Leave += new System.EventHandler(this.txtFine_Leave);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(438, 106);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(159, 20);
            this.txtDescription.TabIndex = 51;
            this.txtDescription.Enter += new System.EventHandler(this.txtDescription_Enter);
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(377, 106);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(61, 20);
            this.txtAmount.TabIndex = 50;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 136);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(675, 150);
            this.dataGridView1.TabIndex = 57;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.dateTimePicker1);
            this.panel13.Location = new System.Drawing.Point(532, 325);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(94, 25);
            this.panel13.TabIndex = 64;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(2, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(90, 20);
            this.dateTimePicker1.TabIndex = 15;
            this.dateTimePicker1.Enter += new System.EventHandler(this.dateTimePicker1_Enter);
            this.dateTimePicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePicker1_KeyPress);
            this.dateTimePicker1.Leave += new System.EventHandler(this.dateTimePicker1_Leave);
            // 
            // plnpopup
            // 
            this.plnpopup.Controls.Add(this.cmbPopUp);
            this.plnpopup.Location = new System.Drawing.Point(443, 325);
            this.plnpopup.Name = "plnpopup";
            this.plnpopup.Size = new System.Drawing.Size(88, 26);
            this.plnpopup.TabIndex = 63;
            // 
            // cmbPopUp
            // 
            this.cmbPopUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPopUp.FormattingEnabled = true;
            this.cmbPopUp.Location = new System.Drawing.Point(2, 3);
            this.cmbPopUp.Name = "cmbPopUp";
            this.cmbPopUp.Size = new System.Drawing.Size(84, 21);
            this.cmbPopUp.Sorted = true;
            this.cmbPopUp.TabIndex = 14;
            this.cmbPopUp.Enter += new System.EventHandler(this.cmbPopUp_Enter);
            this.cmbPopUp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPopUp_KeyPress);
            this.cmbPopUp.Leave += new System.EventHandler(this.cmbPopUp_Leave);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(314, 291);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 16);
            this.label11.TabIndex = 70;
            this.label11.Text = "label11";
            // 
            // lblfinepremium
            // 
            this.lblfinepremium.Font = new System.Drawing.Font("Nina", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfinepremium.Location = new System.Drawing.Point(550, 287);
            this.lblfinepremium.Name = "lblfinepremium";
            this.lblfinepremium.Size = new System.Drawing.Size(145, 16);
            this.lblfinepremium.TabIndex = 68;
            this.lblfinepremium.Text = "label4";
            // 
            // lbltotalfine
            // 
            this.lbltotalfine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalfine.Location = new System.Drawing.Point(159, 291);
            this.lbltotalfine.Name = "lbltotalfine";
            this.lbltotalfine.Size = new System.Drawing.Size(84, 16);
            this.lbltotalfine.TabIndex = 67;
            this.lbltotalfine.Text = "label4";
            this.lbltotalfine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltotalamount
            // 
            this.lbltotalamount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalamount.Location = new System.Drawing.Point(338, 309);
            this.lbltotalamount.Name = "lbltotalamount";
            this.lbltotalamount.Size = new System.Drawing.Size(97, 16);
            this.lbltotalamount.TabIndex = 66;
            this.lbltotalamount.Text = "label4";
            this.lbltotalamount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.YellowGreen;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(346, 326);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(96, 23);
            this.btnPrint.TabIndex = 62;
            this.btnPrint.Text = "PrintSave(Ctr+P)";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(265, 326);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 23);
            this.btnClose.TabIndex = 61;
            this.btnClose.Text = "Close (Esc)";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.YellowGreen;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(184, 326);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(79, 23);
            this.btnRefresh.TabIndex = 60;
            this.btnRefresh.Text = "Refresh (F12)";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.YellowGreen;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(103, 326);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 23);
            this.btnDelete.TabIndex = 59;
            this.btnDelete.Text = "Delete (F11)";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(22, 326);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 23);
            this.btnSave.TabIndex = 58;
            this.btnSave.Text = "Save (F9)";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(443, 352);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(181, 134);
            this.listBox1.TabIndex = 65;
            this.listBox1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbCategory);
            this.panel2.Location = new System.Drawing.Point(274, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 28);
            this.panel2.TabIndex = 71;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(3, 4);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(101, 21);
            this.cmbCategory.TabIndex = 0;
            this.cmbCategory.Enter += new System.EventHandler(this.cmbCategory_Enter);
            this.cmbCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCategory_KeyPress);
            this.cmbCategory.Leave += new System.EventHandler(this.cmbCategory_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(214, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 72;
            this.label5.Text = "Category:";
            // 
            // btnKfOK
            // 
            this.btnKfOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnKfOK.Location = new System.Drawing.Point(1039, 509);
            this.btnKfOK.Name = "btnKfOK";
            this.btnKfOK.Size = new System.Drawing.Size(63, 23);
            this.btnKfOK.TabIndex = 169;
            this.btnKfOK.Text = "OK (F8)";
            this.btnKfOK.UseVisualStyleBackColor = true;
            // 
            // lblfine
            // 
            this.lblfine.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfine.Location = new System.Drawing.Point(1034, 489);
            this.lblfine.Name = "lblfine";
            this.lblfine.Size = new System.Drawing.Size(64, 17);
            this.lblfine.TabIndex = 170;
            this.lblfine.Text = "fine1";
            this.lblfine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblsno
            // 
            this.lblsno.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsno.Location = new System.Drawing.Point(719, 489);
            this.lblsno.Name = "lblsno";
            this.lblsno.Size = new System.Drawing.Size(35, 14);
            this.lblsno.TabIndex = 172;
            this.lblsno.Text = "sno";
            this.lblsno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblweight
            // 
            this.lblweight.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblweight.Location = new System.Drawing.Point(796, 488);
            this.lblweight.Name = "lblweight";
            this.lblweight.Size = new System.Drawing.Size(66, 16);
            this.lblweight.TabIndex = 171;
            this.lblweight.Text = "weight";
            this.lblweight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbltqty
            // 
            this.lbltqty.AutoSize = true;
            this.lbltqty.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltqty.Location = new System.Drawing.Point(648, 490);
            this.lbltqty.Name = "lbltqty";
            this.lbltqty.Size = new System.Drawing.Size(65, 14);
            this.lbltqty.TabIndex = 173;
            this.lbltqty.Text = "Total Item";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(654, 325);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(451, 161);
            this.dataGridView2.TabIndex = 168;
            // 
            // lblcompanykf
            // 
            this.lblcompanykf.AutoSize = true;
            this.lblcompanykf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcompanykf.Location = new System.Drawing.Point(651, 311);
            this.lblcompanykf.Name = "lblcompanykf";
            this.lblcompanykf.Size = new System.Drawing.Size(46, 13);
            this.lblcompanykf.TabIndex = 174;
            this.lblcompanykf.Text = "KF List";
            // 
            // RecieptVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1223, 533);
            this.Controls.Add(this.lblcompanykf);
            this.Controls.Add(this.btnKfOK);
            this.Controls.Add(this.lblfine);
            this.Controls.Add(this.lblsno);
            this.Controls.Add(this.lblweight);
            this.Controls.Add(this.lbltqty);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.plnpopup);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblfinepremium);
            this.Controls.Add(this.lbltotalfine);
            this.Controls.Add(this.lbltotalamount);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.txtFinePremium);
            this.Controls.Add(this.txtPremium);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtFine);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.panel1);
            this.Name = "RecieptVoucher";
            this.Text = "RecieptVoucher";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RecieptVoucher_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel13.ResumeLayout(false);
            this.plnpopup.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ComboBox cmbParty;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton net;
        private System.Windows.Forms.RadioButton gross;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton wt;
        private System.Windows.Forms.RadioButton gram;
        private System.Windows.Forms.TextBox txtFinePremium;
        private System.Windows.Forms.TextBox txtPremium;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtFine;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Panel plnpopup;
        public System.Windows.Forms.ComboBox cmbPopUp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblfinepremium;
        private System.Windows.Forms.Label lbltotalfine;
        private System.Windows.Forms.Label lbltotalamount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnKfOK;
        private System.Windows.Forms.Label lblfine;
        private System.Windows.Forms.Label lblsno;
        private System.Windows.Forms.Label lblweight;
        private System.Windows.Forms.Label lbltqty;
        private Comman.GRIDVIEWCUSTOM1 dataGridView2;
        private System.Windows.Forms.Label lblcompanykf;
    }
}