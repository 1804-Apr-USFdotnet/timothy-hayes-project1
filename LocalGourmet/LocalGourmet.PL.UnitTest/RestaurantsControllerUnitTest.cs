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
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            RestaurantsController controller = new RestaurantsController(fakeRestaurantRepository, fakeReviewRepository);
            string e1 = "Acme";
            string e2 = "Citrus";

            //Act
            var result1 = controller.Index("byName") as ViewResult;
            var data1 = result1.Model as List<Restaurant>;
            var result2 = controller.Index("byCuisine") as ViewResult;
            var data2 = result2.Model as List<Restaurant>;

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
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            RestaurantsController controller = new RestaurantsController(fakeRestaurantRepository, fakeReviewRepository);
            string e1 = "Meat";
            string e2 = "Meat";

            //Act
            var result1 = controller.Details(1) as ViewResult;
            var data1 = result1.Model as RestaurantDetailsVM;
            var result2 = controller.Details(2) as ViewResult;
            var data2 = result2.Model as RestaurantDetailsVM;

            //Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual(e1, data1.Restaurant.Cuisine);
            Assert.IsNotNull(result2);
            Assert.AreEqual(e2, data2.Restaurant.Cuisine);
        }

        [TestMethod]
        public void TestRestaurantsCreate()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            RestaurantsController controller = new RestaurantsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestRestaurantsEdit()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            RestaurantsController controller = new RestaurantsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Edit(1) as ViewResult;
            var data = result.Model as Restaurant;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(data.Name, "McD");
        }

        [TestMethod]
        public void TestRestaurantsDelete()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            RestaurantsController controller = new RestaurantsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Delete(1) as ViewResult;
            var data = result.Model as Restaurant;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(data.Name, "McD");
        }
    }
}
