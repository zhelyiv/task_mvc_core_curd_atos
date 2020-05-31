using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICrudOperation<T> : IDisposable
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(int id);
        T GetSingle(int id);
        void Update(T user);
        void Delete(int id);
        void Insert(T user);
    }
}
