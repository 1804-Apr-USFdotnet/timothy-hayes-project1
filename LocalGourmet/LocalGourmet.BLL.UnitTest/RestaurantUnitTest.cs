using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class RestaurantUnitTest
    {
        [TestMethod]
        public void TestRestaurantDefaultConstructor()
        {
            // Arrange
            Restaurant r = new Restaurant();

            // Act

            // Assert
            Assert.AreNotEqual(r, null);
            Assert.AreNotEqual(r.Reviews, null);
            Assert.AreEqual(r.Active, true);
        }

        [TestMethod]
        public void TestRestaurantGetAvgRating()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);
            Restaurant subway = restaurants[0];
            double expectedRating = 3.0;

            // Act
            double actualRating = subway.GetAvgRating();

            // Assert
            Assert.AreEqual(expectedRating, actualRating);

        }

        [TestMethod]
        public void TestGetTop3()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            // Act
            List<Restaurant> expected = new List<Restaurant>();
            expected.Add(restaurants[1]);
            expected.Add(restaurants[2]);
            expected.Add(restaurants[7]);
            List<Restaurant> actual = RestaurantService.GetTop3(RestaurantService.GetAllFromJSON());

            // Assert
            Assert.AreEqual(expected[0].ToString(), actual[0].ToString());
            Assert.AreEqual(expected[1].ToString(), actual[1].ToString());
            Assert.AreEqual(expected[2].ToString(), actual[2].ToString());
        }

        [TestMethod]
        public void TestSearchByName()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            // Act
            string s1 = "sub";
            List<Restaurant> a1 = (List<Restaurant>) RestaurantService.SearchByName(RestaurantService.GetAllFromJSON(), s1);

            string s2 = "CO";
            List<Restaurant> a2 = (List<Restaurant>) RestaurantService.SearchByName(RestaurantService.GetAllFromJSON(), s2);

            // Assert
            Assert.AreEqual("Subway", a1[0].Name);
            Assert.AreEqual(1, a1.Count);

            Assert.AreEqual("Three Coins Diner", a2[0].Name);
            Assert.AreEqual("Tampa Bay Brewing Company", a2[1].Name);
            Assert.AreEqual("Columbia Restaurant", a2[2].Name);
            Assert.AreEqual(3, a2.Count);
        }

        [TestMethod]
        public void TestSortByAvgRatingDesc()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            string e2 = "Columbia Restaurant"; // rating = 4.15
            string e5 = "Tampa Bay Brewing Company"; // rating = 3.36
            string e7 = "Stonewood Grill & Tavern"; // rating = 3.25

            // Act
            List<Restaurant> a = (List<Restaurant>) RestaurantService.SortByAvgRatingDesc(RestaurantService.GetAllFromJSON());

            // Assert
            Assert.AreEqual(e2, a[2].Name);
            Assert.AreEqual(e5, a[5].Name);
            Assert.AreEqual(e7, a[7].Name);
        }

        [TestMethod]
        public void TestSortByNameAsc()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            string e1 = "Columbia Restaurant";
            string e2 = "Yummy House China Bistro";

            // Act
            List<Restaurant> a = (List<Restaurant>) RestaurantService.SortByNameAsc(RestaurantService.GetAllFromJSON());

            // Assert
            Assert.AreEqual(e1, a[0].Name);
            Assert.AreEqual(e2, a[9].Name);
        }

        [TestMethod]
        public void TestSortByCuisineAsc()
        {
            // Arrange
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);

            string e1 = "Happy Fish";
            string e2 = "Columbia Restaurant";

            // Act
            List<Restaurant> a = (List<Restaurant>) RestaurantService.SortByCuisineAsc(RestaurantService.GetAllFromJSON());

            // Assert
            Assert.AreEqual(e1, a[8].Name);
            Assert.AreEqual(e2, a[9].Name);
        }
    }
}
