using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Naspinski.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Naspinski.Utilities.Tests
{
    [TestClass]
    public class EnumUtilitiesTests
    {
        enum Testing { Abc = 1, Def = 5 }

        [TestMethod]
        public void CanLoopEnums()
        {
            int count = 0;
            var enums = EnumUtilities.GetValues<Testing>();

            Assert.AreEqual(enums.Count(), 2);
            Assert.AreEqual(enums.First().ToString(), "Abc");
            Assert.AreEqual((int)enums.Last(), 5);
        }
    }
}
