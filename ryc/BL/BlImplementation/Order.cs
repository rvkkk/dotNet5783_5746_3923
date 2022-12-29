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
        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// returns all the exist orders
        /// </summary>
        /// <returns>list of the orders</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public IEnumerable<OrderForList?> GetAll()
        {
            List<BO.OrderForList?> lOrders = new List<BO.OrderForList?>();
            foreach (DO.Order? order in dal?.Order.GetAll()!)
            {
                int amount = 0;
                double price = 0;
                try
                {
                    foreach (DO.OrderItem? orderItem in dal?.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem?.OrderID == order?.ID)!)
                    {
                        amount++;
                        price += orderItem?.Price * orderItem?.Amount ?? 0;
                    }
                    BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
                    if (order?.DeliveryDate != null)
                        oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
                    else if (order?.ShipDate != null)
                        oStatus = BO.Enums.OrderStatus.Shipped;
                    lOrders.Add(new BO.OrderForList() { ID = (int)order?.ID!, CustomerName = order?.CustomerName, Status = oStatus, AmountOfItems = amount, TotalPrice = price });
                }
                catch (DO.InvalidID ex) { throw new DalException("error in getting items in order", ex); }
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
            DO.Order oD;
            try
            {
                oD = (DO.Order)dal?.Order.Get(ID)!;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an order", ex); }
            double price = 0;
            List<BO.OrderItem> lItems = new List<BO.OrderItem>();
            try
            {
                foreach (DO.OrderItem? orderItem in dal?.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem?.OrderID == ID)!)
                {
                    try
                    {
                        DO.Product p = (DO.Product)dal?.Product.Get((int)orderItem?.ProductID!)!;
                        lItems.Add(new BO.OrderItem() { ID = (int)orderItem?.ID!, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = (int)orderItem?.Amount!, TotalPrice = p.Price * (int)orderItem?.Amount! });
                    }
                    catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
                    price += orderItem?.Price ?? 0;
                }
                BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
                if (oD.DeliveryDate != DateTime.MinValue)
                    oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
                else if (oD.ShipDate != DateTime.MinValue)
                    oStatus = BO.Enums.OrderStatus.Shipped;
                BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = oStatus, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = oD.DeliveryDate, Items = lItems!, TotalPrice = price };
                return oB;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting items in order", ex); }
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
            catch (DO.InvalidID ex) { throw new DalException("error in getting items in order", ex); }
            if (oD.ShipDate != null)
                throw new BO.AlreadyDone("the order alredy shiped");
            oD.ShipDate = DateTime.Now;
            try
            {
                dal?.Order.Update(oD);
            }
            catch (Exception ex) { throw new DalException("error in updating an order", ex); }
            double price = 0;
            List<BO.OrderItem> lItems = new List<BO.OrderItem>();
            try
            {
                foreach (DO.OrderItem? orderItem in dal?.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem?.OrderID == ID)!)
                {
                    try
                    {
                        DO.Product p = dal.Product.Get((int)orderItem?.ProductID!);
                        lItems.Add(new BO.OrderItem() { ID = (int)orderItem?.ID!, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = (int)orderItem?.Amount!, TotalPrice = p.Price * (int)orderItem?.Amount! });
                        price += orderItem?.Price ?? 0;
                    }
                    catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
                }
                BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.Shipped, OrderDate = oD.OrderDate, ShipDate = DateTime.Now, DeliveryDate = oD.DeliveryDate, Items = lItems!, TotalPrice = price };
                return oB;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting items in order", ex); }
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
            catch (DO.InvalidID ex) { throw new DalException("error in updating an order", ex); }
            if (oD.ShipDate == null || oD.DeliveryDate != null)
                throw new BO.AlreadyDone("the order alredy delivered");
            oD.DeliveryDate = DateTime.Now;
            try
            {
                dal.Order.Update(oD);
            }
            catch (Exception ex) { throw new DalException("error in updating an order", ex); }
            double price = 0;
            List<BO.OrderItem> lItems = new List<BO.OrderItem>();
            try
            {
                foreach (DO.OrderItem? orderItem in dal.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem?.OrderID == ID))
                {
                    try
                    {
                        DO.Product p = dal.Product.Get((int)orderItem?.ProductID!);
                        lItems.Add(new BO.OrderItem() { ID = (int)orderItem?.ID!, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = (int)orderItem?.Amount!, TotalPrice = p.Price * (int)orderItem?.Amount! });
                        price += orderItem?.Price ?? 0;
                    }
                    catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
                }
                BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.DeliveredToCustomer, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = DateTime.Now, Items = lItems!, TotalPrice = price };
                return oB;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting items in order", ex); }
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
            catch (DO.InvalidID ex) { throw new DalException("error in updating an order", ex); }
            if (oD.ShipDate != null)
                throw new BO.AlreadyDone("the order alredy shiped");
            DO.OrderItem item;
            try
            {
                item = dal.OrderItem.GetByF((DO.OrderItem? orderItem) => { return orderItem?.OrderID == ID && orderItem?.ProductID == product; });
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting an item in order", ex); }
            item.Amount = amount;
            try
            {
                dal.Order.Update(oD);
            }
            catch (Exception ex) { throw new DalException("error in updating an order", ex); }
            double price = 0;
            List<BO.OrderItem> lItems = new List<BO.OrderItem>();
            try
            {
                foreach (DO.OrderItem? orderItem in dal.OrderItem.GetAll((DO.OrderItem? orderItem) => orderItem?.OrderID == ID))
                {
                    try
                    {
                        DO.Product p = dal.Product.Get((int)orderItem?.ProductID!);
                        lItems.Add(new BO.OrderItem() { ID = (int)orderItem?.ID!, ProductID = p.ID, ProductName = p.Name, ProductPrice = p.Price, Amount = (int)orderItem?.Amount!, TotalPrice = p.Price * (int)orderItem?.Amount! });
                        price += orderItem?.Price ?? 0;
                    }
                    catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
                }
                BO.Order oB = new BO.Order() { ID = oD.ID, CustomerName = oD.CustomerName, CustomerAddress = oD.CustomerAddress, CustomerEmail = oD.CustomerEmail, Status = BO.Enums.OrderStatus.OrderConfirmed, OrderDate = oD.OrderDate, ShipDate = oD.ShipDate, DeliveryDate = oD.DeliveryDate, Items = lItems!, TotalPrice = price };
                return oB;
            }
            catch (Exception ex) { throw new DalException("error in getting items in order", ex); }
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
            catch (DO.InvalidID ex) { throw new DalException("error in updating an order", ex); }
            List<Tuple<DateTime?, string?>?> lOrderT = new List<Tuple<DateTime?, string?>?>();
            BO.Enums.OrderStatus oStatus = BO.Enums.OrderStatus.OrderConfirmed;
            lOrderT.Add(new Tuple<DateTime?, string?>(oD.OrderDate, "The order has been created"));
            if (oD.ShipDate != DateTime.MinValue)
            {
                lOrderT.Add(new Tuple<DateTime?, string?>(oD.ShipDate, "The order has been sent"));
                oStatus = BO.Enums.OrderStatus.Shipped;
            }
            if (oD.DeliveryDate != DateTime.MinValue)
            {
                lOrderT.Add(new Tuple<DateTime?, string?>(oD.DeliveryDate, "The order has been delivered"));
                oStatus = BO.Enums.OrderStatus.DeliveredToCustomer;
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = ID, Status = oStatus, list = lOrderT };
            return orderTracking;
        }
    }
}