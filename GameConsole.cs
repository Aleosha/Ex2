using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    public class GameConsole
    {
        private const int k_MaxBoardSize = 9;
        private const int k_MinBoardSize = 3;
        private const char k_RowSeparator = '=';
        private const char k_ColumnSeparator = '|';
        private const char k_EmptyCellSign = ' ';
        private const int k_EndGameCode = -1;
        private const int k_PlayAgainCode = 1;

        private GameLogic m_GameLogic;

        public GameConsole()
        {
            initializeGame();
            runGame();
        }

        private void runGame()
        {
            bool endGame = false;
            while (!endGame)
            {
                while (!m_GameLogic.IsGameOver())
                {
                    Print();
                    showWhoseTurnItIs();
                    bool wasCellEmpty = false;
                    while (!wasCellEmpty)
                    {
                        if (m_GameLogic.CurrPlayer.PlayerType == ePlayerType.HUMAN)
                        {
                            makeHumanMove(ref wasCellEmpty);
                            if (m_GameLogic.GameTerminationStatus == eGameTerminationStatus.ABANDONED)
                            {
                                break;
                            }
                        }
                        else
                        {
                            makeComputerMove(ref wasCellEmpty);
                        }
                    }

                    m_GameLogic.AlternatePlayers();
                }

                Print();
                printGameEndingStatus();
                showScores();
                endGame = !proposeNewGame();
            }
        }

        private void printGameEndingStatus()
        {
            if (m_GameLogic.GameTerminationStatus == eGameTerminationStatus.TIE)
            {
                System.Console.WriteLine("It's a tie.");
            }
            else if (m_GameLogic.GameTerminationStatus == eGameTerminationStatus.WON)
            {
                System.Console.WriteLine("Player {0} won.", m_GameLogic.CurrPlayer.ToString());
            }
            else if (m_GameLogic.GameTerminationStatus == eGameTerminationStatus.UNFINISHED)
            {
                System.Console.WriteLine("The game has been left unfinished.");
            }
            else
            {
                System.Console.WriteLine("The game has been abandoned.");
            }
        }

        private bool proposeNewGame()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int playAgain;
            System.Console.WriteLine("Would you like to play again?");
            do
            {
                System.Console.WriteLine("Enter 1 to continue or 2 to end:");
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out playAgain);
                if (!numberIsInt || playAgain < 1 || playAgain > 2)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    goodInput = true;
                }
            }
            while (!goodInput);
            if (playAgain == k_PlayAgainCode)
            {
                m_GameLogic.MakeNewRound();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void showScores()
        {
            System.Console.WriteLine("Player {0}'s score: {1}", m_GameLogic.Player1.ToString(), m_GameLogic.Player1.Score);
            System.Console.WriteLine("Player {0}'s score: {1}", m_GameLogic.Player2.ToString(), m_GameLogic.Player2.Score);
        }

        private void makeComputerMove(ref bool io_WasCellEmpty)
        {
            MoveOption bestOption = ComputerPlayerLogic.GetBestOption(m_GameLogic);
            io_WasCellEmpty = m_GameLogic.SetCell(bestOption.Row, bestOption.Column, m_GameLogic.CurrPlayer.CellValue);
            if (!io_WasCellEmpty)
            {
                System.Console.WriteLine("Computer made an illegal move");
            }
        }

        private void makeHumanMove(ref bool io_WasCellEmpty)
        {
            int currRow = getRowFromUser();
            if (currRow == k_EndGameCode)
            {
                m_GameLogic.GameTerminationStatus = eGameTerminationStatus.ABANDONED;
                return;
            }

            int currColumn = getColumnFromUser();
            io_WasCellEmpty = m_GameLogic.SetCell(currRow, currColumn, m_GameLogic.CurrPlayer.CellValue);
            if (!io_WasCellEmpty)
            {
                System.Console.WriteLine("Cell is already full. Choose another cell.");
            }
        }

        public void showWhoseTurnItIs()
        {
            System.Console.WriteLine("It's {0}'s turn", m_GameLogic.CurrPlayer.ToString());
        }

        private int getRowFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int row;
            do
            {
                System.Console.WriteLine("Please enter row (1-{0}):", m_GameLogic.BoardDimension);
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out row);
                if (!numberIsInt && inputText.Equals((string)"q"))
                {
                    return k_EndGameCode;
                }

                if (!numberIsInt || row < 1 || row > m_GameLogic.BoardDimension)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    goodInput = true;
                }
            }
            while (!goodInput);

            return row;
        }

        private int getColumnFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int column;
            do
            {
                System.Console.WriteLine("Please enter column (1-{0}):", m_GameLogic.BoardDimension);
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out column);
                if (!numberIsInt || column < 1 || column > m_GameLogic.BoardDimension)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    goodInput = true;
                }
            }
            while (!goodInput);

            return column;
        }

        private void initializeGame()
        {
            System.Console.WriteLine("Welcome to Reverse X Mix Drix!\n");
            int boardSize = getBoardSizeFromUser();
            ePlayerType pt = getPlayerTypeFromUser();
            m_GameLogic = new GameLogic(boardSize, pt);
        }

        private ePlayerType getPlayerTypeFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int numericPlayerType;

            do
            {
                System.Console.WriteLine("Please enter 1 for Human player or 2 for Computer:");
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out numericPlayerType);
                if (!numberIsInt || numericPlayerType < 1 || numericPlayerType > 2)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    goodInput = true;
                }
            }
            while (!goodInput);

            return numericPlayerType == 1 ? ePlayerType.HUMAN : ePlayerType.COMPUTER;
        }

        private int getBoardSizeFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int boardSize = 0;
            do
            {
                System.Console.WriteLine("Please enter desired board size (3-9):");
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out boardSize);
                if (!numberIsInt || boardSize < k_MinBoardSize || boardSize > k_MaxBoardSize)
                {
                    System.Console.WriteLine("The input you entered is invalid.");
                }
                else
                {
                    goodInput = true;
                }
            }
            while (!goodInput);

            return boardSize;
        }

        public void Print()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            write(getHorizontalIndexes());

            for (int i = 0; i < m_GameLogic.BoardDimension; i++)
            {
                write(getLine(i));
                write(getSeparatorLine());
            }
        }

        private string getSeparatorLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < (m_GameLogic.BoardDimension * 2) + 2; i++)
            {
                sb.Append(k_RowSeparator);
            }

            return sb.ToString();
        }

        private string getLine(int i_LineIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(i_LineIndex + 1);
            for (int i = 0; i < m_GameLogic.BoardDimension; i++)
            {
                stringBuilder.Append(k_ColumnSeparator);
                switch (m_GameLogic.Board[i_LineIndex, i])
                {
                    case eCellValue.EMPTY:
                        stringBuilder.Append(k_EmptyCellSign);
                        break;
                    case eCellValue.PLAYER_1:
                        stringBuilder.Append(Player.k_Player1Sign);
                        break;
                    case eCellValue.PLAYER_2:
                        stringBuilder.Append(Player.k_Player2Sign);
                        break;
                }
            }

            stringBuilder.Append(k_ColumnSeparator);

            return stringBuilder.ToString();
        }

        private string getHorizontalIndexes()
        {
            StringBuilder stringBuilder = new StringBuilder(k_EmptyCellSign.ToString());

            for (int i = 1; i <= m_GameLogic.BoardDimension; i++)
            {
                stringBuilder.Append(k_EmptyCellSign).Append(i);
            }

            return stringBuilder.ToString();
        }

        private void write(string i_stringToWrite)
        {
            System.Console.WriteLine(i_stringToWrite);
        }
    }
}
