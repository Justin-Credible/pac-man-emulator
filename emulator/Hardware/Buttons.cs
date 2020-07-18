using System;
using SDL2;

namespace JustinCredible.PacEmu
{
    /**
     * Holds the state of the buttons and switches.
     * True = pressed / closed / on
     * False = not pressed / open / off
     */
    public class Buttons
    {
        // Player 1
        public bool P1Left { get; set; } = false;
        public bool P1Right { get; set; } = false;
        public bool P1Up { get; set; } = false;
        public bool P1Down { get; set; } = false;
        public bool P1Start { get; set; } = false;

        // Player 2
        public bool P2Left { get; set; } = false;
        public bool P2Right { get; set; } = false;
        public bool P2Up { get; set; } = false;
        public bool P2Down { get; set; } = false;
        public bool P2Start { get; set; } = false;

        // Coins
        public bool CoinChute1 { get; set; } = false;
        public bool CoinChute2 { get; set; } = false;

        // Servicing
        public bool ServiceCredit { get; set; } = false;
        public bool ServiceRackAdvance { get; set; } = false;
        public bool ServiceBoardTest { get; set; } = false;

        public void SetState(Buttons state)
        {
            // Player 1
            P1Left = state.P1Left;
            P1Right = state.P1Right;
            P1Up = state.P1Up;
            P1Down = state.P1Down;
            P1Start = state.P1Start;

            // Player 2
            P2Left = state.P2Left;
            P2Right = state.P2Right;
            P2Up = state.P2Up;
            P2Down = state.P2Down;
            P2Start = state.P2Start;

            // Coins
            CoinChute1 = state.CoinChute1;
            CoinChute2 = state.CoinChute2;

            // Servicing
            ServiceCredit = state.ServiceCredit;
            ServiceRackAdvance = state.ServiceRackAdvance;
            ServiceBoardTest = state.ServiceBoardTest;
        }

        /**
         * Returns the state of the player 1 joystick, rack advance switch, both coin chute
         * switches, and the service credit button as a single byte in the format the hardware
         * is expecting (each bit is a switch/button where a 0 indicates pressed/closed and 1
         * indicates released/open).
         */
        public byte GetPortIN0()
        {
            return (byte)
            (
                ((P1Up ? 0 : 1) << 0)
                | ((P1Left ? 0 : 1) << 1)
                | ((P1Right ? 0 : 1) << 2)
                | ((P1Down ? 0 : 1) << 3)
                | ((ServiceRackAdvance ? 0 : 1) << 4)
                | ((CoinChute1 ? 0 : 1) << 5)
                | ((CoinChute2 ? 0 : 1) << 6)
                | ((ServiceCredit ? 0 : 1) << 7)
            );
        }

        /**
         * Returns the state of the player 2 joystick, board test switch, and both start buttons
         * in the format the hardware is expecting (each bit is a switch/button where a 0 indicates
         * pressed/closed and 1 indicates released/open).
         */
        public byte GetPortIN1()
        {
            // NOTE: bit 7 (cabinet mode) is omitted here; it is set elsewhere by a DIP switch setting.
            return (byte)
            (
                ((P2Up ? 0 : 1) << 0)
                | ((P2Left ? 0 : 1) << 1)
                | ((P2Right ? 0 : 1) << 2)
                | ((P2Down ? 0 : 1) << 3)
                | ((ServiceBoardTest ? 0 : 1) << 4)
                | ((P1Start ? 0 : 1) << 5)
                | ((P2Start ? 0 : 1) << 6)
            );
        }
    }
}
