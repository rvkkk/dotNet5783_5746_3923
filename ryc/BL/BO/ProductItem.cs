using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    internal class ProductItem
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public bool InStock { get; set; }
        public int Amount { get; set; }
    }
}
