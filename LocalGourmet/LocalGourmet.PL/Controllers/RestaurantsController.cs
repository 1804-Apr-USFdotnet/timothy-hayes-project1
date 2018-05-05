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
        // ICrud implementing class db = new

        public RestaurantsController()
        {
            log = LogManager.GetLogger("file");

            //Review[] revs = Review.GenerateReviews(100);
            //foreach (var item in revs)
            //{
            //    item.AddReviewAsync();
            //}
        }

        // GET: Restaurants
        public ActionResult Index(string sort)
        {
            try
            {
                var restaurants = Restaurant.GetRestaurants();

                if(sort == "byName")
                {
                    restaurants = Restaurant.SortByNameAsc(restaurants);
                }
                else if(sort == "byRating")
                {
                    restaurants = Restaurant.SortByAvgRatingDesc(restaurants);
                }
                else if(sort == "byCuisine")
                {
                    restaurants = Restaurant.SortByCuisineAsc(restaurants);
                }
                else if(sort == "topThree")
                {
                    restaurants = Restaurant.GetTop3((List<Restaurant>) restaurants);
                }
                else if(sort != null)
                {
                    string search = sort; // "sort" var will store search info
                    restaurants = Restaurant.SearchByName(restaurants, search);
                }
                return View(restaurants);
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
                if(Restaurant.GetRestaurantByID(id) == null)
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
                Restaurant r = Restaurant.GetRestaurantByID(id);
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
        public async Task<ActionResult> Edit(Restaurant restaurant)
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
                Restaurant r = Restaurant.GetRestaurantByID(id);
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
        public async Task<ActionResult> Delete(int id, Restaurant restaurant)
        {
            try
            {
                Restaurant r = Restaurant.GetRestaurantByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                await r.DeleteRestaurantAsync();
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
