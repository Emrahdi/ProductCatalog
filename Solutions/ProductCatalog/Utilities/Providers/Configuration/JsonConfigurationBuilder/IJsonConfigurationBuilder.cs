using Microsoft.Extensions.Configuration;


namespace Utilities.Providers.Configuration.JsonConfigurationBuilder
{
    /// <summary>
    /// Added for unit testing the configuration builder of json file
    /// </summary>
    public interface IJsonConfigurationBuilder
    {
        IConfigurationBuilder BuildJsonFile();
    }
}
