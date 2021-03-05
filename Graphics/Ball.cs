using afgc;
using Physics;
using Raylib_cs;
using System;

namespace afgcSolarSystem
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Fill { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public bool IsSelected = false;
        private readonly BallMass _ballMass;
        public delegate void MyEventHandler();
        public event MyEventHandler Click;
        public bool IsShowName { get; set; } = true;
        public string Name { get; set; }
        private Ball()
        {
            Click += new MyEventHandler(OnClick);
        }
        public void OnClick()
        {
            IsShowName = !IsShowName;
            //do work
        }
        public BallMass BallMass
        {
            get { return _ballMass; }
        }
        public Ball(BallMass ballMass, Color color, string name)
        {
            Height = Width = ballMass.Radius * 2;
            Fill = color;
            _ballMass = ballMass;
            Name = name;
            Update();
        }
        public void Update()
        {
            X = (int)(_ballMass.Position.X - _ballMass.Radius);
            Y = (int)(_ballMass.Position.Y - _ballMass.Radius);
        }

        internal void Render()
        {
            Update();

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)
                && Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition()
                , new System.Numerics.Vector2(X, Y), (float)Width))
            { IsSelected = !IsSelected; }

            Raylib.DrawCircle(X, Y, (int)Width, Fill);
            Raylib.DrawCircleLines(X, Y, (int)Width, Defaults.IsNowOdd() ? Color.RAYWHITE : Color.YELLOW);
            if (IsShowName) { Raylib.DrawText(Name, X + (int)Height, Y + (int)Width, Defaults.FontSize / 2, Defaults.colC64BackColor); }

        }
    }
}
