using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace RasterEditor.EditorMenu.Windows
{
    /// <summary>
    /// Menu button to open the Identify form.
    /// </summary>
    public class IdentifyFormButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        // Click and open the Idenfiy form, if it is hiden.
        protected override void OnClick()
        {
            if (ArcMap.Application.CurrentTool.Caption != "Identify")
            {
                return;
            }

            UID dockWinID = new UIDClass();
            dockWinID.Value = ThisAddIn.IDs.RasterEditor_Forms_IdentifyForm;
            IDockableWindow dockWindow = ArcMap.DockableWindowManager.GetDockableWindow(dockWinID);
            if (!dockWindow.IsVisible())
            {
                dockWindow.Show(true);
            }
        }
    }
}
