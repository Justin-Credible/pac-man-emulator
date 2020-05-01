using System;

namespace JustinCredible.ZilogZ80
{
    // TODO: Update with Z80 mnemonics.
    // TODO: Add z80 specific instructions.

    // A list of all the "standard" opcode bytes; can be used to lookup the opcode definition.
    public partial class OpcodeBytes
    {
        /** Halt */
        public const byte HLT = 0x76;

        #region NOP - No operation
            public const byte NOP = 0x00;
            public const byte NOP2 = 0x10; // TODO
            public const byte NOP3 = 0x20; // TODO
            public const byte NOP4 = 0x30; // TODO
            public const byte NOP5 = 0x08; // TODO
            public const byte NOP6 = 0x18; // TODO
            public const byte NOP7 = 0x28; // TODO
            public const byte NOP8 = 0x38; // TODO
        #endregion

        #region Carry bit instructions

            /** Set Carry */
            public const byte STC = 0x37;

            /** Complement Carry */
            public const byte CMC = 0x3f;

        #endregion

        #region Single register instructions

            #region INR - Increment Register or Memory
                public const byte INR_B = 0x04;
                public const byte INR_C = 0x0c;
                public const byte INR_D = 0x14;
                public const byte INR_E = 0x1c;
                public const byte INR_H = 0x24;
                public const byte INR_L = 0x2c;
                public const byte INR_M = 0x34;
                public const byte INR_A = 0x3c;
            #endregion

            #region DCR - Decrement Register or Memory
                public const byte DCR_B = 0x05;
                public const byte DCR_C = 0x0d;
                public const byte DCR_D = 0x15;
                public const byte DCR_E = 0x1d;
                public const byte DCR_H = 0x25;
                public const byte DCR_L = 0x2d;
                public const byte DCR_M = 0x35;
                public const byte DCR_A = 0x3d;
            #endregion

            /** Compliment Accumulator */
            public const byte CMA = 0x2f;

            /** Decimal Adjust Accumulator */
            public const byte DAA = 0x27;

        #endregion

        #region Data transfer instructions

            #region STAX - Store accumulator
                public const byte STAX_B = 0x02;
                public const byte STAX_D = 0x12;
            #endregion

            #region LDAX - Load accumulator
                public const byte LDAX_B = 0x0a;
                public const byte LDAX_D = 0x1a;
            #endregion

            #region MOV - Move (copy) data
                public const byte MOV_B_B = 0x40;
                public const byte MOV_B_C = 0x41;
                public const byte MOV_B_D = 0x42;
                public const byte MOV_B_E = 0x43;
                public const byte MOV_B_H = 0x44;
                public const byte MOV_B_L = 0x45;
                public const byte MOV_B_M = 0x46;
                public const byte MOV_B_A = 0x47;
                public const byte MOV_C_B = 0x48;
                public const byte MOV_C_C = 0x49;
                public const byte MOV_C_D = 0x4a;
                public const byte MOV_C_E = 0x4b;
                public const byte MOV_C_H = 0x4c;
                public const byte MOV_C_L = 0x4d;
                public const byte MOV_C_M = 0x4e;
                public const byte MOV_C_A = 0x4f;
                public const byte MOV_D_B = 0x50;
                public const byte MOV_D_C = 0x51;
                public const byte MOV_D_D = 0x52;
                public const byte MOV_D_E = 0x53;
                public const byte MOV_D_H = 0x54;
                public const byte MOV_D_L = 0x55;
                public const byte MOV_D_M = 0x56;
                public const byte MOV_D_A = 0x57;
                public const byte MOV_E_B = 0x58;
                public const byte MOV_E_C = 0x59;
                public const byte MOV_E_D = 0x5a;
                public const byte MOV_E_E = 0x5b;
                public const byte MOV_E_H = 0x5c;
                public const byte MOV_E_L = 0x5d;
                public const byte MOV_E_M = 0x5e;
                public const byte MOV_E_A = 0x5f;
                public const byte MOV_H_B = 0x60;
                public const byte MOV_H_C = 0x61;
                public const byte MOV_H_D = 0x62;
                public const byte MOV_H_E = 0x63;
                public const byte MOV_H_H = 0x64;
                public const byte MOV_H_L = 0x65;
                public const byte MOV_H_M = 0x66;
                public const byte MOV_H_A = 0x67;
                public const byte MOV_L_B = 0x68;
                public const byte MOV_L_C = 0x69;
                public const byte MOV_L_D = 0x6a;
                public const byte MOV_L_E = 0x6b;
                public const byte MOV_L_H = 0x6c;
                public const byte MOV_L_L = 0x6d;
                public const byte MOV_L_M = 0x6e;
                public const byte MOV_L_A = 0x6f;
                public const byte MOV_M_B = 0x70;
                public const byte MOV_M_C = 0x71;
                public const byte MOV_M_D = 0x72;
                public const byte MOV_M_E = 0x73;
                public const byte MOV_M_H = 0x74;
                public const byte MOV_M_L = 0x75;
                public const byte MOV_M_A = 0x77;
                public const byte MOV_A_B = 0x78;
                public const byte MOV_A_C = 0x79;
                public const byte MOV_A_D = 0x7a;
                public const byte MOV_A_E = 0x7b;
                public const byte MOV_A_H = 0x7c;
                public const byte MOV_A_L = 0x7d;
                public const byte MOV_A_M = 0x7e;
                public const byte MOV_A_A = 0x7f;
            #endregion

