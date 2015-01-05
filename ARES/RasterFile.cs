using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARES
{
    /// <summary>
    /// Provides access to members for raster file operations.
    /// </summary>
    public static class RasterFile
    {
        #region Attributes

        private static Position origin = new Position(0, 0);

        #endregion

        #region Properties

        /// <summary>
        /// Get the default origin of raster.
        /// </summary>
        public static Position Origin
        {
            get { return origin; }
        }

        #endregion

        #region Public Methods

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

        #endregion
    }
}
