using System;
using System.Collections.Generic;
using System.Text;

namespace InverseCaptcha
{
    public static class InverseCaptchaLogic
    {
        public static bool EveryCharacterIsANumber(string input)
        {
            foreach (char c in input)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static int SequenceSum(string inputSequence)
        {
            int[] sequenceArray = GetIntArrayFromString(inputSequence);
            int totalSum = 0;

            for (int i = 0; i <= sequenceArray.Length - 1; i++)
            {
                if (i == sequenceArray.Length - 1)
                {
                    if (sequenceArray[sequenceArray.Length - 1] == sequenceArray[0])
                    {
                        totalSum += sequenceArray[sequenceArray.Length - 1];
                    }
                }
                else if (sequenceArray[i] == sequenceArray[i + 1])
                {
                    totalSum += sequenceArray[i + 1];
                }
            }
            return totalSum;
        }

        public static int[] GetIntArrayFromString(string source)
        {
            int[] returnArray = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                returnArray[i] = Convert.ToInt32(source.Substring(i, 1));
            }
            return returnArray;
        }
    }
}
