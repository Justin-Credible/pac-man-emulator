# Pac-Man Emulator

An emulator for the [Zilog Z80](https://en.wikipedia.org/wiki/Zilog_Z80) CPU and hardware specific to the 1980 arcade game: Pac-Man.

This is based on the Intel 8080 CPU core from my [Space Invaders emulator](https://github.com/Justin-Credible/space-invaders-emulator).

🚧 Work in progress! 🚧

## Progress Checklist

- [X] Project Layout
  - [X] Copy Intel 8080 base CPU
  - [X] Copy assembler and disassembler projects
  - [X] Unit Test project
  - [X] Integration test runner
- [ ] Z80 CPU
  - [ ] Instructions
    - [X] Update Intel 8080 -> Z80 mnemonics
    - [X] Handle executing multi-byte opcodes
    - [X] Get all 600+ original unit tests working again
    - [X] Add Z80 standard instructions
    - [X] Extended Instructions (ED)
    - [ ] Bit Instructions (CB)
    - [ ] IX Instructions (DD)
    - [ ] IX Bit Instructions (DDCB)
    - [ ] IY Instructions (FB)
    - [ ] IY Bit Instructions (FDCB)
  - [ ] Handle Intel 8080 -> Z80 instruction differences
  - [ ] Pass ZEX integration test
- [ ] Pac-Man Hardware
  - [ ] Copy Space Invaders emulator project
  - [ ] CPU / Memory configuration
  - [ ] Interrupts?
  - [ ] Video
  - [ ] Audio
  - [ ] Other hardware?
- [ ] Cleanup
  - [ ] Resolve all `TODO: ...` comments
  - [ ] Proper readme
