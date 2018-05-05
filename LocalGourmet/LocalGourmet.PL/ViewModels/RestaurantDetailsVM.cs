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

        public RestaurantDetailsVM(int newID)
        {
            ID = newID;
        }

        public IEnumerable<Review> Reviews
        {
            get
            {
                return Review.GetReviewsByRestaurantID(this.ID);
            }
        }

        public Restaurant Restaurant
        {
            get
            {
                return BLL.Models.Restaurant.GetRestaurantByID(this.ID);
            }
        }
    }
}