using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using JonesVM.CPU;
using JonesVM.CPU.Operations;
using System.Collections;

namespace JonesVM.Executive.Compiler
{
    // note change bytes to longs or ints (depending on architecture)
    // this class parses our JASM and builds a stream of bytecode that our executive class will execute
    public class JCompiler
    {
        private Hashtable _LTable;
        private string _JBinary;
        private string _SourcePath;
        private int JIndex;
        private bool _IsHex;
        private ulong _ExLength;
        private bool _IsEnd;
        private ulong _ExecAddr;
        private ulong _ExOrigin;
        private int _ExOffsetBegin;

        private struct FileInfo
        {
            const string Identifier = "";
            public int FileMajor;
            public int FileMinor;
            public int FileBuild;

            public DateTime CreationDate;
        }


        private const string _ASCIIHeader = "JVM Executable file format. This program can only be run on a X86 CPU.";

        private const ulong _BinaryMagic = 0xFAC01;

        public JCompiler(string JFilePath)
        {
            Console.WriteLine("JVM Compiler© 1.0\nCopyright (c) 2017 Jones Electric");
            Console.WriteLine("=======================================================\n");
            //Compile(JFile)
            // return the compiled bytecode for our executive to run

            _LTable = new Hashtable(128);
            JIndex = 0;
            _ExLength = 0;
            _ExecAddr = 0;
            _IsEnd = false;
            _SourcePath = JFilePath;
            _ExOrigin = 0x50;


        }

        private enum StatementType
        {
            
        }

        private string ScanLabelName()
        {
            string LName = null;

            while(char.IsLetterOrDigit(_JBinary[JIndex]))
            {
                if (_JBinary[JIndex] == ':')
                {
                    JIndex++;
                    break;
                }

                LName = LName + _JBinary[JIndex];
                JIndex++;
            }
            return LName.ToUpper();
        }

        private void IgnoreWhiteSpaces()
        {
            while (char.IsWhiteSpace(_JBinary[JIndex]))
            {
                JIndex++;
            }
        }

        private ulong ReadWord()
        {
            ulong MethodValue;
            string LineValue = null;

            if (_JBinary[JIndex] == '$') { JIndex++; _IsHex = true; }
            if (!_IsHex && (char.IsLetter(_JBinary[JIndex]))) { MethodValue = (ulong)_LTable[ScanLabelName()]; return MethodValue; }

            while (char.IsLetterOrDigit(_JBinary[JIndex])) { LineValue = LineValue + _JBinary[JIndex]; JIndex++; }


            if (_IsHex) { MethodValue = Convert.ToUInt64(LineValue, 16); }
            else { MethodValue = ulong.Parse(LineValue); }

            return MethodValue;
        }

        private byte ReadByte()
        {
            byte MethodValue;
            string LineValue = null;

            if (_JBinary[JIndex] == '$') { JIndex++; _IsHex = true; }

            while (char.IsLetterOrDigit(_JBinary[JIndex])) { LineValue = LineValue + _JBinary[JIndex]; }

            if (_IsHex) { MethodValue = Convert.ToByte(LineValue, 16); }
            else { MethodValue = byte.Parse(LineValue); }

            return MethodValue;
        }

