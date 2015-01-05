using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;

namespace RasterEditor
{
    /// <summary>
    /// Provide access to values of a raster cell.
    /// </summary>
    public class RasterCell
    {
        /// <summary>
        /// Initialize a new instance of the RasterCell class.
        /// </summary>
        /// <param name="defaultValue">The original value of the cell.</param>
        public RasterCell(double defaultValue, RasterPos position)
        {
            this.defaultValue = defaultValue;
            this.position = position;
        }

        /// <summary>
        /// Initialize a new instance of the RasterCell class.
        /// </summary>
        public RasterCell()
        { }

        #region Attributes

        private double defaultValue = -1;

        private double newValue = -1;

        private IElement graphicElement = null;

        private RasterPos position = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get the original value.
        /// </summary>
        public double DefaultValue
        {
            get { return defaultValue; }
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
        public RasterPos Position
        {
            get { return position; }
        }

        #endregion
    }
}
