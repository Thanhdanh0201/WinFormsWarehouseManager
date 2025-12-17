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
    public partial class UserProfileForm : Form
    {
        public UserProfileForm(string userName, string userPhone, Image userImage)
        {
            InitializeComponent();
            this.lblName.Text = userName;
            this.lblPhone.Text = userPhone;

            
            if (userImage != null)
            {
                this.picAvatar.Image = userImage;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
