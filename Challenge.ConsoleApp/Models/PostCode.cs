using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.ConsoleApp.Models
{
    public class PostCode
    {
        public string? Postcode { get; set; }
        public int? Eastings { get; set; }
        public int? Northings { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Town { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? CountryString { get; set; }
    }
}
