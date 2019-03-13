﻿using System;
using System.Collections.Generic;
using System.Text;

namespace A_Maze_of_Twisty_Trampolines
{
    public static class Maze
    {
        public static int StepCounter(int[] stepArray)
        {
            int steps = 0;
            int currentIndex = 0;
            int nextIndex = 0;
            int[] workingStepArray = stepArray;

            //check that the current index is within the array, if not then the loop ends
            // set the nextIndex to the currentIndex plus (the value at the currentIndex)
            // increment the value at the currentIndex by 1
            // add one to the number of steps
            // set the currentIndex to the nextIndex
            // loop around

            while (currentIndex >= 0 && currentIndex < workingStepArray.Length)
            {
                nextIndex = currentIndex + workingStepArray[currentIndex];
                workingStepArray[currentIndex]++;
                steps++;
                currentIndex = nextIndex;
            }

            return steps;
        }
    }
}
