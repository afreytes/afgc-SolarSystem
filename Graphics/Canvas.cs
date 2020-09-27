using System.Collections.Generic;
namespace RLSolarSystem
{
    public class Canvas
    {
        public int Height { get; set; } = 1000;
        public int Width { get; set; } = 1800;
        public List<Ellipse> Objects { get; set; } = new List<Ellipse>();
    }
}
