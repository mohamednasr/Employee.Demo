using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Interfaces;

namespace Infrastructure.Logger
{
    public class LoggerService : IloggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogError(Exception ex, string msg)
        {
            logger.Error(ex, msg);
        }

        public void LogInfo(string msg)
        {
            logger.Info(msg);
        }

        public void LogWarning(string msg)
        {
            logger.Warn(msg);
        }

        public void LogDebug(string msg)
        {
            logger.Debug(msg);
        }
    }
}
