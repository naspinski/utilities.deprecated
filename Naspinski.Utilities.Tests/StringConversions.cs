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

        [TestMethod]
        public void SplitCamelCase()
        {
            string cc1 = "StanRulesTheUSAF";
            Assert.AreEqual(cc1.SplitCamelCase(), "Stan Rules The USAF");

            string cc2 = "testTestTest";
            Assert.AreEqual(cc2.SplitCamelCase(), "test Test Test");
        }

        [TestMethod]
        public void Between()
        {
            var str = "<p><span style=\"text-decoration:underline;font-size:x-large;\"><strong>[[EventName]]</strong></span></p><p>[[Exclusivity_Window]]</p><ul><li>asdfkhjasl jfhasl fjhasljf halsjfdh alfhj lad</li></ul><p>BLA BLA BLAkj</p>";
            var between = str.Between("[[", "]]").ToArray();

            Assert.AreEqual(2, between.Length);
            Assert.AreEqual(between.First(), "EventName");
            Assert.AreEqual(between.Last(), "Exclusivity_Window");

            str = "((a)a)))bbbb(((cccc)()()((as))df((d))asdfadf";
            between = str.Between("((", "))").ToArray();
            Assert.AreEqual(between.Length, 3);
            Assert.AreEqual(between[0], "a)a");
            Assert.AreEqual(between[1], "(cccc)()()((as");
            Assert.AreEqual(between[2], "d");
        }
    }
}
