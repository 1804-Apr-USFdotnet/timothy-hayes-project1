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
    public class FakeReviewRepository : ReviewRepository
    {
        private List<Review> reviews;
        public FakeReviewRepository()
        {
            reviews = new List<Review>()
            {
                new Review { ID=1, ReviewerName="A", AtmosphereRating=5, PriceRating=5, FoodRating=5, ServiceRating=5},
                new Review { ID=2, ReviewerName="B", AtmosphereRating=1, PriceRating=1, FoodRating=1, ServiceRating=1},
                new Review { ID=3, ReviewerName="C", AtmosphereRating=3, PriceRating=3, FoodRating=4, ServiceRating=4}
            };
        }

        public override IEnumerable<Review> GetAll()
        {
            return reviews;
        }

        public override void Add(Review r)
        {
            reviews.Add(r);
        }

        public override void Delete(Review r)
        {
            reviews.Remove(r);
        }

        public override Review GetById(int id)
        {
            return reviews.First(x => x.ID == id);
        }

        public override void Update(Review r)
        {
            reviews.RemoveAt(reviews.IndexOf(r));
            reviews.Add(r);
        }

        public override List<Review> GetReviewsByRestaurantID(int restID)
        {
            return reviews.Where(x => x.RestaurantID == restID).ToList();
        }
    }

    [TestClass]
    public class ReviewsControllerUnitTest
    {
        [TestMethod]
        public void TestReviewsIndex()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            ReviewsController controller = new ReviewsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestReviewsDetails()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            ReviewsController controller = new ReviewsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Details(1) as ViewResult;
            var data = result.Model as Review;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("A", data.ReviewerName);
        }

        [TestMethod]
        public void TestReviewsCreate()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            ReviewsController controller = new ReviewsController(fakeRestaurantRepository, fakeReviewRepository);
            ReviewsCreateVM vm = new ReviewsCreateVM(fakeRestaurantRepository);
            string e2RestName = "McD";

            //Act
            var result = controller.Create(vm) as ActionResult;
            var result2 = controller.Create(1) as ViewResult;
            var data2 = result2.Model as ReviewsCreateVM;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(e2RestName, data2.Restaurants.First(x=>x.ID == data2.Review.RestaurantID).Name);
        }

        [TestMethod]
        public void TestReviewsEdit()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            ReviewsController controller = new ReviewsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Edit(3) as ViewResult;
            var data = result.Model as ReviewsEditVM;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("C", data.Review.ReviewerName);
        }

        [TestMethod]
        public void TestReviewsDelete()
        {
            //Arrange
            FakeRestaurantRepository fakeRestaurantRepository = new FakeRestaurantRepository();
            FakeReviewRepository fakeReviewRepository = new FakeReviewRepository();
            ReviewsController controller = new ReviewsController(fakeRestaurantRepository, fakeReviewRepository);

            //Act
            var result = controller.Delete(2) as ViewResult;
            var data = result.Model as Review;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("B", data.ReviewerName);
        }
    }
}
