using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.Components.UI
{
    ///<summary>
    /// Style for text that is displayed on screen.
    ///</summary>
    [Flags]
    public enum TextStyle
    {
        ITALICS = 0,
        BOLD = 1,
        STRIKETHROUGH = 2,
        UNDERLINE = 4,
    }
}
