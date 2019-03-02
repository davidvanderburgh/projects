using System;
using System.Collections.Generic;
using System.Text;

namespace WordPlay.Classes
{
    public class WordFindUserInterface
    {
        public WordFind wordFind;
        public string puzzle = "";
        public string solution = "";

        public void Run()
        {
            try
            {
                bool done = false;
                while (!done)
                {
                    Console.Clear();

                    //How big should the grid be?
                    Console.WriteLine("What size grid would you like to use (5 to 30)? Press ENTER for default of 10");
                    int gridSize = GetGridSizeFromUser(Console.ReadLine());

                    //What words should be placed?
                    Console.WriteLine("Enter case-insensitive words to place in your Word Find grid (space separated):");
                    string[] words = Console.ReadLine().ToLower().Split(' ');

                    wordFind = new WordFind(gridSize, new HashSet<string>(words));

                    GeneratePuzzleAndSolution();

                    Console.Clear();

                    Console.WriteLine($"\n{puzzle}");

                    ShowWordsUsedAndNotUsed();

                    Console.WriteLine("Press any key to show the solution");
                    Console.ReadKey();

                    PrintSolutionAnimation();

                    //Ask to generate a new one
                    Console.WriteLine("Press ENTER to generate a new grid with new words, or any other key to end:");

                    if (Console.ReadLine() != "")
                    {
                        done = true;
                    }
                    Console.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GeneratePuzzleAndSolution()
        {
            wordFind.CreateWordGridWithoutRandomLetters();
            solution = wordFind.GridString();

            wordFind.GenerateRandomLettersOnEmptySpaces();
            puzzle = wordFind.GridString();
        }

        private int GetGridSizeFromUser(string userInput)
        {
            int gridSize = 10;

            if (userInput == "")
            {
                Console.WriteLine("Using default size of 10");
                gridSize = 10;
            }
            else if (!int.TryParse(userInput, out gridSize))
            {
                Console.WriteLine("Not a valid number, using default size of 10");
                gridSize = 10;
            }
            else if (gridSize < 5)
            {
                Console.WriteLine("Grid size too small, using smallest size of 5");
                gridSize = 5;
            }
            else if (gridSize > 30)
            {
                Console.WriteLine("Grid size too big, using biggest size of 30");
                gridSize = 30;
            }
            return gridSize;
        }

        private bool GetYNFromUser()
        {
            bool isYes = false;
            string userInput = Console.ReadLine().ToLower();
            switch (userInput)
            {
                case "y":
                    isYes = true;
                    break;
                case "n":
                    isYes = false;
                    break;
                default:
                    Console.WriteLine("Invalid entry. Using 'n'");
                    isYes = false;
                    break;
            }
            return isYes;
        }

        private void ShowWordsUsedAndNotUsed()
        {
            if (wordFind.WordsPlaced.Count != 0)
            {
                Console.WriteLine($"\nWords placed: {GetListString(wordFind.WordsPlaced)}");
            }
            if (wordFind.WordsNotPlaced.Count != 0)
            {
                Console.WriteLine($"\nWords not placed: {GetListString(wordFind.WordsNotPlaced)}");
            }
        }

        public static string GetListString(IEnumerable<string> list) => "\n" + String.Join("\n", list);

        private void PrintSolutionAnimation()
        {
            foreach (char[,] characterGrid in wordFind.SolutionAnimation)
            {
                Console.Clear();
                Console.WriteLine($"{wordFind.GridString()}\n");

                Console.WriteLine(wordFind.GridString(characterGrid));
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
