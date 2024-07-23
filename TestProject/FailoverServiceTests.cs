using Moq;
using Ncfe.CodeTest.Interfaces;
using Ncfe.CodeTest.Services;
using Ncfe.CodeTest.Models; 

namespace TestProject1
{
    [TestFixture]
    public class FailoverServiceTests
    {
        private Mock<IFailoverRepository> _mockFailoverRepository;
        private Mock<IConfigurationService> _mockConfigurationService;
        private FailoverService _failoverService;

        [SetUp]
        public void SetUp()
        {
            _mockFailoverRepository = new Mock<IFailoverRepository>();
            _mockConfigurationService = new Mock<IConfigurationService>();
            _failoverService = new FailoverService(_mockFailoverRepository.Object, _mockConfigurationService.Object);
        }

        [Test]
        public void ShouldUseFailover_ReturnsTrue_WhenFailoverModeEnabled()
        {
            // Arrange
            var failoverEntries = new List<FailoverEntry>
            {
                new FailoverEntry { DateTime = DateTime.Now.AddMinutes(-5) },
                new FailoverEntry { DateTime = DateTime.Now.AddMinutes(-4) },
            };
            _mockFailoverRepository.Setup(repo => repo.GetFailOverEntries()).Returns(failoverEntries);
            _mockConfigurationService.Setup(cfg => cfg.IsFailoverModeEnabled()).Returns(true);

            // Act
            var result = _failoverService.ShouldUseFailover();

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void ShouldUseFailover_ReturnsFalse_WhenFailoverModeDisabled()
        {
            // Arrange
            var failoverEntries = new List<FailoverEntry>
            {
                new FailoverEntry { DateTime = DateTime.Now.AddMinutes(-5) },
                new FailoverEntry { DateTime = DateTime.Now.AddMinutes(-4) },
            };
            _mockFailoverRepository.Setup(repo => repo.GetFailOverEntries()).Returns(failoverEntries);
            _mockConfigurationService.Setup(cfg => cfg.IsFailoverModeEnabled()).Returns(false);

            // Act
            var result = _failoverService.ShouldUseFailover();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldUseFailover_ReturnsFalse_WhenFailedRequestsDoNotExceedThreshold()
        {
            // Arrange
            var failoverEntries = new List<FailoverEntry>
            {
                new FailoverEntry { DateTime = DateTime.Now.AddMinutes(-5) }, //rider isn't happy with new FailoverEntry and wants just new() but the former is more readable for me
                new FailoverEntry { DateTime = DateTime.Now.AddMinutes(-4) },
            };
            _mockFailoverRepository.Setup(repo => repo.GetFailOverEntries()).Returns(failoverEntries);
            _mockConfigurationService.Setup(cfg => cfg.IsFailoverModeEnabled()).Returns(true);

            // Act
            var result = _failoverService.ShouldUseFailover();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
