using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ARES.Editor
{
    public class ClearSelectionButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            Display.ClearElement(Editor.Selections.GetAllGraphicElements());
            Editor.Selections.Clear();
        }
    }
}
