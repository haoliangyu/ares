using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Desktop.AddIns;

using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;

using ARES.Forms;

namespace ARES
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class ValueSymbolForm : UserControl
    {
        public ValueSymbolForm(object hook)
        {
            InitializeComponent();
            this.Hook = hook;

            colorRampComboBox.DrawItem += new DrawItemEventHandler(comboBox1_DrawItem);

            FillColorRampList();
            colorRampComboBox.SelectedIndex = 0;
        }

        #region Attributes

        private IRasterLayer activeLayer = null;

        private int? selectedValue = null;

        private Color selectedColor;

        private List<string> listedValue = new List<string>();

        Random randomRGBCount = new Random();

        #endregion

        #region Properties

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Get the selected value. If there is no value selected, returns null.
        /// </summary>
        public int? SelectedValue
        {
            get { return selectedValue; }
        }

        /// <summary>
        /// Get the selected color. If there is no color selected, returns null.
        /// </summary>
        public IColor SelectedColor
        {
            get 
            {
                if (selectedColor == null)
                {
                    return null;
                }
                else
                {
                    IRgbColor rgbColor = new RgbColorClass();
                    rgbColor.Red = selectedColor.R;
                    rgbColor.Green = selectedColor.G;
                    rgbColor.Blue = selectedColor.B;
                    return (IColor)rgbColor;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load data of given active layer.
        /// </summary>
        /// <param name="activeLayer">A raster layer for painting.</param>
        public void LoadLayer(IRasterLayer activeLayer)
        {
            this.activeLayer = activeLayer;
            this.layerNameTextBox.Text = activeLayer.Name;
        }

        /// <summary>
        /// Clear all data of active layer.
        /// </summary>
        public void ClearLayer()
        {
            this.activeLayer = null;
            this.layerNameTextBox.Text = "";
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize color ramp list.
        /// </summary>
        private void FillColorRampList()
        {
            ColorRamp colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(176, 176, 176);
            colorRamp.toColor = Color.FromArgb(255, 0, 0);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(0, 0, 0);
            colorRamp.toColor = Color.FromArgb(255, 255, 255);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(204, 204, 255);
            colorRamp.toColor = Color.FromArgb(0, 0, 224);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(211, 229, 232);
            colorRamp.toColor = Color.FromArgb(46, 100, 140);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(203, 245, 234);
            colorRamp.toColor = Color.FromArgb(48, 207, 146);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(216, 242, 237);
            colorRamp.toColor = Color.FromArgb(21, 79, 74);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(240, 236, 170);
            colorRamp.toColor = Color.FromArgb(102, 72, 48);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(156, 85, 31);
            colorRamp.toColor = Color.FromArgb(33, 130, 145);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(48, 100, 102);
            colorRamp.toColor = Color.FromArgb(110, 70, 45);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(214, 47, 39);
            colorRamp.toColor = Color.FromArgb(69, 117, 181);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(245, 0, 245);
            colorRamp.toColor = Color.FromArgb(0, 245, 245);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(182, 237, 240);
            colorRamp.toColor = Color.FromArgb(9, 9, 145);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(175, 240, 233);
            colorRamp.toColor = Color.FromArgb(255, 252, 255);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(118, 219, 211);
            colorRamp.toColor = Color.FromArgb(255, 252, 255);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(219, 219, 219);
            colorRamp.toColor = Color.FromArgb(69, 69, 69);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(204, 255, 204);
            colorRamp.toColor = Color.FromArgb(14, 204, 14);
            colorRampComboBox.Items.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(220, 245, 233);
            colorRamp.toColor = Color.FromArgb(34, 102, 51);
            colorRampComboBox.Items.Add(colorRamp);
        }

        /// <summary>
        /// Calculate RGB values within given color range.
        /// </summary>
        /// <param name="FromColor"></param>
        /// <param name="ToColor"></param>
        /// <param name="RowCount"></param>
        /// <returns></returns>
        private int[,] CalculateRGB(Color FromColor, Color ToColor, int RowCount)
        {
            int[,] RGB_Result = new int[RowCount, 3];

            int RF = FromColor.R;
            int RT = ToColor.R;
            int GF = FromColor.G;
            int GT = ToColor.G;
            int BF = FromColor.B;
            int BT = ToColor.B;

            int RS = RF - RT;
            int GS = GF - GT;
            int BS = BF - BT;

            int NRS = RS / (RowCount - 1);
            int NGS = GS / (RowCount - 1);
            int NBS = BS / (RowCount - 1);

            for (int i = 0; i < RowCount; i++)
            {
                int R = RF - i * NRS;
                RGB_Result[i, 0] = R;

                int G = GF - i * NGS;
                RGB_Result[i, 1] = G;

                int B = BF - i * NBS;
                RGB_Result[i, 2] = B;
            }

            return RGB_Result;
        }

        #endregion

        #region Events

        private void addValueButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Painter.ActiveLayer == null)
                {
                    return;
                }

                // Get value list
                IDataset rasterDataset = (IDataset)Painter.ActiveLayer;
                ITable table = (ITable)rasterDataset;
                string[] values = new string[table.RowCount(null)];
                for (int x = 0; x < values.Length; x++)
                {
                    values[x] = table.GetRow(x).get_Value(1).ToString();
                }

                SelectValueForm selectValueForm = new SelectValueForm();
                selectValueForm.InitializeValues(values);
                selectValueForm.ValueName = "Value";

                if (selectValueForm.ShowDialog() == DialogResult.OK)
                {
                    string[] selectedValues = selectValueForm.SelectedValue;
                    
                    ColorRamp colorRamp = (ColorRamp)colorRampComboBox.Items[colorRampComboBox.SelectedIndex];
                    foreach (string value in values)
                    {
                        if (!listedValue.Contains(value))
                        {
                            ListViewItem layerItem = new ListViewItem();

                            layerItem.Text = value;
                            layerItem.SubItems.Add("    ");
 
                            int[]
                            layerItem.SubItems[1].BackColor = Color.FromArgb(RandomRGB.Next(colorRamp.fromColor.R, colorRamp.toColor.R),
                                                                             RandomRGB.Next(colorRamp.fromColor.G, colorRamp.toColor.G),
                                                                             RandomRGB.Next(colorRamp.fromColor.B, colorRamp.toColor.B));

                            layerItem.UseItemStyleForSubItems = false;
                            ValueListBox.Items.Add(layerItem);


                            listedValue.Add(value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        private void deleteValueButton_Click(object sender, EventArgs e)
        {
            if (Painter.ActiveLayer == null)
            {
                return;
            }

            SelectValueForm selectValueForm = new SelectValueForm();
            selectValueForm.InitializeValues(listedValue.ToArray());
            selectValueForm.ValueName = "Value";

            if (selectValueForm.ShowDialog() == DialogResult.OK)
            {
                foreach (string value in selectValueForm.SelectedValue)
                {
                    ValueListBox.Items.RemoveByKey(value); 
                }
            }
        } 

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorRamp Ramp_Color = colorRampComboBox.SelectedItem as ColorRamp;
            Color Ramp_FromC = Ramp_Color.fromColor;
            Color Ramp_ToC = Ramp_Color.toColor;
            int row_count = ValueListBox.Items.Count;
            int[,] ColorList = CalculateRGB(Ramp_FromC, Ramp_ToC, row_count);

            for (int i = 0; i < row_count; i++)
            {
                ValueListBox.Items[i].SubItems[1].BackColor = Color.FromArgb(ColorList[i, 0], ColorList[i, 1], ColorList[i, 2]);
            }
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //throw new NotImplementedException();
            if (colorRampComboBox.Items.Count == 0 || e.Index == -1)
                return;
            Rectangle borderRect;
            if ((e.State & DrawItemState.Selected) != 0)
            {
                //填充区域
                borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);
                //画边框
                Pen pen = new Pen(Color.FromArgb(0, 0, 0));
                e.Graphics.DrawRectangle(pen, borderRect);
            }
            else
            {
                //填充区域
                borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);
                Pen pen = new Pen(Color.FromArgb(255, 255, 255));
                e.Graphics.DrawRectangle(pen, borderRect);
            }

            ColorRamp colorRamp = (ColorRamp)colorRampComboBox.Items[e.Index];
            //渐变画刷
            LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, colorRamp.fromColor,
                                             colorRamp.toColor, LinearGradientMode.Horizontal);
            //填充区域
            borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);

            e.Graphics.FillRectangle(brush, borderRect);
        }

        #endregion

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private ValueSymbolForm m_windowUI;

            // this property is to provide access to the IdentifyForm
            internal ValueSymbolForm UI
            {
                get { return m_windowUI; }
            }

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new ValueSymbolForm(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }

        private void Color_TOC_Load(object sender, EventArgs e)
        {
            //Color_TOC_Control();
        }
        
        /// <summary>
        /// Color TOC
        /// </summary>
        
        public void Color_TOC_Control()
        {
            IRasterLayer color_layer = Editor.ActiveLayer as IRasterLayer;
            ValueListBox.Items.Clear();
            Color ccolor;
            ValueListBox.Width = this.Width;
            if (color_layer != null)
            {
                IRasterRenderer cRender = color_layer.Renderer;
                IDataset cDataset = color_layer as IDataset;

                Table cTable = cDataset as Table;
                int j = cTable.RowCount(null);
                string[,] cArr = new string[j, 2];

                ///set listview location
                columnHeader1.Width = ValueListBox.Width - 110;
              
                for (int x = 0; x < j; x++)
                {
                    ///Get Layer value and count of value
                    cArr[x, 0] = cTable.GetRow(x).get_Value(1).ToString();
                    cArr[x, 1] = cTable.GetRow(x).get_Value(2).ToString();

                    ListViewItem layerItem = new ListViewItem();

                    int[] COLORRGB = new int[3];
                    Random RandomRGB = new Random();
                    ///For color    
                    for (int q = 0; q < 3; q++)
                    {
                        COLORRGB[q] = RandomRGB.Next(0, 255);
                    }

                    ccolor = Color.FromArgb(COLORRGB[0], COLORRGB[1], COLORRGB[2]);
                                    
                    layerItem.Text = cArr[x, 0];
                    layerItem.SubItems.Add("    ");
                    layerItem.SubItems[1].BackColor =ccolor;

                    layerItem.UseItemStyleForSubItems = false;
                    ValueListBox.Items.Add(layerItem);
                }
            }

            else
            { }
        }      

        private void Color_List_Click(object sender, EventArgs e)
        {
            
            System.Drawing.Point mousepos = ValueListBox.PointToClient(Control.MousePosition);
            ListViewHitTestInfo Ht = ValueListBox.HitTest(mousepos);
            int hitColumn = Ht.Item.SubItems.IndexOf(Ht.SubItem);
            MessageBox.Show(hitColumn.ToString());
            //if (hitColumn == 1)
            //{
            //    if (colorDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        Color CD_color = colorDialog1.Color;
            //        Ht.Item.SubItems[1].BackColor = CD_color;
            //        ValueListBox.FullRowSelect = false;
            //        selectedValue = Convert.ToInt32(Ht.Item.SubItems[0].Text);
            //        selectedColor = CD_color;
            //    }
            //    else
            //    { }
            //}
            //else
            //{ }
        }


  
        /// <summary>
        /// Defines a color ramp.
        /// </summary>
        private class ColorRamp
        {
            public Color fromColor { get; set; }
            public Color toColor { get; set; }
        }
    }
}
