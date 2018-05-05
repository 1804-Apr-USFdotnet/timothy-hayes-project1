using LocalGourmet.DL;
using LocalGourmet.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LocalGourmet.DAL
{
    // CRUD class for Restaurant
    public class RestaurantAccessor : ICrud<DL.Restaurant>
    {
        private LocalGourmetDBEntities db;

        public RestaurantAccessor()
        {
            db = new LocalGourmetDBEntities();
        }

        public RestaurantAccessor(LocalGourmetDBEntities testDb)
        {
            db = testDb;
        }

        // CREATE
        public async Task AddRestaurantAsync(DL.Restaurant item)
        {
            try
            {
                if(item != null)
                {
                        db.Restaurants.Add(item);
                        await db.SaveChangesAsync();
                }
            }
            catch(Exception)
            {
                // call Nlog
                throw;
            }
        }

        // READ
        // Does not return inactive ("deleted") restaurants
        public IEnumerable<DL.Restaurant> GetRestaurants()
        {
            return db.Restaurants.Where(x => x.Active == true ).ToList();
        }

        // Does return inactive ("deleted") restaurants
        public DL.Restaurant GetRestaurantByID(int id)
        {
            DL.Restaurant r;
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
        
        // UPDATE
        public async Task UpdateRestaurantAsync(Restaurant r)
        {
            DL.Restaurant oldR;
            try
            {
                if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                oldR = db.Restaurants.Find(r.ID);
                oldR.Name = r.Name;
                oldR.Location = r.Location;
                oldR.Cuisine = r.Cuisine;
                oldR.Specialty = r.Specialty;
                oldR.PhoneNumber = r.PhoneNumber;
                oldR.WebAddress = r.WebAddress;
                oldR.Type = r.Type;
                oldR.Hours = r.Hours;
                await db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        // DELETE
        public async Task DeleteRestaurantAsync(int id)
        {
            DL.Restaurant r;
            try
            {
                if(id != 0)
                {
                    r = db.Restaurants.Find(id);
                    if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                    db.Restaurants.Remove(r);
                    var revs = db.Reviews.Where(x => x.RestaurantID == id);
                    if (revs != null)
                    {
                        foreach (var rev in revs)
                        {
                            db.Reviews.Remove(rev);
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch
            {
                // Nlog
                throw;
            }
        }


        #region ICrud
        public async void AddAsync(Restaurant entity)
        {
            await AddRestaurantAsync(entity);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return GetRestaurants();
        }

        public Restaurant GetById(int id)
        {
            return GetRestaurantByID(id);
        }

        public async void UpdateAsync(Restaurant entity)
        {
            await UpdateRestaurantAsync(entity);
        }

        public void DeleteAsync(Restaurant entity)
        {
        }
        #endregion
    }
}
