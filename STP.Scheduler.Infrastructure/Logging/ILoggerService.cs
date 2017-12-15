using System;

namespace STP.Scheduler.Infrastructure.Logging
{
  public interface ILoggerService
  {
    void LogDebug(string debug);
    void LogInformation(string info);
    void LogError(Exception ex, string message);
  }
}