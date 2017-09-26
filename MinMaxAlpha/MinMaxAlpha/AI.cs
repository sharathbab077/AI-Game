using System.Collections.Generic;

namespace MinMaxAlpha
{
  class AI
  {
    /// <summary>
    /// minimax algorithm.   
    /// </summary>
    public static Space BestMove(GameBoard gb, Player p)
    {
      Space? bestMove = null;
      List<Space> openSpaceList = gb.SquaresAvailable;
      GameBoard newBoard;

      for (int i = 0; i < openSpaceList.Count; i++)
      {
        newBoard = gb.Clone();
        Space newSpace = openSpaceList[i];

        newBoard[newSpace.X, newSpace.Y] = p;

        if (newBoard.Winner == Player.Open && newBoard.SquaresAvailable.Count > 0)
        {
          Space tempMove = BestMove(newBoard, ((Player)(-(int)p)));  
          newSpace.Rank = tempMove.Rank;
        }
        else
        {
          if (newBoard.Winner == Player.Open)
            newSpace.Rank = 0;
          else if (newBoard.Winner == Player.X)
            newSpace.Rank = -1;
          else if (newBoard.Winner == Player.O)
            newSpace.Rank = 1;
        }
        if (bestMove == null ||
           (p == Player.X && newSpace.Rank < ((Space)bestMove).Rank) ||
           (p == Player.O && newSpace.Rank > ((Space)bestMove).Rank))
        {
          bestMove = newSpace;
        }
      }

      return (Space)bestMove;
    }
  }
}