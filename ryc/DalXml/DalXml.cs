using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private DalXml() { }
        public IProduct Product { get; } = new Product();
        public IOrder Order { get; } = new Order();
        public IOrderItem OrderItem { get; } = new OrderItem();
        public static IDal Instance { get; } = new DalXml();
    }
}