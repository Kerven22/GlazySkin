namespace Logging;

public interface ILoggerManager
{
    void Information(string message);
    void Debug(string message);
    void Warning(string message);
    void Error(string message);
}