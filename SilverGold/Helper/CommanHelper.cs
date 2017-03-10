using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Helper
{
    class CommanHelper
    {
        public static int FormX = 0;
        public static int FormY = 0;

        public static void ChangeGridFormate(DataGridView grd)
        {

            grd.EnableHeadersVisualStyles = false;
            grd.ColumnHeadersDefaultCellStyle.BackColor = Color.Wheat;
            grd.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            grd.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            grd.RowHeadersDefaultCellStyle.BackColor = Color.NavajoWhite;
            // Change GridLine Color
            grd.GridColor = Color.Blue;
            // Change Grid Border Style
            grd.BorderStyle = BorderStyle.Fixed3D;
        }
        public static void ChangeGridFormate2(DataGridView grd)
        {
            grd.DefaultCellStyle.Font = new Font("Calibri", 10.25f, FontStyle.Regular);
            grd.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Regular);
            grd.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood;
            grd.EnableHeadersVisualStyles = false;
            grd.RowHeadersVisible = false;

            grd.BackgroundColor = Color.White;
        }
    }
}
