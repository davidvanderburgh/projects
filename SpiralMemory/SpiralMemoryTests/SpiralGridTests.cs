using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SpiralMemory.Tests
{
    [TestClass]
    public class SpiralGridTests
    {
        SpiralGrid spiralGrid;

        [TestInitialize]
        public void Initialize()
        {
            spiralGrid = new SpiralGrid();
        }

        [TestMethod]
        public void GetNumberOfStepsTests()
        {
            Assert.AreEqual(31, spiralGrid.GetNumberOfStepsToCenter(1024));
            Assert.AreEqual(2, spiralGrid.GetNumberOfStepsToCenter(23));
            Assert.AreEqual(2, spiralGrid.GetNumberOfStepsToCenter(23));
            Assert.AreEqual(3, spiralGrid.GetNumberOfStepsToCenter(12));
            Assert.AreEqual(0, spiralGrid.GetNumberOfStepsToCenter(1));
            Assert.AreEqual(4, spiralGrid.GetNumberOfStepsToCenter(21));
        }
    }
}
