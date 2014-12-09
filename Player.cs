using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    public class Player
    {
        private ePlayerType m_PlayerType;
        private eCellValue m_CellValue;

        public const char k_Player1Sign = 'X';
        public const char k_Player2Sign = 'O';

        private int m_Score = 0;

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
            return char.ToString(m_CellValue == eCellValue.PLAYER_1 ? k_Player1Sign : k_Player2Sign);
        }

        public void increaseScore()
        {
            m_Score++;
        }
    }
}
