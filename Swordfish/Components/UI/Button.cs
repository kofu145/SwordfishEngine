using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
namespace Swordfish.Components.UI
{
    ///<summary>
    /// basic context button with width, height, x position, y position and title
    ///</summary>
    public class ContextButton : IComponent
    {
        public enum ButtonStyle
        {
            FLAT = 0,
            ROUNDED = 1,
            ROUND = 2,
        }
        public readonly int _w; // width
        public readonly int _h; // height
        public readonly int _x; // x position (top left)
        public readonly int _y; // y position (top left)
        public readonly string _title; // text in button
        public readonly TextStyle _textStyle; // style of text in button
        public readonly ButtonStyle _buttonStyle; // style of button

        ///<summary>
        /// create context button with width, height, x position, y position and title
        ///</summary>
        public ContextButton(int width, int height, int xpos, int ypos, string title)
        {
            _w = width;
            _h = height;
            _x = xpos;
            _y = ypos;
            _title = title;
        }
        public ContextButton(int width, int height, int xpos, int ypos, string title, ButtonStyle k)
        {
            _w = width;
            _h = height;
            _x = xpos;
            _y = ypos;
            _title = title;
            _buttonStyle = k;
        }
        public ContextButton(int width, int height, int xpos, int ypos, string title, ButtonStyle k, TextStyle z)
        {
            _w = width;
            _h = height;
            _x = xpos;
            _y = ypos;
            _title = title;
            _buttonStyle = k;
            _textStyle = z;
        }

       
        public void onHover()
        { 
        }
        public void onClick()
        {
        }
        public void Update()
        {
        }
        public void OnLoad()
        {
        }
        
    }
}
