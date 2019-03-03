using Microsoft.VisualStudio.TestTools.UnitTesting;
using InverseCaptcha;

namespace InverseCaptchaTest
{
    [TestClass]
    public class InverseCaptchaTests
    {
        [TestMethod]
        public void EveryCharacterIsANumberTests_PassingScenarios_True()
        {
            Assert.IsTrue(InverseCaptchaLogic.EveryCharacterIsANumber("1230987129381237983242893123"), "Should be considered a number");
            Assert.IsTrue(InverseCaptchaLogic.EveryCharacterIsANumber("1"), "Should be considered a number");
            Assert.IsTrue(InverseCaptchaLogic.EveryCharacterIsANumber("4321"), "Should be considered a number");
            Assert.IsTrue(InverseCaptchaLogic.EveryCharacterIsANumber("543211222"), "Should be considered a number");
            Assert.IsTrue(InverseCaptchaLogic.EveryCharacterIsANumber("3333"), "Should be considered a number");
        }
        [TestMethod]
        public void EveryCharacterIsANumberTests_FailingScenarios_False()
        {
            Assert.IsFalse(InverseCaptchaLogic.EveryCharacterIsANumber("-1230987129381237983242893123"), "Includes '-', should not be considered a number");
            Assert.IsFalse(InverseCaptchaLogic.EveryCharacterIsANumber("123081381-2123912839"), "Includes '-', should not be considered a number");
            Assert.IsFalse(InverseCaptchaLogic.EveryCharacterIsANumber("saa3"), "Should not be considered a number");
            Assert.IsFalse(InverseCaptchaLogic.EveryCharacterIsANumber("f"), "Should not be considered a number");
            Assert.IsFalse(InverseCaptchaLogic.EveryCharacterIsANumber("32p"), "Should not be considered a number");
        }

        [TestMethod]
        public void SequenceSumTests()
        {
            Assert.AreEqual(3, InverseCaptchaLogic.SequenceSum("1122"));
            Assert.AreEqual(4, InverseCaptchaLogic.SequenceSum("1111"));
            Assert.AreEqual(0, InverseCaptchaLogic.SequenceSum("1234"));
            Assert.AreEqual(9, InverseCaptchaLogic.SequenceSum("91212129"));
            Assert.AreEqual(9, InverseCaptchaLogic.SequenceSum("912121212121212121212121212121212121212121212121212129"));
            Assert.AreEqual(18, InverseCaptchaLogic.SequenceSum("99"));
        }

        [TestMethod]
        public void GetIntArrayFromStringTests()
        {
            CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, InverseCaptchaLogic.GetIntArrayFromString("123"));
            CollectionAssert.AreEqual(new int[] { 1 }, InverseCaptchaLogic.GetIntArrayFromString("1"));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 5, 6, 7 }, InverseCaptchaLogic.GetIntArrayFromString("123567"));
            CollectionAssert.AreEqual(new int[] { 1, 1, 1, 1}, InverseCaptchaLogic.GetIntArrayFromString("1111"));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 1, 2, 3 }, InverseCaptchaLogic.GetIntArrayFromString("123123"));
        }
    }
}
