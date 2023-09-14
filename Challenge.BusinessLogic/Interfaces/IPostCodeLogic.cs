using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.BusinessLogic.Interfaces
{
    public interface IPostCodeLogic
    {
        /// <summary>
        /// Calculate distance beetween the postcodes
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}
