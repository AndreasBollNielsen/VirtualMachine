using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualMachine.Services.FileHandler;

namespace VirtualMachine.interfaces
{
    internal interface IFileHandler
    {
        public string[] ReadFile(string path);

        public string WriteFile(string text, string path);

        public string GetFilePath(string _fileName, DirectoryTypes _output);

        public string SetOutputPath(string filename, DirectoryTypes _output);

        
    }
}
