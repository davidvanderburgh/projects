using System;
using System.Collections.Generic;
using System.Text;

namespace A_Maze_of_Twisty_Trampolines
{
    public class Maze
    {
        public int[] StepArray { get; set; }
        public List<string> StepString { get; private set; }
        public int StepCounter { get; private set; }

        public Maze()
        {
            StepArray = new int[0];
            StepString = new List<string>();
        }

        public Maze(int[] stepArray)
        {
            StepArray = stepArray;
            StepString = new List<string>();
            ComputeResult();
        }

        private void ComputeResult()
        {
            int steps = 0;
            int currentIndex = 0;
            int nextIndex = 0;
            int[] workingStepArray = StepArray;
            AddStateToPrintOut(workingStepArray, 0);

            //check that the current index is within the array, if not then the loop ends
            while (currentIndex >= 0 && currentIndex < workingStepArray.Length)
            {
                //set the nextIndex to the currentIndex plus the value at the currentIndex
                nextIndex = currentIndex + workingStepArray[currentIndex];

                //increment the value at the currentIndex by 1
                workingStepArray[currentIndex]++;

                //add one to the number of steps
                steps++;

                //set the currentIndex to the nextIndex
                currentIndex = nextIndex;

                //add state to the printout
                AddStateToPrintOut(workingStepArray, currentIndex);
            }

            StepCounter = steps;
        }

        private void AddStateToPrintOut(int[] inputArray, int index)
        {
            // add each element, space separated with () around the current index
            string newLine = string.Empty;
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (i == index)
                {
                    newLine += "(" + inputArray[i] + ") ";
                }
                else
                {
                    newLine += inputArray[i] + " ";
                }
            }
            StepString.Add(newLine);
        }

        public override string ToString()
        {
            return string.Join('\n', StepString);
        }
    }
}
