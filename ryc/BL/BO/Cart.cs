using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        /// <summary>
        /// name of customer
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// email of customer
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// address of customer
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// list of items
        /// </summary>
        public List<OrderItem> Items { get; set; }
        /// <summary>
        /// the price of the cart
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// get a string that represents cart
        /// </summary>
        /// <returns>details of cart</returns>
        public override string ToString()
        {
            string s = $@"
Cart:
customer: name={CustomerName} email={CustomerEmail} address={CustomerAddress}
total price: {TotalPrice}
items: 
";
            foreach(var item in Items)
            {
                s+= item.ToString();
            }
            return s;   
        }
    }
}
