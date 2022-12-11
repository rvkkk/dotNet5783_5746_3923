using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class Product
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// product name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// product price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// product category
        /// </summary>
        public Category? Category { get; set; }
        /// <summary>
        /// amount in stock
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// get a string that represents product
        /// </summary>
        /// <returns>details of product</returns>
        public override string ToString() => $@"
Product ID={ID} name={Name}
category - {Category}
price: {Price}
amount in stock: {InStock}
";
    }
}
