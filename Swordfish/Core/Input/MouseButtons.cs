using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Input
{
    // Just like Keys, set enum compatible with opentk's
    /// <summary>
    /// Enum specifying the buttons of a mouse.
    /// </summary>
    public enum MouseButton
    {
        //
        // Summary:
        //     The first button.
        Button1 = 0,
        //
        // Summary:
        //     The left mouse button. This corresponds to OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Button1.
        Left = 0,
        //
        // Summary:
        //     The second button.
        Button2 = 1,
        //
        // Summary:
        //     The right mouse button. This corresponds to OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Button2.
        Right = 1,
        //
        // Summary:
        //     The third button.
        Button3 = 2,
        //
        // Summary:
        //     The middle mouse button. This corresponds to OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Button3.
        Middle = 2,
        //
        // Summary:
        //     The fourth button.
        Button4 = 3,
        //
        // Summary:
        //     The fifth button.
        Button5 = 4,
        //
        // Summary:
        //     The sixth button.
        Button6 = 5,
        //
        // Summary:
        //     The seventh button.
        Button7 = 6,
        //
        // Summary:
        //     The eighth button.
        Button8 = 7,
        //
        // Summary:
        //     The highest mouse button available.
        Last = 7
    }
}
