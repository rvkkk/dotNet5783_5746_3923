using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem o)
    {
        o.ID = DataSource.Config.OrderItemID;
        DataSource.orderItemsArray[DataSource.Config.lastOrderItem] = o;
        DataSource.Config.lastOrderItem++;
        return o.ID;
    }
    public void Delete(int ID)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].ID == ID)
                flag = true;
            if (flag)
                DataSource.orderItemsArray[i] = DataSource.orderItemsArray[i + 1];
        }
        if (!flag)
            throw new Exception("there in no such an id");
    }
    public void Update(OrderItem o)
    {
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].ID == o.ID)
                DataSource.orderItemsArray[i] = o;
        }
    }
    public OrderItem Get(int ID)
    {
        for (int i = 0; i < DataSource.Config.lastOrderItem; i++)
        {
            if (DataSource.orderItemsArray[i].ID == ID)
                return DataSource.orderItemsArray[i];
        }
        throw new Exception("there in no such an id");
    }
    public OrderItem[] GetAll()
    {
        return DataSource.orderItemsArray;
    }
}