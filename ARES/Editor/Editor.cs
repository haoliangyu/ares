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

using ARES.Forms;

namespace ARES.Editor
{
    /// <summary>
    /// Provide access to properties and functions.
    /// </summary>
    public static class Editor
    {
        #region Attributes

        private static ILayer activeLayer = null;

        private static bool isEditing = false;

        private static bool showEdits = true;

        private static PixelCollection edits = new PixelCollection();

        private static PixelCollection selections = new PixelCollection();
                 
        #endregion

        #region Properties

        /// <summary>
        /// Get or set the active layer.
        /// </summary>
        public static ILayer ActiveLayer
        {
            set { activeLayer = value; }
            get { return activeLayer; }
        }

        /// <summary>
        /// Get or set a value indicating whether the active layer is under editing
        /// </summary>
        public static bool IsEditing
        {
            set { isEditing = value; }
            get { return isEditing; }
        }

        /// <summary>
        /// Get or set a value indicating whether edits are shown.
        /// </summary>
        public static bool ShowEdits
        {
            set { showEdits = value; }
            get { return showEdits; }
        }

        /// <summary>
        /// Get the cell collection of edits.
        /// </summary>
        public static PixelCollection Edits
        {
            get { return edits; }
        }

        /// <summary>
        /// Get the cell collection of selections
        /// </summary>
        public static PixelCollection Selections
        {
            get { return selections; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepare the editor for editing.
        /// </summary>
        public static void StartEditing()
        {
            // Select a layer to edit first
            int rasterLayerCount = ArcMapApp.RasterLayerCount;
            if (rasterLayerCount == 1)
            {
                Editor.activeLayer = ArcMapApp.GetRasterLayer();
            }
            else
            {
                SelectLayerForm selectLayerForm = new SelectLayerForm();
                selectLayerForm.ShowDialog();

                if (selectLayerForm.ReturnLayer == null)
                {
                    return;
                }

                Editor.activeLayer = selectLayerForm.ReturnLayer;
            }

            Editor.IsEditing = true;
            Editor.Edits.Clear();

            // Enable the save button
            SaveEditsButton saveButton = AddIn.FromID<SaveEditsButton>(ThisAddIn.IDs.ARES_Editor_SaveEditsButton);
            saveButton.IsEnabled = true;

            // Enable the save as button
            SaveEditsAsButton saveAsButton = AddIn.FromID<SaveEditsAsButton>(ThisAddIn.IDs.ARES_Editor_SaveEditsAsButton);
            saveAsButton.IsEnabled = true;
            
            // Enable the stop button
            StopEditingButton stopButton = AddIn.FromID<StopEditingButton>(ThisAddIn.IDs.ARES_Editor_StopEditingButton);
            stopButton.IsEnabled = true;

            // Enable the edit tool.
            EditTool selectTool = AddIn.FromID<EditTool>(ThisAddIn.IDs.ARES_Editor_EditTool);
            selectTool.IsEnabled = true;
            
            // Disable the start button
            StartEditingButton startEditingButton = AddIn.FromID<StartEditingButton>(ThisAddIn.IDs.ARES_Editor_StartEditingButton);
            startEditingButton.IsEnabled = false;

            // Enable the ShowEditsButton
            ShowEditsButton showEditsButton = AddIn.FromID<ShowEditsButton>(ThisAddIn.IDs.ARES_Editor_ShowEditsButton);
            showEditsButton.IsEnabled = true;

        }

        /// <summary>
        /// Stop editing.
        /// </summary>
        public static void StopEditing()
        {
            Editor.activeLayer = null;
            Editor.isEditing = false;
            Display.ClearElement(Editor.Edits.GetAllGraphicElements());
            Editor.Edits.Clear();
            Display.ClearElement(Editor.Selections.GetAllGraphicElements());
            Editor.Selections.Clear();

            if (ArcMap.Application.CurrentTool.Caption == "Select")
            {
                ArcMap.Application.CurrentTool = null;
            }

            StopEditingButton stopEditingButton = AddIn.FromID<StopEditingButton>(ThisAddIn.IDs.ARES_Editor_StopEditingButton);
            stopEditingButton.IsEnabled = false;

            StartEditingButton startEditionButton = AddIn.FromID<StartEditingButton>(ThisAddIn.IDs.ARES_Editor_StartEditingButton);
            startEditionButton.IsEnabled = true;

            SaveEditsButton saveEditsButton = AddIn.FromID<SaveEditsButton>(ThisAddIn.IDs.ARES_Editor_SaveEditsButton);
            saveEditsButton.IsEnabled = false;

            SaveEditsAsButton saveEditsAsButton = AddIn.FromID<SaveEditsAsButton>(ThisAddIn.IDs.ARES_Editor_SaveEditsAsButton);
            saveEditsAsButton.IsEnabled = false;

            EditTool selectTool = AddIn.FromID<EditTool>(ThisAddIn.IDs.ARES_Editor_EditTool);
            selectTool.IsEnabled = false;

            ShowEditsButton showEditsButton = AddIn.FromID<ShowEditsButton>(ThisAddIn.IDs.ARES_Editor_ShowEditsButton);
            showEditsButton.IsEnabled = false;
        }

        /// <summary>
        /// Save the edition as a specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static void SaveEditsAs(string fileName)
        {
            Raster.SaveEditsAs(fileName, (IRasterLayer)ActiveLayer, Edits);
        }

        /// <summary>
        /// Save modification to the original file.
        /// </summary>
        public static void SaveEdits()
        {
            Raster.SaveEdits((IRasterLayer)ActiveLayer, Edits);
        }

        /// <summary>
        /// Gets the drawing symbol of selected pixel. (temporary function)
        /// </summary>
        /// <returns></returns>
        public static ISimpleFillSymbol GetSelectionSymbol()
        {
            ISimpleFillSymbol selectionSymbol = new SimpleFillSymbolClass();
            selectionSymbol.Color = new RgbColorClass() { NullColor = true, Transparency = 0 };
            ISimpleLineSymbol selectionOutlineSymbol = new SimpleLineSymbolClass();
            selectionOutlineSymbol.Color = new RgbColorClass() { Red = 0, Green = 255, Blue = 255};
            selectionOutlineSymbol.Width = 2;
            selectionSymbol.Outline = selectionOutlineSymbol;

            return selectionSymbol;
        }

        /// <summary>
        /// Gets the drawing symbol of edited pixel. (temporary function)
        /// </summary>
        /// <returns></returns>
        public static ISimpleFillSymbol GetEidtSymbol()
        {
            ISimpleFillSymbol editSymbol = new SimpleFillSymbolClass();
            editSymbol.Color = new RgbColorClass() { Red = 255, Green = 255, Blue = 90, Transparency = 127 };
            ISimpleLineSymbol editOutlineSymbol = new SimpleLineSymbolClass();
            editOutlineSymbol.Color = new RgbColorClass() { Red = 255, Green = 255, Blue = 0 };
            editOutlineSymbol.Width = 2;
            editSymbol.Style = esriSimpleFillStyle.esriSFSBackwardDiagonal;
            editSymbol.Outline = editOutlineSymbol;

            return editSymbol;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Write edits to the input raster.
        /// </summary>
        /// <param name="raster"></param>
        private static void WriteEdits(IRaster raster)
        {
            IRasterProps rasterProps = (IRasterProps)raster;

            int minRow = rasterProps.Height - 1;
            int maxRow = 0;
            int minCol = rasterProps.Width - 1;
            int maxCol = 0;

            for (int i = 0; i < Editor.Edits.Count; i++)
            {
                #region Get the extent of the edition region

                Position cellPos = Editor.Edits[i].Position;

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

            for (int i = 0; i < Editor.Edits.Count; i++)
            {
                object value = null;
                Raster.CSharpValue2PixelValue(Editor.Edits[i].NewValue, rasterProps.PixelType, out value);

                pixels.SetValue(value,
                                Editor.Edits[i].Position.Column - minCol,
                                Editor.Edits[i].Position.Row - minRow);
            }

            pixelBlock3.set_PixelData(0, (System.Object)pixels);
            IRasterEdit rasterEdit = (IRasterEdit)raster;
            rasterEdit.Write(pos, (IPixelBlock)pixelBlock3);    
        }

        #endregion
    }
}
