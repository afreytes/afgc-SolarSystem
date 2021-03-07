using afgc;
using Physics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace afgcSolarSystem
{
    public class Ball
    {
        //public int X { get; set; }
        //public int Y { get; set; }
        //replaces X and Y
        public Vector2[] Position = new Vector2[1001];
        public int CurrentIndex = 1000;
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
            //X = (int)(_ballMass.Position.X - _ballMass.Radius);
            //Y = (int)(_ballMass.Position.Y - _ballMass.Radius);
            for (int c = 0; c < Position.Length - 1; c++)
            {
                Position[c] = Position[c + 1];
            }
            Position[1000] = new Vector2((float)(_ballMass.Position.X - _ballMass.Radius), (float)(_ballMass.Position.Y - _ballMass.Radius));
        }

        internal void Render()
        {
            Update();

            if (IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)
                && CheckCollisionPointCircle(GetMousePosition()
                , Position[CurrentIndex], (float)Width))
            { IsSelected = !IsSelected; }

            for (int c = 0; c < Position.Length; c++) { DrawPixelV(Position[c], Defaults.colAlphaGrey); }
            DrawCircleV(Position[CurrentIndex], (int)Width + 1, Defaults.IsNowOdd() ? Color.RAYWHITE : Color.YELLOW);
            DrawCircleV(Position[CurrentIndex], (int)Width, Fill);
            if (IsShowName) { DrawTextEx(GetFontDefault(), Name, Position[CurrentIndex] + new Vector2(-5, (float)Width + 2f), Defaults.FontSize / 2, 1f, Defaults.colC64BackColor); }

        }
    }
}
