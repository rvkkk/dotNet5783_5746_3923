using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// 
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int Amount { get; set; }

    public override string ToString() => $@"
Order item ID={ID},
order id: {OrderID}
product id: {ProductID}
Price: {Price}
Amount: {Amount}
";
}