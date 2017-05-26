namespace SilverGold.Transaction
{
    partial class PriceList
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
            this.grpPriceList = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbProduct_PriceList = new System.Windows.Forms.ComboBox();
            this.cmbPartyName_PriseList = new System.Windows.Forms.ComboBox();
            this.grpPriceList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPriceList
            // 
            this.grpPriceList.Controls.Add(this.btnClose);
            this.grpPriceList.Controls.Add(this.dataGridView1);
            this.grpPriceList.Controls.Add(this.label8);
            this.grpPriceList.Controls.Add(this.label7);
            this.grpPriceList.Controls.Add(this.label6);
            this.grpPriceList.Controls.Add(this.label4);
            this.grpPriceList.Controls.Add(this.label1);
            this.grpPriceList.Controls.Add(this.dtpTo);
            this.grpPriceList.Controls.Add(this.dtpFrom);
            this.grpPriceList.Controls.Add(this.cmbProduct_PriceList);
            this.grpPriceList.Controls.Add(this.cmbPartyName_PriseList);
            this.grpPriceList.Location = new System.Drawing.Point(12, 12);
            this.grpPriceList.Name = "grpPriceList";
            this.grpPriceList.Size = new System.Drawing.Size(330, 349);
            this.grpPriceList.TabIndex = 34;
            this.grpPriceList.TabStop = false;
            this.grpPriceList.Text = "Price List";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(247, 317);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 124);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(312, 187);
            this.dataGridView1.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(146, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "To:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "From:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Date Range:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Product Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Party Name:";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(172, 95);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(87, 20);
            this.dtpTo.TabIndex = 3;
            this.dtpTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpTo_KeyPress);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(43, 95);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(88, 20);
            this.dtpFrom.TabIndex = 2;
            this.dtpFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFrom_KeyPress);
            // 
            // cmbProduct_PriceList
            // 
            this.cmbProduct_PriceList.FormattingEnabled = true;
            this.cmbProduct_PriceList.Location = new System.Drawing.Point(102, 47);
            this.cmbProduct_PriceList.Name = "cmbProduct_PriceList";
            this.cmbProduct_PriceList.Size = new System.Drawing.Size(220, 21);
            this.cmbProduct_PriceList.TabIndex = 1;
            this.cmbProduct_PriceList.Enter += new System.EventHandler(this.cmbProduct_PriceList_Enter);
            this.cmbProduct_PriceList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbProduct_PriceList_KeyPress);
            this.cmbProduct_PriceList.Leave += new System.EventHandler(this.cmbProduct_PriceList_Leave);
            // 
            // cmbPartyName_PriseList
            // 
            this.cmbPartyName_PriseList.FormattingEnabled = true;
            this.cmbPartyName_PriseList.Location = new System.Drawing.Point(102, 20);
            this.cmbPartyName_PriseList.Name = "cmbPartyName_PriseList";
            this.cmbPartyName_PriseList.Size = new System.Drawing.Size(220, 21);
            this.cmbPartyName_PriseList.TabIndex = 0;
            this.cmbPartyName_PriseList.Enter += new System.EventHandler(this.cmbPartyName_PriseList_Enter);
            this.cmbPartyName_PriseList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPartyName_PriseList_KeyPress);
            this.cmbPartyName_PriseList.Leave += new System.EventHandler(this.cmbPartyName_PriseList_Leave);
            // 
            // PriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RosyBrown;
            this.ClientSize = new System.Drawing.Size(356, 369);
            this.Controls.Add(this.grpPriceList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PriceList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PriceList";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PriceList_FormClosed);
            this.Load += new System.EventHandler(this.PriceList_Load);
            this.grpPriceList.ResumeLayout(false);
            this.grpPriceList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPriceList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cmbProduct_PriceList;
        private System.Windows.Forms.ComboBox cmbPartyName_PriseList;
    }
}