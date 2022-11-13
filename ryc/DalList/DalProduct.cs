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
    public int Add(Product p)
    {
        p.ID = DataSource.Config.p;//66000
        DataSource.productsArray[DataSource.Config.lastProduct] = p;
        DataSource.Config.lastProduct++;
        return p.ID;
    }
    public void Delete(int ID)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == ID)
                flag = true;
            if (flag)
                DataSource.productsArray[i] = DataSource.productsArray[i + 1];
        }
        if (!flag)
            throw new Exception("there in no such an id");
    }
    public void Update(Product p)
    {
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == p.ID)
                DataSource.productsArray[i] = p;
        }
    }
    public Product Get(int ID)
    {
        for (int i = 0; i < DataSource.Config.lastProduct; i++)
        {
            if (DataSource.productsArray[i].ID == ID)
               
                return DataSource.productsArray[i];
        }
        throw new Exception("there in no such an id");
    }
    public Product[] GetAll()
    {
        return DataSource.productsArray;
    }
}
