using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMachine.interfaces;
using VirtualMachine.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VirtualMachine.Services
{
    internal class ParseService : IParseService
    {
        private IFileHandler _fileService;
        private ICommandDictionary _commandDictionary;

        public ParseService(IFileHandler fileService, ICommandDictionary commandDictionary)
        {

            _fileService = fileService;
            _commandDictionary = commandDictionary;
        }

        /// <summary>
        /// Main method responsible for calling Services and methods to generate asm code.
        /// </summary>
        public void ParseVMCode()
        {
            var filepath = _fileService.GetFilePath("StackTest.vm", FileHandler.DirectoryTypes.Home);
            var text = _fileService.ReadFile(filepath);

            //sort away code containing comments
            var sortedCode = SortLines(text);
            var instructions = SetInstructions(sortedCode.ToArray());


            ConvertToASM(instructions);
        }

        /// <summary>
        /// Removes blank lines and comments
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public IEnumerable<string> SortLines(string[] lines)
        {
            var sortedLines = lines.Where(l => !l.Contains("//") && !string.IsNullOrWhiteSpace(l));
            return sortedLines;
        }

        /// <summary>
        /// Adds VM instructions to a list of type Instruction.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public List<Instruction> SetInstructions(string[] lines)
        {
            List<Instruction> instructions = new List<Instruction>();
            foreach (var line in lines)
            {
                var splittedInstruction = line.Split(' ');
                if (splittedInstruction.Length > 1)
                {
                    instructions.Add(new Instruction() { Command = splittedInstruction[0], Segment = splittedInstruction[1], Value = splittedInstruction[2] });
                }
                else
                {
                    instructions.Add(new Instruction() { Command = splittedInstruction[0] });
                }

            }
            return instructions;
        }

        /// <summary>
        /// takes a list of instruktions and converts it into assembly code
        /// </summary>
        /// <param name="instructions"></param>
        public void ConvertToASM(List<Instruction> instructions)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var instr in instructions)
            {
                switch (instr.Command)
                {
                    case "add":
                        sb.AppendLine(_commandDictionary.Add());
                        break;
                    case "sub":
                        sb.AppendLine(_commandDictionary.Sub());
                        break;
                    case "push":
                        sb.AppendLine(_commandDictionary.Push(instr.Value!));
                        break;
                    case "pop":
                        sb.AppendLine(_commandDictionary.Pop(instr.Segment!, instr.Value!));
                        break;
                    case "eq":
                        sb.AppendLine(_commandDictionary.Eq());
                        break;
                    case "not":
                        sb.AppendLine(_commandDictionary.Not());
                        break;
                    case "or":
                        sb.AppendLine(_commandDictionary.Or());
                        break;
                    case "neg":
                        sb.AppendLine(_commandDictionary.Neg());
                        break;
                    case "gt":
                        sb.AppendLine(_commandDictionary.Gt());
                        break;
                    case "lt":
                        sb.AppendLine(_commandDictionary.Lt());
                        break;
                    default:
                        break;
                }

            }


            //write out file
            var filepath = _fileService.SetOutputPath("StackTest.vm", FileHandler.DirectoryTypes.HomeOutput);
            string result = _fileService.WriteFile(sb.ToString(), filepath);
            Console.WriteLine(result);
        }

    }
}