        #endregion

        #region Register or memory to accumulator instructions

            #region ADD - Add register or memory to accumulator
                public const byte ADD_B = 0x80;
                public const byte ADD_C = 0x81;
                public const byte ADD_D = 0x82;
                public const byte ADD_E = 0x83;
                public const byte ADD_H = 0x84;
                public const byte ADD_L = 0x85;
                public const byte ADD_M = 0x86;
                public const byte ADD_A = 0x87;
            #endregion

            #region SUB - Subtract register or memory from accumulator
                public const byte SUB_B = 0x90;
                public const byte SUB_C = 0x91;
                public const byte SUB_D = 0x92;
                public const byte SUB_E = 0x93;
                public const byte SUB_H = 0x94;
                public const byte SUB_L = 0x95;
                public const byte SUB_M = 0x96;
                public const byte SUB_A = 0x97;
            #endregion

            #region ANA - Logical AND register or memory with accumulator
                public const byte ANA_B = 0xa0;
                public const byte ANA_C = 0xa1;
                public const byte ANA_D = 0xa2;
                public const byte ANA_E = 0xa3;
                public const byte ANA_H = 0xa4;
                public const byte ANA_L = 0xa5;
                public const byte ANA_M = 0xa6;
                public const byte ANA_A = 0xa7;
            #endregion

            #region ORA - Logical OR register or memory with accumulator
                public const byte ORA_B = 0xb0;
                public const byte ORA_C = 0xb1;
                public const byte ORA_D = 0xb2;
                public const byte ORA_E = 0xb3;
                public const byte ORA_H = 0xb4;
                public const byte ORA_L = 0xb5;
                public const byte ORA_M = 0xb6;
                public const byte ORA_A = 0xb7;
            #endregion

            #region ADC - Add register or memory to accumulator with carry
                public const byte ADC_B = 0x88;
                public const byte ADC_C = 0x89;
                public const byte ADC_D = 0x8a;
                public const byte ADC_E = 0x8b;
                public const byte ADC_H = 0x8c;
                public const byte ADC_L = 0x8d;
                public const byte ADC_M = 0x8e;
                public const byte ADC_A = 0x8f;
            #endregion

            #region SBB - Subtract register or memory from accumulator with borrow
                public const byte SBB_B = 0x98;
                public const byte SBB_C = 0x99;
                public const byte SBB_D = 0x9a;
                public const byte SBB_E = 0x9b;
                public const byte SBB_H = 0x9c;
                public const byte SBB_L = 0x9d;
                public const byte SBB_M = 0x9e;
                public const byte SBB_A = 0x9f;
            #endregion

