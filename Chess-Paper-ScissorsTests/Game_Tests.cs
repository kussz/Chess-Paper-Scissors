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
        [DataRow(0,0,false)]
        [DataRow(3,0,true)]
        [DataRow(2,0,true)]
        [DataRow(1,3,false)]
        [DataRow(0,2,false)]
        public void ImpactTest(int piece1Index,int piece2Index, bool expected)
        {
            List<PieceFactory> pieceFactories = [new KingFactory(), new RockFactory(), new PaperFactory(), new ScissorFactory()];
            Piece piece1 = pieceFactories[piece1Index].Create(0,0,false,false);
            Piece piece2 = pieceFactories[piece2Index].Create(1,1,true,false);
            GameLogic.SelectedPiece = piece1;
            bool result = GameLogic.ImpactSelectedPieceWith(piece2);
            Assert.AreEqual(expected, result);

        }
        [TestMethod()]
        [DataRow(0,0,0)]
        [DataRow(1,1,1)]
        [DataRow(2,2,2)]
        [DataRow(3,3,3)]
        [DataRow(0,4,2)]
        [DataRow(1,5,7)]
        public void MovePieceTest(int pieceIndex, int x, int y)
        {
            List<PieceFactory> pieceFactories = [new KingFactory(), new RockFactory(), new PaperFactory(), new ScissorFactory()];
            Piece piece1 = pieceFactories[pieceIndex].Create(0, 0, false, false);
            GameLogic.SelectedPiece = piece1;
            GameLogic.MoveSelectedPieceTo(new Point(x, y));
            Assert.AreEqual(piece1.CellPosition, new Point(x, y));
        }
    }
}