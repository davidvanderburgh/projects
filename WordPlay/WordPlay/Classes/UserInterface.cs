using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordPlay.Classes
{
    public class UserInterface
    {
        WordSnakeUserInterface wordSnakeUserInterface = new WordSnakeUserInterface();
        WordFindUserInterface wordFindUserInterface = new WordFindUserInterface();

        public void Run()
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("Select your program to run:\n(1) Word Snake\n(2) Word Find\nOr press ENTER to quit");

                switch (Console.ReadLine())
                {
                    case "1":
                        wordSnakeUserInterface.Run();
                        break;
                    case "2":
                        wordFindUserInterface.Run();
                        break;
                    case "":
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Invalid entry, please try again");
                        break;
                }
            }
        }        
    }
}
