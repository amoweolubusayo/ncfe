using Moq;
using Ncfe.CodeTest.Interfaces;
using Ncfe.CodeTest.Models;
using Ncfe.CodeTest.Services;

namespace TestProject1;

public class LearnerServiceTests
{
      private Mock<ArchivedDataService> _mockArchivedDataService;
        private Mock<ILearnerDataAccess> _mockLearnerDataAccess;
        private Mock<IFailoverLearnerDataAccess> _mockFailoverLearnerDataAccess;
        private Mock<IFailoverService> _mockFailoverService;
        private LearnerService _learnerService;

        [SetUp]
        public void Setup()
        {
            _mockArchivedDataService = new Mock<ArchivedDataService>();
            _mockLearnerDataAccess = new Mock<ILearnerDataAccess>();
            _mockFailoverLearnerDataAccess = new Mock<IFailoverLearnerDataAccess>();
            _mockFailoverService = new Mock<IFailoverService>();
            _learnerService = new LearnerService(_mockArchivedDataService.Object, _mockLearnerDataAccess.Object,
                _mockFailoverLearnerDataAccess.Object,
                _mockFailoverService.Object);
        }
    
        [Test]
        public void ArchiveLearner_SuccessfullyArchivesLearner()
        {
            // Arrange
            const int learnerId = 1;
            var learner = new Learner { Id = learnerId, Name = "Olubusayo Amowe" };
            var learnerResponse = new LearnerResponse { Learner = learner };
            _mockLearnerDataAccess.Setup(lda => lda.LoadLearner(learnerId)).Returns(learnerResponse);

            // Act
            _learnerService.ArchiveLearner(learnerId);

            // Assert
            Assert.That(learner.IsArchived, Is.True, "Expected learner to be archived.");
        }
}