using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;

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
        /// Refresh the ArcGIS Graphic Symbols.
        /// </summary>
        public static void Refresh()
        {
            ArcMap.Document.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

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
                    Refresh();
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
                Refresh();
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

            Refresh();
        }

        /// <summary>
        /// Covert a .Net Color class to an ArcObject IColor class.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static IColor Color2IColor(Color color)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.NullColor = color.IsEmpty;
            rgbColor.Red = color.R;
            rgbColor.Green = color.G;
            rgbColor.Blue = color.B;

            return (IColor)rgbColor;
        }

        /// <summary>
        /// Covert an ArcObject IColor class to a .Net Color class.
        /// </summary>
        /// <param name="icolor"></param>
        /// <returns></returns>
        public static Color IColor2Color(IColor icolor)
        {
            IRgbColor rgbColor = (IRgbColor)icolor;

            return Color.FromArgb(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
        }

        /// <summary>
        /// Indicates whether two Color classes represent the same color.
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public static bool Compare(Color color1, Color color2)
        {
            return (color1.R == color2.R) &&
                   (color1.G == color2.G) &&
                   (color1.B == color2.B) &&
                   (color1.IsEmpty == color2.IsEmpty);
        }

        /// <summary>
        /// Indicates whether two IColor classes represent the same color.
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public static bool Compare(IColor color1, IColor color2)
        {
            return (color1.RGB == color2.RGB) && (color1.NullColor == color2.NullColor);
        }

        /// <summary>
        /// Indicates whether a IColor class and a Color class represent the same color.
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public static bool Compare(IColor color1, Color color2)
        {
            IRgbColor rgbColor1 = (IRgbColor)color1;

            return (rgbColor1.Red == color2.R) &&
                   (rgbColor1.Green == color2.G) &&
                   (rgbColor1.Blue == color2.B) &&
                   (rgbColor1.NullColor == color2.IsEmpty);
        }

        /// <summary>
        /// Gets locations of pixels on a specified polyline.
        /// </summary>
        /// <param name="startPos">Starting position of the polyline.</param>
        /// <param name="endPos">Ending position of the polyline.</param>
        /// <param name="envelop">Envelop of editting raster.</param>
        /// <returns></returns>
        public static Position[] GetPolyline(Position startPos, Position endPos, Envelope envelop)
        {
            Position[] polyline;
            Position pStartPos = startPos;
            Position pEndPos = endPos;

            #region Horizontal Line

            if (pStartPos.Column == pEndPos.Column)
            { 
                if (pStartPos.Row > pEndPos.Row)
                {
                    Position temp = pStartPos;
                    pStartPos = pEndPos;
                    pEndPos = startPos;
                }

                pStartPos.Adjust(envelop);
                pEndPos.Adjust(envelop);

                polyline = new Position[pEndPos.Row - pStartPos.Row + 1];
                for (int i = pStartPos.Row; i <= pEndPos.Row; i++)
                {
                    polyline[i - pStartPos.Row] = new Position(pStartPos.Column, i);    
                }

                return polyline;
            }

            #endregion

            #region Vertical Line

            if (pStartPos.Row == pEndPos.Row)
            {
                if (pStartPos.Column > pEndPos.Column)
                {
                    Position temp = pStartPos;
                    pStartPos = pEndPos;
                    pEndPos = startPos;
                }

                pStartPos.Adjust(envelop);
                pEndPos.Adjust(envelop);

                polyline = new Position[pEndPos.Column - pStartPos.Column + 1];
                for (int i = pStartPos.Column; i <= pEndPos.Column; i++)
                {
                    polyline[i - pStartPos.Column] = new Position(pStartPos.Column, i);
                }

                return polyline;
            }

            #endregion

            return DDALine(pStartPos, pEndPos, envelop);
        }

        /// <summary>
        /// Get the position of pixels at line of the polygon.
        /// </summary>
        /// <param name="vertexes"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public static Position[] GetPolygon(Position[] vertexes, Envelope envelope)
        {
            List<Position> lines = new List<Position>();

            // It is not sure that whether the final vertex will be the first one. I assume it is.
            for (int i = 0; i < vertexes.Length - 1; i++)
            {
                Position[] line = GetPolyline(vertexes[i], vertexes[i + 1], envelope);
                lines.AddRange(line);
            }

            return lines.ToArray();
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

            return element;
        }

        /// <summary>
        /// Gets pixels on given line using Digital Differential Analyzer(DDA).
        /// </summary>
        /// <param name="startPos">Starting position</param>
        /// <param name="endPos">Ending position</param>
        /// <returns></returns>
        static private Position[] DDALine(Position startPos, Position endPos, Envelope envelope)
        {
            int x0 = startPos.Column;
            int y0 = startPos.Row;
            int x1 = endPos.Column;
            int y1 = endPos.Row;

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

            List<Position> line = new List<Position>();

            double xIncrement = dx / (double)steps;
            double yincrement = dy / (double)steps;
            for (int k = 0; k <= steps; k++)
            {
                if (x >= 0 && x <= envelope.MaxColumn && y >= 0 && y <= envelope.MaxRow)
                {
                    line.Add(new Position((int)System.Math.Round(x), (int)System.Math.Round(y)));
                }

                x += xIncrement;
                y += yincrement;
            }

            return line.ToArray();                                         
        }

        #endregion
    }
}
