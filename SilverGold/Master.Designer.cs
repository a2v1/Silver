namespace SilverGold
{
    partial class Master
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCompToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partyInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labourRatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ghattakListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.introducerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupHeadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterToolStripMenuItem,
            this.masterInfoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCompToolStripMenuItem,
            this.createItemsToolStripMenuItem});
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.masterToolStripMenuItem.Text = "Company";
            // 
            // createCompToolStripMenuItem
            // 
            this.createCompToolStripMenuItem.Name = "createCompToolStripMenuItem";
            this.createCompToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.createCompToolStripMenuItem.Text = "Create Company";
            this.createCompToolStripMenuItem.Click += new System.EventHandler(this.createCompToolStripMenuItem_Click);
            // 
            // createItemsToolStripMenuItem
            // 
            this.createItemsToolStripMenuItem.Name = "createItemsToolStripMenuItem";
            this.createItemsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.createItemsToolStripMenuItem.Text = "Create Items";
            this.createItemsToolStripMenuItem.Click += new System.EventHandler(this.createItemsToolStripMenuItem_Click);
            // 
            // masterInfoToolStripMenuItem
            // 
            this.masterInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.partyInformationToolStripMenuItem,
            this.productDetailsToolStripMenuItem,
            this.labourRatesToolStripMenuItem,
            this.ghattakListToolStripMenuItem,
            this.introducerToolStripMenuItem,
            this.toolStripSeparator1,
            this.groupHeadToolStripMenuItem});
            this.masterInfoToolStripMenuItem.Name = "masterInfoToolStripMenuItem";
            this.masterInfoToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.masterInfoToolStripMenuItem.Text = "Master Info";
            // 
            // partyInformationToolStripMenuItem
            // 
            this.partyInformationToolStripMenuItem.Name = "partyInformationToolStripMenuItem";
            this.partyInformationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.partyInformationToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.partyInformationToolStripMenuItem.Text = "Party Information";
            this.partyInformationToolStripMenuItem.Click += new System.EventHandler(this.partyInformationToolStripMenuItem_Click);
            // 
            // productDetailsToolStripMenuItem
            // 
            this.productDetailsToolStripMenuItem.Name = "productDetailsToolStripMenuItem";
            this.productDetailsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.productDetailsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.productDetailsToolStripMenuItem.Text = "Product Details";
            this.productDetailsToolStripMenuItem.Click += new System.EventHandler(this.productDetailsToolStripMenuItem_Click);
            // 
            // labourRatesToolStripMenuItem
            // 
            this.labourRatesToolStripMenuItem.Name = "labourRatesToolStripMenuItem";
            this.labourRatesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.labourRatesToolStripMenuItem.Text = "Labour Rates";
            this.labourRatesToolStripMenuItem.Click += new System.EventHandler(this.labourRatesToolStripMenuItem_Click);
            // 
            // ghattakListToolStripMenuItem
            // 
            this.ghattakListToolStripMenuItem.Name = "ghattakListToolStripMenuItem";
            this.ghattakListToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.ghattakListToolStripMenuItem.Text = "Ghattak List";
            this.ghattakListToolStripMenuItem.Click += new System.EventHandler(this.ghattakListToolStripMenuItem_Click);
            // 
            // introducerToolStripMenuItem
            // 
            this.introducerToolStripMenuItem.Name = "introducerToolStripMenuItem";
            this.introducerToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.introducerToolStripMenuItem.Text = "Introducer";
            this.introducerToolStripMenuItem.Click += new System.EventHandler(this.introducerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // groupHeadToolStripMenuItem
            // 
            this.groupHeadToolStripMenuItem.Name = "groupHeadToolStripMenuItem";
            this.groupHeadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F5)));
            this.groupHeadToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.groupHeadToolStripMenuItem.Text = "Group Head";
            this.groupHeadToolStripMenuItem.Click += new System.EventHandler(this.groupHeadToolStripMenuItem_Click);
            // 
            // Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "Master";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Master_FormClosed);
            this.Load += new System.EventHandler(this.Master_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCompToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem partyInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labourRatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ghattakListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem introducerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem groupHeadToolStripMenuItem;

    }
}



