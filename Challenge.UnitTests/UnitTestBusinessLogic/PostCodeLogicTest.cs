using Challenge.BusinessLogic.Services;

namespace Challenge.UnitTests.UnitTestBusinessLogic
{
    [TestFixture]
    public class PostCodeLogicTest
    {
        PostCodeLogic postCodeLogic = new PostCodeLogic();

        [Test]
        public void CalculateDistance_ShouldCalculateCorrectDistance()
        {

            // Arrange
            // Berlin, Germany
            double lat1 = 52.5200; 
            double lon1 = 13.4050;
            // Paris, France
            double lat2 = 48.8566; 
            double lon2 = 2.3522;

            // Approximate distance in kilometers
            double expectedDistance = 878.88; 

            // Act
            double actualDistance = postCodeLogic.CalculateDistance(lat1, lon1, lat2, lon2);

            // Assert
            // Use a delta for floating-point comparison
            Assert.That(actualDistance, Is.EqualTo(expectedDistance).Within(2));
        }

        [Test]
        public void CalculateDistance_ShouldHandleSameCoordinates()
        {
            // Arrange
            // New York City, USA
            double lat1 = 40.7128;
            double lon1 = -74.0060;

            // Distance to itself should be 0
            double expectedDistance = 0.0;

            //Act
            double actualDistance = postCodeLogic.CalculateDistance(lat1, lon1, lat1, lon1);

            // Assert
            Assert.That(actualDistance, Is.EqualTo(expectedDistance).Within(0.01));
        }

        [Test]
        public void ToRadians_ConvertsZeroDegreesToZeroRadians()
        {
            // Arrange
            double degrees = 0.0;

            // Act
            double radians = PostCodeLogic.ToRadians(degrees);

            // Assert
            Assert.That(radians, Is.EqualTo(0.0));
        }

        [Test]
        public void ToRadians_ConvertsPositiveDegreesToRadians()
        {
            // Arrange
            // 90 degrees is n/2 radians
            double degrees = 90.0;

            // Act
            double radians = PostCodeLogic.ToRadians(degrees);

            // Assert
            // Use a delta for floating-point comparison
            Assert.That(radians, Is.EqualTo(Math.PI / 2.0).Within(0.0001)); 
        }

        [Test]
        public void ToRadians_ConvertsNegativeDegreesToRadians()
        {
            // Arrange
            // -180 degrees is -n radians
            double degrees = -180.0;

            // Act
            double radians = PostCodeLogic.ToRadians(degrees);

            // Assert
            // Use a delta for floating-point comparison
            Assert.That(radians, Is.EqualTo(-Math.PI).Within(0.0001)); 
        }
    }
}
