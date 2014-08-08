using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar
{
    /// <summary>
    /// Enum for different dialects of Racket.
    /// </summary>
    public enum LanguageDialect
    {
        /// <summary>
        /// Racket* - this is not a dialect.
        /// <para>C# likes default values on enums.</para>
        /// </summary>
        RacketStar = 0,
        /// <summary>
        /// Racket# - C# compatable Racket dialect:
        /// <para>1. Uses non-overridden C# System.dll members.</para>
        /// <para>2. Uses C#-compliant object system.</para>
        /// <para>3. C#-marked type system for functions, fields and parameters.</para>
        /// <para>4. Optional C# type system for instance values (that are not object/dynamic).</para>
        /// <para>5. Cand be compiled into C# bytecode or programs.</para>
        /// </summary>
        RacketSharp,
        /// <summary>
        /// RacketSharpSnirk - Racket dialect designed to become Snirk:
        /// <para>1. Uses Snirk dialect operators.</para>
        /// <para>2. Uses Snirk object/inheritance system.</para>
        /// </summary>
        RacketSnirk,
        /// <summary>
        /// Racket' - Racket dialect emphasizing Racket:
        /// <para>1. All operators inherited from Racket.</para>
        /// <para>2. Racket-style dynamic type system.</para>
        /// <para>3. Racket compilation-compliant goal.</para>
        /// </summary>
        RacketPrime
    }
}
