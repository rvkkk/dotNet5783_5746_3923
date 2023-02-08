using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class InvalidID : Exception
    {
        public InvalidID(string ex) : base(ex) { }
    }

    [Serializable]
    public class InvalidInput : Exception
    {
        public InvalidInput(string ex) : base(ex) { }
    }

    [Serializable]
    public class AlreadyExists : Exception
    {
        public AlreadyExists(string ex) : base(ex) { }
    }

    [Serializable]
    public class AlreadyDone : Exception
    {
        public AlreadyDone(string ex) : base(ex) { }
    }

    [Serializable]
    public class LessAmount : Exception
    {
        public LessAmount(string ex) : base(ex) { }
    }

    [Serializable]
    public class DalException : Exception
    {
        public DalException(string ex, Exception innerEx) : base(ex, innerEx) { }
    }
}
