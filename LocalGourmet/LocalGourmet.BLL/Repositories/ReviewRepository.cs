using LocalGourmet.BLL.Services;
using LocalGourmet.DAL.Repositories;
using LocalGourmet.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Repositories
{
    public class ReviewRepository
    {
        private ReviewAccessor crud;

        public ReviewRepository()
        {
            crud = new ReviewAccessor();
        }

        public ReviewRepository(LocalGourmetDBEntities db)
        {
            crud = new ReviewAccessor(db);
        }

        #region CRUD
        public void Add(BLL.Models.Review r)
        {
            crud.Add(ReviewService.LibraryToData(r));
        }

        public IEnumerable<BLL.Models.Review> GetAll()
        {
            return crud.GetAll().Select(x => ReviewService.DataToLibrary(x)).ToList();
        }

        public BLL.Models.Review GetById(int id)
        {
            BLL.Models.Review r;
            try
            {
                r = ReviewService.DataToLibrary(crud.GetById(id));
            }
            catch
            {
                throw;
            }
            return r;
        }

        public List<BLL.Models.Review> GetReviewsByRestaurantID(int restID)
        {
            try
            {
                return crud.GetReviewsByRestaurantID(restID).Select
                    (x => ReviewService.DataToLibrary(x)).ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Update(BLL.Models.Review r)
        {
            try
            {
                RestaurantRepository restaurantRepository = new RestaurantRepository();
                BLL.Models.Restaurant rest = restaurantRepository.GetByID(r.RestaurantID);
                if (rest == null) { throw new ArgumentException(); }

                // Conform rating input to rating bounds
                r.FoodRating = r.FoodRating < 0 ? 0 : 
                    (r.FoodRating > 5 ? 5 : r.FoodRating);
                r.ServiceRating = r.ServiceRating < 0 ? 0 : 
                    (r.ServiceRating > 5 ? 5 : r.ServiceRating);
                r.PriceRating = r.PriceRating < 0 ? 0 : 
                    (r.PriceRating > 5 ? 5 : r.PriceRating);
                r.AtmosphereRating = r.AtmosphereRating < 0 ? 0 : 
                    (r.AtmosphereRating > 5 ? 5 : r.AtmosphereRating);

                crud.Update(ReviewService.LibraryToData(r));
            }
            catch
            {
                throw;
            }
        }

        public void Delete(BLL.Models.Review r)
        {
            try
            {
                crud.Delete(ReviewService.LibraryToData(r));
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
