using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsWarehouseManager
{
    public partial class LocationAddingForm : Form
    {
        private Guna.UI2.WinForms.Guna2ImageCheckBox[] areas;
        private Guna.UI2.WinForms.Guna2CircleProgressBar[] statusbars;
        public LocationAddingForm()
        {
            InitializeComponent();

            // Reduce blurry rendering with DPI-aware scaling and double buffering
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.DoubleBuffered = true;

            // Make each label larger and center text
            var labels = new[] {
                lblstatus1, lblstatus2, lblstatus3, 
                lblA1, lblA2, lblA3, lblA4, lblA5, lblA6 };
            foreach (var l in labels)
            {
                l.Size = new Size(158, 28); // choose height to fit font
                l.TextAlignment = ContentAlignment.MiddleCenter;
                l.ForeColor = Color.White;
            }

            //Gan cho tung khu vuc
            areas = new[]
            {
                ckbA1, ckbA2, ckbA3, ckbA4, ckbA5, ckbA6
            };

            statusbars = new[]
            {
                statusbarA1, statusbarA2, statusbarA3,
                statusbarA4, statusbarA5, statusbarA6
            };
            foreach (var cb in areas)
            {
                cb.CheckedChanged += Area_CheckedChanged;
            }


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ckbA1_CheckedChanged(object sender, EventArgs e)
        {
            Area_CheckedChanged(sender, e);
        }


        // Common handler for all area checkboxes
        private void Area_CheckedChanged(object sender, EventArgs e)
        {
            var clicked = sender as Guna.UI2.WinForms.Guna2ImageCheckBox;
            if (clicked == null) return;

            // Only proceed when user tries to CHECK it (not when unchecking)
            if (!clicked.Checked) return;

            // Find index to map to corresponding statusbar
            int idx = Array.IndexOf(areas, clicked);
            if (idx < 0 || idx >= statusbars.Length) return;

            var status = statusbars[idx];

            // If statusbar color indicates "Full" (designer used Color.IndianRed),
            // block selection and show a warning message.
            if (status.BackColor == Color.IndianRed)
            {
                MessageBox.Show("Cảnh báo đã đầy và chọn lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Revert selection
                clicked.Checked = false;
                return;
            }
            else
            {
                btnSelect.Visible = true;
            }

                // Uncheck all other checkboxes so only one is selected at a time
                foreach (var cb in areas)
                {
                    if (!ReferenceEquals(cb, clicked))
                        cb.Checked = false;
                }
        }
    }
}
