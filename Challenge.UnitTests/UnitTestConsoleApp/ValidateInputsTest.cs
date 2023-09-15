using Challenge.ConsoleApp;

namespace Challenge.UnitTests.UnitTestConsoleApp
{
	[TestFixture]
	public class ValidateInputsTest
	{
		[Test]
		public void ValidateLatitude_ValidValue_ReturnsTrue()
		{
			// Arrange
			string validLatitude = "50.0";

			// Act
			bool result = ValidateInputs.ValidateLatitude(validLatitude);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void ValidateLatitude_InvalidValue_ReturnsFalse()
		{
			// Arrange
			string invalidLatitude = "200.0"; // Latitude out of valid range

			// Act
			bool result = ValidateInputs.ValidateLatitude(invalidLatitude);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void ValidateLongitude_ValidValue_ReturnsTrue()
		{
			// Arrange
			string validLongitude = "-100.0";

			// Act
			bool result = ValidateInputs.ValidateLongitude(validLongitude);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void ValidateLongitude_InvalidValue_ReturnsFalse()
		{
			// Arrange
			string invalidLongitude = "200.0"; // Longitude out of valid range

			// Act
			bool result = ValidateInputs.ValidateLongitude(invalidLongitude);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void ValidateMaxDistanceInKilometers_ValidValue_ReturnsTrue()
		{
			// Arrange
			string validMaxDistance = "10.0";

			// Act
			bool result = ValidateInputs.ValidateMaxDistanceInKilometers(validMaxDistance);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void ValidateMaxDistanceInKilometers_InvalidValue_ReturnsFalse()
		{
			// Arrange
			string invalidMaxDistance = "-5.0"; // Negative max distance

			// Act
			bool result = ValidateInputs.ValidateMaxDistanceInKilometers(invalidMaxDistance);

			// Assert
			Assert.IsFalse(result);
		}
	}
}
