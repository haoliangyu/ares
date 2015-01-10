using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ARES.Forms;

namespace ARES.Editor
{
    public class GoToPixelTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            if (!FormList.Exists<GoToForm>())
            {
                GoToForm goToForm = new GoToForm();
                FormList.Add(goToForm);
                goToForm.Show();
            }
        }
    }
}
