using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;

using RasterEditor.Forms;

namespace RasterEditor.EditorMenu.Edition
{
    public class SaveEditsAsButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SaveEditsAsButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Indicate whether the save as button is enabled.
        /// </summary>
        public bool IsEnabled
        {
            set { this.Enabled = value; }
            get { return this.Enabled; }
        }

        protected override void OnClick()
        {
            try
            {
                IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;

                RasterFileDialog saveFileDialog = new RasterFileDialog(FileDialogType.Save);
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(rasterLayer.FilePath);
                saveFileDialog.InitialFileName = Path.GetFileNameWithoutExtension(rasterLayer.FilePath) + "_edited";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Editor.SaveEditsAs(saveFileDialog.FileName);

                    Display.ClearEdits();
                    Editor.EditRecord.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }
    }
}
