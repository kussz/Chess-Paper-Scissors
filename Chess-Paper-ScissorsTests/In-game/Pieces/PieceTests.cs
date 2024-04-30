using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects.Factory;
using System.Drawing;
using System.Windows.Media;

namespace GameObjects.Tests
{
    [TestClass()]
    public class PieceTests
    {
        [TestMethod()]
        [DataRow(0, 0, 0, false)]
        [DataRow(3, 0, 1, true)]
        [DataRow(2, 0, 3, true)]
        [DataRow(1, 3, 7, false)]
        [DataRow(0, 2, 4, false)]
        public void PieceTest(int pieceIndex, int x, int y, bool color)
        {
            List<PieceFactory> pieceFactories = [new KingFactory(), new RockFactory(), new PaperFactory(), new ScissorFactory()];
            Piece piece1 = pieceFactories[pieceIndex].Create(x, y, color, false);
            Assert.AreEqual(color, piece1.Color);
            Assert.AreEqual(new Point(x,y), piece1.CellPosition);
        }

        [TestMethod()]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(0, 0, 0)]
        [DataRow(1, 2, -1)]
        [DataRow(2, 1, 1)]
        [DataRow(3, 2, 1)]
        [DataRow(2, 3, -1)]
        [DataRow(1, 3, 1)]
        [DataRow(3, 1, -1)]
        public void IsHigherTest(int p1, int p2, int expected)
        {
            List<PieceFactory> pieceFactories = [new KingFactory(), new RockFactory(), new PaperFactory(), new ScissorFactory()];
            Piece piece1 = pieceFactories[p1].Create(0, 0, false, false);
            Piece piece2 = pieceFactories[p2].Create(0, 0, false, false);
            Assert.AreEqual(expected, piece1.IsHigher(piece2));
        }
    }
}