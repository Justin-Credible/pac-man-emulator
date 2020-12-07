using System;
using SDL2;

namespace JustinCredible.PacEmu.UWP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set hints for how we want SDL to behave: https://wiki.libsdl.org/CategoryHints
            SDL.SDL_SetHint("SDL_WINRT_HANDLE_BACK_BUTTON", "1");

            // Delegate to SDL to allow it to perform all the necessary tasks needed to
            // boostrap a UWP app, so we don't have to! Once it's ready it will fire SDLMain.
            SDL.SDL_main_func mainFunction = SDLMain;
            SDL.SDL_WinRTRunApp(mainFunction, IntPtr.Zero);
        }

        private static int SDLMain(int argc, IntPtr argv)
        {
            Emulator.Start(new EmulatorConfig()
            {
                RomPath = ".\\roms\\pacman",
                RomSet = ROMSet.PacMan,

                //RomPath = ".\\roms\\mspacman",
                //RomSet = ROMSet.MsPacMan,
            });

            return 0;
        }
    }
}
