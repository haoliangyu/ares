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

namespace ARES
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

        protected override bool OnDeactivate()
        {
            try
            {

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
                //UID dockWinID = new UIDClass();
                //dockWinID.Value = ThisAddIn.IDs.EditForm;
                //IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                //if (!dockWindow.IsVisible())
                //{
                //    dockWindow.Show(true);
                //}

                //activeLayer = Painter.ActiveLayer;
                //IRasterLayer rasterLayer = (IRasterLayer)activeLayer;
                //IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
                //maxIndex = new Position(rasterProp.Width -1, rasterProp.Height -1);

                //EditForm editForm = AddIn.FromID<EditForm.AddinImpl>(ThisAddIn.IDs.EditForm).UI;
                //editForm.SetLayer(activeLayer.Name);
                //System.Array noDataValue = (System.Array)rasterProp.NoDataValue;
                //editForm.RasterGridView.NoDataValue = Convert.ToDouble(noDataValue.GetValue(0));
                //editForm.SetNoDataValue(editForm.RasterGridView.NoDataValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }

            base.OnActivate();
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            if (Painter.ActiveLayer != null)
            {
                try
                {
                    //Display.ClearElement(Painter.Selections.GetAllGraphicElements());
                    //Painter.Selections.Clear();

                    //IRgbColor color = new RgbColorClass();
                    //color.Red = 255;
                    //color.Green = 255;
                    //color.Blue = 255;

                    //ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
                    //lineSymbol.Width = 1;
                    //lineSymbol.Color = (IColor)color;
                    

                    //IPoint startCoor = Painter.ScreenCoor2MapCoor(arg.X, arg.Y);
                    //newLineFeedback = new NewLineFeedbackClass();
                    //newLineFeedback.Display = ArcMap.Document.ActiveView.ScreenDisplay;
                    //newLineFeedback.Symbol = (ISymbol)lineSymbol;
                    //newLineFeedback.Start(startCoor);
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

            //if (arg.Button == MouseButtons.Left && Painter.ActiveLayer != null)
            //{
            //    IPoint moveCoor = Painter.ScreenCoor2MapCoor(arg.X, arg.Y);
            //    newLineFeedback.MoveTo(moveCoor);
            //}
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            //if (Painter.ActiveLayer != null)
            //{
            //    try
            //    {
            //        UID uid = new UIDClass();
            //        uid.Value = ThisAddIn.IDs.EditForm;
            //        IDockableWindow dockWin = ArcMap.DockableWindowManager.GetDockableWindow(uid);
            //        EditForm editForm = AddIn.FromID<EditForm.AddinImpl>(ThisAddIn.IDs.EditForm).UI;
                                        
            //        IPolyline polyline = newLineFeedback.Stop();
                    
                                                          
                                                            
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            //    }
            //}
        }

        #endregion
    }

}
