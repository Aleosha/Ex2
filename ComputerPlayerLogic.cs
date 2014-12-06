using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    class ComputerPlayerLogic
    {
        public static int[] getBestOption(GameWorld world)
        {
            int[] bestOption = new int[2] { 1, 1 };
            return bestOption;
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
