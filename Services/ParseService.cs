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
    internal class ParseService: IParseService
    {
        private IFileHandler _fileService;
        private ICommandDictionary _commandDictionary;
        private readonly IConfiguration _configuration;
        public ParseService(IFileHandler fileService, IConfiguration configuration, ICommandDictionary commandDictionary)
        {

            _fileService = fileService;
            _configuration = configuration;
            _commandDictionary = commandDictionary;
        }

        public void ParseVMCode()
        {
            var path = _configuration["filepath:vm"];
           var text = _fileService.ReadFile(path);

            //sort away code containing comments
            var sortedCode = SortLines(text);
            SetInstructions(sortedCode.ToArray());



        }

        //removes comments and blank spaces
        public IEnumerable<string> SortLines(string[] lines)
        {
           var sortedLines = lines.Where(l => !l.Contains("//") && !string.IsNullOrWhiteSpace(l));
            return sortedLines;
        }

        public List<Instruction> SetInstructions(string[] lines)
        {
            List<Instruction> instructions = new List<Instruction>();
            foreach (var line in lines)
            {
                var splittedInstruction =line.Split(' ');
                if(splittedInstruction.Length > 1)
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

        public void ConvertToASM(List<Instruction> instructions)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var instr in instructions)
            {

            }
        }

    }
}
