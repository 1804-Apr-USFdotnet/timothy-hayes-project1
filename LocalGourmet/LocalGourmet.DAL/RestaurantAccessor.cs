using LocalGourmet.DL;
using LocalGourmet.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace LocalGourmet.DAL
{
    public class RestaurantAccessor : ICrud<DL.Restaurant>
    {
        private LocalGourmetDBEntities db;

        #region Constructors
        public RestaurantAccessor()
        {
            db = new LocalGourmetDBEntities();
        }

        public RestaurantAccessor(LocalGourmetDBEntities testDb)
        {
            db = testDb;
        }
        #endregion

        #region ICrud
        public void Add(Restaurant entity)
        {
            try
            {
                if(entity != null)
                {
                        db.Restaurants.Add(entity);
                        db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return db.Restaurants.Where(x => x.Active == true ).ToList();
        }

        public Restaurant GetById(int id)
        {
            Restaurant r;
            try
            {
                r = db.Restaurants.Find(id);
                if(r == null)
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
            return r;
        }

        public void Update(Restaurant entity)
        {
            DL.Restaurant oldR;
            try
            {
                if (entity == null) { throw new ArgumentOutOfRangeException("id"); }
                oldR = db.Restaurants.Find(entity.ID);
                oldR.Name = entity.Name;
                oldR.Location = entity.Location;
                oldR.Cuisine = entity.Cuisine;
                oldR.Specialty = entity.Specialty;
                oldR.PhoneNumber = entity.PhoneNumber;
                oldR.WebAddress = entity.WebAddress;
                oldR.Type = entity.Type;
                oldR.Hours = entity.Hours;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Restaurant entity)
        {
            DL.Restaurant r;
            try
            {
                r = db.Restaurants.Find(entity.ID);
                if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                db.Restaurants.Remove(r);
                var revs = db.Reviews.Where(x => x.RestaurantID == entity.ID);
                if (revs != null)
                {
                    foreach (var rev in revs)
                    {
                        db.Reviews.Remove(rev);
                    }
                }
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
