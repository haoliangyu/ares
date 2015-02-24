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

namespace ARES
{
    /// <summary>
    /// Provide access to global attributes and functions.
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
            SaveEditsButton saveButton = AddIn.FromID<SaveEditsButton>(ThisAddIn.IDs.SaveEditsButton);
            saveButton.IsEnabled = true;

            // Enable the save as button
            SaveEditsAsButton saveAsButton = AddIn.FromID<SaveEditsAsButton>(ThisAddIn.IDs.SaveEditsAsButton);
            saveAsButton.IsEnabled = true;

            // Enable the stop button
            StopEditingButton stopButton = AddIn.FromID<StopEditingButton>(ThisAddIn.IDs.StopEditingButton);
            stopButton.IsEnabled = true;

            // Enable the edit tool.
            EditTool selectTool = AddIn.FromID<EditTool>(ThisAddIn.IDs.EditTool);
            selectTool.IsEnabled = true;

            // Disable the start button
            StartEditingButton startEditingButton = AddIn.FromID<StartEditingButton>(ThisAddIn.IDs.StartEditingButton);
            startEditingButton.IsEnabled = false;

            // Enable the ShowEditsButton
            ShowEditsButton showEditsButton = AddIn.FromID<ShowEditsButton>(ThisAddIn.IDs.ShowEditsButton);
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

            StopEditingButton stopEditingButton = AddIn.FromID<StopEditingButton>(ThisAddIn.IDs.StopEditingButton);
            stopEditingButton.IsEnabled = false;

            StartEditingButton startEditionButton = AddIn.FromID<StartEditingButton>(ThisAddIn.IDs.StartEditingButton);
            startEditionButton.IsEnabled = true;
            
            SaveEditsButton saveEditsButton = AddIn.FromID<SaveEditsButton>(ThisAddIn.IDs.SaveEditsButton);
            saveEditsButton.IsEnabled = false;

            SaveEditsAsButton saveEditsAsButton = AddIn.FromID<SaveEditsAsButton>(ThisAddIn.IDs.SaveEditsAsButton);
            saveEditsAsButton.IsEnabled = false;

            EditTool selectTool = AddIn.FromID<EditTool>(ThisAddIn.IDs.EditTool);
            selectTool.IsEnabled = false;

            ShowEditsButton showEditsButton = AddIn.FromID<ShowEditsButton>(ThisAddIn.IDs.ShowEditsButton);
            showEditsButton.IsEnabled = false;

            EditForm editForm = AddIn.FromID<EditForm.AddinImpl>(ThisAddIn.IDs.EditForm).UI;
            editForm.ClearValues();
            editForm.SetLayer("");
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
    }
}
