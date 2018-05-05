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
        public void Add(BLL.Models.Restaurant r)
        {
            crud.Add(RestaurantService.LibraryToData(r));
        }

        public IEnumerable<BLL.Models.Restaurant> GetAll()
        {
            return crud.GetAll().Select(x => RestaurantService.DataToLibrary(x)).ToList();
        }

        public BLL.Models.Restaurant GetByID(int id)
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
        
        public void Update(BLL.Models.Restaurant r)
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

        public void Delete(BLL.Models.Restaurant r)
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
