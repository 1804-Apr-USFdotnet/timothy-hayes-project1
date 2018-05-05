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
    public class ReviewsControllerUnitTest
    {
        [TestMethod]
        public void TestReviewsIndex()
        {
            //Arrange
            ReviewsController controller = new ReviewsController();

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestReviewsDetails()
        {
            //Arrange
            ReviewsController controller = new ReviewsController();

            //Act
            var result = controller.Details(5100) as ViewResult;
            var data = result.Model as Review;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Danit Isahella", data.ReviewerName);
        }

        [TestMethod]
        public void TestReviewsCreate()
        {
            //Arrange
            ReviewsController controller = new ReviewsController();

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestReviewsEdit()
        {
            //Arrange
            ReviewsController controller = new ReviewsController();

            //Act
            var result = controller.Edit(5100) as ViewResult;
            var data = result.Model as ReviewsEditVM;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Danit Isahella", data.Review.ReviewerName);
        }

        [TestMethod]
        public void TestReviewsDelete()
        {
            //Arrange
            ReviewsController controller = new ReviewsController();

            //Act
            var result = controller.Delete(5100) as ViewResult;
            var data = result.Model as Review;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Danit Isahella", data.ReviewerName);
        }
    }
}
