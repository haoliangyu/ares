using System;
using System.Windows.Forms;

using RasterEditor.Forms;

namespace RasterEditor.EditorMenu
{
    public class OptionButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public OptionButton()
        {
        }

        protected override void OnClick()
        {
            if (FormReference.OptionForm == null)
            {
                try
                {
                    FormReference.OptionForm = new OptionForm();
                    FormReference.OptionForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }
    }
}
                                                                                    