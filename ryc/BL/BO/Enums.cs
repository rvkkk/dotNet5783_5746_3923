using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Enums
    {
        /// <summary>
        /// an enum of burgeranch categories
        /// </summary>
        public enum Category { BURGERS, EXTRAS, DESSERTS, JUICES, FOODֹ_COMBOS, NONE };

        /// <summary>
        /// an enum of order status
        /// </summary>
        public enum OrderStatus { OrderConfirmed, Shipped, DeliveredToCustomer, NONE };
    }
}
