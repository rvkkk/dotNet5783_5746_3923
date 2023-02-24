using DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// adds a new product
    /// </summary>
    /// <param name="p">recived product</param>
    /// <returns>the products id</returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product p)
    {
        if (DataSource.lProduct.FirstOrDefault(pr => pr?.ID == p.ID) != null)
            throw new AlreadyExists("the id already exists");
        DataSource.lProduct.Add(p);
        return p.ID;
    }

    /// <summary>
    /// deletes product
    /// </summary>
    /// <param name="ID">recived product id</param>
    /// <exception cref="Exception">there in no such a product id</exception>
    public void Delete(int ID)
    {
        if (DataSource.lProduct.RemoveAll(p => p?.ID == ID) == 0)
            throw new InvalidID("there in no such a product id");
    }

    /// <summary>
    /// updates product
    /// </summary>
    /// <param name="p">recived product</param>
    public void Update(Product p)
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
    public Product Get(int ID)
    {
        return DataSource.lProduct.FirstOrDefault(p => p?.ID == ID) ??
           throw new InvalidID("there in no such a product id");
    }

    /// <summary>
    /// returns product by a function
    /// </summary>
    /// <param name="func">a delegate</param>
    /// <returns>product</returns>
    /// <exception cref="InvalidID">there in no such a product</exception>
    public Product GetByF(Func<Product?, bool> func)
    {
        return DataSource.lProduct.Where(func).FirstOrDefault() ??
         throw new InvalidID("there in no such a product");
    }

    /// <summary>
    /// returns all products
    /// </summary>
    /// <returns>products</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
    {
        if (func == null)
            return DataSource.lProduct.Select(p => p);
        else
            return DataSource.lProduct.Where(func);
    }
}