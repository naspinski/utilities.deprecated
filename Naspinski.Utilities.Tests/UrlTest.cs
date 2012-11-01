using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Naspinski.Utilities.Tests
{
    [TestClass]
    public class UrlTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var url = Url.Combine("http://naspinski.net/", "directory/", "////image.png");
            var url2 = new Uri("http://naspinski.net/directory/image.png");
            Assert.AreEqual(url, url2);

            url = Url.Combine("http://naspinski.net/", "directory/image.png");
            Assert.AreEqual(url, url2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NoArgs()
        {
            var url = Url.Combine();
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void BadUri()
        {
            var url = Url.Combine("not a uri");
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void BadUri2()
        {
            var url = Url.Combine("not_a_uri", "something_else");
        }
    }
}
