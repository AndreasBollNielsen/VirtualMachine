using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMachine.interfaces
{
    internal interface IFileHandler
    {
        public string[] ReadFile(string path);

        public string WriteFile(string text, string path);
    }
}
