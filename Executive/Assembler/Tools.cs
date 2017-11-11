using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesVM.Executive.Assembler
{
    public static class Tools
    {
        private static Int64 _exLength;
        private static Int64 _exAddress;
        private static Int64 _exOrigin;
        private static Int64 _exOffset;
        private static Int64 _exOffsetOrigin;

        private static bool _isEnd;
        private static bool _isHex;
        private static bool _isLabelScan;

        public static Int64 ExecutableAddress { get => _exAddress; set => _exAddress = value; }
        public static Int64 ExecutableLength { get => _exLength; set => _exLength = value; }
        public static Int64 ExecutableOrigin { get => _exOrigin; set => _exOrigin = value; }
        public static Int64 ExecutableOffset { get => _exOffset; set => _exOffset = value; }
        public static Int64 ExecutableOffsetOrigin { get => _exOrigin + _exOffset; }

        public static bool IsEnd { get => _isEnd; set => _isEnd = value; }
        public static bool IsHex { get => _isHex; set => _isHex = value; }
        public static bool IsLabelScan { get => _isLabelScan; set => _isLabelScan = value; }

        
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
