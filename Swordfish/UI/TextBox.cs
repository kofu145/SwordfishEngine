using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
namespace Swordfish.UI
{
    public class TextBox : IComponent
    {
        private int _l;
        private int _fs;
        private TextStyle _s;
        ///<summary>
        /// create textbox of num of chars, fontsize, and textstyle
        ///</summary>
        public TextBox(int length, int fontSize, TextStyle style)
        {
            _l = length;
            _fs = fontSize;
            _s = style;
        }
        public void onHover()
        {

        }
        public void onClick()
        {

        }
        public void OnLoad()
        {
            
        }
        public void Update()
        {

        }
    }
}
