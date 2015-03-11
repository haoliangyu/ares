using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ARES.Painter.Menu
{
    public class StartPaintingButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public StartPaintingButton()
        {
        }

        /// <summary>
        /// Get or set a value Indicating whether the StartPaintingButton is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        protected override void OnClick()
        {
            if (ArcMapApp.RasterLayerCount > 0)
            {
                try
                {
                    Painter.StartPainting();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }
    }
}
