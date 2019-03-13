using A_Maze_of_Twisty_Trampolines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeTests
{
    [TestClass]
    public class MazeTests
    {
        [TestMethod]
        public void StepCounterTests()
        {
            int[] testArray1 = { 1, 0, 5, -2, -3 };
            Assert.AreEqual(4, Maze.StepCounter(testArray1));

            int[] testArray2 = { 1, 0, 1, -2, -4 };
            Assert.AreEqual(11, Maze.StepCounter(testArray2));


            int[] testArray3 = { 0, 3, 0, 1, -3 };
            Assert.AreEqual(5, Maze.StepCounter(testArray3));
        }
    }
}
