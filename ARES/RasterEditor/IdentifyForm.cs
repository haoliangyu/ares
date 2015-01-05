using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ARES.Control;

namespace ARES
{
    /// <summary>
    /// Dockable Form to display values and statistical result of selected pixels.
    /// </summary>
    public partial class IdentifyForm : UserControl
    {
        public IdentifyForm(object hook)
        {
            InitializeComponent();
            this.Hook = hook;

            rasterGridView.Editable = false;
            brTextBoxToolTip.SetToolTip(brPosTextBox, "Column and row index of buttom right corner.");
            tlTextBoxToolTip.SetToolTip(tlPosTextBox, "Column and row index of top left corner.");

            rasterGridView.SelectionChanged += new EventHandler(rasterGridView_SelectionChanged);
        }

        #region Properties

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Get the RasterGridView control on the form.
        /// </summary>
        public RasterGridView RasterGridView
        {
            get { return this.rasterGridView; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the name of identified layer
        /// </summary>
        /// <param name="layerName"></param>
        public void SetLayerName(string layerName)
        {
            layerNameTextBox.Text = layerName;
        }

        /// <summary>
        /// Set the cell values of the identified region
        /// </summary>
        /// <param name="tlCorner">Left-top corner</param>
        /// <param name="brCorner">Right-bottom corner</param>
        /// <param name="values"></param>
        public void SetValues(Position tlCorner, Position brCorner, double[,] values)
        {
            tlPosTextBox.Text = String.Format("({0}, {1})", tlCorner.Column + 1, tlCorner.Row + 1);
            brPosTextBox.Text = String.Format("({0}, {1})", brCorner.Column + 1, brCorner.Row + 1);

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

            rasterGridView[0, 0].Selected = false;
        }

        /// <summary>
        /// Clear all values.
        /// </summary>
        public void ClearValues()
        {
            rasterGridView.Clear();
            statDataGridView.Rows.Clear();

            tlPosTextBox.Text = "";
            brPosTextBox.Text = "";
            layerNameTextBox.Text = "";
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Set values of specific fields for the selected cell
        /// </summary>
        /// <param name="fieldValue">Dictionary of statistical fields and their values</param>
        private void SetStatistic(Dictionary<string, double> fieldValue)
        {
            statDataGridView.Rows.Clear();

            foreach (KeyValuePair<string, double> pair in fieldValue)
            {
                statDataGridView.Rows.Add(new object[] { pair.Key, pair.Value });
            }
        }

        #endregion

        #region Control Events

        // When selection changes, statistic of selection change as well.
        void rasterGridView_SelectionChanged(object sender, EventArgs e)
        {
            List<double> selectedValues = new List<double>();

            if (rasterGridView.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in rasterGridView.SelectedCells)
                {
                    if (cell.ColumnIndex == 0)
                        continue;

                    selectedValues.Add(Convert.ToDouble(cell.Value));
                }
            }
            else
            {
                for (int col = 1; col < rasterGridView.Columns.Count; col++)
                {
                    for (int row = 0; row < rasterGridView.Rows.Count; row++)
                    {
                        selectedValues.Add(Convert.ToDouble(rasterGridView[col, row].Value));
                    }
                }
            }

            SetStatistic(Math.CalStat(selectedValues.ToArray(), rasterGridView.NoDataValue));
        }

        #endregion

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private IdentifyForm m_windowUI;

            // this property is to provided access to the IdentifyForm
            internal IdentifyForm UI
            {
                get { return m_windowUI; }
            }

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new IdentifyForm(this.Hook);
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
