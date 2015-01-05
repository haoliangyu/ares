using System;
using System.Windows.Forms;

namespace RasterEditor.EditorMenu
{
    public class RefreshButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public RefreshButton()
        {
        }

        protected override void OnClick()
        {
            try
            {
                Editor.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }
    }
}
