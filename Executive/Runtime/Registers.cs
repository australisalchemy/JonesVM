using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesVM.Executive
{
    public static class Registers
    {
        /* 
         
        RA          Accumulator Register         
        RB          Base Register
        RX          RX (Accumulator and Base Registers)
        RC          Counter/Control Register
        RD          Data/IO Register

        RSP         Stack Pointer Register (points to the top of the stack)
        RPC         Program Counter Register
        RBP         Base Pointer Register (points to the base of the stack)
             
             
        */
        private static uint _RA;
        private static uint _RB;
        private static ulong _RX;

        private static ulong _RC;
        private static ulong _RD;

        private static ulong _RSP;
        private static ulong _RPC;
        private static ulong _RBP;

        public static uint RA { get => _RA; set => _RA = value; }
        public static uint RB { get => _RB; set => _RB = value; }
        public static ulong RX { get => _RX; private set => _RX = (RA << 32) + RB; }
        public static ulong RC { get => _RC; set => _RC = value; }
        public static ulong RD { get => _RD; set => _RD = value; }
        public static ulong RSP { get => _RSP; set => _RSP = value; }
        public static ulong RPC { get => _RPC; set => _RPC = value; }
        public static ulong RBP { get => _RBP; set => _RBP = value; }

        public static unsafe ulong GetReg64Ptr(ulong Register)
        {
            ulong* StoredPointer = &Register;
            return *StoredPointer;
        }

        public static unsafe uint GetReg32Ptr(uint Register)
        {
                uint* StoredPointer = &Register;
            return *StoredPointer;
        }




    }
}
