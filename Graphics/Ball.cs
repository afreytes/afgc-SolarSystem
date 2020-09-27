using Physics;
using Raylib_cs;

namespace RLSolarSystem
{
    public class Ball
    {
        private readonly BallMass _ballMass;
        private readonly Ellipse _ellipse;

        public BallMass BallMass
        {
            get { return _ballMass; }
        }

        public Ellipse Ellipse
        {
            get { return _ellipse; }
        }

        public Ball(BallMass ballMass, Color color)
        {
            _ellipse = new Ellipse();
            _ellipse.Height = _ellipse.Width = ballMass.Radius * 2;
            _ellipse.Fill = color;
            _ballMass = ballMass;
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
        public double Width { get ; set ; }
    }
}