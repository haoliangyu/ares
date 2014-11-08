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
    /// A dialog allowing user to input two values.
    /// </summary>
    public partial class DoubleInputDialog : Form
    {
        #region Delegate

        public delegate bool ValueValidate(string inputValue, out object validValue);

        #endregion

        #region Attribute

        private object value = null;

        private object value2 = null;

        private ValueValidate validateMethod = null; 

        #endregion

        #region Property

        /// <summary>
        /// Set the input value name.
        /// </summary>
        public string ValueName
        {
            set { label1.Text = value; }
        }

        /// <summary>
        /// Set the input value 2 name.               
        /// </summary>
        public string ValueName2
        {
            set { label2.Text = value; }
        }

        /// <summary>
        /// Get the input value.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Get the input value 2.
        /// </summary>
        public object Value2
        {
            get { return value2; }
        }

        /// <summary>
        /// Set the value validation method.
        /// </summary>
        public ValueValidate ValueValidateMethod
        {
            set { validateMethod = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the double input dialog.
        /// </summary>
        public DoubleInputDialog()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(DoubleInputForm_FormClosing);
        }

        /// <summary>
        /// Initialize the double input dialog.
        /// </summary>
        /// <param name="valueName">Input value name.</param>
        /// <param name="valueName2">Input value 2 name.</param>
        public DoubleInputDialog(string valueName, string valueName2)
            : this()
        {
            label1.Text = valueName;
            label2.Text = valueName2;
        }

        /// <summary>
        /// Initialize the double input dialog.
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="valueName2"></param>
        /// <param name="dialogCaption"></param>
        public DoubleInputDialog(string valueName, string valueName2, string dialogCaption)
            : this(valueName, valueName2)
        {
            this.Text = dialogCaption;
        }

        #endregion

        #region Event

        // If the input fails, return DailogResult.Cancel.
        protected void DoubleInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }

        // Click the okButton the confirm the input.
        private void okButton_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "" || inputTextBox2.Text == "")
            {
                MessageBox.Show("Please input required values.", "Notice");
                return;
            }

            if (validateMethod != null)
            {
                object tValue = null;
                object tValue2 = null;
                if (!validateMethod(inputTextBox.Text, out tValue) || !validateMethod(inputTextBox2.Text, out tValue2))
                {
                    MessageBox.Show("Invalid input value.", "Notice");
                    return;
                }

                value = tValue;
                value2 = tValue2;
            }
            else
            {
                value = inputTextBox.Text;
                value2 = inputTextBox2.Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
