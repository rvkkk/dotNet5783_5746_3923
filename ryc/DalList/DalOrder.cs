using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// adds a new order
    /// </summary>
    /// <param name="o">recived order</param>
    /// <returns>the order id</returns>
    public int Add(Order o)
    {
        o.ID = DataSource.Config.OrderID;
        DataSource.ordersArray[DataSource.Config.lastOrder] = o;
        DataSource.Config.lastOrder++;
        return o.ID;
    }
    /// <summary>
    /// deletes order
    /// </summary>
    /// <param name="ID">recived order id</param>
    /// <exception cref="Exception">there in no such an order id</exception>
    public void Delete(int ID)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.lastOrder; i++)
        {
            if (DataSource.ordersArray[i].ID == ID)
            {
                DataSource.Config.lastOrder--;
                flag = true;
            }                   
            if(flag)
                DataSource.ordersArray[i] = DataSource.ordersArray[i+1];
        }
        if (!flag)
            throw new Exception("there in no such an id");
    }
    /// <summary>
    /// updates order
    /// </summary>
    /// <param name="o">recived order</param>
    public void Update(Order o)
    {
        for (int i = 0; i < DataSource.Config.lastOrder; i++)
        {
            if (DataSource.ordersArray[i].ID == o.ID)
                DataSource.ordersArray[i] = o;
        }
    }
    /// <summary>
    /// return order by id
    /// </summary>
    /// <param name="ID">recived order id</param>
    /// <returns>order</returns>
    /// <exception cref="Exception">there in no such an order id</exception>
    public Order Get(int ID)
    {
        for(int i=0; i < DataSource.Config.lastOrder; i++)
        {
            if (DataSource.ordersArray[i].ID == ID)
                return DataSource.ordersArray[i];
        }
        throw new Exception("there in no such an id");
    }
    /// <summary>
    /// returns all orders
    /// </summary>
    /// <returns>orders</returns>
    public Order[] GetAll()
    {
        Order[] orders = new Order[DataSource.Config.lastOrder];
        for (int i = 0; i < orders.Length; i++)
        {
            orders[i] = DataSource.ordersArray[i];
        }
        return orders;
    }
}