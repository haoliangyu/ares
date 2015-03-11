using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ARES.Painter.Menu
{
    public class SavePaintsButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public SavePaintsButton()
        {
            this.Enabled = false;
        }

        /// <summary>
        /// Get or set a value Indicating whether the SaveEditsButton is enabled.
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
                Painter.SavePaints();
                Display.ClearElement(Painter.Paints.GetAllGraphicElements());
                Painter.Paints.Clear();

                ArcMap.Document.ActiveView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }
    }
}
