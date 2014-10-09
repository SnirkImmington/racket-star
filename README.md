Racket*
==================
**WIP** | Current version 0.0.4 alpha

Two high-level [Racket](http://racket-lang.org) interpreters, using the .NET runtime.

RacketStar consists of two Racket derivatices: Racket# and Racket' (prime).

## Racket♯

Racket# is a.NET language. It compiles into .NET assemblies using the CLR and other libraries. It features both a compiler and a REPL. The compiler can make class files/executables and the REPL uses an `AssemblyBuilder` to create a dynamic assembly. Racket# uses the .NET libraries and runtime as its codebase. Although it features Lisp style naming and keyword abilities, compiling an assembly with a class named `some-class` can break functionality with C#.

Ports to other environments, i.e. the JVM, are for later.

## Racket' (RacketPrime)

This will be an implementation of Racket at a high-level (using C#). Although I plan to include concepts such as keywords, quotes, quasiquotes, and the like, a certain amount of .NET -> Racket' will make the task easier (i.e. not having to redefine libraries). I hope, however, to evolve Racket' into being mostly self-contained in order to stay true to the language and its libraries.
