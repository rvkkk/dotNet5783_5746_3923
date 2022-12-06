using BlApi;
using BO;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Product : IProduct
    {
        private DalApi.IDal Dal = new DalList();
        /// <summary>
        /// returns all the exist products
        /// </summary>
        /// <returns>list of the products</returns>
        public IEnumerable<BO.ProductForList> GetAll()
        {
            List<BO.ProductForList> lproducts = new List<BO.ProductForList>();
            foreach (DO.Product product in Dal.Product.GetAll())
            {
                lproducts.Add(new BO.ProductForList() { ID = product.ID, Name = product.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), product.Category.ToString()), Price = product.Price });
            }
            return lproducts;
        }
        /// <summary>
        /// returns product
        /// </summary>
        /// <param name="ID">recived product id</param>
        /// <returns>product</returns>
        /// <exception cref="BO.InvalidID">recived invalid id</exception>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.Product Get(int ID)
        {
            if (ID <= 0)
                throw new BO.InvalidID("there in no such an id");
            try
            {
                DO.Product pD = Dal.Product.Get(ID);
                BO.Product pB = new BO.Product() { ID = pD.ID, Name = pD.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), pD.Category.ToString()), Price = pD.Price, InStock = pD.InStock };
                return pB;
            }
            catch(Exception ex) { throw new DalException("error in getting a product", ex); }
        }
        /// <summary>
        /// returns product in cart
        /// </summary>
        /// <param name="ID">recived product id</param>
        /// <param name="cart">recived cart</param>
        /// <returns>product item in cart</returns>
        /// <exception cref="BO.InvalidID">recived invalid id</exception>
        /// <exception cref="DalException">exception from dal</exception>
        public BO.ProductItem Get(int ID, BO.Cart cart)
        {
            if (ID <= 0)
                throw new BO.InvalidID("there in no such an id");
            try
            {
                DO.Product pD = Dal.Product.Get(ID);
                BO.ProductItem pIB = new BO.ProductItem() { ID = pD.ID, Name = pD.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), pD.Category.ToString()), Price = pD.Price, Amount = cart.Items.First(x => x.ID == ID).Amount, InStock = pD.InStock != 0 };
                return pIB;
            }
            catch (Exception ex) { throw new DalException("error in getting a product in cart", ex); }
        }
        /// <summary>
        /// adds product
        /// </summary>
        /// <param name="p">recived product</param>
        /// <exception cref="BO.InvalidID">recived invalid id</exception>
        /// <exception cref="BO.InvalidInput">recived invalid input</exception>
        /// <exception cref="DalException">exception from dal</exception>
        public void Add(BO.Product p)
        {
            if (p.ID <= 0) 
                throw new BO.InvalidID("there in no such an id");
            if (p.Name == "")
                throw new BO.InvalidInput("the name is empty");
            if (p.Price <= 0)
                throw new BO.InvalidInput("the price is less than 0");
            if (p.InStock < 0)
                throw new BO.InvalidInput("the amount in stock is less than 0");
            try
            {
                DO.Product pD = new DO.Product() { ID = p.ID, Name = p.Name, Category = (DO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()), Price = p.Price, InStock = p.InStock };
                Dal.Product.Add(pD);
            }
            catch (Exception ex) { throw new DalException("error in adding a product", ex); }
        }
        /// <summary>
        /// deletes product
        /// </summary>
        /// <param name="ID">recived product id</param>
        /// <exception cref="DalException">exception from dal</exception>
        public void Delete(int ID)
        {
            try
            {
                foreach (DO.Order order in Dal.Order.GetAll())
                {
                    foreach (DO.OrderItem item in Dal.OrderItem.GetOrderItemsOfOrder(order.ID))
                    {
                        if (item.ID == ID)
                            throw new BO.AlreadyExists("the product is in alredy in order");
                    }
                }
                Dal.Product.Delete(ID); 
            }
            catch (Exception ex) { throw new DalException("error in deleting a product", ex); }
        }
        /// <summary>
        /// updates product
        /// </summary>
        /// <param name="p">recived product</param>
        public void Update(BO.Product p)
        {
            if (p.ID <= 0)
                throw new BO.InvalidID("there in no such an id");
            if (p.Name == "")
                throw new BO.InvalidInput("the name is empty");
            if (p.Price <= 0)
                throw new BO.InvalidInput("the price is less than 0");
            if (p.InStock < 0)
                throw new BO.InvalidInput("the amount in stock is less than 0");
            try
            {
                DO.Product pD = new DO.Product() { ID = p.ID, Name = p.Name, Category = (DO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()), Price = p.Price, InStock = p.InStock };
                Dal.Product.Update(pD);
            }
            catch (Exception ex) { throw new DalException("error in updating a product", ex); }
        }
    }
}
