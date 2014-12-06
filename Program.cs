using System;
using System.Collections.Generic;
using System.Text;


namespace Ex2
{
    class Program
    {


        static void Main(string[] args)
        {

            GameConsole myConsole = new GameConsole();

        }
    }

    public class GameConsole
    {
        private GameWorld worldRef;

        public GameConsole()
        {
            InitializeGame();
            RunGame();
        }

        private void RunGame()
        {
            bool endGame = false;
            while(!endGame)
            {
                while (!worldRef.IsGameOver())
                {
                    Print();
                    showWhoseTurnItIs();
                    bool wasCellEmpty = false;
                    while (!wasCellEmpty)
                    {
                        if (worldRef.CurrPlayer.PlayerType == PlayerType.HUMAN)
                        {
                            makeHumanMove(ref wasCellEmpty);
                            if(worldRef.GameTerminationStatus == eGameTerminationStatus.ABANDONED)
                            {
                                break;
                            }
                        }
                        else
                        {
                            makeComputerMove();
                        }
                    }
                    worldRef.AlternatePlayers();
                }
                Print();
                if (worldRef.GameTerminationStatus == eGameTerminationStatus.TIE)
                {
                    System.Console.WriteLine("It's a tie.");
                }
                else if (worldRef.GameTerminationStatus == eGameTerminationStatus.WON)
                {
                    System.Console.WriteLine("Player {0} won.", worldRef.CurrPlayer.ToString());
                    worldRef.CurrPlayer.increaseScore();
                }
                else if(worldRef.GameTerminationStatus == eGameTerminationStatus.UNFINISHED)
                {
                    System.Console.WriteLine("The game has been left unfinished.");
                }
                else
                {
                    System.Console.WriteLine("The game has been abandoned.");
                }
                showScores();
                endGame = !proposeNewGame();
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
            if(playAgain == 1)
            {
                worldRef.makeNewRound();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void showScores()
        {
            System.Console.WriteLine("Player {0}'s score: {1}", worldRef.Player1.ToString(), worldRef.Player1.Score);
            System.Console.WriteLine("Player {0}'s score: {1}", worldRef.Player2.ToString() ,worldRef.Player2.Score);
        }

        private void makeComputerMove()
        {
            throw new NotImplementedException();
        }

        private void makeHumanMove(ref bool io_WasCellEmpty)
        {
            int currRow = getRowFromUser();
            if(currRow == -1)
            {
                worldRef.GameTerminationStatus = eGameTerminationStatus.ABANDONED;
                return;
            }
            int currColumn = getColumnFromUser();
            io_WasCellEmpty = worldRef.SetCell(currRow, currColumn, worldRef.CurrPlayer.CellValue);
            if (!io_WasCellEmpty)
            {
                System.Console.WriteLine("Cell is already full. Choose another cell.");
            }
        }

        public void showWhoseTurnItIs()
        {
            System.Console.WriteLine("It's {0}'s turn", worldRef.CurrPlayer.ToString());
        }

        private int getRowFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int row;
            do
            {
                System.Console.WriteLine("Please enter row (1-{0}):", worldRef.BoardDimension);
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out row);
                if(!numberIsInt && inputText.Equals((string)("q")))
                {
                    return -1;
                }
                if (!numberIsInt || row < 1 || row > worldRef.BoardDimension)
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
                System.Console.WriteLine("Please enter column (1-{0}):", worldRef.BoardDimension);
                string inputText = System.Console.ReadLine();
                numberIsInt = int.TryParse(inputText, out column);
                if (!numberIsInt || column < 1 || column > worldRef.BoardDimension)
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


        private void InitializeGame()
        {
            System.Console.WriteLine("Welcome to Reverse X Mix Drix!\n");
            int boardSize = getBoardSizeFromUser();
            PlayerType pt = getPlayerTypeFromUser();
            worldRef = new GameWorld(boardSize, pt);
        }

        private PlayerType getPlayerTypeFromUser()
        {
            bool numberIsInt = false;
            bool goodInput = false;
            int numericPlayerType;
            PlayerType pt;
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

            return numericPlayerType == 1 ? PlayerType.HUMAN : PlayerType.COMPUTER;
        }

        public GameWorld World
        {
            get { return worldRef; }
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
                if (!numberIsInt || boardSize < 3 || boardSize > 9)
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

            for (int i = 0; i < worldRef.BoardDimension; i++)
            {
                write(getLine(i));
                write(getSeparatorLine());
            }
        }

        private string getSeparatorLine()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < worldRef.BoardDimension * 2 + 2; i++)
            {
                sb.Append("=");
            }
            return sb.ToString();
        }

        private string getLine(int i_LineIndex)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(i_LineIndex + 1);
            for (int i = 0; i < worldRef.BoardDimension; i++)
            {
                sb.Append("|");
                switch (worldRef.Board[i_LineIndex, i])
                {
                    case CellValue.EMPTY:
                        sb.Append(" ");
                        break;
                    case CellValue.PLAYER_1:
                        sb.Append("X");
                        break;
                    case CellValue.PLAYER_2:
                        sb.Append("O");
                        break;
                }
            }
            sb.Append("|");

            return sb.ToString();

        }


