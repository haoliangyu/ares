using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

namespace ARES.Forms
{
    public partial class SelectLayerForm : Form
    {
        #region Constructor

        public SelectLayerForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Attributes

        private IRasterLayer returnLayer = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected raster layer.
        /// </summary>
        public IRasterLayer ReturnLayer
        {
            get { return returnLayer; }
        }

        #endregion

        #region Events

        private void SelectLayerForm_Load(object sender, EventArgs e)
        {
            label1.Text = "This map contains more than one editable raster layers.\nPlease choose the layer to edit.";

            // Update the layer information without drawing.
            layerListView.BeginUpdate();

            for (int i = 0; i < ArcMap.Document.FocusMap.LayerCount; i++)
            {
                ILayer player = ArcMap.Document.FocusMap.get_Layer(i);
                
                if (player is IRasterLayer)
                {
                    ListViewItem layerItem = new ListViewItem();
                    layerItem.Text = player.Name;
                    layerItem.SubItems.Add((player as IRasterLayer).FilePath);
                    layerListView.Items.Add(layerItem);
                }
            }
            
            // Drawing items of list view.
            layerListView.EndUpdate();
             
        }

        private void layerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If selected item changes, change its icon as edit style.
            foreach (ListViewItem item in layerListView.Items)
            {
                if (item.Checked)
                {
                    item.ImageIndex = 0;
                }
                else
                {
                    if (item.ImageIndex == 0)
                    {
                        item.ImageIndex = 1;
                    }
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            returnLayer = null;

            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (layerListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("No layer is selected to edit.", "Note");
                return;                                           
            }

            for (int i = 0; i < ArcMap.Document.FocusMap.LayerCount; i++)
            {
                ILayer player = ArcMap.Document.FocusMap.get_Layer(i);

                if (player is IRasterLayer)
                {
                    IRasterLayer rasterLayer = (IRasterLayer)player;
                    if (rasterLayer.FilePath == layerListView.SelectedItems[0].SubItems[1].Text)
                    {
                        returnLayer = rasterLayer;
                        break;
                    }

                }
            }

            this.Close();
        }

        #endregion


         
    }
}
