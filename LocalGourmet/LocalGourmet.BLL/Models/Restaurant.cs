using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace LocalGourmet.BLL.Models
{
    [DataContract]
    public class Restaurant : IRestaurant
    {
        public Restaurant() {
            Reviews = new List<Review>();
            Active = true;
        }

        #region Properties
        public int ID { get; set; }
        public bool Active { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; } 
        [DataMember]
        [Required]
        public string Location { get; set; }
        [DataMember]
        [Required]
        public string Cuisine { get; set; }
        [DataMember]
        [Required]
        public string Specialty { get; set; }
        [DataMember]
        [Required]
        [RegularExpression(@"^([0-9]{3}-[0-9]{3}-[0-9]{4})$", 
            ErrorMessage = "Invalid Phone Number. Format is: 111-222-3333")]
        public string PhoneNumber { get; set; }
        [DataMember]
        [Required]
        [Url]
        public string WebAddress { get; set; }
        [DataMember]
        public List<Review> Reviews { get; set; }
        [DataMember]
        [Required]
        public string Type { get; set; }
        [DataMember]
        [Required]
        public string Hours { get; set; }
        #endregion

        public double GetAvgRating()
        {
            if (Reviews == null || Reviews.Count == 0) { return 0.0f; }
            return Math.Round(Reviews.Average(x => x.GetRating()), 2);
        }

        #region ToString
        // Return name and rating only
        public string GetNameAndRating()
        {
            return $"{Name} [{GetAvgRating()}]";
        }

        // Return summary of info
        public string GetSummary()
        {
            return $"{Name}, {Cuisine}, {Reviews.Count} Reviews, " +
                $"{Type}, AvgRating: {GetAvgRating()}";
        }

        // Return all info
        public override string ToString()
        {
            return $"{Name}, {Cuisine}, {Type}, {Specialty}, " +
                $"AvgRating: {GetAvgRating()}, {Reviews.Count} Reviews, " +
                $"{Location}, {PhoneNumber}, {WebAddress}, {Hours}";
        }
        #endregion
    }
}
