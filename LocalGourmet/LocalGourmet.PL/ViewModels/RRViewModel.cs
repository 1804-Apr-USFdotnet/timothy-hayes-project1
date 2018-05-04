using LocalGourmet.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalGourmet.PL.ViewModels
{
    public class RRViewModel
    {
        public Restaurant Restaurant { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}