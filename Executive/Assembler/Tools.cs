using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesVM.Executive.Assembler
{
    public static class Tools
    {
        private static Int64 _exOffsetOrigin;

        public static Int64 ExecutableAddress { get; set; }
        public static Int64 ExecutableLength { get; set; }
        public static Int64 ExecutableOrigin { get; set; }
        public static Int64 ExecutableOffset { get; set; }
        public static Int64 ExecutableOffsetOrigin { get => ExecutableOrigin + ExecutableOffset; }

        public static bool IsEnd { get; set; }
        public static bool IsHex { get; set; }
        public static bool IsLabelScan { get; set; }


        /// <summary>
        /// Skips each char value if it is a whitespace.
        /// </summary>
        /// <param name="Source">JSource File</param>
        /// <param name="Index">JSource Index Pointer</param>
        public static void IgnoreWhiteSpaces(string Source, Int32 Index)
        {
            while (char.IsWhiteSpace(Source[Index]))
            {
                Index++;
            }
        }


       
    }
}
