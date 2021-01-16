using Physics;
using Raylib_cs;
using System.Collections.Generic;
namespace RLSolarSystem
{
    public class CanvasManager
    {
        private readonly InertialFrameReference _engine;
        private readonly Canvas _canvas;
        private readonly List<Ball> _balls;
        private Ball SelectedBall;
        private bool IsPaused = false;

        public CanvasManager(Canvas canvas)
        {
            _canvas = canvas;
            _balls = new List<Ball>();
            _engine = new InertialFrameReference();
        }
        
        public void Render()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) { IsPaused = !IsPaused; }
            if (!IsPaused) { _engine.DoPhysics(); }
            foreach (var b in _balls)
            {
                b.Update();
                Raylib.DrawCircle(b.X, b.Y, (int)b.Width, b.Fill);
                Raylib.DrawCircleLines(b.X, b.Y, (int)b.Width, Color.WHITE);
                if (b.IsShowName) { Raylib.DrawText(b.Name,b.X+(int)b.Height,b.Y+(int)b.Width,10,Color.WHITE); }
                if (b==SelectedBall)
                {
                    Raylib.DrawText("Name: " + b.Name, 2, 24, 10, Color.WHITE);
                    Raylib.DrawText("Mass: " + b.BallMass.Mass, 2, 36, 10, Color.WHITE);
                    Raylib.DrawText("Position: " + b.BallMass.Position, 2, 48, 10, Color.WHITE);
                    Raylib.DrawText("Velocity: " + b.BallMass.Velocity, 2, 60, 10, Color.WHITE);
                    Raylib.DrawText("Acceleration: " + b.BallMass.Acceleration, 2, 72, 10, Color.WHITE);
                }
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON) && Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new System.Numerics.Vector2(b.X, b.Y), (float)b.Width)) { b.OnClick(); };
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new System.Numerics.Vector2(b.X, b.Y), (float)b.Width)) { SelectedBall = SelectedBall == b ? null : b; };

                Raylib.DrawText(IsPaused?"Paused":"", 2, 2, 10, Color.WHITE);
                Raylib.DrawText("Spacebar: Pause | Left Click: De/Select | Right Click: Show/Hide Name ", 2, _canvas.Height - 12, 10, Color.WHITE);
            }
        }

        public void AddBall(double mass, double radius,
            double x, double y, double vx, double vy, Color color, string name)
        {
            var ball = new Ball(new BallMass
                                    {
                                        Mass = mass,
                                        Radius = radius,
                                        Position = new Vector(x, y),
                                        Velocity = new Vector(vx, vy)
                                        
                                    }, color,name);
            _balls.Add(ball);
            _engine.AddBallMass(ball.BallMass);
        }
    }

}
