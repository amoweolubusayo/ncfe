using Ncfe.CodeTest.Interfaces;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Services
{
    public class LearnerService
    {
        private readonly IArchivedDataService _archivedDataService;
        private readonly ILearnerDataAccess _learnerDataAccess;
        private readonly IFailoverLearnerDataAccess _failoverLearnerDataAccess;
        private readonly IFailoverService _failoverService;

        public LearnerService(
            IArchivedDataService archivedDataService,
            ILearnerDataAccess learnerDataAccess,
            IFailoverLearnerDataAccess failoverLearnerDataAccess,
            IFailoverService failoverService)
        {
            _archivedDataService = archivedDataService;
            _learnerDataAccess = learnerDataAccess;
            _failoverLearnerDataAccess = failoverLearnerDataAccess;
            _failoverService = failoverService;
        }

        public Learner GetLearner(int learnerId, bool isLearnerArchived)
        {
            if (isLearnerArchived)
            {
                return _archivedDataService.GetArchivedLearner(learnerId);
            }

            LearnerResponse learnerResponse = _failoverService.ShouldUseFailover()
                ? _failoverLearnerDataAccess.GetLearnerById(learnerId)
                : _learnerDataAccess.LoadLearner(learnerId);

            return learnerResponse.IsArchived
                ? _archivedDataService.GetArchivedLearner(learnerId)
                : learnerResponse.Learner;
        }
    }
}