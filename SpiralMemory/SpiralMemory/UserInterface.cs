using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpiralMemory
{
    public class UserInterface
    {
        public void Run()
        {
            SpiralGrid spiralGrid = new SpiralGrid(40);
            
            foreach (string s in spiralGrid.Animation)
            {
                Console.Clear();
                Console.WriteLine(s);
                Thread.Sleep(20);
            }

            Console.WriteLine($"It takes {spiralGrid.GetNumberOfStepsToCenter(spiralGrid.MaxNumber)} steps to get from {spiralGrid.MaxNumber} to 1");
            Console.ReadLine();
        }
    }
}
