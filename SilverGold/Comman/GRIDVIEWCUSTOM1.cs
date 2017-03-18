﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Comman
{
    public partial class GRIDVIEWCUSTOM1 : DataGridView
    {
        [System.Security.Permissions.UIPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Extract the key code from the key value. 
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (key == Keys.Enter)
            {
                return this.ProcessTabKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand, Flags =
            System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (e.KeyCode == Keys.Enter)
            {
                return this.ProcessTabKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }
    }
}


