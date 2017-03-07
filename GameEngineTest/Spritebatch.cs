using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GameEngineTest
{
    class Spritebatch
    {
        /// <summary>
        /// Setup the orthographic rendering tecnique.
        /// </summary>
        /// <param name="windowWidth">The width of the window</param>
        /// <param name="windowHeight">TheThe height of the window</param>
        public static void Initialize(int windowWidth, int windowHeight)
        {
            //Load the projection matrix and zero it.
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            //Setup Orthographic rendering
            GL.Ortho(-windowWidth / 2f, windowWidth / 2f, windowHeight / 2f, -windowHeight / 2f, 0f, 1f);

        }

        /// <summary>
        /// Used to draw a quad sprite.
        /// </summary>
        /// <param name="texture">The sprites texture.</param>
        /// <param name="position">The sprites position.</param>
        /// <param name="scale">The sprites scale.</param>
        /// <param name="color">The sprites color</param>
        /// <param name="origin"></param>
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color, Vector2 origin)
        {
            //The sprites bounds
            Vector2[] verticies = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };

            GL.BindTexture(TextureTarget.Texture2D, texture.ID);
            
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(color);
            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(verticies[i]);

                //manipulate the position and scale
                verticies[i].X *= texture.Width;
                verticies[i].Y *= texture.Height;
                verticies[i] -= origin;
                verticies[i] *= scale;
                verticies[i] += position;

                GL.Vertex2(verticies[i]);
            }

            GL.End();
        }
    }
}
