using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Comman
{
    public delegate void MyEventHandler(object source, KeyEventArgs keyData);
    public partial class GRIDVIEWCUSTOM1 : DataGridView
    {
        public event MyEventHandler EnterPress;
        public GRIDVIEWCUSTOM1()
        {
            this.EnterPress += new MyEventHandler(MyEvent);
        }
        
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
                KeyEventArgs e1 = new KeyEventArgs(keyData);
                this.EnterPress(this, e1);
                return this.ProcessTabKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }

        void MyEvent(object source, KeyEventArgs keyData)
        {
            if (this.EnterPress != null)
            {
                //do something
            }
        }

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand, Flags =
            System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (e.KeyCode == Keys.Enter)
            {
                this.EnterPress(this, e);
                return this.ProcessTabKey(e.KeyData);
            }
            return base.ProcessDataGridViewKey(e);
        }
    }
}


