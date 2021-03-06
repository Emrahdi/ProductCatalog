﻿

namespace Utilities.Providers.DbConnectionProvider {
    /// <summary>
    /// Db Connection provider for entity framework.
    /// </summary>
    public interface IEFDbConnectionProvider {
        string GetConnectionString();
        T GetConnectionProperty<T>(string propertyName);
    }
}
