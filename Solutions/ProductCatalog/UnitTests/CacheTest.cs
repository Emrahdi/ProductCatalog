using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using Utilities.Cache;
using WebApi.Models;

namespace UnitTests {
    [TestClass]
    public class CacheTests {

        [TestMethod]
        public void TestCache() {
            TestCache test = new TestCache();
            test.Add("ProductCode1", CommonTestData.GetTestData("1"));
            test.Add("ProductCode2", CommonTestData.GetTestData("2"));
            test.Add("ProductCode3", CommonTestData.GetTestData("3"));
            Product p;
            test.TryGetValue("ProductCode2", out p);
            Assert.AreEqual(p.Code, "ProductCode2");
            test.Clear();
            Assert.AreEqual(test.Count, 0);

            test.Add("ProductCode1", CommonTestData.GetTestData("1"));
            test.Add("ProductCode2", CommonTestData.GetTestData("2"));
            test.Add("ProductCode3", CommonTestData.GetTestData("3"));
            Assert.AreEqual(test.ContainsKey("ProductCode1"), true);
            test.TryGetValue("ProductCode3", out p);
            Assert.AreEqual(test.Values.Contains(p), true);

            test.Remove("ProductCode1");
            Assert.AreEqual(test.ContainsKey("ProductCode1"), false);
            Assert.AreEqual(test.Count, 2);
        }
    }



    public class TestCache : MemoryCache<string, Product> {
        public TestCache()
           : base("TEST_CACHE") {

        }
        public static TestCache Instance {
            get { return SingletonProvider<TestCache>.Instance; }
        }
    }
}