        private string getHorizontalIndexes()
        {
            StringBuilder sb = new StringBuilder(" ");

            for (int i = 1; i <= worldRef.BoardDimension; i++)
            {
                sb.Append(" ").Append(i);
            }


            return sb.ToString();
        }

        private void write(string i_stringToWrite)
        {
            System.Console.WriteLine(i_stringToWrite);
        }
    }

    public class GameWorld
    {

        private CellValue[,] m_board;
        private int m_dimension;
        private CellValue cellValuesEnum;
        private Player m_Player1, m_Player2, m_CurrPlayer;
        private eGameTerminationStatus m_GameTerminationStatus;

        public eGameTerminationStatus GameTerminationStatus
        {
            get { return m_GameTerminationStatus; }
            set { m_GameTerminationStatus = value;}
        }

        public CellValue[,] Board
        {
            get { return m_board; }
        }

        public Player CurrPlayer
        {
            get { return m_CurrPlayer; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public int BoardDimension
        {
            get { return m_dimension; }
        }

        public bool SetCell(int row, int column, CellValue value)
        {
            if (CellValue.EMPTY.Equals(value))
            {
                return false;
            }
            else if (!CellValue.EMPTY.Equals(m_board[row - 1, column - 1]))
            {
                return false;
            }
            m_board[row - 1, column - 1] = value;

            return true;
        }

        public void makeNewRound()
        {
            m_board = new CellValue[m_dimension, m_dimension];
            for (int i = 0; i < m_dimension; i++)
            {
                for (int j = 0; j < m_dimension; j++)
                {
                    m_board[i, j] = CellValue.EMPTY;
                }
            }
            m_CurrPlayer = m_Player1;
            m_GameTerminationStatus = eGameTerminationStatus.UNFINISHED;
        }

        public bool IsGameOver()
        {

            // Check vertical
            // Check horizontal
            // Check diagonal left to right
            // Check diagonal right to left
            return isGameWon() || isBoardFull() || isGameAbandoned();
        }

        private bool isGameAbandoned()
        {
            return (m_GameTerminationStatus == eGameTerminationStatus.ABANDONED);
        }

        private bool isGameWon()
        {
            bool wonGame = isRowFull() || isColumnFull() || isDiagonalFull() || isReverseDiagonalFull();
            if (wonGame)
            {
                m_GameTerminationStatus = eGameTerminationStatus.WON;
            }
            return wonGame;
        }

        private bool isBoardFull()
        {
            bool fullBoard = true;
            for (int i = 0; i < m_dimension; i++)
            {
                for (int j = 0; j < m_dimension; j++)
                {

                    CellValue currentCell = m_board[i, j];
                    if (CellValue.EMPTY.Equals(currentCell))
                    {
                        fullBoard = false;
                        break;
                    }

                }
            }
            if (fullBoard)
            {
                m_GameTerminationStatus = eGameTerminationStatus.TIE;
            }
            return fullBoard;
        }

        private bool isReverseDiagonalFull()
        {

            bool fullDiagonal = true;
            for (int i = 0; i < m_dimension - 1; i++)
            {
                CellValue currentCell = m_board[i, m_dimension - i - 1];
                CellValue nextCell = m_board[i + 1, m_dimension - i - 2];

                if (CellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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
                CellValue currentCell = m_board[i, i];
                CellValue nextCell = m_board[i + 1, i + 1];

                if (CellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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

                    CellValue currentCell = m_board[j, i];
                    CellValue nextCell = m_board[j + 1, i];
                    if (CellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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

                    CellValue currentCell = m_board[i, j];
                    CellValue nextCell = m_board[i, j + 1];
                    if (CellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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

        public GameWorld(int i_dimension, PlayerType i_PlayerType)
        {
            m_dimension = i_dimension;
            m_Player1 = new Player(PlayerType.HUMAN, CellValue.PLAYER_1); // m_Player1 is always HUMAN
            m_Player2 = new Player(i_PlayerType, CellValue.PLAYER_2); // m_Player2 is the one whose type changes depending on the user's input
            makeNewRound();
        }

        public CellValue GetCell(int i, int j)
        {
            return m_board[i - 1, j - 1];
        }



        internal void AlternatePlayers()
        {
            m_CurrPlayer = (m_CurrPlayer.CellValue == m_Player1.CellValue) ? m_Player2 : m_Player1;
        }
    }

    public class Player
    {
        PlayerType m_PlayerType;
        CellValue m_CellValue;
        int m_score = 0;

        public Player(PlayerType i_playerType, string i_name)
        {

        }

        public Player(PlayerType i_PlayerType, CellValue i_CellValue)
        {
            m_PlayerType = i_PlayerType;
            m_CellValue = i_CellValue;
        }

        public int Score
        {
            get { return m_score; }
        }

        public CellValue CellValue
        {
            get { return m_CellValue; }
        }

        public PlayerType PlayerType
        {
            get { return m_PlayerType; }
        }

        public string ToString()
        {
            return m_CellValue == CellValue.PLAYER_1 ? "X" : "O";
        }

        public void increaseScore()
        {
            m_score++;
        }
    }

    public enum PlayerType
    {
        HUMAN,
        COMPUTER
    }

    public enum eGameTerminationStatus
    {
        WON,
        TIE,
        UNFINISHED,
        ABANDONED
    }

}
