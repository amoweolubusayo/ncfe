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
        
        [Test]
        public void GetLearner_ShouldReturnArchivedLearner_WhenIsLearnerArchivedIsTrue()
        {
            // Arrange
            const int learnerId = 1;
            const bool isLearnerArchived = true;
            var expectedLearner = new Learner();

            // Act
            var result = _learnerService.GetLearner(learnerId, isLearnerArchived);

            // Assert
            Assert.That(result.Id, Is.EqualTo(expectedLearner.Id));
            Assert.That(result.Name, Is.EqualTo(expectedLearner.Name));
            Assert.That(result.IsArchived, Is.EqualTo(expectedLearner.IsArchived));
        }
        
        [Test]
        public void GetLearner_ShouldReturnArchivedLearner_WhenLearnerResponseIsArchived()
        {
            // Arrange
            const int learnerId = 1;
            const bool isLearnerArchived = false;
            var archivedLearner = new Learner();
            var learnerResponse = new LearnerResponse { IsArchived = true };
            _mockLearnerDataAccess.Setup(d => d.LoadLearner(learnerId)).Returns(learnerResponse);

            // Act
            var result = _learnerService.GetLearner(learnerId, isLearnerArchived);

            // Assert
            Assert.That(result.Id, Is.EqualTo(archivedLearner.Id));
            Assert.That(result.Name, Is.EqualTo(archivedLearner.Name));
            Assert.That(result.IsArchived, Is.EqualTo(archivedLearner.IsArchived));
        }
        [Test]
        public void GetLearner_ShouldUseFailover_WhenFailoverServiceReturnsTrue()
        {
            // Arrange
            const int learnerId = 1;
            const bool isLearnerArchived = false;
            var expectedLearner = new Learner();
            var learnerResponse = new LearnerResponse { IsArchived = false, Learner = expectedLearner };
            _mockFailoverService.Setup(s => s.ShouldUseFailover()).Returns(true);
            _mockFailoverLearnerDataAccess.Setup(d => d.GetLearnerById(learnerId)).Returns(learnerResponse);

            // Act
            var result = _learnerService.GetLearner(learnerId, isLearnerArchived);

            // Assert
            Assert.AreEqual(expectedLearner, result);
            _mockFailoverLearnerDataAccess.Verify(d => d.GetLearnerById(learnerId), Times.Once);
        }
        
        [Test]
        public void GetLearner_ShouldUseNormalDataAccess_WhenFailoverServiceReturnsFalse()
        {
            // Arrange
            const int learnerId = 1;
            const bool isLearnerArchived = false;
            var expectedLearner = new Learner();
            var learnerResponse = new LearnerResponse { IsArchived = false, Learner = expectedLearner };
            _mockFailoverService.Setup(s => s.ShouldUseFailover()).Returns(false);
            _mockLearnerDataAccess.Setup(d => d.LoadLearner(learnerId)).Returns(learnerResponse);

            // Act
            var result = _learnerService.GetLearner(learnerId, isLearnerArchived);

            // Assert
            Assert.AreEqual(expectedLearner, result);
            _mockLearnerDataAccess.Verify(d => d.LoadLearner(learnerId), Times.Once);
        }
        
}