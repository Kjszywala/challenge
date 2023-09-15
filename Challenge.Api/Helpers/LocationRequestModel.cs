namespace Challenge.Api.Helpers
{
	/// <summary>
	/// Helper class needed to receive data from post request.
	/// </summary>
	public class LocationRequestModel
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double MaxDistanceInKilometers { get; set; }
	}
}
