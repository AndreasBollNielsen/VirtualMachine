﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMachine.Models
{
    /// <summary>
    /// model containing the parts of a single VM command
    /// note value and segment can be null
    /// </summary>
    internal class Instruction
    {
        private string _Command;
        private string? _value;
        private string? _segment;

        public string Command { get => _Command; set => _Command = value; }
        public string? Value { get => _value; set => _value = value; }
        public string? Segment { get => _segment; set => _segment = value; }
    }
}
