using BlApi;
using BO;
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
        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// the function returns the status of the order
        /// </summary>
        /// <param name="order">a recived order</param>
        /// <returns>the order status</returns>
        private BO.Enums.OrderStatus OrderStatus(DO.Order? order)
        {
            BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
            if (order?.DeliveryDate != null)
                oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
            else if (order?.ShipDate != null)
                oStatus = BO.Enums.OrderStatus.Shipped;
            return oStatus;
        }

        /// <summary>
        /// returns all the exist orders
        /// </summary>
        /// <returns>list of the orders</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public IEnumerable<OrderForList?> GetAll(Func<BO.OrderForList?, bool>? func = null)
        {
            IEnumerable<BO.OrderForList?> lOrders;
            try
            {
                lOrders = from DO.Order? order in dal!.Order.GetAll()
                          select new BO.OrderForList()
                          {
                              ID = (int)order?.ID!,
                              CustomerName = order?.CustomerName ?? "",
                              Status = OrderStatus(order),
                              AmountOfItems = dal.OrderItem.GetAll(x => x?.OrderID == order?.ID).Count(x => x?.Amount > 0),
                              TotalPrice = dal.OrderItem.GetAll(x => x?.OrderID == order?.ID).Sum(x => (x?.Amount * x?.Price) ?? 0)
                          };
                return func is null ? lOrders : lOrders.Where(func);
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting items in order", ex); }
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
            DO.Order oD;
            try
            {
                oD = (DO.Order)dal?.Order.Get(ID)!;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an order", ex); }
            IEnumerable<BO.OrderItem?> lItems;
            try
            {
                lItems = from orderItem in dal!.OrderItem.GetAll(orderItem => orderItem?.OrderID == ID)
                         select new BO.OrderItem
                         {
                             ID = (int)orderItem?.ID!,
                             ProductID = orderItem?.ProductID ?? 0,
                             ProductName = dal!.Product.Get((int)orderItem?.ProductID!).Name,
                             ProductPrice = orderItem?.Price ?? 0,
                             Amount = orderItem?.Amount ?? 0,
                             TotalPrice = orderItem?.Price * orderItem?.Amount ?? 0
                         };
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
            BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = OrderStatus(oD), OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = oD.DeliveryDate, Items = lItems, TotalPrice = lItems.Sum(x => x?.ProductPrice * x?.Amount) ?? 0 };
            return oB;
        }

        /// <summary>
        /// updates the ship date
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>the updated order</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Order ShipUpdate(int ID)
        {
            DO.Order oD;
            try
            {
                oD = (DO.Order)dal?.Order.Get(ID)!;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an order", ex); }
            if (oD.ShipDate != null)
                throw new BO.AlreadyDone("the order alredy shipped");
            oD.ShipDate = DateTime.Now;
            try
            {
                dal?.Order.Update(oD);
            }
            catch (Exception ex) { throw new DalException("error in updating an order", ex); }
            IEnumerable<BO.OrderItem?> lItems;
            try
            {
                lItems = from orderItem in dal!.OrderItem.GetAll(orderItem => orderItem?.OrderID == ID)
                         select new BO.OrderItem
                         {
                             ID = (int)orderItem?.ID!,
                             ProductID = orderItem?.ProductID ?? 0,
                             ProductName = dal!.Product.Get((int)orderItem?.ProductID!).Name,
                             ProductPrice = orderItem?.Price ?? 0,
                             Amount = orderItem?.Amount ?? 0,
                             TotalPrice = orderItem?.Price * orderItem?.Amount ?? 0
                         };
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
            BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.Shipped, OrderDate = oD.OrderDate, ShipDate = DateTime.Now, DeliveryDate = oD.DeliveryDate, Items = lItems, TotalPrice = lItems.Sum(x => x?.ProductPrice * x?.Amount) ?? 0 };
            return oB;
        }

        /// <summary>
        /// updates the delivery date
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>the updated order</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Order DeliveryUpdate(int ID)
        {
            DO.Order oD;
            try
            {
                oD = (DO.Order)dal?.Order.Get(ID)!;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an order", ex); }
            if (oD.ShipDate == null)
                throw new Exception("Delivery date can not be updated before shipping date");
            if (oD.DeliveryDate != null)
                throw new BO.AlreadyDone("the order alredy delivered");
            oD.DeliveryDate = DateTime.Now;
            try
            {
                dal.Order.Update(oD);
            }
            catch (Exception ex) { throw new DalException("error in updating an order", ex); }
            IEnumerable<BO.OrderItem?> lItems;
            try
            {
                lItems = from orderItem in dal!.OrderItem.GetAll(orderItem => orderItem?.OrderID == ID)
                         select new BO.OrderItem
                         {
                             ID = (int)orderItem?.ID!,
                             ProductID = orderItem?.ProductID ?? 0,
                             ProductName = dal!.Product.Get((int)orderItem?.ProductID!).Name,
                             ProductPrice = orderItem?.Price ?? 0,
                             Amount = orderItem?.Amount ?? 0,
                             TotalPrice = orderItem?.Price * orderItem?.Amount ?? 0
                         };
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
            BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.DeliveredToCustomer, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = DateTime.Now, Items = lItems, TotalPrice = lItems.Sum(x => x?.ProductPrice * x?.Amount) ?? 0 };
            return oB;
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
            DO.Order oD;
            try
            {
                oD = (DO.Order)dal?.Order.Get(ID)!;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an order", ex); }
            if (oD.ShipDate != null)
                throw new BO.AlreadyDone("you can not change the amount because the order alredy shipped");
            DO.OrderItem item;
            try
            {
                item = dal.OrderItem.GetByF((DO.OrderItem? orderItem) => { return orderItem?.OrderID == ID && orderItem?.ProductID == product; });
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an item in order", ex); }
            item.Amount = amount;
            try
            {
                dal.OrderItem.Update(item);
            }
            catch (Exception ex) { throw new DalException("error in updating an item", ex); }
            IEnumerable<BO.OrderItem?> lItems;
            try
            {
                lItems = from orderItem in dal!.OrderItem.GetAll(orderItem => orderItem?.OrderID == ID)
                         select new BO.OrderItem
                         {
                             ID = (int)orderItem?.ID!,
                             ProductID = orderItem?.ProductID ?? 0,
                             ProductName = dal!.Product.Get((int)orderItem?.ProductID!).Name,
                             ProductPrice = orderItem?.Price ?? 0,
                             Amount = orderItem?.Amount ?? 0,
                             TotalPrice = orderItem?.Price * orderItem?.Amount ?? 0
                         };
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
            BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.OrderConfirmed, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = oD.DeliveryDate, Items = lItems, TotalPrice = lItems.Sum(x => x?.ProductPrice * x?.Amount) ?? 0 };
            return oB;
        }

        /// <summary>
        /// returns list of the order tracking
        /// </summary>
        /// <param name="ID">recived order id</param>
        /// <returns>order tracking</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public OrderTracking OrderTracking(int ID)
        {
            DO.Order oD;
            try
            {
                oD = (DO.Order)dal?.Order.Get(ID)!;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an order", ex); }
            List<Tuple<DateTime?, string?>?> lOrderT = new List<Tuple<DateTime?, string?>?>();
            BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
            lOrderT.Add(new Tuple<DateTime?, string?>(oD.OrderDate, "The order has been created"));
            if (oD.ShipDate != null)
            {
                lOrderT.Add(new Tuple<DateTime?, string?>(oD.ShipDate, "The order has been sent"));
                oStatus = BO.Enums.OrderStatus.Shipped;
            }
            if (oD.DeliveryDate != null)
            {
                lOrderT.Add(new Tuple<DateTime?, string?>(oD.DeliveryDate, "The order has been delivered"));
                oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = ID, Status = oStatus, list = lOrderT };
            return orderTracking;
        }
    }
}