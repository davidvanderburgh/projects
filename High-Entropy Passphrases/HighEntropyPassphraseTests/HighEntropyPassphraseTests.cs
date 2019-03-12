using Microsoft.VisualStudio.TestTools.UnitTesting;
using High_Entropy_Passphrases;

namespace HighEntropyPassphraseTests
{
    [TestClass]
    public class HighEntropyPassphraseTests
    {
        HighEntropyPassphrase highEntropyPassphrase;

        [TestInitialize]
        public void Initialize()
        {
            highEntropyPassphrase = new HighEntropyPassphrase();
        }

        [TestMethod]
        public void IsValid_goodphrases_true()
        {
            highEntropyPassphrase.PassPhrase = "a s d f gg ff dd ss aa";
            Assert.IsTrue(highEntropyPassphrase.IsValid());
                
            highEntropyPassphrase.PassPhrase = "all good code here";
            Assert.IsTrue(highEntropyPassphrase.IsValid());

            highEntropyPassphrase.PassPhrase = "why is my dog barking again?";
            Assert.IsTrue(highEntropyPassphrase.IsValid());

            highEntropyPassphrase.PassPhrase = "shortphrase";
            Assert.IsTrue(highEntropyPassphrase.IsValid());
        }

        [TestMethod]
        public void IsValid_badphrases_false()
        {
            highEntropyPassphrase.PassPhrase = "a s d f gg ff dd ss aa a";
            Assert.IsFalse(highEntropyPassphrase.IsValid());

            highEntropyPassphrase.PassPhrase = "all good code here but maybe not all of it";
            Assert.IsFalse(highEntropyPassphrase.IsValid());

            highEntropyPassphrase.PassPhrase = "why is my dog barking again? why is my dog barking again?";
            Assert.IsFalse(highEntropyPassphrase.IsValid());
        }
    }
}
