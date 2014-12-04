using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Please enter board size (3-9)

            // Do you want to play agains another player or against computer


            GameWorld world = new GameWorld(5);
            world.Print();
            System.Console.WriteLine("Done");
        }
    }

    class GameWorld
    {

        private int[,] m_board;
        private int m_dimension;

        public GameWorld(int i_dimension)
        {
            m_dimension = i_dimension;
            m_board = new int[i_dimension, i_dimension];
            m_board[2 - 1, 1 - 1] = 1;

            m_board[3 - 1, 3 - 1] = 2;
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
                sb.Append("|").Append(getCellValue(i_lineIndex, i));
            }

            return sb.ToString();

        }

        private string getCellValue(int i_lineIndex, int i_columnIndex)
        {
            string value = " ";
            int cellContent = m_board[i_lineIndex, i_columnIndex];
            switch (cellContent) 
            {
                 case 1:
                    value = "X";
                    break;
                case 2:
                    value = "O";
                    break;
            }

            return value;
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
}
