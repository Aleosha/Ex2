using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    public class GameLogic
    {
        private eCellValue[,] m_Board;
        private int m_Dimension;
        private Player m_Player1, m_Player2, m_CurrPlayer;
        private eGameTerminationStatus m_GameTerminationStatus;

        public eGameTerminationStatus GameTerminationStatus
        {
            get { return m_GameTerminationStatus; }
            set { m_GameTerminationStatus = value; }
        }

        public eCellValue[,] Board
        {
            get { return m_Board; }
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
            get { return m_Dimension; }
        }

        public bool SetCell(int i_Row, int i_Column, eCellValue i_CellValue)
        {
            bool setSuccessful = true;
            if (eCellValue.EMPTY.Equals(i_CellValue))
            {
                setSuccessful = false;
            }
            else if (!eCellValue.EMPTY.Equals(m_Board[i_Row - 1, i_Column - 1]))
            {
                setSuccessful = false;
            }
            else
            {
                m_Board[i_Row - 1, i_Column - 1] = i_CellValue;
            }

            return setSuccessful;
        }

        public void MakeNewRound()
        {
            m_Board = new eCellValue[m_Dimension, m_Dimension];
            for (int i = 0; i < m_Dimension; i++)
            {
                for (int j = 0; j < m_Dimension; j++)
                {
                    m_Board[i, j] = eCellValue.EMPTY;
                }
            }

            m_CurrPlayer = m_Player1;
            m_GameTerminationStatus = eGameTerminationStatus.UNFINISHED;
        }

        public bool IsGameOver()
        {
            return isGameWon() || isBoardFull() || isGameAbandoned();
        }

        private bool isGameAbandoned()
        {
            return m_GameTerminationStatus == eGameTerminationStatus.ABANDONED;
        }

        private bool isGameWon()
        {
            bool wonGame = isRowFull() || isColumnFull() || isDiagonalFull() || isReverseDiagonalFull();
            if (wonGame)
            {
                m_GameTerminationStatus = eGameTerminationStatus.WON;
                m_CurrPlayer.increaseScore();
            }

            return wonGame;
        }

        private bool isBoardFull()
        {
            bool fullBoard = true;
            for (int i = 0; i < m_Dimension; i++)
            {
                for (int j = 0; j < m_Dimension; j++)
                {
                    eCellValue currentCell = m_Board[i, j];
                    if (eCellValue.EMPTY.Equals(currentCell))
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
            for (int i = 0; i < this.m_Dimension - 1; i++)
            {
                eCellValue currentCell = this.m_Board[i, this.m_Dimension - i - 1];
                eCellValue nextCell = this.m_Board[i + 1, m_Dimension - i - 2];

                if (eCellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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
            for (int i = 0; i < m_Dimension - 1; i++)
            {
                eCellValue currentCell = m_Board[i, i];
                eCellValue nextCell = m_Board[i + 1, i + 1];

                if (eCellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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
            for (int i = 0; i < m_Dimension; i++)
            {
                bool fullColumn = true;
                for (int j = 0; j < m_Dimension - 1; j++)
                {
                    eCellValue currentCell = m_Board[j, i];
                    eCellValue nextCell = m_Board[j + 1, i];
                    if (eCellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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
            for (int i = 0; i < m_Dimension; i++)
            {
                bool fullRow = true;
                for (int j = 0; j < m_Dimension - 1; j++)
                {
                    eCellValue currentCell = m_Board[i, j];
                    eCellValue nextCell = m_Board[i, j + 1];
                    if (eCellValue.EMPTY.Equals(currentCell) || !nextCell.Equals(currentCell))
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

        public GameLogic(int i_Dimension, ePlayerType i_PlayerType)
        {
            m_Dimension = i_Dimension;
            m_Player1 = new Player(ePlayerType.HUMAN, eCellValue.PLAYER_1); // m_Player1 is always HUMAN
            m_Player2 = new Player(i_PlayerType, eCellValue.PLAYER_2); // m_Player2 is the one whose type changes depending on the user's input
            MakeNewRound();
        }

        public eCellValue GetCell(int i_Row, int i_Column)
        {
            return m_Board[i_Row - 1, i_Column - 1];
        }

        internal void AlternatePlayers()
        {
            m_CurrPlayer = (m_CurrPlayer.CellValue == m_Player1.CellValue) ? m_Player2 : m_Player1;
        }
    }
}
