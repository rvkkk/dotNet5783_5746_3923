using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class Order : IOrder
    {
        const string xmlOrderL = @"Order";

        /// <summary>
        /// adds a new order
        /// </summary>
        /// <param name="o">recived order</param>
        /// <returns>the order id</returns>
        public int Add(DO.Order o)
        {
            List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(xmlOrderL);

            o.ID = (int)((listOrders.Last()?.ID) + 1)!;
            listOrders.Add(o);
            XMLTools.SaveListToXMLSerializer(listOrders, xmlOrderL);
            return o.ID;
        }

        /// <summary>
        /// deletes order
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <exception cref="Exception">there in no such an order id</exception>
        public void Delete(int ID)
        {
            List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(xmlOrderL);

            if (listOrders.RemoveAll(order => order?.ID == ID) == 0)
                throw new InvalidID("there in no such an id");
            XMLTools.SaveListToXMLSerializer(listOrders, xmlOrderL);
        }

        /// <summary>
        /// updates order
        /// </summary>
        /// <param name="o">recived order</param>
        public void Update(DO.Order o)
        {
            List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(xmlOrderL);

            listOrders.RemoveAll(order => order?.ID == o.ID);
            listOrders.Add(o);
            XMLTools.SaveListToXMLSerializer(listOrders, xmlOrderL);
        }

        /// <summary>
        /// return order by id
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>order</returns>
        /// <exception cref="Exception">there in no such an order id</exception>
        public DO.Order Get(int ID)
        {
            List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(xmlOrderL);
            return listOrders.FirstOrDefault(o => o?.ID == ID) ?? throw new InvalidID("there in no such an order");
        }

        /// <summary>
        /// returns order by a function
        /// </summary>
        /// <param name="func">a delegate</param>
        /// <returns>order</returns>
        /// <exception cref="InvalidID">there in no such an order</exception>
        public DO.Order GetByF(Func<DO.Order?, bool> func)
        {
            List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(xmlOrderL);
            return listOrders.FirstOrDefault(func) ?? throw new InvalidID("there in no such an order");
        }

        /// <summary>
        /// returns all orders
        /// </summary>
        /// <returns>orders</returns>
        public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
        {
            List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(xmlOrderL);
            if (func == null)
                return listOrders.Select(o => o);
            else
                return listOrders.Where(func);
        }
    }
}