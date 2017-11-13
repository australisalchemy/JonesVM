using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using JonesVM.CPU;
using JonesVM.CPU.Operations;
using System.IO;

namespace JonesVM.Executive.Assembler
{
    public static class Reader
    {
        /// <summary>
        /// Reads a 64-bit unsigned integer from a source file.
        /// </summary>
        /// <param name="Source">JSource File</param>
        /// <param name="Index">JSource Index Pointer</param>
        /// <returns>A 64-bit unsigned integer</returns>
        public static UInt64 ReadQWord(string Source, Int32 Index)
        {
            UInt64 QWordValue;
            string lineValue = String.Empty;

            if (Source[Index] == '$') { Index++; Tools.IsHex = true; }
            if (!Tools.IsHex && (char.IsLetter(Source[Index]))) { QWordValue = (UInt64)LabelScanner.LabelTable[LabelScanner.ScanLabelName(Source, Index)]; return QWordValue; }

            while (Char.IsLetterOrDigit(Source[Index])) { lineValue = lineValue + Source[Index]; Index++; }

            if (Tools.IsHex) { QWordValue = Convert.ToUInt64(lineValue, 16); }
            else { QWordValue = UInt64.Parse(lineValue); }

            return QWordValue;
        }

        /// <summary>
        /// Reads a 32-bit unsigned integer from a source file.
        /// </summary>
        /// <param name="Source">JSource File</param>
        /// <param name="Index">JSource Index Pointer</param>
        /// <returns>A 32-bit unsigned integer</returns>
        public static UInt32 ReadDWord(string Source, Int32 Index)
        {
            UInt32 DWordValue;
            string LineValue = null;

            if (Source[Index] == '$') { Index++; Tools.IsHex = true; }

            while (char.IsLetterOrDigit(Source[Index])) { LineValue = LineValue + Source[Index]; }

            if (Tools.IsHex) { DWordValue = Convert.ToUInt32(LineValue, 16); }
            else { DWordValue = UInt32.Parse(LineValue); }

            return DWordValue;
        }

        /// <summary>
        /// Reads a 16-bit unsigned integer from a source file.
        /// </summary>
        /// <param name="Source">JSource File</param>
        /// <param name="Index">JSource Index Pointer</param>
        /// <returns>A 16-bit unsigned integer</returns>
        public static UInt16 ReadWord(string Source, Int32 Index)
        {
            UInt16 WordValue;
            string LineValue = null;

            if (Source[Index] == '$') { Index++; Tools.IsHex = true; }

            while (char.IsLetterOrDigit(Source[Index])) { LineValue = LineValue + Source[Index]; }

            if (Tools.IsHex) { WordValue = Convert.ToUInt16(LineValue, 16); }
            else { WordValue = UInt16.Parse(LineValue); }

            return WordValue;
        }

        /// <summary>
        /// Reads a 8-bit unsigned integer from a source file.
        /// </summary>
        /// <param name="Source">JSource File</param>
        /// <param name="Index">JSource Index Pointer</param>
        /// <returns>An 8-bit unsigned integer</returns>
        public static Byte ReadByte(string Source, Int32 Index)
        {
            byte ByteValue;
            string LineValue = null;

            if (Source[Index] == '$') { Index++; Tools.IsHex = true; }

            while (char.IsLetterOrDigit(Source[Index])) { LineValue = LineValue + Source[Index]; }

            if (Tools.IsHex) { ByteValue = Convert.ToByte(LineValue, 16); }
            else { ByteValue = byte.Parse(LineValue); }

            return ByteValue;
        }

        public static void ReadOpcode(string Source, Int32 Index, BinaryWriter Outfile)
        {
             string opcode = null;

            while (!(char.IsWhiteSpace(Source[Index]))) { opcode = opcode + Source[Index]; Index++; }

            if (opcode.ToUpper() == "LDRA") { MachineOperations.ReadRegister(Source, Index, Register.RA, Outfile); }
            if (opcode.ToUpper() == "LDRB") { MachineOperations.ReadRegister(Source, Index, Register.RB, Outfile); }
            if (opcode.ToUpper() == "LDRC") { MachineOperations.ReadRegister(Source, Index, Register.RC, Outfile); }
            if (opcode.ToUpper() == "LDRD") { MachineOperations.ReadRegister(Source, Index, Register.RD, Outfile); }
            if (opcode.ToUpper() == "LDRX") { MachineOperations.ReadRegister(Source, Index, Register.RX, Outfile); }
            if (opcode.ToUpper() == "LDSP") { MachineOperations.ReadRegister(Source, Index, Register.RSP, Outfile); }
            if (opcode.ToUpper() == "LDBP") { MachineOperations.ReadRegister(Source, Index, Register.RBP, Outfile); }
            if (opcode.ToUpper() == "LDPC") { MachineOperations.ReadRegister(Source, Index, Register.RPC, Outfile); }

            if (opcode.ToUpper() == "JUMP") { }
            if (opcode.ToUpper() == "CMPA") { }
            if (opcode.ToUpper() == "CMPB") { }
            if (opcode.ToUpper() == "CMPC") { }
            if (opcode.ToUpper() == "CMPD") { }
            if (opcode.ToUpper() == "CMPX") { }
            if (opcode.ToUpper() == "PUSH") { Outfile.Write((Byte)Opcodes.PUSH); Tools.ExecutableLength++; }
            if (opcode.ToUpper() == "TAKE") { Outfile.Write((Byte)Opcodes.TAKE); Tools.ExecutableLength++; }

            if (opcode.ToUpper() == "ADD") { MathOperations.PerformMathOperation(Source, Index, Outfile, MathOperations.MathOperation.ADD); }
            if (opcode.ToUpper() == "SUB") { MathOperations.PerformMathOperation(Source, Index, Outfile, MathOperations.MathOperation.SUB); }
            if (opcode.ToUpper() == "MUL") { MathOperations.PerformMathOperation(Source, Index, Outfile, MathOperations.MathOperation.MUL); }
            if (opcode.ToUpper() == "DIV") { MathOperations.PerformMathOperation(Source, Index, Outfile, MathOperations.MathOperation.DIV); }

            if (opcode.ToUpper() == "HALT") { Outfile.Write((Byte)Opcodes.HALT); Tools.ExecutableLength++; }
            if (opcode.ToUpper() == "CALL") { }
            if (opcode.ToUpper() == "JTS") { }
            if (opcode.ToUpper() == "END") { Tools.IsEnd = true; Tools.ExecutableLength++; Outfile.Write((Byte)Opcodes.END); Tools.IgnoreWhiteSpaces(Source, Index); Tools.ExecutableAddress = (Int64)LabelScanner.LabelTable[(LabelScanner.ScanLabelName(Source, Index))]; return; }

            while (Source[Index] != '\n') { Index++; }

            Index++;
        }
    }
}
