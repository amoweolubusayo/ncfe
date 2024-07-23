using Moq;
using Ncfe.CodeTest.Interfaces;
using Ncfe.CodeTest.Models;
using Ncfe.CodeTest.Services;

namespace TestProject1;

[TestFixture]
    public class ArchivedDataServiceTests
    {
        private ArchivedDataService _archivedDataService;
        private Mock<IArchivedDataService> _mockArchivedDataService;

        [SetUp]
        public void Setup()
        {
            _archivedDataService = new ArchivedDataService();
            _mockArchivedDataService = new Mock<IArchivedDataService>();
        }

        [Test]
        public void GetArchivedLearner_ReturnsValidLearner()
        {
            // Arrange
            const int learnerId = 1;

            // Act
            var result = _archivedDataService.GetArchivedLearner(learnerId);

            // Assert
            Assert.That(result, Is.Not.Null, "Expected GetArchivedLearner to return a non-null result.");
            Assert.That(result, Is.InstanceOf<Learner>(), "Expected result to be of type Learner.");
        }

        [Test]
        public void GetArchivedLearner_ReturnsExpectedLearner()
        {
            // Arrange
            const int learnerId = 1;
            var expectedLearner = new Learner();
            _mockArchivedDataService.Setup(service => service.GetArchivedLearner(learnerId))
                .Returns(expectedLearner);

            // Act
            var result = _archivedDataService.GetArchivedLearner(learnerId);

            // Assert
            Assert.That(result, Is.Not.Null, "Expected GetArchivedLearner to return a non-null result.");
            Assert.That(result.Id, Is.EqualTo(expectedLearner.Id));
        }
    }
    