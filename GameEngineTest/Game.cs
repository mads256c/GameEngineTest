using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GameEngineTest
{
    class Game : GameWindow
    {
        Texture2D texture;
        View view;

        readonly static Color color = Color.CornflowerBlue;

        /// <summary>
        /// Constructs a overridden GameWindow.
        /// </summary>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        public Game(int width, int height)
            : base(width, height)
        {
            GL.Enable(EnableCap.Texture2D);
            view = new View(Vector2.Zero, 1, 0);

            Input.Initialize(this);
        }


        /// <summary>
        /// Called after an OpenGL context has been established, but before entering main loop.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            texture = ContentPipe.LoadTexture("Texture/Wood01.jpg");
        }

        /// <summary>
        /// Called when the frame is updated.
        /// </summary>
        /// <param name="e">Contains information necessary for frame updating.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Input.MousePress(MouseButton.Left))
            {
                Vector2 pos = new Vector2(Mouse.X, Mouse.Y) - new Vector2(this.Width, this.Height) / 2f;
                pos = view.ToWorld(pos);

                view.SetPosition(pos, TweenType.QuadraticInOut, 15);
            }

            if (Input.KeyDown(Key.Right))
            {
                view.SetPosition(view.PositionGoto + new Vector2(5, 0), TweenType.QuarticOut, 15);
            }

            if (Input.KeyDown(Key.Left))
            {
                view.SetPosition(view.PositionGoto + new Vector2(-5, 0), TweenType.QuarticOut, 15);
            }

            if (Input.KeyDown(Key.Up))
            {
                view.SetPosition(view.PositionGoto + new Vector2(0, -5), TweenType.QuarticOut, 15);
            }

            if (Input.KeyDown(Key.Down))
            {
                view.SetPosition(view.PositionGoto + new Vector2(0, 5), TweenType.QuarticOut, 15);
            }

            view.Update();
            Input.Update();
        }

        /// <summary>
        /// Called when the frame is rendered.
        /// </summary>
        /// <param name="e">Contains information necessary for frame rendering.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //Clears the color buffer bit and sets the clear color
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(color);

            Spritebatch.Initialize(this.Width, this.Height);
            view.ApplyTransform();

            Spritebatch.Draw(texture, Vector2.Zero, new Vector2(0.2f, 0.2f), Color.White, new Vector2(10, 50));

            this.SwapBuffers();
        }
    }
}
