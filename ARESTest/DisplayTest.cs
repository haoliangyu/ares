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
    public class DisplayTest
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
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 5, 0, 5)));

            // Vertical Line
            start.Row = end.Row = 3;
            start.Column = 3;
            end.Column = 9;

            Console.WriteLine("Vertical Line");
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 5, 0, 5)));

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

            // outside box
            start.Row = 0;
            end.Row = 5;
            start.Column = 0;
            end.Column = 5;

            Console.WriteLine("outside box");
            PrintPositions(ARES.Display.GetPolyline(start, end, new ARES.Envelope(0, 3, 0, 3)));  
        }

        //[TestMethod]
        public void DrawPolygon()
        {
            Console.Write("draw polygon");
            PrintPositions(Display.GetPolygon(new Position[]{new Position(5, 5),
                                                             new Position(5, 15),
                                                             new Position(15, 15),
                                                             new Position(15, 5)},
                                              new Envelope(0, 10, 0, 10)));
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
