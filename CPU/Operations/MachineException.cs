using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesVM.CPU.Operations
{
    public class MachineException
    {
        private string _MachineExceptionName;

        public string ExceptionName
        {
            get { return _MachineExceptionName; }
            set { _MachineExceptionName = value; }
        }

        private string _MachineExceptionMessage;

        public string ExceptionMessage
        {
            get { return _MachineExceptionMessage; }
            set { _MachineExceptionMessage = value; }
        }

        
    }
}
