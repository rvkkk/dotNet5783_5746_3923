using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// an item in order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// orderItem id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// order id
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// product id
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// price of item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// amount of items
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// get a string that represents orderItem
    /// </summary>
    /// <returns>details of orderItem</returns>
    public override string ToString() => $@"
Order item ID={ID},
order id: {OrderID}
product id: {ProductID}
Price: {Price}
Amount: {Amount}
";
}