using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Repositories;

namespace LocalGourmet.PL.ViewModels
{
    public class RestaurantDetailsVM
    {
        private int ID;
        private IEnumerable<Review> MyReviews;
        private Restaurant MyRestaurant;
        private ReviewRepository reviewRepository;
        private RestaurantRepository restaurantRepository;

        public RestaurantDetailsVM(int newID)
        {
            this.ID = newID;
            reviewRepository = new ReviewRepository();
            restaurantRepository = new RestaurantRepository();
            MyReviews = reviewRepository.GetReviewsByRestaurantID(this.ID);
            MyRestaurant = restaurantRepository.GetByID(this.ID);
        }

        public IEnumerable<Review> Reviews
        {
            get
            {
                return MyReviews;
            }
        }

        public Restaurant Restaurant
        {
            get
            {
                return MyRestaurant;
            }
        }
    }
}