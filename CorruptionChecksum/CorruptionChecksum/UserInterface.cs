using System;
using System.Collections.Generic;
using System.Text;

namespace CorruptionChecksum
{
    public class UserInterface
    {
        public void Run()
        {
            int[][] testArray1 =
{
                new [] {5, 1, 9, 5},
                new [] {7, 5, 3},
                new [] {2, 4, 6, 8}
            };

            int[][] testArray2 =
            {
                new [] {10, 10},
                new [] {7, -5, 0},
                new [] {8},
                new [] {0}
            };

            int[][] testArray3 =
            {
                new [] {6, 3, 18, 24 }
            };

            Console.WriteLine(CorruptionChecksumLogic.GetChecksum(testArray1));
            Console.WriteLine(CorruptionChecksumLogic.GetChecksum(testArray2));
            Console.WriteLine(CorruptionChecksumLogic.GetChecksum(testArray3));

            Console.ReadLine();
        }
    }
}
