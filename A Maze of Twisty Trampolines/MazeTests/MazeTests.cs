using A_Maze_of_Twisty_Trampolines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazeTests
{
    [TestClass]
    public class MazeTests
    {
        Maze maze;

        [TestInitialize]
        public void Initialize()
        {
            maze = new Maze();
        }

        [TestMethod]
        public void StepCounterTests()
        {
            maze.StepArray = { 1, 0, 5, -2, -3 };
            Assert.AreEqual(4, maze.StepCounter());

            maze.StepArray = { 1, 0, 1, -2, -4 };
            Assert.AreEqual(11, maze.StepCounter());

            maze.StepArray = { 0, 3, 0, 1, -3 };
            Assert.AreEqual(5, maze.StepCounter());
        }
    }
}
