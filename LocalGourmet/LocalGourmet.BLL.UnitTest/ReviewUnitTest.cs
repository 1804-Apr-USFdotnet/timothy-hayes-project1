using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class ReviewUnitTest
    {
        [TestMethod]
        public void TestReviewDefaultConstructor()
        {
            Review r = new Review();
            Assert.AreNotEqual(r, null);
        }

        [TestMethod]
        public void TestReviewNameCommentConstructor()
        {
            Review r = new Review("James Alexander", "Great!");
            Assert.AreNotEqual(r, null);
            Assert.AreEqual("James Alexander", r.ReviewerName);
            Assert.AreEqual("Great!", r.Comment);
        }

        [TestMethod]
        public void TestReviewNameCommentRatingConstructor()
        {
            Review r = new Review("James Alexander", "Great!", 4);
            Assert.AreNotEqual(r, null);
            Assert.AreEqual("James Alexander", r.ReviewerName);
            Assert.AreEqual("Great!", r.Comment);
            Assert.AreEqual(4, r.FoodRating);
            Assert.AreEqual(4, r.ServiceRating);
            Assert.AreEqual(4, r.AtmosphereRating);
            Assert.AreEqual(4, r.PriceRating);

            r = new Review("James Alexander", "Great!", -10);
            Assert.AreNotEqual(r, null);
            Assert.AreEqual("James Alexander", r.ReviewerName);
            Assert.AreEqual("Great!", r.Comment);
            Assert.AreEqual(0, r.FoodRating);
            Assert.AreEqual(0, r.ServiceRating);
            Assert.AreEqual(0, r.AtmosphereRating);
            Assert.AreEqual(0, r.PriceRating);

            r = new Review("James Alexander", "Great!", 100);
            Assert.AreNotEqual(r, null);
            Assert.AreEqual("James Alexander", r.ReviewerName);
            Assert.AreEqual("Great!", r.Comment);
            Assert.AreEqual(5, r.FoodRating);
            Assert.AreEqual(5, r.ServiceRating);
            Assert.AreEqual(5, r.AtmosphereRating);
            Assert.AreEqual(5, r.PriceRating);
        }

        [TestMethod]
        public void TestReviewFullConstructor()
        {
            Review r = new Review("James Alexander", "Great!", 4, -5, 2, 10);
            Assert.AreNotEqual(r, null);
            Assert.AreEqual("James Alexander", r.ReviewerName);
            Assert.AreEqual("Great!", r.Comment);
            Assert.AreEqual(4, r.FoodRating);
            Assert.AreEqual(0, r.ServiceRating);
            Assert.AreEqual(2, r.AtmosphereRating);
            Assert.AreEqual(5, r.PriceRating);
        }

        [TestMethod]
        public void TestGetRating()
        {
            // Arrange
            Review r = new Review("John Smith", "Yuck! Nice location though.");
            r.ServiceRating = 3;
            r.PriceRating = 3;
            r.FoodRating = 0;
            r.AtmosphereRating = 4;

            // 1000 brought down to max of 5
            Review r2 = new Review("X", "Great!", 1000);

            // 0 + 3 + 3 + 3 = 9....9/4 = 2.25
            Review r3 = new Review("James", "");
            r3.FoodRating = -4;

            // 0 + 2 + 4 + 5 = 11.....11/4= 2.75
            Review r4 = new Review("x", "a", -100, 2, 4, 5);

            // Act
            float e1 = 2.5f;
            float a1 = r.GetRating();

            float e2 = 5f;
            float a2 = r2.GetRating();

            float e3 = 2.25f;
            float a3 = r3.GetRating();

            float e4 = 2.75f;
            float a4 = r4.GetRating();

            // Assert
            Assert.AreEqual(e1, a1);
            Assert.AreEqual(e2, a2);
            Assert.AreEqual(e3, a3);
            Assert.AreEqual(e4, a4);
        }

        [TestMethod]
        public void TestSortByReviewerNameAsc()
        {
            // Arrange
            IEnumerable<Review> reviews = new List<Review>()
            {
                new Review { ID=1, ReviewerName= "Cade" },
                new Review { ID=2, ReviewerName= "Billy" },
                new Review { ID=3, ReviewerName= "Adam" }
            };

            IEnumerable<Review> list = ReviewService.SortByReviewerNameAsc(reviews);
            int expectedIDFirst = 3;
            int expectedIDLast = 1;

            // Act 
            int actualFirst = list.First().ID;
            int actualLast = list.Last().ID;

            // Assert
            Assert.AreEqual(expectedIDFirst, actualFirst);
            Assert.AreEqual(expectedIDLast, actualLast);
        }

        [TestMethod]
        public void TestSortByCommentAsc()
        {
            // Arrange
            IEnumerable<Review> reviews = new List<Review>()
            {
                new Review { ID=2, RestaurantID=3, Comment="Great!"},
                new Review { ID=3, RestaurantID=2, Comment="Bleh!"},
                new Review { ID=1, RestaurantID=5, Comment="Meh!" }
            };


            IEnumerable<Review> list = ReviewService.SortByCommentAsc(reviews);
            int expectedRevIDFirst = 3;
            int expectedRevIDLast = 1;

            // Act 
            int actualFirstID = reviews.First().ID;
            int actualLastID = reviews.Last().ID;

            // Assert
            Assert.AreEqual(expectedRevIDFirst, actualFirstID);
            Assert.AreEqual(expectedRevIDLast, actualLastID);
        }

        [TestMethod]
        public void TestSortByOverallRatingDesc()
        {
            // Arrange
            IEnumerable<Review> reviews = new List<Review>()
            {
                new Review { ID=2, AtmosphereRating=3, FoodRating=3, ServiceRating=3, PriceRating=3},
                new Review { ID=1, AtmosphereRating=1, FoodRating=5, ServiceRating=5, PriceRating=5},
                new Review { ID=3, AtmosphereRating=2, FoodRating=2, ServiceRating=2, PriceRating=1}
            };

            IEnumerable<Review> list = ReviewService.SortByOverallRatingDesc(reviews);
            int expectedRevIDFirst = 1;
            int expectedRevIDLast = 3;

            // Act 
            int actualFirstID = reviews.First().ID;
            int actualLastID = reviews.Last().ID;

            // Assert
            Assert.AreEqual(expectedRevIDFirst, actualFirstID);
            Assert.AreEqual(expectedRevIDLast, actualLastID);
        }
    }
}
