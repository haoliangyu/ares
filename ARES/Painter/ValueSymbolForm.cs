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
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

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
        }

        #region Attributes

        private IRasterLayer activeLayer = null;

        private double? selectedValue = null;

        // private Color selectedColor = Color.Empty;

        private IColor selectedIColor = null;

        private List<string> listedValue = new List<string>();

        private Random randomRGB = new Random();

        private int changeColorIndex = -1;

        private IColorPalette colorPalette = new ColorPaletteClass();

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
        public double? SelectedValue
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
                //if (selectedColor == Color.Empty)
                //{
                //    return null;
                //}
                //else
                //{
                //    IRgbColor rgbColor = new RgbColorClass();
                //    rgbColor.Red = selectedColor.R;
                //    rgbColor.Green = selectedColor.G;
                //    rgbColor.Blue = selectedColor.B;
                //    return (IColor)rgbColor;
                //}
                return selectedIColor;
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
            this.selectedIColor = null;
            this.selectedValue = null;
            this.layerNameTextBox.Text = "";

            valueListBox.Items.Clear();
            listedValue.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validate the input value.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <param name="validValue">If validation successes, return the validated value. If not, return null.</param>
        /// <returns>A value indicating whether the validationg is successful.</returns>
        private bool ValueValidate(string value, out object validValue)
        {
            IRasterProps rasterProp = (IRasterProps)Painter.ActiveLayer.Raster;

            return Raster.CSharpValue2PixelValue(value, rasterProp.PixelType, out validValue);
        }

        /// <summary>
        /// Add new value to the value symbol list.
        /// </summary>
        /// <param name="valueStr"></param>
        private void AddNewValueSymbol(string valueStr)
        {
            ListViewItem layerItem = new ListViewItem();

            layerItem.Text = valueStr;
            layerItem.SubItems.Add("    ");

            layerItem.SubItems[1].BackColor = Color.FromArgb(randomRGB.Next(0, 255),
                                                                randomRGB.Next(0, 255),
                                                                randomRGB.Next(0, 255));

            layerItem.UseItemStyleForSubItems = false;
            valueListBox.Items.Add(layerItem);

            listedValue.Add(valueStr);    
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

                if (Raster.IsCategorical(Painter.ActiveLayer))
                {
                    // If this data is a categorical data, we are able to get access
                    // to its attribute table and get the value list.

                    IDataset rasterDataset = (IDataset)Painter.ActiveLayer;
                    ITable table = (ITable)rasterDataset;
                    List<string> unlistedValues = new List<string>();
                    for (int x = 0; x < table.RowCount(null); x++)
                    {
                        string value = table.GetRow(x).get_Value(1).ToString();
                        if (!listedValue.Contains(value))
                        {
                            unlistedValues.Add(value);
                        }
                    }

                    SelectValueForm selectValueForm = new SelectValueForm();
                    selectValueForm.InitializeValues(unlistedValues.ToArray());
                    selectValueForm.ValueName = "Value";
                    selectValueForm.NewValue = false;
                    selectValueForm.MultiSelect = true;
                    selectValueForm.Text = "Add Values";

                    if (selectValueForm.ShowDialog() == DialogResult.OK)
                    {
                        string[] selectedValues = selectValueForm.SelectedValues;

                        foreach (string value in selectedValues)
                        {
                            AddNewValueSymbol(value);
                        }
                    }
                }
                else
                { 
                    // If it is a data with continuous data type, we are not able to 
                    // get a value list. We can only add new data without using a 
                    // value list.

                    addNewValueToolStripMenuItem.PerformClick();
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
            selectValueForm.NewValue = false;
            selectValueForm.Text = "Remove Values";
            selectValueForm.MultiSelect = true;

            if (selectValueForm.ShowDialog() == DialogResult.OK)
            {
                string[] removedValues = selectValueForm.SelectedValues;
                ListViewItem[] items = new ListViewItem[removedValues.Length];
                for(int i = 0; i < removedValues.Length; i++)
                {
                    for(int j = 0; j < valueListBox.Items.Count; j++)
                    {
                        if (valueListBox.Items[j].Text == removedValues[i])
                        {
                            items[i] = valueListBox.Items[j];
                            break;
                        }
                    }
                }
                foreach (ListViewItem item in items)
                {
                    listedValue.Remove(item.Text);
                    valueListBox.Items.Remove(item);
                }

                
            }
        }

        private void ValueListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (valueListBox.SelectedItems.Count > 0)
                {
                    selectedValue = double.Parse(valueListBox.SelectedItems[0].Text);
                    // selectedColor = valueListBox.SelectedItems[0].SubItems[1].BackColor;
                    selectedIColor = Display.Color2IColor(valueListBox.SelectedItems[0].SubItems[1].BackColor);
                }
                else
                {
                    selectedValue = null;
                    // selectedColor = Color.Empty;
                    selectedIColor = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        private void changeColorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    valueListBox.Items[changeColorIndex].SubItems[1].BackColor = colorDialog.Color;

                    // Redraw the paint symbol based on new fill color
                    double value = double.Parse(valueListBox.Items[changeColorIndex].Text);
                    PixelCollection pixels = Painter.Paints.FindAll(pixel => pixel.NewValue == value);
                    if (pixels.Count > 0)
                    {
                        IColor color = this.SelectedColor;
                        foreach (Pixel pixel in pixels)
                        {
                            Display.RemoveElement(pixel.GraphicElement);
                            pixel.GraphicElement = Display.DrawBox(pixel.Position, Painter.GetPaintSymbol(color), Painter.ActiveLayer);
                            ArcMap.Document.ActiveView.Refresh();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }

        private void addNewValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Painter.IsPainting)
                {
                    return;
                }

                SingleInputForm inputValueForm = new SingleInputForm("New Value:", "Add New Value");
                inputValueForm.ValueValidateMethod = this.ValueValidate;

                if ((inputValueForm.ShowDialog() == DialogResult.OK) &&
                    !listedValue.Contains(Convert.ToString(inputValueForm.Value)))
                {
                    ListViewItem layerItem = new ListViewItem();

                    layerItem.Text = Convert.ToString(inputValueForm.Value);
                    layerItem.SubItems.Add("    ");

                    layerItem.SubItems[1].BackColor = Color.FromArgb(randomRGB.Next(0, 255),
                                                                        randomRGB.Next(0, 255),
                                                                        randomRGB.Next(0, 255));

                    layerItem.UseItemStyleForSubItems = false;
                    valueListBox.Items.Add(layerItem);
                    listedValue.Add(layerItem.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
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
    }
}
