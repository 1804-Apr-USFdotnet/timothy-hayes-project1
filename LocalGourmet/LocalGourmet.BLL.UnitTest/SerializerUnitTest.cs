using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LocalGourmet.BLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocalGourmet.BLL.UnitTest
{
    [TestClass]
    public class SerializerUnitTest
    {
        [TestMethod]
        public void TestSerialization()
        {
            Restaurant r = new Restaurant();
            r.Name = "Subway";
            r.Location = "14961 N Florida Ave, Suite 14961, Florida " +
                "Crossing Shopping Cntr, Tampa, FL 33613, USA";
            r.Cuisine = "American";
            r.PhoneNumber = "(813) 963-2670";
            r.WebAddress = "www.subway.com";
            r.Type = "Fast Food";
            r.Specialty = "Subs";
            r.Hours = "Mon-Sat 8am-10pm, Sun 9am-9pm";
            Review rev1 = new Review("James", "Great!", 5);
            Review rev2 = new Review("Bob", "Pretty good", 3);
            Review rev3 = new Review("Alice", "Too busy", 1);
            r.Reviews.Add(rev1);
            r.Reviews.Add(rev2);
            r.Reviews.Add(rev3);

            Restaurant r2 = new Restaurant();
            r2.Name = "Villa Gallace";
            r2.Location = "109 Gulf Blvd, Indian Rocks Beach, FL 33785, USA";
            r2.Cuisine = "Italian";
            r2.PhoneNumber = "(727) 596-0200";
            r2.WebAddress = "www.villagallace.com";
            r2.Type = "Fine Restaurant";
            r2.Hours = "Mon-Sat 5pm-10:30pm, Sun 4pm-10pm";

            Restaurant r3 = new Restaurant();
            r3.Name = "Three Coins Diner";
            r3.Location = "7410 N Nebraska Ave, Tampa, FL 33604, USA";
            r3.Cuisine = "American";
            r3.PhoneNumber = "(813) 239-1256";
            r3.WebAddress = "threecoinsdiner.net";
            r3.Type = "Diner";
            r3.Hours = "Mon-Sun 12am-11:30pm";

            string expected = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest.json");

            List<Restaurant> actual = new List<Restaurant>();
            actual.Add(r);
            actual.Add(r2);
            actual.Add(r3);
            string expectedNoWhitespace = Regex.Replace(expected, @"\s+", "");
            string serializedObj = Serializer.Serialize(actual);
            string serializedObjNoWhitespace = 
                Regex.Replace(serializedObj, @"\s+", "");
            Assert.AreEqual(expectedNoWhitespace, serializedObjNoWhitespace);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            Restaurant r = new Restaurant();
            r.Name = "Subway";
            r.Location = "14961 N Florida Ave, Suite 14961, Florida " +
                "Crossing Shopping Cntr, Tampa, FL 33613, USA";
            r.Cuisine = "American";
            r.PhoneNumber = "(813) 963-2670";
            r.WebAddress = "www.subway.com";
            r.Type = "Fast Food";
            r.Specialty = "Subs";
            r.Hours = "Mon-Sat 8am-10pm, Sun 9am-9pm";
            Review rev1 = new Review("James", "Great!", 5);
            Review rev2 = new Review("Bob", "Pretty good", 3);
            Review rev3 = new Review("Alice", "Too busy", 1);
            r.Reviews.Add(rev1);
            r.Reviews.Add(rev2);
            r.Reviews.Add(rev3);

            Restaurant r2 = new Restaurant();
            r2.Name = "Villa Gallace";
            r2.Location = "109 Gulf Blvd, Indian Rocks Beach, FL 33785, USA";
            r2.Cuisine = "Italian";
            r2.PhoneNumber = "(727) 596-0200";
            r2.WebAddress = "www.villagallace.com";
            r2.Type = "Fine Restaurant";
            r2.Hours = "Mon-Sat 5pm-10:30pm, Sun 4pm-10pm";

            Restaurant r3 = new Restaurant();
            r3.Name = "Three Coins Diner";
            r3.Location = "7410 N Nebraska Ave, Tampa, FL 33604, USA";
            r3.Cuisine = "American";
            r3.PhoneNumber = "(813) 239-1256";
            r3.WebAddress = "threecoinsdiner.net";
            r3.Type = "Diner";
            r3.Hours = "Mon-Sun 12am-11:30pm";

            string jsonStr = System.IO.File.ReadAllText(@"C:\revature\" + 
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest.json");

            List<Restaurant> expected = new List<Restaurant>();
            expected.Add(r);
            expected.Add(r2);
            expected.Add(r3);

            List<Restaurant> actual = 
                Serializer.Deserialize<List<Restaurant>>(jsonStr);
            Assert.AreEqual(expected[0].ToString(), actual[0].ToString());
            Assert.AreEqual(expected[1].ToString(), actual[1].ToString());
            Assert.AreEqual(expected[2].ToString(), actual[2].ToString());
        }
    }
}
