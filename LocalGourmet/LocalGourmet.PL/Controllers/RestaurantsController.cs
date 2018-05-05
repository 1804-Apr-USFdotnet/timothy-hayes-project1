﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.ViewModels;

namespace LocalGourmet.PL.Controllers
{
    public delegate IEnumerable<Restaurant> RestaurantSortDelegate (IEnumerable<Restaurant> restaurants);

    public class RestaurantsController : Controller
    {
        // ICrud implementing class db = new

        // GET: Restaurants
        public ActionResult Index(string sort)
        {
            var restaurants = BLL.Models.Restaurant.GetRestaurants();

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
                restaurants = BLL.Models.Restaurant.GetTop3((List<Restaurant>) restaurants);
            }
            else if(sort != null)
            {
                string search = sort; // "sort" var will store search info
                restaurants = Restaurant.SearchByName(restaurants, search);
            }

            return View(restaurants);
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
