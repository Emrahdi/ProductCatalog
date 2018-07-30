using Microsoft.Extensions.Logging;


namespace Utilities.Providers.LogProviders
{
    public interface ILog
    {
        void LogFormat(string txt, LogLevel logLevel, params object[] p);
    }
}
