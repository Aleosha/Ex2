using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    class ComputerPlayerLogic
    {
        private const int ROW_INDEX = 0;
        private const int COLUMN_INDEX = 1;

        public static int[] getBestOption(GameWorld world)
        {
            int[] bestOption = new int[2] { 1, 1 };
            float bestOptionWeight = 0;
            int dimensions = world.BoardDimension;
            int[][] options = new int[dimensions*dimensions][];

            int optionsCount = 0;
            for (int i = 1; i <= dimensions; i++ )
            {
                for (int j = 1; j <= dimensions; j++)
                {
                    if (CellValue.EMPTY.Equals(world.GetCell(i, j)))
                    {
                        options[optionsCount] = new int[] { i, j};
                        optionsCount++;
                    }
                }
            }

            for (int i = 0; i < optionsCount; i++ )
            {
                float currentOptionWeight = weightOption(options[i], world);
                if (currentOptionWeight > bestOptionWeight)
                {
                    bestOptionWeight = currentOptionWeight;
                    bestOption = options[i];
                }
            }

            return bestOption;
        }

        private static float weightOption(int[] p, GameWorld world)
        {
            float weight = 0;
            
            int row = p[ROW_INDEX];
            int column = p[COLUMN_INDEX];
            int dimensions = world.BoardDimension;
            bool isOnDiagonal = (row == column) || (row+column==dimensions+1);

            for (int i = 1; i <= dimensions; i++)
            {
                if (i != column)
                {
                    switch (world.GetCell(row, i)) 
                    {
                        case CellValue.EMPTY:
                            weight++;
                            break;
                        case CellValue.PLAYER_2:
                            weight--;
                            break;            
                    }
                                
                }
            }

            for (int i = 1; i <= dimensions; i++)
            {
                if (i != row)
                {
                    switch (world.GetCell(i, column))
                    {
                        case CellValue.EMPTY:
                            weight++;
                            break;
                        case CellValue.PLAYER_2:
                            weight--;
                            break;
                    }
                       
                }
            }

            if (isOnDiagonal)
            {
                weight *= 0.5f;
            }

            return weight;
        }


        internal static string printBestOption(GameWorld world)
        {
            int[] bestOption = getBestOption(world);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bestOption.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }
                sb.Append(bestOption[i]);
            }

            return sb.ToString();
        }
    }
}
