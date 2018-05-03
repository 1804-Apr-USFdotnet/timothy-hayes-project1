﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalGourmet.BLL.Models;

namespace LocalGourmet.PL.Controllers
{
    public class RestaurantsController : Controller
    {
        // ICrud implementing class db = new


        // GET: Restaurants
        public ActionResult Index()
        {
            return View((BLL.Models.Restaurant.GetRestaurants()).Select(x=>PL.Models.Restaurant.LibraryToWeb(x)));
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                PL.Models.Restaurant plr = 
                    PL.Models.Restaurant.LibraryToWeb
                      (BLL.Models.Restaurant.GetRestaurantByID(id));
                return View(plr);
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
        public ActionResult Create(BLL.Models.Restaurant restaurant)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    //BLL.Models.Restaurant.Add
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
            return View();
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PL.Models.Restaurant restaurant)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, PL.Models.Restaurant restaurant)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}