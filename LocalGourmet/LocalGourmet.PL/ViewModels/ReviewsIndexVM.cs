using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Repositories;
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
        private ReviewRepository reviewRepository;
        private RestaurantRepository restaurantRepository;

        public ReviewsIndexVM(ReviewRepository newReviewRepository, RestaurantRepository newRestaurantRepository)
        {
            reviewRepository = newReviewRepository;
            restaurantRepository = newRestaurantRepository;
            MyReviews = reviewRepository.GetAll().Reverse(); // Most recently created first
            MyRestaurants = restaurantRepository.GetAll();
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