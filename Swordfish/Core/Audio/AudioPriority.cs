using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Audio
{
    /// <summary>
    /// Determines the importance of given audio, and what should prioritize when channels overflow.
    /// </summary>
    /// <remarks>
    /// Critical: This sound will never die, unless explicitly told to do so.<br/>
    /// Standard: This sound will be overwritten if the number of playing sounds overflows given channels.<br/>
    /// Minor: This sound will be always be overwritten first before any Standard flagged sounds.
    /// </remarks>
    public enum AudioPriority
    {
        /// <summary>
        /// This sound will never die, unless explicitly told to do so.
        /// </summary>
        Critical = 2,
        /// <summary>
        /// This sound will be overwritten if the number of playing sounds overflows given channels.
        /// </summary>
        Standard = 1,
        /// <summary>
        /// This sound will be always be overwritten first before any Standard flagged sounds.
        /// </summary>
        Minor = 0
    }
}
