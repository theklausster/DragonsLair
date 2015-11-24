using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDAL.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Post(T entity);
        bool Put(T entity);
        void Delete(int id);
    }
}