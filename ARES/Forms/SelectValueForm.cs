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

        #region Attributes

        private List<string> selectedValues = new List<string>();
                              
        #endregion

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
        public string[] SelectedValues
        {
            get 
            {
                if (selectedValues.Count > 0)
                {
                    return selectedValues.ToArray();
                }
                else 
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Set a value indicating whether adding new value is possible.
        /// </summary>
        public bool NewValue
        {
            set { newButton.Visible = value; }    
        }

        /// <summary>
        /// Set a value indicating whether mulitselection is possible.
        /// </summary>
        public bool MultiSelect
        {
            set 
            {
                if (value)
                {
                    valueListBox.SelectionMode = SelectionMode.MultiExtended;
                    selectAllButton.Visible = true;
                }
                else
                {
                    valueListBox.SelectionMode = SelectionMode.One;
                    selectAllButton.Visible = false;
                }
            }       
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize values in the selection list.
        /// </summary>
        /// <param name="values"></param>
        public void InitializeValues(string[] values)
        {
            valueListBox.Items.Clear();
                                  
            valueListBox.Items.AddRange(values);
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

        private void valueListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValues.Clear();

            foreach(object item in valueListBox.SelectedItems)
            {
                selectedValues.Add(item.ToString());
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < valueListBox.Items.Count; i++)
            {
                valueListBox.SetSelected(i, true);
            }
        }

        #endregion
    }
}
