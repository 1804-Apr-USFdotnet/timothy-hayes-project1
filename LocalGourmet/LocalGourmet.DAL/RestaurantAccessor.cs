using LocalGourmet.DL;
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
        // CREATE
        public async Task AddRestaurantAsync(DL.Restaurant item)
        {
            using (var db = new LocalGourmetDBEntities())
            {
                db.Restaurants.Add(item);
                await db.SaveChangesAsync();
            }
        }

        // READ
        // Does not return inactive ("deleted") restaurants
        public IEnumerable<DL.Restaurant> GetRestaurants()
        {
            IEnumerable<DL.Restaurant> dataList;
            using (var db = new LocalGourmetDBEntities())
            {
                dataList = db.Restaurants.Where(x => x.Active == true ).ToList();
            }
            return dataList;
        }

        // Does return inactive ("deleted") restaurants
        public DL.Restaurant GetRestaurantByID(int id)
        {
            DL.Restaurant r;
            using (var db = new LocalGourmetDBEntities())
            {
                try
                {
                    r = db.Restaurants.Find(id);
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
        public async Task UpdateRestaurantAsync(int id, string name, 
            string location, string cuisine, string specialty, 
            string phoneNumber, string webAddress, string type, string hours)
        {
            DL.Restaurant r;
            try
            {
                using (var db = new LocalGourmetDBEntities())
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
                using (var db = new LocalGourmetDBEntities())
                {
                    r = db.Restaurants.Find(id);
                    if (r == null) { throw new ArgumentOutOfRangeException("id"); }
                    db.Restaurants.Remove(r);
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
