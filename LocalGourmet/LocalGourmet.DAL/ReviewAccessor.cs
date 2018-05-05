using LocalGourmet.DAL.Interfaces;
using LocalGourmet.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL
{
    public class ReviewAccessor : ICrud<DL.Review>
    {
        private LocalGourmetDBEntities db;

        #region Constructors
        public ReviewAccessor()
        {
            db = new LocalGourmetDBEntities();
        }

        public ReviewAccessor(LocalGourmetDBEntities testDb)
        {
            db = testDb;
        }
        #endregion

        #region ICrud
        public void Add(Review entity)
        {
            try
            {
                if(entity != null)
                {
                        db.Reviews.Add(entity);
                        db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Review> GetAll()
        {
            return db.Reviews.ToList();
        }

        public IEnumerable<Review> GetReviewsByRestaurantID(int restID)
        {
            return db.Reviews.Where(x => x.RestaurantID == restID).ToList();
        }

        public Review GetById(int id)
        {
            Review r;
            try
            {
                r = db.Reviews.Find(id);
                if(r == null) { throw new ArgumentOutOfRangeException("id"); }
            }
            catch
            {
                throw;
            }
            return r;
        }

        public void Update(Review entity)
        {
            DL.Review oldR;
            try
            {
                if (entity == null) { throw new ArgumentOutOfRangeException("id"); }
                oldR = db.Reviews.Find(entity.ID);
                oldR.ReviewerName = entity.ReviewerName;
                oldR.Comment = entity.Comment;
                oldR.FoodRating = entity.FoodRating;
                oldR.ServiceRating = entity.ServiceRating;
                oldR.PriceRating = entity.PriceRating;
                oldR.AtmosphereRating = entity.AtmosphereRating;
                oldR.RestaurantID = entity.RestaurantID;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Review entity)
        {
            DL.Review r;
            try
            {
                r = db.Reviews.Find(entity.ID);
                if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                db.Reviews.Remove(r);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
