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

namespace ARES.Editor
{
    /// <summary>
    /// Provides access to the function and behavior of Identify tool.
    /// </summary>
    public class IdentifyTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public IdentifyTool()
        {
        }

        #region Attributes

        /// <summary>
        /// The maximum row and col index of current layer
        /// </summary>
        private Position maxExtent = null;

        private INewEnvelopeFeedback newEnvelopeFeedback = null;

        private IRasterLayer activeLayer = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set a value indicating whether the identify tool is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Examine whether the given rectangle region is within the extent of active layer. 
        /// </summary>
        /// <param name="tlCorner">Top-left corner of the given rectangle region.</param>
        /// <param name="brCorner">Bottom-right corner of the given rectangle region</param>
        /// <param name="maxIndex">Maximium extent of the active layer.</param>
        /// <returns></returns>
        private bool IsIntersect(Position tlCorner, Position brCorner, Position maxIndex)
        {
            return !(tlCorner.Column > maxIndex.Column || tlCorner.Row > maxIndex.Row || brCorner.Column < 0 || brCorner.Row < 0);
        }

        #endregion

        #region Events

        /// <summary>
        /// Remove all selecion and close the select window once the tool is disabled.
        /// </summary>
        /// <returns></returns>
        protected override bool OnDeactivate()
        {
            try
            {
                Display.ClearElement(Editor.Selections.GetAllGraphicElements());
                Editor.Selections.Clear();

                UID dockWinID = new UIDClass();
                dockWinID.Value = ThisAddIn.IDs.ARES_Editor_IdentifyForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (dockWindow.IsVisible())
                {
                    dockWindow.Show(false);
                    IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.ARES_Editor_IdentifyForm).UI;
                    identifyForm.ClearValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }

            return base.OnDeactivate();
        }

        /// <summary>
        /// Show the select form when the tool is abled.
        /// </summary>
        protected override void OnActivate()
        {
            try
            {
                UID dockWinID = new UIDClass();
                dockWinID.Value = ThisAddIn.IDs.ARES_Editor_IdentifyForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (!dockWindow.IsVisible())
                {
                    dockWindow.Show(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }

            base.OnActivate();
        }

        /// <summary>
        /// Remove all selection if there is no layer loaded
        /// </summary>
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;

            try
            {
                if (ArcMap.Document.FocusMap.LayerCount == 0)
                {
                    Display.ClearElement(Editor.Selections.GetAllGraphicElements());
                    Editor.Selections.Clear();

                    UID dockWinID = new UIDClass();
                    dockWinID.Value = ThisAddIn.IDs.ARES_Editor_IdentifyForm;
                    IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                    if (dockWindow.IsVisible())
                    {
                        IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.ARES_Editor_IdentifyForm).UI;
                        identifyForm.ClearValues();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        /// <summary>
        /// Start to track the rectangle when mouse down.
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            activeLayer = ArcMapApp.GetRasterLayer();

            if (activeLayer != null)
            {
                try
                {
                    Display.ClearElement(Editor.Selections.GetAllGraphicElements());
                    Editor.Selections.Clear();

                    // Define the selection symbol.
                    IRgbColor color = new RgbColorClass();
                    color.Red = 255;
                    color.Green = 255;
                    color.Blue = 255;

                    ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
                    lineSymbol.Width = 1;
                    lineSymbol.Color = (IColor)color;

                    IPoint startCoor = Raster.ScreenCoor2MapCoor(arg.X, arg.Y);
                    newEnvelopeFeedback = new NewEnvelopeFeedbackClass();
                    newEnvelopeFeedback.Display = ArcMap.Document.ActiveView.ScreenDisplay;
                    newEnvelopeFeedback.Symbol = (ISymbol)lineSymbol;
                    newEnvelopeFeedback.Start(startCoor);

                    // Get the maximum extent of the active layer.
                    IRasterProps rasterProps = (IRasterProps)activeLayer.Raster;
                    maxExtent = new Position(rasterProps.Width, rasterProps.Height);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }

        protected override void OnMouseMove(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseMove(arg);

            if (arg.Button == MouseButtons.Left && activeLayer != null)
            {
                IPoint moveCoor = Raster.ScreenCoor2MapCoor(arg.X, arg.Y);
                newEnvelopeFeedback.MoveTo(moveCoor);
            }
        }

        /// <summary>
        /// Select the pixels when mouse up.
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseUp(arg);

            if (activeLayer != null)
            {
                try
                {
                    IEnvelope envelop = newEnvelopeFeedback.Stop();

                    UID dockWinID = new UIDClass();
                    dockWinID.Value = ThisAddIn.IDs.ARES_Editor_IdentifyForm;

                    // Use GetDockableWindow directly as we want the client IDockableWindow not the internal class
                    IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                    IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.ARES_Editor_IdentifyForm).UI;

                    Position tlCorner, brCorner;
                    if (envelop.UpperLeft.IsEmpty)
                    {
                        tlCorner = Raster.ScreenCoor2RasterCoor(arg.X, arg.Y, activeLayer);
                        brCorner = tlCorner;
                    }
                    else
                    {
                        tlCorner = Raster.MapCoor2RasterCoor(envelop.UpperLeft, activeLayer);
                        brCorner = Raster.MapCoor2RasterCoor(envelop.LowerRight, activeLayer);
                    }

                    if (!IsIntersect(tlCorner, brCorner, maxExtent))
                    {
                        identifyForm.ClearValues();
                        return;
                    }

                    tlCorner.Adjust(0, 0, maxExtent.Column, maxExtent.Row);
                    brCorner.Adjust(0, 0, maxExtent.Column, maxExtent.Row);

                    // Show symbols of selected pixels
                    for (int row = tlCorner.Row; row <= brCorner.Row; row++)
                    {
                        for (int col = tlCorner.Column; col <= brCorner.Column; col++)
                        {
                            Pixel pixel = new Pixel(new Position(col, row));
                            pixel.GraphicElement = Display.DrawBox(pixel.Position, Editor.GetSelectionSymbol(), ArcMapApp.GetRasterLayer());
                            Editor.Selections.Add(pixel);
                        }
                    }

                    Display.PartialRefresh();

                    double[,] values = Raster.GetValues(tlCorner, brCorner, activeLayer.Raster);

                    identifyForm.SetValues(tlCorner, brCorner, values);
                    identifyForm.SetLayerName(activeLayer.Name);
                    if (!dockWindow.IsVisible())
                    {
                        dockWindow.Show(true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }

        #endregion
    }

}
