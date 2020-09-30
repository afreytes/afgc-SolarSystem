using Physics;
using Raylib_cs;
namespace RLSolarSystem
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Fill { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        private readonly BallMass _ballMass;
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
    }
}