using System;
using System.Linq;
using Ncfe.CodeTest.Interfaces;

namespace Ncfe.CodeTest.Services
{
    public class FailoverService : IFailoverService
    {
        private readonly IFailoverRepository _failoverRepository;
        private readonly IConfigurationService _configurationService;

        public FailoverService(IFailoverRepository failoverRepository, IConfigurationService configurationService)
        {
            _failoverRepository = failoverRepository;
            _configurationService = configurationService;
        }
        public bool ShouldUseFailover()
        {
            var failoverEntries = _failoverRepository.GetFailOverEntries();
            var failedRequests = failoverEntries.Count(f => f.DateTime > DateTime.Now.AddMinutes(-10));
            return failedRequests > 100 && _configurationService.IsFailoverModeEnabled();
        }
    }
}