using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Desktop.AddIns;

using RasterEditor;

namespace RasterEditor.EditorMenu.Edition
{
    public class StartEditingButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public StartEditingButton()
        {
            
        }

        /// <summary>
        /// Indicate whether the start button is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        protected override void OnClick()
        {
            if (Editor.ActiveLayer != null)
            {
                Editor.StartEditing();
            }
        }
    }
}
