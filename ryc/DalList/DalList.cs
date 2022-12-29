using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        private DalList() {}
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();
        public IProduct Product => new DalProduct();
    }
}