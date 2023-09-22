using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMachine.interfaces;

namespace VirtualMachine.Services
{
    internal class FileHandler : IFileHandler
    {
        private readonly IConfiguration _configuration;
        public FileHandler(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        //enums for choosing different directory environment
        public enum DirectoryTypes { Laptop, Home,LaptopOutput,HomeOutput };
        public DirectoryTypes DirectoryType;

        /// <summary>
        /// returns an array of strings containing VM commandos
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// writes out an asm file containing assembly commandos
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns full path to filename, based on directory environment
        /// </summary>
        /// <param name="_fileName"></param>
        /// <param name="_output"></param>
        /// <returns></returns>
        public string GetFilePath(string _fileName, DirectoryTypes _output)
        {
            string root = "";
            StringBuilder sb = new StringBuilder();

            root = _getRoot(_output);

            //get path to filename
            string[] files = Directory.GetFiles(root, _fileName, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                var split = files[0].Split("07");
               
                sb.Append(split[1].Remove(0,1));
            }

            var filepath = $"{root}{sb.ToString()}";
           
            return filepath;
        }

        /// <summary>
        /// returns the destination path for file writing. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="_output"></param>
        /// <returns></returns>
        public string SetOutputPath(string filename, DirectoryTypes _output)
        {
            string root = "";
            root = _getRoot(_output);
            string filepath = $"{root}{filename}";
            return filepath;
        }

        /// <summary>
        /// returns the root path based on environment directory
        /// </summary>
        /// <param name="_output"></param>
        /// <returns></returns>
        private string _getRoot(DirectoryTypes _output)
        {
            string root = "";
            switch (_output)
            {
                case DirectoryTypes.Laptop:
                    root = _configuration["filepath:vm"]!;
                    break;
                case DirectoryTypes.Home:
                    root = _configuration["filepath:Home"]!;
                    break;
                case DirectoryTypes.LaptopOutput:
                    root = _configuration["filepath:compiled"]!;
                    break;
                case DirectoryTypes.HomeOutput:
                    root = _configuration["filepath:compiledHome"]!;
                    break;
                default:
                    break;
            }
            return root;
        }
    }
}
