using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARES
{                                                                   
    /// <summary>
    /// Provides access to members that define a region on a raster layer.
    /// </summary>
    class Envelope
    {
        #region Attributes

        int minRow = 0;

        int maxRow = 0;

        int minCol = 0;

        int maxCol = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Get the minimal row index of the envelope.
        /// </summary>
        public int MinRow
        {
            get { return minRow; }
        }

        /// <summary>
        /// Get the maximum row index of the envelope.
        /// </summary>
        public int MaxRow
        {
            get { return maxRow; }
        }

        /// <summary>
        /// Get the minimal column index of the envelope.
        /// </summary>
        public int MinColumn
        {
            get { return minCol; }
        }

        /// <summary>
        /// Get the maxmum column index of the envelope.
        /// </summary>
        public int MaxColumn 
        {
            get { return maxCol; }
        }

        /// <summary>
        /// Get the top-left corner of the envelope.
        /// </summary>
        public Position TLCorner
        {
            get { return new Position(minCol, minRow); }
        }

        /// <summary>
        /// Get the bottom-right corner of the envelope.
        /// </summary>
        public Position BRCorner
        {
            get { return new Position(maxCol, maxRow); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new evelope class.
        /// </summary>
        /// <param name="minRow">Minimal row index</param>
        /// <param name="minCol">Minimal colmun index</param>
        /// <param name="maxRow">Maxmum row index</param>
        /// <param name="maxCol">Maxmum column index</param>
        public Envelope(int minRow, int maxRow, int minCol, int maxCol)
        {
            SetExtent(minRow, maxRow, minCol, maxCol);    
        }

        /// <summary>
        /// Initialize a new evelope class.
        /// </summary>
        /// <param name="tlCorner">Top-left corner of the envelope.</param>
        /// <param name="brCorner">Bottom-right corner of the envelope.</param>
        public Envelope(Position tlCorner, Position brCorner)
        {
            SetExtent(tlCorner, brCorner);    
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the extent of the envelope.
        /// </summary>
        /// <param name="minRow">Minimal row index</param>
        /// <param name="minCol">Minimal colmun index</param>
        /// <param name="maxRow">Maxmum row index</param>
        /// <param name="maxCol">Maxmum column index</param>
        public void SetExtent(int minRow, int maxRow, int minCol, int maxCol)
        {
            if ((minRow <= maxRow) && (minCol <= maxCol))
            {
                this.minRow = minRow;
                this.maxRow = maxRow;
                this.minCol = minCol;
                this.maxCol = maxCol;
            }
            else
            {
                throw new ArgumentException("Input row or column index is problematic.");
            }
        }

        /// <summary>
        /// Set the extent of the envelope.
        /// </summary>
        /// <param name="tlCorner">Top-left corner of the envelope.</param>
        /// <param name="brCorner">Bottom-right corner of the envelope.</param>
        public void SetExtent(Position tlCorner, Position brCorner)
        {
            SetExtent(tlCorner.Row, brCorner.Row, tlCorner.Column, brCorner.Column);
        }

        /// <summary>
        /// Indicates whether the given row and column index is within the envelope.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public bool Contains(int row, int col)
        {
            return ((row >= minRow) && (row <= maxRow) && (col <= maxCol) && (col >= minCol));
        }

        /// <summary>
        /// Indicates whether the given position is within the envelope.
        /// </summary>                   
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Contains(Position position)
        {
            return Contains(position.Row, position.Column); 
        }

        #endregion
    }
}
