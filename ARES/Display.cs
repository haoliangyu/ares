using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

using ARES;

namespace ARES
{
    public static class Display
    {
        #region Public Methods

        /// <summary>
        /// Dras all selection boxes.
        /// </summary>
        /// <param name="activeLayer">Raster layer to be symbolized.</param>
        public static void DrawSelectionBox(ILayer activeLayer = null)
        {
            // Probably should be removed.
            ILayer layer = activeLayer == null ? Editor.ActiveLayer : activeLayer;

            if (layer != null)
            {
                for (int i = 0; i < Editor.SelectionRecord.Count; i++)
                {
                    Display.DrawSelectionBox(Editor.SelectionRecord[i].Position, false, activeLayer);
                }
            }
        }                                                                     

        /// <summary>
        /// Draw a selection box at the selected pixel.
        /// </summary>
        /// <param name="position">Position of the selected pixel.</param>
        /// <param name="activeLayer">Raster layer to be symbolized.</param>
        public static void DrawSelectionBox(Position position, bool refresh = false, ILayer activeLayer = null)
        {
            // Probably should be removed.
            ILayer layer = activeLayer == null ? Editor.ActiveLayer : activeLayer;

            if (layer != null)
            {
                Pixel cell = Editor.SelectionRecord[position];
                if (cell != null)
                    return;

                IElement element = DrawBoxElement(Config.SelectionSmbol, position, layer);

                Pixel newCell = new Pixel(0, position);
                newCell.GraphicElement = element;
                Editor.SelectionRecord.Add(newCell);

                if (refresh)
                {
                    ArcMap.Document.ActiveView.Refresh();    
                }
            }
       }

        /// <summary>
        /// Draw a selection box.
        /// </summary>
        /// <param name="brCorner">Buttom-right corner of the selected region.</param>
        /// <param name="tlCorner">Top-left corner of the selected region.</param>
        /// <param name="activeLayer">Raster layer to be symbolized.</param>
        public static void DrawSelectionBox(Position tlCorner, Position brCorner, ILayer activeLayer = null)
        {
            // Probably should be removed.
            ILayer layer = activeLayer == null ? Editor.ActiveLayer : activeLayer;

            if (layer != null)
            {
                IRasterLayer rasterLayer = (IRasterLayer)layer;
                IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
                brCorner.Adjust(RasterFile.Origin.Column, RasterFile.Origin.Row, 
                                rasterProp.Width - 1, rasterProp.Height - 1);

                for (int col = tlCorner.Column; col <= brCorner.Column; col++)
                {
                    for (int row = tlCorner.Row; row <= brCorner.Row; row++)
                    {
                        DrawSelectionBox(new Position(col, row), false, layer);
                    }
                }
            }
        }

        /// <summary>
        /// Remove the selection box at the given position.
        /// </summary>
        /// <param name="pos"></param>
        public static void RemoveSelectionBox(Position pos)
        {
            if (Editor.SelectionRecord.Exists(pos.Column, pos.Row))
            {
                Pixel pixel = Editor.SelectionRecord[pos];
                ArcMap.Document.ActiveView.GraphicsContainer.DeleteElement(pixel.GraphicElement);
                pixel.GraphicElement = null;
                ArcMap.Document.ActiveView.Refresh();
            }
        }

        /// <summary>
        /// Clear all selection graphic elements.
        /// </summary>
        public static void ClearSelections()
        {
            IActiveView activeView = ArcMap.Document.ActiveView;

            for (int i = 0; i < Editor.SelectionRecord.Count; i++)
            {
                if (Editor.SelectionRecord[i].GraphicElement != null)
                {
                    activeView.GraphicsContainer.DeleteElement(Editor.SelectionRecord[i].GraphicElement);
                    Editor.SelectionRecord[i].GraphicElement = null;
                }
            }

            activeView.Refresh();
        }

        /// <summary>
        /// Draw box for all edited sites.
        /// </summary>
        public static void DrawEditionBox()
        {
            for (int i = 0; i < Editor.EditRecord.Count; i++)
            {
                Display.DrawEditionBox(Editor.EditRecord[i].Position);
            }
        }

