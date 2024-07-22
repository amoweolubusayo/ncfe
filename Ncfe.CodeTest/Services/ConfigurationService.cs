using System.Configuration;
using Ncfe.CodeTest.Interfaces;

namespace Ncfe.CodeTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public bool IsFailoverModeEnabled()
        {
            return ConfigurationManager.AppSettings["IsFailoverModeEnabled"]?.ToLower() == "true";
        }
    }
}