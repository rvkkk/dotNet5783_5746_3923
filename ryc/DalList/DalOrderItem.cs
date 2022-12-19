using DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// adds a new orderItem
    /// </summary>
    /// <param name="o">recived orderItem</param>
    /// <returns>the orderItem id</returns>
    public int Add(OrderItem o)
    {
        o.ID = DataSource.Config.OrderItemID;
        DataSource.lOrderItem.Add(o);
        return o.ID;
    }
    /// <summary>
    /// deletes orderItem
    /// </summary>
    /// <param name="ID">recived orderItem id</param>
    /// <exception cref="Exception">there in no such an orderItem id</exception>
    public void Delete(int ID)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (DataSource.lOrderItem[i]?.ID == ID)
            {
                DataSource.lOrderItem.Remove(DataSource.lOrderItem[i]);
                flag = true;
                break;
            }
        }
        if (!flag)
            throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// updates orderItem
    /// </summary>
    /// <param name="o">recived orderItem</param>
    public void Update(OrderItem o)
    {
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (DataSource.lOrderItem[i]?.ID == o.ID)
                DataSource.lOrderItem[i] = o;
        }
    }
    /// <summary>
    /// return orderItem by id
    /// </summary>
    /// <param name="ID">recived orderItem id</param>
    /// <returns>orderItem</returns>
    /// <exception cref="Exception">there in no such an orderItem id</exception>
    public OrderItem Get(int ID)
    {
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (DataSource.lOrderItem[i]?.ID == ID)
                return (OrderItem)DataSource.lOrderItem[i]!;
        }
        throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// returns orderItem by a function
    /// </summary>
    /// <param name="func">a delegate</param>
    /// <returns>orderItem</returns>
    /// <exception cref="InvalidID">there in no such an orderItem</exception>
    public OrderItem GetByF(Func<OrderItem?, bool>? func)
    {
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (func!(DataSource.lOrderItem[i]))
                return (OrderItem)DataSource.lOrderItem[i]!;
        }
        throw new InvalidID("there in no such an order item");
    }
    /// <summary>
    /// returns all orderItems
    /// </summary>
    /// <returns>orderItems</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        List<OrderItem?> lorderItems = new List<OrderItem?>();
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (func == null)
                lorderItems.Add(DataSource.lOrderItem[i]);
            else if (func(DataSource.lOrderItem[i]))
                lorderItems.Add(DataSource.lOrderItem[i]);
        }
        return lorderItems;
    } 
}