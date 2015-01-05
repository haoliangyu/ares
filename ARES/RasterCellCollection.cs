using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RasterEditor
{                    
    /// <summary>
    /// Provide access to functions for a raster cell collection.
    /// </summary>
    public class RasterCellCollection
    {
        #region Construction Method

        /// <summary>
        /// Initializes an empty instance of RasterCellCollection class.
        /// </summary>
        public RasterCellCollection()
        {
            cellCollection = new List<RasterCell>();    
        }
        
        /// <summary>
        /// Initializes an instance of RasterCellCollection class with an existing list of raster cells. 
        /// </summary>
        /// <param name="rasterCells"></param>
        public RasterCellCollection(List<RasterCell> rasterCells)
        {
            cellCollection = rasterCells;
        }

        #endregion

        #region Attributes

        private List<RasterCell> cellCollection = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get the count of cells in the collection.
        /// </summary>
        public int Count
        {
            get { return cellCollection.Count; }
        }

        /// <summary>
        /// Get the cell with its index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public RasterCell this[int index]
        {
            get { return cellCollection[index]; }
        }

        /// <summary>
        /// Get the cell with the input position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public RasterCell this[RasterPos position]
        {
            get { return this[position.Column, position.Row]; }    
        }

        /// <summary>
        /// Get the cell with input column and row index
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public RasterCell this[int column, int row]
        {
            get { return cellCollection.Find(item => (item.Position.Row == row) && (item.Position.Column == column)); }
        }

        #endregion
                  
        #region Public Methods

        /// <summary>
        /// Find all cells in the collection whose position are within the specified envelop.
        /// </summary>
        /// <param name="tlCorner"></param>
        /// <param name="brCorner"></param>
        /// <returns></returns>
        public RasterCellCollection WithIn(RasterPos tlCorner, RasterPos brCorner)
        {
            return WithIn(tlCorner.Column, brCorner.Column, tlCorner.Row, brCorner.Row);
        }

        /// <summary>
        /// Find all cells in the collection whose position are within the specified envelop.
        /// </summary>
        /// <param name="minCol"></param>
        /// <param name="maxCol"></param>
        /// <param name="minRow"></param>
        /// <param name="maxRow"></param>
        /// <returns></returns>
        public RasterCellCollection WithIn(int minCol, int maxCol, int minRow, int maxRow)
        {
            return new RasterCellCollection(cellCollection.FindAll(cell => cell.Position.Column >= minCol &&
                                                                           cell.Position.Column <= maxCol &&
                                                                           cell.Position.Row >= minRow &&
                                                                           cell.Position.Row <= maxRow));
        }

        /// <summary>
        /// Searches for a cell that matches the certain condition. If not found, it returns null.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public RasterCell Find(Predicate<RasterCell> match)
        {
            return cellCollection.Find(match);
        }

        /// <summary>
        /// Searches for a cell that locates at the input position in the collection and returns its index. If nothing is found, it returns -1.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int IndexOf(RasterPos position)
        {
            return IndexOf(position.Column, position.Row);       
        }

        /// <summary>
        /// Searches for a cell that locates at the input position in the collection and returns its index. If nothing is found, it returns -1.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public int IndexOf(int column, int row)
        {
            return cellCollection.FindIndex(item => (item.Position.Row == row) && (item.Position.Column == column));
        }

        /// <summary>
        /// Determine whether the collection contains elements with the specified position. 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Exists(RasterPos position)
        {
            return this.Exists(position.Column, position.Row);
        }

        /// <summary>
        /// etermine whether the collection contains elements with the specified column and row index.
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool Exists(int columnIndex, int rowIndex)
        {
            return cellCollection.Exists(item => (item.Position.Row == rowIndex) && (item.Position.Column == columnIndex));    
        }

        /// <summary>
        /// Add a new raster cell in the collection.
        /// </summary>      
        /// <param name="cell"></param>
        /// <returns>The collection index of added cell.</returns>
        public int Add(RasterCell cell)
        {
            int index = cellCollection.FindIndex(item => (item.Position.Row == cell.Position.Row) && (item.Position.Column == cell.Position.Column));

            if (index == -1)
            {
                cellCollection.Add(cell);
                return cellCollection.IndexOf(cell);
            }
            else
            {
                RasterCell item = cellCollection[index];
                item.GraphicElement = cell.GraphicElement;
                item.NewValue = cell.NewValue;

                return index;
            }
        }

        /// <summary>
        /// Remove the input cell.
        /// </summary>
        /// <param name="cell"></param>
        public void Remove(RasterCell cell)
        {
            cellCollection.Remove(cell);
        }

        /// <summary>
        /// Remove the cell at the specific position.
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveAt(RasterPos pos)
        {
            RemoteAt(pos.Column, pos.Row);
        }

        /// <summary>
        /// Remove the cell at the specific position.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void RemoteAt(int column, int row)
        {
            cellCollection.RemoveAll(item => (item.Position.Row == row) && (item.Position.Column == column));
        }

        /// <summary>
        /// Clear all cells in the collection.
        /// </summary>
        public void Clear()
        {
            cellCollection.Clear();
        }

        #endregion
    }
}
