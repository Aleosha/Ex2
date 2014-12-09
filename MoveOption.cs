using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    public class MoveOption
    {
        private int m_Row;
        private int m_Column;

        public MoveOption(int i_Row, int i_Column)
        {
            this.m_Row = i_Row;
            this.m_Column = i_Column;
        }

        public int Column 
        {
            get { return m_Column; }
        }

        public int Row 
        {
            get { return m_Row; } 
        }
    }
}
