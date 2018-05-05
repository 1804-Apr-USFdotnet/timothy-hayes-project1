using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.Controllers;

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
        }

        [TestMethod]
        public void TestRestaurantsCreate()
        {
        }

        [TestMethod]
        public void TestRestaurantsEdit()
        {
        }

        [TestMethod]
        public void TestRestaurantsDelete()
        {
        }
    }
}
