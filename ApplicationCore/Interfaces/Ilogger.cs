using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IloggerService
    {
        void LogInfo(string msg);
        void LogWarning(string msg);
        void LogDebug(string msg);
        void LogError(Exception ex ,string msg);
    }
}
