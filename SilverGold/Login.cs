using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnClose;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var directoryInfo = new System.IO.DirectoryInfo(Application.StartupPath);
            var dirName = directoryInfo.GetDirectories();
            if (dirName.Count() > 0)
            {

            }

            Master oMaster = new Master();
            oMaster.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
