using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGourmet.PL.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }
        public string Specialty { get; set; }
        public string PhoneNumber { get; set; }
        public string WebAddress { get; set; }
        public string Type { get; set; }
        public string Hours { get; set; }

        // Navigation Props
        public virtual ICollection<Review> Reviews { get; set; }

        public static PL.Models.Restaurant LibraryToWeb(BLL.Models.Restaurant libModel)
        {
            var presModel = new PL.Models.Restaurant();
            {
                presModel.ID = libModel.ID;
                presModel.Name = libModel.Name;
                presModel.Location = libModel.Location;
                presModel.Cuisine = libModel.Cuisine;
                presModel.Specialty = libModel.Specialty;
                presModel.PhoneNumber = libModel.PhoneNumber;
                presModel.WebAddress = libModel.WebAddress;
                presModel.Type = libModel.Type;
                presModel.Hours = libModel.Hours;
            };
            return presModel;
        }
    }
}