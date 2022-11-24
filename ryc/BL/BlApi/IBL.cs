﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        public IProduct Product { get; }
        public IOrder Order { get; }
        public ICart Cart { get; }     
    }
}
