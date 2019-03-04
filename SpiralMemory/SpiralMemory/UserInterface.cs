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
            Console.WriteLine(spiralMemoryLogic.GetNumberOfSteps(1024));
            Console.ReadLine();
        }
    }
}
