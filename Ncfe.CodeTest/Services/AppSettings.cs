using System.Configuration;
using Ncfe.CodeTest.Interfaces;

namespace Ncfe.CodeTest.Services
{
    //utility
    public class AppSettings : IAppSettings
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}