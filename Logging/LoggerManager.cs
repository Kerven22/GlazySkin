using ILogger = Serilog.ILogger;

namespace Logging;

public class LoggerManager:ILoggerManager
{
    private static readonly ILogger _logger;

    public LoggerManager()
    {
        
    }
    public void Information(string message)
    {
        _logger.Information(message);
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Warning(string message)
    {
        _logger.Warning(message);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }
}