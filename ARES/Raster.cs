using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Desktop.AddIns;

namespace ARES
{
    /// <summary>
    /// Provides access to functions that manipulate raster file.
    /// </summary>
    public class Raster
    {
        /// <summary>
        /// Convert screen coordinates to map coordinates.
        /// </summary>         
        /// <param name="x">X-coordinate of the position on screen</param>
        /// <param name="y">Y-coordinate of the position on screen</param>
        /// <returns></returns>
        public static IPoint ScreenCoor2MapCoor(int x, int y)
        {
            IActiveView activeView = (IActiveView)ArcMap.Document.FocusMap;
            return activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
        }

        /// <summary>
        /// Covert map coordinates to raster coordinates.
        /// </summary>
        /// <param name="mapCoors"></param>
        /// <param name="rasterLayer">Reference raster layer.</param>
        /// <returns></returns>
        public static Position MapCoor2RasterCoor(IPoint mapCoors, IRasterLayer rasterLayer)
        {
            if (rasterLayer != null)
            {
                IRaster2 raster = (IRaster2)rasterLayer.Raster;

                int row, col;
                raster.MapToPixel(mapCoors.X, mapCoors.Y, out col, out row);
                return new Position(col, row);
            }

            return null;
        }

        /// <summary>
        /// Convert screen coordinats to raster coordinates.
        /// </summary>
        /// <param name="x">X-coordinate of the position on screen</param>
        /// <param name="y">Y-coordinate of the position on screen</param>
        /// <param name="rasterLayer">Reference raster layer</param>
        /// <returns></returns>
        public static Position ScreenCoor2RasterCoor(int x, int y, IRasterLayer rasterLayer)
        {
            if (rasterLayer != null)
            {
                IPoint mapCoor = ScreenCoor2MapCoor(x, y);
                return MapCoor2RasterCoor(mapCoor, rasterLayer);
            }

            return null;
        }

        /// <summary>
        /// Save the edition as a specified file.
        /// </summary>
        /// <param name="fileName">New file path of raster layer to be saved.</param>
        /// <param name="activeLayer">Raster layer to be saved.</param>
        /// <param name="edits">Pixel collection that contains edited pixels.</param>
        public static void SaveEditsAs(string fileName, IRasterLayer activeLayer, PixelCollection edits)
        {
            if (activeLayer == null || edits.Count == 0)
                return;

            //Random rnd = new Random();
            //string tempFile = rnd.Next().ToString();

            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            // Get the original file
            // IRasterLayer rasterLayer = activeLayer;
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            //IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(rasterLayer.FilePath), 0);
            //IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(System.IO.Path.GetFileName(rasterLayer.FilePath));

            // Open the new file save location
            IWorkspace mWorkspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);

            // Save the original file to a new file
            ISaveAs saveAs = (ISaveAs)activeLayer.Raster;
            IRasterDataset2 mRasterDataset = (IRasterDataset2)saveAs.SaveAs(System.IO.Path.GetFileName(fileName),
                                                                            mWorkspace,
                                                                            Raster.GetFormat(System.IO.Path.GetExtension(fileName)));
            IRaster mRaster = mRasterDataset.CreateFullRaster();

            // Save edits to file
            Raster.WriteEdits(mRaster, edits);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(mRaster);
        }

        /// <summary>
        /// Save edits to the original file.
        /// </summary>
        /// <param name="activeLayer">Raster layer to be saved.</param>
        /// <param name="edits">Pixel collection that contains edited pixels.</param>
        public static void SaveEdits(IRasterLayer activeLayer, PixelCollection edits)
        {
            if (activeLayer == null || edits.Count == 0)
                return;

            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            // Get the original file
            //IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            //IRasterWorkspace rasterWorkspace = (IRasterWorkspace)workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(activeLayer.FilePath), 0);
            //IRasterDataset2 mRasterDataset = (IRasterDataset2)rasterWorkspace.OpenRasterDataset(System.IO.Path.GetFileName(activeLayer.FilePath));
            //IRaster mRaster = mRasterDataset.CreateFullRaster();

            Raster.WriteEdits(activeLayer.Raster, edits);

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(mRaster);

            ArcMap.Document.ActiveView.Refresh();
        }

