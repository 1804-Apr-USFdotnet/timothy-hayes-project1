using LocalGourmet.DL;
using LocalGourmet.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL
{
    // CRUD class for Restaurant
    public class RestaurantAccessor
    {
        private LocalGourmetDBEntities db;

        public RestaurantAccessor()
        {
            db = new LocalGourmetDBEntities();

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
        public async Task UpdateRestaurantAsync(int id, string name, 
            string location, string cuisine, string specialty, 
            string phoneNumber, string webAddress, string type, string hours)
        {
            DL.Restaurant r;
            try
            {
                r = db.Restaurants.Find(id);
                if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                r.Name = name;
                r.Location = location;
                r.Cuisine = cuisine;
                r.Specialty = specialty;
                r.PhoneNumber = phoneNumber;
                r.WebAddress = webAddress;
                r.Type = type;
                r.Hours = hours;
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





        
    }
}
