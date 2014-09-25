using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;

using RasterEditor.Raster;

namespace RasterEditor
{
    // Provide access to values of a raster cell.
    public class Pixel
    {
        /// <summary>
        /// Initialize a new instance of the RasterPixel class.
        /// </summary>
        /// <param name="value">Default value of the pixel</param>
        /// <param name="position">Position of the pixel</param>
        public Pixel(double value, Position position)
        {
            this.value = value;
            this.position = position;
        }

        /// <summary>
        /// Initialize a new instance of the RasterPixel class.
        /// </summary>
        /// <param name="position">Position of the pixel</param>
        public Pixel(Position position)
        {
            this.position = position;
        }

        /// <summary>
        /// Initialize a new instance of the RasterCell class.
        /// </summary>
        public Pixel()
        { }

        #region Attributes

        private double value = -1;

        private double newValue = -1;

        private IElement graphicElement = null;

        private Position position = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get the original value.
        /// </summary>
        public double Value
        {
            get { return value; }
        }

        /// <summary>
        /// Get and set the current value.
        /// </summary>
        public double NewValue
        {
            get { return newValue; }
            set { newValue = value; }
        }

        /// <summary>
        /// Get and set the graphic element of edition. 
        /// </summary>
        public IElement GraphicElement
        {
            get { return graphicElement; }
            set { graphicElement = value; }
        }

        /// <summary>
        /// Get the position of the cell.
        /// </summary>
        public Position Position
        {
            get { return position; }
        }

        #endregion
    }
}
