using Ncfe.CodeTest.Interfaces;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.DataAccess
{
    public class LearnerDataAccess : ILearnerDataAccess
    {
        public LearnerResponse LoadLearner(int learnerId)
        {
            // Retrieve learner from 3rd party web service
            return new LearnerResponse();
        }
    }
}