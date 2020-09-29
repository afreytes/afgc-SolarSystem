using Physics;
using Raylib_cs;
using System;

namespace RLSolarSystem
{
    public class Ball
    {
        private readonly BallMass _ballMass;
        private readonly Ellipse _ellipse;

        public delegate void MyEventHandler();
        public event MyEventHandler Click;

        public bool IsShowName { get; set; } = true;
        public string Name { get; set; }

        Ball()
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
        public Ellipse Ellipse
        {
            get { return _ellipse; }
        }


        public Ball(BallMass ballMass, Color color, string name)
        {
            _ellipse = new Ellipse();
            _ellipse.Height = _ellipse.Width = ballMass.Radius * 2;
            _ellipse.Fill = color;
            _ballMass = ballMass;
            Name = name;
            Update();
        }
        public void Update()
        {
            double X = _ballMass.Position.X - _ballMass.Radius;
            double Y = _ballMass.Position.Y - _ballMass.Radius;
            _ellipse.X = (int)X;
            _ellipse.Y = (int)Y;
        }

    }

    public class Ellipse
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Fill { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}