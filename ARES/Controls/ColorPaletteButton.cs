using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Reflection;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

namespace RasterEditor.Controls {

    /// <summary>
    /// Represents a split button with color palette to select a color.
    /// </summary>
    public class ColorPaletteButton : System.Windows.Forms.Button
    {

        #region Attributes

        private PushButtonState _state;
        private const int PushButtonWidth = 14;
        private int displayColorMargin = 2;
        private static int BorderSize = SystemInformation.Border3DSize.Width * 2;
        private Rectangle dropDownRectangle = new Rectangle();

        private Image nullColorImage = null;
        private IColorPalette colorPalette = new ColorPaletteClass();
        private IColor color = null;
        private tagRECT rect = new tagRECT();

        #endregion

        #region Properties

        private PushButtonState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (!_state.Equals(value))
                {
                    _state = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected color on the paltte.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IColor Color
        {
            set
            {
                if (value == this.color)
                    return;

                this.color = value;
                Invalidate();
            }
            get 
            {
                if (color == null)
                {
                    return new RgbColorClass() { NullColor = true };
                }

                return color; 
            }
        } 

        #endregion

        #region Construction Method

        /// <summary>
        /// Initialize the color palette split button.
        /// </summary>
        public ColorPaletteButton()
        {
            this.AutoSize = true;
            this.nullColorImage = Properties.Resources.NullColor32;
        }

        #endregion

        #region Events

        public override Size GetPreferredSize(Size proposedSize) {
            Size preferredSize = base.GetPreferredSize(proposedSize);
            if (!string.IsNullOrEmpty(Text) && TextRenderer.MeasureText(Text, Font).Width + PushButtonWidth > preferredSize.Width) {
                return preferredSize + new Size(PushButtonWidth + BorderSize * 2, 0);
            }
            return preferredSize;
        }

        protected override bool IsInputKey(Keys keyData) {
            if (keyData.Equals(Keys.Down)) {
                return true;
            }
            else {
                return base.IsInputKey(keyData);
            }
        }

        protected override void OnGotFocus(EventArgs e) {
            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Default;
            }
        }

        protected override void OnKeyDown(KeyEventArgs kevent) {
            if (kevent.KeyCode.Equals(Keys.Down))
            {
                State = PushButtonState.Pressed;
            }
            else if (kevent.KeyCode.Equals(Keys.Space) && kevent.Modifiers == Keys.None)
            {
                State = PushButtonState.Pressed;
            }

            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent) {
            if (kevent.KeyCode.Equals(Keys.Space)) {
                if (System.Windows.Forms.Control.MouseButtons == MouseButtons.None) {
                    State = PushButtonState.Normal;
                }
            }
            base.OnKeyUp(kevent);
        }

        protected override void OnLostFocus(EventArgs e) {
            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Normal;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {

            if (dropDownRectangle.Contains(e.Location)) {
                State = PushButtonState.Pressed;
                ShowColorPalette();
            }
            else {
                State = PushButtonState.Pressed;
            }
        }

        protected override void OnMouseEnter(EventArgs e) {
            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                State = PushButtonState.Hot;
            }
        }

