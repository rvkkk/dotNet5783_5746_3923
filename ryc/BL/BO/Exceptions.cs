using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class InvalidID : Exception
    {
        public InvalidID(string ex) : base(ex) { }
    }
    public class InvalidInput : Exception
    {
        public InvalidInput(string ex) : base(ex) { }
    }
    public class AlreadyExists : Exception
    {
        public AlreadyExists(string ex) : base(ex) { }
    }
    public class AlreadyDone : Exception
    {
        public AlreadyDone(string ex) : base(ex) { }
    }
    public class LessAmount : Exception
    {
        public LessAmount(string ex) : base(ex) { }
    }
    public class DalException : Exception
    {
        public DalException(string ex, Exception innerEx) : base(ex, innerEx) { }
    }
}
