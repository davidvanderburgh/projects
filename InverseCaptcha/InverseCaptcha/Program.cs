using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay1
{
    class Program
    {
        static void Main(string[] args)
        {
            int userEntryInt;
            string userEntryString;

            while (true)
            {
                Console.WriteLine("Enter your number to check or type \"q\" to quit:");
                userEntryString = Console.ReadLine();
                if (int.TryParse(userEntryString, out userEntryInt))
                {
                    Console.WriteLine("Your sum is: " + sequenceSum(userEntryInt.ToString()));
                }
                else if (userEntryString == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You entered a bad sequence");
                }
            }
        }

        static int sequenceSum(string inputSequence)
        {
            int[] sequenceArray = getIntArrayFromString(inputSequence);
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

        static int[] getIntArrayFromString(string source)
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
