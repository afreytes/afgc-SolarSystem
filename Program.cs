using Raylib_cs;
namespace RLSolarSystem
{
    class Program
    {
        static CanvasManager _canvasManager;

        static void Main()
        {
            Canvas _canvas = new Canvas();
            _canvasManager = new CanvasManager(_canvas);
            int Middle = _canvas.Height / 2;
            int Center = _canvas.Width / 2;

            //Sun
            _canvasManager.AddBall(mass: 99, radius: 25, x: Center, y: Middle, vx: -0.0, vy: 0.0, Color.YELLOW,"Sun");
            //Planets
            _canvasManager.AddBall(mass: 0.01, radius: 01, x: Center + 100 + 000, y: Middle, vx: -0.06, vy:+1.80, Color.DARKBROWN, "Mercury");
            _canvasManager.AddBall(mass: 0.02, radius: 03, x: Center + 100 + 050, y: Middle, vx: -0.36, vy:+1.35, Color.GREEN, "Venus");
            _canvasManager.AddBall(mass: 0.02, radius: 04, x: Center + 100 + 100, y: Middle, vx: -0.36, vy:+1.10, Color.BLUE, "Earth");
            _canvasManager.AddBall(mass: 0.01, radius: 02, x: Center + 100 + 150, y: Middle, vx: -0.36, vy:+1.00, Color.RED, "Mars");
            _canvasManager.AddBall(mass: 0.09, radius: 09, x: Center + 100 + 300, y: Middle, vx: -0.20, vy:+0.90, Color.MAROON, "Jupiter");
            _canvasManager.AddBall(mass: 0.08, radius: 08, x: Center + 100 + 400, y: Middle, vx: -0.15, vy:+0.70, Color.SKYBLUE, "Saturn");
            _canvasManager.AddBall(mass: 0.06, radius: 06, x: Center + 100 + 500, y: Middle, vx: -0.13, vy:+0.60, Color.BROWN, "Uranus");
            _canvasManager.AddBall(mass: 0.05, radius: 05, x: Center + 100 + 600, y: Middle, vx: -0.08, vy:+0.48, Color.LIME, "Neptune");
            //Pluto is no longer a planet!
            //_canvasManager.AddBall(mass: 0.01, radius: 01, x: Center + 100 + 900, y: Middle, vx: -0.07, vy:+0.07, Color.PURPLE);

            Raylib.InitWindow(_canvas.Width, _canvas.Height, "RLSolarSystem");
            
            //Uncomment next line to get a less chaotic animation
            Raylib.SetTargetFPS(60);
            
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                _canvasManager.Render();

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
