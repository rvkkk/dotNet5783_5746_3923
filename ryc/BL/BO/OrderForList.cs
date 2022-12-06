using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class OrderForList
    {
        /// <summary>
        /// order id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// name of customer
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// order status
        /// </summary>
        public OrderStatus Status { get; set; }
        /// <summary>
        /// amount of items
        /// </summary>
        public int AmountOfItems { get; set; }
        /// <summary>
        /// price of order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// get a string that represents order
        /// </summary>
        /// <returns>details of order</returns>
        public override string ToString() => $@"
Order ID={ID}:
customer: name={CustomerName}
status: {Status}
items amount: {AmountOfItems}
total price: {TotalPrice}
";
    }
}