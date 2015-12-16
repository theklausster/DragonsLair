using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDAL.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        T Create(T entity);
        bool Update(T entity);
        void Delete(int id);

       
    }
}