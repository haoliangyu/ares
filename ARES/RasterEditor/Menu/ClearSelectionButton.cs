using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ARES
{
    public class ClearSelectionButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            Display.ClearSelections();
            Editor.SelectionRecord.Clear();
        }
    }
}
