
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

        // Colors based on default ANSI colors from Terminal.app
        private const string COLOR_WHITE = "{170,172,172}";
        private const string COLOR_BRIGHT_WHITE = "{229,229,229}";
        private const string COLOR_BRIGHT_RED = "{229,0,0}";
        private const string COLOR_GREEN = "{1,165,0}";
        private const string COLOR_BRIGHT_YELLOW = "{229,229,1}";
        private const string COLOR_YELLOW = "{152,153,1}";

        public static void Render(IntPtr surface, bool isDebuggerActive, bool isDebuggerSingleStepping, CPU cpu, bool showAnnotatedDisassembly)
        {
            SDL.SDL_SetRenderDrawColor(surface, 0, 0, 0, 255);
            SDL.SDL_RenderClear(surface);

            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}------------------------------[STATS]-------------------------------------------", 0, 0 * ROW_HEIGHT);

            var status = $"{COLOR_GREEN}Running";

            if (isDebuggerActive)
            {
                status = $"{COLOR_BRIGHT_RED}Breakpoint";
            }
            else if (isDebuggerSingleStepping)
            {
                status = $"{COLOR_BRIGHT_YELLOW}Stepping...";
            }

            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[CPU State]: {status}            {COLOR_BRIGHT_WHITE}[Cycles]:   {COLOR_WHITE}TODO            {COLOR_BRIGHT_WHITE}[Ave. FPS]: {COLOR_WHITE}TODO", 0, 2 * ROW_HEIGHT);

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

            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[PC]: {COLOR_WHITE}{pc}        {COLOR_BRIGHT_WHITE}[SP]: {COLOR_WHITE}{sp}", 0, 6 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[A]: {COLOR_WHITE}{regA}  {COLOR_BRIGHT_WHITE}[B]: {COLOR_WHITE}{regB}  {COLOR_BRIGHT_WHITE}[C]: {COLOR_WHITE}{regC}  {COLOR_BRIGHT_WHITE}[D]: {COLOR_WHITE}{regD}  {COLOR_BRIGHT_WHITE}[E]: {COLOR_WHITE}{regE}  {COLOR_BRIGHT_WHITE}[H]: {COLOR_WHITE}{regH}  {COLOR_BRIGHT_WHITE}[L]: {COLOR_WHITE}{regL}", 0, 8 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[DE]: {COLOR_WHITE}{regDE}   {COLOR_BRIGHT_WHITE}[HL]: {COLOR_WHITE}{regHL}   {COLOR_BRIGHT_WHITE}[(DE)]: {COLOR_WHITE}{memDE}   {COLOR_BRIGHT_WHITE}[(HL)]: {COLOR_WHITE}{memHL}   {COLOR_BRIGHT_WHITE}[Flags]: {COLOR_WHITE}{regF}", 0, 10 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[Sign]: {COLOR_WHITE}{sign}   {COLOR_BRIGHT_WHITE}[Zero]: {COLOR_WHITE}{zero}   {COLOR_BRIGHT_WHITE}[Parity/Overflow]: {COLOR_WHITE}{parityOverflow}   {COLOR_BRIGHT_WHITE}[Subtract]: {COLOR_WHITE}{subtract}", 0, 12 * ROW_HEIGHT);
            FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[Carry]: {COLOR_WHITE}{carry}   {COLOR_BRIGHT_WHITE}[Half-Carry]: {COLOR_WHITE}{halfCarry}", 0, 14 * ROW_HEIGHT);

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

                    // Convert tabs to spaces (there is no tab character in the font set, only 8x8 glyphs).
                    disassemblyLine = disassemblyLine.Replace("\t", "     ");

                    if (disassemblyLine.Contains(Disassembler.CURRENT_LINE_MARKER))
                        disassemblyLine = $"{COLOR_BRIGHT_WHITE}{disassemblyLine}";
                    else
                        disassemblyLine = $"{COLOR_WHITE}{disassemblyLine}";

                    FontRenderer.RenderString(surface, disassemblyLine, 0, (DISASSEMBLY_START_ROW + i) * ROW_HEIGHT);
                }
            }

            var commandsStartRow = (DISASSEMBLY_START_ROW + DIASSEMBLY_ROW_COUNT);

            FontRenderer.RenderString(surface, "------------------------------[COMMANDS]----------------------------------------", 0, (commandsStartRow + 1) * ROW_HEIGHT);

            if (isDebuggerActive)
            {
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[F1] {COLOR_WHITE}Save State     {COLOR_BRIGHT_WHITE}[F2] {COLOR_WHITE}Load State     {COLOR_BRIGHT_WHITE}[F4] {COLOR_WHITE}Edit Breakpoints", 0, (commandsStartRow + 3) * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[F5] {COLOR_WHITE}Continue       {COLOR_BRIGHT_WHITE}[F9] {COLOR_WHITE}Step Backwards {COLOR_BRIGHT_WHITE}[F10] {COLOR_WHITE}Single Step", 0, (commandsStartRow + 4) * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[F11] {COLOR_WHITE}Toggle Annotated Disassembly      {COLOR_BRIGHT_WHITE}[F12] {COLOR_WHITE}Print Last 12 Opcodes", 0, (commandsStartRow + 5) * ROW_HEIGHT);
            }
            else if (isDebuggerSingleStepping)
            {
                FontRenderer.RenderString(surface, $" {COLOR_BRIGHT_YELLOW}Single stepping; please wait...", 0, (commandsStartRow + 4) * ROW_HEIGHT);
            }
            else
            {
                FontRenderer.RenderString(surface, $" {COLOR_YELLOW}Press [BREAK], [PAUSE], or [9] to interrupt execution and start debugging...", 0, (commandsStartRow + 4) * ROW_HEIGHT);
            }

            FontRenderer.RenderString(surface, "--------------------------------------------------------------------------------", 0, (commandsStartRow + 7) * ROW_HEIGHT);

            SDL.SDL_RenderPresent(surface);
        }
    }
}
