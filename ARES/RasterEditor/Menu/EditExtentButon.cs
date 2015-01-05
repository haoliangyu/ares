using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ARES.Forms;

namespace ARES
{
    public class EditExtentButon : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            if (FormReference.EditExtentForm == null)
            {
                FormReference.EditExtentForm = new EditExtentForm();
                FormReference.EditExtentForm.Show();
            }
        }
    }
}
