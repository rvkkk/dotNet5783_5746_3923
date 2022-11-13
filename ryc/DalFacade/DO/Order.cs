using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// 
/// </summary>
public struct Order
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    public override string ToString() => $@"
Order ID={ID}:
customer: name={CustomerName} email={CustomerEmail} address={CustomerAddress}, 
date: {OrderDate}
ship date: {OrderDate}
delivery date: {ShipDate}
";
}