using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ARES
{
    public class StopPaintingButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public StopPaintingButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Get or set a value Indicating whether the StopPaintingButton is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        protected override void OnClick()
        {
        }
    }
}
