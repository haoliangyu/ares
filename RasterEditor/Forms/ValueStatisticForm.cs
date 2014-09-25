using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RasterEditor.Forms
{
    /// <summary>
    /// Form that show the statistical result.
    /// </summary>
    public partial class ValueStatisticForm : System.Windows.Forms.Form
    {
        public ValueStatisticForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set values of specific fields.
        /// </summary>
        /// <param name="fieldValue">Dictionary of statistical fields and their values</param>
        public void SetStatistic(Dictionary<string, double> fieldValue)
        {
            foreach (KeyValuePair<string, double> pair in fieldValue)
            {
                statDataGridView.Rows.Add(new object[] { pair.Key, pair.Value });
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
