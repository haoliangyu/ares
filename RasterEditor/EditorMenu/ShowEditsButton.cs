using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace RasterEditor.EditorMenu
{
    public class ShowEditsButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ShowEditsButton()
        {
            this.Enabled = false;
            this.Caption = "Hide Edits";
        }

        /// <summary>
        /// Get or set a value indicating whether the ShowEditsButton is enabled.
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
                if (Editor.ShowEdits)
                {
                    // Hide Edits
                    Display.ClearEdits();
                    this.Caption = "Show Edits";
                }
                else
                {
                    // Show Edits
                    Display.DrawEditionBox();
                    this.Caption = "Hide Edits";
                }

                Editor.ShowEdits = !Editor.ShowEdits;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }
    }
}
