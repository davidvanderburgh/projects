using System;
using System.Collections.Generic;
using System.Text;

namespace SpiralMemory
{
    public class SpiralMemoryLogic
    {
        public int GetNumberOfSteps(int value)
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
            // the array is populated right to left decrementing to the lowest value, then incrementing to the highest until the array is filled

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
