using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Desktop.AddIns;

namespace ARES
{
    /// <summary>
    /// ArcMap tool that provides identification function for values and statistic of raster pixels.
    /// </summary>
    public class LayerComboBox : ESRI.ArcGIS.Desktop.AddIns.ComboBox
    {
        /// <summary>
        /// Initialize the Layer ComboBox.
        /// </summary>
        public LayerComboBox()
        {
            // Save the current environment.
            currentMap = ArcMap.Document.FocusMap;
            layerCount = currentMap.LayerCount;

            LoadLayers();
        }

        #region Attributes

        private int layerCount = -1;
        private IMap currentMap = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set a value indicating whether the Layer ComboBox is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Periodically check whether map or layers changed.
        /// # Important # Some scripts are commented as the EditorControl is not implemented.
        /// </summary>
        protected override void OnUpdate()
        {
            if (this.currentMap != ArcMap.Document.FocusMap)
            {
                this.currentMap = ArcMap.Document.FocusMap;
                this.layerCount = currentMap.LayerCount;

                LoadLayers();
            }

            if (layerCount == currentMap.LayerCount)
                return;

            #region Check whether there are layers added or deleted

            if (layerCount > currentMap.LayerCount)
            {
                // If there are layer deleted
                List<int> removelist = new List<int>();
                bool activeLayerDeleted = false;

                foreach (Item item in this.items)
                {
                    bool flag = false;
                    for (int i = 0; i < currentMap.LayerCount; i++)
                    {
                        if (currentMap.Layer[i] == item.Tag)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                        removelist.Add(item.Cookie);
                }

                foreach (int cookie in removelist)
                {
                    Item item = this.GetItem(cookie);
                    if (Editor.ActiveLayer != null && item.Caption == Editor.ActiveLayer.Name)
                    {
                        activeLayerDeleted = true;
                    }

                    this.Remove(cookie);
                }

                if (activeLayerDeleted)
                {
                    Editor.ActiveLayer = null;
                }

                if (Editor.IsEditing && Editor.ActiveLayer == null)
                {
                    MessageBox.Show("The editing layer has been removed!", "Error");

                    Editor.StopEditing();
                }

                this.layerCount = currentMap.LayerCount;
            }
            else
            {
                // If there are layers added
                for (int i = 0; i < currentMap.LayerCount; i++)
                {
                    ILayer mlayer = currentMap.Layer[i];
                    if (mlayer is IRasterLayer)
                    {
                        bool flag = true;
                        for (int j = 0; j < this.items.Count; j++)
                        {
                            if (mlayer == (ILayer)this.items[j].Tag)
                            {
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            int cookie = this.Add(mlayer.Name, mlayer);
                        }
                    }
                }

                this.layerCount = currentMap.LayerCount;
            }

            #endregion
        }

        /// <summary>
        /// If one layer is selected, mark it as the active layer for the Editor                                    
        /// </summary>
        /// <param name="cookie"></param>
        protected override void OnSelChange(int cookie)
        {
            base.OnSelChange(cookie);

            if (cookie != -1)
            {
                Item selectedItem = this.GetItem(cookie);
                Editor.ActiveLayer = (ILayer)selectedItem.Tag;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load raster layers at ArcMap to the Layer ComboBox.
        /// </summary>
        private void LoadLayers()
        {
            this.Clear();

            for (int i = 0; i < currentMap.LayerCount; i++)
            {
                ILayer layer = currentMap.Layer[i];
                if (layer is IRasterLayer)
                {
                    int cookie = this.Add(layer.Name, layer);
                }
            }
        }

        #endregion
    }

}
