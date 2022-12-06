﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> GetAll();
        public Order Get(int ID);
        public Order ShipUpdate(int ID);
        public Order DeliveryUpdate(int ID);
        public Order ManagerUpdate(int ID, int product, int amount);
        public OrderTracking OrderTracking(int ID);
    }
}
