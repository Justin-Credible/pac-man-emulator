using System;
using SDL2;

namespace JustinCredible.PacEmu
{
    class FontRenderer
    {
        public static void RenderCharacter(IntPtr sdlRenderer, char character, int x, int y)
        {
            for (var row = 0; row < 8; row++)
            {
                int bitmapRow = Font.Basic8x8[(int)'?', row];

                try
                {
                    bitmapRow = Font.Basic8x8[(int)character, row];
                }
                catch
                {
                    Console.WriteLine($"[WARN] FontRenderer.RenderCharacter: Error fetching bitmap for character ${character}; using '?' instead.");
                }

                for (var column = 0; column < 8; column++)
                {
                    int bitmapRowShifted = bitmapRow >> column;
                    bool isPixelSet = (bitmapRowShifted & 0x01) == 1;

                    if (isPixelSet)
                        SDL.SDL_SetRenderDrawColor(sdlRenderer, 255, 255, 255, 255);
                    else
                        SDL.SDL_SetRenderDrawColor(sdlRenderer, 0, 0, 0, 255);

                    SDL.SDL_RenderDrawPoint(sdlRenderer, x + column, y + row);
                }
            }
        }

        public static void RenderString(IntPtr sdlRenderer, string str, int x, int y)
        {
            for (var i = 0; i < str.Length; i++)
            {
                var character = str[i];
                RenderCharacter(sdlRenderer, character, x + (i * 8), y);
            }
        }
    }
}
