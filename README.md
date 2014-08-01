racket-sharp
==================

A .NET based LISP style interpreter, designed to work with most racket code, adapting the C# codebase.

This project is not finished. The current goal is to have a high-level LISP syntaxed language using .NET reflection for the codebase. In the future, racket-sharp will have two dialects:

**RacketSharpSharp**

This variant will compile into .NET code. RacketSharpSharp will use C# and .NET object structure, as well as having side effects and other C# features. High-level internal runtime with RacketSharpSharp will still be supported.

**racket-sharp-racket**

This dialect will more closely mirror racket, in syntax and function. Although the base libraries will use C# reflection, some names will be selectively changed and some features of C# will be blocked in order to more properly emulate racket. racket-sharp-racket will *not* be compatible with C# or .NET; the ultimate goal is a language and codebase almost identical to racket.
