using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JonesVM.Executive.Assembler;


namespace JonesVM.CPU.Operations
{
    public static class MachineOperations
    {
        public static void ReadRegister(string Source, Int32 Index, Register Register, BinaryWriter Outfile)
        {
            Tools.IgnoreWhiteSpaces(Source, Index);
            if (Source[Index] == '#') { Index++; UInt64 QWValue = Reader.ReadQWord(Source, Index); Tools.ExecutableLength += 3; if (!Tools.IsLabelScan) { Outfile.Write((Byte)Register); Outfile.Write(QWValue); } }
        }

        public static Register ReadRegisterValue(string Source, Int32 Index)
        {
            CPU.Register regs = CPU.Register.R0;

            string Register = null;

            while (char.IsLetterOrDigit(Source[Index])) { Register = Register + Source[Index]; }

            switch (Register.ToUpper())
            {
                case "RA":
                    Index++;
                    return CPU.Register.RA;

                case "RB":
                    Index++;
                    regs = CPU.Register.RB;
                    break;

                case "RC":
                    Index++;
                    regs = CPU.Register.RC;
                    break;

                case "RD":
                    Index++;
                    regs = CPU.Register.RD;
                    break;

                case "RX":
                    Index++;
                    regs = CPU.Register.RX;
                    break;

                case "RBP":
                    Index++;
                    regs = CPU.Register.RBP;
                    break;

                case "RSP":
                    Index++;
                    regs = CPU.Register.RSP;
                    break;

                case "RPC":
                    Index++;
                    regs = CPU.Register.RPC;
                    break;
            }

            return regs;
        }
    }
}
