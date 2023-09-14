using Challenge.BusinessLogic.Interfaces;

namespace Challenge.BusinessLogic.Services
{
    public class PostCodeLogic : IPostCodeLogic
    {
        #region Variables

        private const double EarthRadiusKm = 6371.0; // Earth radius in kilometers

        #endregion

        #region Methods

        /// <summary>
        /// Helper method to calculate distance between two points (Haversine formula)
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Convert latitude and longitude from degrees to radians
            lat1 = ToRadians(lat1);
            lon1 = ToRadians(lon1);
            lat2 = ToRadians(lat2);
            lon2 = ToRadians(lon2);

            // Haversine formula
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            // Calculate the distance
            double distance = EarthRadiusKm * c;

            return distance;
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        #endregion
    }
}
