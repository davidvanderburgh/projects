using System;
using System.Collections.Generic;
using System.Text;

namespace CorruptionChecksum.Logic
{
    public class CorruptionChecksumLogic
    {
        public int GetChecksum(List<int[]> worksheet)
        {
            int checksum = 0;
            for (int i = 0; i < worksheet.Count; i++)
            {
                checksum += GetMinMaxDifference(worksheet[i]);
            }
            return checksum;
        }

        private int GetMinMaxDifference(int[] inputArray)
        {
            return GetMaxInArray(inputArray) - GetMinInArray(inputArray);
        }

        private int GetMaxInArray(int[] inputArray)
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

        private int GetMinInArray(int[] inputArray)
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

        private string GetJaggedArrayString(int[][] jaggedArray)
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
