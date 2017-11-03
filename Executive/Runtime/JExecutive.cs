using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonesVM.CPU;
using JonesVM;
using JonesVM.Executive.Compiler;

namespace JonesVM.Executive.Runtime
{
    // this class reads the JIL bytecode fed from the compiler
    // this is the main execution class
    // the heart of the VM
    public class JExecutive
    {
        private bool _IsRunning = false;

        // JExecutive(JBinary bin)
       
        private void DecodeInstructionInput(ulong Instruction)
        {

        }
    }
}
