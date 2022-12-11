using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        public int Add(T t);
        public void Delete(int ID);
        public void Update(T t);
        public T Get(int ID);
        public T GetByF(Func<T?, bool>? func);
        public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);
    }
}
