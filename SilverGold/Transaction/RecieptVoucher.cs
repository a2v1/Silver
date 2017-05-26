using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Transaction
{
    public partial class RecieptVoucher : Form
    {
        public RecieptVoucher()
        {
            InitializeComponent();
        }

        private void RecieptVoucher_Load(object sender, EventArgs e)
        {
            this.Width = CommanHelper.FormX;
            this.Height = CommanHelper.FormY;
        }
    }
}
