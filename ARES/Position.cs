using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARES
{      
    /// <summary>
    /// Provide access to the members that define a positio on a raster.
    /// </summary>
    public class Position
    {
        #region Construction Method

        /// <summary>
        /// Initialize a Position class.
        /// </summary>
        public Position()
        { }

        /// <summary>
        /// Initialize a Position class and specify indexes of row and column.
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="col">Column index</param>
        public Position(int col, int row)
        {
            this.Row = row;
            this.Column = col;
        }

        #endregion

        #region Attributes

        private int row = 0;

        private int col = 0;

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
        /// Indicate whether the position is identical to the given one.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool Equals(Position pos)
        {
            return (pos.Column == this.col) && (pos.Row == this.row);
        }

        /// <summary>
        /// Indicate whether the position is within the given extent.
        /// </summary>
        /// <param name="tlCorner">Position of top-left corner.</param>
        /// <param name="brCorner">Position of buttom-right corner.</param>
        /// <returns></returns>
        public bool Within(Position tlCorner, Position brCorner)
        {
            return Within(tlCorner.Column, tlCorner.Row, brCorner.Column, brCorner.Row); 
        }

        /// <summary>
        /// Indicate whether the position is within the given extent.
        /// </summary>
        /// <param name="maxCol"></param>
        /// <param name="maxRow"></param>
        /// <returns></returns>
        public bool Within(int minCol, int minRow, int maxCol, int maxRow)
        {
            return col >= 0 && row >= 0 && col <= maxCol && row <= maxRow;     
        }

        /// <summary>
        /// Adjust the position to satisfy the given extent.
        /// </summary>
        /// <param name="maxExtent"></param>
        public void Adjust(Position tlCorner, Position brCorner)
        {
            Adjust(tlCorner.Column, tlCorner.Row, brCorner.Column, brCorner.Row);
        }

        /// <summary>
        /// Validate the position using the origin and the maximum extent.
        /// </summary>
        /// <param name="maxRow">Maximum index of row</param>
        /// <param name="maxCol">Maximum index of column</param>
        public void Adjust(int minCol, int minRow, int maxCol, int maxRow)
        {
            if (this.col > maxCol)
                this.col = maxCol;

            if (this.col < minCol)
                this.col = minCol;

            if (this.row > maxRow)
                this.row = maxRow;

            if (this.row < minRow)
                this.row = minRow;
        }

        #endregion
    }
}