            #region XRA - Logical XOR register or memory with accumulator
                public const byte XRA_B = 0xa8;
                public const byte XRA_C = 0xa9;
                public const byte XRA_D = 0xaa;
                public const byte XRA_E = 0xab;
                public const byte XRA_H = 0xac;
                public const byte XRA_L = 0xad;
                public const byte XRA_M = 0xae;
                public const byte XRA_A = 0xaf;
            #endregion

            #region CMP - Compare register or memory with accumulator
                public const byte CMP_B = 0xb8;
                public const byte CMP_C = 0xb9;
                public const byte CMP_D = 0xba;
                public const byte CMP_E = 0xbb;
                public const byte CMP_H = 0xbc;
                public const byte CMP_L = 0xbd;
                public const byte CMP_M = 0xbe;
                public const byte CMP_A = 0xbf;
            #endregion

        #endregion

        #region Rotate accumulator instructions

            /** Rotate accumulator left */
            public const byte RLC = 0x07;

            /** Rotate accumulator right */
            public const byte RRC = 0x0f;

            /** Rotate accumulator left through carry */
            public const byte RAL = 0x17;

            /** Rotate accumulator right through carry */
            public const byte RAR = 0x1f;

        #endregion

        #region Register pair instructions

            #region INX - Increment register pair
                public const byte INX_B = 0x03;
                public const byte INX_D = 0x13;
                public const byte INX_H = 0x23;
                public const byte INX_SP = 0x33;
            #endregion

            #region DCX - Decrement register pair
                public const byte DCX_B = 0x0b;
                public const byte DCX_D = 0x1b;
                public const byte DCX_H = 0x2b;
                public const byte DCX_SP = 0x3b;
            #endregion

            #region PUSH - Push data onto the stack
                public const byte PUSH_B = 0xc5;
                public const byte PUSH_D = 0xd5;
                public const byte PUSH_H = 0xe5;
                public const byte PUSH_PSW = 0xf5;
            #endregion

            #region POP - Pop data off of the stack
                public const byte POP_B = 0xc1;
                public const byte POP_D = 0xd1;
                public const byte POP_H = 0xe1;
                public const byte POP_PSW = 0xf1;
            #endregion

            #region DAD - Double (16-bit) add
                public const byte DAD_B = 0x09;
                public const byte DAD_D = 0x19;
                public const byte DAD_H = 0x29;
                public const byte DAD_SP = 0x39;
            #endregion

            /** Load SP from H and L */
            public const byte SPHL = 0xf9;

            /** Exchange stack */
            public const byte XTHL = 0xe3;

            /** Exchange registers */
            public const byte XCHG = 0xeb;

        #endregion

        #region Immediate instructions

            #region MVI - Move immediate data
                public const byte MVI_B = 0x06;
                public const byte MVI_C = 0x0e;
                public const byte MVI_D = 0x16;
                public const byte MVI_E = 0x1e;
                public const byte MVI_H = 0x26;
                public const byte MVI_L = 0x2e;
                public const byte MVI_M = 0x36;
                public const byte MVI_A = 0x3e;
            #endregion

            #region LXI - Load register pair immediate
                public const byte LXI_B = 0x01;
                public const byte LXI_D = 0x11;
                public const byte LXI_H = 0x21;
                public const byte LXI_SP = 0x31;
            #endregion

            /** Add immediate to accumulator */
            public const byte ADI = 0xc6;

            /** Add immediate to accumulator with carry */
            public const byte ACI = 0xce;

            /** Subtract immediate from accumulator */
            public const byte SUI = 0xd6;

            /** Subtract immediate from accumulator with borrow */
            public const byte SBI = 0xde;

            /** Logical AND immediate with accumulator */
            public const byte ANI = 0xe6;

            /** XOR immediate with accumulator */
            public const byte XRI = 0xee;

            /** Logical OR immediate with accumulator */
            public const byte ORI = 0xf6;

