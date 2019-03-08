using System;
using System.Collections.Generic;
using System.Text;

namespace SpiralMemory
{
    public class UserInterface
    {
        public void Run()
        {
            SpiralMemoryLogic spiralMemoryLogic = new SpiralMemoryLogic();
            SpiralGrid spiralGrid = new SpiralGrid(115);
            Console.WriteLine(spiralGrid);

            Console.WriteLine(spiralMemoryLogic.GetNumberOfSteps(115));
            Console.ReadLine();
        }
    }
}
