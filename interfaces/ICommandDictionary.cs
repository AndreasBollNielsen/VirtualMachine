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

        public string Pop(string segment, string value);

        public string Eq();

        public string Lt();

        public string Gt();

        public string Not();

        public string Or();

        public string Neg();
        

    }
}
