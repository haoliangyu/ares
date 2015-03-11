using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Geodatabase;

namespace ARES.Painter
{
    public class EraseTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public EraseTool()
        {
            this.Enabled = false;
        }

        #region Attributes

        private Envelope layerExetent = null;

        private Position preMousePos = null;

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

            IRasterLayer rasterLayer = (IRasterLayer)Painter.ActiveLayer;
            IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
            layerExetent = new Envelope(0, rasterProp.Height - 1, 0, rasterProp.Width - 1);
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            if ((Painter.ActiveLayer == null) ||
                (arg.Button != MouseButtons.Left))
            {
                return;
            }

            preMousePos = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, Painter.ActiveLayer);
        }

        protected override void OnMouseMove(MouseEventArgs arg)
        {
            base.OnMouseMove(arg);

            if ((Painter.ActiveLayer == null) || arg.Button != MouseButtons.Left)
            {
                return;
            }

            try
            {
                Position mousePos = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, Painter.ActiveLayer);
                if (layerExetent.Contains(mousePos) && !(mousePos.Equals(preMousePos)))
                {
                    Pixel paintedPixel = Painter.Paints[mousePos];
                    if (paintedPixel != null)
                    {
                        Display.RemoveElement(paintedPixel.GraphicElement, true);
                        Painter.Paints.Remove(mousePos);
                    }
                    preMousePos = mousePos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnMouseUp(MouseEventArgs arg)
        {
            base.OnMouseUp(arg);

            try
            {
                if ((Painter.ActiveLayer == null) || (arg.Button != MouseButtons.Left))
                {
                    return;
                }
                else
                {
                    // If the mouse does not move, paint at the clicked pixel
                    Position mousePos = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, Painter.ActiveLayer);
                    if (layerExetent.Contains(mousePos))
                    {
                        Pixel paintedPixel = Painter.Paints[mousePos];
                        if (paintedPixel != null)
                        {
                            Display.RemoveElement(paintedPixel.GraphicElement, true);
                            Painter.Paints.Remove(mousePos);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }

            preMousePos = null;
        }

        #endregion
    }

}
