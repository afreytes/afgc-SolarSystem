using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
namespace afgc
{
    public class afgcCamera
    {
        public int Width;
        public int Height;
        private Camera2D camera = new Camera2D();
        public Vector2 Target { get { return camera.target; } set { camera.target = value; } }
        public Vector2 Offset { get { return camera.offset; } set { camera.offset = value; } }
        public afgcCamera()
        {
            Initialize(GetScreenWidth(), GetScreenHeight());
        }
        public afgcCamera(int w, int h)
        {
            Initialize(w, h);
        }
        private void Initialize(int w, int h)
        {
            Width = w;
            Height = h;
            camera.target = new Vector2(Width / 2, Height / 2);
            camera.offset = new Vector2(0, 0);
            camera.rotation = 0.0f;
            camera.zoom = Defaults.Camera2dZoom;
        }
        public void Start()
        {
            BeginMode2D(camera);
        }
        public void End()
        {
            EndMode2D();
            Input();
        }
        public void Input()
        {
            // Camera3D zoom controls
            camera.zoom += (float)GetMouseWheelMove() * Defaults.Camera2dZoomIncrement;

            if (camera.zoom > Defaults.Camera2dZoomMax) camera.zoom = Defaults.Camera2dZoomMax;
            else if (camera.zoom < Defaults.Camera2dZoomMin) camera.zoom = Defaults.Camera2dZoomMin;

            // Camera3D reset (zoom and rotation)
            if (IsKeyPressed(KeyboardKey.KEY_R))
            {
                camera.zoom = Defaults.Camera2dZoom;
                camera.rotation = Defaults.Camera2dRotation;
            }
        }
    }
}
