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
            if (DataSource.lOrderItem[i].ID == ID)
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
            if (DataSource.lOrderItem[i].ID == o.ID)
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
            if (DataSource.lOrderItem[i].ID == ID)
                return DataSource.lOrderItem[i];
        }
        throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// returns all orderItems
    /// </summary>
    /// <returns>orderItems</returns>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> lorderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            lorderItems.Add(DataSource.lOrderItem[i]);
        }
        return lorderItems;
    }
    /// <summary>
    /// returns orderItem by orderId and productId
    /// </summary>
    /// <param name="IDO">recived order id</param>
    /// <param name="IDP">recived product id</param>
    /// <returns>orderItem</returns>
    /// <exception cref="Exception">there in no such an order item</exception>
    public OrderItem GetOrderItem(int IDO, int IDP)
    {
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (DataSource.lOrderItem[i].OrderID == IDO && DataSource.lOrderItem[i].ProductID == IDP)
                return DataSource.lOrderItem[i];
        }
        throw new InvalidID("there in no such an order item");
    }
    /// <summary>
    /// returns orderItems that belongs to orderId
    /// </summary>
    /// <param name="IDO">recived order id</param>
    /// <returns>an array of orderItems</returns>
    public IEnumerable<OrderItem> GetOrderItemsOfOrder(int IDO)
    {
        List<OrderItem> lorderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.lOrderItem.Count; i++)
        {
            if (DataSource.lOrderItem[i].OrderID == IDO)
                lorderItems.Add(DataSource.lOrderItem[i]);
        }
        return lorderItems;
    }
}