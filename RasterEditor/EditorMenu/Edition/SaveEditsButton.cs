using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

namespace RasterEditor.EditorMenu.Edition
{
    public class SaveEditsButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SaveEditsButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Get or set a value Indicating whether the SaveEditsButton is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        #region Events

        protected override void OnClick()
        {
            Editor.SaveEdits();
            Display.ClearEdits();
            Editor.EditRecord.Clear();

            ArcMap.Document.ActiveView.Refresh();
        }

        #endregion
    }
}
