using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeLib;

using CellValue = TicTacToeLib.TicTacToeBoard.CellValue;


namespace TicTacToeTestLib
{
    [TestFixture]
    public class TicTacToeTest
    {
        private TicTacToeBoard board;

        [SetUp]
        public void Init()
        {
            board = new TicTacToeBoard();
        }

        [Test]
        public void TestCanary()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMakeMove()
        {
            Assert.IsTrue(board.MakeMoveInCell(5));

            Assert.AreEqual(CellValue.X, board.ValueAt(1, 1));
        }

        [Test]
        public void TestMakeInvalidMove()
        {
            board.MakeMoveInCell(5);

            Assert.IsFalse(board.MakeMoveInCell(5));
        }

        [Test]
        public void TestCheckForWinnerX()
        {
            board.MakeMoveInCell(1);
            board.MakeMoveInCell(2);
            board.MakeMoveInCell(3);

            Assert.AreEqual(CellValue.X, board.WinnerCheck());
        }

        [Test]
        public void TestNoWinner() 
        {
            Assert.AreEqual(CellValue.BLANK, board.WinnerCheck());
        }
    }
}
