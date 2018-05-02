using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Interfaces
{
    interface IRestaurant
    {
        string Name { get; set; }
        string Location { get; set; }
        string Cuisine { get; set; } // Examples: Italian, French, Indian, ...
        string Type { get; set; } // Examples: Bakery, Pizzeria, Diner, ... 
        string Hours { get; set; } // Operating Hours
        string PhoneNumber { get; set; }
        string WebAddress { get; set; }
        List<Review> Reviews { get; set; }

        double GetAvgRating();
    }
}
