using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static ulong ExecMathOperation(MathOperation OperationType)
        {
            switch (OperationType)
            {
                case MathOperation.ADD:
                    return 0;

                case MathOperation.DIV:
                    return 1;

                case MathOperation.MUL:
                    return 2;

                case MathOperation.SUB:
                    return 3;
            }
            return 0;
        }
    }
}
