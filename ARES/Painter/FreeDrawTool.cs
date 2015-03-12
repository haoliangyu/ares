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
    /// <summary>
    /// Defines functions and behaviors of Edit tool on the Raster Painter toolbar.
    /// </summary>
    public class FreeDrawTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public FreeDrawTool()
        {
            this.Enabled = false;
        }

        #region Attributes

        private Envelope layerExetent = null;

        private Position preMousePos = null;

        private double? selectedValue = null;

        private IColor selectedColor = null;

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
            try
            {
                if (Painter.ActiveLayer == null)
                {
                    return;
                }

                UID dockWinID = new UIDClass();
                dockWinID.Value = ThisAddIn.IDs.ARES_Painter_ValueSymbolForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (!dockWindow.IsVisible())
                {
                    dockWindow.Show(true);
                }

                IRasterLayer rasterLayer = (IRasterLayer)Painter.ActiveLayer;
                IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
                layerExetent = new Envelope(0, rasterProp.Height - 1, 0, rasterProp.Width - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }

            base.OnActivate();
        }

        protected override bool OnDeactivate()
        {
            selectedColor = null;
            selectedValue = null;

            return base.OnDeactivate();
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            try
            {
                ValueSymbolForm valueSymbolForm = AddIn.FromID<ValueSymbolForm.AddinImpl>(ThisAddIn.IDs.ARES_Painter_ValueSymbolForm).UI;
                selectedValue = valueSymbolForm.SelectedValue;
                selectedColor = valueSymbolForm.SelectedColor;

                if ((Painter.ActiveLayer == null) ||
                    (arg.Button != MouseButtons.Left) ||
                    (selectedValue == null))
                {
                    return;
                }

                preMousePos = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, Painter.ActiveLayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnMouseMove(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseMove(arg);

            if ((Painter.ActiveLayer == null) ||
                (arg.Button != MouseButtons.Left) ||
                (selectedValue == null))
            {
                return;
            }

            try
            {
                Position mousePos = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, Painter.ActiveLayer);
                if (layerExetent.Contains(mousePos) && !(mousePos.Equals(preMousePos)))
                {
                    PaintPixel(mousePos);
                    preMousePos = mousePos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            try
            {
                if ((Painter.ActiveLayer == null) ||
                    (arg.Button != MouseButtons.Left) ||
                    (selectedValue == null))
                {
                    return;
                }
                else
                {
                    // If the mouse does not move, paint at the clicked pixel
                    Position mousePos = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, Painter.ActiveLayer);
                    if (layerExetent.Contains(mousePos))
                    {
                        PaintPixel(mousePos);
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

        #region Private Methods

        /// <summary>
        /// Paint the pixel at given position.
        /// </summary>
        /// <param name="pos"></param>
        private void PaintPixel(Position pos)
        {
            Pixel paintedPixel = Painter.Paints[pos];
            if (paintedPixel == null)
            {
                paintedPixel = new Pixel(Raster.GetValue(pos, Painter.ActiveLayer), pos);
                Painter.Paints.Add(paintedPixel);
            }
            else
            {
                Display.RemoveElement(paintedPixel.GraphicElement);
            }
            paintedPixel.NewValue = Convert.ToDouble(selectedValue);
            paintedPixel.GraphicElement = Display.DrawBox(pos,
                                  Painter.GetPaintSymbol(selectedColor),
                                  Painter.ActiveLayer,true);
        }

        #endregion
    }

}
