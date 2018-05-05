using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalGourmet.BLL.Models;

namespace LocalGourmet.PL.ViewModels
{
    public class RestaurantDetailsVM
    {
        private int ID;
        private IEnumerable<Review> MyReviews;
        private Restaurant MyRestaurant;

        public RestaurantDetailsVM(int newID)
        {
            this.ID = newID;
            MyReviews = Review.GetReviewsByRestaurantID(this.ID);
            MyRestaurant = BLL.Models.Restaurant.GetRestaurantByID(this.ID);
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