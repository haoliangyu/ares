using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;

namespace ARES
{
    public class TestButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public TestButton()
        {
        }

        protected override void OnClick()
        {
            try
            {
                IAlgorithmicColorRamp colorRampe = new AlgorithmicColorRampClass();
                colorRampe.FromColor = (new RgbColorClass() { Red = 255, Green = 0, Blue = 255 }) as IColor;
                colorRampe.ToColor = (new RgbColorClass() { Red = 255, Green = 0, Blue = 0 }) as IColor;
                colorRampe.Size = 256;
                colorRampe.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
                bool ok = false;
                colorRampe.CreateRamp(out ok);
                MessageBox.Show(colorRampe.Color[255].RGB.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        protected override void OnUpdate()
        {
        }
    }
}
