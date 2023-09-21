using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMachine.interfaces;

namespace VirtualMachine.Managers
{
    internal class VMManager: IVMManager
    {
        private IParseService _parseService;

        public VMManager(IParseService parseService)
        {
            _parseService = parseService;
        }

        public void CompileCode()
        {
            _parseService.ParseVMCode();
            
        }
    }
}
