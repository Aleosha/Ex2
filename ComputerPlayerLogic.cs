using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    class ComputerPlayerLogic
    {
        private const int ROW_INDEX = 0;
        private const int COLUMN_INDEX = 1;
        private const float k_DiagonalFactor = 0.5f;

        public static MoveOption GetBestOption(GameLogic i_Game)
        {
            MoveOption bestOption = null;            
            int dimensions = i_Game.BoardDimension;
            MoveOption[] options = new MoveOption[dimensions*dimensions];
            float bestOptionWeight = -1 * options.Length;

            int optionsCount = 0;
            for (int i = 1; i <= dimensions; i++ )
            {
                for (int j = 1; j <= dimensions; j++)
                {
                    if (eCellValue.EMPTY.Equals(i_Game.GetCell(i, j)))
                    {
                        options[optionsCount] = new MoveOption(i, j);
                        optionsCount++;
                    }
                }
            }

            for (int i = 0; i < optionsCount; i++ )
            {
                float currentOptionWeight = weightOption(options[i], i_Game);
                if (currentOptionWeight > bestOptionWeight)
                {
                    bestOptionWeight = currentOptionWeight;
                    bestOption = options[i];
                }
            }

            return bestOption;
        }

        private static float weightOption(MoveOption i_option, GameLogic i_Game)
        {
            float weight = 0;

            int row = i_option.Row;
            int column = i_option.Column;
            int dimensions = i_Game.BoardDimension;
            bool isOnDiagonal = (row == column) || (row+column==dimensions+1);

            // Count horizontal 
            for (int i = 1; i <= dimensions; i++)
            {
                if (i != column)
                {
                    switch (i_Game.GetCell(row, i)) 
                    {
                        case eCellValue.EMPTY:
                            weight++;
                            break;
                        case eCellValue.PLAYER_2:
                            weight--;
                            break;            
                    }
                                
                }
            }

            // Count vertical
            for (int i = 1; i <= dimensions; i++)
            {
                if (i != row)
                {
                    switch (i_Game.GetCell(i, column))
                    {
                        case eCellValue.EMPTY:
                            weight++;
                            break;
                        case eCellValue.PLAYER_2:
                            weight--;
                            break;
                    }
                       
                }
            }

            if (isOnDiagonal)
            {
                weight *= k_DiagonalFactor;
            }

            return weight;
        }


        internal static string printBestOption(GameLogic i_Game)
        {
            MoveOption bestOption = GetBestOption(i_Game);

            return bestOption.ToString();
        }
    }
}
