using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryRaschet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRaschet.Tests
{
    [TestClass()]
    public class TverdTeloTests
    {
        private TverdTelo _tverdTelo;

        [TestInitialize]
        public void Setup()
        {
            _tverdTelo = new TverdTelo();
        }

        [TestMethod()]
        public void EntPitWatTest()
        {
            // Arrange
            double tempPitWat = 30;
            double expected = tempPitWat * (2 * Math.Pow(10, -7) * tempPitWat + Math.Pow(10, -4) * tempPitWat + 1.495);

            // Act
            double actual = _tverdTelo.EntPitWat(tempPitWat);

            // Assert
            Assert.AreEqual(expected, actual, 1e-6);
        }
    }
}