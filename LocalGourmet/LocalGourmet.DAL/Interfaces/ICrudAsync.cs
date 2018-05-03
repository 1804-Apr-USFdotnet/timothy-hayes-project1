using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalGourmet.DAL.Interfaces
{
    public interface ICrudAsync<T>
    {
        void AddAsync(T Entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void UpdateAsync(T Entity);
        void DeleteAsync(int id);
    }
}
