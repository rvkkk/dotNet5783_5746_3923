using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList?> GetAll(Func<ProductForList?, bool>? func = null);
        public Product Get(int ID);
        public ProductItem Get(int ID, Cart cart);
        public void Add(Product p);
        public void Delete(int ID);
        public void Update(Product p);
    }
}
