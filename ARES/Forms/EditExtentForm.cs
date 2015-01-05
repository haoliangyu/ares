using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

namespace ARES.Forms
{
    /// <summary>
    /// Represents a window that allows the user to edit the extent of a raster layer.
    /// </summary>
    public partial class EditExtentForm : System.Windows.Forms.Form
    {
        #region Attributes

        private IRasterLayer editLayer = null;

        #endregion

        #region Construction Method

        /// <summary>
        /// Initialize the window to edit the raster layer extent.
        /// </summary>
        public EditExtentForm()
        {
            InitializeComponent();

            InitializeRasterComboBox(inputRasterComboBox);
            InitializeRasterComboBox(outputExtentComboBox);

            FormReference.EditExtentForm = this;
            this.FormClosed += (s, e) => FormReference.EditExtentForm = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the raster combobox by adding existing raster layers.
        /// </summary>
        /// <param name="rasterComboBox"></param>
        private void InitializeRasterComboBox(System.Windows.Forms.ComboBox rasterComboBox)
        {
            for (int i = 0; i < ArcMap.Document.FocusMap.LayerCount; i++)
            {
                ILayer layer = ArcMap.Document.FocusMap.Layer[i];
                if (layer is IRasterLayer)
                {
                    rasterComboBox.Items.Add(layer.Name);
                }
            }

            rasterComboBox.KeyPress += (s, e) => e.Handled = true;
            rasterComboBox.KeyUp += (s, e) => e.Handled = true;
            rasterComboBox.KeyDown += (s, e) => e.Handled = true;
        }

        /// <summary>
        /// Show the extent of input layer in the control.
        /// </summary>
        /// <param name="rasterLayer"></param>
        private void ShowExtent(IRasterLayer rasterLayer)
        {
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;

            leftCoorTextBox.Text = rasterProps.Extent.XMin.ToString();
            buttomCoorTextBox.Text = rasterProps.Extent.YMin.ToString();

            IPnt pixelSize = rasterProps.MeanCellSize();
            pixelSizeTextBox.Text = pixelSize.Y.ToString();
        }

        #endregion

        #region Control Events

        /// <summary>
        /// Prompts the user to a file dialog and select the input raster file.
        /// </summary>
        private void openInputRasterBbutton_Click(object sender, EventArgs e)
        {
            RasterFileDialog openFileDialog = new RasterFileDialog(FileDialogType.Open);
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    inputRasterComboBox.Text = openFileDialog.FileName;
                    editLayer = new RasterLayerClass();
                    editLayer.CreateFromFilePath(openFileDialog.FileName);
                    ShowExtent(editLayer);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                    inputRasterComboBox.Text = "";
                    editLayer = null;
                }
            }

            this.BringToFront();
        }

        /// <summary>
        /// Open a file dialog and select the output raster file.
        /// </summary>
        private void openOutputRasterButton_Click(object sender, EventArgs e)
        {
            RasterFileDialog openFileDialog = new RasterFileDialog(FileDialogType.Save);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputRasterTextBox.Text = openFileDialog.FileName;
            }

            this.BringToFront();
        }

        /// <summary>
        /// Opens a file dialog and select the reference raster file.
        /// </summary>
        private void openOutputExtentButton_Click(object sender, EventArgs e)
        {
            RasterFileDialog openFileDialog = new RasterFileDialog(FileDialogType.Open);
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputExtentComboBox.Text = openFileDialog.FileName;

                try
                {
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromFilePath(openFileDialog.FileName);
                    ShowExtent(rasterLayer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
                    outputExtentComboBox.Text = "";
                }
            }

            this.BringToFront();
        }

        /// <summary>
        /// Select the reference file loaded in the ArcMap.
        /// </summary>
        private void outputExtentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IRasterLayer rasterLayer = (IRasterLayer)Editor.GetLayer(outputExtentComboBox.Text);
            ShowExtent(rasterLayer);
        }

        /// <summary>
        /// Inidcate the input file have already loaded into the ArcMap.
        /// </summary>
        private void inputRasterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editLayer = (IRasterLayer)Editor.GetLayer(inputRasterComboBox.Text);
            ShowExtent(editLayer);
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (editLayer == null)
            {
                MessageBox.Show("The input raster file is not specified.", "Warnning");
                return;
            }

            double xmin, ymin, pixelSize;
            if (!double.TryParse(leftCoorTextBox.Text, out xmin) ||
                !double.TryParse(buttomCoorTextBox.Text, out ymin) ||
                !double.TryParse(pixelSizeTextBox.Text, out pixelSize))
            {
                MessageBox.Show("The input minimum x coordinate, minimum y coordinate or pixel size is invalid.", "Warnning");
                return;
            }

            try
            {
                Editor.SaveExtentAs(editLayer, outputRasterTextBox.Text, xmin, ymin, pixelSize);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unfortunately, the application meets an error.\n\nSource: {0}\nSite: {1}\nMessage: {2}", ex.Source, ex.TargetSite, ex.Message), "Error");
            }
        }

        #endregion

    }
}
