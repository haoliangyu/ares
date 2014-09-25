using System;
using System.Windows.Forms;

namespace RasterEditor.Forms
{
    /// <summary>
    /// A window allowing user to input one single value.
    /// </summary>
    public partial class SingleInputDialog : System.Windows.Forms.Form
    {
        #region Delegates

        public delegate bool ValueValidate(string inputValue, out object validValue);

        #endregion

        #region Attributes

        private object value = null;

        private ValueValidate valueValidateMethod = null;

        //private 

        #endregion

        #region Properties

        /// <summary>
        /// Set the label showing name of the input value.
        /// </summary>
        public string ValueLabel
        {
            set { label1.Text = value; }
        }

        /// <summary>
        /// Get the input value.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Set the value validation method.
        /// </summary>
        public ValueValidate ValueValidateMethod
        {
            set { valueValidateMethod = value; }
        }

        #endregion

        #region Construction Method

        /// <summary>
        /// Initialize the single input form.
        /// </summary>
        public SingleInputDialog()
        {
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(SingleInputForm_FormClosing);
        }

        /// <summary>
        /// Initialize the single input form.
        /// </summary>
        /// <param name="valueLabel">The label showing name of the input value.</param>
        public SingleInputDialog(string valueLabel)
        {
            InitializeComponent();

            label1.Text = valueLabel;
            FormClosing += new FormClosingEventHandler(SingleInputForm_FormClosing);
        }

        /// <summary>
        /// Initialize the single input form.
        /// </summary>
        /// <param name="valueLabel">The label showing name of the input value.</param>
        /// <param name="formCaption">The caption of form.</param>
        public SingleInputDialog(string valueLabel,string formCaption)
        {
            InitializeComponent();

            label1.Text = valueLabel;
            this.Text = formCaption;
            FormClosing += new FormClosingEventHandler(SingleInputForm_FormClosing);
        }

        #endregion

        #region Evnets

        protected void SingleInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
                this.DialogResult = DialogResult.Cancel;
        }

        protected void okButton_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "")
            {
                MessageBox.Show("Please input a value.", "Notice");
                return;
            }

            if (valueValidateMethod != null)
            {
                object tValue = null;
                if (!valueValidateMethod(inputTextBox.Text, out tValue))
                {
                    MessageBox.Show("Invalid input value.", "Notice");
                    return;
                }

                value = tValue;
            }
            else
            {
                value = inputTextBox.Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
