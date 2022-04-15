using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using SharpFont;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;


namespace Swordfish.Core.Rendering
{

    public struct Character
    {
        public int TextureID { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Bearing { get; set; }
        public int Advance { get; set; }
    }

    internal class CharTexture
    {
        Library ftLibrary;

        public Dictionary<uint, Character> characters = new Dictionary<uint, Character>();

        public CharTexture(TextureUnit texUnit, string font="C:/Windows/Fonts/arial.ttf")
        {
            ftLibrary = new Library();

            Face face = new Face(ftLibrary, font);

            face.SetPixelSizes(0, 48);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            GL.ActiveTexture(texUnit);

            for (uint c = 0; c < 128; c++)
            {
                face.LoadChar(c, LoadFlags.Render, LoadTarget.Normal);
                GlyphSlot glyph = face.Glyph;
                FTBitmap bitmap = glyph.Bitmap;

                int handle = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, handle);

                GL.TexImage2D(TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.R8,
                    bitmap.Width,
                    bitmap.Rows,
                    0,
                    PixelFormat.Red,
                    PixelType.UnsignedByte,
                    bitmap.Buffer);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                // Now, set the wrapping mode. S is for the X axis, and T is for the Y axis.
                // We set this to Repeat so that textures will repeat when wrapped. Not demonstrated here since the texture coordinates exactly match
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                Character character = new Character();
                character.TextureID = handle;
                character.Size = new Vector2(bitmap.Width, bitmap.Rows);
                character.Bearing = new Vector2(glyph.BitmapLeft, glyph.BitmapTop);
                character.Advance = (int)glyph.Advance.X.Value;

                characters.Add(c, character);

            }

            // bind default texture
            GL.BindTexture(TextureTarget.Texture2D, 0);

            // set default (4 byte) pixel alignment 
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);

        }




    }
}
