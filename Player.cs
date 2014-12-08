using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    public class Player
    {
        ePlayerType m_PlayerType;
        eCellValue m_CellValue;

        public const char PLAYER_1_SIGN = 'X';
        public const char PLAYER_2_SIGN = 'O';

        int m_Score = 0;

        public Player(ePlayerType i_playerType, string i_Name)
        {

        }

        public Player(ePlayerType i_PlayerType, eCellValue i_CellValue)
        {
            m_PlayerType = i_PlayerType;
            m_CellValue = i_CellValue;
        }

        public int Score
        {
            get { return m_Score; }
        }

        public eCellValue CellValue
        {
            get { return m_CellValue; }
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
        }

        public string ToString()
        {
            return char.ToString((m_CellValue == eCellValue.PLAYER_1 ? PLAYER_1_SIGN : PLAYER_2_SIGN));
        }

        public void increaseScore()
        {
            m_Score++;
        }
    }
}
