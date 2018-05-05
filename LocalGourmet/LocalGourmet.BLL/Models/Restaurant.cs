using LocalGourmet.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using LocalGourmet.DAL.Repositories;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LocalGourmet.DL;

namespace LocalGourmet.BLL.Models
{
    [DataContract]
    public class Restaurant : IRestaurant
    {
        private RestaurantAccessor crud;

        #region Constructors
        public Restaurant() {
            Reviews = new List<Review>();
            Active = true;
            crud = new RestaurantAccessor();
        }

        public Restaurant(LocalGourmetDBEntities db) {
            Reviews = new List<Review>();
            Active = true;
            crud = new RestaurantAccessor(db);
        }
        #endregion

        #region Properties
        public int ID { get; set; }
        public bool Active { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; } 
        [DataMember]
        [Required]
        public string Location { get; set; }
        [DataMember]
        [Required]
        public string Cuisine { get; set; }
        [DataMember]
        [Required]
        public string Specialty { get; set; }
        [DataMember]
        [Required]
        [RegularExpression(@"^([0-9]{3}-[0-9]{3}-[0-9]{4})$", ErrorMessage = "Invalid Phone Number. Format is: 111-222-3333")]
        public string PhoneNumber { get; set; }
        [DataMember]
        [Required]
        [Url]
        public string WebAddress { get; set; }
        [DataMember]
        public List<Review> Reviews { get; set; }
        [DataMember]
        [Required]
        public string Type { get; set; }
        [DataMember]
        [Required]
        public string Hours { get; set; }
        #endregion

        #region Getters
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

        public double GetAvgRating()
        {
            if (Reviews == null || Reviews.Count == 0) { return 0.0f; }
            return Math.Round(Reviews.Average(x => x.GetRating()), 2);
        }

        public static List<Restaurant> GetTop3(List<Restaurant> restaurants)
        {
            return restaurants.OrderByDescending(x => x.GetAvgRating()).Take(3).ToList();
        }
        #endregion

        #region CRUD
        public void Add()
        {
            crud.Add(LibraryToData(this));
        }

        public static IEnumerable<Restaurant> GetAll()
        {
            RestaurantAccessor crudTemp = new RestaurantAccessor();
            return crudTemp.GetAll().Select(x => DataToLibrary(x)).ToList();
        }

        public static Restaurant GetByID(int id)
        {
            RestaurantAccessor crudTemp = new RestaurantAccessor();
            Restaurant r;
            try
            {
                r = DataToLibrary(crudTemp.GetById(id));
            }
            catch
            {
                throw;
            }
            return r;
        }
        
        public void Update(Restaurant r)
        {
            try
            {
                crud.Update(LibraryToData(r));
            }
            catch
            {
                throw;
            }
        }

        public void Delete()
        {
            try
            {
                crud.Delete(LibraryToData(this));
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Sort & Search
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
            return Restaurant.SearchByName(list, search);
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

        #region Display
        public delegate List<Restaurant> SortDel(List<Restaurant> list);
        public delegate void DisplayDel(SortDel sortDel, List<Restaurant> list);

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

        #region ToString
        // Return name and rating only
        public string GetNameAndRating()
        {
            return $"{Name} [{GetAvgRating()}]";
        }

        // Return summary of info
        public string GetSummary()
        {
            return $"{Name}, {Cuisine}, {Reviews.Count} Reviews, " +
                $"{Type}, AvgRating: {GetAvgRating()}";
        }

        // Return all info
        public override string ToString()
        {
            return $"{Name}, {Cuisine}, {Type}, {Specialty}, " +
                $"AvgRating: {GetAvgRating()}, {Reviews.Count} Reviews, " +
                $"{Location}, {PhoneNumber}, {WebAddress}, {Hours}";
        }
        #endregion

        #region BLL-DL Mappers
        public static BLL.Models.Restaurant DataToLibrary(DL.Restaurant dataModel)
        {
            int restID = dataModel.ID;
            List<Review> revs = Review.GetReviewsByRestaurantID(restID);

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
