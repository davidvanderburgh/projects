using System;
using System.Collections.Generic;
using System.Text;

namespace High_Entropy_Passphrases
{
    public class UserInterface
    {
        public void Run()
        {
            Console.WriteLine("Please enter a password phrase.  Password phrases cannot have any repeating phrases:");
            HighEntropyPassphrase highEntropyPassphrase = new HighEntropyPassphrase(Console.ReadLine());

            string result = "";

            if (highEntropyPassphrase.IsValid())
            {
                result = "Password is good!";
            }
            else
            {
                result = "Password is no good :(";
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
