using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinFormsWarehouseManager.RJControls
{
    [DefaultEvent("ValueChanged")]
    public class RJDateTimePicker : UserControl
    {
        // Fields
        private Color skinColor = Color.FromArgb(39, 174, 96);
        private Color textColor = Color.White;
        private Color borderColor = Color.FromArgb(34, 153, 84);
        private int borderSize = 2;
        private int borderRadius = 8;
        private bool droppedDown = false;
        private Image calendarIcon;

        // Components
        private DateTimePicker dateTimePicker1;
        private Label labelText;
        private Button buttonIcon;

        // Win32 API
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int DTM_GETMONTHCAL = 0x1008;
        private const int MCM_SETCOLOR = 0x100A;
        private const int MCSC_BACKGROUND = 0;
        private const int MCSC_MONTHBK = 4;
        private const int MCSC_TEXT = 1;
        private const int MCSC_TITLEBK = 2;
        private const int MCSC_TITLETEXT = 3;
        private const int MCSC_TRAILINGTEXT = 5;

        // Properties
        [Category("RJ Code - Appearance")]
        public Color SkinColor
        {
            get { return skinColor; }
            set
            {
                skinColor = value;
                if (labelText != null) labelText.BackColor = value;
                if (buttonIcon != null) buttonIcon.BackColor = value;
                this.BackColor = value;
                UpdateCalendarColors();
                UpdateCalendarIcon();
                this.Invalidate();
            }
        }

        [Category("RJ Code - Appearance")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                if (labelText != null) labelText.ForeColor = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code - Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code - Appearance")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Padding = new Padding(borderSize);
                this.Invalidate();
            }
        }

        [Category("RJ Code - Appearance")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        [Category("RJ Code - Data")]
        public DateTime Value
        {
            get { return dateTimePicker1.Value; }
            set
            {
                dateTimePicker1.Value = value;
                UpdateTextValue();
            }
        }

        [Category("RJ Code - Data")]
        public DateTimePickerFormat Format
        {
            get { return dateTimePicker1.Format; }
            set { dateTimePicker1.Format = value; }
        }

        [Category("RJ Code - Data")]
        public string CustomFormat
        {
            get { return dateTimePicker1.CustomFormat; }
            set { dateTimePicker1.CustomFormat = value; }
        }

        // Events
        public event EventHandler ValueChanged;

        // Constructor
        public RJDateTimePicker()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.SuspendLayout();

            // DateTimePicker - KEY FIX: Đặt vào cuối để ở trên cùng
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(borderSize, borderSize);
            dateTimePicker1.MinimumSize = new Size(0, 35);
            dateTimePicker1.Size = new Size(this.Width - (borderSize * 2), 35);
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // KEY FIX: Opacity = 0 thay vì Visible = false
            dateTimePicker1.TabStop = false;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            dateTimePicker1.DropDown += dateTimePicker1_DropDown;
            dateTimePicker1.CloseUp += dateTimePicker1_CloseUp;
            dateTimePicker1.KeyPress += dateTimePicker1_KeyPress;

            // Label Text
            labelText = new Label();
            labelText.Dock = DockStyle.Fill;
            labelText.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            labelText.ForeColor = textColor;
            labelText.TextAlign = ContentAlignment.MiddleLeft;
            labelText.Padding = new Padding(15, 0, 0, 0);
            labelText.BackColor = skinColor;
            labelText.Click += surface_Click;

            // Button Icon
            buttonIcon = new Button();
            buttonIcon.Dock = DockStyle.Right;
            buttonIcon.FlatStyle = FlatStyle.Flat;
            buttonIcon.FlatAppearance.BorderSize = 0;
            buttonIcon.BackColor = skinColor;
            buttonIcon.Size = new Size(45, 35);
            buttonIcon.Cursor = Cursors.Hand;
            buttonIcon.ImageAlign = ContentAlignment.MiddleCenter;
            buttonIcon.Click += surface_Click;
            buttonIcon.TabStop = false;

            // User Control
            this.Controls.Add(labelText);
            this.Controls.Add(buttonIcon);
            this.Controls.Add(dateTimePicker1); // KEY FIX: Thêm cuối để ở trên cùng

            this.MinimumSize = new Size(200, 40);
            this.Size = new Size(250, 40);
            this.Font = new Font("Segoe UI", 9.5F);
            this.ForeColor = textColor;
            this.Cursor = Cursors.Hand;
            this.BackColor = skinColor;
            this.Padding = new Padding(borderSize);
            this.Click += surface_Click;

            this.ResumeLayout();

            UpdateCalendarIcon();
            UpdateTextValue();
        }

        // Private Methods
        private void UpdateCalendarIcon()
        {
            calendarIcon = CreateCalendarIcon();
            if (buttonIcon != null)
                buttonIcon.Image = calendarIcon;
        }

        private Image CreateCalendarIcon()
        {
            Bitmap bmp = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Color iconColor = skinColor.GetBrightness() >= 0.8F ? Color.FromArgb(64, 64, 64) : Color.White;

                using (Pen pen = new Pen(iconColor, 2))
                using (SolidBrush brush = new SolidBrush(iconColor))
                {
                    g.DrawRectangle(pen, 2, 5, 20, 16);
                    g.FillRectangle(brush, 2, 5, 20, 4);
                    g.DrawLine(pen, 7, 3, 7, 7);
                    g.DrawLine(pen, 17, 3, 17, 7);
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            g.FillRectangle(brush, 5 + j * 6, 11 + i * 5, 3, 3);
                        }
                    }
                }
            }
            return bmp;
        }

        private void UpdateCalendarColors()
        {
            if (dateTimePicker1 != null && dateTimePicker1.IsHandleCreated)
            {
                IntPtr hCalendar = SendMessage(dateTimePicker1.Handle, DTM_GETMONTHCAL, IntPtr.Zero, IntPtr.Zero);

                if (hCalendar != IntPtr.Zero)
                {
                    SendMessage(hCalendar, MCM_SETCOLOR, (IntPtr)MCSC_BACKGROUND, (IntPtr)ColorTranslator.ToWin32(Color.White));
                    SendMessage(hCalendar, MCM_SETCOLOR, (IntPtr)MCSC_MONTHBK, (IntPtr)ColorTranslator.ToWin32(Color.White));
                    SendMessage(hCalendar, MCM_SETCOLOR, (IntPtr)MCSC_TITLEBK, (IntPtr)ColorTranslator.ToWin32(skinColor));
                    SendMessage(hCalendar, MCM_SETCOLOR, (IntPtr)MCSC_TITLETEXT, (IntPtr)ColorTranslator.ToWin32(Color.White));
                    SendMessage(hCalendar, MCM_SETCOLOR, (IntPtr)MCSC_TEXT, (IntPtr)ColorTranslator.ToWin32(Color.FromArgb(64, 64, 64)));
                    SendMessage(hCalendar, MCM_SETCOLOR, (IntPtr)MCSC_TRAILINGTEXT, (IntPtr)ColorTranslator.ToWin32(Color.LightGray));
                }
            }
        }

        private void UpdateTextValue()
        {
            if (labelText != null && dateTimePicker1 != null)
                labelText.Text = dateTimePicker1.Text;
        }

        // Event Methods
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateTextValue();
            ValueChanged?.Invoke(this, e);
        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            droppedDown = true;
            UpdateCalendarColors();
            this.Invalidate();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            droppedDown = false;
            this.Invalidate();
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // KEY FIX: Sử dụng phương pháp mới để mở calendar
        private void surface_Click(object sender, EventArgs e)
        {
            OpenCalendar();
        }

        private void OpenCalendar()
        {
            if (dateTimePicker1 != null)
            {
                // KEY FIX: Focus vào DateTimePicker trước
                dateTimePicker1.Focus();
                // Đợi một chút để focus hoàn tất
                Application.DoEvents();
                // Mở dropdown
                SendKeys.Send("%{DOWN}");
            }
        }

        // Overridden Methods
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateCalendarColors();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (dateTimePicker1 != null)
            {
                this.Height = dateTimePicker1.Height + (borderSize * 2);
                dateTimePicker1.Width = this.Width - (borderSize * 2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (borderRadius > 1)
            {
                var rectSurface = Rectangle.Inflate(this.ClientRectangle, -1, -1);
                var rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent?.BackColor ?? this.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorder);
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;

                    if (borderSize >= 1)
                    {
                        graph.DrawPath(penBorder, pathBorder);
                    }
                    graph.DrawPath(penSurface, pathSurface);
                }
            }
            else
            {
                this.Region = new Region(this.ClientRectangle);
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                calendarIcon?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}