using System.Collections.Generic;

namespace MinMaxAlpha
{
  
  class TicTacToeBoard : GameBoard
  {
    public TicTacToeBoard()
    {
      squares = new Player[3, 3] { 
          {0,0,0}, 
          {0,0,0},
          {0,0,0}
      };
    }

    public override int BoardSize { get { return 9; } }

    public override Player this[int x, int y]
    {
      get
      {
        return squares[x, y];
      }

      set
      {
        squares[x, y] = value;
      }
    }

    public override bool IsFull
    {
      get
      {
        foreach (Player i in squares)
          if (i == Player.Open) { return false; }
        return true;
      }
    }

    public override List<Space> SquaresAvailable
    {
      get
      {
        List<Space> openSquares = new List<Space>();

        for (int x = 0; x <= 2; x++)
          for (int y = 0; y <= 2; y++)
            if (squares[x, y] == Player.Open)
              openSquares.Add(new Space(x, y));

        return openSquares;
      }
    }

    public override Player Winner
    {
      get
      {
        int ctr = 0;

        //col
        for (int x = 0; x < 3; x++)
        {
          ctr = 0;

          for (int y = 0; y < 3; y++)
            ctr += (int)squares[x, y];

          if (ctr == 3)
            return Player.X;
          if (ctr == -3)
            return Player.O;
        }

        //rows
        for (int x = 0; x < 3; x++)
        {
          ctr = 0;

          for (int y = 0; y < 3; y++)
            ctr += (int)squares[y, x];

          if (ctr == 3)
            return Player.X;
          if (ctr == -3)
            return Player.O;
        }

        // right to left
        ctr = 0;
        ctr += (int)squares[0, 0];
        ctr += (int)squares[1, 1];
        ctr += (int)squares[2, 2];
        if (ctr == 3)
          return Player.X;
        if (ctr == -3)
          return Player.O;

        //left to right
        ctr = 0;
        ctr += (int)squares[0, 2];
        ctr += (int)squares[1, 1];
        ctr += (int)squares[2, 0];
        if (ctr == 3)
          return Player.X;
        if (ctr == -3)
          return Player.O;

        return Player.Open;
      }
    }

    public override GameBoard Clone()
    {
      GameBoard b = new TicTacToeBoard();
      b.squares = (Player[,])this.squares.Clone();
      return b;
    }
  }
}
