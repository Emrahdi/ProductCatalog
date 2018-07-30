using Microsoft.Extensions.Configuration;
using Utilities.Providers.Configuration.JsonConfigurationBuilder;

namespace Utilities.Providers.DbConnectionProvider.JsonDbConnection
{
    /// <summary>
    /// Gets the provider name from default json configuration file,
    /// appsettings.json
    /// </summary>
    public class JsonDefaultDbProvider : IDefaultDbProvider
    {
        IConfigurationBuilder ConfigurationBuilder { get; set; }
        IConfigurationRoot Configuration { get; set; }
        public string GetDefaultDbProviderName()
        {
            JsonConfigurationBuilder builder = new JsonConfigurationBuilder();
            ConfigurationBuilder = builder.BuildJsonFile();
            Configuration = ConfigurationBuilder.Build();
            return Configuration.GetValue<string>(Constants.DbConnectionProviderKey);
        }
    }
}
