using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace WinFormsWarehouseManager.Forms
{
    public partial class ForgotPasswordModal : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private IconButton iconbtnExit;

        public string EmailEntered { get; private set; }

        public ForgotPasswordModal()
        {
            InitializeComponent();
            this.Load += ForgotPasswordModal_Load;
        }

        private void ForgotPasswordModal_Load(object sender, EventArgs e)
        {
            // Tạo nút Exit
            iconbtnExit = new IconButton
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                IconChar = IconChar.X,
                IconColor = Color.White,
                IconFont = IconFont.Auto,
                IconSize = 24,
                Size = new Size(40, 40),
                Location = new Point(this.Width - 45, 10),
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            iconbtnExit.FlatAppearance.BorderSize = 0;
            iconbtnExit.Click += IconbtnExit_Click;

            panelHeader.Controls.Add(iconbtnExit);
            iconbtnExit.BringToFront();

            // Thêm khả năng kéo form
            panelHeader.MouseDown += PanelHeader_MouseDown;
            panelHeader.MouseMove += PanelHeader_MouseMove;
            panelHeader.MouseUp += PanelHeader_MouseUp;

            lblTitle.MouseDown += PanelHeader_MouseDown;
            lblTitle.MouseMove += PanelHeader_MouseMove;
            lblTitle.MouseUp += PanelHeader_MouseUp;

            // Focus vào textbox
            txtEmail.Focus();
        }

        private void PanelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void PanelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void PanelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void IconbtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Texts.Trim();

            // Validate email
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Validate email format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            EmailEntered = email;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Vẽ shadow xung quanh form
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(180, 190, 200), 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Đóng khi nhấn ESC
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            // Submit khi nhấn Enter
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(this, EventArgs.Empty);
            }
        }
    }
}