        /// <summary>
        /// Save the extent change into a new raster file.
        /// </summary>
        /// <param name="rasterLayer">Raster layer</param>
        /// <param name="fileName">Path of the output file</param>
        /// <param name="xmin">Minimum X coordinate</param>
        /// <param name="ymin">Minimum Y coordinate</param>                                                                          
        /// <param name="pixelSize">Pixel size</param>
        public static void SaveExtentAs(IRasterLayer rasterLayer, string fileName, double xmin, double ymin, double pixelSize)
        {
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;
            IEnvelope extent = new EnvelopeClass();
            extent.PutCoords(xmin, ymin, xmin + rasterProps.Width * pixelSize, ymin + rasterProps.Height * pixelSize);
            extent.Project(rasterProps.Extent.SpatialReference);
            rasterProps.Extent = extent;

            ISaveAs saveAs = (ISaveAs)rasterLayer.Raster;
            IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
            IWorkspace mWorkspace = workspaceFactory.OpenFromFile(System.IO.Path.GetDirectoryName(fileName), 0);
            saveAs.SaveAs(System.IO.Path.GetFileName(fileName),
                          mWorkspace,
                          Raster.GetFormat(System.IO.Path.GetExtension(fileName)));
        }

        /// <summary>
        /// Gets value of pixel at the specified position.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rasterLayer"></param>
        /// <returns></returns>
        public static double GetValue(Position pos, IRasterLayer rasterLayer)
        {
            return GetValue(pos, rasterLayer.Raster);
        }

        /// <summary>
        /// Gets value of pixel at the specified position.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="raster"></param>
        /// <returns></returns>
        public static double GetValue(Position pos, IRaster raster)
        {
            IPnt regionSize = new PntClass();
            regionSize.SetCoords(1, 1);
            IPixelBlock pixelBlock = raster.CreatePixelBlock(regionSize);
            IPnt tl = new PntClass();
            tl.SetCoords(pos.Column, pos.Row);
            raster.Read(tl, pixelBlock);

            return Convert.ToDouble(pixelBlock.GetVal(0, 0, 0));
        }

