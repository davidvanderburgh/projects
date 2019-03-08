using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SpiralMemory.Tests
{
    [TestClass]
    public class SpiralMemoryLogicTests
    {
        SpiralMemoryLogic spiralMemoryLogic;

        [TestInitialize]
        public void Initialize()
        {
            spiralMemoryLogic = new SpiralMemoryLogic();
        }

        [TestMethod]
        public void GetNumberOfStepsTests()
        {
            Assert.AreEqual(31, spiralMemoryLogic.GetNumberOfSteps(1024));
            Assert.AreEqual(2, spiralMemoryLogic.GetNumberOfSteps(23));
            Assert.AreEqual(2, spiralMemoryLogic.GetNumberOfSteps(23));
            Assert.AreEqual(3, spiralMemoryLogic.GetNumberOfSteps(12));
            Assert.AreEqual(0, spiralMemoryLogic.GetNumberOfSteps(1));
            Assert.AreEqual(4, spiralMemoryLogic.GetNumberOfSteps(21));
        }
    }
}
