using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Panel = System.Windows.Forms.Panel;
using System.Web.UI.WebControls;



namespace WinFormsWarehouseManager
{
    public partial class FormNhapKho : Form
    {
        //Fields
        private IconButton currBtn;
        private Panel leftBorderBtn;
        private int borderSize = 2;


        public FormNhapKho()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelNCC.Controls.Add(leftBorderBtn);
            panelSP.Controls.Add(leftBorderBtn);
            this.Padding = new Padding(borderSize);

        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(255, 179, 71);
            public static Color color2 = Color.FromArgb(256, 179, 71);
        }

        private void PanelSP1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void iconbtnAddNCC_Click(object sender, EventArgs e)
        {
            if(iconbtnAddNCC.Text != "Lưu")
            {
                iconbtnAddNCC.Text = "Lưu";
                iconbtnAddNCC.IconChar = IconChar.FloppyDisk;
                iconbtnAddNCC.ForeColor = Color.FromArgb(255,179,71);
                iconbtnAddNCC.IconColor = Color.FromArgb(255,179,71);
            }
            else
            {
                iconbtnAddNCC.Text = "Nhà cung cấp mới";
                iconbtnAddNCC.IconChar = IconChar.Pen;
                iconbtnAddNCC.ForeColor = Color.White;
                iconbtnAddNCC.IconColor = Color.White;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void iconbtnAddSP_Click(object sender, EventArgs e)
        {
            if (iconbtnAddSP.Text != "Lưu")
            {
                iconbtnAddSP.Text = "Lưu";
                iconbtnAddSP.IconChar = IconChar.FloppyDisk;
                iconbtnAddSP.ForeColor = Color.FromArgb(255, 179, 71);
                iconbtnAddSP.IconColor = Color.FromArgb(255, 179, 71);
            }
            else 
            {
                iconbtnAddSP.Text = "Sản phẩm mới";
                iconbtnAddSP.IconChar = IconChar.Pen;
                iconbtnAddSP.ForeColor = Color.White;
                iconbtnAddSP.IconColor = Color.White;
            }
        }
    }
}
