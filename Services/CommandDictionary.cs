using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMachine.interfaces;

namespace VirtualMachine.Services
{
    /// <summary>
    /// returns a string containing the corresponding asm code from VM code
    /// </summary>
    internal class CommandDictionary:ICommandDictionary
    {
        
        public string Add() 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("M=M+D");

            return sb.ToString();
        }

        public string Sub()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("M=M-1");
            sb.AppendLine("A=M");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("M=M-D");

            return sb.ToString();
        }

        public string Eq()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("D=M-D");
            sb.AppendLine("M=-1");
            sb.AppendLine("@EQ_TRUE");
            sb.AppendLine("D;JEQ");
            sb.AppendLine("@SP");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=0");
            sb.AppendLine("(EQ_TRUE)");


            return sb.ToString();
        }

        public string Lt()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("D=M-D");
            sb.AppendLine("M=-1");
            sb.AppendLine("@LT_TRUE");
            sb.AppendLine("D;JLT");
            sb.AppendLine("@SP");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=0");
            sb.AppendLine("(LT_TRUE)");


            return sb.ToString();
        }

        public string Gt()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("D=M-D");
            sb.AppendLine("M=-1");
            sb.AppendLine("@GT_TRUE");
            sb.AppendLine("D;JGT");
            sb.AppendLine("@SP");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=0");
            sb.AppendLine("(GT_TRUE)");


            return sb.ToString();
        }

        public string Not()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("M=!M");
            
            return sb.ToString();
        }

        public string Or()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("M=D|M");

            return sb.ToString();
        }

        public string Neg()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("@SP");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("M=-M");
            
            return sb.ToString();
        }

        public string Push(string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"@{value}");
            sb.AppendLine("D=A");
            sb.AppendLine("@SP");
            sb.AppendLine("A=M");
            sb.AppendLine("M=D");
            sb.AppendLine("@SP");
            sb.AppendLine("M=M+1");

            return sb.ToString();
        }

        public string Pop(string segment,string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"@{value}");
            sb.AppendLine("D=A");
            sb.AppendLine($"@{GetSegment(segment)}");
            sb.AppendLine("D=D+A");
            sb.AppendLine("@R13");
            sb.AppendLine("M=D");
            sb.AppendLine("@SP");
            sb.AppendLine("A=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("@R13");
            sb.AppendLine("A=M");
            sb.AppendLine("M=D");
            sb.AppendLine("@SP");
            sb.AppendLine("M=M-1");

            return sb.ToString();
        }

        public string GetSegment(string segment)
        {
            switch (segment)
            {
                case "local":
                    return "LCL";
                case "argument":
                    return "ARG";
                case "this":
                    return "THIS";
                case "that":
                    return "THAT";
                case "temp":
                    return "5";
                case "pointer":
                    return "3"; 
                              
                default:
                    throw new ArgumentException($"unknown segment: {segment}");
            }
        }

    }
}
