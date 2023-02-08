using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// order id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// order status
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// list of satuses of order
        /// </summary>
        public IEnumerable<Tuple<DateTime?, string?>?>? list { get; set; }
        /// <summary>
        /// get a string that represents order tracking
        /// </summary>
        /// <returns>details of order tracking</returns>
        public override string ToString()
        {
            string s =$@"
Order tracking ID={ID}:
status: {Status}
status list: 
";
            foreach (var item in list!)
            {
                s+= item?.Item1 + " ";
                s+=item?.Item2 + "\n";
            }
            return s;
        }
    }
}
