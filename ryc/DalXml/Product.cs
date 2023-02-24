using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class Product : IProduct
    {
        const string xmlProductL = @"Product";

        /// <summary>
        /// adds a new product
        /// </summary>
        /// <param name="p">recived product</param>
        /// <returns>the products id</returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.Product p)
        {
            XElement productRootElement = XMLTools.LoadListFromXMLElement(xmlProductL);
            XElement? product = (from productE in productRootElement.Elements()
                                 where (int)productE.Element("ID")! == p.ID
                                 select productE).FirstOrDefault();
            if(product != null) throw new AlreadyExists("the ID already exists");

            XElement productElement = new XElement("Product",
                                       new XElement("ID", p.ID),
                                       new XElement("Name", p.Name),
                                       new XElement("Category", p.Category),
                                       new XElement("Price", p.Price),
                                       new XElement("InStock", p.InStock)
                                       );

            productRootElement.Add(productElement);
            XMLTools.SaveListToXMLElement(productRootElement, xmlProductL);
            return p.ID;
        }

        /// <summary>
        /// deletes product
        /// </summary>
        /// <param name="ID">recived product id</param>
        /// <exception cref="Exception">there in no such a product id</exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int ID)
        {
            XElement prroductRootElement = XMLTools.LoadListFromXMLElement(xmlProductL);

            XElement? product = (from p in prroductRootElement.Elements()
                                 where (int)p.Element("ID")! == ID
                                 select p).FirstOrDefault() ?? throw new InvalidID("there in no such a product id");

            product.Remove();

            XMLTools.SaveListToXMLElement(prroductRootElement, xmlProductL);
        }

        /// <summary>
        /// updates product
        /// </summary>
        /// <param name="p">recived product</param>
        public void Update(DO.Product p)
        {
            Delete(p.ID);
            Add(p);
        }

        /// <summary>
        /// returns product by id
        /// </summary>
        /// <param name="ID">recived product id</param>
        /// <returns>product</returns>
        /// <exception cref="Exception">there in no such a product id</exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public DO.Product Get(int ID)
        {
            XElement productRootElement = XMLTools.LoadListFromXMLElement(xmlProductL);
            return (from p in productRootElement?.Elements()
                    let product = createProductfromXElement(p)
                    where product?.ID == ID
                    select product).FirstOrDefault() ?? throw new InvalidID("there in no such a product id");
        }

        /// <summary>
        /// returns product by a function
        /// </summary>
        /// <param name="func">a delegate</param>
        /// <returns>product</returns>
        /// <exception cref="InvalidID">there in no such a product</exception>
        public DO.Product GetByF(Func<DO.Product?, bool> func)
        {
            XElement productRootElement = XMLTools.LoadListFromXMLElement(xmlProductL);
            return (from p in productRootElement?.Elements()
                    let product = createProductfromXElement(p)
                    where func(product)
                    select product).FirstOrDefault() ?? throw new InvalidID("there in no such a product");
        }

        /// <summary>
        /// returns all products
        /// </summary>
        /// <returns>products</returns>
        public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
        {
            XElement? productRootElement = XMLTools.LoadListFromXMLElement(xmlProductL);

            if (func == null)
            {
                return from p in productRootElement.Elements()
                       let product = createProductfromXElement(p)
                       select product;
            }
            else
            {
                return from p in productRootElement.Elements()
                       let product = createProductfromXElement(p)
                       where func(product)
                       select product;
            }
        }

        /// <summary>
        /// returns DO.Product entity
        /// </summary>
        static DO.Product? createProductfromXElement(XElement p)
        {
            return new DO.Product()
            {
                ID = (int)p.Element("ID")!,
                Name = (string?)p.Element("Name"),
                Category = p.ToEnum<DO.Enums.Category>("Category"),
                Price = (double)p.Element("Price")!,
                InStock = (int)p.Element("InStock")!,
            };
        }
    }
}