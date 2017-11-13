using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JonesVM.Executive.Assembler;

namespace JonesVM.CPU.Operations
{
    public static class MathOperations
    {
        public enum MathOperation
        {
            ADD = 0xE0,
            SUB = 0xE1,
            MUL = 0xE2,
            DIV = 0xE3,
        }

        public static void PerformMathOperation(string Source, Int32 Index, BinaryWriter Outfile, MathOperation OperationType)
        {
            if (OperationType == MathOperation.ADD)
            {
                Outfile.Write((Byte)Opcodes.ADD);
            }
            else if (OperationType == MathOperation.DIV)
            {
                Outfile.Write((Byte)Opcodes.DIV);
            }
            else if (OperationType == MathOperation.SUB)
            {
                Outfile.Write((Byte)Opcodes.SUB);
            }
            else if (OperationType == MathOperation.MUL)
            {
                Outfile.Write((Byte)Opcodes.MUL);
            }

            if (Source[Index] == '#')
            {
                Index++;
                Tools.ExecutableLength += 3;
                Outfile.Write(Reader.ReadByte(Source, Index));
            }
        }
    }
}