            /** Compare immediate with accumulator */
            public const byte CPI = 0xfe;

        #endregion

        #region Direct addressing instructions

            /** Store accumulator direct */
            public const byte STA = 0x32;

            /** Load accumulator direct */
            public const byte LDA = 0x3a;

            /** Store H and L direct */
            public const byte SHLD = 0x22;

            /** Load H and L direct */
            public const byte LHLD = 0x2a;

        #endregion

        #region Jump instructions

            /** Load program counter */
            public const byte PCHL = 0xe9;

            /** Jump */
            public const byte JP = 0xc3;

            /** Jump (duplicate) */
            public const byte JMP2 = 0xcb; // TODO: Bit Instructions

            /** Jump if parity odd */
            public const byte JPO = 0xe2;

            /** Jump if parity even */
            public const byte JPE = 0xea;

            /** Jump if plus/positive */
            public const byte JPP = 0xf2;

            /** Jump if zero */
            public const byte JZ = 0xca;

            /** Jump if not zero */
            public const byte JNZ = 0xc2;

            /** Jump if not carry */
            public const byte JNC = 0xd2;

            /** Jump if carry */
            public const byte JC = 0xda;

            /** Jump if minus/negative */
            public const byte JM = 0xfa;

        #endregion

        #region Call subroutine instructions

            public const byte CALL = 0xcd;
            public const byte CALL2 = 0xdd; // TODO: IX Instructions (DD)
            public const byte CALL3 = 0xed; // TODO: Extended Instructions (ED)
            public const byte CALL4 = 0xfd; // TODO: IY Instructions (FD)

            /** Call if minus/negative */
            public const byte CM = 0xfc;

            /** Call if party even */
            public const byte CPE = 0xec;

            /** Call if carry */
            public const byte CC = 0xdc;

            /** Call if zero */
            public const byte CZ = 0xcc;

            /** Call if plus/positive */
            public const byte CP = 0xf4;

            /** Call if party odd */
            public const byte CPO = 0xe4;

            /** Call if no carry */
            public const byte CNC = 0xd4;

            /** Call if not zero */
            public const byte CNZ = 0xc4;

        #endregion

        #region Return from subroutine instructions

            /** Return from subroutine */
            public const byte RET = 0xc9;

            /** Return from subroutine (duplicate) */
            public const byte RET2 = 0xd9; // TODO

            /** Return if not zero */
            public const byte RNZ = 0xc0;

            /** Return if zero */
            public const byte RZ = 0xc8;

            /** Return if no carry */
            public const byte RNC = 0xd0;

            /** Return if carry */
            public const byte RC = 0xd8;

            /** Return if parity odd */
            public const byte RPO = 0xe0;

            /** Return if parity even */
            public const byte RPE = 0xe8;

            /** Return if plus/positive */
            public const byte RP = 0xf0;

            /** Return if minus/negative */
            public const byte RM = 0xf8;

        #endregion

        #region Restart (interrupt handlers) instructions

            /** CALL $0 */
            public const byte RST_0 = 0xc7;

            /** CALL $8 */
            public const byte RST_1 = 0xcf;

            /** CALL $10 */
            public const byte RST_2 = 0xd7;

            /** CALL $18 */
            public const byte RST_3 = 0xdf;

            /** CALL $20 */
            public const byte RST_4 = 0xe7;

            /** CALL $28 */
            public const byte RST_5 = 0xef;

            /** CALL $30 */
            public const byte RST_6 = 0xf7;

            /** CALL $38 */
            public const byte RST_7 = 0xff;

        #endregion

        #region Interrupt flip-flop instructions

            /** Enable interrupts */
            public const byte EI = 0xfb;

            /** Disable interrupts */
            public const byte DI = 0xf3;

        #endregion

        #region Input/Output Instructions

            /** Output accumulator to given device number */
            public const byte OUT = 0xd3;

            /** Retrieve input from given device number and populate accumulator */
            public const byte IN = 0xdb;

        #endregion
    }
}
