using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class InvalidID : Exception
    {
        public InvalidID(string ex) : base(ex) {}
    }
    public class AlreadyExists : Exception
    {
        public AlreadyExists(string ex) : base(ex) { }
    }
}
