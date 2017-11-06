using Chapter1ArraysAndStrings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter16Tests
{
    [TestClass]
    public class ArraysAndStringsTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsPermutationTestSecondInputIsNullOrWhitespace()
        {
            // Arrange
            String string1 = "boot";
            String string2 = " ";
            // Act
            try
            {
                var actual = ArraysAndStrings.IsPermutation(string1, string2);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Please enter a non-empty string.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsPermutationTestFirstInputIsNullOrWhitespace()
        {
            // Arrange
            String string1 = null;
            String string2 = "boot";

            // Act
            try
            {
                var actual = ArraysAndStrings.IsPermutation(string1, string2);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Please enter a non-empty string.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUnique2TestInputIsValidAndUniqiue()
        {
            // Arrange
            String input = "chant";
            bool expected = true;

            // Act
            var actual = ArraysAndStrings.IsUnique(input);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUnique2TestInputIsValidAndNotUniqiue()
        {
            // Arrange
            String input = "manhattan";
            bool expected = false;

            // Act
            var actual = ArraysAndStrings.IsUnique(input);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUnique2TestInputIsNullOrWhitespace()
        {
            // Arrange
            String input = " ";

            // Act
            try
            {
                var actual = ArraysAndStrings.IsUnique2(input);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Please enter a non-empty string.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUnique2TestInputIsNullOrEmpty()
        {
            // Arrange
            String input = null;

            // Act
            try
            {
                var actual = ArraysAndStrings.IsUnique2(input);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Please enter a non-empty string.", ex.Message);
                throw;
            }   
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUniqueTestInputIsValidAndUniqiue()
        {
            // Arrange
            String input = "chant";
            bool expected = true;

            // Act
            var actual = ArraysAndStrings.IsUnique(input);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUniqueTestInputIsValidAndNotUniqiue()
        {
            // Arrange
            String input = "manhattan";
            bool expected = false;

            // Act
            var actual = ArraysAndStrings.IsUnique(input);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUniqueTestInputIsWhitespace()
        {
            // Arrange
            String input = " ";

            // Act
            try
            {
                var actual = ArraysAndStrings.IsUnique(input);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Please enter a non-empty string.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsUniqueTestInputIsNullOrEmpty()
        {
            // Arrange
            String input = null;

            // Act
            try
            {
                var actual = ArraysAndStrings.IsUnique(input);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Please enter a non-empty string.", ex.Message);
                throw;
            }
        }
    }
}
