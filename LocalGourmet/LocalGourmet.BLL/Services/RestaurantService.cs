using System;
using System.Collections.Generic;
using System.Linq;
using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Repositories;

namespace LocalGourmet.BLL.Services
{
    public class RestaurantService
    {
        #region Display
        public static void DisplayWithAllInfo(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant);
                Console.WriteLine();
            }
        }

        public static void DisplaySummarized(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant.GetSummary());
                Console.WriteLine();
            }
        }

        public static void DisplaySummarizedWithReviews(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant.GetSummary());
                Console.WriteLine();
                List<Review> reviews = restaurant.Reviews;
                foreach (var r in reviews)
                {
                    Console.WriteLine(r);
                }
                Console.WriteLine();
                Console.WriteLine("*********************************************");
                Console.WriteLine("*********************************************");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void DisplayAllInfoWithReviews(List<Restaurant> list)
        {
            foreach (var restaurant in list)
            {
                Console.WriteLine(restaurant);
                Console.WriteLine();
                List<Review> reviews = restaurant.Reviews;
                foreach (var r in reviews)
                {
                    Console.WriteLine(r);
                }
                Console.WriteLine();
                Console.WriteLine("*********************************************");
                Console.WriteLine("*********************************************");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    #endregion 

        #region Sort & Search
        public static IEnumerable<Restaurant> GetTop3(IEnumerable<Restaurant> restaurants)
        {
            return restaurants.OrderByDescending(x => x.GetAvgRating()).Take(3);
        }

        // Deprecated -- only use for serialization testing
        public static List<Restaurant> GetAllFromJSON()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string json = System.IO.File.ReadAllText(@"C:\revature\" +
                @"hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\" +
                @"Configs\RestaurantsForUnitTest2.json");
            restaurants = Serializer.Deserialize<List<Restaurant>>(json);
            return restaurants;
        }

        public static IEnumerable<Restaurant> SortByAvgRatingDesc(IEnumerable<Restaurant> list)
        {
            return list.OrderByDescending(x => x.GetAvgRating()).ToList();
        }

        public static IEnumerable<Restaurant> SortByNameAsc(IEnumerable<Restaurant> list)
        {
            return list.OrderBy(x => x.Name).ToList();
        }

        public static IEnumerable<Restaurant> SortByCuisineAsc(IEnumerable<Restaurant> list)
        {
            return list.OrderBy(x => x.Cuisine).ToList();
        }

        // Helper Search method for use with console UI
        public static IEnumerable<Restaurant> SearchByName(IEnumerable<Restaurant> list)
        {
            Console.WriteLine("Enter (partial) restaurant name:");
            Console.Write("<input> ");
            string search = Console.ReadLine();
            return RestaurantService.SearchByName(list, search);
        }

        // Return a list of all restaurants whose names were partially
        // matched by the search string.
        public static IEnumerable<Restaurant> SearchByName(IEnumerable<Restaurant> list, string search)
        {
            List<Restaurant> matches = new List<Restaurant>();
            if (search == "") { return matches; } // return with zero matches
            var result = (from r in list
                          where r.Name.ToLower().Contains(search.ToLower())
                          select r).ToList();
            foreach (var item in result)
            {
                matches.Add(item);
            }
            return matches;
        }
        #endregion

        #region BLL-DL Mappers
        public static BLL.Models.Restaurant DataToLibrary(DL.Restaurant dataModel)
        {
            ReviewRepository reviewRepository = new ReviewRepository();
            int restID = dataModel.ID;
            List<Review> revs = reviewRepository.GetReviewsByRestaurantID(restID);

            var libModel = new BLL.Models.Restaurant()
            {
                ID = dataModel.ID,
                Name = dataModel.Name,
                Location = dataModel.Location,
                Cuisine = dataModel.Cuisine,
                Specialty = dataModel.Specialty,
                PhoneNumber = dataModel.PhoneNumber,
                WebAddress = dataModel.WebAddress,
                Type = dataModel.Type,
                Hours = dataModel.Hours,
                Active = dataModel.Active,
                Reviews = revs // Eager loading
            };
            return libModel;
        }

        public static DL.Restaurant LibraryToData(BLL.Models.Restaurant libModel)
        {
            var dataModel = new DL.Restaurant();
            {
                dataModel.ID = libModel.ID;
                dataModel.Name = libModel.Name;
                dataModel.Location = libModel.Location;
                dataModel.Cuisine = libModel.Cuisine;
                dataModel.Specialty = libModel.Specialty;
                dataModel.PhoneNumber = libModel.PhoneNumber;
                dataModel.WebAddress = libModel.WebAddress;
                dataModel.Type = libModel.Type;
                dataModel.Hours = libModel.Hours;
                dataModel.Active = libModel.Active;
            };
            return dataModel;
        }
        #endregion
    }
}
