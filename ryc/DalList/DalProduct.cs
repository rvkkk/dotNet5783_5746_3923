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
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (DataSource.lProduct[i]?.ID == p.ID)
                throw new AlreadyExists("the id already exists");
        }
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
        bool flag = false;
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (DataSource.lProduct[i]?.ID == ID)
            {
                DataSource.lProduct.Remove(DataSource.lProduct[i]);
                flag = true;
                break;
            }

        }
        if (!flag)
            throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// updates product
    /// </summary>
    /// <param name="p">recived product</param>
    public void Update(Product p)
    {
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (DataSource.lProduct[i]?.ID == p.ID)
                DataSource.lProduct[i] = p;
        }
    }
    /// <summary>
    /// returns product by id
    /// </summary>
    /// <param name="ID">recived product id</param>
    /// <returns>product</returns>
    /// <exception cref="Exception">there in no such a product id</exception>
    public Product Get(int ID)
    {
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (DataSource.lProduct[i]?.ID == ID)

                return (Product)DataSource.lProduct[i]!;
        }
        throw new InvalidID("there in no such an id");
    }
    /// <summary>
    /// returns product by a function
    /// </summary>
    /// <param name="func">a delegate</param>
    /// <returns>product</returns>
    /// <exception cref="InvalidID">there in no such a product</exception>
    public Product GetByF(Func<Product?, bool>? func)
    {
        for (int i = 0; i < DataSource.lOrder.Count; i++)
        {
            if (func!(DataSource.lProduct[i]))
                return (Product)DataSource.lProduct[i]!;
        }
        throw new InvalidID("there in no such an order");
    }
    /// <summary>
    /// returns all products
    /// </summary>
    /// <returns>products</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func = null)
    {
        List<Product?> lproducts = new List<Product?>();
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (func == null)
                lproducts.Add(DataSource.lProduct[i]);
            else if(func(DataSource.lProduct[i]))
                lproducts.Add(DataSource.lProduct[i]);
        }
        return lproducts;
    }
}
