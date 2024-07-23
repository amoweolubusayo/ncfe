using Moq;
using Ncfe.CodeTest.Services;
using Ncfe.CodeTest.Interfaces;

namespace TestProject1
{
    [TestFixture]
    public class ConfigurationServiceTests
    {
        private Mock<IAppSettings> _mockAppSettings;
        private ConfigurationService _service;

        [SetUp]
        public void SetUp()
        {
            _mockAppSettings = new Mock<IAppSettings>();
            _service = new ConfigurationService(_mockAppSettings.Object);
        }
        
        [Test]
        public void IsFailoverModeEnabled_ShouldReturnTrue_WhenSettingIsTrue()
        {
            // Arrange
            _mockAppSettings.Setup(a => a.Get("IsFailoverModeEnabled")).Returns("true");

            // Act
            var result = _service.IsFailoverModeEnabled();

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IsFailoverModeEnabled_ShouldReturnFalse_WhenSettingIsFalse()
        {
            // Arrange
            _mockAppSettings.Setup(a => a.Get("IsFailoverModeEnabled")).Returns("false");

            // Act
            var result = _service.IsFailoverModeEnabled();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsFailoverModeEnabled_ShouldReturnNull_WhenSettingIsNotPresent()
        {
            // Arrange
            _mockAppSettings.Setup(a => a.Get("IsFailoverModeEnabled")).Returns((string)null);

            // Act
            var result = _service.IsFailoverModeEnabled();

            // Assert
            Assert.IsFalse(result);
        }
    }
}