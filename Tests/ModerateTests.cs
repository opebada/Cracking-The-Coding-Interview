using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Chapter16_Moderate.Moderate;

namespace Chapter16Tests
{
    [TestClass]
    public class ModerateTests
    {
        [TestMethod]
        public void TwoSumTest()
        {
            int[] array1 = { 1, 2, 3, 5, 6, 7, 8, 9 };
            Console.WriteLine("TwoSum: ", TwoSum(array1, 14));
            bool expected = true;

            bool actual = TwoSum(array1, 14);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContiguousSequenceTest()
        {
            // Arrange
            int[] arr = { 2, -8, 3, -2, 4, -10 };
            int expected = 5;

            // Act
            var actual = ContiguousSequence(arr);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SubSortTest()
        {
            // Arrange
            int[] arr = { 1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19 };
            //int[] arr = { 1, 2, 4, 7, 10, 11, 8, 12, 5, 6, 16, 18, 19 };

            var expected = new Tuple<int, int>(3, 9);

            // Act
            var actual = SubSort(arr);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MasterMindTest()
        {
            // Arrange
            string solution = "RGBY";
            string guess = "GGRR";
            MasterMindResult expected = new MasterMindResult() { Hits = 1, PseudoHits = 1 };

            // Act
            MasterMindResult actual = MasterMind(solution, guess);

            // Assert
            Assert.AreEqual(expected.Hits, actual.Hits);
            Assert.AreEqual(expected.PseudoHits, actual.PseudoHits);
        }

        [TestMethod]
        public void DivisionTest()
        {
            int a = 12;
            int b = 3;
            int expected = 4;

            int actual = Division(a, b);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SubtractionTest()
        {
            // Arrange
            int a = 9;
            int b = 7;
            int expected = 2;

            // Act
            int actual = Subtraction(a, b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            // Arrange
            int a = 9;
            int b = 7;
            int expected = 63;

            // Act
            int actual = Multiply(a, b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnglishIntTest()
        {
            // Arrange
            int num = 1890456321;
            string expected = "";

            // Act
            var actual = EnglishInt(num);

            // Assert
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void NumberMaxTest()
        {
            // Arrange
            int a = 5;
            int b = -2;
            int expected = 5;

            // Act
            int actual = NumberMax(a, b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SmallestDifferenceTestArraysEmpty()
        {
            // Arrange
            int[] arr1 = {};
            int[] arr2 = {};
            int expected = -1;

            // Act
            var actual = SmallestDifference(arr1, arr2);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SmallestDifferenceTestArraysNotEmpty()
        {
            // Arrange
            int[] arr1 = { 1, 3, 15, 11, 2 };
            int[] arr2 = { 23, 127, 235, 19, 8 };
            int expected = 3;

            // Act
            var actual = SmallestDifference(arr1, arr2);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FactorialZerosTestNumLessThanZero()
        {
            // Arrange
            int num = -5;
            int expect = -1;

            // Act
            var actual = FactorialZeros(num);

            // Assert
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void FactorialZerosTestNumGreaterThanZero()
        {
            // Arrange
            int num = 5;
            int expect = 1;

            // Act
            var actual = FactorialZeros(num);

            // Assert
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WordFrequencyTestWordIsNullOrEmpty()
        {
            // Arrange
            string[] words = { "You", "called", "me", "out", "upon", "the", "waters", "the", "great", "unknown" };
            string word = "";

            // Act
            var actual = WordFrequency(words, word);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WordFrequencyTestWordsIsNullOrEmpty()
        {
            // Arrange
            string[] words = null;
            string word = "upon";

            // Act
            var actual = WordFrequency(words, word);

            // Assert
        }

        [TestMethod]
        public void WordFrequencyTestValid()
        {
            // Arrange
            string[] words = { "You", "called", "me", "out", "upon", "the", "waters", "the", "great", "unknown" };
            string word = "upon";
            int expected = 1;

            // Act
            var actual = WordFrequency(words, word);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NumberSwapper2Test()
        {
            // Arrange
            int a = 9;
            int b = 4;
            Result expected = new Result(b, a);

            // Act
            var actual = NumberSwapper2(a, b);

            // Assert
            Assert.AreEqual(expected.A, actual.A);
            Assert.AreEqual(expected.B, actual.B);
        }

        [TestMethod]
        public void NumberSwapperTest()
        {
            // Arrange
            int a = 9;
            int b = 4;
            Result expected = new Result(b, a);

            // Act
            var actual = NumberSwapper(a, b);

            // Assert
            Assert.AreEqual(expected.A, actual.A);
            Assert.AreEqual(expected.B, actual.B);
        }
    }
}
