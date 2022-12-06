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
/// an order
/// </summary>
public struct Order
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// name of customer
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// email of customer
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// address of customer
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    /// date of order
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// date of shipping
    /// </summary>
    public DateTime ShipDate { get; set; }
    /// <summary>
    /// date of delivery
    /// </summary>
    public DateTime DeliveryDate { get; set; }
    /// <summary>
    /// get a string that represents order
    /// </summary>
    /// <returns>details of order</returns>
    public override string ToString() => $@"
Order ID={ID}:
customer: name={CustomerName} email={CustomerEmail} address={CustomerAddress}
date: {OrderDate}
ship date: {ShipDate}
delivery date: {DeliveryDate}
";
}