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

        public CanvasManager(Canvas canvas)
        {
            _canvas = canvas;
            _balls = new List<Ball>();
            _engine = new InertialFrameReference();
        }
        
        public void Render()
        {
            _engine.DoPhysics();
            foreach (var b in _balls)
            {
                b.Update();
                Raylib.DrawCircle(b.Ellipse.X, b.Ellipse.Y, (int)b.Ellipse.Width, b.Ellipse.Fill);
                Raylib.DrawCircleLines(b.Ellipse.X, b.Ellipse.Y, (int)b.Ellipse.Width, Color.WHITE);
            }
        }

        public void AddBall(double mass, double radius,
            double x, double y, double vx, double vy, Color color)
        {
            var ball = new Ball(new BallMass
                                    {
                                        Mass = mass,
                                        Radius = radius,
                                        Position = new Vector(x, y),
                                        Velocity = new Vector(vx, vy),
                                    }, color);
            _balls.Add(ball);
            _engine.AddBallMass(ball.BallMass);
            _canvas.Objects.Add(ball.Ellipse);
        }
    }

}
