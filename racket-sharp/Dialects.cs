using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racket_sharp
{
    /// <summary>
    /// Enum for different dialects of racket.
    /// </summary>
    public enum LanguageDialect
    {
        /// <summary>
        /// racket-sharp - this is not a dialect.
        /// <para>C# likes default values on enums.</para>
        /// </summary>
        RacketSharp = 0,
        /// <summary>
        /// RacketSharpSharp - C# compatable racket dialect:
        /// <para>1. Uses non-overridden C# System.dll members.</para>
        /// <para>2. Uses C#-compliant object system.</para>
        /// <para>3. C#-marked type system for functions, fields and parameters.</para>
        /// <para>4. Optional C# type system for instance values (that are not object/dynamic).</para>
        /// <para>5. Compiles into C# bytecode or programs.</para>
        /// </summary>
        RacketSharpSharp,
        /// <summary>
        /// RacketSharpSnirk - racket dialect designed to become Snirk:
        /// <para>1. Uses Snirk dialect operators.</para>
        /// <para>2. Uses Snirk object/inheritance system.</para>
        /// </summary>
        RacketSharpSnirk,
        /// <summary>
        /// RacketSharpRacket - racket dialect emphasizing racket:
        /// <para>1. All operators inherited from racket.</para>
        /// <para>2. Racket-style dynamic type system.</para>
        /// <para>3. Racket compilation-compliant goal.</para>
        /// </summary>
        RacketSharpRacket
    }
}
