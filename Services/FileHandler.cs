using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMachine.interfaces;

namespace VirtualMachine.Services
{
    internal class FileHandler: IFileHandler
    {
        public string[] ReadFile(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                return lines;
            }
            catch (IOException e)
            {
                string[] error = { e.Message };
                return error;
            }
        }

        public string WriteFile(string text, string path)
        {
            try
            {
                File.WriteAllText(path, text);
                return "file successfully written";
            }
            catch (IOException e)
            {

                return $"{e.Message}";
            }
        }
    }
}
