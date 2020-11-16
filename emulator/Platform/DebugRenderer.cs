
using System;
using JustinCredible.Z80Disassembler;
using JustinCredible.ZilogZ80;
using SDL2;

namespace JustinCredible.PacEmu
{
    public static class DebugRenderer
    {
        // The height of each row of characters (based on the 8x8 pixel font).
        private const int ROW_HEIGHT = 8;

        // Empty string place holder values for the different data fields.
        private const string EMPTY_8BIT_HEX_VALUE_DISPLAY = "    ";
        private const string EMPTY_16BIT_HEX_VALUE_DISPLAY = "      ";
        private const string EMPTY_BIT_VALUE_DISPLAY = " ";

        // The row at which the disassembly section starts and the number of the diassembly rows.
        private const int DISASSEMBLY_START_ROW = 18;
        private const int DIASSEMBLY_ROW_COUNT = 34;

        public static void Render(IntPtr surface, bool isDebuggerActive, CPU cpu, bool showAnnotatedDisassembly)
        {
            SDL.SDL_SetRenderDrawColor(surface, 0, 0, 0, 255);
            SDL.SDL_RenderClear(surface);

            FontRenderer.RenderString(surface, "------------------------------[STATS]-------------------------------------------", 0, 0 * ROW_HEIGHT);

            if (isDebuggerActive)
            {
                FontRenderer.RenderString(surface, "[CPU State]: Breakpoint         [Cycles]:   TODO            [Ave. FPS]: TODO", 0, 2 * ROW_HEIGHT);
            }
            else
            {
                FontRenderer.RenderString(surface, "[CPU State]: Running            [Cycles]:   TODO            [Ave. FPS]: TODO", 0, 2 * ROW_HEIGHT);
            }

            FontRenderer.RenderString(surface, "------------------------------[CPU STATE]---------------------------------------", 0, 4 * ROW_HEIGHT);

            var pc = EMPTY_16BIT_HEX_VALUE_DISPLAY;
            var sp = EMPTY_16BIT_HEX_VALUE_DISPLAY;
            var regA = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regB = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regC = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regD = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regE = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regH = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regL = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regDE = EMPTY_16BIT_HEX_VALUE_DISPLAY;
            var regHL = EMPTY_16BIT_HEX_VALUE_DISPLAY;
            var regIX = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regIY = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var regF = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var memDE = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var memHL = EMPTY_8BIT_HEX_VALUE_DISPLAY;
            var sign = EMPTY_BIT_VALUE_DISPLAY;
            var zero = EMPTY_BIT_VALUE_DISPLAY;
            var parityOverflow = EMPTY_BIT_VALUE_DISPLAY;
            var subtract = EMPTY_BIT_VALUE_DISPLAY;
            var carry = EMPTY_BIT_VALUE_DISPLAY;
            var halfCarry = EMPTY_BIT_VALUE_DISPLAY;

            if (isDebuggerActive && cpu != null)
            {
                pc = String.Format("0x{0:X4}", cpu.Registers.PC);
                sp = String.Format("0x{0:X4}", cpu.Registers.SP);
                regA = String.Format("0x{0:X2}", cpu.Registers.A);
                regB = String.Format("0x{0:X2}", cpu.Registers.B);
                regC = String.Format("0x{0:X2}", cpu.Registers.C);
                regD = String.Format("0x{0:X2}", cpu.Registers.D);
                regE = String.Format("0x{0:X2}", cpu.Registers.E);
                regH = String.Format("0x{0:X2}", cpu.Registers.H);
                regL = String.Format("0x{0:X2}", cpu.Registers.L);
                regDE = String.Format("0x{0:X4}", cpu.Registers.DE);
                regHL = String.Format("0x{0:X4}", cpu.Registers.HL);
                regIX = String.Format("0x{0:X2}", cpu.Registers.IX);
                regIY = String.Format("0x{0:X2}", cpu.Registers.IY);
                regF = String.Format("0x{0:X2}", cpu.Flags.ToByte());

                try {
                    var value = cpu.Memory.Read(cpu.Registers.DE);
                    memDE = String.Format("0x{0:X2}", value);
                }
                catch {
                    memDE = "N/A ";
                }

                try {
                    var value = cpu.Memory.Read(cpu.Registers.HL);
                    memHL = String.Format("0x{0:X2}", value);
                }
                catch {
                    memDE = "N/A ";
                }

                sign = cpu.Flags.Sign ? "1" : "0";
                zero = cpu.Flags.Zero ? "1" : "0";
                parityOverflow = cpu.Flags.ParityOverflow ? "1" : "0";
                subtract = cpu.Flags.Subtract ? "1" : "0";
                carry = cpu.Flags.Carry ? "1" : "0";
                halfCarry = cpu.Flags.HalfCarry ? "1" : "0";
            }

            FontRenderer.RenderString(surface, $"[PC]: {pc}        [SP]: {sp}", 0, 6 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"[A]: {regA}  [B]: {regB}  [C]: {regC}  [D]: {regD}  [E]: {regE}  [H]: {regH}  [L]: {regL}", 0, 8 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"[DE]: {regDE}   [HL]: {regHL}   [(DE)]: {memDE}   [(HL)]: {memHL}   [Flags]: {regF}", 0, 10 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"[Sign]: {sign}   [Zero]: {zero}   [Parity/Overflow]: {parityOverflow}   [Subtract]: {subtract}", 0, 12 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"[Carry]: {carry}   [Half-Carry]: {halfCarry}", 0, 14 * ROW_HEIGHT);

            FontRenderer.RenderString(surface, "------------------------------[DISASSEMBLY]-------------------------------------", 0, 16 * ROW_HEIGHT);

            if (isDebuggerActive)
            {
                // TODO: Pass annotations through.
                var disassembly = Disassembler.FormatDisassemblyForDisplay(cpu.Registers.PC, cpu.Memory, 16, 16, true, null);

                var disassemblyLines = disassembly.Split(Environment.NewLine);

                for (var i = 0; i < DIASSEMBLY_ROW_COUNT; i++)
                {
                    if (i >= disassemblyLines.Length)
                        continue;

                    var disassemblyLine = disassemblyLines[i];
                    disassemblyLine = disassemblyLine.Replace("\t", "     ");

                    FontRenderer.RenderString(surface, disassemblyLine, 0, (DISASSEMBLY_START_ROW + i) * ROW_HEIGHT);
                }
            }

            var commandsStartRow = (DISASSEMBLY_START_ROW + DIASSEMBLY_ROW_COUNT);

            FontRenderer.RenderString(surface, "------------------------------[COMMANDS]----------------------------------------", 0, (commandsStartRow + 1) * ROW_HEIGHT);

            if (isDebuggerActive)
            {
                FontRenderer.RenderString(surface, "F1 = Save State     F2 = Load State     F4 = Edit Breakpoints", 0, (commandsStartRow + 3) * ROW_HEIGHT);
                FontRenderer.RenderString(surface, "F5 = Continue       F9 = Step Backwards F10 = Single Step", 0, (commandsStartRow + 4) * ROW_HEIGHT);
                FontRenderer.RenderString(surface, "F11 = Toggle Annotated Disassembly      F12 = Print Last 12 Opcodes", 0, (commandsStartRow + 5) * ROW_HEIGHT);
            }
            else
            {
                FontRenderer.RenderString(surface, " Press [BREAK], [PAUSE], or [9] to interrupt execution and start debugging...", 0, (commandsStartRow + 4) * ROW_HEIGHT);
            }

            FontRenderer.RenderString(surface, "--------------------------------------------------------------------------------", 0, (commandsStartRow + 7) * ROW_HEIGHT);

            SDL.SDL_RenderPresent(surface);
        }
    }
}
