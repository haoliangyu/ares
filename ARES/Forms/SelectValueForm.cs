using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ARES.Forms
{
    /// <summary>
    /// Define the behavior of the value selection form.
    /// </summary>
    public partial class SelectValueForm : Form
    {
        public SelectValueForm()
        {
            InitializeComponent();
            valueListBox.Items.Clear();

            this.DialogResult = DialogResult.Cancel;
        }

        #region Properties

        /// <summary>
        /// Set the display name of value.
        /// </summary>
        public string ValueName
        {
            set
            {
                label1.Text = value + " List";       
            }
        }

        /// <summary>
        /// Get the list of selected values.
        /// </summary>
        /// <returns></returns>
        public string[] SelectedValue
        {
            get 
            {
                if (valueListBox.SelectedIndices.Count == 0)
                {
                    return null;
                }

                string[] values = new string[valueListBox.SelectedIndices.Count];
                foreach (int index in valueListBox.SelectedIndices)
                {
                    values[index] = valueListBox.Items[index].ToString();
                }

                return values;    
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize values in the selection list.
        /// </summary>
        /// <param name="valueNames"></param>
        public void InitializeValues(string[] valueNames)
        {
            valueListBox.Items.Clear();
                                  
            valueListBox.Items.AddRange(valueNames);
        }                 

        #endregion

        #region Events

        private void okButton_Click(object sender, EventArgs e)
        {
            if (valueListBox.SelectedItems.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else 
            {
                MessageBox.Show("No value selected.");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
