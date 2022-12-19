using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class Order
    {
        /// <summary>
        /// order id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// name of customer
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// email of customer
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// address of customer
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// order status
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// date of order
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// date of shipping
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// date of delivery
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// list of items in order
        /// </summary>
        public List<OrderItem?>? Items { get; set; }
        /// <summary>
        /// price of order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// get a string that represents order
        /// </summary>
        /// <returns>details of order</returns>
        public override string ToString()
        {
            string s = $@"
Order ID={ID}:
customer: name={CustomerName} email={CustomerEmail} address={CustomerAddress}
status: {Status}
date: {OrderDate}
ship date: {ShipDate}
delivery date: {DeliveryDate}
total price: {TotalPrice}
items: 
";
            foreach(var item in Items!)
            {
                s+=item?.ToString(); 
            }
            return s;
        }
    }
}