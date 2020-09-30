using Physics;
using Raylib_cs;
using System.Collections.Generic;

namespace RLSolarSystem
{
    public class Canvas
    {
        private readonly InertialFrameReference _engine;
        private readonly List<Ball> _balls;
        private readonly List<Ball> _initialValues;
        private Ball SelectedBall;
        private bool SelectedFocus = false;
        private int OffsetX = 0;
        private int OffsetY = 0;
        private bool IsPaused = false;
        public int Height { get; set; } = 1000;
        public int Width { get; set; } = 1000;
        public int Margin = 10;
        public int TextSize = 10;
        public Raylib_cs.Color TextColor = Raylib_cs.Color.WHITE;
        private int _textY;
        public Canvas()
        {
            _textY = TextSize + (Margin / 2);
            _balls = new List<Ball>();
            _initialValues = new List<Ball>();
            _engine = new InertialFrameReference();
        }
        public void AddBall(double mass, double radius, double x, double y, double vx, double vy, Color color, string name)
        {
            var ball = new Ball(new BallMass
            {
                Mass = mass,
                Radius = radius,
                Position = new Vector(x, y),
                Velocity = new Vector(vx, vy)

            }, color, name);
            var ballClone = new Ball(new BallMass
            {
                Mass = mass,
                Radius = radius,
                Position = new Vector(x, y),
                Velocity = new Vector(vx, vy)

            }, color, name);
            _balls.Add(ball);
            _initialValues.Add(ballClone);
            _engine.AddBallMass(ball.BallMass);
        }
        public void Render()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_F)) { SelectedFocus = !SelectedFocus; }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) { IsPaused = !IsPaused; }

            if (SelectedBall?.Name.Length > 0)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_H)) { SelectedBall.IsShowName = !SelectedBall.IsShowName; };
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_N))
                {
                    int Index = _balls.FindIndex(x => x.Name == SelectedBall.Name) + 1;
                    Index = Index > _balls.Count - 1 ? 0 : Index;
                    SelectedBall = _balls[Index];
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
                {
                    Ball _i = _initialValues.Find(x => x.Name == SelectedBall.Name);
                    SelectedBall.BallMass.Position.X = (double)_i.X;
                    SelectedBall.BallMass.Position.Y = (double)_i.Y;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_T)) //reset vel
                {
                    Ball _i = _initialValues.Find(x => x.Name == SelectedBall.Name);
                    SelectedBall.BallMass.Velocity.X = 0;
                    SelectedBall.BallMass.Velocity.Y = 0;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_Y)) //reset vel
                {
                    Ball _i = _initialValues.Find(x => x.Name == SelectedBall.Name);
                    SelectedBall.BallMass.Acceleration.X = 0;
                    SelectedBall.BallMass.Acceleration.Y = 0;
                }

                if (SelectedFocus)
                {
                    OffsetX = SelectedBall.X - (Width / 2);
                    OffsetY = SelectedBall.Y - (Height / 2);
                }
                else
                {
                    OffsetX = 0;
                    OffsetY = 0;
                }
            }

            if (!IsPaused) { _engine.DoPhysics(); }

            foreach (var b in _balls)
            {
                b.Update();

                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)
                    && Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition()
                    , new System.Numerics.Vector2(b.X - OffsetX, b.Y - OffsetY), (float)b.Width))
                { SelectedBall = SelectedBall == b ? null : b; };

                Raylib.DrawCircle(b.X - OffsetX, b.Y - OffsetY, (int)b.Width, b.Fill);
                Raylib.DrawCircleLines(b.X - OffsetX, b.Y - OffsetY, (int)b.Width, TextColor);
                if (b.IsShowName) { Raylib.DrawText(b.Name, b.X + (int)b.Height - OffsetX, b.Y + (int)b.Width - OffsetY, TextSize/2, TextColor); }
                if (b == SelectedBall)
                {
                    Raylib.DrawText("Name: " + b.Name, Margin, _textY * 2, TextSize, TextColor);
                    Raylib.DrawText("Mass: " + b.BallMass.Mass, Margin, _textY * 3, TextSize, TextColor);
                    Raylib.DrawText("Position: " + b.BallMass.Position, Margin, _textY * 4, TextSize, TextColor);
                    Raylib.DrawText("Velocity: " + b.BallMass.Velocity, Margin, _textY * 5, TextSize, TextColor);
                    Raylib.DrawText("Acceleration: " + b.BallMass.Acceleration, Margin, _textY * 6, TextSize, TextColor);
                }
                Raylib.DrawText(IsPaused ? "Paused" : "", Margin, Margin, TextSize, TextColor);
                Raylib.DrawText("Spacebar: Pause | Left Click: De/Select | H: Show/Hide Name | F: Planet Focus", Margin, this.Height - ((_textY+Margin)*2), TextSize, TextColor);
                Raylib.DrawText("R: Planet Reset | T: Velocity Reset | Y: Acceleration Reset | N: Next Planet", Margin, this.Height - (_textY+Margin), TextSize, TextColor);
            }
        }
    }

}
