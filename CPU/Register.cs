using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesVM.CPU
{
    public enum Register
    {
        R0      =       0,
        RA      =       0x01,
        RB      =       0x02,
        RC      =       0x03,
        RD      =       0x04,
        RX      =       0x08,

        RSP     =       0x10,
        RBP     =       0x11,
        RPC     =       0x12,
    }
}
