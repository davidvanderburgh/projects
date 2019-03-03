using System;
using System.Collections.Generic;
using System.Text;
using InverseCaptcha;

namespace InverseCaptcha
{
    public class UserInterface
    {
        public void Run()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Enter your number to check or type \"q\" to quit:");
                string userEntryString = Console.ReadLine();
                if (InverseCaptcha.EveryCharacterIsANumber(userEntryString))
                {
                    Console.WriteLine("Your sum is: " + InverseCaptcha.SequenceSum(userEntryString));
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
    }
}
