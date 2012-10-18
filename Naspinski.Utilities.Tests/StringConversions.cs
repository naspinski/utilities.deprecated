using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Naspinski.Utilities.Tests
{
    public enum TestEnums
    {
        Abc,
        Efg,
        Hij
    }

    [TestClass]
    public class StringConversions
    {
        [TestMethod]
        public void ToEnums()
        {
            Assert.AreEqual("abc".ToEnum<TestEnums>(), TestEnums.Abc);
            Assert.AreEqual("Efg".ToEnum<TestEnums>(), TestEnums.Efg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToEnumsFailures()
        {
            var x = "abcd".ToEnum<TestEnums>();
        }
    }
}
