using CsvHelper;

namespace Challenge.ConsoleApp
{
	public static class ValidateInputs
	{
		/// <summary>
		/// Validate latitude
		/// </summary>
		/// <param name="givenLatitude"></param>
		/// <returns></returns>
		public static bool ValidateLatitude(string givenLatitude)
		{
			if (!double.TryParse(givenLatitude, out double latitude) || latitude < -90.0 || latitude > 90.0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validate longitude
		/// </summary>
		/// <param name="latitude"></param>
		/// <returns></returns>
		public static bool ValidateLongitude(string givenLongitude)
		{
			if (!double.TryParse(givenLongitude, out double longitude) || longitude < -180.0 || longitude > 180.0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Validate maxDistanceInKilometers
		/// </summary>
		/// <param name="latitude"></param>
		/// <returns></returns>
		public static bool ValidateMaxDistanceInKilometers(string givenDistance)
		{
			if (!double.TryParse(givenDistance, out double maxDistance) || maxDistance <= 0)
			{
				return false;
			}
			return true;
		}
	}
}
