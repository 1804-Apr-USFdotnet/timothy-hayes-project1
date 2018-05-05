using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL.Repositories
{
    public interface ICrud<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Delete(T entity);
    }
}
