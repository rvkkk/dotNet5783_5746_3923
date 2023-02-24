using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class OrderItem : IOrderItem
    {
        const string xmlOrderItemL = @"OrderItem";

        /// <summary>
        /// adds a new orderItem
        /// </summary>
        /// <param name="o">recived orderItem</param>
        /// <returns>the orderItem id</returns>
        public int Add(DO.OrderItem o)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(xmlOrderItemL);

            o.ID = (int)((orderItemsList.Last()?.ID) + 1)!;
            orderItemsList.Add(o);
            XMLTools.SaveListToXMLSerializer(orderItemsList, xmlOrderItemL);
            return o.ID;
        }

        /// <summary>
        /// deletes orderItem
        /// </summary>
        /// <param name="ID">recived orderItem id</param>
        /// <exception cref="Exception">there in no such an orderItem id</exception>
        public void Delete(int ID)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(xmlOrderItemL);

            if (orderItemsList.RemoveAll(ordItem => ordItem?.ID == ID) == 0)
                throw new InvalidID("there in no such an order item id");

            XMLTools.SaveListToXMLSerializer(orderItemsList, xmlOrderItemL);
        }

        /// <summary>
        /// updates orderItem
        /// </summary>
        /// <param name="o">recived orderItem</param>
        public void Update(DO.OrderItem o)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(xmlOrderItemL);

            orderItemsList.RemoveAll(orderI => orderI?.ID == o.ID);
            orderItemsList.Add(o);
            XMLTools.SaveListToXMLSerializer(orderItemsList, xmlOrderItemL);
        }

        /// <summary>
        /// return orderItem by id
        /// </summary>
        /// <param name="ID">recived orderItem id</param>
        /// <returns>orderItem</returns>
        /// <exception cref="Exception">there in no such an orderItem id</exception>
        public DO.OrderItem Get(int ID)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(xmlOrderItemL);
            return orderItemsList.FirstOrDefault(o => o?.ID == ID) ?? throw new InvalidID("there in no such an order item");
        }

        /// <summary>
        /// returns orderItem by a function
        /// </summary>
        /// <param name="func">a delegate</param>
        /// <returns>orderItem</returns>
        /// <exception cref="InvalidID">there in no such an orderItem</exception>
        public DO.OrderItem GetByF(Func<DO.OrderItem?, bool> func)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(xmlOrderItemL);
            return orderItemsList.FirstOrDefault(func) ?? throw new InvalidID("there in no such an order item");
        }

        /// <summary>
        /// returns all orderItems
        /// </summary>
        /// <returns>orderItems</returns>
        public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
        {
            List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(xmlOrderItemL);

            if (func == null)
                return orderItemsList.Select(o => o);
            else
                return orderItemsList.Where(func);
        }
    }
}
