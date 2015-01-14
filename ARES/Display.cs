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
        /// Draw box symbol at the given position.
        /// </summary>
        /// <param name="pos">Position of pixel</param>
        /// <param name="activeLayer">Raster layer to add symbol</param>
        /// <param name="refresh">An value indicating whether to refresh screen after adding symbol</param>
        /// <param name="symbol">Symbol style</param>
        public static IElement DrawBox(Position pos, ISimpleFillSymbol symbol, ILayer activeLayer, bool refresh = false)
        {
            if (activeLayer != null)
            {
                IElement element = DrawBoxElement(symbol, pos, activeLayer);

                if (refresh)
                {
                    ArcMap.Document.ActiveView.Refresh();
                }

                return element;
            }

            return null;
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

        /// <summary>
        /// Remvoe a graphic box symbol.                                                     
        /// </summary>
        /// <param name="graphicElement">Graphic element to be removed</param>
        /// <param name="refresh">An boolean value indicating whether to refresh screen after adding symbol</param>
        public static void RemoveElement(IElement graphicElement, bool refresh = false)
        {
            if (graphicElement != null)
            {
                ArcMap.Document.ActivatedView.GraphicsContainer.DeleteElement(graphicElement);
            }

            if (refresh)
            {
                ArcMap.Document.ActiveView.Refresh();
            }
        }

        /// <summary>
        /// Remvoe graphic box symbols.
        /// </summary>
        /// <param name="graphicElements">Graphic elements to be removed</param>
        public static void ClearElement(IElement[] graphicElements)
        {
            foreach (IElement element in graphicElements)
            {
                RemoveElement(element);
            }

            ArcMap.Document.ActiveView.Refresh();
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
