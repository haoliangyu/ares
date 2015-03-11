using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Desktop.AddIns;

using ARES.Forms;
using ARES.Controls;
using ARES.Painter.Menu;

namespace ARES.Painter
{
    /// <summary>
    /// Provides access of properties and functions that control the Raster Painter toolbar.
    /// </summary>
    static class Painter
    {
        # region Attributes

        private static IRasterLayer activeLayer = null;

        private static bool isPainting = false;

        private static bool showPaints = true;

        private static PixelCollection paints = new PixelCollection();

        #endregion

        #region Properties

        /// <summary>
        /// Get the active layer.
        /// </summary>
        public static IRasterLayer ActiveLayer
        {
            get { return activeLayer; }
        }

        /// <summary>
        /// Get a boolean value indicating whether the active layer is being painted.
        /// </summary>
        public static bool IsPainting
        {
            get { return isPainting; }
        }

        /// <summary>
        /// Get the collection of painted pixels.
        /// </summary>
        public static PixelCollection Paints
        {
            get { return paints; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start a painting section.
        /// </summary>
        public static void StartPainting()
        {
            // Select a layer to edit first
            int rasterLayerCount = ArcMapApp.RasterLayerCount;
            if (rasterLayerCount == 1)
            {
                activeLayer = ArcMapApp.GetRasterLayer();
            }
            else
            {
                SelectLayerForm selectLayerForm = new SelectLayerForm();
                selectLayerForm.ShowDialog();

                if (selectLayerForm.ReturnLayer == null)
                {
                    return;
                }

                activeLayer = selectLayerForm.ReturnLayer;
            }

            isPainting = true;

            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.ARES_Painter_ValueSymbolForm;
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            if (!dockWindow.IsVisible())
            {
                dockWindow.Show(true);
            }

            ValueSymbolForm valueSymbolForm = AddIn.FromID<ValueSymbolForm.AddinImpl>(ThisAddIn.IDs.ARES_Painter_ValueSymbolForm).UI;
            valueSymbolForm.LoadLayer(activeLayer);

            StartPaintingButton startPaintingButton = AddIn.FromID<StartPaintingButton>(ThisAddIn.IDs.ARES_Painter_Menu_StartPaintingButton);
            startPaintingButton.IsEnabled = false;

            StopPaintingButton stopPaintingButton = AddIn.FromID<StopPaintingButton>(ThisAddIn.IDs.ARES_Painter_Menu_StopPaintingButton);
            stopPaintingButton.IsEnabled = true;

            SavePaintsButton savePaintsButton = AddIn.FromID<SavePaintsButton>(ThisAddIn.IDs.ARES_Painter_Menu_SavePaintsButton);
            savePaintsButton.IsEnabled = true;

            SavePaintsAsButton savePaintAsButton = AddIn.FromID<SavePaintsAsButton>(ThisAddIn.IDs.ARES_Painter_Menu_SavePaintsAsButton);
            savePaintAsButton.IsEnabled = true;

            FreeDrawTool freeDrawTool = AddIn.FromID<FreeDrawTool>(ThisAddIn.IDs.ARES_Painter_FreeDrawTool);
            freeDrawTool.IsEnabled = true;

            EraseTool eraseTool = AddIn.FromID<EraseTool>(ThisAddIn.IDs.ARES_Painter_EraseTool);
            eraseTool.IsEnabled = true;
        }

        /// <summary>
        /// Stop a painting section. 
        /// </summary>
        public static void StopPainting()
        {
            activeLayer = null;
            isPainting = false;
            Display.ClearElement(paints.GetAllGraphicElements());
            paints.Clear();

            StartPaintingButton startPaintingButton = AddIn.FromID<StartPaintingButton>(ThisAddIn.IDs.ARES_Painter_Menu_StartPaintingButton);
            startPaintingButton.IsEnabled = true;

            StopPaintingButton stopPaintingButton = AddIn.FromID<StopPaintingButton>(ThisAddIn.IDs.ARES_Painter_Menu_StopPaintingButton);
            stopPaintingButton.IsEnabled = false;

            SavePaintsButton savePaintsButton = AddIn.FromID<SavePaintsButton>(ThisAddIn.IDs.ARES_Painter_Menu_SavePaintsButton);
            savePaintsButton.IsEnabled = false;

            SavePaintsAsButton savePaintAsButton = AddIn.FromID<SavePaintsAsButton>(ThisAddIn.IDs.ARES_Painter_Menu_SavePaintsAsButton);
            savePaintAsButton.IsEnabled = false;

            FreeDrawTool freeDrawTool = AddIn.FromID<FreeDrawTool>(ThisAddIn.IDs.ARES_Painter_FreeDrawTool);
            freeDrawTool.IsEnabled = false;

            EraseTool eraseTool = AddIn.FromID<EraseTool>(ThisAddIn.IDs.ARES_Painter_EraseTool);
            eraseTool.IsEnabled = false;

            ValueSymbolForm valueSymbolForm = AddIn.FromID<ValueSymbolForm.AddinImpl>(ThisAddIn.IDs.ARES_Painter_ValueSymbolForm).UI;
            valueSymbolForm.ClearLayer();
        }

        /// <summary>
        /// Save the edition as a specified file.
        /// </summary>
        /// <param name="fileName"></param>
        public static void SaveEditsAs(string fileName)
        {
            try
            {
                Raster.SaveEditsAs(fileName, activeLayer, Paints);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        /// <summary>
        /// Save all edits to file.
        /// </summary>
        public static void SavePaints()
        {
            Raster.SaveEdits(activeLayer, Paints);
        }

        /// <summary>
        /// Get the paint symble of painted pixel.
        /// </summary>
        /// <param name="filledColor">Fill color for the symbol</param>
        /// <returns></returns>
        public static ISimpleFillSymbol GetPaintSymbol(IColor filledColor)
        {
            ISimpleFillSymbol selectionSymbol = new SimpleFillSymbolClass();
            selectionSymbol.Color = filledColor;
            ISimpleLineSymbol selectionOutlineSymbol = new SimpleLineSymbolClass();
            selectionOutlineSymbol.Color = new RgbColorClass() { Red = 255, Green = 255, Blue = 0 };
            selectionOutlineSymbol.Width = 2;
            selectionSymbol.Outline = selectionOutlineSymbol;

            return selectionSymbol;
        }

        #endregion
    }
}
