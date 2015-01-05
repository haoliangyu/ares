using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ARES.Forms;

namespace ARES
{
    public class GoToPixelTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            if (FormReference.GoToForm == null)
            {
                GoToForm goToForm = new GoToForm();
                goToForm.Show();
            }
        }
    }
}
