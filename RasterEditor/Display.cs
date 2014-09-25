using System;
using System.Windows.Forms;
using System.Threading;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

using RasterEditor.Raster;

namespace RasterEditor
{
    public static class Display
    {
        #region Public Methods

        /// <summary>
        /// Dras all selection boxes.
        /// </summary>
        public static void DrawSelectionBox()
        {
            for (int i = 0; i < Editor.SelectionRecord.Count; i++)
            {
                Display.DrawSelectionBox(Editor.SelectionRecord[i].Position);
            }
        }

        /// <summary>
        /// Draw a selection box at the selected position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="refresh">A value indicating whether the screen will be refreshed immediately.</param>
        public static void DrawSelectionBox(Position position, bool refresh = false)
        {
            if (Editor.ActiveLayer != null)
            {
                Pixel cell = Editor.SelectionRecord[position];
                if (cell != null)
                    return;

                IElement element = DrawBoxElement(Config.SelectionSmbol, position);

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
        /// Draw a selection region specified by the given top-left corner and bottom-right corner.
        /// </summary>
        /// <param name="selectedRegion"></param>
        public static void DrawSelectionBox(Position tlCorner, Position brCorner)
        {
            if (Editor.ActiveLayer != null)
            {
                IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;
                IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;
                brCorner.Adjust(Raster.RasterFile.Origin.Column, Raster.RasterFile.Origin.Row, 
                                rasterProp.Width - 1, rasterProp.Height - 1);

                for (int col = tlCorner.Column; col <= brCorner.Column; col++)
                {
                    for (int row = tlCorner.Row; row <= brCorner.Row; row++)
                    {
                        DrawSelectionBox(new Position(col, row));
                    }
                }
            }
        }

        /// <summary>
        /// Remove selection box at the input position.
        /// </summary>
        /// <param name="pos"></param>
        public static void RemoveSelectionBox(Position pos)
        {
            if (Editor.ActiveLayer == null)
            {
                return;
            }

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
        public static void ClearSelection()
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
                    ISimpleFillSymbol editSymbol = new SimpleFillSymbolClass();
                    double newValue = Editor.EditRecord[position].NewValue;
                    editSymbol.Color = Render.GetRenderColor((IRasterLayer)Editor.ActiveLayer, newValue);
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
            Display.ClearSelection();
        }

        /// <summary>
        /// Flash the selected pixel.
        /// </summary>
        /// <param name="pixelPos">Position of the selected pixel</param>
        /// <param name="IntervalOfFlashing">Interval of Flashing (ms)</param>
        public static void FlashBox(Position pixelPos, int IntervalOfFlashing = 500)
        {
            ArcMap.Document.ActiveView.Refresh();

            IDisplay display = (IDisplay)ArcMap.Document.ActiveView.ScreenDisplay;
            display.StartDrawing(display.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache);

            ISymbol symbol = (ISymbol)Config.SelectionSmbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            // Retrive the cell coordinate and the cell size
            double x, y;
            IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;
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

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Drawing the graphic element of the pixel box.
        /// </summary>
        /// <param name="boxSymbol">The graphic symbol of the drawing box.</param>
        /// <param name="pixelPos">The postion of drawing cell.</param>
        /// <returns>An IFillShapeElement implimentation as IElement</returns>
        private static IElement DrawBoxElement(ISimpleFillSymbol boxSymbol, Position pixelPos)
        {
            IActiveView activeView = ArcMap.Document.ActiveView;

            IFillShapeElement fillShapeElement = new RectangleElementClass();
            fillShapeElement.Symbol = (IFillSymbol)boxSymbol;

            // Retrive the cell coordinate and the cell size
            double x, y;
            IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;
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
    }
}
