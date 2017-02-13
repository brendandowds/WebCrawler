using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCrawler
{
    [TestClass]
    public class TestWebCrawler
    {
        string invalidResult = "The URL entered is not valid. Please try a different one.";
        [TestMethod]
        public void Crawl_NullURL_ReturnsErrorMessage()
        {
            IEnumerable<string> results = WebCrawler.Crawl(null);

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(invalidResult, results.ElementAt(0));
        }

        [TestMethod]
        public void Crawl_EmptyURL_ReturnsErrorMessage()
        {
            IEnumerable<string> results = WebCrawler.Crawl("");

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(invalidResult, results.ElementAt(0));
        }

        [TestMethod]
        public void Crawl_InvalidURL_ReturnsErrorMessage()
        {
            IEnumerable<string> results = WebCrawler.Crawl("An_invalid url");

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(invalidResult, results.ElementAt(0));
        }

        [TestMethod]
        public void Crawl_ExceptionHandled()
        {
            IEnumerable<string> results = WebCrawler.Crawl("http://something.com"); // Not good as it depends on this site not being used. 

            Assert.AreEqual(2, results.Count());
            Assert.IsTrue(results.ElementAt(0).StartsWith("An error occured in crawling the domain "));
        }
        
        [TestMethod]
        public void Crawl_URL_GetsDomainAsFirstResult()
        {
            IEnumerable<string> results = WebCrawler.Crawl("http://www.google.co.uk/something/or/other");

            Assert.IsTrue(results.Count() > 1);
            Assert.AreEqual("http://www.google.co.uk", results.ElementAt(0));
        }
    }
}
