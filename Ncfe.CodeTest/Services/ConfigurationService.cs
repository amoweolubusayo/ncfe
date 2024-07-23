using System.Configuration;
using Ncfe.CodeTest.Interfaces;

namespace Ncfe.CodeTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IAppSettings _appSettings;
        public ConfigurationService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public bool IsFailoverModeEnabled()
        {
            return ConfigurationManager.AppSettings["IsFailoverModeEnabled"]?.ToLower() == "true";
        }
    }
}