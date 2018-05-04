using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LocalGourmet.BLL.Models;

namespace LocalGourmet.PL.Controllers
{
    public delegate IEnumerable<Restaurant> RestaurantSortDelegate (IEnumerable<Restaurant> restaurants);

    public class RestaurantsController : Controller
    {
        // ICrud implementing class db = new


        //// GET: Restaurants
        //public ActionResult Index()
        //{
        //    return View(BLL.Models.Restaurant.GetRestaurants());
        //}

        // GET: Restaurants
        public ActionResult Index(string sort)
        {
            RestaurantSortDelegate del = x => x;
            switch(sort)
            {
                case "name":
                    del = Restaurant.SortByNameAsc;
                    break;
                case "rating":
                    del = Restaurant.SortByAvgRatingDesc;
                    break;
                case "cuisine":
                    del = Restaurant.SortByCuisineAsc;
                    break;
                case "search":
                    del = Restaurant.SearchByName;
                    break;
            }

            return View(del(BLL.Models.Restaurant.GetRestaurants()));
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                BLL.Models.Restaurant r =
                      (BLL.Models.Restaurant.GetRestaurantByID(id));
                if (r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        public async Task<ActionResult> Create(BLL.Models.Restaurant restaurant)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    restaurant.Active = true;
                    await restaurant.AddRestaurantAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ModelState);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                BLL.Models.Restaurant r =
                     BLL.Models.Restaurant.GetRestaurantByID(id);
                if(r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch
            {
                throw;
            }
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, BLL.Models.Restaurant restaurant)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    await restaurant.UpdateRestaurantAsync(restaurant);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ModelState);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                BLL.Models.Restaurant r =
                     BLL.Models.Restaurant.GetRestaurantByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch
            {
                throw;
            }
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, BLL.Models.Restaurant restaurant)
        {
            try
            {
                BLL.Models.Restaurant r =
                                   BLL.Models.Restaurant.GetRestaurantByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                await r.DeleteRestaurantAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(restaurant);
            }
        }
    }
}
