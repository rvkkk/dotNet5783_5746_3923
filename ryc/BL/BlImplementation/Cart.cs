using BlApi;
using BO;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// adds product to cart
        /// </summary>
        /// <param name="cart">recived cart</param>
        /// <param name="ID">recived product id</param>
        /// <returns>the updated cart</returns>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Cart AddToCart(BO.Cart cart, int ID)
        {
            if (ID <= 0)
                throw new BO.InvalidID("there in no such an id");
            if (cart.Items?.FirstOrDefault(p => p?.ProductID == ID) != null)
                throw new BO.AlreadyExists("the product already exists in the cart");
            try
            {
                DO.Product product = dal!.Product.Get(ID);
                if (product.InStock == 0)
                    throw new LessAmount("there is no much amount of product in shop");
                cart.Items?.Add(new BO.OrderItem() { ID = cart.Items.Count + 1, ProductID = ID, ProductName = product.Name, ProductPrice = product.Price, Amount = 1, TotalPrice = product.Price });
                cart.TotalPrice += product.Price;
                return cart;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
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
            if (ID <= 0)
                throw new BO.InvalidID("there in no such an id");
            if (cart.Items?.FirstOrDefault(p => p?.ProductID == ID) == null)
                throw new BO.InvalidID("the product is not exist in the cart yet");
            BO.OrderItem? item = cart.Items?.FirstOrDefault(p => p!.ProductID == ID);
            try
            {
                if (amount == 0)
                {
                    cart.TotalPrice -= (double)cart.Items?.FirstOrDefault(x => x!.ProductID == ID)!.TotalPrice!;
                    cart.Items.Remove(item);
                }
                else if (amount > dal?.Product.Get(ID).InStock)
                    throw new LessAmount("there is much amount of the product in shop");
                else
                {
                    cart.Items?.Remove(item);
                    item!.Amount = amount;
                    item.TotalPrice = amount * item.ProductPrice;
                    cart.Items?.Add(item);
                    cart.TotalPrice = (double)cart.Items!.Sum(x => x?.TotalPrice ?? 0);
                }
                return cart;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
        }

        /// <summary>
        /// creates an order according to the cart
        /// </summary>
        /// <param name="cart">recived cart</param>
        /// <exception cref="BO.InvalidInput">recived invalid input</exception>
        /// <exception cref="DalException">exception from dal</exception>
        public int MakeAnOrder(BO.Cart cart)
        {
            if (cart.CustomerName == "")
                throw new BO.InvalidInput("the customer name is empty");
            if (cart.CustomerEmail == "")
                throw new BO.InvalidInput("the customer email is empty");
            if (cart.CustomerAddress == "")
                throw new BO.InvalidInput("the customer address is empty");
            DO.Order order = new DO.Order() { CustomerName = cart.CustomerName, CustomerEmail = cart.CustomerEmail, CustomerAddress = cart.CustomerEmail, OrderDate = DateTime.Now, ShipDate = null, DeliveryDate = null };
            int ID;
            try
            {
                ID = (int)dal?.Order.Add(order)!;
            }
            catch (Exception ex) { throw new DalException("error in adding an order", ex); }
            try
            {
                var orderItems = cart.Items?.FindAll(p => dal?.Product.Get((int)p?.ProductID!).InStock - p?.Amount >= 0 ? true : throw new LessAmount("there is no much amount of the product in shop"));
                orderItems?.ForEach(item =>
                {
                    DO.Product product = dal.Product.Get(item!.ProductID);
                    product.InStock -= item.Amount;
                    dal.Product.Update(product);
                    dal.OrderItem.Add(new DO.OrderItem() { OrderID = ID, ProductID = (int)item?.ProductID!, Amount = item.Amount, Price = item.ProductPrice });
                });
                return ID;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product", ex); }
        }

        public bool UpdateProduct(DO.Product p)
        {
            try
            {
                dal?.Product.Update(p);
                return true;
            }
            catch (DO.AlreadyExists ex) { throw new DalException("error in updating a product", ex); }
            catch (DO.InvalidID ex) { throw new DalException("error in updating a product", ex); }
        }
    }
}