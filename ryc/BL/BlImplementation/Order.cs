using BlApi;
using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Order : IOrder
    {
        private DalApi.IDal Dal = new DalList();
        /// <summary>
        /// returns all the exist orders
        /// </summary>
        /// <returns>list of the orders</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public IEnumerable<OrderForList> GetAll()
        {
            List<BO.OrderForList> lOrders = new List<BO.OrderForList>();
            foreach (DO.Order order in Dal.Order.GetAll())
            {
                int amount = 0;
                double price = 0;
                try
                {
                    foreach (DO.OrderItem orderItem in Dal.OrderItem.GetOrderItemsOfOrder(order.ID))
                    {
                        amount++;
                        price += orderItem.Price * orderItem.Amount;
                    }
                    BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
                    if (order.DeliveryDate != DateTime.MinValue)
                        oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
                    else if (order.ShipDate != DateTime.MinValue)
                        oStatus = BO.Enums.OrderStatus.Shipped;
                    lOrders.Add(new BO.OrderForList() { ID = order.ID, CustomerName = order.CustomerName, Status = oStatus, AmountOfItems = amount, TotalPrice = price });
                }
                catch (Exception ex) { throw new DalException("error in getting items in order", ex); }
            }
            return lOrders;
        }
        /// <summary>
        /// returns order
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>order</returns>
        /// <exception cref="BO.InvalidID">recived invalid id</exception>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Order Get(int ID)
        {
            if (ID <= 0)
                throw new BO.InvalidID("there in no such an id");
            try
            {
                DO.Order oD = Dal.Order.Get(ID);
                double price = 0;
                List<BO.OrderItem> lItems = new List<BO.OrderItem>();
                try
                {
                    foreach (DO.OrderItem orderItem in Dal.OrderItem.GetOrderItemsOfOrder(ID))
                    {
                        try
                        {
                            DO.Product p = Dal.Product.Get(orderItem.ProductID);
                            lItems.Add(new BO.OrderItem() { ID = orderItem.ID, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = orderItem.Amount, TotalPrice = p.Price * orderItem.Amount });
                        }
                        catch (Exception ex) { throw new DalException("error in getting a product", ex); }
                        price += orderItem.Price;
                    }
                    BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
                    if (oD.DeliveryDate != DateTime.MinValue)
                        oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
                    else if (oD.ShipDate != DateTime.MinValue)
                        oStatus = BO.Enums.OrderStatus.Shipped;
                    BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = oStatus, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = oD.DeliveryDate, Items = lItems, TotalPrice = price };
                    return oB;
                }
                catch (Exception ex) { throw new DalException("error in getting items in order", ex); }
            }
            catch (Exception ex) { throw new DalException("error in getting an order", ex); }
        }
        /// <summary>
        /// updates the ship date
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>the updated order</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Order ShipUpdate(int ID)
        {
            try
            {
                DO.Order oD = Dal.Order.Get(ID);
                if (oD.ShipDate != DateTime.MinValue)
                    throw new BO.AlreadyDone("the order alredy shiped");
                oD.ShipDate = DateTime.Now;
                try
                {
                    Dal.Order.Update(oD);
                }
                catch (Exception ex) { throw new DalException("error in updating an order", ex); }
                double price = 0;
                List<BO.OrderItem> lItems = new List<BO.OrderItem>();
                foreach (DO.OrderItem orderItem in Dal.OrderItem.GetOrderItemsOfOrder(ID))
                {
                    try
                    {
                        DO.Product p = Dal.Product.Get(orderItem.ProductID);
                        lItems.Add(new BO.OrderItem() { ID = orderItem.ID, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = orderItem.Amount, TotalPrice = p.Price * orderItem.Amount });
                        price += orderItem.Price;
                    }
                    catch (Exception ex) { throw new DalException("error in getting a product", ex); }
                }
                BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.Shipped, OrderDate = oD.OrderDate, ShipDate = DateTime.Now, DeliveryDate = oD.DeliveryDate, Items = lItems, TotalPrice = price };
                return oB;
            }
            catch (Exception ex) { throw new DalException("error in getting items in order", ex); }
        }
        /// <summary>
        /// updates the delivery date
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>the updated order</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Order DeliveryUpdate(int ID)
        {
            try
            {
                DO.Order oD = Dal.Order.Get(ID);
                if (oD.ShipDate == DateTime.MinValue || oD.DeliveryDate != DateTime.MinValue)
                    throw new BO.AlreadyDone("the order alredy delivered");
                oD.DeliveryDate = DateTime.Now;
                try
                {
                    Dal.Order.Update(oD);
                }
                catch (Exception ex) { throw new DalException("error in updating an order", ex); }
                double price = 0;
                List<BO.OrderItem> lItems = new List<BO.OrderItem>();
                foreach (DO.OrderItem orderItem in Dal.OrderItem.GetOrderItemsOfOrder(ID))
                {
                    try
                    {
                        DO.Product p = Dal.Product.Get(orderItem.ProductID);
                        lItems.Add(new BO.OrderItem() { ID = orderItem.ID, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = orderItem.Amount, TotalPrice = p.Price * orderItem.Amount });
                        price += orderItem.Price;
                    }
                    catch (Exception ex) { throw new DalException("error in getting a product", ex); }
                }
                BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.DeliveredToCustomer, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = DateTime.Now, Items = lItems, TotalPrice = price };
                return oB;
            }
            catch (Exception ex) { throw new DalException("error in getting items in order", ex); }
        }
        /// <summary>
        /// updates the amount of product in order
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <param name="product">recived product id</param>
        /// <param name="amount">recived amount</param>
        /// <returns>the updated order</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Order ManagerUpdate(int ID, int product, int amount)
        {
            try
            {
                DO.Order oD = Dal.Order.Get(ID);
                try
                {
                    if (oD.ShipDate != DateTime.MinValue)
                        throw new BO.AlreadyDone("the order alredy shiped");
                    DO.OrderItem item = Dal.OrderItem.GetOrderItemsOfOrder(ID).First(x => x.ProductID == product);
                    item.Amount = amount;
                    try
                    {
                        Dal.Order.Update(oD);
                    }
                    catch (Exception ex) { throw new DalException("error in updating an order", ex); }
                    double price = 0;
                    List<BO.OrderItem> lItems = new List<BO.OrderItem>();
                    foreach (DO.OrderItem orderItem in Dal.OrderItem.GetOrderItemsOfOrder(ID))
                    {
                        DO.Product p = Dal.Product.Get(orderItem.ProductID);
                        lItems.Add(new BO.OrderItem() { ID = orderItem.ID, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = orderItem.Amount, TotalPrice = p.Price * orderItem.Amount });
                        price += orderItem.Price;
                    }
                    BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.OrderConfirmed, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = oD.DeliveryDate, Items = lItems, TotalPrice = price };
                    return oB;
                }
                catch (Exception ex) { throw new DalException("error in getting items in order", ex); }
            }
            catch (Exception ex) { throw new DalException("error in getting an order", ex); }
        }
        /// <summary>
        /// returns list of the order tracking
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>order tracking</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public OrderTracking OrderTracking(int ID)
        {
            try
            {
                DO.Order oD = Dal.Order.Get(ID);
                List<Tuple<DateTime, string>> lOrderT = new List<Tuple<DateTime, string>>();
                BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
                lOrderT.Add(new Tuple<DateTime, string>(oD.OrderDate, "The order has been created"));
                if (oD.ShipDate != DateTime.MinValue)
                {
                    lOrderT.Add(new Tuple<DateTime, string>(oD.ShipDate, "The order has been sent"));
                    oStatus = BO.Enums.OrderStatus.Shipped;
                }
                if (oD.DeliveryDate != DateTime.MinValue)
                {
                    lOrderT.Add(new Tuple<DateTime, string>(oD.DeliveryDate, "The order has been delivered"));
                    oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
                }
                BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = ID, Status = oStatus, list = lOrderT };
                return orderTracking;
            }
            catch (Exception ex) { throw new DalException("error in getting an order", ex); }
        }
    }
}
