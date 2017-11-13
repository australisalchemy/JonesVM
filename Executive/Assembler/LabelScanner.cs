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
        public static Hashtable LabelTable { get; set; } = new Hashtable(512);

        public static void ScanLabel(string Source, Int32 Index, BinaryWriter Outfile)
        {
            if (char.IsLetter(Source[Index]))
            {
                if (Tools.IsLabelScan)
                {
                    LabelTable.Add(ScanLabelName(Source, Index), Tools.ExecutableLength);
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
            string LabelName = null;

            while (char.IsLetterOrDigit(Source[Index]))
            {
                if (Source[Index] == ':')
                {
                    Index++;
                    break;
                }

                LabelName = LabelName + Source[Index];
                Index++;
            }
            return LabelName.ToUpper();
        }
    }
}
