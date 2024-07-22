using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Interfaces
{
    public interface ILearnerDataAccess
    {
        LearnerResponse LoadLearner(int learnerId);
    }
}