        protected override void OnMouseLeave(EventArgs e) {

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled)) {
                if (Focused) {
                    State = PushButtonState.Default;
                }
                else {
                    State = PushButtonState.Normal;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {

            if (ContextMenuStrip == null || !ContextMenuStrip.Visible) {
                SetButtonDrawState();
                if (Bounds.Contains(Parent.PointToClient(Cursor.Position)) && !dropDownRectangle.Contains(mevent.Location)) {
                    OnClick(new EventArgs());
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent) {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            Rectangle bounds = this.ClientRectangle;

            // draw the button background as according to the current state.
            if (State != PushButtonState.Pressed && IsDefault && !Application.RenderWithVisualStyles) {
                Rectangle backgroundBounds = bounds;
                backgroundBounds.Inflate(-1, -1);
                ButtonRenderer.DrawButton(g, backgroundBounds, State);

                // button renderer doesnt draw the black frame when themes are off =(
                g.DrawRectangle(SystemPens.WindowFrame, 0, 0, bounds.Width - 1, bounds.Height - 1);

            }
            else {
                ButtonRenderer.DrawButton(g, bounds, State);
            }
            // calculate the current dropdown rectangle.
            dropDownRectangle = new Rectangle(bounds.Right - PushButtonWidth - 1, BorderSize, PushButtonWidth, bounds.Height - BorderSize * 2);

            int internalBorder = BorderSize;
            Rectangle focusRect =
                new Rectangle(internalBorder,
                              internalBorder,
                              bounds.Width - dropDownRectangle.Width - internalBorder,
                              bounds.Height - (internalBorder * 2));

            bool drawSplitLine = (State == PushButtonState.Hot || State == PushButtonState.Pressed || !Application.RenderWithVisualStyles);

            if (RightToLeft == RightToLeft.Yes) {
                dropDownRectangle.X = bounds.Left + 1;
                focusRect.X = dropDownRectangle.Right;
                if (drawSplitLine) {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Left + PushButtonWidth, BorderSize, bounds.Left + PushButtonWidth, bounds.Bottom - BorderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Left + PushButtonWidth + 1, BorderSize, bounds.Left + PushButtonWidth + 1, bounds.Bottom - BorderSize);
                }
            }
            else {
                if (drawSplitLine) {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Right - PushButtonWidth, BorderSize, bounds.Right - PushButtonWidth, bounds.Bottom - BorderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Right - PushButtonWidth - 1, BorderSize, bounds.Right - PushButtonWidth - 1, bounds.Bottom - BorderSize);
                }

            }

            // Draw an arrow in the correct location 
            PaintArrow(g, dropDownRectangle);

            // Figure out how to draw the text
            TextFormatFlags formatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

            // If we dont' use mnemonic, set formatFlag to NoPrefix as this will show ampersand.
            if (!UseMnemonic) {
                formatFlags = formatFlags | TextFormatFlags.NoPrefix;
            }
            else if (!ShowKeyboardCues) {
                formatFlags = formatFlags | TextFormatFlags.HidePrefix;
            }

            if (!string.IsNullOrEmpty(this.Text)) {
                TextRenderer.DrawText(g, Text, Font, focusRect, SystemColors.ControlText, formatFlags);
            }

            // draw the focus rectangle.
            if (State != PushButtonState.Pressed && Focused) {
                ControlPaint.DrawFocusRectangle(g, focusRect);
            }

            PaintColorRegion(g, this.ClientRectangle);
        }

        #endregion

        #region Methods

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void PaintArrow(Graphics g, Rectangle dropDownRect)
        {
            Point middle = new Point(Convert.ToInt32(dropDownRect.Left + dropDownRect.Width / 2), Convert.ToInt32(dropDownRect.Top + dropDownRect.Height / 2));

            //if the width is odd - favor pushing it over one pixel right.
            middle.X += (dropDownRect.Width % 2);

            Point[] arrow = new Point[] { new Point(middle.X - 2, middle.Y - 1), new Point(middle.X + 3, middle.Y - 1), new Point(middle.X, middle.Y + 2) };

            g.FillPolygon(SystemBrushes.ControlText, arrow);
        }

        private void PaintColorRegion(Graphics g, Rectangle drawSpace)
        {
            Rectangle location = new Rectangle();
            location.X = drawSpace.X + 5;
            location.Y = drawSpace.Y + 5;
            location.Width = drawSpace.Right - PushButtonWidth - drawSpace.X - displayColorMargin * 2 - 4;
            location.Height = drawSpace.Bottom - drawSpace.Y - displayColorMargin * 2 - 6;

            if (this.color == null || this.color.NullColor)
            {
                g.DrawImage(this.nullColorImage, location);
            }
            else 
            {
                IRgbColor rgbColor = (IRgbColor)this.color;
                System.Drawing.Color brushColor = System.Drawing.Color.FromArgb(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
                Brush brush = new SolidBrush(brushColor);
                g.FillRectangle(brush, location);
            }
        }

        private void ShowColorPalette()
        {
            IColor color = this.color;
            if (color == null)
            {
                color = new RgbColorClass() { NullColor = true }; 
            }
                        
            Point point = Parent.PointToScreen(this.Location);
            this.rect.left = point.X;
            this.rect.bottom = point.Y + this.Size.Height;

            if (colorPalette.TrackPopupMenu(this.rect, color, false, 0))
            {
                this.Color = colorPalette.Color;    
            }
        }

        public void SetButtonDrawState()
        {
            if (Bounds.Contains(Parent.PointToClient(Cursor.Position)))
            {
                State = PushButtonState.Hot;
            }
            else if (Focused)
            {
                State = PushButtonState.Default;
            }
            else
            {
                State = PushButtonState.Normal;
            }
        }

        #endregion
    }

}

