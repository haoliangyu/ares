using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Desktop.AddIns;

namespace ARES
{
    public class DrawPolylineTool : ARES.BaseDrawTool
    {
        #region Attributes

        private INewLineFeedback newLineFeedback = null;

        private bool isDrawing = false;

        #endregion

        #region Events

        protected override void OnActivate()
        {
            try
            {
                base.OnActivate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            base.OnMouseDown(arg);

            if (Painter.ActiveLayer != null && arg.Button == MouseButtons.Left)
            {
                try
                {
                    ValueSymbolForm valueSymbolForm = AddIn.FromID<ValueSymbolForm.AddinImpl>(ThisAddIn.IDs.ValueSymbolForm).UI;
                    selectedValue = valueSymbolForm.SelectedValue;
                    selectedColor = valueSymbolForm.SelectedColor;

                    IPoint startCoor = Raster.ScreenCoor2MapCoor(arg.X, arg.Y);

                    if (!isDrawing)
                    {
                        newLineFeedback = new NewLineFeedback();
                        newLineFeedback.Display = ArcMap.Document.ActiveView.ScreenDisplay;
                        newLineFeedback.Symbol = (ISymbol)Display.GetDefaultSelectLineSymbol();
                        newLineFeedback.Start(startCoor);
                    }

                    newLineFeedback.AddPoint(startCoor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs arg)
        {
            base.OnMouseMove(arg);

            if (isDrawing && arg.Button == MouseButtons.Left && Editor.ActiveLayer != null)
            {
                IPoint moveCoor = Raster.ScreenCoor2MapCoor(arg.X, arg.Y);
                newLineFeedback.MoveTo(moveCoor);
            }
        }

        protected override void OnDoubleClick()
        {
            base.OnDoubleClick();

            if (isDrawing && Editor.ActiveLayer != null)
            {
                IPoint startCoor = Raster.ScreenCoor2MapCoor(Control.MousePosition.X, Control.MousePosition.Y);
                newLineFeedback.AddPoint(startCoor);

                IPointCollection pointCollection = (IPointCollection)newLineFeedback.Stop();
                List<Position> pointList = new List<Position>();
                for (int i = 0; i < pointCollection.PointCount - 1; i++)
                {
                    Position startPos = Raster.MapCoor2RasterCoor(pointCollection.get_Point(i), Painter.ActiveLayer);
                    Position endPos = Raster.MapCoor2RasterCoor(pointCollection.get_Point(i + 1), Painter.ActiveLayer);

                    pointList.AddRange(Display.GetPolyline(startPos, endPos, layerExetent));
                }

                

                isDrawing = false;    
            }
        }

        #endregion
    }

}
