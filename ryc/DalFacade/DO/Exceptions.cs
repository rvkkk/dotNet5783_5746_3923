using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class InvalidID : Exception
    {
        public InvalidID(string ex) : base(ex) {}
    }

    [Serializable]
    public class AlreadyExists : Exception
    {
        public AlreadyExists(string ex) : base(ex) { }
    }

    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}
