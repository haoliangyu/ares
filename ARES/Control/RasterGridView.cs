using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ARES.Control
{
    public class RasterGridView : DataGridView
    {
        public RasterGridView() : base()
        {
            // Not allow user to modify the raster
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;

            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            MultiSelect = true; 
        }
                             
        #region Delegates

        public delegate void CheckEditedCell(DataGridViewCell cell);

        #endregion

        #region attributes

        private double noDataValue = 0;

        private bool editable = true;

        private CheckEditedCell checkEditedCellMethod = null;

        #endregion

        #region Properties

        /// <summary>
        /// Indicate whether the grdi control is editable.
        /// </summary>
        public bool Editable
        {
            get { return editable; }
            set
            {
                editable = value;
                SetEditability(editable);
            }
        }

        /// <summary>
        /// Set the method used to check values while being set into cells.
        /// </summary>
        public CheckEditedCell CheckEditedCellMethod
        { 
            set { checkEditedCellMethod = value; } 
        }

        /// <summary>
        /// Get or set the NoData value.
        /// </summary>
        public double NoDataValue
        {
            set { noDataValue = value; }
            get { return noDataValue; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set the editability of the raster cell
        /// </summary>
        /// <param name="editability"></param>
        private void SetEditability(bool editability)
        {
            if (Columns.Count > 1)
            {
                for (int i = 1; i < Columns.Count; i++)
                {
                    Columns[i].ReadOnly = !editability;    
                }
            }
        }

        /// <summary>
        /// Blod the text of specific cell with given column and row index.
        /// </summary>
        /// <param name="columnIndex">Column index of the rasterGridView.</param>
        /// <param name="rowIndex">Row index of the rasterGridView.</param>
        public void BoldCellText(int columnIndex, int rowIndex)
        {
            this[columnIndex, rowIndex].Style.Font = new Font(this.Font, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// Unbold the text of specific cell with given column and row index.
        /// </summary>
        /// <param name="columnIndex">Column index of the rasterGridView.</param>
        /// <param name="rowIndex">Row index of the rasterGridView.</param>
        public void UnboldCellText(int columnIndex, int rowIndex)
        {
            this[columnIndex, rowIndex].Style.Font = new Font(this.Font, System.Drawing.FontStyle.Regular);
        }

        /// <summary>
        /// Fill the value grid.
        /// </summary>
        /// <param name="tlCorner">The column and row index of the left-top concern</param>
        /// <param name="brCorner">The column and row index of the right-buttom concern</param>
        /// <param name="values">The matrix that contains values</param>
        public void SetValues(Position tlCorner, Position brCorner, double[,] values)
        {
            Clear();

            Columns.Add("RowNumber", "");
            Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
 
            for (int i = tlCorner.Row; i <= brCorner.Row; i++)
            {
                int index = Rows.Add();
                Rows[index].Cells[0].Value = i + 1;
                Rows[index].Cells[0].Style.BackColor = Color.Gray;
            }
            Columns[0].ReadOnly = true;

            for (int x = tlCorner.Column; x <= brCorner.Column; x++)
            {
                string colStr = (x + 1).ToString();
                int index = Columns.Add(colStr, colStr);

                Columns[index].ReadOnly = !editable;
                Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int y = tlCorner.Row; y <= brCorner.Row; y++)
                {
                    double value = values[x - tlCorner.Column, y - tlCorner.Row];

                    if (value == noDataValue)
                        continue;

                    Rows[y - tlCorner.Row].Cells[x - tlCorner.Column + 1].Value = value;

                    if (checkEditedCellMethod != null)
                        checkEditedCellMethod(Rows[y - tlCorner.Row].Cells[x - tlCorner.Column + 1]);
                }
            }
        }

        /// <summary>
        /// Clear all values.
        /// </summary>
        public void Clear()
        {
            Rows.Clear();
            Columns.Clear();
        }

        #endregion
    }
}
