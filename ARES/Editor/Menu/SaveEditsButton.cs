using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ARES.Editor
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

        protected override void OnClick()
        {
            Editor.SaveEdits();
            Display.ClearElement(Editor.Edits.GetAllGraphicElements());
            Editor.Edits.Clear();

            ArcMap.Document.ActiveView.Refresh();
        }
    }
}
