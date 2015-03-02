using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ARES
{
    public class EraseTool : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public EraseTool()
        {
            this.Enabled = false;
        }

        #region Attributes

        private Envelope layerExetent = null;

        private Position preMousePos = null;

        #endregion

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
    }

}
