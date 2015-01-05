using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;

namespace ARES.Forms
{
    /// <summary>
    /// Prompts the user to open or save a raster file.
    /// </summary>      
    class RasterFileDialog
    {
        #region Attributes

        private IGxDialog dialog = null;
        private FileDialogType type = FileDialogType.Open;
        private string fileName = "";
        private string[] fileNames = null;

        #endregion

        #region Properties

        /// <summary>
        /// Sets the initial directory displayed by the file dialog box.
        /// </summary>
        public string InitialDirectory
        {
            set
            {
                if (System.IO.Directory.Exists(value))
                {
                    object path = (object)value;
                    dialog.set_StartingLocation(ref path);
                }
            }
        }

        /// <summary>
        /// Gets the type of the file dialog boxss.
        /// </summary>
        public FileDialogType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Sets the file dialog box title.
        /// </summary>
        public string Title
        {
            set { dialog.Title = value; }
        }

        /// <summary>
        /// Sets the caption of button on the file dailog box.
        /// </summary>
        public string ButtonCaption
        {
            set { dialog.ButtonCaption = value; }
        }

        /// <summary>
        /// Sets a value indicating whether the dialog box allows multiple files to be selected.
        /// </summary>
        public bool Multiselect
        {
            set { dialog.AllowMultiSelect = value; }
        }

        /// <summary>          
        /// Sets a value indicating whether the dialog box restores the current directory before closing.
        /// </summary>
        public bool RestoreDirectory
        {
            set { dialog.RememberLocation = value; }
        }

        /// <summary>
        /// Gets a string containing the file name selected in the file dialog box.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Gets the file names of all selected files in the dialog box.
        /// </summary>
        public string[] FileNames
        {
            get { return fileNames; }
        }

        /// <summary>
        /// Sets an initial file name for the file in the save file dialog box.
        /// </summary>
        public string InitialFileName
        {
            set { dialog.Name = value; }
        }

        #endregion

        #region Construction Method

        /// <summary>
        /// Initialize the OpenRasterFileDialog.
        /// </summary>
        /// <param name="type">Type of the file dialog.</param>
        public RasterFileDialog(FileDialogType type)
        {
            dialog = new GxDialogClass();
            IGxObjectFilterCollection filterCollection = (IGxObjectFilterCollection)dialog;

            IGxObjectFilter objectFilter = new RasterFormatTifFilter();
            filterCollection.AddFilter(objectFilter, true);
            objectFilter = new RasterFormatBMPFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatENVIFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatBILFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatBIPFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatBSQFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatGIFFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatGridFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatImgFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatJP2Filter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatJPGFilter();
            filterCollection.AddFilter(objectFilter, false);
            objectFilter = new RasterFormatPNGFilter();
            filterCollection.AddFilter(objectFilter, false);

            this.type = type;
            switch (type)
            {
                case FileDialogType.Save:
                    this.Title = "Save Raster Layer as";
                    break;
                case FileDialogType.Open:
                    this.Title = "Open Raster Layer";
                    break;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Runs a raster file dialog box with the specified owner.
        /// </summary>
        /// <returns></returns>
        public DialogResult ShowDialog()
        {
            fileName = "";
            fileNames = null;

            switch (this.type)
            {
                case FileDialogType.Open:
                    IEnumGxObject enumGxObject = new GxObjectArrayClass();
                    if (dialog.DoModalOpen(0, out enumGxObject))
                    {
                        IGxObjectArray gxObjectArray = (IGxObjectArray)enumGxObject;
                        fileName = gxObjectArray.Item(0).FullName;
                        fileNames = new string[gxObjectArray.Count];
                        for (int i = 0; i < gxObjectArray.Count; i++)
                        {
                            fileNames[i] = gxObjectArray.Item(i).FullName;
                        }

                        return DialogResult.OK;
                    }
                    break;
                case FileDialogType.Save:
                    if (dialog.DoModalSave(0))
                    {
                        fileName = dialog.Name;
                        string extension = GetExtension(dialog.ObjectFilter.Name);
                        if ((Path.GetExtension(fileName) != "") &&
                            (Path.GetExtension(fileName).ToLower() == extension.Substring(1)))
                        {
                            fileName = Path.GetFileNameWithoutExtension(fileName);
                        }

                        fileName = dialog.FinalLocation.FullName + "\\" + fileName + extension;
                        return DialogResult.OK;
                    }
                    break;
            }

            return DialogResult.Cancel;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the extension of the given format.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private string GetExtension(string format)
        {
            switch (format)
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

        #endregion
    }
}
