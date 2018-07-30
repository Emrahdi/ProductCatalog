using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Utilities.Providers.Configuration.JsonConfigurationBuilder;
using Utilities.Providers.DbConnectionProvider.JsonDbConnection;

namespace UnitTests
{
    [TestClass]
    public class DbConnectiontests
    {
        public static string GetConnectionStringForTest()
        {
            var mockDependency = new Mock<IJsonConfigurationBuilder>();
            var dict = new Dictionary<string, string> { { "ConnectionStrings:ProductCatalog", "test" } };
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(dict);
            mockDependency.Setup(x => x.BuildJsonFile())
                          .Returns(builder);
            JsonDbConnectionProvider provider = new JsonDbConnectionProvider("ProductCatalog", mockDependency.Object);
            var connectionString = provider.GetConnectionString();
            return connectionString;
        }
        [TestMethod]
        public void TestConnectionString()
        {
            var connectionString = GetConnectionStringForTest();
            Assert.AreEqual(connectionString, "test");
        }
        [TestMethod]
        public void TestConnectionDetails()
        {
            var mockDependency = new Mock<IJsonConfigurationBuilder>();
            var dict = new Dictionary<string, string> { { "ConnectionStrings:ProductCatalogConnection_Details:CommandTimeout", "100" } };
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(dict);
            mockDependency.Setup(x => x.BuildJsonFile())
                          .Returns(builder);
            JsonDbConnectionProvider provider = new JsonDbConnectionProvider("ProductCatalogConnection", mockDependency.Object);
            var commandTimeout = provider.GetConnectionProperty<int>("CommandTimeout");
            Assert.AreEqual(commandTimeout, 100);
        }
    }
}
