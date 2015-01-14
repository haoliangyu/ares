using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ARES
{
    public class StopEditingButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public StopEditingButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Get or set a value Indicating whether the StopEditingButton is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.Enabled; }
            set { this.Enabled = value; }
        }

        protected override void OnClick()
        {
            try
            {
                if (Editor.Edits.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Save edits to the file?", "Message", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        Editor.SaveEdits();
                    }
                }

                Editor.StopEditing();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }
    }
}
