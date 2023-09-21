using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMachine.interfaces
{
    internal interface ICommandDictionary
    {
        public string Add();

        public string Sub();

        public string Push(string value);
    }
}
