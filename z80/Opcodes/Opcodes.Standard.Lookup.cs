
using System.Collections.Generic;

namespace JustinCredible.ZilogZ80
{
    // A lookup table to locate opcode definitions by the opcode byte.
    public partial class Opcodes
    {
        public static Dictionary<byte, Opcode> Lookup = new Dictionary<byte, Opcode>()
        {
            /** Halt */
            [OpcodeBytes.HALT] = HALT,
    
            #region NOP - No operation
                [OpcodeBytes.NOP] = NOP,
                [OpcodeBytes.NOP2] = NOP2,
                [OpcodeBytes.NOP3] = NOP3,
                [OpcodeBytes.NOP4] = NOP4,
                [OpcodeBytes.NOP5] = NOP5,
                [OpcodeBytes.NOP6] = NOP6,
                [OpcodeBytes.NOP7] = NOP7,
                [OpcodeBytes.NOP8] = NOP8,
            #endregion

            #region Carry bit instructions

                /** Set Carry */
                [OpcodeBytes.STC] = STC,

                /** Complement Carry */
                [OpcodeBytes.CMC] = CMC,

            #endregion

            #region Single register instructions

            #region INR - Increment Register or Memory
                [OpcodeBytes.INR_B] = INR_B,
                [OpcodeBytes.INR_C] = INR_C,
                [OpcodeBytes.INR_D] = INR_D,
                [OpcodeBytes.INR_E] = INR_E,
                [OpcodeBytes.INR_H] = INR_H,
                [OpcodeBytes.INR_L] = INR_L,
                [OpcodeBytes.INR_M] = INR_M,
                [OpcodeBytes.INR_A] = INR_A,
            #endregion

            #region DCR - Decrement Register or Memory
                [OpcodeBytes.DCR_B] = DCR_B,
                [OpcodeBytes.DCR_C] = DCR_C,
                [OpcodeBytes.DCR_D] = DCR_D,
                [OpcodeBytes.DCR_E] = DCR_E,
                [OpcodeBytes.DCR_H] = DCR_H,
                [OpcodeBytes.DCR_L] = DCR_L,
                [OpcodeBytes.DCR_M] = DCR_M,
                [OpcodeBytes.DCR_A] = DCR_A,
            #endregion

            /** Compliment Accumulator */
            [OpcodeBytes.CMA] = CMA,

            /** Decimal Adjust Accumulator */
            [OpcodeBytes.DAA] = DAA,

            #endregion

            #region Data transfer instructions

                #region STAX - Store accumulator
                    [OpcodeBytes.STAX_B] = STAX_B,
                    [OpcodeBytes.STAX_D] = STAX_D,
                #endregion

                #region LDAX - Load accumulator
                    [OpcodeBytes.LDAX_B] = LDAX_B,
                    [OpcodeBytes.LDAX_D] = LDAX_D,
                #endregion

                #region MOV - Move (copy) data
                    [OpcodeBytes.MOV_B_B] = MOV_B_B,
                    [OpcodeBytes.MOV_B_C] = MOV_B_C,
                    [OpcodeBytes.MOV_B_D] = MOV_B_D,
                    [OpcodeBytes.MOV_B_E] = MOV_B_E,
                    [OpcodeBytes.MOV_B_H] = MOV_B_H,
                    [OpcodeBytes.MOV_B_L] = MOV_B_L,
                    [OpcodeBytes.MOV_B_M] = MOV_B_M,
                    [OpcodeBytes.MOV_B_A] = MOV_B_A,
                    [OpcodeBytes.MOV_C_B] = MOV_C_B,
                    [OpcodeBytes.MOV_C_C] = MOV_C_C,
                    [OpcodeBytes.MOV_C_D] = MOV_C_D,
                    [OpcodeBytes.MOV_C_E] = MOV_C_E,
                    [OpcodeBytes.MOV_C_H] = MOV_C_H,
                    [OpcodeBytes.MOV_C_L] = MOV_C_L,
                    [OpcodeBytes.MOV_C_M] = MOV_C_M,
                    [OpcodeBytes.MOV_C_A] = MOV_C_A,
                    [OpcodeBytes.MOV_D_B] = MOV_D_B,
                    [OpcodeBytes.MOV_D_C] = MOV_D_C,
                    [OpcodeBytes.MOV_D_D] = MOV_D_D,
                    [OpcodeBytes.MOV_D_E] = MOV_D_E,
                    [OpcodeBytes.MOV_D_H] = MOV_D_H,
                    [OpcodeBytes.MOV_D_L] = MOV_D_L,
                    [OpcodeBytes.MOV_D_M] = MOV_D_M,
                    [OpcodeBytes.MOV_D_A] = MOV_D_A,
                    [OpcodeBytes.MOV_E_B] = MOV_E_B,
                    [OpcodeBytes.MOV_E_C] = MOV_E_C,
                    [OpcodeBytes.MOV_E_D] = MOV_E_D,
                    [OpcodeBytes.MOV_E_E] = MOV_E_E,
                    [OpcodeBytes.MOV_E_H] = MOV_E_H,
                    [OpcodeBytes.MOV_E_L] = MOV_E_L,
                    [OpcodeBytes.MOV_E_M] = MOV_E_M,
                    [OpcodeBytes.MOV_E_A] = MOV_E_A,
                    [OpcodeBytes.MOV_H_B] = MOV_H_B,
                    [OpcodeBytes.MOV_H_C] = MOV_H_C,
                    [OpcodeBytes.MOV_H_D] = MOV_H_D,
                    [OpcodeBytes.MOV_H_E] = MOV_H_E,
                    [OpcodeBytes.MOV_H_H] = MOV_H_H,
                    [OpcodeBytes.MOV_H_L] = MOV_H_L,
                    [OpcodeBytes.MOV_H_M] = MOV_H_M,
                    [OpcodeBytes.MOV_H_A] = MOV_H_A,
                    [OpcodeBytes.MOV_L_B] = MOV_L_B,
                    [OpcodeBytes.MOV_L_C] = MOV_L_C,
                    [OpcodeBytes.MOV_L_D] = MOV_L_D,
                    [OpcodeBytes.MOV_L_E] = MOV_L_E,
                    [OpcodeBytes.MOV_L_H] = MOV_L_H,
                    [OpcodeBytes.MOV_L_L] = MOV_L_L,
                    [OpcodeBytes.MOV_L_M] = MOV_L_M,
                    [OpcodeBytes.MOV_L_A] = MOV_L_A,
                    [OpcodeBytes.MOV_M_B] = MOV_M_B,
                    [OpcodeBytes.MOV_M_C] = MOV_M_C,
                    [OpcodeBytes.MOV_M_D] = MOV_M_D,
                    [OpcodeBytes.MOV_M_E] = MOV_M_E,
                    [OpcodeBytes.MOV_M_H] = MOV_M_H,
                    [OpcodeBytes.MOV_M_L] = MOV_M_L,
                    [OpcodeBytes.MOV_M_A] = MOV_M_A,
                    [OpcodeBytes.MOV_A_B] = MOV_A_B,
                    [OpcodeBytes.MOV_A_C] = MOV_A_C,
                    [OpcodeBytes.MOV_A_D] = MOV_A_D,
                    [OpcodeBytes.MOV_A_E] = MOV_A_E,
                    [OpcodeBytes.MOV_A_H] = MOV_A_H,
                    [OpcodeBytes.MOV_A_L] = MOV_A_L,
                    [OpcodeBytes.MOV_A_M] = MOV_A_M,
                    [OpcodeBytes.MOV_A_A] = MOV_A_A,
                #endregion

            #endregion

            #region Register or memory to accumulator instructions

                #region ADD - Add register or memory to accumulator
                    [OpcodeBytes.ADD_B] = ADD_B,
                    [OpcodeBytes.ADD_C] = ADD_C,
                    [OpcodeBytes.ADD_D] = ADD_D,
                    [OpcodeBytes.ADD_E] = ADD_E,
                    [OpcodeBytes.ADD_H] = ADD_H,
                    [OpcodeBytes.ADD_L] = ADD_L,
                    [OpcodeBytes.ADD_M] = ADD_M,
                    [OpcodeBytes.ADD_A] = ADD_A,
                #endregion

                #region SUB - Subtract register or memory from accumulator
                    [OpcodeBytes.SUB_B] = SUB_B,
                    [OpcodeBytes.SUB_C] = SUB_C,
                    [OpcodeBytes.SUB_D] = SUB_D,
                    [OpcodeBytes.SUB_E] = SUB_E,
                    [OpcodeBytes.SUB_H] = SUB_H,
                    [OpcodeBytes.SUB_L] = SUB_L,
                    [OpcodeBytes.SUB_M] = SUB_M,
                    [OpcodeBytes.SUB_A] = SUB_A,
                #endregion

                #region ANA - Logical AND register or memory with accumulator
                    [OpcodeBytes.ANA_B] = ANA_B,
                    [OpcodeBytes.ANA_C] = ANA_C,
                    [OpcodeBytes.ANA_D] = ANA_D,
                    [OpcodeBytes.ANA_E] = ANA_E,
                    [OpcodeBytes.ANA_H] = ANA_H,
                    [OpcodeBytes.ANA_L] = ANA_L,
                    [OpcodeBytes.ANA_M] = ANA_M,
                    [OpcodeBytes.ANA_A] = ANA_A,
                #endregion

                #region ORA - Logical OR register or memory with accumulator
                    [OpcodeBytes.ORA_B] = ORA_B,
                    [OpcodeBytes.ORA_C] = ORA_C,
                    [OpcodeBytes.ORA_D] = ORA_D,
                    [OpcodeBytes.ORA_E] = ORA_E,
                    [OpcodeBytes.ORA_H] = ORA_H,
                    [OpcodeBytes.ORA_L] = ORA_L,
                    [OpcodeBytes.ORA_M] = ORA_M,
                    [OpcodeBytes.ORA_A] = ORA_A,
                #endregion

                #region ADC - Add register or memory to accumulator with carry
                    [OpcodeBytes.ADC_B] = ADC_B,
                    [OpcodeBytes.ADC_C] = ADC_C,
                    [OpcodeBytes.ADC_D] = ADC_D,
                    [OpcodeBytes.ADC_E] = ADC_E,
                    [OpcodeBytes.ADC_H] = ADC_H,
                    [OpcodeBytes.ADC_L] = ADC_L,
                    [OpcodeBytes.ADC_M] = ADC_M,
                    [OpcodeBytes.ADC_A] = ADC_A,
                #endregion

                #region SBB - Subtract register or memory from accumulator with borrow
                    [OpcodeBytes.SBB_B] = SBB_B,
                    [OpcodeBytes.SBB_C] = SBB_C,
                    [OpcodeBytes.SBB_D] = SBB_D,
                    [OpcodeBytes.SBB_E] = SBB_E,
                    [OpcodeBytes.SBB_H] = SBB_H,
                    [OpcodeBytes.SBB_L] = SBB_L,
                    [OpcodeBytes.SBB_M] = SBB_M,
                    [OpcodeBytes.SBB_A] = SBB_A,
                #endregion

                #region XRA - Logical XOR register or memory with accumulator
                    [OpcodeBytes.XRA_B] = XRA_B,
                    [OpcodeBytes.XRA_C] = XRA_C,
                    [OpcodeBytes.XRA_D] = XRA_D,
                    [OpcodeBytes.XRA_E] = XRA_E,
                    [OpcodeBytes.XRA_H] = XRA_H,
                    [OpcodeBytes.XRA_L] = XRA_L,
                    [OpcodeBytes.XRA_M] = XRA_M,
                    [OpcodeBytes.XRA_A] = XRA_A,
                #endregion

                #region CMP - Compare register or memory with accumulator
                    [OpcodeBytes.CMP_B] = CMP_B,
                    [OpcodeBytes.CMP_C] = CMP_C,
                    [OpcodeBytes.CMP_D] = CMP_D,
                    [OpcodeBytes.CMP_E] = CMP_E,
                    [OpcodeBytes.CMP_H] = CMP_H,
                    [OpcodeBytes.CMP_L] = CMP_L,
                    [OpcodeBytes.CMP_M] = CMP_M,
                    [OpcodeBytes.CMP_A] = CMP_A,
                #endregion

            #endregion

            #region Rotate accumulator instructions

                /** Rotate accumulator left */
                [OpcodeBytes.RLC] = RLC,

                /** Rotate accumulator right */
                [OpcodeBytes.RRC] = RRC,

                /** Rotate accumulator left through carry */
                [OpcodeBytes.RAL] = RAL,

                /** Rotate accumulator right through carry */
                [OpcodeBytes.RAR] = RAR,

            #endregion

            #region Register pair instructions

                #region INX - Increment register pair
                    [OpcodeBytes.INX_B] = INX_B,
                    [OpcodeBytes.INX_D] = INX_D,
                    [OpcodeBytes.INX_H] = INX_H,
                    [OpcodeBytes.INX_SP] = INX_SP,
                #endregion

                #region DCX - Decrement register pair
                    [OpcodeBytes.DCX_B] = DCX_B,
                    [OpcodeBytes.DCX_D] = DCX_D,
                    [OpcodeBytes.DCX_H] = DCX_H,
                    [OpcodeBytes.DCX_SP] = DCX_SP,
                #endregion

                #region PUSH - Push data onto the stack
                    [OpcodeBytes.PUSH_B] = PUSH_B,
                    [OpcodeBytes.PUSH_D] = PUSH_D,
                    [OpcodeBytes.PUSH_H] = PUSH_H,
                    [OpcodeBytes.PUSH_PSW] = PUSH_PSW,
                #endregion

                #region POP - Pop data off of the stack
                    [OpcodeBytes.POP_B] = POP_B,
                    [OpcodeBytes.POP_D] = POP_D,
                    [OpcodeBytes.POP_H] = POP_H,
                    [OpcodeBytes.POP_PSW] = POP_PSW,
                #endregion

                #region DAD - Double (16-bit) add
                    [OpcodeBytes.DAD_B] = DAD_B,
                    [OpcodeBytes.DAD_D] = DAD_D,
                    [OpcodeBytes.DAD_H] = DAD_H,
                    [OpcodeBytes.DAD_SP] = DAD_SP,
                #endregion

                /** Load SP from H and L */
                [OpcodeBytes.SPHL] = SPHL,

                /** Exchange stack */
                [OpcodeBytes.XTHL] = XTHL,

                /** Exchange registers */
                [OpcodeBytes.XCHG] = XCHG,

            #endregion

            #region Immediate instructions

                #region MVI - Move immediate data
                    [OpcodeBytes.MVI_B] = MVI_B,
                    [OpcodeBytes.MVI_C] = MVI_C,
                    [OpcodeBytes.MVI_D] = MVI_D,
                    [OpcodeBytes.MVI_E] = MVI_E,
                    [OpcodeBytes.MVI_H] = MVI_H,
                    [OpcodeBytes.MVI_L] = MVI_L,
                    [OpcodeBytes.MVI_M] = MVI_M,
                    [OpcodeBytes.MVI_A] = MVI_A,
                #endregion

                #region LXI - Load register pair immediate
                    [OpcodeBytes.LXI_B] = LXI_B,
                    [OpcodeBytes.LXI_D] = LXI_D,
                    [OpcodeBytes.LXI_H] = LXI_H,
                    [OpcodeBytes.LXI_SP] = LXI_SP,
                #endregion

                /** Add immediate to accumulator */
                [OpcodeBytes.ADI] = ADI,

                /** Add immediate to accumulator with carry */
                [OpcodeBytes.ACI] = ACI,

                /** Subtract immediate from accumulator */
                [OpcodeBytes.SUI] = SUI,

                /** Subtract immediate from accumulator with borrow */
                [OpcodeBytes.SBI] = SBI,

                /** Logical AND immediate with accumulator */
                [OpcodeBytes.ANI] = ANI,

                /** XOR immediate with accumulator */
                [OpcodeBytes.XRI] = XRI,

                /** Logical OR immediate with accumulator */
                [OpcodeBytes.ORI] = ORI,

                /** Compare immediate with accumulator */
                [OpcodeBytes.CPI] = CPI,

            #endregion

            #region Direct addressing instructions

                /** Store accumulator direct */
                [OpcodeBytes.STA] = STA,

                /** Load accumulator direct */
                [OpcodeBytes.LDA] = LDA,

                /** Store H and L direct */
                [OpcodeBytes.SHLD] = SHLD,

                /** Load H and L direct */
                [OpcodeBytes.LHLD] = LHLD,

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

            #endregion

            #region Call subroutine instructions

                /** Call Subroutine */
                [OpcodeBytes.CALL] = CALL,

                /** Call if minus/negative */
                [OpcodeBytes.CM] = CM,

                /** Call if party even */
                [OpcodeBytes.CPE] = CPE,

                /** Call if carry */
                [OpcodeBytes.CC] = CC,

                /** Call if zero */
                [OpcodeBytes.CZ] = CZ,

                /** Call if plus/positive */
                [OpcodeBytes.CP] = CP,

                /** Call if party odd */
                [OpcodeBytes.CPO] = CPO,

                /** Call if no carry */
                [OpcodeBytes.CNC] = CNC,

                /** Call if not zero */
                [OpcodeBytes.CNZ] = CNZ,

            #endregion

            #region Return from subroutine instructions

                /** Return from subroutine */
                [OpcodeBytes.RET] = RET,

                /** Return from subroutine (duplicate) */
                [OpcodeBytes.RET2] = RET2,

                /** Return if not zero */
                [OpcodeBytes.RNZ] = RNZ,

                /** Return if zero */
                [OpcodeBytes.RZ] = RZ,

                /** Return if no carry */
                [OpcodeBytes.RNC] = RNC,

                /** Return if carry */
                [OpcodeBytes.RC] = RC,

                /** Return if parity odd */
                [OpcodeBytes.RPO] = RPO,

                /** Return if parity even */
                [OpcodeBytes.RPE] = RPE,

                /** Return if plus/positive */
                [OpcodeBytes.RP] = RP,

                /** Return if minus/negative */
                [OpcodeBytes.RM] = RM,


            #endregion

            #region Restart (interrupt handlers) instructions

                /** CALL $0 */
                [OpcodeBytes.RST_0] = RST_0,

                /** CALL $8 */
                [OpcodeBytes.RST_1] = RST_1,

                /** CALL $10 */
                [OpcodeBytes.RST_2] = RST_2,

                /** CALL $18 */
                [OpcodeBytes.RST_3] = RST_3,

                /** CALL $20 */
                [OpcodeBytes.RST_4] = RST_4,

                /** CALL $28 */
                [OpcodeBytes.RST_5] = RST_5,

                /** CALL $30 */
                [OpcodeBytes.RST_6] = RST_6,

                /** CALL $38 */
                [OpcodeBytes.RST_7] = RST_7,

            #endregion

            #region Interrupt flip-flop instructions

                /** Enable interrupts */
                [OpcodeBytes.EI] = EI,

                /** Disable interrupts */
                [OpcodeBytes.DI] = DI,

            #endregion

            #region Input/Output Instructions

                /** Output accumulator to given device number */
                [OpcodeBytes.OUT] = OUT,

                /** Retrieve input from given device number and populate accumulator */
                [OpcodeBytes.IN] = IN,

            #endregion
        };
    }
}
