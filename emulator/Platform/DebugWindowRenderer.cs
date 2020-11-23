
using System;
using JustinCredible.Z80Disassembler;
using SDL2;

namespace JustinCredible.PacEmu
{
    public static class DebugWindowRenderer
    {
        // The height of each row of characters (based on the 8x8 pixel font).
        private const int ROW_HEIGHT = 8;

        // Empty string place holder values for the different data fields.
        private const string EMPTY_8BIT_HEX_VALUE_DISPLAY = "--";
        private const string EMPTY_16BIT_HEX_VALUE_DISPLAY = "----";
        private const string EMPTY_BIT_VALUE_DISPLAY = "-";

        // The row at which the disassembly section starts and the number of the diassembly rows.
        private const int DISASSEMBLY_START_ROW = 18;
        private const int DIASSEMBLY_ROW_COUNT = 34;

        // Colors based on default ANSI colors from Terminal.app
        private const string COLOR_WHITE = "{170,172,172}";
        private const string COLOR_BRIGHT_WHITE = "{229,229,229}";
        private const string COLOR_BRIGHT_RED = "{229,0,0}";
        private const string COLOR_GREEN = "{1,165,0}";
        private const string COLOR_BRIGHT_GREEN = "{64,216,0}";
        private const string COLOR_BRIGHT_YELLOW = "{229,229,1}";
        private const string COLOR_YELLOW = "{152,153,1}";
        private const string COLOR_BLUE = "{25,12,178}";
        private const string COLOR_BRIGHT_BLUE = "{40,23,255}";

        public static void Render(IntPtr surface, DebuggerState state, string inputString, PacManPCB pcb, bool showAnnotatedDisassembly)
        {
            SDL.SDL_SetRenderDrawColor(surface, 0, 0, 0, 255);
            SDL.SDL_RenderClear(surface);

            if (state == DebuggerState.EditBreakpoints) // User editing breakpoints
            {
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}------------------------------[BREAKPOINTS]-------------------------------------", 0, 0 * ROW_HEIGHT);

                for (var i = 0; i < pcb.BreakAtAddresses.Count && i < 50; i++)
                {
                    var address = String.Format("0x{0:X4}", pcb.BreakAtAddresses[i]);
                    FontRenderer.RenderString(surface, $"{COLOR_WHITE} * {address}", 0, (i + 2) * ROW_HEIGHT);
                }

                if (pcb.BreakAtAddresses.Count >= 50)
                {
                    FontRenderer.RenderString(surface, $"   {COLOR_YELLOW}(only showing the first 50 of {pcb.BreakAtAddresses.Count} breakpoints)", 0, (50 + 2) * ROW_HEIGHT);
                }

                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}--------------------------------------------------------------------------------", 0, (50 + 3) * ROW_HEIGHT);

