λ* (racket-star)
==================
**WIP** | Current version 0.0.3 alpha | Written in C#

A high-level [Racket](http://racket-lang.org) interpreter, using the .NET runtime.

This project is not finished. The current goal is to have a high-level Racket syntaxed language using .NET reflection for the main libraries. λ* will have two dialects:

## λ♯ (RacketSharp)

This variant will compile into .NET code. RacketSharp will use the .NET object structure, as well as having side effects and other C♯ features. High-level internal runtime with λ♯ will still be supported.

## λ' (RacketPrime)

This dialect will more closely mirror racket, in syntax and function. Although the base libraries will use C♯ reflection, some names will be selectively changed and some features of C♯ and .NET will be blocked in order to more properly emulate racket. λ' will *not* be compatible with .NET; the ultimate goal is a language and library base identical to Racket itself.
