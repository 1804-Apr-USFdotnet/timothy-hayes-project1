using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL.Interfaces
{
    public interface ICrud<T>
    {
        void AddAsync(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
