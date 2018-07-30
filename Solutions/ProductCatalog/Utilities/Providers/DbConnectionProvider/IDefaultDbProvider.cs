using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Providers.DbConnectionProvider
{
    /// <summary>
    /// Gets the source of the DbProvider properties.Provider Name etc.
    /// </summary>
    public interface IDefaultDbProvider
    {
        string GetDefaultDbProviderName();
    }
}
