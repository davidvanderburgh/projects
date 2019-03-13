using Microsoft.VisualStudio.TestTools.UnitTesting;
using CorruptionChecksum.Logic;
using System.Collections.Generic;

namespace CorruptionChecksum.Test
{
    [TestClass]
    public class CorruptionCheckSumTests
    {
        CorruptionChecksumLogic corruptionChecksumLogic;

        [TestInitialize]
        public void Initialize()
        {
            corruptionChecksumLogic = new CorruptionChecksumLogic();
        }

        [TestMethod]
        public void GetChecksumTests()
        {
            List<int[]> testArray1 = new List<int[]>
            {
                new [] {5, 1, 9, 5},
                new [] {7, 5, 3},
                new [] {2, 4, 6, 8}
            };

            List<int[]> testArray2 = new List<int[]>
            {
                new [] {10, 10},
                new [] {7, -5, 0},
                new [] {8},
                new [] {0}
            };

            List<int[]> testArray3 = new List<int[]>
            {
                new [] {6, 3, 18, 24 }
            };

            Assert.AreEqual(18, corruptionChecksumLogic.GetChecksum(testArray1));
            Assert.AreEqual(12, corruptionChecksumLogic.GetChecksum(testArray2));
            Assert.AreEqual(21, corruptionChecksumLogic.GetChecksum(testArray3));
        }
    }
}
