using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.UI
{
    ///<summary>
    /// basic context button with width, height, x position, y position and title
    ///</summary>
    internal class ContextButton
    {
        public enum ButtonStyle
        {
            FLAT = 0,
            ROUNDED = 1,
            ROUND = 3,
        }
        private int _w; // width
        private int _h; // height
        private int _x; // x position (top left)
        private int _y; // y position (top left)
        private string _title; // text in button
        private TextStyle _textStyle; // style of text in button
        private ButtonStyle _buttonStyle; // style of button

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
        public ContextButton(int width, int height, int xpos, int ypos, string title, buttonStyle k)
        {
            _w = width;
            _h = height;
            _x = xpos;
            _y = ypos;
            _title = title;
            _buttonStyle = k;
        }
        public ContextButton(int width, int height, int xpos, int ypos, string title, buttonStyle k, textStyle z)
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
        public void update()
        {

        }
        
    }
}
