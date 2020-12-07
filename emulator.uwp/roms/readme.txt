
Include the ROM files in this directory, each rom set should be in its own directory. For example:

\roms\mspacman
\roms\pacman

Ensure these files are included in the Visual Studio project file with the following settings:

• Build Action: None
• Copy to Output Directory: Copy Always

This will ensure the ROM files are bundled into the UWP application package.

To choose which ROM is launched, see the Program::SDLMain method (the path and ROMSet enumeration will need to be set).
