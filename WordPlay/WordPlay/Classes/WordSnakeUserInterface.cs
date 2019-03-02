using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordPlay.Classes
{
    public class WordSnakeUserInterface
    {
        public void Run()
        {
            try
            {
                //Ask for the word input file
                Console.Clear();
                string filePath = GetInputWordListFileFromUser();

                bool done = false;
                while (!done)
                {
                    Console.Clear();

                    //How big should the grid be?
                    int gridSize = GetGridSizeFromUser();

                    //Create instance of WordList class with input file
                    WordSnake wordSnake = new WordSnake(filePath, gridSize);

                    //Generate a random character grid
                    wordSnake.GenerateRandomGrid();

                    //Print the character grid to the console
                    Console.WriteLine(wordSnake.GetCharacterGridString());

                    //Find the longest words and print them to the console
                    HashSet<string> longestWords = wordSnake.FindLongestWords();
                    if (longestWords.Count == 1)
                    {
                        Console.WriteLine($"The longest word found is:{wordSnake.GetListString(longestWords)}");
                    }
                    else
                    {
                        Console.WriteLine($"The longest words found are:{wordSnake.GetListString(longestWords)}");
                    }

                    //Ask to generate a new one
                    Console.WriteLine("Press ENTER to generate a new word grid, or any other key to end:");

                    if (Console.ReadLine() != "")
                    {
                        done = true;
                    }
                }
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private int GetGridSizeFromUser()
        {
            Console.WriteLine("How big should the grid be? Enter a number between 5 and 20, or ENTER to use the default 10:");
            string userEntry = Console.ReadLine();

            if (string.IsNullOrEmpty(userEntry))
            {
                Console.WriteLine("Using default size 10");
                return 10;
            }
            else if (int.TryParse(userEntry, out int size) &&
                (size >= 5 && size <= 20))
            {
                return size;
            }
            else
            {
                Console.WriteLine("Entry was not valid, using 10");
                return 10;
            }
        }

        private string GetInputWordListFileFromUser()
        {
            bool wordListfileExists = false;
            string filePath = "";
            while (!wordListfileExists)
            {
                Console.WriteLine("Please provide the absolute path of the word list file, or press ENTER to use default 'words.txt'");
                filePath = Console.ReadLine();
                if (filePath == "")
                {
                    filePath = "words.txt";
                    wordListfileExists = true;
                }
                else if (File.Exists(filePath))
                {
                    wordListfileExists = true;
                }
                else
                {
                    Console.WriteLine("File not found... please input again");
                }
            }
            return filePath;
        }
    }
}
