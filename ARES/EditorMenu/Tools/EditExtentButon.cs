using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using RasterEditor.Forms;

namespace RasterEditor.EditorMenu.Tools
{
    public class EditExtentButon : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public EditExtentButon()
        {
        }

        protected override void OnClick()
        {
            if (FormReference.EditExtentForm == null)
            {
                FormReference.EditExtentForm = new EditExtentForm();
                FormReference.EditExtentForm.Show();
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}
