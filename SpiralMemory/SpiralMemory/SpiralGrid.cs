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

        public SpiralGrid()
        {

        }

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

        public int GetNumberOfStepsToCenter(int value)
        {
            int lowerRoot = 0;
            int upperRoot = 0;
            int block = 0;

            //1. Determine what 2 roots the value is between (what box it's in)

            //set 1 as the epicenter (0 steps)
            if (value == 1)
            {
                return 0;
            }

            //finds lower odd square
            //determine the square number that the value is in
            for (int root = 1; root < value * value; root += 2)
            {
                if (value <= (root * root))
                {
                    lowerRoot = root - 2; //returns previous odd# root
                    upperRoot = root;
                    break;
                }
                block++;
            }

            //2. Create an array sized from the lowerRoot+1 to the UpperRoot
            int[] squareArray = new int[(upperRoot) * (upperRoot) - (lowerRoot) * (lowerRoot)];

            //3.  Fill the array with the 'bouncing' values
            // the highest value is 'upperRoot - 1'
            // the lowest value is the 'block' number
            // the highest value starts in the last element of the array
            // the array is populated right to left decrementing to the lowest value, then incrementing 
            //  to the highest until the array is filled

            int changeAmount = 1; //changes by 'changeAmount = -changeAmount' at max and min values
            int stepValue = (upperRoot - 1); //starts the steps at the highest amount

            //starting at the last element in the array until the first element
            for (int i = squareArray.Length - 1; i >= 0; i--)
            {
                //set the entry as the stepValue
                squareArray[i] = stepValue;

                //if the stepValue is at the extreme values, then reverse the changeAmount direction
                if (stepValue == (upperRoot - 1) || stepValue == block)
                {
                    changeAmount = -changeAmount;
                }

                //the value to put in the array for the next entry needs to change by the change amount
                stepValue += changeAmount;
            }
            //4.  Determine where in the 'bouncing' sequence the value is
            // the value is x amount of distance from the upperRoot^2
            // upperRoot^2 is at index length-1
            // value is at index ((length-1)-(upperRoot*upperRoot-value))

            return squareArray[(squareArray.Length - 1) - (upperRoot * upperRoot - value)];
        }
    }
}
