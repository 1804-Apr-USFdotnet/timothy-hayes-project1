using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalGourmet.BLL.Models;

namespace LocalGourmet.PL.ViewModels
{
    public class ReviewsEditVM
    {
        private int ID;
        private Review MyReview;
        private IEnumerable<Restaurant> MyRestaurants;

        public ReviewsEditVM()
        {
            MyRestaurants = BLL.Models.Restaurant.GetRestaurants();
        }

        public ReviewsEditVM(int newID)
        {
            this.ID = newID;
            MyReview = Review.GetReviewByID(this.ID);
            MyRestaurants = BLL.Models.Restaurant.GetRestaurants();
        }

        public ReviewsEditVM(int revID, int restID)
        {
            this.ID = revID;
            MyReview = Review.GetReviewByID(this.ID);
            MyReview.RestaurantID = restID;
            MyRestaurants = BLL.Models.Restaurant.GetRestaurants();
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