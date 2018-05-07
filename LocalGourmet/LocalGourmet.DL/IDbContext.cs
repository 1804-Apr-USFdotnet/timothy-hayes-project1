using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DL
{
    public interface IDbContext
    {
        int SaveChanges();
    }
}
