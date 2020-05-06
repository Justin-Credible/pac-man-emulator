
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80
{
    // A lookup table to locate opcode definitions by the opcode byte.
    public partial class Opcodes
    {
        public static Dictionary<byte, Opcode> StandardLookup = new Dictionary<byte, Opcode>()
        {
            /** Halt */
            [OpcodeBytes.HALT] = HALT,
    
            #region NOP - No operation
                [OpcodeBytes.NOP] = NOP,
                [OpcodeBytes.NOP2] = NOP2,
                [OpcodeBytes.NOP5] = NOP5,
            #endregion

            #region Carry bit instructions

                /** Set Carry */
                [OpcodeBytes.SCF] = SCF,

                /** Complement Carry */
                [OpcodeBytes.CCF] = CCF,

            #endregion

            #region Single register instructions

            #region INC r - Increment Register or Memory
                [OpcodeBytes.INC_B] = INC_B,
                [OpcodeBytes.INC_C] = INC_C,
                [OpcodeBytes.INC_D] = INC_D,
                [OpcodeBytes.INC_E] = INC_E,
                [OpcodeBytes.INC_H] = INC_H,
                [OpcodeBytes.INC_L] = INC_L,
                [OpcodeBytes.INC_MHL] = INC_MHL,
                [OpcodeBytes.INC_A] = INC_A,
            #endregion

            #region DEC r - Decrement Register or Memory
                [OpcodeBytes.DEC_B] = DEC_B,
                [OpcodeBytes.DEC_C] = DEC_C,
                [OpcodeBytes.DEC_D] = DEC_D,
                [OpcodeBytes.DEC_E] = DEC_E,
                [OpcodeBytes.DEC_H] = DEC_H,
                [OpcodeBytes.DEC_L] = DEC_L,
                [OpcodeBytes.DEC_MHL] = DEC_MHL,
                [OpcodeBytes.DEC_A] = DEC_A,
            #endregion

            /** Compliment Accumulator */
            [OpcodeBytes.CPL] = CPL,

            /** Decimal Adjust Accumulator */
            [OpcodeBytes.DAA] = DAA,

            #endregion

            #region Data transfer instructions

                #region LD (rr), A - Store accumulator
                    [OpcodeBytes.LD_MBC_A] = LD_MBC_A,
                    [OpcodeBytes.LD_MDE_A] = LD_MDE_A,
                #endregion

                #region LD A, (rr) - Load accumulator
                    [OpcodeBytes.LD_A_MBC] = LD_A_MBC,
                    [OpcodeBytes.LD_A_MDE] = LD_A_MDE,
                #endregion

                #region LD r, r - Load (copy) data
                    [OpcodeBytes.LD_B_B] = LD_B_B,
                    [OpcodeBytes.LD_B_C] = LD_B_C,
                    [OpcodeBytes.LD_B_D] = LD_B_D,
                    [OpcodeBytes.LD_B_E] = LD_B_E,
                    [OpcodeBytes.LD_B_H] = LD_B_H,
                    [OpcodeBytes.LD_B_L] = LD_B_L,
                    [OpcodeBytes.LD_B_MHL] = LD_B_MHL,
                    [OpcodeBytes.LD_B_A] = LD_B_A,
                    [OpcodeBytes.LD_C_B] = LD_C_B,
                    [OpcodeBytes.LD_C_C] = LD_C_C,
                    [OpcodeBytes.LD_C_D] = LD_C_D,
                    [OpcodeBytes.LD_C_E] = LD_C_E,
                    [OpcodeBytes.LD_C_H] = LD_C_H,
                    [OpcodeBytes.LD_C_L] = LD_C_L,
                    [OpcodeBytes.LD_C_MHL] = LD_C_MHL,
                    [OpcodeBytes.LD_C_A] = LD_C_A,
                    [OpcodeBytes.LD_D_B] = LD_D_B,
                    [OpcodeBytes.LD_D_C] = LD_D_C,
                    [OpcodeBytes.LD_D_D] = LD_D_D,
                    [OpcodeBytes.LD_D_E] = LD_D_E,
                    [OpcodeBytes.LD_D_H] = LD_D_H,
                    [OpcodeBytes.LD_D_L] = LD_D_L,
                    [OpcodeBytes.LD_D_MHL] = LD_D_MHL,
                    [OpcodeBytes.LD_D_A] = LD_D_A,
                    [OpcodeBytes.LD_E_B] = LD_E_B,
                    [OpcodeBytes.LD_E_C] = LD_E_C,
                    [OpcodeBytes.LD_E_D] = LD_E_D,
                    [OpcodeBytes.LD_E_E] = LD_E_E,
                    [OpcodeBytes.LD_E_H] = LD_E_H,
                    [OpcodeBytes.LD_E_L] = LD_E_L,
                    [OpcodeBytes.LD_E_MHL] = LD_E_MHL,
                    [OpcodeBytes.LD_E_A] = LD_E_A,
                    [OpcodeBytes.LD_H_B] = LD_H_B,
                    [OpcodeBytes.LD_H_C] = LD_H_C,
                    [OpcodeBytes.LD_H_D] = LD_H_D,
                    [OpcodeBytes.LD_H_E] = LD_H_E,
                    [OpcodeBytes.LD_H_H] = LD_H_H,
                    [OpcodeBytes.LD_H_L] = LD_H_L,
                    [OpcodeBytes.LD_H_MHL] = LD_H_MHL,
                    [OpcodeBytes.LD_H_A] = LD_H_A,
                    [OpcodeBytes.LD_L_B] = LD_L_B,
                    [OpcodeBytes.LD_L_C] = LD_L_C,
                    [OpcodeBytes.LD_L_D] = LD_L_D,
                    [OpcodeBytes.LD_L_E] = LD_L_E,
                    [OpcodeBytes.LD_L_H] = LD_L_H,
                    [OpcodeBytes.LD_L_L] = LD_L_L,
                    [OpcodeBytes.LD_L_MHL] = LD_L_MHL,
                    [OpcodeBytes.LD_L_A] = LD_L_A,
                    [OpcodeBytes.LD_MHL_B] = LD_MHL_B,
                    [OpcodeBytes.LD_MHL_C] = LD_MHL_C,
                    [OpcodeBytes.LD_MHL_D] = LD_MHL_D,
                    [OpcodeBytes.LD_MHL_E] = LD_MHL_E,
                    [OpcodeBytes.LD_MHL_H] = LD_MHL_H,
                    [OpcodeBytes.LD_MHL_L] = LD_MHL_L,
                    [OpcodeBytes.LD_MHL_A] = LD_MHL_A,
                    [OpcodeBytes.LD_A_B] = LD_A_B,
                    [OpcodeBytes.LD_A_C] = LD_A_C,
                    [OpcodeBytes.LD_A_D] = LD_A_D,
                    [OpcodeBytes.LD_A_E] = LD_A_E,
                    [OpcodeBytes.LD_A_H] = LD_A_H,
                    [OpcodeBytes.LD_A_L] = LD_A_L,
                    [OpcodeBytes.LD_A_MHL] = LD_A_MHL,
                    [OpcodeBytes.LD_A_A] = LD_A_A,
                #endregion

            #endregion

            #region Register or memory to accumulator instructions

                #region ADD r - Add register or memory to accumulator
                    [OpcodeBytes.ADD_A_B] = ADD_A_B,
                    [OpcodeBytes.ADD_A_C] = ADD_A_C,
                    [OpcodeBytes.ADD_A_D] = ADD_A_D,
                    [OpcodeBytes.ADD_A_E] = ADD_A_E,
                    [OpcodeBytes.ADD_A_H] = ADD_A_H,
                    [OpcodeBytes.ADD_A_L] = ADD_A_L,
                    [OpcodeBytes.ADD_A_MHL] = ADD_A_MHL,
                    [OpcodeBytes.ADD_A_A] = ADD_A_A,
                #endregion

                #region SUB r - Subtract register or memory from accumulator
                    [OpcodeBytes.SUB_B] = SUB_B,
                    [OpcodeBytes.SUB_C] = SUB_C,
                    [OpcodeBytes.SUB_D] = SUB_D,
                    [OpcodeBytes.SUB_E] = SUB_E,
                    [OpcodeBytes.SUB_H] = SUB_H,
                    [OpcodeBytes.SUB_L] = SUB_L,
                    [OpcodeBytes.SUB_MHL] = SUB_MHL,
                    [OpcodeBytes.SUB_A] = SUB_A,
                #endregion

                #region AND r - Logical AND register or memory with accumulator
                    [OpcodeBytes.AND_B] = AND_B,
                    [OpcodeBytes.AND_C] = AND_C,
                    [OpcodeBytes.AND_D] = AND_D,
                    [OpcodeBytes.AND_E] = AND_E,
                    [OpcodeBytes.AND_H] = AND_H,
                    [OpcodeBytes.AND_L] = AND_L,
                    [OpcodeBytes.AND_MHL] = AND_MHL,
                    [OpcodeBytes.AND_A] = AND_A,
                #endregion

                #region OR r - Logical OR register or memory with accumulator
                    [OpcodeBytes.OR_B] = OR_B,
                    [OpcodeBytes.OR_C] = OR_C,
                    [OpcodeBytes.OR_D] = OR_D,
                    [OpcodeBytes.OR_E] = OR_E,
                    [OpcodeBytes.OR_H] = OR_H,
                    [OpcodeBytes.OR_L] = OR_L,
                    [OpcodeBytes.OR_MHL] = OR_MHL,
                    [OpcodeBytes.OR_A] = OR_A,
                #endregion

                #region ADC A, r - Add register or memory to accumulator with carry
                    [OpcodeBytes.ADC_A_B] = ADC_A_B,
                    [OpcodeBytes.ADC_A_C] = ADC_A_C,
                    [OpcodeBytes.ADC_A_D] = ADC_A_D,
                    [OpcodeBytes.ADC_A_E] = ADC_A_E,
                    [OpcodeBytes.ADC_A_H] = ADC_A_H,
                    [OpcodeBytes.ADC_A_L] = ADC_A_L,
                    [OpcodeBytes.ADC_A_MHL] = ADC_A_MHL,
                    [OpcodeBytes.ADC_A_A] = ADC_A_A,
                #endregion

                #region SBC A, r - Subtract register or memory from accumulator with borrow
                    [OpcodeBytes.SBC_A_B] = SBC_A_B,
                    [OpcodeBytes.SBC_A_C] = SBC_A_C,
                    [OpcodeBytes.SBC_A_D] = SBC_A_D,
                    [OpcodeBytes.SBC_A_E] = SBC_A_E,
                    [OpcodeBytes.SBC_A_H] = SBC_A_H,
                    [OpcodeBytes.SBC_A_L] = SBC_A_L,
                    [OpcodeBytes.SBC_A_MHL] = SBC_A_MHL,
                    [OpcodeBytes.SBC_A_A] = SBC_A_A,
                #endregion

                #region XOR r - Logical XOR register or memory with accumulator
                    [OpcodeBytes.XOR_B] = XOR_B,
                    [OpcodeBytes.XOR_C] = XOR_C,
                    [OpcodeBytes.XOR_D] = XOR_D,
                    [OpcodeBytes.XOR_E] = XOR_E,
                    [OpcodeBytes.XOR_H] = XOR_H,
                    [OpcodeBytes.XOR_L] = XOR_L,
                    [OpcodeBytes.XOR_MHL] = XOR_MHL,
                    [OpcodeBytes.XOR_A] = XOR_A,
                #endregion

                #region CP r - Compare register or memory with accumulator
                    [OpcodeBytes.CP_B] = CP_B,
                    [OpcodeBytes.CP_C] = CP_C,
                    [OpcodeBytes.CP_D] = CP_D,
                    [OpcodeBytes.CP_E] = CP_E,
                    [OpcodeBytes.CP_H] = CP_H,
                    [OpcodeBytes.CP_L] = CP_L,
                    [OpcodeBytes.CP_MHL] = CP_MHL,
                    [OpcodeBytes.CP_A] = CP_A,
                #endregion

            #endregion

            #region Rotate accumulator instructions

                /** Rotate accumulator left */
                [OpcodeBytes.RLCA] = RLCA,

                /** Rotate accumulator right */
                [OpcodeBytes.RRCA] = RRCA,

                /** Rotate accumulator left through carry */
                [OpcodeBytes.RLA] = RLA,

                /** Rotate accumulator right through carry */
                [OpcodeBytes.RRA] = RRA,

            #endregion

            #region Register pair instructions

                #region INC rr - Increment register pair
                    [OpcodeBytes.INC_BC] = INC_BC,
                    [OpcodeBytes.INC_DE] = INC_DE,
                    [OpcodeBytes.INC_HL] = INC_HL,
                    [OpcodeBytes.INC_SP] = INX_SP,
                #endregion

                #region DEC rr - Decrement register pair
                    [OpcodeBytes.DEC_BC] = DEC_BC,
                    [OpcodeBytes.DEC_DE] = DEC_DE,
                    [OpcodeBytes.DEC_HL] = DEC_HL,
                    [OpcodeBytes.DEC_SP] = DEC_SP,
                #endregion

                #region PUSH rr - Push data onto the stack
                    [OpcodeBytes.PUSH_BC] = PUSH_BC,
                    [OpcodeBytes.PUSH_DE] = PUSH_DE,
                    [OpcodeBytes.PUSH_HL] = PUSH_HL,
                    [OpcodeBytes.PUSH_AF] = PUSH_AF,
                #endregion

                #region POP rr - Pop data off of the stack
                    [OpcodeBytes.POP_BC] = POP_BC,
                    [OpcodeBytes.POP_DE] = POP_DE,
                    [OpcodeBytes.POP_HL] = POP_HL,
                    [OpcodeBytes.POP_AF] = POP_AF,
                #endregion

                #region ADD HL, rr - Double (16-bit) add
                    [OpcodeBytes.ADD_HL_BC] = ADD_HL_BC,
                    [OpcodeBytes.ADD_HL_DE] = ADD_HL_DE,
                    [OpcodeBytes.ADD_HL_HL] = ADD_HL_HL,
                    [OpcodeBytes.ADD_HL_SP] = ADD_HL_SP,
                #endregion

                /** Load SP from H and L */
                [OpcodeBytes.LD_SP_HL] = LD_SP_HL,

                /** Exchange stack */
                [OpcodeBytes.EX_MSP_HL] = EX_MSP_HL,

                /** Exchange registers */
                [OpcodeBytes.EX_DE_HL] = EX_DE_HL,

            #endregion

            #region Immediate instructions

                #region LD r, n - Load immediate data
                    [OpcodeBytes.LD_B_N] = LD_B_N,
                    [OpcodeBytes.LD_C_N] = LD_C_N,
                    [OpcodeBytes.LD_D_N] = LD_D_N,
                    [OpcodeBytes.LD_E_N] = LD_E_N,
                    [OpcodeBytes.LD_H_N] = LD_H_N,
                    [OpcodeBytes.LD_L_N] = LD_L_N,
                    [OpcodeBytes.LD_MHL_N] = LD_MHL_N,
                    [OpcodeBytes.LD_A_N] = LD_A_N,
                #endregion

                #region LD rr, nn - Load register pair immediate
                    [OpcodeBytes.LD_BC_NN] = LD_BC_NN,
                    [OpcodeBytes.LD_DE_NN] = LD_DE_NN,
                    [OpcodeBytes.LD_HL_NN] = LD_HL_NN,
                    [OpcodeBytes.LD_SP_NN] = LD_SP_NN,
                #endregion

                /** Add immediate to accumulator */
                [OpcodeBytes.ADD_A_N] = ADD_A_N,

                /** Add immediate to accumulator with carry */
                [OpcodeBytes.ADC_A_N] = ADC_A_N,

                /** Subtract immediate from accumulator */
                [OpcodeBytes.SUB_N] = SUB_N,

                /** Subtract immediate from accumulator with borrow */
                [OpcodeBytes.SBC_A_N] = SBC_A_N,

                /** Logical AND immediate with accumulator */
                [OpcodeBytes.AND_N] = AND_N,

                /** XOR immediate with accumulator */
                [OpcodeBytes.XOR_N] = XOR_N,

                /** Logical OR immediate with accumulator */
                [OpcodeBytes.OR_N] = OR_N,

                /** Compare immediate with accumulator */
                [OpcodeBytes.CP_N] = CP_N,

            #endregion

            #region Direct addressing instructions

                /** Store accumulator direct */
                [OpcodeBytes.LD_MNN_A] = LD_MNN_A,

                /** Load accumulator direct */
                [OpcodeBytes.LD_A_MNN] = LD_A_MNN,

                /** Store H and L direct */
                [OpcodeBytes.LD_MNN_HL] = LD_MNN_HL,

                /** Load H and L direct */
                [OpcodeBytes.LD_HL_MNN] = LD_HL_MNN,

            #endregion

            #region Jump instructions

                /** Load program counter */
                [OpcodeBytes.JP_HL] = JP_HL,

                /** Jump */
                [OpcodeBytes.JP] = JP,

                /** Jump if parity odd */
                [OpcodeBytes.JP_PO] = JP_PO,

                /** Jump if parity even */
                [OpcodeBytes.JP_PE] = JP_PE,

                /** Jump if plus/positive */
                [OpcodeBytes.JP_P] = JP_P,

                /** Jump if zero */
                [OpcodeBytes.JP_Z] = JP_Z,

                /** Jump if not zero */
                [OpcodeBytes.JP_NZ] = JP_NZ,

                /** Jump if not carry */
                [OpcodeBytes.JP_NC] = JP_NC,

                /** Jump if carry */
                [OpcodeBytes.JP_C] = JP_C,

                /** Jump if minus/negative */
                [OpcodeBytes.JP_M] = JP_M,

                /** Relative jump */
                [OpcodeBytes.JR] = JR,

                /** Relative jump if zero */
                [OpcodeBytes.JR_Z] = JR_Z,

                /** Relative jump if not zero */
                [OpcodeBytes.JR_NZ] = JR_NZ,

                /** Relative jump if carry */
                [OpcodeBytes.JR_C] = JR_C,

                /** Relative jump if not carry */
                [OpcodeBytes.JR_NC] = JR_NC,

            #endregion

            #region Call subroutine instructions

                /** Call Subroutine */
                [OpcodeBytes.CALL] = CALL,

                /** Call if minus/negative */
                [OpcodeBytes.CALL_M] = CALL_M,

                /** Call if party even */
                [OpcodeBytes.CALL_PE] = CALL_PE,

                /** Call if carry */
                [OpcodeBytes.CALL_C] = CALL_C,

                /** Call if zero */
                [OpcodeBytes.CALL_Z] = CALL_Z,

                /** Call if plus/positive */
                [OpcodeBytes.CALL_P] = CALL_P,

                /** Call if party odd */
                [OpcodeBytes.CALL_PO] = CALL_PO,

                /** Call if no carry */
                [OpcodeBytes.CALL_NC] = CALL_NC,

                /** Call if not zero */
                [OpcodeBytes.CALL_NZ] = CALL_NZ,

            #endregion

            #region Return from subroutine instructions

                /** Return from subroutine */
                [OpcodeBytes.RET] = RET,

                /** Return from subroutine (duplicate) */
                [OpcodeBytes.RET2] = RET2,

                /** Return if not zero */
                [OpcodeBytes.RET_NZ] = RET_NZ,

                /** Return if zero */
                [OpcodeBytes.RET_Z] = RET_Z,

                /** Return if no carry */
                [OpcodeBytes.RET_NC] = RET_NC,

                /** Return if carry */
                [OpcodeBytes.RET_C] = RET_C,

                /** Return if parity odd */
                [OpcodeBytes.RET_PO] = RET_PO,

                /** Return if parity even */
                [OpcodeBytes.RET_PE] = RET_PE,

                /** Return if plus/positive */
                [OpcodeBytes.RET_P] = RET_P,

                /** Return if minus/negative */
                [OpcodeBytes.RET_M] = RET_M,


            #endregion

            #region Restart (interrupt handlers) instructions

                /** CALL $0 */
                [OpcodeBytes.RST_00] = RST_00,

                /** CALL $8 */
                [OpcodeBytes.RST_08] = RST_08,

                /** CALL $10 */
                [OpcodeBytes.RST_10] = RST_10,

                /** CALL $18 */
                [OpcodeBytes.RST_18] = RST_18,

                /** CALL $20 */
                [OpcodeBytes.RST_20] = RST_20,

                /** CALL $28 */
                [OpcodeBytes.RST_28] = RST_28,

                /** CALL $30 */
                [OpcodeBytes.RST_30] = RST_30,

                /** CALL $38 */
                [OpcodeBytes.RST_38] = RST_38,

            #endregion

            #region Interrupt flip-flop instructions

                /** Enable interrupts */
                [OpcodeBytes.EI] = EI,

                /** Disable interrupts */
                [OpcodeBytes.DI] = DI,

            #endregion

            #region Input/Output Instructions

                /** Output accumulator to given device number */
                [OpcodeBytes.OUT_MN_A] = OUT_MN_A,

                /** Retrieve input from given device number and populate accumulator */
                [OpcodeBytes.IN_A_MN] = IN_A_MN,

            #endregion
        };
    }
}
