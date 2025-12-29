using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace WinFormsWarehouseManager.CustomControls
{
    [DefaultEvent("_TextChanged")]
    public partial class CustomTextBox : UserControl
    {
        // Fields
        private Color borderColor = Color.FromArgb(4, 119, 154);
        private Color borderFocusColor = Color.FromArgb(0, 162, 173);
        private int borderSize = 2;
        private bool underlinedStyle = false;
        private bool isFocused = false;
        private int borderRadius = 8;
        private Color placeholderColor = Color.Gray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;

        // Controls
        private Panel panelIcon;
        private IconPictureBox iconPicture;
        private TextBox textBox1;
        private IconButton btnTogglePassword;

        // Properties
        [Category("Custom Props")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Props")]
        public Color BorderFocusColor
        {
            get { return borderFocusColor; }
            set { borderFocusColor = value; }
        }

        [Category("Custom Props")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                if (value >= 1)
                {
                    borderSize = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Custom Props")]
        public bool UnderlinedStyle
        {
            get { return underlinedStyle; }
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }

        [Category("Custom Props")]
        public bool PasswordChar
        {
            get { return isPasswordChar; }
            set
            {
                isPasswordChar = value;
                if (!isPlaceholder)
                    textBox1.UseSystemPasswordChar = value;
            }
        }

        [Category("Custom Props")]
        public bool Multiline
        {
            get { return textBox1.Multiline; }
            set { textBox1.Multiline = value; }
        }

        [Category("Custom Props")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }

        [Category("Custom Props")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }

        [Category("Custom Props")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (this.DesignMode)
                    UpdateControlHeight();
            }
        }

        [Category("Custom Props")]
        public string Texts
        {
            get
            {
                if (isPlaceholder) return "";
                else return textBox1.Text;
            }
            set
            {
                // Nếu set giá trị thực (không rỗng)
                if (!string.IsNullOrWhiteSpace(value))
                {
                    isPlaceholder = false;
                    textBox1.Text = value;
                    textBox1.ForeColor = this.ForeColor;  // Màu bình thường
                    if (isPasswordChar)
                        textBox1.UseSystemPasswordChar = true;
                }
                else
                {
                    // Nếu set rỗng thì hiện placeholder
                    textBox1.Text = "";
                    SetPlaceholder();
                }
            }
        }

        [Category("Custom Props")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Custom Props")]
        public Color PlaceholderColor
        {
            get { return placeholderColor; }
            set
            {
                placeholderColor = value;
                if (isPlaceholder)
                    textBox1.ForeColor = value;
            }
        }

        [Category("Custom Props")]
        public string PlaceholderText
        {
            get { return placeholderText; }
            set
            {
                placeholderText = value;
                textBox1.Text = "";
                SetPlaceholder();
            }
        }

        [Category("Custom Props")]
        public IconChar IconChar
        {
            get { return iconPicture.IconChar; }
            set
            {
                iconPicture.IconChar = value;
                panelIcon.Visible = (value != IconChar.None);
                UpdateTextBoxPosition();
            }
        }

        [Category("Custom Props")]
        public Color IconColor
        {
            get { return iconPicture.IconColor; }
            set { iconPicture.IconColor = value; }
        }

        [Category("Custom Props")]
        public bool ShowTogglePassword
        {
            get { return btnTogglePassword.Visible; }
            set
            {
                btnTogglePassword.Visible = value && isPasswordChar;
                UpdateTextBoxPosition();
            }
        }

        // Events
        public event EventHandler _TextChanged;

        // Constructor
        public CustomTextBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.panelIcon = new Panel();
            this.iconPicture = new IconPictureBox();
            this.btnTogglePassword = new IconButton();
            this.panelIcon.SuspendLayout();
            ((ISupportInitialize)(this.iconPicture)).BeginInit();
            this.SuspendLayout();

            // textBox1
            this.textBox1.BorderStyle = BorderStyle.None;
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Location = new Point(35, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(215, 19);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox1.Click += new EventHandler(this.textBox1_Click);
            this.textBox1.Enter += new EventHandler(this.textBox1_Enter);
            this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);

            // panelIcon
            this.panelIcon.Dock = DockStyle.Left;
            this.panelIcon.Location = new Point(0, 0);
            this.panelIcon.Name = "panelIcon";
            this.panelIcon.Padding = new Padding(5, 0, 0, 0);
            this.panelIcon.Size = new Size(35, 30);
            this.panelIcon.TabIndex = 1;
            this.panelIcon.Visible = false;
            this.panelIcon.Controls.Add(this.iconPicture);

            // iconPicture
            this.iconPicture.BackColor = Color.Transparent;
            this.iconPicture.Dock = DockStyle.Fill;
            this.iconPicture.ForeColor = Color.Gray;
            this.iconPicture.IconChar = IconChar.None;
            this.iconPicture.IconColor = Color.Gray;
            this.iconPicture.IconFont = IconFont.Auto;
            this.iconPicture.IconSize = 20;
            this.iconPicture.Location = new Point(5, 0);
            this.iconPicture.Name = "iconPicture";
            this.iconPicture.Size = new Size(30, 30);
            this.iconPicture.TabIndex = 0;
            this.iconPicture.TabStop = false;

            // btnTogglePassword
            this.btnTogglePassword.BackColor = Color.Transparent;
            this.btnTogglePassword.Cursor = Cursors.Hand;
            this.btnTogglePassword.Dock = DockStyle.Right;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatStyle = FlatStyle.Flat;
            this.btnTogglePassword.IconChar = IconChar.Eye;
            this.btnTogglePassword.IconColor = Color.Gray;
            this.btnTogglePassword.IconFont = IconFont.Auto;
            this.btnTogglePassword.IconSize = 20;
            this.btnTogglePassword.Location = new Point(220, 0);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new Size(30, 30);
            this.btnTogglePassword.TabIndex = 2;
            this.btnTogglePassword.UseVisualStyleBackColor = false;
            this.btnTogglePassword.Visible = false;
            this.btnTogglePassword.Click += new EventHandler(this.btnTogglePassword_Click);

            // CustomTextBox
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.White;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panelIcon);
            this.Controls.Add(this.btnTogglePassword);
            this.Font = new Font("Segoe UI", 10F);
            this.ForeColor = Color.FromArgb(64, 64, 64);
            this.Margin = new Padding(4);
            this.Name = "CustomTextBox";
            this.Size = new Size(250, 30);
            this.Resize += new EventHandler(this.CustomTextBox_Resize);
            this.panelIcon.ResumeLayout(false);
            ((ISupportInitialize)(this.iconPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Private methods
        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrEmpty(placeholderText))
            {
                isPlaceholder = true;
                textBox1.Text = placeholderText;
                textBox1.ForeColor = placeholderColor;
                if (isPasswordChar)
                    textBox1.UseSystemPasswordChar = false;
            }
        }

        private void RemovePlaceholder()
        {
            if (isPlaceholder && !string.IsNullOrEmpty(placeholderText))
            {
                isPlaceholder = false;
                textBox1.Text = "";
                textBox1.ForeColor = this.ForeColor;
                if (isPasswordChar)
                    textBox1.UseSystemPasswordChar = true;
            }
        }

        private void UpdateTextBoxPosition()
        {
            int leftPadding = panelIcon.Visible ? 35 : 10;
            int rightPadding = btnTogglePassword.Visible ? 35 : 10;

            textBox1.Location = new Point(leftPadding, 7);
            textBox1.Width = this.Width - leftPadding - rightPadding - borderSize * 2;
        }

        // Event methods
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
                _TextChanged.Invoke(sender, e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            this.Invalidate();
            RemovePlaceholder();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            this.Invalidate();
            SetPlaceholder();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (isPlaceholder) return;

            if (textBox1.UseSystemPasswordChar)
            {
                textBox1.UseSystemPasswordChar = false;
                btnTogglePassword.IconChar = IconChar.EyeSlash;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                btnTogglePassword.IconChar = IconChar.Eye;
            }
            textBox1.Focus();
        }

        private void CustomTextBox_Resize(object sender, EventArgs e)
        {
            if (this.DesignMode)
                UpdateControlHeight();
            UpdateTextBoxPosition();
        }

        private void UpdateControlHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;
                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }

        // Overridden methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (borderRadius > 1)
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(isFocused ? borderFocusColor : borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    if (borderRadius > 15) SetTextBoxRoundedRegion();
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;
                    if (!isFocused) penBorder.Alignment = PenAlignment.Inset;

                    if (underlinedStyle)
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                using (Pen penBorder = new Pen(isFocused ? borderFocusColor : borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = PenAlignment.Inset;
                    if (underlinedStyle)
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if (Multiline)
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius - borderSize);
                textBox1.Region = new Region(pathTxt);
            }
            else
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderSize * 2);
                textBox1.Region = new Region(pathTxt);
            }
            pathTxt.Dispose();
        }

        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }
    }
}