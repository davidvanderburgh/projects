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
            SpiralMemoryLogic spiralMemoryLogic = new SpiralMemoryLogic();
            SpiralGrid spiralGrid = new SpiralGrid(400);
            
            foreach (string s in spiralGrid.Animation)
            {
                Console.Clear();
                Console.WriteLine(s);
                Thread.Sleep(10);
            }


            Console.WriteLine($"It takes {spiralMemoryLogic.GetNumberOfSteps(spiralGrid.MaxNumber)} steps to get from {spiralGrid.MaxNumber} to 1");
            Console.ReadLine();
        }
    }
}
