using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TicTacToeLib
{
    public class TicTacToeBoard
    {
        public enum CellValue { BLANK, X, O };

        private CellValue[,] Board = new CellValue[3, 3];

        private CellValue CurrentPlayer = CellValue.X;

        public CellValue ValueAt(int row, int col)
        {
            return Board[row, col];
        }

        public bool MakeMoveInCell(int position) //MakeMoveInCell
        {
            int row, col;
            ConvertPositionToRowCol(position, out row, out col);

            if (ValueAt(row, col) != CellValue.BLANK)
            {
                return false;
            }

            Board[row, col] = CellValue.X;

            return true;

        }

        public void ConvertPositionToRowCol(int pos, out int row, out int col)
        {
            switch (pos)
            {
                case 1:
                    row = 0;
                    col = 0;
                    break;
                case 2:
                    row = 0;
                    col = 1;
                    break;
                case 3:
                    row = 0;
                    col = 2;
                    break;
                case 4:
                    row = 1;
                    col = 0;
                    break;
                case 5:
                    row = 1;
                    col = 1;
                    break;
                case 6:
                    row = 1;
                    col = 2;
                    break;
                case 7:
                    row = 2;
                    col = 0;
                    break;
                case 8:
                    row = 2;
                    col = 1;
                    break;
                case 9:
                    row = 2;
                    col = 2;
                    break;
                default:
                    throw new ArgumentException("Invalid Argument to ConvertPosition");
            }
        }

        public CellValue WinnerCheck()
        {
            for (int row = 0; row < 3; row++)
            {
                if( Board[row, 0] == Board[row, 1] &&
                    Board[row, 1] == Board[row, 2] &&
                    Board[row, 0] != CellValue.BLANK)
                {
                    return Board[row, 0];
                }
            }

            // Check Vertical
                         for (int col = 0; col < 3; col++)
            {
                if (Board[0, col] == Board[1, col] &&
                    Board[1, col] == Board[2, col] &&
                    Board[0, col] != CellValue.BLANK)
                {
                    return Board[0, col];
                }
            }

            ///Check Diagonals
             
            if( Board[0,0] == Board[1,1] &&
                Board[1,1] == Board[2,2] &&
                Board[0, 0] != CellValue.BLANK)
            {
                return Board[0, 0];
            }

            if (Board[2, 0] == Board[1, 1] &&
                Board[1, 1] == Board[0, 2] &&
                Board[1, 1] != CellValue.BLANK)
            {
                return Board[1, 1];
            }

            return CellValue.BLANK;
        }

        public bool DoAIMove() //DoAIMove
        {
            int[] bestMove = MiniMax(2, CellValue.O, int.MinValue, int.MaxValue);

            if (bestMove[1] < 0 || bestMove[2] < 0)
            {
                return false;
            }

            Board[bestMove[1], bestMove[2]] = CellValue.O;
            return true;
        }

        private int[] MiniMax(int depth, CellValue player, int alpha, int beta)
        {
            var nextMoves = GenerateAvailableMoves();

            int score;
            int bestRow = -1;
            int bestCol = -1;

            if (nextMoves.Count == 0 || depth == 0)
            {
                //Game over or depth reached.
                score = Evaluate();
                return new int[] { score, bestRow, bestCol};
            }
           
            foreach(int move in nextMoves) 
            {
                int row, col;
                ConvertPositionToRowCol(move, out row, out col);

                //move for current "player"
                Board[row, col] = player;

                if (player == CellValue.O) //Maximizing player
                {
                    score = MiniMax(depth - 1, CellValue.X, alpha, beta)[0];
                    if (score > alpha)
                    {
                        alpha = score;
                        bestRow = row;
                        bestCol = col;
                    }
                }
                else //Minimizing Player
                {
                    score = MiniMax(depth - 1, CellValue.O, alpha, beta)[0];
                    if (score < beta)
                    {
                        beta = score;
                        bestRow = row;
                        bestCol = col;
                    }
                }

                Board[row, col] = CellValue.BLANK;

                //Alpha Beta cut-off
                if (alpha >= beta) break;
            }

            return new int[] {player == CellValue.O ? alpha : beta, bestRow, bestCol};
        }

        public List<int> GenerateAvailableMoves()
        {
            var nextMoves = new List<int>();

            if (WinnerCheck() != CellValue.BLANK) return nextMoves;

            for (int position = 1; position <= 9; position++)
            {
                int row, col;
                ConvertPositionToRowCol(position, out row, out col);

                if (Board[row, col] == CellValue.BLANK) nextMoves.Add(position);
            }

            return nextMoves;
        }

        private int Evaluate()
        {
            int score = 0;
            score += EvaluateLine(0, 0, 0, 1, 0, 2);
            score += EvaluateLine(1, 0, 1, 1, 1, 2);
            score += EvaluateLine(2, 0, 2, 1, 2, 2);
            score += EvaluateLine(0, 0, 1, 0, 2, 0);
            score += EvaluateLine(0, 1, 1, 1, 2, 1);
            score += EvaluateLine(0, 2, 1, 2, 2, 2);
            score += EvaluateLine(0, 0, 1, 1, 2, 2);
            score += EvaluateLine(0, 2, 1, 1, 2, 0);
            return score;


        }

        private int EvaluateLine(int row1, int col1, int row2, int col2, int row3, int col3)
        {
            int score = 0;

            //First Cell
            if (Board[row1, col1] == CellValue.O)
            {
                score = 1;
            }
            else if (Board[row1, col1] == CellValue.X)
            {
                score = -1;
            }

            //Second Cell
            if (Board[row2, col2] == CellValue.O)
            {
                if (score == 1) score = 10;
                else if (score == -1) return 0;
                else score = 1;
            }
            else if (Board[row2, col2] == CellValue.X)
            {
                if (score == -1) score = -10;
                else if (score == 1) return 0;
                else score = -1;
            }

            //Third Cell
            if (Board[row3, col3] == CellValue.O)
            {
                if (score > 0) score *= 10;
                else if (score < 0) return 0;
                else score = 1;
            }
            else if (Board[row3, col3] == CellValue.X)
            {
                if (score < 0) score *= 10;
                else if (score > 1) return 0;
                else score = -1;
            }

            return score;
        }

        public String printGrid()
        {
            String grid = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i,j].ToString() != "BLANK")
                    {
                        if (grid == null)
                            grid = Board[i, j].ToString() + "[" + i + "," + j + "]";
                        else
                            grid += " , " + Board[i, j].ToString() + "[" + i + "," + j + "]";
                    }
                    else
                    {
                        if (grid == null)
                            grid = Board[i, j].ToString();
                        else
                            grid += " , " + Board[i, j].ToString();
                    }
                    
                }
            }
            return grid;
        }
    }
}
