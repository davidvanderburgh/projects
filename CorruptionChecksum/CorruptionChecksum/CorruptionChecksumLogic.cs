using System;
using System.Collections.Generic;
using System.Text;

namespace CorruptionChecksum
{
    public static class CorruptionChecksumLogic
    {
        public static int GetChecksum(int[][] worksheet)
        {
            int checksum = 0;
            for (int i = 0; i < worksheet.Length; i++)
            {
                checksum += GetRowMinMaxDifference(worksheet[i]);
            }
            return checksum;
        }

        public static int GetRowMinMaxDifference(int[] inputArray)
        {
            return GetMaxInArray(inputArray) - GetMinInArray(inputArray);
        }

        public static int GetMaxInArray(int[] inputArray)
        {
            int max = inputArray[0];
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] > max)
                {
                    max = inputArray[i];
                }
            }
            return max;
        }

        public static int GetMinInArray(int[] inputArray)
        {
            int min = inputArray[0];
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i] < min)
                {
                    min = inputArray[i];
                }
            }
            return min;
        }

        public static string GetJaggedArrayString(int[][] jaggedArray)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    result.Append(jaggedArray[i][j] + " ");
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}
