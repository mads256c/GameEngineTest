using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace GameEngineTest
{
    class ContentPipe
    {
        /// <summary>
        /// Loads a texture from file.
        /// </summary>
        /// <param name="path">The path to the raw texture.</param>
        /// <returns>A Texture2D</returns>
        public static Texture2D LoadTexture(string path)
        {
            if (!File.Exists($"Content/{path}"))
            {
                throw new FileNotFoundException($"File not found at 'Content/{path}'");
            }

            //Generate a texture id
            int id = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bitmap = new Bitmap($"Content/{path}");
            //Lock the bitmap in memory
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //Define texture
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            //Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
        
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return new Texture2D(id, bitmap.Width, bitmap.Height);
        }

    }
}
