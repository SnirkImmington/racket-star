using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketStar.Dialects
{
    /// <summary>
    /// Dialect enumeration.
    /// </summary>
    enum Dialects
    {
        /// <summary>
        /// #justcsharpenumthings
        /// </summary>
        None = 0,
        /// <summary>
        /// racket-compliant language.
        /// </summary>
        Racket,
        /// <summary>
        /// .NET-compiling racket-syntax language.
        /// </summary>
        NET,
        /// <summary>
        /// Snirk.
        /// </summary>
        Snirk
    }
}
