using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WordPlay.Classes;

namespace WordPlay
{
    [TestClass]
    public class WordFindTests
    {
        WordFind wordFind;
        HashSet<string> words;

        [TestInitialize]
        public void Initialize()
        {
            words = new HashSet<string>()
            {
                "quick",
                "brown",
                "fox",
                "jumped",
                "over",
                "fence"
            };
            wordFind = new WordFind(10, words);
        }

        [TestMethod]
        public void CheckWordAtCoordinateOrientation_Workingwords_true()
        {
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(0, 6), WordFind.Orientation.North));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(3, 7), WordFind.Orientation.NorthEast));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(3, 0), WordFind.Orientation.East));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(2, 4), WordFind.Orientation.SouthEast));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(3, 2), WordFind.Orientation.South));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(4, 1), WordFind.Orientation.SouthWest));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(5, 7), WordFind.Orientation.West));
            Assert.AreEqual(true, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(5, 5), WordFind.Orientation.NorthWest));
        }

        [TestMethod]
        public void CheckWordAtCoordinateOrientation_notWorkingWords_false()
        {
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(0, 0), WordFind.Orientation.North));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(0, 0), WordFind.Orientation.NorthEast));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(9, 0), WordFind.Orientation.East));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(7, 0), WordFind.Orientation.SouthEast));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(0, 8), WordFind.Orientation.South));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(3, 8), WordFind.Orientation.SouthWest));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(2, 4), WordFind.Orientation.West));
            Assert.AreEqual(false, wordFind.CheckWordAtCoordinateOrientation("quick", new Coordinate(2, 2), WordFind.Orientation.NorthWest));
        }

        [TestMethod]
        public void CheckLetterAtCoordinate_valid_true()
        {
            Assert.AreEqual(true, wordFind.CheckLetterAtCoordinate('q', new Coordinate(1, 1)));
            Assert.AreEqual(true, wordFind.CheckLetterAtCoordinate('q', new Coordinate(1, 1)));
            Assert.AreEqual(true, wordFind.CheckLetterAtCoordinate('u', new Coordinate(1, 2)));
            Assert.AreEqual(true, wordFind.CheckLetterAtCoordinate('i', new Coordinate(1, 3)));
            Assert.AreEqual(true, wordFind.CheckLetterAtCoordinate('c', new Coordinate(1, 4)));
            Assert.AreEqual(true, wordFind.CheckLetterAtCoordinate('k', new Coordinate(1, 5)));
        }

        [TestMethod]
        public void CheckLetterAtCoordinate_invalid_false()
        {
            Assert.AreEqual(false, wordFind.CheckLetterAtCoordinate('q', new Coordinate(-1, 1)));
            Assert.AreEqual(false, wordFind.CheckLetterAtCoordinate('u', new Coordinate(1, -2)));
            Assert.AreEqual(false, wordFind.CheckLetterAtCoordinate('i', new Coordinate(10, 3)));
            Assert.AreEqual(false, wordFind.CheckLetterAtCoordinate('c', new Coordinate(1, 40)));
            Assert.AreEqual(false, wordFind.CheckLetterAtCoordinate('k', new Coordinate(9, 10)));
        }

        [TestMethod]
        public void CoordinateIsOutOfBounds_valid_false()
        {
            Assert.AreEqual(false, wordFind.CoordinateIsOutOfBounds(new Coordinate(0, 0)));
            Assert.AreEqual(false, wordFind.CoordinateIsOutOfBounds(new Coordinate(8, 4)));
            Assert.AreEqual(false, wordFind.CoordinateIsOutOfBounds(new Coordinate(9, 9)));
        }

        [TestMethod]
        public void CoordinateIsOutOfBounds_invalid_true()
        {
            Assert.AreEqual(true, wordFind.CoordinateIsOutOfBounds(new Coordinate(-1, 0)));
            Assert.AreEqual(true, wordFind.CoordinateIsOutOfBounds(new Coordinate(10, 4)));
            Assert.AreEqual(true, wordFind.CoordinateIsOutOfBounds(new Coordinate(10, 10)));
        }
    }
}
