using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Repositories;
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
        private RestaurantRepository restaurantRepository;

        public ReviewsCreateVM()
        {
            restaurantRepository = new RestaurantRepository();
            MyReview = new Review();
            MyRestaurants = restaurantRepository.GetAll();
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