using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RasterEditor
{      
    /// <summary>
    /// Provide access to the members that control a positio on a raster.
    /// </summary>
    public class RasterPos
    {           
        #region Construction Methods

        /// <summary>
        /// Initialize a RasterPos class.
        /// </summary>
        public RasterPos()
        { }

        /// <summary>
        /// Initialize a RasterPos class and specify indexes of row and column.
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        public RasterPos(int col, int row)
        {
            this.Row = row;
            this.Column = col;
        }

        #endregion

        #region Attributes

        private int row = -1;
        private int col = -1;

        #endregion
                                  
        #region Properties

        /// <summary>
        /// The row index of the position
        /// </summary>
        public int Row
        {
            get { return row; }
            set 
            {
                row = value; 
            }
        }

        /// <summary>
        /// The column index of the position
        /// </summary>
        public int Column
        {
            get { return col; }
            set 
            {
                col = value; 
            }
        }

        #endregion      

        #region Methods

        /// <summary>
        /// Examine wether the position is within the extent specified by the origin and the given buttom right corner.
        /// </summary>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public bool Within(RasterPos maxIndex)
        {
            return Within(maxIndex.Column, maxIndex.Row);
        }

        /// <summary>
        /// Examine wether the position is within the extent specified by the origin and the given buttom right corner.
        /// </summary>
        /// <param name="maxCol"></param>
        /// <param name="maxRow"></param>
        /// <returns></returns>
        public bool Within(int maxCol, int maxRow)
        {
            return col >= 0 && row >= 0 && col <= maxCol && row <= maxRow;
        }

        /// <summary>
        /// Validate the position using the origin and the maximum extent.
        /// </summary>
        /// <param name="maxExtent"></param>
        public void Validate(RasterPos maxExtent)
        {
            Validate(maxExtent.Column, maxExtent.Row);
        }

        /// <summary>
        /// Validate the position using the origin and the maximum extent.
        /// </summary>
        /// <param name="maxRow">Maximum index of row</param>
        /// <param name="maxCol">Maximum index of column</param>
        public void Validate(int maxCol, int maxRow)
        {
            if (this.col > maxCol)
                this.col = maxCol;

            if (this.col < 0)
                this.col = 0;

            if (this.row > maxRow)
                this.row = maxRow;

            if (this.row < 0)
                this.row = 0;
        }

        #endregion
    }
}
