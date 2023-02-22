using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public Cart AddToCart(Cart cart, int ID);
        public Cart UpdateCart(Cart cart, int ID, int amount);
        public int MakeAnOrder(Cart cart);
    }
}
