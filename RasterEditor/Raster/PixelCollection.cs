using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RasterEditor.Raster
{                    
    /// <summary>
    /// Provide access to functions for a raster cell collection.
    /// </summary>
    public class PixelCollection
    {
        #region Construction Method

        /// <summary>
        /// Initializes an empty instance of RasterCellCollection class.
        /// </summary>
        public PixelCollection()
        {
            pixelCollection = new List<Pixel>();    
        }
        
        /// <summary>
        /// Initializes an instance of RasterCellCollection class with an existing list of raster cells. 
        /// </summary>
        /// <param name="rasterCells"></param>
        public PixelCollection(List<Pixel> rasterCells)
        {
            pixelCollection = rasterCells;
        }

        #endregion

        #region Attributes

        private List<Pixel> pixelCollection = null;

        #endregion

        #region Properties

        /// <summary>
        /// Get the count of cells in the collection.
        /// </summary>
        public int Count
        {
            get { return pixelCollection.Count; }
        }

        /// <summary>
        /// Get the cell with its index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Pixel this[int index]
        {
            get { return pixelCollection[index]; }
        }

        /// <summary>
        /// Get the cell with the input position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Pixel this[Position position]
        {
            get { return this[position.Column, position.Row]; }    
        }

        /// <summary>
        /// Get the cell with input column and row index
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public Pixel this[int column, int row]
        {
            get { return pixelCollection.Find(item => (item.Position.Row == row) && (item.Position.Column == column)); }
        }

        #endregion
                  
        #region Public Methods

        /// <summary>
        /// Find all cells in the collection whose position are within the specified envelop.
        /// </summary>
        /// <param name="tlCorner"></param>
        /// <param name="brCorner"></param>
        /// <returns></returns>
        public PixelCollection WithIn(Position tlCorner, Position brCorner)
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
        public PixelCollection WithIn(int minCol, int maxCol, int minRow, int maxRow)
        {
            return new PixelCollection(pixelCollection.FindAll(cell => cell.Position.Column >= minCol &&
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
        public Pixel Find(Predicate<Pixel> match)
        {
            return pixelCollection.Find(match);
        }

        /// <summary>
        /// Searches for a cell that locates at the input position in the collection and returns its index. If nothing is found, it returns -1.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int IndexOf(Position position)
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
            return pixelCollection.FindIndex(item => (item.Position.Row == row) && (item.Position.Column == column));
        }

        /// <summary>
        /// Determine whether the collection contains elements with the specified position. 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Exists(Position position)
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
            //return pixelCollection.Exists(item => item == null); 
            return pixelCollection.Exists(item => (item.Position.Row == rowIndex) && (item.Position.Column == columnIndex));    
        }

        /// <summary>
        /// Add a new raster cell in the collection.
        /// </summary>      
        /// <param name="pixel"></param>
        /// <returns>The collection index of added cell.</returns>
        public int Add(Pixel pixel)
        {
            int index = pixelCollection.FindIndex(item => (item.Position.Row == pixel.Position.Row) && (item.Position.Column == pixel.Position.Column));

            if (index == -1)
            {
                pixelCollection.Add(pixel);
                return pixelCollection.IndexOf(pixel);
            }
            else
            {
                Pixel item = pixelCollection[index];
                item.GraphicElement = pixel.GraphicElement;
                item.NewValue = pixel.NewValue;

                return index;
            }
        }

        /// <summary>
        /// Remove the pixel with input position.
        /// </summary>
        /// <param name="pos"></param>
        public void Remove(Position pos)
        {
            Pixel pixel = this[pos];

            if (pixel != null)
            {
                pixelCollection.Remove(pixel);
            }
        }

        /// <summary>
        /// Remove the input pixel.
        /// </summary>
        /// <param name="pixel"></param>
        public void Remove(Pixel pixel)
        {
            pixelCollection.Remove(pixel);
        }

        /// <summary>
        /// Remove the cell at the specific position.
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveAt(Position pos)
        {
            RemoveAt(pos.Column, pos.Row);
        }

        /// <summary>
        /// Remove the cell at the specific position.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void RemoveAt(int column, int row)
        {
            pixelCollection.RemoveAll(item => (item.Position.Row == row) && (item.Position.Column == column));
        }

        /// <summary>
        /// Clear all cells in the collection.
        /// </summary>
        public void Clear()
        {
            pixelCollection.Clear();
        }

        #endregion
    }
}
