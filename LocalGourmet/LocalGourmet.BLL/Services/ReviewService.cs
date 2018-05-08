using System;
using System.Collections.Generic;
using System.Linq;
using LocalGourmet.BLL.Models;
using LocalGourmet.BLL.Repositories;

namespace LocalGourmet.BLL.Services
{
    public class ReviewService
    {
        // Returns a custom list of reviews, with RestaurantIDs from 1-10
        public static Review[] GenerateReviews(int howMany)
        {
            RestaurantRepository restaurantRepository = new RestaurantRepository();
            string[] names = new string[4945];
            string nameString = System.IO.File.ReadAllText(@"C:\revature\hayes-timothy-project0\LocalGourmet\LocalGourmet.BLL\Configs\Names.txt");
            System.IO.StringReader r = new System.IO.StringReader(nameString);
            for(int i = 0; i < 4945; i++)
            {
                names[i] = r.ReadLine();
            }

            Review r1 = new Review("", "I'm never coming here again!", 0, 1, 1, 1);
            Review r2 = new Review("", "I'd rather eat bread and water.", 1, 2, 1, 0);
            Review r3 = new Review("", "Bleh!", 1, 2, 1, 2);
            Review r4 = new Review("", "I hope no one saw me eat here.", 2, 3, 1, 2);
            Review r5 = new Review("", "Better than starving...", 2, 3, 2, 3);
            Review r6 = new Review("", "At least it was cheap.", 3, 2, 4, 3);
            Review r7 = new Review("", "I'd come here again.", 3, 4, 3, 4);
            Review r8 = new Review("", "I had great expectations...", 2, 3, 4, 4);
            Review r9 = new Review("", "Wow!", 4, 4, 5, 4);
            Review r10 = new Review("", "Best restaurant ever!", 5, 5, 5, 5);

            Review[] revs = new Review[10];
            revs[0] = r1;
            revs[1] = r2;
            revs[2] = r3;
            revs[3] = r4;
            revs[4] = r5;
            revs[5] = r6;
            revs[6] = r7;
            revs[7] = r8;
            revs[8] = r9;
            revs[9] = r10;

            int revIndex;
            string firstName, lastName;
            Random rnd = new Random();
            Review[] customReviews = new Review[howMany];
            for(int i = 0; i < howMany; i++)
            {
                revIndex = rnd.Next(10);
                Review customRev = new Review();
                Review q = revs[revIndex];
                customRev.Comment = q.Comment;
                customRev.FoodRating = q.FoodRating;
                customRev.ServiceRating = q.ServiceRating;
                customRev.AtmosphereRating = q.AtmosphereRating;
                customRev.PriceRating = q.PriceRating;
                firstName = names[rnd.Next(4945)];
                lastName = names[rnd.Next(4945)];
                customRev.ReviewerName = $"{firstName} {lastName}";
                List<int> restIds = restaurantRepository.GetAll().Select(x => x.ID).ToList();
                int numRests = restIds.Count;
                customRev.RestaurantID = restIds[rnd.Next(numRests)];
                customReviews[i] = customRev;
            }
            return customReviews;
        }

        public static IEnumerable<Review> SortByReviewerNameAsc(IEnumerable<Review> list)
        {
            return list;
        }

        public static IEnumerable<Review> SortByOverallRatingDesc(IEnumerable<Review> list)
        {
            return list;
        }

        public static IEnumerable<Review> SortByCommentAsc(IEnumerable<Review> list)
        {

            return list;
        }



        #region BLL-DL Mappers
        public static BLL.Models.Review DataToLibrary(DL.Review dataModel)
        {
            int revID = dataModel.ID;

            var libModel = new BLL.Models.Review()
            {
                ID = dataModel.ID,
                RestaurantID = dataModel.RestaurantID,
                ReviewerName = dataModel.ReviewerName,
                Comment = dataModel.Comment,
                FoodRating = dataModel.FoodRating,
                ServiceRating = dataModel.ServiceRating,
                AtmosphereRating = dataModel.AtmosphereRating,
                PriceRating = dataModel.PriceRating
            };
            return libModel;
        }

        public static DL.Review LibraryToData(BLL.Models.Review libModel)
        {
            var dataModel = new DL.Review();
            {
                dataModel.ID = libModel.ID;
                dataModel.RestaurantID = libModel.RestaurantID;
                dataModel.ReviewerName = libModel.ReviewerName;
                dataModel.Comment = libModel.Comment;
                dataModel.FoodRating = libModel.FoodRating;
                dataModel.ServiceRating = libModel.ServiceRating;
                dataModel.AtmosphereRating = libModel.AtmosphereRating;
                dataModel.PriceRating = libModel.PriceRating;
            };
            return dataModel;
        }
        #endregion
    }
}
