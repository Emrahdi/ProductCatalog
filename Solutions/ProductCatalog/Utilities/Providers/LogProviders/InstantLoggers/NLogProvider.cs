using System;
using Microsoft.Extensions.Logging;

namespace Utilities.Providers.LogProviders.InstantLoggers
{
    /// <summary>
    /// todo:can be implemented as a option to log4net logging
    /// </summary>
    public class NLogProvider : ILog
    {
        public void LogFormat(string txt, LogLevel logLevel, params object[] p)
        {
            throw new NotImplementedException();
        }
    }
}
