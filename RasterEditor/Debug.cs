using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

using RasterEditor.Raster;
using RasterEditor.Forms;

namespace RasterEditor
{
    public class Debug : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public Debug()
        {
        }

        protected override void OnClick()
        {

            try
            {
                IColor oneColor = Render.GetRenderColor((IRasterLayer)Editor.ActiveLayer, 1);
                IColor zeroColor = Render.GetRenderColor((IRasterLayer)Editor.ActiveLayer, 0);

                MessageBox.Show(string.Format("1 - RGB:{0} G:{1} B:{2}", (oneColor.RGB >> 16) & 0x0ff, 
                                                                          (oneColor.RGB >> 8) & 0x0ff, 
                                                                          oneColor.RGB & 0x0ff));
                MessageBox.Show(string.Format("0 - R:{0} G:{1} B:{2}", (zeroColor.RGB >> 16) & 0x0ff,
                                                                          (zeroColor.RGB >> 8) & 0x0ff,
                                                                          zeroColor.RGB & 0x0ff));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}
