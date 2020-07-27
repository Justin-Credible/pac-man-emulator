# Pac-Man Emulator

![.NET Core](https://github.com/Justin-Credible/pac-man-emulator/workflows/.NET%20Core/badge.svg)

An emulator for the [Zilog Z80](https://en.wikipedia.org/wiki/Zilog_Z80) CPU and hardware specific to the 1980 arcade game: Pac-Man.

This is based on the Intel 8080 CPU core from my [Space Invaders emulator](https://github.com/Justin-Credible/space-invaders-emulator).

🚧 Work in progress! 🚧

## Progress Checklist

- [X] Project Layout
  - [X] Copy Intel 8080 base CPU
  - [X] Copy assembler and disassembler projects
  - [X] Unit Test project
  - [X] Integration test runner
- [X] Z80 CPU
  - [X] Instructions
    - [X] Update Intel 8080 -> Z80 mnemonics
    - [X] Handle executing multi-byte opcodes
    - [X] Get all 600+ original unit tests working again
    - [X] Add Z80 standard instructions
    - [X] Extended Instructions (ED)
    - [X] Bit Instructions (CB)
    - [X] IX Instructions (DD)
    - [X] IX Bit Instructions (DDCB)
    - [X] IY Instructions (FB)
    - [X] IY Bit Instructions (FDCB)
  - [X] Handle Intel 8080 -> Z80 instruction differences
  - [X] Pass ZEX integration test
  - [X] Re-work flag setting for arithmetic vs logical operations
- [X] Pac-Man Hardware
  - [X] Copy Space Invaders emulator project
  - [X] CPU / Memory configuration
  - [X] Interrupts?
  - [X] Video
    - [X] Parse Color ROM
    - [X] Parse Palettes
    - [X] Parse Tiles
    - [X] Render Tiles
    - [X] Parse Sprites
    - [X] Render Sprites
    - [X] Render entire screen
  - [X] Audio
  - [X] Controllers / Input
  - [X] DIP Switches
- [X] Cleanup
  - [X] Rendering bugs
  - [X] Save/Load state bugs
  - [ ] Resolve all `TODO: ...` comments
  - [ ] Proper readme
