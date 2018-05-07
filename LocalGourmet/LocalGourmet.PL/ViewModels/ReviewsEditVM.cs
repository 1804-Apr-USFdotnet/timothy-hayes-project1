using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Repositories;

namespace LocalGourmet.PL.ViewModels
{
    public class ReviewsEditVM
    {
        private Review MyReview;
        private IEnumerable<Restaurant> MyRestaurants;
        private ReviewRepository reviewRepository;
        private RestaurantRepository restaurantRepository;

        public ReviewsEditVM(int reviewID)
        {
            restaurantRepository = new RestaurantRepository();
            reviewRepository = new ReviewRepository();
            MyReview = reviewRepository.GetById(reviewID);
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