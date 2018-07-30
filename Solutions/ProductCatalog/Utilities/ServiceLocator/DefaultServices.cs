using Utilities.Providers.Configuration.JsonConfigurationBuilder;
using Utilities.Providers.DbConnectionProvider;
using Utilities.Providers.DbConnectionProvider.JsonDbConnection;
using Utilities.Providers.LogProviders;
using Utilities.Providers.LogProviders.InstantLoggers;

namespace Utilities.ServiceLocator
{
    public class DefaultServices
    {
        public static void RegisterDefaultServices()
        {
            Services.Register<IDefaultDbProvider>(new JsonDefaultDbProvider());
            Services.Register<IJsonConfigurationBuilder>(new JsonConfigurationBuilder());
            Services.Register<ILog>(new Log4NetProvider());
        }

    }
}
