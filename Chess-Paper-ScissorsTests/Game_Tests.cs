using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess_Paper_Scissors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Desktop;
using GameObjects;
using GameObjects.Factory;
using GameObjects.Functional;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
namespace Chess_Paper_Scissors.Tests
{
    [TestClass()]
    public class Game_Tests
    {

        [TestMethod()]
        [DataRow(0,0,0,false,1,1,1,true,1)]
        public void ImpactTest(int piece1Index, int x1, int y1, bool col1, int piece2Index, int x2, int y2, bool col2, int expected)
        {
            List<PieceFactory> pieceFactories = [new KingFactory(), new RockFactory(), new PaperFactory(), new ScissorFactory()];
            Piece piece1 = pieceFactories[piece1Index].Create(x1,y1,col1,false);
            Piece piece2 = pieceFactories[piece2Index].Create(x2,y2,col2,false);
            GameLogic.Turn = piece1.Color;
            Point[] avalPts = GameLogic.GetAvailablePoints(piece1);
            int result;
            if (Array.Exists(avalPts, element => element == piece2.CellPosition))
            {
                result = piece1.IsHigher(piece2);
            }
            else
                result = 0;
            Assert.AreEqual(expected, result);

        }
        [TestMethod()]
        public void MovePieceTest()
        {
            
        }
    }
}