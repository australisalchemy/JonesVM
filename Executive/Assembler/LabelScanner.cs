using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace JonesVM.Executive.Assembler
{
    public static class LabelScanner
    {
        private static Hashtable _labelTable = new Hashtable(512);

        public static Hashtable LabelTable { get => _labelTable; set => _labelTable = value; }

        public static void ScanLabel(string Source, Int32 Index, BinaryWriter Outfile)
        {
            if (char.IsLetter(Source[Index]))
            {
                if (Tools.IsLabelScan)
                {
                    _labelTable.Add(ScanLabelName(Source, Index), Tools.ExecutableLength);
                    while (Source[Index] != '\n') { Index++; }
                    Index++;
                    return;
                }
            }
            Tools.IgnoreWhiteSpaces(Source, Index);
            Reader.ReadOpcode(Source, Index, Outfile);
        }

        public static string ScanLabelName(string Source, int Index)
        {
            string LName = null;

            while (char.IsLetterOrDigit(Source[Index]))
            {
                if (Source[Index] == ':')
                {
                    Index++;
                    break;
                }

                LName = LName + Source[Index];
                Index++;
            }
            return LName.ToUpper();
        }
    }
}
