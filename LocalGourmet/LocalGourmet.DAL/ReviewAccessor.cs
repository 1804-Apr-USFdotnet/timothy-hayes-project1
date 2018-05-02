using LocalGourmet.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL
{
    public class ReviewAccessor
    {
        // CREATE
        public async Task AddReviewAsync(DL.Review item)
        {
            using (var db = new LocalGourmetDBEntities())
            {
                db.Reviews.Add(item);
                await db.SaveChangesAsync();
            }
        }

        // READ
        public IEnumerable<DL.Review> GetReviews()
        {
            IEnumerable<DL.Review> dataList;
            using (var db = new LocalGourmetDBEntities())
            {
                dataList = db.Reviews.ToList();
            }
            return dataList;
        }

        public IEnumerable<DL.Review> GetReviewsByRestaurantID(int restID)
        {
            IEnumerable<DL.Review> dataList;
            using (var db = new LocalGourmetDBEntities())
            {
                dataList = db.Reviews.Where(x => x.RestaurantID == restID).ToList();
            }
            return dataList;
        }

        public DL.Review GetReviewByID(int id)
        {
            DL.Review r;
            using (var db = new LocalGourmetDBEntities())
            {
                try
                {
                    r = db.Reviews.Find(id);
                }
                catch
                {
                    throw;
                }
            }
            if(r == null) { throw new ArgumentOutOfRangeException("id"); }
            return r;
        }


        // UPDATE
        public async Task UpdateReviewAsync(int id, string reviewerName, 
            string comment, int foodRating, int serviceRating, int priceRating,
            int atmosphereRating, int restaurantID)
        {
            DL.Review r;
            try
            {
                using (var db = new LocalGourmetDBEntities())
                {
                    r = db.Reviews.Find(id);
                    if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                    r.ReviewerName = reviewerName;
                    r.Comment = comment;
                    r.FoodRating = foodRating;
                    r.ServiceRating = serviceRating;
                    r.PriceRating = priceRating;
                    r.AtmosphereRating = atmosphereRating;
                    r.RestaurantID = restaurantID;
                    await db.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        // DELETE
        public async Task DeleteReviewAsync(int id)
        {
            DL.Review r;
            try
            {
                using (var db = new LocalGourmetDBEntities())
                {
                    r = db.Reviews.Find(id);
                    if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                    db.Reviews.Remove(r);
                    await db.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
