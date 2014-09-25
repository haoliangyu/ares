using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using RasterEditor.Forms;

namespace RasterEditor
{
    public class GoToPixelTool : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public GoToPixelTool()
        {
        }

        protected override void OnClick()
        {
            if (FormReference.GoToForm != null)
            {
                return;
            }

            GoToForm goToForm = new GoToForm();
            goToForm.Show();
        }

        protected override void OnUpdate()
        {
        }
    }
}
