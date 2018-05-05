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
        private int ID;
        private Review MyReview;
        private IEnumerable<Restaurant> MyRestaurants;
        private ReviewRepository reviewRepository;
        private RestaurantRepository restaurantRepository;

        public ReviewsEditVM()
        {
            restaurantRepository = new RestaurantRepository();
            reviewRepository = new ReviewRepository();
            MyRestaurants = restaurantRepository.GetAll();
        }

        public ReviewsEditVM(int newID)
        {
            this.ID = newID;
            MyReview = reviewRepository.GetById(this.ID);
            MyRestaurants = restaurantRepository.GetAll();
        }

        public ReviewsEditVM(int revID, int restID)
        {
            this.ID = revID;
            MyReview = reviewRepository.GetById(this.ID);
            MyReview.RestaurantID = restID;
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