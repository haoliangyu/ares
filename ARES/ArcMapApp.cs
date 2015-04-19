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

namespace ARES
{
    /// <summary>
    /// Provides additional functions to get access to the ArcMap.
    /// </summary>
    static class ArcMapApp
    {
        #region Properties

        /// <summary>
        /// Gets the number of raster layers in ArcMap.
        /// </summary>
        /// <returns></returns>
        static public int RasterLayerCount
        {
            get
            {
                int count = 0;

                for (int n = 0; n < ArcMap.Document.FocusMap.LayerCount; n++)
                {
                    ILayer mlayer = ArcMap.Document.FocusMap.get_Layer(n);
                    if (mlayer is IRasterLayer)
                    {
                        count++;
                    }
                }

                return count;
            }

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the layer with the specified name.
        /// </summary>
        /// <param name="layerName">Layer name.</param>
        public static ILayer GetLayer(string layerName)
        {
            for (int i = 0; i < ArcMap.Document.FocusMap.LayerCount; i++)
            {
                ILayer layer = ArcMap.Document.FocusMap.Layer[i];
                if (layer.Name == layerName)
                {
                    return layer;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the raster layer in ArcMap.
        /// </summary>
        /// <param name="index">Index of raster layer. Zero means topmost.</param>
        /// <returns></returns>
        public static IRasterLayer GetRasterLayer(int index = 0)
        {
            int layerCount = 0;

            for (int i = 0; i < ArcMap.Document.FocusMap.LayerCount; i++)
            {
                ILayer layer = ArcMap.Document.FocusMap.get_Layer(i);
                if (layer is IRasterLayer)
                {
                    if(layerCount == index)
                    {
                        return (IRasterLayer)layer;
                    }

                    layerCount++;
                }
            }

            return null;
        }

        #endregion
    }
}
