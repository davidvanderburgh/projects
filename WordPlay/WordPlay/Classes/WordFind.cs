using System;
using System.Collections.Generic;
using System.Text;

namespace WordPlay.Classes
{
    /// <summary>
    /// Generates a grid of characters where given words have been placed
    /// </summary>
    public class WordFind
    {
        public int GridSize { get; private set; }
        public HashSet<string> RequestedWords { get; private set; }
        private char[,] CharacterGrid { get; set; }
        public List<char[,]> SolutionAnimation { get; private set; }
        private Random RandomNumberGenerator = new Random();
        public HashSet<string> WordsPlaced { get; private set; } = new HashSet<string>();
        public HashSet<string> WordsNotPlaced { get; private set; } = new HashSet<string>();

        public enum Orientation
        {
            North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
        };

        public WordFind(int gridSize, HashSet<string> requestedWords)
        {
            if (gridSize < 5)
            {
                throw new Exception("Error: Grid size is too small (must be at least 5)");
            }
            else if (gridSize > 30)
            {
                throw new Exception("Error: Grid size is too big (must be at least 30)");
            }

            GridSize = gridSize;
            CharacterGrid = new char[GridSize, GridSize];
            SolutionAnimation = new List<char[,]>();
            RequestedWords = requestedWords;
        }

        public void CreateWordGridWithoutRandomLetters()
        {
            foreach (string word in RequestedWords)
            {
                //track if the word is successfully placed
                bool wordPlaced = false;

                //Go through random coordinates
                List<Coordinate> randomCoordinateOrder = GenerateRandomGridAccessOrder();

                foreach (Coordinate coordinate in randomCoordinateOrder)
                {
                    //For each word, generate a random orientation order to try
                    List<Orientation> randomOrientationOrder = GenerateRandomOrientationOrder();

                    foreach (Orientation orientation in randomOrientationOrder)
                    {
                        //If a word can be placed without interfering with existing letters, place it
                        if (CheckWordAtCoordinateOrientation(word, coordinate, orientation))
                        {
                            PlaceWordAtCoordinateOrientation(word, coordinate, orientation);
                            wordPlaced = true;
                            WordsPlaced.Add(word);

                            // do not place same word multiple times
                            break; 
                        }
                    }

                    if (wordPlaced)
                    {
                        //if the word was placed, don't go onto other coordinates
                        break; 
                    }
                }
                if (!wordPlaced)
                {
                    //keep a list of words not placed
                    WordsNotPlaced.Add(word);
                }
            }
        }

        public bool CheckWordAtCoordinateOrientation(string word, Coordinate coordinate, Orientation orientation)
        {
            //check that the existing letters in place match or that they are empty
            for (int i = 0; i < word.Length; i++)
            {
                Coordinate charCoordinate = new Coordinate(
                    coordinate.X + (i * GetXIncrementFromOrientation(orientation)),
                    coordinate.Y + (i * GetYIncrementFromOrientation(orientation)));
                if (!CheckLetterAtCoordinate(word[i], charCoordinate))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckLetterAtCoordinate(char letter, Coordinate charCoordinate)
        {
            if (CoordinateIsOutOfBounds(charCoordinate))
            {
                return false;
            }
            else if (Char.Equals(CharacterGrid[charCoordinate.X, charCoordinate.Y], '\0') ||
                Char.Equals(CharacterGrid[charCoordinate.X, charCoordinate.Y], letter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int GetXIncrementFromOrientation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return 0;
                case Orientation.NorthEast:
                    return 1;
                case Orientation.East:
                    return 1;
                case Orientation.SouthEast:
                    return 1;
                case Orientation.South:
                    return 0;
                case Orientation.SouthWest:
                    return -1;
                case Orientation.West:
                    return -1;
                case Orientation.NorthWest:
                    return -1;
                default:
                    return 0;
            }
        }

        private int GetYIncrementFromOrientation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return -1;
                case Orientation.NorthEast:
                    return -1;
                case Orientation.East:
                    return 0;
                case Orientation.SouthEast:
                    return 1;
                case Orientation.South:
                    return 1;
                case Orientation.SouthWest:
                    return 1;
                case Orientation.West:
                    return 0;
                case Orientation.NorthWest:
                    return -1;
                default:
                    return 0;
            }
        }

        private void PlaceWordAtCoordinateOrientation(string word, Coordinate coordinate, Orientation orientation)
        {
            for (int i = 0; i < word.Length; i++)
            {
                Coordinate letterCoordinate = new Coordinate(
                        coordinate.X + i * GetXIncrementFromOrientation(orientation),
                        coordinate.Y + i * GetYIncrementFromOrientation(orientation));
                CharacterGrid[letterCoordinate.X, letterCoordinate.Y] = word[i];
                SolutionAnimation.Add((char[,])CharacterGrid.Clone());
            }
        }

        private List<Coordinate> GenerateRandomGridAccessOrder()
        {
            List<Coordinate> randomGridAccessOrder = new List<Coordinate>();

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    randomGridAccessOrder.Add(new Coordinate(i, j));
                }
            }
            ShuffleList<Coordinate>(randomGridAccessOrder);

            return randomGridAccessOrder;
        }

        public bool CoordinateIsOutOfBounds(Coordinate coordinate)
        {
            return (coordinate.X >= GridSize || coordinate.X < 0 || coordinate.Y >= GridSize || coordinate.Y < 0);
        }

        private List<Orientation> GenerateRandomOrientationOrder()
        {
            List<Orientation> orientationOrder = new List<Orientation>
            {
                Orientation.North,
                Orientation.NorthEast,
                Orientation.East,
                Orientation.SouthEast,
                Orientation.South,
                Orientation.SouthWest,
                Orientation.West,
                Orientation.NorthWest
            };

            ShuffleList<Orientation>(orientationOrder);

            return orientationOrder;
        }

        private void ShuffleList<T>(List<T> list)
        {
            //Starting at the end of the list
            int n = list.Count;

            //Finish at the first index
            while (n > 1)
            {
                n--;

                // randomly create an index that can maximally be the scope of (0 to n)
                int k = RandomNumberGenerator.Next(n + 1);

                // swap the value at that random index with the index of n
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void GenerateRandomLettersOnEmptySpaces()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    if (CharacterGrid[row, column] == '\0')
                    {
                        CharacterGrid[row, column] = GetRandomLowercaseCharacter();
                    }
                }
            }
        }

        private char GetRandomLowercaseCharacter()
        {
            string characters = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            return characters[random.Next(characters.Length)];
        }

        public string GridString(char[,] characterGrid)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    sb.Append(characterGrid[row, column]);
                    sb.Append(" ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string GridString()
        {
            return GridString(CharacterGrid);
        }
    }
}
