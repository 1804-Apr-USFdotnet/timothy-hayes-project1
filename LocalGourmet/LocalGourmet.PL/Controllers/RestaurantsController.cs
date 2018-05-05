using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.ViewModels;
using NLog;

namespace LocalGourmet.PL.Controllers
{
    public delegate IEnumerable<Restaurant> RestaurantSortDelegate (IEnumerable<Restaurant> restaurants);

    public class RestaurantsController : Controller
    {
        private Logger log;
        private IEnumerable<Restaurant> restaurants;

        public RestaurantsController()
        {
            log = LogManager.GetLogger("file");
            restaurants = Restaurant.GetAll();
        }

        // GET: Restaurants
        public ActionResult Index(string sort)
        {
            IEnumerable<Restaurant> tempRestaurants = restaurants;
            try
            {
                if(sort == "byName")
                {
                    tempRestaurants = Restaurant.SortByNameAsc(restaurants);
                }
                else if(sort == "byRating")
                {
                    tempRestaurants = Restaurant.SortByAvgRatingDesc(restaurants);
                }
                else if(sort == "byCuisine")
                {
                    tempRestaurants = Restaurant.SortByCuisineAsc(restaurants);
                }
                else if(sort == "topThree")
                {
                    tempRestaurants = Restaurant.GetTop3((List<Restaurant>) restaurants);
                }
                else if(sort != null)
                {
                    string search = sort; // "sort" var will store search info
                    tempRestaurants = Restaurant.SearchByName(restaurants, search);
                }
                return View(tempRestaurants);
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Index] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if(Restaurant.GetByID(id) == null)
                { throw new ArgumentNullException(); }
                RestaurantDetailsVM vm = new RestaurantDetailsVM(id);
                return View(vm);
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Details] Exception thrown: {e.Message}");
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
        public ActionResult Create(BLL.Models.Restaurant restaurant)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    restaurant.Active = true;
                    restaurant.Add();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ModelState);
                }
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Create] Exception thrown: {e.Message}");
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Restaurant r = Restaurant.GetByID(id);
                if(r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Edit] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        public ActionResult Edit(Restaurant restaurant)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    restaurant.Update(restaurant);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ModelState);
                }
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Edit] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Restaurant r = Restaurant.GetByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Delete] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Restaurant restaurant)
        {
            try
            {
                Restaurant r = Restaurant.GetByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                r.Delete();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                log.Error($"[Restaurants Controller] [Delete] Exception thrown: {e.Message}");
                return View(restaurant);
            }
        }
    }
}
