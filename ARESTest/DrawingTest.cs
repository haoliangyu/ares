using System;
using System.Text;          
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ARES;

namespace ARESTest
{
    [TestClass]
    public class DrawingTest
    {
        #region Test Method

        [TestMethod]
        public void DrawLine()
        {
            Position start = new Position();
            Position end = new Position();

            // Horizontal Line
            start.Column = end.Column = 3;
            start.Row = 3;
            end.Row = 9;
            
            Console.WriteLine("Horizontal Line");
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 100, 0, 100)));

            // Vertical Line
            start.Row = end.Row = 3;
            start.Column = 3;
            end.Column = 9;

            Console.WriteLine("Vertical Line");
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 100, 0, 100)));

            // k > 0
            start.Row = 8;
            end.Row = 3;
            start.Column = 12;
            end.Column = 8;

            Console.WriteLine("k >０");
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 100, 0, 100)));

            // k < 0
            start.Row = 8;
            end.Row = 12;
            start.Column = 12;
            end.Column = 7;

            Console.WriteLine("k >０");
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 100, 0, 100)));
        }

        #endregion

        #region Private Method

        private void PrintPositions(IEnumerable positions)
        {
            foreach (Position pos in positions)
            {
                Console.WriteLine(string.Format("({0}, {1})", pos.Row, pos.Column));
            }
        }

        #endregion
    }
}
