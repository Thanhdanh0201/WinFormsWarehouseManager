using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace WinFormsWarehouseManager.RJControls
{
    public class CustomDateTimePicker : DateTimePicker
    {
        // Fields - Appearance
        private Color skinColor = Color.FromArgb(2, 51, 66);
        private Color textColor = Color.White;
        private Color borderColor = Color.FromArgb(222, 226, 230);
        private Color borderColorFocus = Color.FromArgb(2, 51, 66);
        private Color borderColorHover = Color.FromArgb(100, 150, 170);
        private int borderSize = 1;
        private int borderRadius = 4;

        // Fields - State
        private bool droppedDown = false;
        private bool isHovered = false;
        private bool isFocused = false;

        // Fields - Icon
        private RectangleF iconButtonArea;
        private const int calendarIconWidth = 34;
        private const int arrowIconWidth = 17;
        private int iconSize = 28; // Default size

        // Properties
        public Color SkinColor
        {
            get { return skinColor; }
            set
            {
                skinColor = value;
                this.Invalidate();
            }
        }

        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        public Color BorderColorFocus
        {
            get { return borderColorFocus; }
            set
            {
                borderColorFocus = value;
                this.Invalidate();
            }
        }

        public Color BorderColorHover
        {
            get { return borderColorHover; }
            set
            {
                borderColorHover = value;
                this.Invalidate();
            }
        }

        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        public int IconSize
        {
            get { return iconSize; }
            set
            {
                iconSize = value;
                this.Invalidate();
            }
        }

        // Constructor
        public CustomDateTimePicker()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.MinimumSize = new Size(193, 55);
            this.Size = new Size(223, 55);
            this.Font = new Font("Segoe UI", 9F);
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "MMM dd, yyyy";
        }

        // Overridden methods
        protected override void OnDropDown(EventArgs eventargs)
        {
            base.OnDropDown(eventargs);
            droppedDown = true;
            Invalidate();
        }

        protected override void OnCloseUp(EventArgs eventargs)
        {
            base.OnCloseUp(eventargs);
            droppedDown = false;
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Determine border color based on state
            Color currentBorderColor = borderColor;
            if (isFocused)
                currentBorderColor = borderColorFocus;
            else if (isHovered)
                currentBorderColor = borderColorHover;

            // Client area
            RectangleF clientArea = new RectangleF(0, 0, this.Width - 0.5F, this.Height - 0.5F);
            RectangleF iconArea = new RectangleF(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height);

            // Draw background with rounded corners
            using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, this.Width - 1, this.Height - 1), borderRadius))
            {
                // Fill background
                using (SolidBrush bgBrush = new SolidBrush(skinColor))
                {
                    g.FillPath(bgBrush, path);
                }

                // Draw border
                if (borderSize >= 1)
                {
                    using (Pen borderPen = new Pen(currentBorderColor, borderSize))
                    {
                        borderPen.Alignment = PenAlignment.Inset;
                        g.DrawPath(borderPen, path);
                    }
                }
            }

            // Draw open calendar icon highlight
            if (droppedDown)
            {
                using (SolidBrush openIconBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                {
                    using (GraphicsPath iconPath = GetRoundedRectangle(
                        new Rectangle((int)iconArea.X, (int)iconArea.Y, (int)iconArea.Width - 1, (int)iconArea.Height - 1),
                        borderRadius))
                    {
                        g.FillPath(openIconBrush, iconPath);
                    }
                }
            }

            // Draw text
            using (SolidBrush textBrush = new SolidBrush(textColor))
            using (StringFormat textFormat = new StringFormat())
            {
                textFormat.LineAlignment = StringAlignment.Center;
                textFormat.Alignment = StringAlignment.Near;
                RectangleF textArea = new RectangleF(15, 0, clientArea.Width - calendarIconWidth - 15, clientArea.Height);
                g.DrawString(this.Text, this.Font, textBrush, textArea, textFormat);
            }

            // Draw calendar icon using IconChar
            DrawCalendarIcon(g);

            // Draw dropdown arrow
            DrawDropdownArrow(g);
        }

        private void DrawCalendarIcon(Graphics g)
        {
            int iconX = this.Width - calendarIconWidth + 1;
            int iconY = (this.Height - iconSize) / 2;

            // Create a temporary IconPictureBox to render the icon
            using (var tempIcon = new IconPictureBox())
            {
                tempIcon.IconChar = IconChar.CalendarDays;
                tempIcon.IconColor = textColor;
                tempIcon.IconSize = iconSize;
                tempIcon.Size = new Size(iconSize, iconSize);

                // Create bitmap from icon with anti-aliasing
                using (Bitmap bmp = new Bitmap(iconSize, iconSize))
                {
                    using (Graphics iconGraphics = Graphics.FromImage(bmp))
                    {
                        iconGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                        iconGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        tempIcon.DrawToBitmap(bmp, new Rectangle(0, 0, iconSize, iconSize));
                    }
                    g.DrawImage(bmp, iconX, iconY);
                }
            }
        }

        private void DrawDropdownArrow(Graphics g)
        {
            int arrowSize = 7; // Tăng từ 6 lên 7
            int arrowX = this.Width - 11 - arrowSize / 2;
            int arrowY = (this.Height - arrowSize) / 2 + 2;

            using (SolidBrush arrowBrush = new SolidBrush(textColor))
            {
                Point[] arrowPoints = {
                    new Point(arrowX - arrowSize / 2, arrowY),
                    new Point(arrowX + arrowSize / 2, arrowY),
                    new Point(arrowX, arrowY + arrowSize / 2)
                };
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPolygon(arrowBrush, arrowPoints);
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));

            // Top left
            path.AddArc(arcRect, 180, 90);

            // Top right
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // Bottom right
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // Bottom left
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();
            return path;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            int iconWidth = GetIconButtonWidth();
            iconButtonArea = new RectangleF(this.Width - iconWidth, 0, iconWidth, this.Height);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (iconButtonArea.Contains(e.Location))
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            isFocused = true;
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isFocused = false;
            Invalidate();
        }

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        // Private methods
        private int GetIconButtonWidth()
        {
            int textWidth = TextRenderer.MeasureText(this.Text, this.Font).Width;
            if (textWidth <= this.Width - (calendarIconWidth + 20))
                return calendarIconWidth;
            else
                return arrowIconWidth;
        }
    }
}