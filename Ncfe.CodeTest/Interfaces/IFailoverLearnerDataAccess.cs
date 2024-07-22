using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Interfaces
{
    public interface IFailoverLearnerDataAccess
    {
        LearnerResponse GetLearnerById(int id);
    }
}