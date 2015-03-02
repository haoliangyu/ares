using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ARES
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
            Painter.SavePaints();
            Display.ClearElement(Painter.Paints.GetAllGraphicElements());
            Painter.Paints.Clear();

            ArcMap.Document.ActiveView.Refresh();
        }
    }
}
