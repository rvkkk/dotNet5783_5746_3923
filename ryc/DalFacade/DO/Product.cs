using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;

namespace DO;

/// <summary>
/// a product
/// </summary>
public struct Product
{
    /// <summary>
    /// product id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// category of product
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// sum of products in stock
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// get a string that represents product
    /// </summary>
    /// <returns>details of product</returns>
    public override string ToString() => $@"
Product ID={ID} name={Name}, 
category - {Category}
Price: {Price}
Amount in stock: {InStock}
";
}