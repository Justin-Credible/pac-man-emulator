using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using SDL2;

namespace JustinCredible.PacEmu
{
    class FontRenderer
    {
        private static Regex COLOR_CONTROL = new Regex("{(\\d+),(\\d+),(\\d+)}");

        public static void RenderCharacter(IntPtr sdlRenderer, char character, int x, int y, Color? foreground = null, Color? background = null)
        {
            if (foreground == null)
                foreground = Color.White;

            if (background == null)
                background = Color.Black;

            for (var row = 0; row < 8; row++)
            {
                int bitmapRow = Font.Basic8x8[(int)'?', row];

                if (character >= 128)
                {
                    try
                    {
                        var extendedBitmapRow = Font.GetExtendedCharacter((int)character);

                        if (extendedBitmapRow == null)
                        {
                            Console.WriteLine($"[WARN] FontRenderer.RenderCharacter: Error fetching bitmap for extended character ${character}; using '?' instead.");
                        }
                        else
                        {
                            bitmapRow = extendedBitmapRow[row];
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"[WARN] FontRenderer.RenderCharacter: Error fetching bitmap for extended character ${character}; using '?' instead.");
                    }
                }
                else
                {
                    try
                    {
                        bitmapRow = Font.Basic8x8[(int)character, row];
                    }
                    catch
                    {
                        Console.WriteLine($"[WARN] FontRenderer.RenderCharacter: Error fetching bitmap for character ${character}; using '?' instead.");
                    }
                }

                for (var column = 0; column < 8; column++)
                {
                    int bitmapRowShifted = bitmapRow >> column;
                    bool isPixelSet = (bitmapRowShifted & 0x01) == 1;

                    if (isPixelSet)
                        SDL.SDL_SetRenderDrawColor(sdlRenderer, foreground.Value.R, foreground.Value.G, foreground.Value.B, 255);
                    else
                        SDL.SDL_SetRenderDrawColor(sdlRenderer, background.Value.R, background.Value.G, background.Value.B, 255);

                    SDL.SDL_RenderDrawPoint(sdlRenderer, x + column, y + row);
                }
            }
        }

        /**
         * Renders a string of characters at the given coordinates.
         * 
         * Color can be specified inline with the magic string in the format: {R,G,B}
         * Example: "{255,255,255}This is the color {255,0,0}Red{255,255,255}!"
         */
        public static void RenderString(IntPtr sdlRenderer, string str, int x, int y)
        {
            // Will contain the indexs in the string at which colors should be applied.
            var colors = new Dictionary<int, Color>();

            var match = COLOR_CONTROL.Match(str);

            while (match.Success)
            {
                // If we found a color string, extract the RGB values and insert the color
                // by string character index into the dictionary so we know which color to
                // apply at the location.
                var index = match.Index;
                var r = int.Parse(match.Groups[1].Value);
                var g = int.Parse(match.Groups[2].Value);
                var b = int.Parse(match.Groups[3].Value);
                var color = Color.FromArgb(r, g, b);
                colors.Add(index, color);

                // Remove the magic string since we don't want to render it.
                str = str.Remove(index, match.Length);

                // Attempt to find the next match.
                match = COLOR_CONTROL.Match(str);
            }

            // Start off assuming the color is white.
            Color currentColor = Color.White;

            for (var i = 0; i < str.Length; i++)
            {
                var character = str[i];

                // See if we need to change the active rendering color for this character index.
                if (colors.ContainsKey(i))
                    currentColor = colors[i];

                RenderCharacter(sdlRenderer, character, x + (i * 8), y, currentColor);
            }
        }
    }
}
