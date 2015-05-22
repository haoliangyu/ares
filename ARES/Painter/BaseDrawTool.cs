using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Geodatabase;

namespace ARES
{
    public class BaseDrawTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public BaseDrawTool()
        {
            this.IsEnabled = false;
        }

        #region Attributes

        protected Envelope layerExetent = null;

        protected double? selectedValue = null;

        protected IColor selectedColor = null;

        #endregion

        #region Properties

        /// <summary>
        /// Indicate whether the select tool is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        #endregion

        #region Events

        protected override void OnActivate()
        {
            base.OnActivate();

            if (Painter.ActiveLayer == null)
            {
                return;
            }

            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.ValueSymbolForm;
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            if (!dockWindow.IsVisible())
            {
                dockWindow.Show(true);
            }

            IRasterLayer rasterLayer = (IRasterLayer)Painter.ActiveLayer;
            IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
            layerExetent = new Envelope(0, rasterProp.Height - 1, 0, rasterProp.Width - 1);
        }

        protected override bool OnDeactivate()
        {
            selectedColor = null;
            selectedValue = null;

            return base.OnDeactivate();
        }

        #endregion
    }
}
