using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace Swordfish.Core.Rendering
{
    public enum TextureFilter
    {
        linear = 0,
        nearest = 1
    }

    /// <summary>
    /// This entire class is constructed from example snippets
    /// Creates a texture class to handle loading and managing textures.
    /// </summary>
    internal class Texture
    {
        public readonly int Handle;

        public readonly int Width;
        public readonly int Height;

        public static Texture LoadFromFile(string path, TextureFilter texFilter)
        {
            int handle = GL.GenTexture();
            int width;
            int height;

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            using (var image = new Bitmap(path))
            {
                width = image.Width;
                height = image.Height;
                // openGL loads from bottom left, when the bitmap loads from top left - need to flip to orient properly.
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);

                var data = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width,
                    image.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0);
            }

            switch(texFilter)
            {
                case TextureFilter.linear:
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                    break;

                case TextureFilter.nearest:
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                    break;

                default:
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                    break;
            }
            
            

            // Maybe set texturewrap mode to clamp in future?
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle, width, height);

        }

        public Texture(int glHandle, int width, int height)
        {
            Handle = glHandle;
            Width = width;
            Height = height;
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

    }
}
