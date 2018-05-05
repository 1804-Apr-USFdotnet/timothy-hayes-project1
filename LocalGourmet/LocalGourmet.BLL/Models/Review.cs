using LocalGourmet.BLL.Interfaces;
using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LocalGourmet.BLL.Repositories;

namespace LocalGourmet.BLL.Models
{
    [DataContract]
    public class Review : IReview
    {
        #region Constructors
        public Review()
        {
        }

        public Review(string name, string comment)
        {
            ReviewerName = name;
            Comment = comment;

            int defaultRating = 3;
            FoodRating = defaultRating;
            ServiceRating = defaultRating;
            AtmosphereRating = defaultRating;
            PriceRating = defaultRating;
        }

        // Review constructor that assigns the same rating to all
        // rating categories.
        public Review(string name, string comment, int simpleRating)
        {
            // Enforce a rating between 0 and 5 inclusive
            simpleRating = 
                simpleRating < 0 ? 0 : (simpleRating > 5 ? 5 : simpleRating);
            ReviewerName = name;
            Comment = comment;
            FoodRating = simpleRating;
            ServiceRating = simpleRating;
            AtmosphereRating = simpleRating;
            PriceRating = simpleRating;
        }

        public Review(string name, string comment, int foodRat, 
            int serviceRat, int atmosphereRat, int priceRat)
        {
            ReviewerName = name;
            Comment = comment;

            // Enforce a rating between 0 and 5 inclusive
            foodRat = 
                foodRat < 0 ? 0 : (foodRat > 5 ? 5 : foodRat);
            serviceRat = 
                serviceRat < 0 ? 0 : (serviceRat > 5 ? 5 : serviceRat);
            atmosphereRat = 
                atmosphereRat < 0 ? 0 : (atmosphereRat > 5 ? 5 : atmosphereRat);
            priceRat = 
                priceRat < 0 ? 0 : (priceRat > 5 ? 5 : priceRat);

            FoodRating = foodRat;
            ServiceRating = serviceRat;
            AtmosphereRating = atmosphereRat;
            PriceRating = priceRat;
        }
        #endregion

        #region Rating Components
        // Individual rating components
        [DataMember]
        private int foodRating;
        [Range(0 ,5 , ErrorMessage ="Rating must be between 0 and 5 inclusive.")]
        public int FoodRating
        {
            get { return foodRating; }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                foodRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }

        [DataMember]
        private int serviceRating;
        [Range(0 ,5 , ErrorMessage ="Rating must be between 0 and 5 inclusive.")]
        public int ServiceRating 
        {
            get { return serviceRating; }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                serviceRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }

        [DataMember]
        private int atmosphereRating;
        [Range(0 ,5 , ErrorMessage ="Rating must be between 0 and 5 inclusive.")]
        public int AtmosphereRating 
        {
            get { return atmosphereRating;  }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                atmosphereRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }
        
        // Was it a good deal, or too expensive?
        [DataMember]
        private int priceRating;
        [Range(0 ,5 , ErrorMessage ="Rating must be between 0 and 5 inclusive.")]
        public int PriceRating 
        {
            get { return priceRating; }
            set
            {
                // Enforce a rating between 0 and 5 inclusive
                priceRating = value < 0 ? 0 : (value > 5 ? 5 : value);
            }
        }
        #endregion

        public int RestaurantID { get; set; }
        // ID indexing is handled by the data source.
        // This ID Property is only for storing the ID when converting
        // the object between layers. Do not set this when creating an
        // object -- it will be ignored.
        public int ID { get; set; }
        [DataMember]
        public string Comment { get; set; } 
        [DataMember]
        [Required]
        public string ReviewerName { get; set; } 


        // Calculates and returns the overall review rating based on the
        // individual review components.
        public float GetRating()
        {
            return (float) Math.Round(((FoodRating + ServiceRating +
                AtmosphereRating + PriceRating) / 4.0), 2);
        }

        public Restaurant GetRestaurant()
        {
            RestaurantRepository restaurantRepository = new RestaurantRepository();
            return restaurantRepository.GetByID(RestaurantID);
        }

        public override string ToString()
        {
            return $"[{GetRating()}] {ReviewerName}: {Comment} [Food: " +
                $"{FoodRating}, Service: {ServiceRating}, Atmosphere: " +
                $"{AtmosphereRating}, Price: {PriceRating}]";
        }
    }
}
