using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Interfaces
{
    public interface IArchivedDataService
    {
        Learner GetArchivedLearner(int learnerId);
    }
}