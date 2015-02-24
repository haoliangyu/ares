using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ARES
{
    /// <summary>
    /// Defines functions and behaviors of Erase tool on the Raster Painter toolbar.
    /// </summary>
    public class EraseTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public EraseTool()
        {
            this.Enabled = false;
        }

        #region Properties

        /// <summary>
        /// Indicate whether the select tool is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        #endregion

        protected override void OnClick()
        {
        }

        protected override void OnUpdate()
        {
        }
    }
}
