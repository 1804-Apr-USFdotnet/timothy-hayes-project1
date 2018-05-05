using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.Controllers;
using LocalGourmet.PL.ViewModels;

namespace LocalGourmet.PL.UnitTest
{
    [TestClass]
    public class RestaurantsControllerUnitTest
    {
        [TestMethod]
        public void TestRestaurantsIndex()
        {
            //Arrange
            RestaurantsController controller = new RestaurantsController();

            //Act
            var result1 = controller.Index("byName") as ViewResult;
            var data1 = result1.Model as List<Restaurant>;

            var result2 = controller.Index("byRating") as ViewResult;
            var data2 = result2.Model as List<Restaurant>;

            //Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual("Columbia Restaurant", data1[0].Name);

            Assert.IsNotNull(result2);
            Assert.AreEqual("Yummy House China Bistro", data2[0].Name);
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