        private void ReadOpcode(BinaryWriter OutFile, bool IsLabelScan)
        {
            string opcode = null;

            while (!(char.IsWhiteSpace(_JBinary[JIndex]))) { opcode = opcode + _JBinary[JIndex]; JIndex++; }

            if (opcode.ToUpper() == "LDRA") { LoadRegister(OutFile, IsLabelScan, Register.RA); }
            if (opcode.ToUpper() == "LDRB") { LoadRegister(OutFile, IsLabelScan, Register.RB); }
            if (opcode.ToUpper() == "LDRC") { LoadRegister(OutFile, IsLabelScan, Register.RC); }
            if (opcode.ToUpper() == "LDRD") { LoadRegister(OutFile, IsLabelScan, Register.RD); }
            if (opcode.ToUpper() == "LDRX") { LoadRegister(OutFile, IsLabelScan, Register.RX); }
            if (opcode.ToUpper() == "LDSP") { LoadRegister(OutFile, IsLabelScan, Register.RSP); }
            if (opcode.ToUpper() == "LDBP") { LoadRegister(OutFile, IsLabelScan, Register.RBP); }
            if (opcode.ToUpper() == "LDPC") { LoadRegister(OutFile, IsLabelScan, Register.RPC); }

            if (opcode.ToUpper() == "JUMP") { }
            if (opcode.ToUpper() == "CMPA") { }
            if (opcode.ToUpper() == "CMPB") { }
            if (opcode.ToUpper() == "CMPC") { }
            if (opcode.ToUpper() == "CMPD") { }
            if (opcode.ToUpper() == "CMPX") { }
            if (opcode.ToUpper() == "PUSH") { OutFile.Write((byte)Opcodes.PUSH); _ExLength++; }
            if (opcode.ToUpper() == "TAKE") { OutFile.Write((byte)Opcodes.TAKE); _ExLength++; }

            if (opcode.ToUpper() == "ADD") { MathOperation(OutFile, IsLabelScan, MathOperations.MathOperation.ADD); }
            if (opcode.ToUpper() == "SUB") { MathOperation(OutFile, IsLabelScan, MathOperations.MathOperation.SUB); }
            if (opcode.ToUpper() == "MUL") { MathOperation(OutFile, IsLabelScan, MathOperations.MathOperation.MUL); }
            if (opcode.ToUpper() == "DIV") { MathOperation(OutFile, IsLabelScan, MathOperations.MathOperation.DIV); }

            if (opcode.ToUpper() == "HALT") { OutFile.Write((byte)Opcodes.HALT); _ExLength++; }
            if (opcode.ToUpper() == "CALL") { }
            if (opcode.ToUpper() == "JTS") { }
            if (opcode.ToUpper() == "END") { _IsEnd = true; EndJApp(OutFile, IsLabelScan); IgnoreWhiteSpaces();  _ExecAddr = (ulong)_LTable[(ScanLabelName())]; return; }

            while (_JBinary[JIndex] != '\n') { JIndex++; }

            JIndex++;
        }

        private Register ReadRegisterValue()
        {
            CPU.Register regs = CPU.Register.R0;

            string Register = null;

            while (char.IsLetterOrDigit(_JBinary[JIndex])) { Register = Register + _JBinary[JIndex]; }

            switch (Register.ToUpper())
            {
                case "RA":
                    JIndex++;
                    return CPU.Register.RA;

                case "RB":
                    JIndex++;
                    regs = CPU.Register.RB;
                    break;

                case "RC":
                    JIndex++;
                    regs = CPU.Register.RC;
                    break;

                case "RD":
                    JIndex++;
                    regs = CPU.Register.RD;
                    break;

                case "RX":
                    JIndex++;
                    regs = CPU.Register.RX;
                    break;

                case "RBP":
                    JIndex++;
                    regs = CPU.Register.RBP;
                    break;

                case "RSP":
                    JIndex++;
                    regs = CPU.Register.RSP;
                    break;

                case "RPC":
                    JIndex++;
                    regs = CPU.Register.RPC;
                    break;
            }

            return regs;
        }

        private void EndJApp(BinaryWriter OutFile, bool IsLabelScan)
        {
            _ExLength++;

            if (!IsLabelScan) { OutFile.Write((byte)Opcodes.END); }
        }

        private void LabelScanner(BinaryWriter OutFile, bool IsLabelScan)
        {
            if (char.IsLetter(_JBinary[JIndex]))
            {
                if (IsLabelScan)
                {
                    _LTable.Add(ScanLabelName(), _ExLength);
                    while (_JBinary[JIndex] != '\n') { JIndex++; }
                    JIndex++;
                    return;
                }
            }
            IgnoreWhiteSpaces();
            ReadOpcode(OutFile, IsLabelScan);
        }

