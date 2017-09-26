using System.Collections.Generic;

namespace MinMaxAlpha
{
  public enum Player
  {
    X = 1,
    O = -1,
    Open = 0,
  }

  
  public abstract class GameBoard
  {
    
    public Player[,] squares; //container for board spaces

    public abstract Player this[int x, int y] { get; set; }

    public abstract bool IsFull { get; }
    public abstract int BoardSize { get; }
    public abstract List<Space> SquaresAvailable { get; }
    public abstract Player Winner { get; }
   
    public abstract GameBoard Clone(); //deep copy
  }

  
  public struct Space //boaard space
  {
    public int X;
    public int Y;
    public double Rank;

    public Space(int x, int y)
    {
      this.X = x;
      this.Y = y;
      Rank = 0;
    }
  }
}
