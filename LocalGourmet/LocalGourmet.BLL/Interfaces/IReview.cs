using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.BLL.Interfaces
{
    interface IReview
    {
        // Calculates and returns the overall review rating.
        float GetRating(); 
        string ReviewerName { get; set; }
        string Comment { get; set; }
    }
}
