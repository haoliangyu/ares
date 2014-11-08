using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace RasterEditor.EditorMenu.Windows
{
    /// <summary>
    /// Menu button to show the edit form.
    /// </summary>
    public class EditFormButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        // Disabled when it is not at editing section.
        public EditFormButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Get or set a value indicating whether the ShowEditsButton is enabled.
        /// </summary>
        public bool IsEnabled
        {
            set { this.Enabled = value; }
            get { return this.Enabled; }
        }

        // Click to show the Edit Form, if it is closed.
        protected override void OnClick()
        {
            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.RasterEditor_Forms_EditForm;
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            if (!dockWindow.IsVisible())
            {
                dockWindow.Show(true);
            }
        }
    }
}
