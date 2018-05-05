using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.Controllers;

namespace LocalGourmet.PL.UnitTest
{
    [TestClass]
    public class HomeControllerUnitTest
    {
        [TestMethod]
        public void TestHomeHome()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Home() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeAbout()
        {
        }

        [TestMethod]
        public void TestHomeContact()
        {
        }
    }
}
