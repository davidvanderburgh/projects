using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordPlay.Classes
{
    public class WordSnake
    {
        private IEnumerable<string> listOfWords;
        public HashSet<string> LongestWordGridWordList { get; private set; }
        public int LongestWordGridWordLength { get; private set; }
        private char[,] CharacterGrid { get; set; }
        private int GridSize { get; set; }

        /// <summary>
        /// Contains a word grid and the list of longest words found in that grid
        /// </summary>
        /// <param name="wordListFile">"Dictionary" input file</param>
        public WordSnake(string wordListFile, int gridSize)
        {
            ListOfWords = GetWordListFromFile(wordListFile).ToList();
            LongestWordGridWordList = new HashSet<string>();
            LongestWordGridWordLength = 0;
            GridSize = gridSize;
            CharacterGrid = new char[GridSize, GridSize];
        }

        /// <summary>
        /// Accessor for listOfWords
        /// </summary>
        private List<string> ListOfWords
        {
            get
            {
                return listOfWords.ToList();
            }

            set
            {
                listOfWords = value.ToList();
            }
        }

        /// <summary>
        /// Creates the List<string> ListOfWords based on the given TextFile
        /// Avoids punctuation characters and Uppercase words
        /// </summary>
        private IEnumerable<string> GetWordListFromFile(string wordListFile)
        {
            return File.ReadAllLines(wordListFile).Where(line => line.All(y => char.IsLower(y) && char.IsLetter(y)));
        }

        /// <summary>
        /// Creates a useful string for a List(string) object
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetListString(IEnumerable<string> list) => "\n" + String.Join("\n", list);

        /// <summary>
        /// Finds the longest words and returns a list of them
        /// </summary>
        /// <returns>longest words in a List</returns>
        public HashSet<string> FindLongestWords()
        {
            for (int column = 0; column < GridSize; column++)
            {
                for (int row = 0; row < GridSize; row++)
                {
                    FindLongestWordGridWordFromCoordinate("", new List<string>(), row, column);
                }
            }
            return LongestWordGridWordList;
        }

        /// <summary>
        /// Recursive function to set the longest word from a given coordinate
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void FindLongestWordGridWordFromCoordinate(string prefix, List<string> existingPrefixList, int row, int column)
        {
            //End the call if:
            // index out of bounds on the array
            // or the Count of the prefix list is 0 (no words have that prefix)
            // or there are no words in the prefix list which are longer than the longest found word
            // or if the Count of the prefix list is 1 (found the longest path which generates a word)
            //    set the longest word the remaining word if it is longer than the longest found word

            // Out of bounds condition
            if (row < 0 || row > (GridSize - 1) || column < 0 || column > (GridSize - 1))
            {
                return;
            }

            //Add the current letter to the prefix
            prefix += CharacterGrid[row, column];

            //Create the prefix list one time per function call to save resources
            //The prefix is composed of what the prefix was, plus the letter at the current position

            List<string> prefixList = GetPrefixList(prefix, existingPrefixList).ToList();

            // No words start with the given prefix
            if (prefixList.Count == 0)
            {
                return;
            }
            // No words in the prefix list are longer than the longest found word
            else if (!LongerWordsExistInList(prefixList, LongestWordGridWordLength))
            {
                return;
            }
            //Exactly one word shares the given prefix AND the "prefix" is a word AND it's at least as big as the largest found word
            else if (prefixList.Count == 1 &&
                prefix == prefixList[0] &&
                prefixList[0].Length >= LongestWordGridWordLength)
            {
                LongestWordGridWordLength = prefixList[0].Length;

                //update the known list of longest words
                UpdateListOfKnownLargestWords(prefixList[0]);
                return;
            }
            else
            {
                //recurse to the cell in each direction

                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row + 1, column); //East
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row + 1, column + 1); //SoutEast
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row, column + 1); // South
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row - 1, column + 1); // SouthWest
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row - 1, column); // West
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row - 1, column - 1); // NorthWest
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row, column - 1); // North
                FindLongestWordGridWordFromCoordinate(prefix, prefixList, row + 1, column - 1); // NorthEast
            }
        }

        /// <summary>
        /// Get a list of words that contain the given prefix
        /// </summary>
        /// <param name="prefix">Prefix to check in a word</param>
        /// <returns>List containing all of the words that start with the given prefix</returns>
        private IEnumerable<string> GetPrefixList(string prefix, List<string> existingPrefixList)
        {
            //Set an existing prefix list to help streamline processing
            //If the existing prefix list is empty, make the existing prefix list the entire list of words
            if (existingPrefixList.Count() == 0)
            {
                existingPrefixList = ListOfWords;
            }

            return existingPrefixList.Where(x => x.StartsWith(prefix, StringComparison.Ordinal));
        }

        /// <summary>
        /// Checks if there is a longer word in a list of words than the length given
        /// </summary>
        /// <param name="listOfWords">List of words</param>
        /// <param name="longestWordLength">Length of the longest word to check against</param>
        /// <returns>True if there is a longer word in the given word list</returns>
        private bool LongerWordsExistInList(IEnumerable<string> listOfWords, int longestWordLength)
        {
            return (listOfWords.Any(x => x.Length >= longestWordLength));
        }

        /// <summary>
        /// Add to the list of largest known words.  Removes words that are shorter than the longest found length.
        /// </summary>
        /// <param name="newLargestWord">The new largest word to add</param>
        private void UpdateListOfKnownLargestWords(string newLargestWord)
        {
            LongestWordGridWordList.Add(newLargestWord);
            LongestWordGridWordList.RemoveWhere(x => x.Length < newLargestWord.Length);
        }

        /// <summary>
        /// Generate a random GridSize x GridSize character grid and replace the existing one
        /// </summary>
        public void GenerateRandomGrid()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    CharacterGrid[row, column] = GetRandomLowercaseCharacter();
                }
            }
        }

        /// <summary>
        /// Generate a random lower case character
        /// </summary>
        /// <returns></returns>
        private char GetRandomLowercaseCharacter()
        {
            string characters = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            return characters[random.Next(characters.Length)];
        }

        /// <summary>
        /// Gets a string representation of the character grid
        /// </summary>
        /// <returns>string of the character grid</returns>
        public string GetCharacterGridString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    sb.Append(CharacterGrid[row, column]);
                    sb.Append(" ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
