using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using RasterEditor.Forms;

namespace RasterEditor.EditorMenu.Windows
{
    /// <summary>
    /// Menu button to open the GoTo Pixel form.
    /// </summary>
    public class GoToPixelFormButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        // Click and open the GoTo Pixel form, if it is hiden.
        protected override void OnClick()
        {
            if (FormReference.GoToForm == null)
            {
                FormReference.GoToForm = new GoToForm();
                FormReference.GoToForm.Show();
            }
        }
    }
}
