using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesVM.CPU
{
    public enum Opcodes
    {
        START =         0xE0,           // Start Program, <programname>
        LDRA  =         0xE1,           // Load Register A, <val>
        LDRB  =         0xE2,           // Load Register B, <val>
        LDRC  =         0xE3,           // Load Register C, <val>
        LDRD  =         0xE4,           // Load Register D, <val>
        LDRX  =         0xE5,           // Return Register X
        LDSP  =         0xE6,           // Load Stack Pointer Register, <val>
        LDPC  =         0xE7,           // Load Program Counter Register, <val>
        LDBP  =         0xE8,           // Load Base Pointer Register, <val>

        JUMP  =         0xB0,           // JUMP, <location>
        CMPA  =         0xB1,           // Compare Register A, <val>
        CMPB  =         0xB2,           // Compare Register B, <val>
        CMPC  =         0xB3,           // Compare Register C, <val>
        CMPD  =         0xB4,           // Compare Register D, <val>
        CMPX  =         0xB9,           // Compare Register X, <val>
        LDSA  =         0xB5,           // Load Stack Address, <val>
        PUSH  =         0xB6,           // Push to the top of the stack
        TAKE  =         0xB7,           // Take from the top of the stack

        ADD   =         0xA1,          // Add RA with a specified value
        SUB   =         0xA4,          // Subtract RA with a specified value
        MUL   =         0xA3,          // Multiply RA with a specified value
        DIV   =         0xA2,          // Divide RA with a specified value

        HALT  =         0xC0,           // Halt machine
        CALL  =         0xC1,           // Call <function>
        JTS   =         0xC2,           // Jump to start
        END   =         0xC3,           // Terminate Program, <programname>
    }
}