        private void LoadRegister(BinaryWriter OutFile, bool IsLabelScan, Register Register)
        {
            IgnoreWhiteSpaces();
            if (_JBinary[JIndex] == '#') { JIndex++; ulong WValue = ReadWord(); _ExLength += 3; if (!IsLabelScan) { OutFile.Write((byte)Register); OutFile.Write(WValue); } }
        }

        private void CompareRegister(BinaryWriter OutFile, bool IsLabelScan, Register Register)
        {

        }

        private void StoreRegister(BinaryWriter OutFile, bool IsLabelScan, Register Register)
        {
            IgnoreWhiteSpaces();
            if (_JBinary[JIndex] == '%')
            {
                Register r;
                byte Opcode = 0x00;

                JIndex++;

                r = ReadRegisterValue();

                switch (r)
                {
                    case Register.RX:
                        Opcode = (byte)Register.RX;
                        break;
                }
            }
        }

        private void BuildHeaders(BinaryWriter OutFile)
        {
            FileInfo FiHeaders;

            FiHeaders.FileMajor = 1;
            FiHeaders.FileMinor = 0;
            FiHeaders.CreationDate = DateTime.Now;

            OutFile.Write(_ASCIIHeader);
            OutFile.Write(_BinaryMagic);
            OutFile.Write(_ExLength);
            OutFile.Write(_ExecAddr);

            //  0xFAC01
            //  (ulong)(byte)
            //  (ulong)(byte)
        }

        private void MathOperation(BinaryWriter OutFile, bool IsLabelScan, MathOperations.MathOperation OperationType)
        {
            IgnoreWhiteSpaces();
            Opcodes opcode;

            switch (OperationType)
            {
                case MathOperations.MathOperation.ADD:
                    opcode = Opcodes.ADD;
                    break;

                case MathOperations.MathOperation.SUB:
                    opcode = Opcodes.SUB;
                    break;

                case MathOperations.MathOperation.DIV:
                    opcode = Opcodes.DIV;
                    break;

                case MathOperations.MathOperation.MUL:
                    opcode = Opcodes.MUL;
                    break;

                default:
                    opcode = Opcodes.ADD;
                    break;
            }

            if (_JBinary[JIndex] == '#')
            {
                JIndex++;

                _ExLength += 3;
                if (IsLabelScan) { return; }
                ulong BValue = ReadByte();

                if (!IsLabelScan) { OutFile.Write((byte)opcode); OutFile.Write(BValue); }
            }
        }

        private void ParseAssembly(BinaryWriter OutFile)
        {
            JIndex = 0;
            while (!_IsEnd) { LabelScanner(OutFile, true); }
            _IsEnd = false;
            JIndex = 0;
            _ExLength = Convert.ToUInt64(_ExOrigin);
            while (!_IsEnd) { LabelScanner(OutFile, false); }
        }

        public void CompileAssembly(string OutFileName)
        {
            TextReader AInput;
            BinaryWriter AOutput;
            FileStream AStream;

            AStream = new FileStream(OutFileName + ".jvm", FileMode.Create);
            AOutput = new BinaryWriter(AStream);
            AInput = File.OpenText(_SourcePath);

            Console.WriteLine("Source Path: {0}\nOutput Path: {1}.jvm\nPreparing to build headers...\n", _SourcePath, OutFileName);

            _JBinary = AInput.ReadToEnd();
            AInput.Close();

            BuildHeaders(AOutput);

            Console.WriteLine("Headers have been built!\n");

            AOutput.Write((ulong)0);
            ParseAssembly(AOutput);

            Console.WriteLine("Parsing assembly...\n");

            AOutput.Seek(_ExOffsetBegin, SeekOrigin.Begin);
            AOutput.Write(_ExecAddr);
            AOutput.Close();
            AStream.Close();

            Console.WriteLine("{0}.jvm has been successfully written and saved!\n", OutFileName);
            
        }

    }
}
