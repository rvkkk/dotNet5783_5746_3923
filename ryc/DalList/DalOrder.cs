using DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// adds a new order
    /// </summary>
    /// <param name="o">recived order</param>
    /// <returns>the order id</returns>
    public int Add(Order o)
    {
        o.ID = DataSource.Config.OrderID;
        DataSource.lOrder.Add(o);
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
        for (int i = 0; i < DataSource.lOrder.Count; i++)
        {
            if (DataSource.lOrder[i].ID == ID)
            {
                DataSource.lOrder.RemoveAt(i);  
                flag = true;  
            }                       
        }
        if (!flag)
            throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// updates order
    /// </summary>
    /// <param name="o">recived order</param>
    public void Update(Order o)
    {
        for (int i = 0; i < DataSource.lOrder.Count; i++)
        {
            if (DataSource.lOrder[i].ID == o.ID)
                DataSource.lOrder[i] = o;
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
        for(int i=0; i < DataSource.lOrder.Count; i++)
        {
            if (DataSource.lOrder[i].ID == ID)
                return DataSource.lOrder[i];
        }
        throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// returns all orders
    /// </summary>
    /// <returns>orders</returns>
    public IEnumerable<Order> GetAll()
    {
        List<Order> lorders = new List<Order>();
        for (int i = 0; i < DataSource.lOrder.Count; i++)
        {
            lorders.Add( DataSource.lOrder[i] );
        }
        return lorders;
    }
}