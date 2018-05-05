using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGourmet.PL.ViewModels
{
    public class ReviewsIndexVM
    {
        private IEnumerable<Review> MyReviews;
        private IEnumerable<Restaurant> MyRestaurants;

        public ReviewsIndexVM()
        {
            MyReviews = Review.GetReviews();
            MyRestaurants = Restaurant.GetAll();
        }

        public IEnumerable<Review> Reviews
        {
            get
            {
                return MyReviews;
            }
        }

        public IEnumerable<Restaurant> Restaurants
        {
            get
            {
                return MyRestaurants; 
            }
        }
    }
}