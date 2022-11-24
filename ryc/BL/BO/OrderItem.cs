using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class OrderItem
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
    }
}
