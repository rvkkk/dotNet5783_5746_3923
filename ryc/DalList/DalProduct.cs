using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// adds a new product
    /// </summary>
    /// <param name="p">recived product</param>
    /// <returns>the products id</returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product p)
    {
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == p.ID)
                throw new Exception("the id already exists");
        }
        DataSource.productsArray[DataSource.Config.lastProduct] = p;
        DataSource.Config.lastProduct++;
        return p.ID;
    }
    /// <summary>
    /// deletes product
    /// </summary>
    /// <param name="ID">recived product id</param>
    /// <exception cref="Exception">there in no such a product id</exception>
    public void Delete(int ID)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == ID)
            {
                DataSource.Config.lastProduct--;
                flag = true;
            }
            if (flag)
                DataSource.productsArray[i] = DataSource.productsArray[i + 1];
        }
        if (!flag)
            throw new Exception("there in no such an id");
    }
    /// <summary>
    /// updates product
    /// </summary>
    /// <param name="p">recived product</param>
    public void Update(Product p)
    {
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == p.ID)
                DataSource.productsArray[i] = p;
        }
    }
    /// <summary>
    /// return product by id
    /// </summary>
    /// <param name="ID">recived product id</param>
    /// <returns>product</returns>
    /// <exception cref="Exception">there in no such a product id</exception>
    public Product Get(int ID)
    {
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == ID)
               
                return DataSource.productsArray[i];
        }
        throw new Exception("there in no such an id");
    }
    /// <summary>
    /// returns all products
    /// </summary>
    /// <returns>products</returns>
    public Product[] GetAll()
    {
        Product[] products = new Product[DataSource.Config.lastProduct];
        for (int i = 0; i < products.Length; i++)
        {
            products[i] = DataSource.productsArray[i];
        }
        return products;
    }
}
