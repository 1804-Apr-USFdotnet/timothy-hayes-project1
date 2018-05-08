using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.Controllers;
using LocalGourmet.PL.ViewModels;
using LocalGourmet.BLL.Repositories;
using System.Linq;

namespace LocalGourmet.PL.UnitTest
{
    public class FakeRestaurantRepository : RestaurantRepository
    {
        private List<Restaurant> restaurants;

        public FakeRestaurantRepository()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant() { ID = 1, Name = "McD", Cuisine = "Meat" },
                new Restaurant() { ID = 2, Name = "BK", Cuisine = "Meat" },
                new Restaurant() { ID = 3, Name = "DQ", Cuisine = "Fries" },
                new Restaurant() { ID = 4, Name = "Acme", Cuisine = "Slop" },
                new Restaurant() { ID = 5, Name = "Citrus", Cuisine = "Apples" }
            };

        }

        public override void Add(Restaurant r)
        {
            restaurants.Add(r);
        }

        public override IEnumerable<Restaurant> GetAll()
        {
            return restaurants;
        }

        public override Restaurant GetByID(int id)
        {
            return restaurants.First(x => x.ID == id);
        }

        public override void Delete(Restaurant r)
        {
            restaurants.Remove(r);
        }

        public override void Update(Restaurant r)
        {
            restaurants.RemoveAt(restaurants.IndexOf(r));
            restaurants.Add(r);
        }
    }

    [TestClass]
    public class RestaurantsControllerUnitTest
    {
        [TestMethod]
        public void TestRestaurantsIndex()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            RestaurantsController controller = new RestaurantsController(fakeRestaurantRepository);
            string e1 = "Acme";
            string e2 = "Citrus";

            //Act
            var result1 = controller.Index("byName") as ViewResult;
            var data1 = result1.Model as Restaurant[];
            var result2 = controller.Index("byCuisine") as ViewResult;
            var data2 = result2.Model as Restaurant[];

            //Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual(e1, data1[0].Name);
            Assert.IsNotNull(result2);
            Assert.AreEqual(e2, data2[0].Name);
        }

        [TestMethod]
        public void TestRestaurantsDetails()
        {
            //Arrange
            RestaurantsController controller = new RestaurantsController();

            //Act
            var result1 = controller.Details(1) as ViewResult;
            var data1 = result1.Model as RestaurantDetailsVM;

            var result2 = controller.Details(9) as ViewResult;
            var data2 = result2.Model as RestaurantDetailsVM;

            //Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual("Subway", data1.Restaurant.Name);

            Assert.IsNotNull(result2);
            Assert.AreEqual("Hattricks", data2.Restaurant.Name);
        }

        [TestMethod]
        public void TestRestaurantsCreate()
        {
            //Arrange
            RestaurantsController controller = new RestaurantsController();

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestRestaurantsEdit()
        {
            //Arrange
            RestaurantsController controller = new RestaurantsController();

            //Act
            var result = controller.Edit(1) as ViewResult;
            var data = result.Model as Restaurant;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(data.Name, "Subway");
        }

        [TestMethod]
        public void TestRestaurantsDelete()
        {
            //Arrange
            RestaurantsController controller = new RestaurantsController();

            //Act
            var result = controller.Delete(1) as ViewResult;
            var data = result.Model as Restaurant;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(data.Name, "Subway");
        }
    }
}