        /// <summary>
        /// Draw a box that represents the edition site.
        /// Note: This method assumes that the responding cell class has been added into the edition record.
        /// </summary>
        /// <param name="position"></param>
        public static void DrawEditionBox(Position position)
        {
            if (Editor.ActiveLayer != null)
            {
                if (Editor.EditRecord[position].GraphicElement != null)
                    return;

                IElement element;
                if (Config.CustormEditColor)
                {
                    MessageBox.Show((Config.CustormEditColor == true).ToString());
                    ISimpleFillSymbol editSymbol = new SimpleFillSymbolClass();
                    double newValue = Editor.EditRecord[position].NewValue;
                    editSymbol.Color = RasterRender.GetRenderColor((IRasterLayer)Editor.ActiveLayer, newValue);
                    editSymbol.Outline = Config.EditSymbol.Outline;
                    editSymbol.Style = Config.EditSymbol.Style;
                    element = DrawBoxElement(editSymbol, position);   
                }
                else
                {
                    element = DrawBoxElement(Config.EditSymbol, position);
                }

                Editor.EditRecord[position].GraphicElement = element;
            }
        }

        /// <summary>
        /// Remove the edition box at the specified position.
        /// </summary>
        /// <param name="position"></param>
        public static void RemoveEditionBox(Position position)
        {
            RemoveEditionBox(position.Column, position.Row);
        }

        /// <summary>
        /// Remove the edition box at the specified position.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public static void RemoveEditionBox(int column, int row)
        {
            Pixel cell = Editor.EditRecord[column, row];

            if (cell != null && cell.GraphicElement != null)
            {
                ArcMap.Document.ActivatedView.GraphicsContainer.DeleteElement(cell.GraphicElement);
                ArcMap.Document.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                cell.GraphicElement = null;
            }     
        }

        /// <summary>
        /// Clear all edition graphic elements.
        /// </summary>
        public static void ClearEdits()
        {
            IActiveView activeView = ArcMap.Document.ActiveView;

            for (int i = 0; i < Editor.EditRecord.Count; i++)
            {
                if (Editor.EditRecord[i].GraphicElement != null)
                {
                    activeView.GraphicsContainer.DeleteElement(Editor.EditRecord[i].GraphicElement);
                    Editor.EditRecord[i].GraphicElement = null;
                }
            }

            activeView.Refresh();
        }

        /// <summary>
        /// Clear all graphic elements.
        /// </summary>
        public static void Clear()
        {
            Display.ClearEdits();
            Display.ClearSelections();
        }

