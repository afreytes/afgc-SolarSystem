using afgc;
using Physics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections.Generic;

namespace afgcSolarSystem
{
    public class Canvas
    {
        public int WindowHeight = 600;
        public int WindowWidth = 800;

        private readonly List<Ball> Balls;
        private readonly List<Ball> BallsInitialValues;
        private readonly InertialFrameReference Engine;
        private afgcCamera Camera;
        private System.Numerics.Vector2 CameraTarget = new System.Numerics.Vector2();
        private int FontSize = Defaults.FontSize;
        private bool IsPaused = false;
        private int Margin = Defaults.Margin;
        private Ball SelectedBall;
        private bool SelectedFocus = false;
        private Color TextColor = Defaults.colC64BackColor;
        private int TextLocationY;

        public Canvas()
        {
            TextLocationY = FontSize + (Margin / 2);
            Balls = new List<Ball>();
            BallsInitialValues = new List<Ball>();
            Engine = new InertialFrameReference();
            Camera = new afgcCamera(WindowWidth, WindowHeight);
            //Camera.Offset = new System.Numerics.Vector2(0, 0);
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
            Balls.Add(ball);
            BallsInitialValues.Add(ballClone);
            Engine.AddBallMass(ball.BallMass);
        }
        public void Render()
        {
            HandleInputs();

            if (!IsPaused) { Engine.DoPhysics(); }

            Camera.Start();

            Balls.ForEach(x => x.Render());

            Camera.Target = CameraTarget;
            //camera.Offset = _canvas.CameraTarget;
            Camera.End();

            DrawInterface();
        }
        private void DrawInterface()
        {
            //Draw interface elements when camera finishes
            DrawText(IsPaused ? "Paused" : "", Defaults.Margin, Defaults.Margin, Defaults.FontSize, Defaults.colC64BackColor);
            DrawText("[Spacebar - Pause][H - Show/Hide Name][F - Planet Focus]", Defaults.Margin, Defaults.WindowHeight - ((TextLocationY + Defaults.Margin) * 2), Defaults.FontSize, Defaults.colC64BackColor);
            DrawText("[R - Planet Reset][N - Next Planet][Mouse Wheel - Zoom]", Defaults.Margin, Defaults.WindowHeight - (TextLocationY + Defaults.Margin), Defaults.FontSize, Defaults.colC64BackColor);
            if (SelectedBall != null)
            {
                DrawText("Name: " + SelectedBall.Name, Margin, TextLocationY * 2, FontSize, TextColor);
                DrawText("Mass: " + SelectedBall.BallMass.Mass, Margin, TextLocationY * 3, FontSize, TextColor);
                DrawText("Position: " + SelectedBall.BallMass.Position, Margin, TextLocationY * 4, FontSize, TextColor);
                DrawText("Velocity: " + SelectedBall.BallMass.Velocity, Margin, TextLocationY * 5, FontSize, TextColor);
                DrawText("Acceleration: " + SelectedBall.BallMass.Acceleration, Margin, TextLocationY * 6, FontSize, TextColor);
            }
        }
        private void HandleInputs()
        {
            if (IsKeyPressed(KeyboardKey.KEY_F)) { SelectedFocus = !SelectedFocus; }
            if (IsKeyPressed(KeyboardKey.KEY_SPACE)) { IsPaused = !IsPaused; }

            if (IsKeyPressed(KeyboardKey.KEY_N))
            {
                int Index = 0;
                if (Balls.Find(x => x.IsSelected == true) != null)
                {
                    Index = Balls.FindIndex(x => x.IsSelected == true) + 1;
                    Balls[Index - 1].IsSelected = false;
                }
                Index = Index > Balls.Count - 1 ? 0 : Index;
                Balls[Index].IsSelected = true;
                SelectedBall = Balls[Index];
            }

            if (SelectedBall?.Name.Length > 0)
            {
                if (IsKeyPressed(KeyboardKey.KEY_H)) { SelectedBall.IsShowName = !SelectedBall.IsShowName; };
                if (IsKeyPressed(KeyboardKey.KEY_R))
                {
                    Ball _i = BallsInitialValues.Find(x => x.Name == SelectedBall.Name);
                    SelectedBall.BallMass.Position.X = (double)_i.Position[0].X;
                    SelectedBall.BallMass.Position.Y = (double)_i.Position[0].Y;
                    SelectedBall.BallMass.Velocity.X = 0;
                    SelectedBall.BallMass.Velocity.Y = 0;
                    SelectedBall.BallMass.Acceleration.X = 0;
                    SelectedBall.BallMass.Acceleration.Y = 0;
                }
                if (SelectedFocus)
                {
                    Camera.Target = new System.Numerics.Vector2(SelectedBall.Position[SelectedBall.CurrentIndex].X, SelectedBall.Position[SelectedBall.CurrentIndex].Y);
                    Camera.Offset = new System.Numerics.Vector2(WindowWidth / 2, WindowHeight / 2);
                }
                else
                {
                    Camera.Target = new System.Numerics.Vector2(0, 0);
                    Camera.Offset = new System.Numerics.Vector2(0, 0);
                }
            }
        }
    }
}
