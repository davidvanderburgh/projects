using System;
using System.Collections.Generic;
using System.Text;

namespace SpiralMemory
{
    public class SpiralGrid
    {
        public int?[,] NumberGrid { get; private set; }
        public int MaxNumber { get; private set; }
        public int GridSize { get; private set; }

        private int Column { get; set; }
        private int Row { get; set; }

        private int CellSize { get; set; }

        public List<string> Animation { get; private set; } = new List<string>();

        private enum Direction { Up, Down, Left, Right };
        private Direction CurrentDirection { get; set; }

        public SpiralGrid(int maxNumber)
        {
            MaxNumber = maxNumber;
            GridSize = GetGridSize(MaxNumber);
            CurrentDirection = Direction.Right;

            CellSize = GetCellSize();

            CreateNumberGrid();
        }

        private void CreateNumberGrid()
        {
            NumberGrid = new int?[GridSize, GridSize];

            //Start at the center of the grid
            Column = GridSize / 2;
            Row = GridSize / 2;

            //place a 1 at the center
            int number = 1;
            NumberGrid[Row, Column] = number;
            number++;

            while (number <= MaxNumber)
            {
                PlaceNextNumber(number);
                number++;
            }
        }

        public int GetGridSize(int maxNumber)
        {
            //find next largest odd square and set grid size to the root of it
            int size = 1;

            bool foundSize = false;
            while (!foundSize)
            {
                //if the test size is an odd number and its square is greater or equal to the max number
                //  then return the size
                if (size % 2 == 1 && size * size >= maxNumber)
                {
                    foundSize = true;
                    continue;
                }
                size++;
            }
            return size;
        }

        public void PlaceNextNumber(int number)
        {
            if (!ExistsNumberToRight() &&
                !ExistsNumberAbove() &&
                !ExistsNumberToLeft() &&
                !ExistsNumberBelow())
            {
                //Initial placement
                MoveRight();
            }
            else
            {
                switch (CurrentDirection)
                {
                    //build rightward
                    //   if no digits are above it, change direction to upward
                    //   otherwise keep going in the same direction
                    case Direction.Right:
                        if (!ExistsNumberAbove())
                        {
                            MoveUp();
                        }
                        else
                        {
                            MoveRight();
                        }
                        break;
                    //build upward
                    //   if no digits are to the left, change direction to leftward
                    //   otherwise keep going in the same direction
                    case Direction.Up:
                        if (!ExistsNumberToLeft())
                        {
                            MoveLeft();
                        }
                        else
                        {
                            MoveUp();
                        }
                        break;
                    //build leftward
                    //   if no digits are below, change direction to downward
                    //   otherwise keep going in the same direction
                    case Direction.Left:
                        if (!ExistsNumberBelow())
                        {
                            MoveDown();
                        }
                        else
                        {
                            MoveLeft();
                        }
                        break;
                    //build downward
                    //   if no digits are to the right, change direction to rightward
                    //   otherwise keep going in the same direction
                    case Direction.Down:
                        if (!ExistsNumberToRight())
                        {
                            MoveRight();
                        }
                        else
                        {
                            MoveDown();
                        }
                        break;
                }
            }
            NumberGrid[Row, Column] = number;

            Animation.Add(this.ToString());
        }

        private bool ExistsNumberAbove()
        {
            try
            {
                return (NumberGrid[Row - 1, Column] != null);
            }
            catch (IndexOutOfRangeException e)
            {
                return true;
            }
        }

        private bool ExistsNumberToRight()
        {
            try
            {
                return (NumberGrid[Row, Column + 1] != null);
            }
            catch (IndexOutOfRangeException e)
            {
                return true;
            }
        }

        private bool ExistsNumberBelow()
        {
            try
            {
                return (NumberGrid[Row + 1, Column] != null);
            }
            catch (IndexOutOfRangeException e)
            {
                return true;
            }
        }

        private bool ExistsNumberToLeft()
        {
            try
            {
                return (NumberGrid[Row, Column - 1] != null);
            }
            catch (IndexOutOfRangeException e)
            {
                return true;
            }
        }

        private void MoveRight()
        {
            CurrentDirection = Direction.Right;
            Column++;
        }

        private void MoveUp()
        {
            CurrentDirection = Direction.Up;
            Row--;
        }

        private void MoveLeft()
        {
            CurrentDirection = Direction.Left;
            Column--;
        }

        private void MoveDown()
        {
            CurrentDirection = Direction.Down;
            Row++;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (NumberGrid[i, j] == null)
                    {
                        sb.Append(CenterString(" ", CellSize));
                    }
                    else
                    {
                        sb.Append(CenterString(NumberGrid[i, j].ToString(), CellSize));
                    }
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public int GetCellSize()
        {
            return (MaxNumber.ToString().Length + 2);
        }

        public string CenterString(string stringToCenter, int lengthOfField)
        {
            //Find how many empty spaces are available in the field. Ex: "test" - "        " = 4
            int spaces = lengthOfField - stringToCenter.Length;

            //Take half of the available spaces add them to the length of the input string
            int padLeft = (spaces / 2) + stringToCenter.Length;

            //Pad left half of the spaces and pad right the other half
            return stringToCenter.PadLeft(padLeft).PadRight(lengthOfField);
        }
    }
}
