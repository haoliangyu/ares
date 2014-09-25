using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Display;

namespace RasterEditor.Forms
{

    /// <summary>                                       
    /// Represents a window allowing the user to change configuration of the editor.
    /// </summary>
    public partial class OptionForm : Form
    {
        #region Construction Method

        /// <summary>
        /// Initialize the option form.
        /// </summary>
        public OptionForm()
        {
            InitializeComponent();

            FormReference.OptionForm = this;
            this.FormClosed += (s, r) => FormReference.OptionForm = null;
            cancelButton.Click += (s, r) => this.Close();

            ShowConfig();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Show the editor config on the window.
        /// </summary>
        private void ShowConfig()
        {
            selFillColorPaletteButton.Color = Config.SelectionSmbol.Color;
            selTranNumericUpDown.Value = (255 - Convert.ToDecimal(Config.SelectionSmbol.Color.Transparency)) / 255 * 100;
            selOutlineColorPaletteButton.Color = Config.SelectionSmbol.Outline.Color;
            selOutlineWidthNumericUpDown.Value = Convert.ToDecimal(Config.SelectionSmbol.Outline.Width);

            editFillColorColorPaletteButton.Color = Config.EditSymbol.Color;
            editTranNumericUpDown.Value = (255 - Convert.ToDecimal(Config.EditSymbol.Color.Transparency)) / 255 * 100;
            editOutlineColorPaletteButton.Color = Config.EditSymbol.Outline.Color;
            editOutlineWidthNumericUpDown.Value = Convert.ToDecimal(Config.EditSymbol.Outline.Width);
            editCustomCheckBox.Checked = Config.CustormEditColor;
        }

        #endregion

        #region Events

        // Disable the fill color option if unchecked
        private void editCustomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            editFillColorColorPaletteButton.Enabled = !editCustomCheckBox.Checked;
            editTranNumericUpDown.Enabled = !editCustomCheckBox.Checked;
        }

        // Reset the default configuration.
        private void defaultButton_Click(object sender, EventArgs e)
        {
            Config.SetDefault();
            ShowConfig();
        }

        // Save config and exit.
        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                ISimpleFillSymbol selectionSymbol = new SimpleFillSymbolClass();
                selectionSymbol.Color = selFillColorPaletteButton.Color;
                selectionSymbol.Color.Transparency = Convert.ToByte(255 - Convert.ToDouble(selTranNumericUpDown.Value) * 2.55);
                ISimpleLineSymbol selectionOutlineSymbol = new SimpleLineSymbolClass();
                selectionOutlineSymbol.Color = selOutlineColorPaletteButton.Color;
                selectionOutlineSymbol.Width = Convert.ToDouble(selOutlineWidthNumericUpDown.Value);
                selectionSymbol.Outline = selectionOutlineSymbol;
                Config.SelectionSmbol = selectionSymbol;

                ISimpleFillSymbol editSymbol = new SimpleFillSymbolClass();
                editSymbol.Color = editFillColorColorPaletteButton.Color;
                editSymbol.Color.Transparency = Convert.ToByte(255 - Convert.ToDouble(editTranNumericUpDown.Value) * 2.55);
                ISimpleLineSymbol editOutlineSymbol = new SimpleLineSymbolClass();
                editOutlineSymbol.Color = editOutlineColorPaletteButton.Color;
                editOutlineSymbol.Width = Convert.ToDouble(editOutlineWidthNumericUpDown.Value);
                editSymbol.Outline = editOutlineSymbol;
                Config.EditSymbol = editSymbol;
                Config.CustormEditColor = editCustomCheckBox.Checked;

                Config.Save();
                Editor.Refresh();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        #endregion
    }
}