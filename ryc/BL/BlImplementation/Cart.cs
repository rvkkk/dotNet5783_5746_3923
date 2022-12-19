using BlApi;
using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        private DalApi.IDal Dal = new DalList();
        /// <summary>
        /// adds product to cart
        /// </summary>
        /// <param name="cart">recived cart</param>
        /// <param name="ID">recived product id</param>
        /// <returns>the updated cart</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Cart AddToCart(BO.Cart cart, int ID)
        {
            foreach (BO.OrderItem? item in cart.Items!)
            {
                if (item?.ProductID == ID)
                    throw new BO.AlreadyExists("the product already exists");
            }
            try
            {
                DO.Product product = Dal.Product.Get(ID);
                if (product.InStock == 0)
                    throw new LessAmount("there is much amount of the product in shop");
                cart.Items.Add(new BO.OrderItem() { ID = cart.Items.Count + 1, ProductID = ID, ProductName = product.Name, ProductPrice = product.Price, Amount = 1, TotalPrice = product.Price });
                cart.TotalPrice += product.Price;
                return cart;
            }
            catch (Exception ex) { throw new DalException("error in getting a product", ex); }
        }
        /// <summary>
        /// updates amount of product in cart
        /// </summary>
        /// <param name="cart">recived cart</param>
        /// <param name="ID">recived product id</param>
        /// <param name="amount">recived amount</param>
        /// <returns>the updated cart</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Cart UpdateCart(BO.Cart cart, int ID, int amount)
        {
            BO.OrderItem? item = cart.Items?.FirstOrDefault(x => x!.ProductID == ID);
            if (item == null)
                throw new InvalidID("there is no such an id in the cart");
            try
            {
                if (amount == 0)
                {
                    cart.TotalPrice -= (double)cart.Items?.FirstOrDefault(x => x!.ProductID == ID)!.TotalPrice!;
                    cart.Items.Remove(item);
                }
                else
                {
                    cart.Items?.Remove(item);
                    DO.Product product = Dal.Product.Get(ID);
                    item.Amount = amount;
                    item.TotalPrice = amount * item.ProductPrice;
                    cart.TotalPrice += product.Price * amount;
                    cart.Items?.Add(item);
                }
                return cart;
            }
            catch (Exception ex) { throw new DalException("error in getting a product", ex); }
        }
        /// <summary>
        /// creates an order according to the cart
        /// </summary>
        /// <param name="cart">recived cart</param>
        /// <exception cref="BO.InvalidInput">recived invalid input</exception>
        /// <exception cref="DalException">exception from dal</exception>
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.CustomerName == "")
                throw new BO.InvalidInput("the customer name is empty");
            if (cart.CustomerEmail == "")
                throw new BO.InvalidInput("the customer email is empty");
            if (cart.CustomerAddress == "")
                throw new BO.InvalidInput("the customer address is empty");
            foreach (BO.OrderItem? orderItem in cart.Items!)
            {
                try
                {
                    DO.Product product = Dal.Product.Get((int)orderItem?.ProductID!);
                    if (orderItem.Amount <= 0)
                        throw new LessAmount("there is invalid amount of product");
                    if (product.InStock - orderItem.Amount < 0)
                        throw new LessAmount("there is much amount of the product in shop");
                }
                catch (Exception ex) { throw new DalException("error in getting a product", ex); }
            }
            DO.Order order = new DO.Order() { CustomerName = cart.CustomerName, CustomerEmail = cart.CustomerEmail, CustomerAddress = cart.CustomerEmail, OrderDate = DateTime.Now, ShipDate = DateTime.MinValue, DeliveryDate = DateTime.MinValue };
            try
            {
                int ID = Dal.Order.Add(order);
                foreach (BO.OrderItem? orderItem in cart.Items)
                {
                    try
                    {
                        Dal.OrderItem.Add(new DO.OrderItem() { OrderID = ID, ProductID = (int)orderItem?.ProductID!, Amount = orderItem.Amount, Price = orderItem.ProductPrice });
                    }
                    catch (Exception ex) { throw new DalException("error in adding a product to order", ex); }
                    try
                    {
                        DO.Product product = Dal.Product.Get(orderItem.ProductID);
                        product.InStock -= orderItem.Amount;
                        Dal.Product.Update(product);
                    }
                    catch (Exception ex) { throw new DalException("error in getting and updating a product", ex); }
                }
            }
            catch (Exception ex) { throw new DalException("error in adding an order", ex); }
        }
    }
}