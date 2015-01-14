using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ARES
{
    public class ShowEditsButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ShowEditsButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Get or set a value Indicating whether the ShowEditsButton is enabled.
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
                if (Editor.ShowEdits)
                {
                    // Hide Edits
                    Display.ClearElement(Editor.Edits.GetAllGraphicElements());
                    foreach(Pixel pixel in Editor.Edits)
                    {
                        pixel.GraphicElement = null;
                    }
                }
                else
                {
                    // Show Edits
                    foreach (Pixel pixel in Editor.Edits)
                    {
                        if (pixel.GraphicElement == null)
                        {
                            pixel.GraphicElement = Display.DrawBox(pixel.Position, Editor.GetEidtSymbol(), Editor.ActiveLayer);   
                        }
                    }
                    ArcMap.Document.ActiveView.Refresh();
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
