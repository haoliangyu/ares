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
            if (!FormList.Exists<EditExtentForm>())
            {
                EditExtentForm editExtentForm = new EditExtentForm();
                FormList.Add(editExtentForm);
                editExtentForm.Show();
            }
        }
    }
}
