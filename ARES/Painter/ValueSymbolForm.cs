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
        }

        #region Attributes

        private IRasterLayer activeLayer = null;

        private double? selectedValue = null;

        private Color selectedColor = Color.Empty;

        private List<string> listedValue = new List<string>();

        private Random randomRGB = new Random();

        private int changeColorIndex = -1;

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
                if (selectedColor == Color.Empty)
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
                List<string> unlistedValues = new List<string>();
                for (int x = 0; x < table.RowCount(null); x++)
                {
                    string value = table.GetRow(x).get_Value(1).ToString();
                    if(!listedValue.Contains(value))
                    {
                        unlistedValues.Add(value);    
                    }
                }

                SelectValueForm selectValueForm = new SelectValueForm();
                selectValueForm.InitializeValues(unlistedValues.ToArray());
                selectValueForm.ValueName = "Value";  
                selectValueForm.NewValue = false;
                selectValueForm.MultiSelect = true;
                selectValueForm.Text = "Add new values for painting";

                if (selectValueForm.ShowDialog() == DialogResult.OK)
                {
                    string[] selectedValues = selectValueForm.SelectedValues;

                    foreach (string value in selectedValues)
                    {
                            ListViewItem layerItem = new ListViewItem();

                            layerItem.Text = value;
                            layerItem.SubItems.Add("    ");
 
                            layerItem.SubItems[1].BackColor = Color.FromArgb(randomRGB.Next(0, 255),
                                                                             randomRGB.Next(0, 255),
                                                                             randomRGB.Next(0, 255));

                            layerItem.UseItemStyleForSubItems = false;
                            valueListBox.Items.Add(layerItem);

                            listedValue.Add(value);
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
            selectValueForm.NewValue = false;
            selectValueForm.Text = "Remove values from painting";
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
            if (valueListBox.SelectedItems.Count > 0)
            {
                selectedValue = double.Parse(valueListBox.SelectedItems[0].Text);
                selectedColor = valueListBox.SelectedItems[0].SubItems[1].BackColor;
            }
            else
            {
                selectedValue = null;
                selectedColor = Color.Empty;
            }
        }

        private void valueListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                System.Drawing.Point mousepos = valueListBox.PointToClient(Control.MousePosition);
                ListViewHitTestInfo Ht = valueListBox.HitTest(mousepos);
                if (Ht.Item != null)
                {
                    int hitColumn = Ht.Item.SubItems.IndexOf(Ht.SubItem);
                    changeColorIndex = Ht.Item.Index;

                    // fill color
                    if (hitColumn == 1)
                    {
                        valueBoxContextMenuStrip.Show(Control.MousePosition);
                    }
                }
            }
        }

        private void changeColorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    valueListBox.Items[changeColorIndex].SubItems[1].BackColor = colorDialog.Color;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
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
