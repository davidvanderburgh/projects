using System;
using System.Collections.Generic;
using System.Text;

namespace A_Maze_of_Twisty_Trampolines
{
    public class UserInterface
    {
        public void Run()
        {
            Console.WriteLine("Read the README.md for information!");

            bool done = false;
            while (!done)
            {
                Console.WriteLine("Enter a space separated array maze (or a non-numeric value to quit):");
                string userInput = Console.ReadLine();
                string[] inputs = userInput.Split(' ');
                List<int> inputNumbers = new List<int>();

                foreach (string s in inputs)
                {
                    if (Int32.TryParse(s, out int result))
                    {
                        inputNumbers.Add(result);
                    }
                    else
                    {
                        done = true;
                        Console.WriteLine("Goodbye!");
                        continue;
                    }
                }

                if (!done)
                {
                    Maze maze = new Maze(inputNumbers.ToArray());

                    Console.WriteLine("Here is the path taken:");
                    Console.WriteLine(maze);

                    Console.WriteLine($"It took {maze.StepCounter} steps to reach the end");
                }
            }

            Console.Read();
        }
    }
}
