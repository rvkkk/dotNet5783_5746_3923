﻿using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    sealed public class Bl : IBL
    {
        public ICart Cart => new Cart();
        public IOrder Order => new Order();
        public IProduct Product => new Product();
    }
}
