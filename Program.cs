using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    class Program
    {


        static void Main(string[] args)
        {
            // Please enter board size (3-9)

            // Do you want to play agains another player or against computer


            GameWorld world = new GameWorld(3);
            
     
            // Horizontal
            /*  
             * world.SetCell(1, 1, CellValues.PLAYER_1);
          world.SetCell(1, 2, CellValues.PLAYER_1);
            world.SetCell(1, 3, CellValues.PLAYER_1);
         * /
            // Vertical
             * world.SetCell(1, 1, CellValues.PLAYER_1);
        /*    world.SetCell(2, 1, CellValues.PLAYER_1);
            world.SetCell(3, 1, CellValues.PLAYER_1);
         */

            // Diagonal
            /*
            world.SetCell(1, 1, CellValues.PLAYER_1);
          //  world.SetCell(2, 2, CellValues.PLAYER_1);
           // world.SetCell(3, 3, CellValues.PLAYER_1);
             */

            // Reverse diagonal
            world.SetCell(1, 3, CellValues.PLAYER_1);
            world.SetCell(2, 2, CellValues.PLAYER_1);
            world.SetCell(3, 1, CellValues.PLAYER_1);

            world.Print();
            System.Console.WriteLine("Is game over? " + world.IsGameOver());
            System.Console.WriteLine("Done");
        }
    }

    class GameWorld
    {

        private CellValues[,] m_board;
        private int m_dimension;


        public bool SetCell(int row, int column, CellValues value)
        {
            if (CellValues.EMPTY.Equals(value))
            {
                return false;
            }
            else if (!CellValues.EMPTY.Equals(m_board[row - 1, column - 1]))
            {
                return false;
            }
            m_board[row - 1, column - 1] = value;

            return true;
        }

        public bool IsGameOver() 
        {
            
            // Check vertical
            // Check horizontal
            // Check diagonal left to right
            // Check diagonal right to left
            return isRowFull() || isColumnFull() || isDiagonalFull() || isReverseDiagonalFull();
        }

        private bool isReverseDiagonalFull()
        {

            bool fullDiagonal = true;
            for (int i = 0; i < m_dimension - 1; i++)
            {
                CellValues currentCell = m_board[i, m_dimension - i - 1];
                CellValues nextCell = m_board[i + 1, m_dimension - i - 2];

                if (CellValues.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
                {
                    fullDiagonal = false;
                    break;
                }            
            }

            return fullDiagonal;
        }

        private bool isDiagonalFull()
        {
            bool fullDiagonal = true;
            for (int i = 0; i < m_dimension - 1; i++)
            {
                CellValues currentCell = m_board[i, i];
                CellValues nextCell = m_board[i + 1, i + 1];

                if (CellValues.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
                {
                    fullDiagonal = false;
                    break;
                }
 
            }
            return fullDiagonal;
        }



        private bool isColumnFull()
        {
            bool isGameOver = false;
            for (int i = 0; i < m_dimension; i++)
            {
                bool fullColumn = true;
                for (int j = 0; j < m_dimension - 1; j++)
                {

                    CellValues currentCell = m_board[j, i];
                    CellValues nextCell = m_board[j + 1, i];
                    if (CellValues.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
                    {
                        fullColumn = false;
                        break;
                    }

                }
                if (fullColumn)
                {
                    isGameOver = true;
                    break;
                }
            }
            return isGameOver;
        }

        private bool isRowFull()
        {
            bool isGameOver = false;
            for (int i = 0; i < m_dimension; i++)
            {
                bool fullRow = true;
                for (int j = 0; j < m_dimension - 1; j++)
                {

                    CellValues currentCell = m_board[i, j];
                    CellValues nextCell = m_board[i, j + 1];
                    if (CellValues.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
                    {
                        fullRow = false;
                        break;
                    }

                }
                if (fullRow)
                {
                    isGameOver = true;
                    break;
                }
            }
            return isGameOver;
        }

        public GameWorld(int i_dimension)
        {
            m_dimension = i_dimension;
            m_board = new CellValues[i_dimension, i_dimension];
            for (int i = 0; i < m_dimension; i++)
            {
                for (int j = 0; j < m_dimension; j++)
                {
                    m_board[i, j] = CellValues.EMPTY;
                }
            }
        }

        public void Print()
        {
            write(getHorizontalIndexes());

            for (int i = 0; i < m_dimension; i++)
            {
                write(getLine(i));
                write(getSeparatorLine());
            }
        }

        private string getSeparatorLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_dimension*2+1; i++)
            {
                sb.Append("=");
            }
            return sb.ToString();
        }

        private string getLine(int i_lineIndex)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(i_lineIndex + 1);
            for (int i = 0; i < m_dimension; i++)
            {
                sb.Append("|");
                switch(m_board[i_lineIndex, i])
                {
                    case CellValues.EMPTY:
                        sb.Append(" ");
                        break;
                    case CellValues.PLAYER_1:
                        sb.Append("X");
                        break;
                    case CellValues.PLAYER_2:
                        sb.Append("O");
                        break;
                }
            }

            return sb.ToString();

        }


        private string getHorizontalIndexes() 
        {
            StringBuilder sb = new StringBuilder(" ");

            for (int i = 1; i <= m_dimension; i++) {
                sb.Append(" ").Append(i);
            }
            

            return sb.ToString();
        }

        private void write(string i_stringToWrite)
        {
            System.Console.WriteLine(i_stringToWrite);
        }
    }

    class Player
    {
        public Player(PlayerType i_playerType, string i_name)
        {

        }
    }

    enum PlayerType
    {
        HUMAN,
        COMPUTER
    }

    enum CellValues
    {
        EMPTY,
        PLAYER_1,
        PLAYER_2
    }
}
