using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

using ARES.Control;
using ARES.Forms;

namespace ARES
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class EditForm : UserControl
    {
        public EditForm(object hook)
        {
            InitializeComponent();
            this.Hook = hook;

            rasterGridView.Editable = true;
            rasterGridView.SelectionChanged += rasterGridView_SelectionChanged;
            rasterGridView.CellBeginEdit += rasterGridView_CellBeginEdit;
            rasterGridView.CellEndEdit += rasterGridView_CellEndEdit;

            editionToolStripStatusLabel.Text = "";
        }

        #region Attributes

        private double oldValue = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Get the RasterGridView control on the form.
        /// </summary>
        public RasterGridView RasterGridView
        {
            get { return this.rasterGridView; }
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the NoData value of the identified layer.
        /// </summary>
        /// <param name="value"></param>
        public void SetNoDataValue(double value)
        {
            noDataValueLabel.Text = String.Format("NoData Value: {0}", value);
        }

        /// <summary>
        /// Set the name of the editing layer.
        /// </summary>
        /// <param name="layerName"></param>
        public void SetLayer(string layerName)
        {
            layerNameTextBox.Text = layerName;
        }

        /// <summary>
        /// Set the cell values of the selected region
        /// </summary>
        /// <param name="tlCorner">Left-top corner</param>
        /// <param name="brCorner">Right-bottom corner</param>
        /// <param name="values"></param>
        public void SetValues(Position tlCorner, Position brCorner, double[,] values)
        {
            rasterGridView.SetValues(tlCorner, brCorner, values);
            PixelCollection editedCellCollection = Editor.EditRecord.WithIn(tlCorner, brCorner);
            for (int i = 0; i < editedCellCollection.Count; i++)
            {
                int gridViewCol = editedCellCollection[i].Position.Column - tlCorner.Column + 1;
                int gridViewRow = editedCellCollection[i].Position.Row - tlCorner.Row;

                if (editedCellCollection[i].NewValue == rasterGridView.NoDataValue)
                {
                    rasterGridView[gridViewCol, gridViewRow].Value = editedCellCollection[i].NewValue;
                }

                rasterGridView[gridViewCol, gridViewRow].Style.Font = new Font(rasterGridView.Font, FontStyle.Bold);
            }
        }

        /// <summary>
        /// Clear all values.
        /// </summary>
        public void ClearValues()
        {
            layerNameTextBox.Text = "";
            rasterGridView.Clear();
            editionToolStripStatusLabel.Text = "";
            noDataValueLabel.Text = "NoData Value:";
        }

        /// <summary>
        /// Show the value information of selected cells at the status label.
        /// </summary>
        private void ShowCellStatus()
        {
            if (rasterGridView.SelectedCells.Count == 0)
            {
                editionToolStripStatusLabel.Text = "";
            }
            else if (rasterGridView.SelectedCells.Count == 1)
            {
                if (rasterGridView.SelectedCells[0].ColumnIndex == 0)
                {
                    editionToolStripStatusLabel.Text = "";
                }
                else
                {
                    Pixel cell = Editor.EditRecord[Convert.ToInt32(rasterGridView.SelectedCells[0].OwningColumn.HeaderText) - 1,
                                                         Convert.ToInt32(rasterGridView.SelectedCells[0].OwningRow.Cells[0].Value) - 1];

                    if (cell != null)
                    {
                        editionToolStripStatusLabel.Text = String.Format("New Value: {0}, Original Value: {1}", cell.NewValue, cell.NewValue);
                    }
                    else
                    {
                        editionToolStripStatusLabel.Text = String.Format("Cell Value: {0}", Convert.ToDouble(rasterGridView.SelectedCells[0].Value));
                    }
                }
            }
            else
            {
                editionToolStripStatusLabel.Text = String.Format("Selected Count: {0}", rasterGridView.SelectedCells.Count);
            }
        }

        /// <summary>
        /// Edit the value of RasterGridView at the specified position.
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        /// <param name="gridViewCol"></param>
        /// <param name="gridViewRow"></param>
        private void EditValue(double newValue, double oldValue, int gridViewCol, int gridViewRow)
        {
            Position pos = new Position(Convert.ToInt32(rasterGridView.Columns[gridViewCol].HeaderText) - 1,
                                              Convert.ToInt32(rasterGridView.Rows[gridViewRow].Cells[0].Value) - 1);

            Pixel existingCell = Editor.EditRecord[pos];
            if (existingCell == null)
            {
                Pixel newCell = new Pixel(oldValue, pos);
                newCell.NewValue = newValue;
                Editor.EditRecord.Add(newCell);

                rasterGridView.BoldCellText(gridViewCol, gridViewRow);

                if (Editor.ShowEdits)
                    Display.DrawEditionBox(pos);
            }
            else
            {
                if (existingCell.NewValue == newValue)
                {
                    if (Editor.ShowEdits)
                        Display.RemoveEditionBox(pos);

                    Editor.EditRecord.Remove(existingCell);
                    rasterGridView.UnboldCellText(gridViewCol, gridViewRow);

                    if (newValue == rasterGridView.NoDataValue)
                    {
                        rasterGridView[gridViewCol, gridViewRow].Value = null;
                        return;
                    }
                }
                else
                {
                    existingCell.NewValue = newValue;
                    rasterGridView.BoldCellText(gridViewCol, gridViewRow);
                }
            }

            rasterGridView[gridViewCol, gridViewRow].Value = newValue;
        }

        /// <summary>
        /// Validate the input value.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <param name="validValue">If validation successes, return the validated value. If not, return null.</param>
        /// <returns>A value indicating whether the validationg is successful.</returns>
        private bool ValueValidate(string value, out object validValue)
        {
            IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;
            IRasterProps rasterProp = (IRasterProps)rasterLayer.Raster;

            return Editor.CSharpValue2PixelValue(value, rasterProp.PixelType, out validValue);
        }

        #endregion

        #region Control Events

        // When cells are selected in the rasterGridView, the same cells will also
        // be selected on the map.
        void rasterGridView_SelectionChanged(object sender, EventArgs e)
        {
            ShowCellStatus();
        }

        // Before editing the cell value, the original value is recorded.
        void rasterGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = Convert.ToDouble(rasterGridView[e.ColumnIndex, e.RowIndex].Value);
        }

        // After editing the cell value, the current value is recorded. 
        void rasterGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Position pos = new Position(Convert.ToInt32(rasterGridView.SelectedCells[0].OwningColumn.HeaderText) - 1,
                              Convert.ToInt32(rasterGridView.SelectedCells[0].OwningRow.Cells[0].Value) - 1);

                IRasterLayer rasterLayer = (IRasterLayer)Editor.ActiveLayer;
                IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;

                object newValue = null;

                if (rasterGridView[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    if (oldValue != rasterGridView.NoDataValue)
                    {
                        EditValue(rasterGridView.NoDataValue, oldValue, e.ColumnIndex, e.RowIndex);
                    }
                }
                else
                {
                    if (Editor.CSharpValue2PixelValue(rasterGridView[e.ColumnIndex, e.RowIndex].Value, rasterProps.PixelType, out newValue))
                    {
                        double newDValue = Convert.ToDouble(newValue);
                        EditValue(newDValue, oldValue, e.ColumnIndex, e.RowIndex);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Invalid pixel value!\n\nValid pixel type: {0}", rasterProps.PixelType.ToString()), "Error");

                        if (oldValue != rasterGridView.NoDataValue)
                        {
                            rasterGridView[e.ColumnIndex, e.RowIndex].Value = oldValue;
                        }     
                    }
                }

                ShowCellStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        #region Options Menu

        // Edit values of selected pixels in ROI as a specified value.
        private void editSelectedPixelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (rasterGridView.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Please select pixels to be edited.", "Message");
                    return;
                }

                SingleInputForm inputValueForm = new SingleInputForm("New Value:", "Edit All");
                inputValueForm.ValueValidateMethod = this.ValueValidate;

                if (inputValueForm.ShowDialog() != DialogResult.OK)
                    return;

                double newValue = Convert.ToDouble(inputValueForm.Value);

                foreach (DataGridViewCell selectedCell in rasterGridView.SelectedCells)
                {
                    if (selectedCell.ColumnIndex == 0)
                        continue;

                    double oldValue = Convert.ToDouble(selectedCell.Value);
                    if (oldValue == newValue)
                        continue;

                    EditValue(newValue, oldValue, selectedCell.ColumnIndex, selectedCell.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        // Change values of pixels in ROI as a specified value.
        private void editAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SingleInputForm inputValueForm = new SingleInputForm("New Value:", "Edit All");
                inputValueForm.ValueValidateMethod = this.ValueValidate;

                if (inputValueForm.ShowDialog() != DialogResult.OK)
                    return;

                double newValue = Convert.ToDouble(inputValueForm.Value);

                for (int col = 1; col < rasterGridView.Columns.Count; col++)
                {
                    for (int row = 0; row < rasterGridView.Rows.Count; row++)
                    {
                        double oldValue = Convert.ToDouble(rasterGridView[col, row].Value);
                        if (oldValue == newValue)
                            continue;

                        EditValue(newValue, oldValue, col, row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        // Clear edits of selected cells
        private void clearSelectedEditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you going to clear edits of selected pixels?", "Confirm", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            if (rasterGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select pixels to be edited.", "Message");
                return;
            }

            try
            {
                foreach (DataGridViewCell selectedCell in rasterGridView.SelectedCells)
                {
                    if (selectedCell.ColumnIndex == 0)
                        continue;

                    int col = Convert.ToInt32(selectedCell.OwningColumn.HeaderText) - 1;
                    int row = Convert.ToInt32(selectedCell.OwningRow.Cells[0].Value) - 1;

                    Pixel editedCell = Editor.EditRecord[col, row];
                    if (editedCell != null)
                    {
                        EditValue(editedCell.NewValue, editedCell.NewValue, selectedCell.ColumnIndex, selectedCell.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        // Clear all edits within the ROI
        private void clearAllEditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you going to clear all edits within the selected region?", "Confirm", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            Position tlCorner = new Position(Convert.ToInt32(rasterGridView.Columns[1].HeaderText) - 1,
                                                   Convert.ToInt32(rasterGridView.Rows[0].Cells[0].Value) - 1);
            Position brCorner = new Position(Convert.ToInt32(rasterGridView.Columns[rasterGridView.Columns.Count - 1].HeaderText) - 1,
                                                   Convert.ToInt32(rasterGridView.Rows[rasterGridView.Rows.Count - 1].Cells[0].Value) - 1);

            PixelCollection editedCellCollection = Editor.EditRecord.WithIn(tlCorner, brCorner);
            for (int i = 0; i < editedCellCollection.Count; i++)
            {
                Pixel editedCell = editedCellCollection[i];
                EditValue(editedCell.NewValue, editedCell.NewValue, editedCell.Position.Column - tlCorner.Column + 1, editedCell.Position.Row - tlCorner.Row);
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private EditForm m_windowUI;

            // this property is to provide access to the IdentifyForm
            internal EditForm UI
            {
                get { return m_windowUI; }
            }

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new EditForm(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }
    }
}
