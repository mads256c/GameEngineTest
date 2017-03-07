using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace GameEngineTest
{
    public enum TweenType
    {
        Instant,
        Linear,
        QuadraticInOut,
        CubicInOut,
        QuarticOut,
        BounceOut
    }

    class View
    {
        private Vector2 position;

        /// <summary>
        /// In radians, + = clockwise.
        /// </summary>
        public double rotation;

        /// <summary>
        /// 1 = no zoom.
        /// 2 = 2x zoom.
        /// </summary>
        public double zoom;

        private Vector2 positionGoto, positionFrom;
        private TweenType tweenType;
        private int currentStep, tweenSteps;

        public Vector2 Position
        {
            get { return this.position; }
        }

        public Vector2 PositionGoto
        {
            get { return this.positionGoto; }
        }

        /// <summary>
        /// Takes a position in client area and converts it to a world position.
        /// </summary>
        /// <param name="input">Position in client area.</param>
        /// <returns>The world position.</returns>
        public Vector2 ToWorld(Vector2 input)
        {
            input /= (float)zoom;
            Vector2 dX = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Vector2 dY = new Vector2((float)Math.Cos(rotation + MathHelper.PiOver2), (float)Math.Sin(rotation + MathHelper.PiOver2));

            return this.position + dX * input.X + dY * input.Y;
        }

        /// <summary>
        /// Create a new view (camera). Only one should be used per GameWindow.
        /// </summary>
        /// <param name="startPosition">The position the view (camera) starts with.</param>
        /// <param name="startZoom">The zoom the view (camera) starts with.</param>
        /// <param name="startRotation">The rotation in radians the view (camera) starts with.</param>
        public View(Vector2 startPosition, double startZoom = 1.0, double startRotation = 0.0)
        {
            this.position = startPosition;
            this.zoom = startZoom;
            this.rotation = startRotation;
        }

        /// <summary>
        /// Updates the position with one of the tweening types in TweenType
        /// </summary>
        public void Update()
        {
            if (currentStep < tweenSteps)
            {
                currentStep++;

                switch (tweenType)
                {
                    case TweenType.Linear:
                        position = positionFrom + (positionGoto - positionFrom) * GetLinear((float)currentStep / tweenSteps);
                        break;

                    case TweenType.QuadraticInOut:
                        position = positionFrom + (positionGoto - positionFrom) * GetQuadraticInOut((float)currentStep / tweenSteps);
                        break;

                    case TweenType.CubicInOut:
                        position = positionFrom + (positionGoto - positionFrom) * GetCubicInOut((float)currentStep / tweenSteps);
                        break;

                    case TweenType.QuarticOut:
                        position = positionFrom + (positionGoto - positionFrom) * GetQuarticOut((float)currentStep / tweenSteps);
                        break;

                    case TweenType.BounceOut:
                        position = positionFrom + (positionGoto - positionFrom) * GetBounceOut((float)currentStep / tweenSteps);
                        break;
                }
            }
            else
            {
                position = positionGoto;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            this.position = newPosition;
            this.positionFrom = newPosition;
            this.positionGoto = newPosition;
            tweenType = TweenType.Instant;
            currentStep = 0;
            tweenSteps = 0;
        }
        public void SetPosition(Vector2 newPosition, TweenType type, int numSteps)
        {
            this.positionFrom = position;
            this.position = newPosition;
            this.positionGoto = newPosition;
            tweenType = type;
            currentStep = 0;
            tweenSteps = numSteps;
        }

        //Used for tweening
        private static float GetLinear(float t)
        {
            return t;
        }

        private static float GetQuadraticInOut(float t)
        {
            return (t * t) / ((2 * t * t) - (2 * t) + 1);
        }

        private static float GetCubicInOut(float t)
        {
            return (t * t * t) / ((3 * t * t) - (3 * t) + 1);
        }

        private static float GetQuarticOut(float t)
        {
            return -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
        }

        private static float GetBounceOut(float t)
        {
            float p = 0.3f;
            return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
        }

        /// <summary>
        /// Creating a matrix and applying the Translation, Rotation and Scale.
        /// </summary>
        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;

            //Set Translation, Rotaion and Scale
            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-(float)rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));

            GL.MultMatrix(ref transform);
        }

    }
}
