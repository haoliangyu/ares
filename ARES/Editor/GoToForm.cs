using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

using ARES;
using ARES.Forms;

namespace ARES.Editor
{
    /// <summary>
    /// Defines behaviors of Go To Pixel form.
    /// </summary>
    public partial class GoToForm : System.Windows.Forms.Form
    {
        public GoToForm()
        {
            InitializeComponent();

            this.toolStrip.Enabled = false;
            this.flashingToolStripButton.ToolTipText = "Flashing";
            this.addSelectionToolStripButton.ToolTipText = "Add to selection";
            this.deleteSelectionToolStripButton.ToolTipText = "Delete from selection";

            this.colIndexTextBox.KeyPress += rowIndexTextBox_KeyPress;
            this.rowIndexTextBox.KeyPress += rowIndexTextBox_KeyPress;
            this.colIndexTextBox.TextChanged += colIndexTextBox_TextChanged;
            this.rowIndexTextBox.TextChanged += colIndexTextBox_TextChanged;
            this.FormClosed += GoToForm_FormClosed;
        }

        void colIndexTextBox_TextChanged(object sender, EventArgs e)
        {
            this.toolStrip.Enabled = CheckRowColValue();
        }

        void rowIndexTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            if (CheckRowColValue())
            {
                try
                {
                    int row = int.Parse(rowIndexTextBox.Text) - 1;
                    int col = int.Parse(colIndexTextBox.Text) - 1;
                    Display.FlashSelection(new Position(col, row), ArcMapApp.GetRasterLayer());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }

        void GoToForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormList.Remove<GoToForm>();
        }

        #region Tool Button Event

        private void flashingToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(rowIndexTextBox.Text) - 1;
                int col = int.Parse(colIndexTextBox.Text) - 1;
                Display.FlashSelection(new Position(col, row), ArcMapApp.GetRasterLayer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }  
        }

        private void addSelectionToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(rowIndexTextBox.Text) - 1;                                
                int col = int.Parse(colIndexTextBox.Text) - 1;

                if (!Editor.Selections.Exists(col, row))
                {
                    Pixel pixel = new Pixel(new Position(col, row));
                    pixel.GraphicElement = Display.DrawBox(pixel.Position, Editor.GetSelectionSymbol(), ArcMapApp.GetRasterLayer(), true);
                    Editor.Selections.Add(pixel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        private void deleteSelectionToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(rowIndexTextBox.Text) - 1;
                int col = int.Parse(colIndexTextBox.Text) - 1;
                Position pos = new Position(col, row);

                if (Editor.Selections.Exists(pos))
                {
                    Display.RemoveElement(Editor.Selections[pos].GraphicElement, true);
                    Editor.Selections.Remove(pos);    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check the 
        /// </summary>
        /// <returns></returns>
        private bool CheckRowColValue()
        {
            ILayer layer = ArcMapApp.GetRasterLayer();
            if (layer == null)
            {
                return false;
            }

            int row = 0;
            int col = 0;
            if (!int.TryParse(rowIndexTextBox.Text, out row) ||
                !int.TryParse(colIndexTextBox.Text, out col))
            {
                return false;
            }

            IRasterLayer rasterLayer = (IRasterLayer)layer;
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;
            if (row < 1 || row > rasterProps.Height || col < 1 || col > rasterProps.Width)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
