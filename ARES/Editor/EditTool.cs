using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Desktop.AddIns;

namespace ARES.Editor
{
    /// <summary>
    /// Provides access to the function and behavior of Edit tool.
    /// </summary>
    public class EditTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public EditTool()
        {
            this.Enabled = false;

            // Should be deleted later.
            if (File.Exists(Config.ConfigFile))
            {
                File.Delete(Config.ConfigFile);
            }

            if (!Config.IsLoaded)
            {
                Config.Load();
            }
        }

        #region Attributes

        private INewEnvelopeFeedback newEnvelopeFeedback = null;

        private ILayer activeLayer = null;

        private Position maxIndex = null;

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
                Display.ClearElement(Editor.Selections.GetAllGraphicElements());
                Editor.Selections.Clear();

                UID dockWinID = new UIDClass();
                dockWinID.Value = ThisAddIn.IDs.ARES_Editor_EditForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (dockWindow.IsVisible())
                {
                    dockWindow.Show(false);
                    EditForm editForm = AddIn.FromID<EditForm.AddinImpl>(ThisAddIn.IDs.ARES_Editor_EditForm).UI;
                    editForm.ClearValues();
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
                dockWinID.Value = ThisAddIn.IDs.ARES_Editor_EditForm;
                IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
                if (!dockWindow.IsVisible())
                {
                    dockWindow.Show(true);
                }

                activeLayer = Editor.ActiveLayer;
                IRasterLayer rasterLayer = (IRasterLayer)activeLayer;
                IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
                maxIndex = new Position(rasterProp.Width - 1, rasterProp.Height - 1);

                EditForm editForm = AddIn.FromID<EditForm.AddinImpl>(ThisAddIn.IDs.ARES_Editor_EditForm).UI;
                editForm.SetLayer(activeLayer.Name);
                System.Array noDataValue = (System.Array)rasterProp.NoDataValue;
                editForm.RasterGridView.NoDataValue = Convert.ToDouble(noDataValue.GetValue(0));
                editForm.SetNoDataValue(editForm.RasterGridView.NoDataValue);
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

            if (Editor.ActiveLayer != null)
            {
                try
                {
                    Display.ClearElement(Editor.Selections.GetAllGraphicElements());
                    Editor.Selections.Clear();

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

            if (arg.Button == MouseButtons.Left && Editor.ActiveLayer != null)
            {
                IPoint moveCoor = Editor.ScreenCoor2MapCoor(arg.X, arg.Y);
                newEnvelopeFeedback.MoveTo(moveCoor);
            }
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            if (Editor.ActiveLayer != null)
            {
                try
                {
                    UID uid = new UIDClass();
                    uid.Value = ThisAddIn.IDs.ARES_Editor_EditForm;
                    IDockableWindow dockWin = ArcMap.DockableWindowManager.GetDockableWindow(uid);
                    EditForm editForm = AddIn.FromID<EditForm.AddinImpl>(ThisAddIn.IDs.ARES_Editor_EditForm).UI;

                    IEnvelope envelop = newEnvelopeFeedback.Stop();

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
                        editForm.ClearValues();
                        return;
                    }

                    tlCorner.Adjust(0, 0, maxIndex.Column, maxIndex.Row);
                    brCorner.Adjust(0, 0, maxIndex.Column, maxIndex.Row);

                    // Show symbols of selected pixels
                    for (int row = tlCorner.Row; row <= brCorner.Row; row++)
                    {
                        for (int col = tlCorner.Column; col <= brCorner.Column; col++)
                        {
                            Pixel pixel = new Pixel(new Position(col, row));
                            pixel.GraphicElement = Display.DrawBox(pixel.Position, Editor.GetSelectionSymbol(), Editor.ActiveLayer);
                            Editor.Selections.Add(pixel);
                        }
                    }

                    IRasterLayer rasterLayer = (IRasterLayer)activeLayer;
                    double[,] values = Editor.GetValues(tlCorner, brCorner, rasterLayer.Raster);
                    editForm.SetValues(tlCorner, brCorner, values);

                    // If there is only one value, select that.
                    if (values.Length == 1)
                    {
                        editForm.RasterGridView[0, 0].Selected = false;
                        editForm.RasterGridView[1, 0].Selected = true;
                    }

                    if (!dockWin.IsVisible())
                    {
                        dockWin.Show(true);
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
