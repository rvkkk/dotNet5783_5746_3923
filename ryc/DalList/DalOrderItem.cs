using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// adds a new orderItem
    /// </summary>
    /// <param name="o">recived orderItem</param>
    /// <returns>the orderItem id</returns>
    public int Add(OrderItem o)
    {
        o.ID = DataSource.Config.OrderItemID;
        DataSource.orderItemsArray[DataSource.Config.lastOrderItem] = o;
        DataSource.Config.lastOrderItem++;
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
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].ID == ID)
            {
                DataSource.Config.lastOrderItem--;
                flag = true;
            }
            if (flag)
                DataSource.orderItemsArray[i] = DataSource.orderItemsArray[i + 1];
        }
        if (!flag)
            throw new Exception("there in no such an id");
    }
    /// <summary>
    /// updates orderItem
    /// </summary>
    /// <param name="o">recived orderItem</param>
    public void Update(OrderItem o)
    {
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].ID == o.ID)
                DataSource.orderItemsArray[i] = o;
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
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].ID == ID)
                return DataSource.orderItemsArray[i];
        }
        throw new Exception("there in no such an id");
    }
    /// <summary>
    /// returns all orderItems
    /// </summary>
    /// <returns>orderItems</returns>
    public OrderItem[] GetAll()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config.lastOrderItem];
        for (int i = 0; i < orderItems.Length; i++)
        {
            orderItems[i] = DataSource.orderItemsArray[i];
        }
        return orderItems;
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
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].OrderID == IDO && DataSource.orderItemsArray[i].ProductID == IDP)
                return DataSource.orderItemsArray[i];
        }
        throw new Exception("there in no such an order item");
    }
    /// <summary>
    /// returns orderItems that belongs to orderId
    /// </summary>
    /// <param name="IDO">recived order id</param>
    /// <returns>an array of orderItems</returns>
    public OrderItem[] GetOrderItemsOfOrder(int IDO)
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config.lastOrderItem];
        int c = 0;
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].OrderID == IDO)
                orderItems[c++] = DataSource.orderItemsArray[i];
        }
        OrderItem[] items = new OrderItem[c];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = orderItems[i];
        }
        return items;
    }
}