                FontRenderer.RenderString(surface, $"{COLOR_WHITE}Enter an address in hexidecimal format and press {COLOR_BRIGHT_GREEN}[ENTER] {COLOR_WHITE}to toggle a breakpoint.", 0, (50 + 4) * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_WHITE}Type \"{COLOR_BRIGHT_RED}clear{COLOR_WHITE}\" to remove all breakpoints. Press {COLOR_BRIGHT_YELLOW}[ESCAPE] {COLOR_WHITE}to abort.", 0, (50 + 5) * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"> {inputString}", 0, (50 + 7) * ROW_HEIGHT);

                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}--------------------------------------------------------------------------------", 0, (50 + 9) * ROW_HEIGHT);
            }
            else // Running, at a breakpoint, or stepping over a breakpoint.
            {
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}------------------------------[STATS]-------------------------------------------", 0, 0 * ROW_HEIGHT);

                var statusColor = $"{COLOR_GREEN}";
                var status = $"Running";
                var cycleCount = (state == DebuggerState.Breakpoint ? String.Format("{0:n0}", pcb._totalCycles) : "---").PadRight(13);
                var opcodeCount = (state == DebuggerState.Breakpoint ? String.Format("{0:n0}", pcb._totalOpcodes) : "---").PadRight(13);

                if (state == DebuggerState.Breakpoint)
                {
                    statusColor = $"{COLOR_BRIGHT_RED}";
                    status = "Breakpoint";
                }
                else if (state == DebuggerState.SingleStepping)
                {
                    statusColor = $"{COLOR_BRIGHT_YELLOW}";
                    status = "Stepping...";
                }

                status = status.PadRight(12);

                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[Status]: {statusColor}{status} {COLOR_BRIGHT_WHITE}[Cycles]: {COLOR_WHITE}{cycleCount}  {COLOR_BRIGHT_WHITE}[Opcodes]: {COLOR_WHITE}{opcodeCount}", 0, 2 * ROW_HEIGHT);

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

                if (state == DebuggerState.Breakpoint && pcb._cpu != null)
                {
                    pc = String.Format("{0:X4}", pcb._cpu.Registers.PC);
                    sp = String.Format("{0:X4}", pcb._cpu.Registers.SP);
                    regA = String.Format("{0:X2}", pcb._cpu.Registers.A);
                    regB = String.Format("{0:X2}", pcb._cpu.Registers.B);
                    regC = String.Format("{0:X2}", pcb._cpu.Registers.C);
                    regD = String.Format("{0:X2}", pcb._cpu.Registers.D);
                    regE = String.Format("{0:X2}", pcb._cpu.Registers.E);
                    regH = String.Format("{0:X2}", pcb._cpu.Registers.H);
                    regL = String.Format("{0:X2}", pcb._cpu.Registers.L);
                    regDE = String.Format("{0:X4}", pcb._cpu.Registers.DE);
                    regHL = String.Format("{0:X4}", pcb._cpu.Registers.HL);
                    regIX = String.Format("{0:X2}", pcb._cpu.Registers.IX);
                    regIY = String.Format("{0:X2}", pcb._cpu.Registers.IY);
                    regF = String.Format("{0:X2}", pcb._cpu.Flags.ToByte());

                    try {
                        var value = pcb._cpu.Memory.Read(pcb._cpu.Registers.DE);
                        memDE = String.Format("{0:X2}", value);
                    }
                    catch {
                        memDE = "N/A ";
                    }

                    try {
                        var value = pcb._cpu.Memory.Read(pcb._cpu.Registers.HL);
                        memHL = String.Format("{0:X2}", value);
                    }
                    catch {
                        memDE = "N/A ";
                    }

                    sign = pcb._cpu.Flags.Sign ? "1" : "0";
                    zero = pcb._cpu.Flags.Zero ? "1" : "0";
                    parityOverflow = pcb._cpu.Flags.ParityOverflow ? "1" : "0";
                    subtract = pcb._cpu.Flags.Subtract ? "1" : "0";
                    carry = pcb._cpu.Flags.Carry ? "1" : "0";
                    halfCarry = pcb._cpu.Flags.HalfCarry ? "1" : "0";
                }

                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[PC]: {COLOR_WHITE}{pc}        {COLOR_BRIGHT_WHITE}[SP]: {COLOR_WHITE}{sp}        {COLOR_BRIGHT_WHITE}[Flags]: {COLOR_WHITE}{regF}", 0, 6 * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[A]: {COLOR_WHITE}{regA}  {COLOR_BRIGHT_WHITE}[B]: {COLOR_WHITE}{regB}  {COLOR_BRIGHT_WHITE}[C]: {COLOR_WHITE}{regC}  {COLOR_BRIGHT_WHITE}[D]: {COLOR_WHITE}{regD}  {COLOR_BRIGHT_WHITE}[E]: {COLOR_WHITE}{regE}  {COLOR_BRIGHT_WHITE}[H]: {COLOR_WHITE}{regH}  {COLOR_BRIGHT_WHITE}[L]: {COLOR_WHITE}{regL}", 0, 8 * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[DE]: {COLOR_WHITE}{regDE}        {COLOR_BRIGHT_WHITE}[HL]: {COLOR_WHITE}{regHL}        {COLOR_BRIGHT_WHITE}[(DE)]: {COLOR_WHITE}{memDE}        {COLOR_BRIGHT_WHITE}[(HL)]: {COLOR_WHITE}{memHL}", 0, 10 * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[Sign]: {COLOR_WHITE}{sign}         {COLOR_BRIGHT_WHITE}[Zero]: {COLOR_WHITE}{zero}         {COLOR_BRIGHT_WHITE}[Parity/Overflow]: {COLOR_WHITE}{parityOverflow}", 0, 12 * ROW_HEIGHT);
                FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[Subtract]: {COLOR_WHITE}{subtract}     {COLOR_BRIGHT_WHITE}[Carry]: {COLOR_WHITE}{carry}        {COLOR_BRIGHT_WHITE}[Half-Carry]: {COLOR_WHITE}{halfCarry}", 0, 14 * ROW_HEIGHT);

                FontRenderer.RenderString(surface, "------------------------------[DISASSEMBLY]-------------------------------------", 0, 16 * ROW_HEIGHT);

                if (state == DebuggerState.Breakpoint)
                {
                    var disassembly = Disassembler.FormatDisassemblyForDisplay(pcb._cpu.Registers.PC, pcb._cpu.Memory, 16, 16, showAnnotatedDisassembly ? pcb.Annotations : null);

                    var disassemblyLines = disassembly.Split(Environment.NewLine);

                    for (var i = 0; i < DIASSEMBLY_ROW_COUNT; i++)
                    {
                        if (i >= disassemblyLines.Length)
                            continue;

                        // Double tabs followed by a semi-colon indicate a split between the address/instruction disassembly
                        // and the generated psuedocode or annotations.
                        var disassemblyLineParts = disassemblyLines[i].Split("\t\t; ");
                        var disassemblyInstruction = disassemblyLineParts[0];
                        var disassemblyComments = disassemblyLineParts.Length > 1 ? disassemblyLineParts[1] : "";

                        // Convert tabs to spaces (there is no tab character in the font set, only 8x8 glyphs).
                        disassemblyInstruction = disassemblyInstruction.Replace("\t", "     ");

                        var instructionColor = $"{COLOR_WHITE}";
                        var commentColor = $"{COLOR_BLUE}";

                        if (disassemblyInstruction.Contains(Disassembler.CURRENT_LINE_MARKER))
                        {
                            instructionColor = $"{COLOR_BRIGHT_WHITE}";

                            if (showAnnotatedDisassembly)
                            {
                                commentColor = $"{COLOR_BRIGHT_GREEN}";
                            }
                            else
                            {
                                commentColor = $"{COLOR_BRIGHT_BLUE}";
                            }
                        }
                        else
                        {
                            if (showAnnotatedDisassembly)
                            {
                                commentColor = $"{COLOR_GREEN}";
                            }
                        }

                        disassemblyInstruction = disassemblyInstruction.PadRight(30);
                        var line = $"{instructionColor}{disassemblyInstruction} {commentColor};{disassemblyComments}";

                        FontRenderer.RenderString(surface, line, 0, (DISASSEMBLY_START_ROW + i) * ROW_HEIGHT);
                    }
                }

                var commandsStartRow = (DISASSEMBLY_START_ROW + DIASSEMBLY_ROW_COUNT);

                FontRenderer.RenderString(surface, "------------------------------[COMMANDS]----------------------------------------", 0, (commandsStartRow + 1) * ROW_HEIGHT);

                if (state == DebuggerState.Breakpoint)
                {
                    var stepBackwards = pcb.ReverseStepEnabled ? $"{COLOR_BRIGHT_WHITE}[F9] {COLOR_WHITE}Step Backwards" : "                   ";

                    FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[F1] {COLOR_WHITE}Save State     {COLOR_BRIGHT_WHITE}[F2] {COLOR_WHITE}Load State     {COLOR_BRIGHT_WHITE}[F4] {COLOR_WHITE}Edit Breakpoints", 0, (commandsStartRow + 3) * ROW_HEIGHT);
                    FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[F5] {COLOR_WHITE}Continue       {stepBackwards} {COLOR_BRIGHT_WHITE}[F10] {COLOR_WHITE}Single Step", 0, (commandsStartRow + 4) * ROW_HEIGHT);
                    FontRenderer.RenderString(surface, $"{COLOR_BRIGHT_WHITE}[F11] {COLOR_WHITE}Toggle Annotated Disassembly      {COLOR_BRIGHT_WHITE}[F12] {COLOR_WHITE}Print Last 12 Opcodes", 0, (commandsStartRow + 5) * ROW_HEIGHT);
                }
                else if (state == DebuggerState.SingleStepping)
                {
                    FontRenderer.RenderString(surface, $" {COLOR_BRIGHT_YELLOW}Single stepping; please wait...", 0, (commandsStartRow + 4) * ROW_HEIGHT);
                }
                else
                {
                    FontRenderer.RenderString(surface, $" {COLOR_YELLOW}Press [BREAK], [PAUSE], or [F3] to interrupt execution and start debugging...", 0, (commandsStartRow + 4) * ROW_HEIGHT);
                }

                FontRenderer.RenderString(surface, "--------------------------------------------------------------------------------", 0, (commandsStartRow + 7) * ROW_HEIGHT);
            }

            SDL.SDL_RenderPresent(surface);
        }
    }
}
