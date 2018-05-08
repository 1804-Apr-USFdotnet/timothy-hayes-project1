using System.Collections.Generic;
using System.Linq;
using LocalGourmet.BLL.Models;
using LocalGourmet.DAL.Repositories;
using LocalGourmet.DL;
using LocalGourmet.BLL.Services;

namespace LocalGourmet.BLL.Repositories
{
    public class RestaurantRepository
    {
        private RestaurantAccessor crud;

        public RestaurantRepository()
        {
            crud = new RestaurantAccessor();
        }

        public RestaurantRepository(LocalGourmetDBEntities db)
        {
            crud = new RestaurantAccessor(db);
        }

        #region CRUD
        public virtual void Add(BLL.Models.Restaurant r)
        {
            crud.Add(RestaurantService.LibraryToData(r));
        }

        public virtual IEnumerable<BLL.Models.Restaurant> GetAll()
        {
            return crud.GetAll().Select(x => RestaurantService.DataToLibrary(x)).ToList();
        }

        public virtual BLL.Models.Restaurant GetByID(int id)
        {
            BLL.Models.Restaurant r;
            try
            {
                r = RestaurantService.DataToLibrary(crud.GetById(id));
            }
            catch
            {
                throw;
            }
            return r;
        }
        
        public virtual void Update(BLL.Models.Restaurant r)
        {
            try
            {
                crud.Update(RestaurantService.LibraryToData(r));
            }
            catch
            {
                throw;
            }
        }

        public virtual void Delete(BLL.Models.Restaurant r)
        {
            try
            {
                crud.Delete(RestaurantService.LibraryToData(r));
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