        /// <summary>
        /// Get the pixel values in a region of the input raster.
        /// </summary>
        /// <param name="tlCorner"></param>
        /// <param name="brCorner"></param>
        /// <param name="raster"></param>
        /// <returns></returns>
        public static double[,] GetValues(Position tlCorner, Position brCorner, IRaster raster)
        {
            int colCount = brCorner.Column - tlCorner.Column + 1;
            int rowCount = brCorner.Row - tlCorner.Row + 1;

            IPnt regionSize = new PntClass();
            regionSize.SetCoords(colCount, rowCount);
            IPixelBlock pixelBlock = raster.CreatePixelBlock(regionSize);
            IPnt tl = new PntClass();
            tl.SetCoords(tlCorner.Column, tlCorner.Row);
            raster.Read(tl, pixelBlock);

            double[,] values = new double[colCount, rowCount];
            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    values[x, y] = Convert.ToDouble(pixelBlock.GetVal(0, x, y));
                }
            }
            return values;
        }

        /// <summary>
        /// Convert the csharp value to the ArcObject pixel value.
        /// </summary>
        /// <param name="csharpValue">Cshapr value</param>
        /// <param name="pixelValueType">The pixel type of ouput value</param>
        /// <param name="pixelValue">Output pixel value</param>
        /// <returns>A value indicating whether the convention is successful</returns>
        public static bool CSharpValue2PixelValue(object csharpValue, rstPixelType pixelValueType, out object pixelValue)
        {
            try
            {
                switch (pixelValueType)
                {
                    case rstPixelType.PT_UCHAR:
                        pixelValue = (object)Convert.ToByte(csharpValue);
                        return true;
                    case rstPixelType.PT_CHAR:
                        pixelValue = (object)Convert.ToSByte(csharpValue);
                        return true;
                    case rstPixelType.PT_SHORT:
                        pixelValue = (object)Convert.ToInt16(csharpValue);
                        return true;
                    case rstPixelType.PT_USHORT:
                        pixelValue = (object)Convert.ToUInt16(csharpValue);
                        return true;
                    case rstPixelType.PT_LONG:
                        pixelValue = (object)Convert.ToInt32(csharpValue);
                        return true;
                    case rstPixelType.PT_ULONG:
                        pixelValue = (object)Convert.ToUInt32(csharpValue);
                        return true;
                    case rstPixelType.PT_FLOAT:
                        pixelValue = (object)Convert.ToSingle(csharpValue);
                        return true;
                    case rstPixelType.PT_DOUBLE:
                        pixelValue = (object)Convert.ToDouble(csharpValue);
                        return true;
                    default:
                        pixelValue = null;
                        return false;
                }
            }
            catch (Exception)
            {
                pixelValue = null;
                return false;
            }
        }

        /// <summary>
        /// Gets the extension of the given ESRI format string.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetExtension(string format)
        {
            switch (format.ToUpper())
            {
                case "TIFF":
                    return ".tif";
                case "GRID":
                    return "";
                case "IMAGINE Image":
                    return ".img";
                case "JPG":
                    return ".jpg";
                case "JP2":
                    return ".jp2";
                case "BMP":
                    return ".bmp";
                case "GIF":
                    return ".gif";
                case "PNG":
                    return ".png";
                case "PIX":
                    return ".pix";
                case "BIL":
                    return ".bil";
                case "BIP":
                    return ".bip";
                case "BSQ":
                    return ".bsq";
                case "ENVI":
                    return ".img";
            }
            return "";
        }

        /// <summary>
        /// Gets the ESRI format string of the given extension.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case "tif":
                    return "TIFF";
                case "":
                    return "GRID";
                case "img":
                    return "IMAGINE Image";
                case "jpg":
                    return "JPG";
                case "jp2":
                    return "JP2";
                case "bmp":
                    return "BMP";
                case "gif":
                    return "GIF";
                case "png":
                    return "PNG";
                case "pix":
                    return "PIX";
                case "bip":
                    return "BIP";
                case "bsq":
                    return "BSQ";
                case "bil":
                    return "BIL";
                default:
                    return "TIFF";
            }
        }

        #region Private Methods

        /// <summary>
        /// Write edits to the input raster.
        /// </summary>
        /// <param name="raster">Raster of raster layer to be edited.</param>
        /// <param name="edits">Pixel collection that contains edited pixels.</param>
        private static void WriteEdits(IRaster raster, PixelCollection edits)
        {
            IRasterProps rasterProps = (IRasterProps)raster;

            int minRow = rasterProps.Height - 1;
            int maxRow = 0;
            int minCol = rasterProps.Width - 1;
            int maxCol = 0;

            for (int i = 0; i < edits.Count; i++)
            {
                #region Get the extent of the edition region

                Position cellPos = edits[i].Position;

                if (cellPos.Row > maxRow)
                {
                    maxRow = cellPos.Row;
                }

                if (cellPos.Row < minRow)
                {
                    minRow = cellPos.Row;
                }

                if (cellPos.Column > maxCol)
                {
                    maxCol = cellPos.Column;
                }

                if (cellPos.Column < minCol)
                {
                    minCol = cellPos.Column;
                }

                #endregion
            }

            IPnt pos = new PntClass();
            pos.SetCoords(maxCol - minCol + 1, maxRow - minRow + 1);
            IPixelBlock pixelBlock = raster.CreatePixelBlock(pos);
            pos.SetCoords(minCol, minRow);
            raster.Read(pos, pixelBlock);

            // Set new values
            IPixelBlock3 pixelBlock3 = (IPixelBlock3)pixelBlock;
            Array pixels = (Array)pixelBlock3.get_PixelData(0);

            for (int i = 0; i < edits.Count; i++)
            {
                object value = null;
                Raster.CSharpValue2PixelValue(edits[i].NewValue, rasterProps.PixelType, out value);

                pixels.SetValue(value,
                                edits[i].Position.Column - minCol,
                                edits[i].Position.Row - minRow);
            }

            pixelBlock3.set_PixelData(0, (System.Object)pixels);
            IRasterEdit rasterEdit = (IRasterEdit)raster;
            rasterEdit.Write(pos, (IPixelBlock)pixelBlock3);
        }

        #endregion
    }
}
