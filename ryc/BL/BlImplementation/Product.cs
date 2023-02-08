using BlApi;
using BO;
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation
{
    internal class Product : IProduct
    {
        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// returns all the exist products
        /// </summary>
        /// <param name="func">a recived function</param>
        /// <returns>list of the products according to the function</returns>
        public IEnumerable<BO.ProductForList?> GetAll(Func<BO.ProductForList?, bool>? func = null)
        {
            IEnumerable<BO.ProductForList?> lproducts;
            lproducts = from DO.Product? product in dal!.Product.GetAll()
                        select new BO.ProductForList() { ID = (int)product?.ID!, Name = product?.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), product?.Category.ToString()!), Price = (double)product?.Price! };
            return func is null ? lproducts : lproducts.Where(func);
        }

        /// <summary>
        /// returns all the exist product items
        /// </summary>
        /// <param name="func">a recived function</param>
        /// <returns>list of the products according to the function</returns>
        public IEnumerable<ProductItem?> GetAllPI(Func<BO.ProductItem?, bool>? func = null)
        {

            IEnumerable<ProductItem?> items;
            items = dal!.Product.GetAll().Select(pI => {
                DO.Product p = dal!.Product.Get((int)pI?.ID!);
                if (p.InStock != 0)
                    return new ProductItem() { ID = p.ID, Name = p.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), p.Category.ToString()!), Price = p.Price, Amount = 0, InStock = true };
                return null;
            });
            return func is null ? items : items.Where(func);
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
                DO.Product pD = dal!.Product.Get(ID);
                BO.Product pB = new BO.Product() { ID = pD.ID, Name = pD.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), pD.Category.ToString()!), Price = pD.Price, InStock = pD.InStock };
                return pB;
            }
            catch (Exception ex) { throw new DalException("error in getting a product", ex); }
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
                DO.Product pD = (DO.Product)dal?.Product.Get(ID)!;
                BO.ProductItem pIB = new BO.ProductItem() { ID = pD.ID, Name = pD.Name, Category = (BO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category), pD.Category.ToString()!), Price = pD.Price, Amount = (int)cart?.Items?.First(x => x!.ID == ID)!.Amount!, InStock = pD.InStock != 0 };
                return pIB;
            }
            catch (DO.InvalidID ex) { throw new DalException("error in getting a product in cart", ex); }
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
                DO.Product pD = new DO.Product() { ID = p.ID, Name = p.Name, Category = (DO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()!), Price = p.Price, InStock = p.InStock };
                dal?.Product.Add(pD);
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
                if (dal?.OrderItem.GetByF(oI => oI?.ProductID == ID) != null)
                    throw new BO.AlreadyExists("the product is in alredy in order");
                else
                    dal?.Product.Delete(ID);
            }
            catch (DO.InvalidID ex) { throw new DalException("error in deleting a product", ex); }
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
                DO.Product pD = new DO.Product() { ID = p.ID, Name = p.Name, Category = (DO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()!), Price = p.Price, InStock = p.InStock };
                dal?.Product.Update(pD);
            }
            catch (Exception ex) { throw new DalException("error in updating a product", ex); }
        }
    }
}
