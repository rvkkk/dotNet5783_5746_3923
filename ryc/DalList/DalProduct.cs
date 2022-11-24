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
            if (DataSource.lProduct[i].ID == p.ID)
                throw new Exception("the id already exists");
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
            if (DataSource.lProduct[i].ID == ID)
                flag = true;
            if (flag)
                DataSource.lProduct[i] = DataSource.lProduct[i + 1];
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
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (DataSource.lProduct[i].ID == p.ID)
                DataSource.lProduct[i] = p;
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
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            if (DataSource.lProduct[i].ID == ID)
               
                return DataSource.lProduct[i];
        }
        throw new Exception("there in no such an id");
    }
    /// <summary>
    /// returns all products
    /// </summary>
    /// <returns>products</returns>
    public IEnumerable<Product> GetAll()
    {
        List<Product> lproducts = new List<Product>();
        for (int i = 0; i < DataSource.lProduct.Count; i++)
        {
            lproducts.Add(DataSource.lProduct[i]);
        }
        return lproducts;
    }
}
