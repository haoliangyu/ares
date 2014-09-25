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

using RasterEditor.Forms;
using RasterEditor.Raster;

namespace RasterEditor                        
{
    /// <summary>
    /// Provides access to members for identification function for values and statistic of raster pixels.
    /// </summary>
    public class IdentifyTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public IdentifyTool()
        {
           
        }

        #region Attributes

        /// <summary>
        /// The current layer
        /// </summary>
        private ILayer activeLayer = null;

        /// <summary>
        /// The maximum row and col index of current layer
        /// </summary>
        private Position maxIndex = null;

        private INewEnvelopeFeedback newEnvelopeFeedback = null;

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

        #region Methods

        /// <summary>
        /// Examine whether the region specified by the given top-left corner and bottom-right corner initersects with given the maximum extent.
        /// </summary>
        /// <param name="tlCorner"></param>
        /// <param name="brCorner"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        private bool IsIntersect(Position tlCorner, Position brCorner, Position maxIndex)
        {
            return !(tlCorner.Column > maxIndex.Column || tlCorner.Row > maxIndex.Row || brCorner.Column < 0 || brCorner.Row < 0);
        }

        #endregion

        #region Events

        protected override bool OnDeactivate()
        {
            try
            {
                Display.ClearSelection();
                Editor.SelectionRecord.Clear();

                UID dockWinID = new UIDClass();
                dockWinID.Value = ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (dockWindow.IsVisible())
                {
                    dockWindow.Show(false);
                    IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm).UI;
                    identifyForm.ClearValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }

            return base.OnDeactivate();
        }
         
        protected override void OnActivate()
        {
            try
            {
                UID dockWinID = new UIDClass();
                dockWinID.Value = ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (!dockWindow.IsVisible() && Editor.ActiveLayer != null)
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

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;

            try
            {
                if (activeLayer != Editor.ActiveLayer)
                {
                    activeLayer = Editor.ActiveLayer;

                    Display.ClearSelection();
                    Editor.SelectionRecord.Clear();

                    if (Editor.ActiveLayer != null)
                    {
                        IRasterLayer rasterLayer = (IRasterLayer)activeLayer;
                        IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
                        maxIndex = new Position(rasterProp.Width - 1, rasterProp.Height - 1);

                        IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm).UI;
                        identifyForm.ClearValues();
                        identifyForm.SetLayer(activeLayer.Name);
                        System.Array noDataValue = (System.Array)rasterProp.NoDataValue;
                        identifyForm.RasterGridView.NoDataValue = Convert.ToDouble(noDataValue.GetValue(0));
                    }
                    else
                    {
                        IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm).UI;
                        identifyForm.ClearValues();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            if (Editor.ActiveLayer != null)
            {
                try
                {
                    Display.ClearSelection();
                    Editor.SelectionRecord.Clear();

                    IRgbColor color = new RgbColorClass();
                    color.Red = 255;
                    color.Green = 255;
                    color.Blue = 255;

                    ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
                    lineSymbol.Width = 1;
                    lineSymbol.Color = (IColor)color;

                    IPoint startCoor = Editor.ScreenCoor2MapCoor(arg.X, arg.Y);
                    newEnvelopeFeedback = new NewEnvelopeFeedbackClass();
                    newEnvelopeFeedback.Display = ArcMap.Document.ActiveView.ScreenDisplay;
                    newEnvelopeFeedback.Symbol = (ISymbol)lineSymbol;
                    newEnvelopeFeedback.Start(startCoor);
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

            if(arg.Button == MouseButtons.Left && Editor.ActiveLayer != null)
            {
                IPoint moveCoor = Editor.ScreenCoor2MapCoor(arg.X, arg.Y);
                newEnvelopeFeedback.MoveTo(moveCoor);
            }
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseUp(arg);

            if (Editor.ActiveLayer != null)
            {
                try
                {
                    IEnvelope envelop = newEnvelopeFeedback.Stop();

                    UID dockWinID = new UIDClass();
                    dockWinID.Value = ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm;

                    // Use GetDockableWindow directly as we want the client IDockableWindow not the internal class
                    IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                    IdentifyForm identifyForm = AddIn.FromID<IdentifyForm.AddinImpl>(ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm).UI;

                    Position tlCorner, brCorner;
                    if (envelop.UpperLeft.IsEmpty)
                    {
                        tlCorner = Editor.ScreenCoor2RasterCoor(arg.X, arg.Y);
                        brCorner = tlCorner;
                    }
                    else
                    {
                        tlCorner = Editor.MapCoor2RasterCoor(envelop.UpperLeft);
                        brCorner = Editor.MapCoor2RasterCoor(envelop.LowerRight);
                    }

                    if (!IsIntersect(tlCorner, brCorner, maxIndex))
                    {
                        identifyForm.ClearValues();
                        return;
                    }

                    tlCorner.Adjust(Raster.RasterFile.Origin ,maxIndex);
                    brCorner.Adjust(Raster.RasterFile.Origin, maxIndex);

                    Display.DrawSelectionBox(tlCorner, brCorner);
                    double[,] values = Editor.GetValues(tlCorner, brCorner, ((IRasterLayer)Editor.ActiveLayer).Raster);

                    identifyForm.SetValues(tlCorner, brCorner, values);
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
