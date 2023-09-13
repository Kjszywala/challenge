﻿using System.ComponentModel.DataAnnotations;

namespace Challenge.Api.Models.Database
{
    /// <summary>
    /// Database PostCodes table class.
    /// </summary>
    public class PostCodes
    {
        [Key]
        public int Id { get; set; }
        public string? Postcode { get; set; }
        public int? Eastings { get; set; }
        public int? Northings { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Town { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? CountryString { get; set; }
        //public bool? IsActive { get; set; }
        //public DateTime? CreationDate { get; set; }
        //public DateTime? ModificationDate { get; set; }
        //public string? Notes { get; set; }

    }
}