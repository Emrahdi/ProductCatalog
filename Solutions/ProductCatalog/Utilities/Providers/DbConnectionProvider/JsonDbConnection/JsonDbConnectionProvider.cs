using Microsoft.Extensions.Configuration;
using Utilities.Providers.Configuration.JsonConfigurationBuilder;
using Utilities.ServiceLocator;

namespace Utilities.Providers.DbConnectionProvider.JsonDbConnection
{
    /// <summary>
    /// Gets db connection string and other connection properties
    /// from json configuration file.
    /// </summary>
    public class JsonDbConnectionProvider : IEFDbConnectionProvider
    {
        IConfigurationRoot Configuration { get; set; }
        string ConnectionName { get; set; }
        IConfigurationBuilder ConfigurationBuilder { get; set; }
        IJsonConfigurationBuilder JsonConfigurationBuilder { get; set; }
        /// <summary>
        /// Ctor for getting default connection string
        /// </summary>
        public JsonDbConnectionProvider()
        {
            BuildConfigurationFile();
            ConnectionName = Configuration.GetSection(Constants.ConnectionStringsKey).GetValue<string>(Constants.DefaultConnectionName);
        }
        public JsonDbConnectionProvider(string connectionName)
        {
            BuildConfigurationFile();
            ConnectionName = connectionName;
        }
        public JsonDbConnectionProvider(string connectionName, IJsonConfigurationBuilder jsonBuildProvider)
        {
            ConnectionName = connectionName;
            this.JsonConfigurationBuilder = jsonBuildProvider;
            ConfigurationBuilder = JsonConfigurationBuilder.BuildJsonFile();
            Configuration = ConfigurationBuilder.Build();
        }
        void BuildConfigurationFile()
        {
            ConfigurationBuilder = Services.Create<IJsonConfigurationBuilder>().BuildJsonFile();
            Configuration = ConfigurationBuilder.Build();
        }
        public string GetConnectionString()
        {
            return Configuration.GetConnectionString(ConnectionName);
        }
        public T GetConnectionProperty<T>(string propertyName)
        {
            string connectionStringDetailKey = string.Concat(ConnectionName + Constants.ConnectionStringDetailsTag);
            return Configuration.GetSection(Constants.ConnectionStringsKey).GetSection(connectionStringDetailKey).GetValue<T>(propertyName);
        }
    }
}
