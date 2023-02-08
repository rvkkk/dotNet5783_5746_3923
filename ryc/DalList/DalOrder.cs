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
        if (DataSource.lProduct.RemoveAll(p => p?.ID == ID) == 0)
            throw new InvalidID("there in no such an id");
    }

    /// <summary>
    /// updates order
    /// </summary>
    /// <param name="o">recived order</param>
    public void Update(Order o)
    {
        DataSource.lOrder[DataSource.lOrder.FindIndex(order => order?.ID == o.ID)] = o;
    }

    /// <summary>
    /// return order by id
    /// </summary>
    /// <param name="ID">recived order id</param>
    /// <returns>order</returns>
    /// <exception cref="Exception">there in no such an order id</exception>
    public Order Get(int ID)
    {
        return DataSource.lOrder.FirstOrDefault(p => p?.ID == ID) ??
           throw new InvalidID("there in no such an id");
    }

    /// <summary>
    /// returns order by a function
    /// </summary>
    /// <param name="func">a delegate</param>
    /// <returns>order</returns>
    /// <exception cref="InvalidID">there in no such an order</exception>
    public Order GetByF(Func<Order?, bool>? func)
    {
        return DataSource.lOrder.Where(func!).First() ??
         throw new InvalidID("there in no such an order");
    }

    /// <summary>
    /// returns all orders
    /// </summary>
    /// <returns>orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        if (func == null)
            return DataSource.lOrder.Select(p => p);
        else
            return DataSource.lOrder.Where(func);
    }
}