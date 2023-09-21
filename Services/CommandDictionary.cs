using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMachine.interfaces;

namespace VirtualMachine.Services
{
    internal class CommandDictionary:ICommandDictionary
    {
        public CommandDictionary() { }

        public string Add() 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("@SP");
            sb.Append("M=M-1");
            sb.Append("D=M");
            sb.Append("A=A-1");
            sb.Append("M=M+D");

            return sb.ToString();
        }

        public string Sub()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("@SP");
            sb.Append("M=M-1");
            sb.Append("A=M");
            sb.Append("D=M");
            sb.Append("A=A-1");
            sb.Append("M=M-D");

            return sb.ToString();
        }

        public string Push(string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"@{value}");
            sb.Append("D=A");
            sb.Append("@SP");
            sb.Append("A=M");
            sb.Append("M=D");
            sb.Append("$SP");
            sb.Append("M=M+1");

            return sb.ToString();
        }
    }
}
