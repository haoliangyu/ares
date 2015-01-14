using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ARES
{
    public class StartEditingButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public StartEditingButton()
        {
        }

        /// <summary>
        /// Get or set a value Indicating whether the StartEditingButton is enabled.
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
                Editor.StartEditing();
            }
        }
    }
}
