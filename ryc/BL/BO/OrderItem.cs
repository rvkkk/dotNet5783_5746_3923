using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// order item id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// product id
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// name of product 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// price of product 
        /// </summary>
        public double ProductPrice { get; set; }
        /// <summary>
        /// amount of product in order
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// total price of product in order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// get a string that represents order item
        /// </summary>
        /// <returns>details of order item</returns>
        public override string ToString() => $@"
Order item ID={ID}:
product ID: {ProductID}
product name: {ProductName}
product price: {ProductPrice}
amount : {Amount}
total price: {TotalPrice}
";
    }
}