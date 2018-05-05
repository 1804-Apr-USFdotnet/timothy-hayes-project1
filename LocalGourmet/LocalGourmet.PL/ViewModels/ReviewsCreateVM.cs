using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGourmet.PL.ViewModels
{
    public class ReviewsCreateVM
    {
        private Review MyReview;
        private IEnumerable<Restaurant> MyRestaurants;

        public ReviewsCreateVM()
        {
            MyReview = new Review();
            MyRestaurants = Restaurant.GetRestaurants();
        }

        public Review Review
        {
            get
            {
                return MyReview;
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