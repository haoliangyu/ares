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

using RasterEditor;
using RasterEditor.Raster;

namespace RasterEditor.Forms
{
    public partial class GoToForm : System.Windows.Forms.Form
    {
        public GoToForm()
        {
            InitializeComponent();
            FormReference.GoToForm = this;

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
                    Display.FlashBox(new Position(col, row));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                }
            }
        }

        void GoToForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormReference.GoToForm = null;
        }

        #region Tool Button Event

        private void flashingToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(rowIndexTextBox.Text) - 1;
                int col = int.Parse(colIndexTextBox.Text) - 1;
                Display.FlashBox(new Position(col, row));
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
                Position pos = new Position(col, row);

                Display.DrawSelectionBox(pos);
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

                Display.RemoveSelectionBox(pos);
                Editor.SelectionRecord.Remove(pos);
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
            if (Editor.ActiveLayer == null)
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

            IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;
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