        /// <summary>
        /// Flash the selected pixel.
        /// </summary>
        /// <param name="pixelPos">Position of the selected pixel</param>
        /// <param name="IntervalOfFlashing">Interval of Flashing (ms)</param>
        /// <param name="activeLayer">Raster layer to be symbolized.</param>
        public static void FlashSelection(Position pixelPos, int IntervalOfFlashing = 500, ILayer activeLayer = null)
        {
            ArcMap.Document.ActiveView.Refresh();

            // Probably should be removed.
            ILayer layer = activeLayer == null ? Editor.ActiveLayer : activeLayer;

            if (layer != null)
            {
                IDisplay display = (IDisplay)ArcMap.Document.ActiveView.ScreenDisplay;
                display.StartDrawing(display.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache);

                ISymbol symbol = (ISymbol)Config.SelectionSmbol;
                symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

                // Retrive the cell coordinate and the cell size
                double x, y;
                IRasterLayer rasterLayer = (IRasterLayer)layer;
                IRaster2 raster = (IRaster2)rasterLayer.Raster;
                raster.PixelToMap(pixelPos.Column, pixelPos.Row, out x, out y);
                IRasterProps rasterProp = (IRasterProps)raster;
                IPnt cellSize = rasterProp.MeanCellSize();

                // Define the extent of the selection box
                IEnvelope envelop = new EnvelopeClass();
                envelop.XMin = x - cellSize.X / 2;
                envelop.XMax = x + cellSize.X / 2;
                envelop.YMin = y - cellSize.Y / 2;
                envelop.YMax = y + cellSize.Y / 2;

                display.SetSymbol(symbol);
                display.DrawPolygon((IGeometry)envelop);
                Thread.Sleep(IntervalOfFlashing);
                display.DrawPolygon((IGeometry)envelop);
                display.FinishDrawing();    
            }
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Drawing the graphic element of the pixel box.
        /// </summary>
        /// <param name="boxSymbol">The graphic symbol of the drawing box.</param>
        /// <param name="pixelPos">The postion of drawing cell.</param>
        /// <param name="activeLayer">Raster layer to be symbolized.</param>
        /// <returns>An IFillShapeElement implimentation as IElement</returns>
        private static IElement DrawBoxElement(ISimpleFillSymbol boxSymbol, Position pixelPos, ILayer activeLayer = null)
        {
            // Probably should be removed.
            ILayer layer = activeLayer == null ? Editor.ActiveLayer : activeLayer;

            IActiveView activeView = ArcMap.Document.ActiveView;

            IFillShapeElement fillShapeElement = new RectangleElementClass();
            fillShapeElement.Symbol = (IFillSymbol)boxSymbol;

            // Retrive the cell coordinate and the cell size
            double x, y;
            IRasterLayer rasterLayer = (IRasterLayer)layer;
            IRaster2 raster = (IRaster2)rasterLayer.Raster;
            raster.PixelToMap(pixelPos.Column, pixelPos.Row, out x, out y);
            IRasterProps rasterProp = (IRasterProps)raster;
            IPnt cellSize = rasterProp.MeanCellSize();

            // Define the extent of the selection box
            IEnvelope envelop = new EnvelopeClass();
            envelop.XMin = x - cellSize.X / 2;
            envelop.XMax = x + cellSize.X / 2;
            envelop.YMin = y - cellSize.Y / 2;
            envelop.YMax = y + cellSize.Y / 2;

            IElement element = (IElement)fillShapeElement;
            element.Geometry = (IGeometry)envelop;

            activeView.GraphicsContainer.AddElement(element, 0);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            return element;
        }

        #endregion

        #region Computer Graphic Portion (Need to be merged)

        /// <summary>
        /// Gets pixels on given line using Digital Differential Analyzer(DDA).
        /// </summary>
        /// <param name="startPos">Starting position</param>
        /// <param name="endPos">Ending position</param>
        /// <returns></returns>
        static private Position[] DDALine(Position startPos, Position endPos)
        {
            int x0 = startPos.Column;
            int y0 = -startPos.Row;
            int x1 = endPos.Column;
            int y1 = -endPos.Row;

            int dx = x1 - x0;
            int dy = y1 - y0;
            double x = x0;
            double y = y0;

            int steps = 0;
            if (System.Math.Abs(dx) > System.Math.Abs(dy))
            {
                steps = (int)System.Math.Abs(dx);
            }
            else
            {
                steps = (int)System.Math.Abs(dy);
            }
            Position[] line = new Position[steps + 1];
            line[0] = new Position(x0, y0);

            double xIncrement = dx / (double)steps;
            double yincrement = dy / (double)steps;
            for (int k = 1; k <= steps; k++)
            {
                x += xIncrement;
                y += yincrement;
                line[k] = new Position((int)System.Math.Round(x), (int)System.Math.Round(y));
            }

            return line;
        }

        /// <summary>
        /// Gets pixels on the boundary of given polygon using Digital Differential Analyzer(DDA).
        /// </summary>
        /// <param name="vertex">Vertexss of polygons</param>
        /// <returns></returns>
        static private Position[] DDAPolygon(Position[] vertexes)
        {
            List<Position> lines = new List<Position>();

            // It is not sure that whether the final vertex will be the first one. I assume it is.
            for (int i = 0; i < vertexes.Length - 1; i++)
            {
                Position[] line = DDALine(vertexes[i], vertexes[i + 1]);
                lines.AddRange(line);
            }

            return lines.ToArray();
        }

        #endregion
    }
}
