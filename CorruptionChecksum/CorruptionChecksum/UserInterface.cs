using System;
using System.Collections.Generic;
using System.Text;
using CorruptionChecksum.Logic;

namespace CorruptionChecksum
{
    public class UserInterface
    {
        public void Run()
        {
            CorruptionChecksumLogic corruptionChecksumLogic = new CorruptionChecksumLogic();

            bool done = false;

            Console.WriteLine("This program will compute the corruption checksum " +
                "\n(sum of difference of largest and smallest items in each row).");
            while (!done)
            {
                List<int[]> checksumNumberInput = new List<int[]>();
                Console.WriteLine("Enter space separated numbers (an array). Press ENTER to add arrays. Type a letter to end.");
                while(!done)
                {
                    string userInput = Console.ReadLine();

                    if (userInput != "" && StringContainsOnlyNumbersAndSpaces(userInput))
                    {
                        List<int> inputNumbers = new List<int>();
                        foreach(string s in userInput.Split(' '))
                        {
                            inputNumbers.Add(Convert.ToInt32(s));
                        }
                        checksumNumberInput.Add(inputNumbers.ToArray());
                    }
                    else
                    {
                        Console.WriteLine("Done!");
                        done = true;
                    }
                }
                Console.WriteLine("The computed checksum is:");
                Console.WriteLine(corruptionChecksumLogic.GetChecksum(checksumNumberInput));
            }
            // end of program
            Console.ReadLine(); 
        }

        private bool StringContainsOnlyNumbersAndSpaces(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c) && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
