using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGourmet.PL.ViewModels
{
    public class ReviewsIndexVM
    {
        public IEnumerable<Review> Reviews
        {
            get
            {
                return Review.GetReviews();
            }
        }

        public IEnumerable<Restaurant> Restaurants
        {
            get
            {
                return Restaurant.GetRestaurants();
            }
        }
    }
}