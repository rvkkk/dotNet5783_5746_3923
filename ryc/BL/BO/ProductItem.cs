using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class ProductItem
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// name of product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// price of product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// category of product
        /// </summary>
        public Category? Category { get; set; }
        /// <summary>
        /// if the product exist in stock
        /// </summary>
        public bool InStock { get; set; }
        /// <summary>
        /// amount of product in stock
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// get a string that represents product item
        /// </summary>
        /// <returns>details of product item</returns>
        public override string ToString() => $@"
Product item ID={ID}
name={Name}
category - {Category}
price: {Price}
in stock: {InStock}
amount in order: {Amount}
";
    }
}
