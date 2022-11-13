using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;

namespace DO;

/// <summary>
/// 
/// </summary>
public struct Product
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() => $@"
Product ID={ID} name={Name}, 
category - {Category}
Price: {Price}
Amount in stock: {InStock}
";
}