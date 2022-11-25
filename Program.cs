using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Kursach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Grid d = new Grid(5, 2);
            ////d[4, 0] = 5;
            //d[2,1] = 6;
            //d[0, 0] = 1;
            //d[0, 1] = 1;
            //d[1, 0] = 1;
            //d[1, 1] = 1;
            //d[3, 0] = 1;
            //d[3, 1] = 1;
            //d.ClearAllRows();
            StartGame();

            //Console.WriteLine(1%4);
            //Console.WriteLine(2%4);
            //Console.WriteLine(3%4);
            //Console.WriteLine(4%4);

        }

        static void StartGame()
        {
            GameWindowSettings gSettings = GameWindowSettings.Default;

            NativeWindowSettings nSettings = new NativeWindowSettings()
            {
                Title = "Game",
                Size = (700, 500),
                //Flags = ContextFlags.Offscreen | ContextFlags.ForwardCompatible,
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability
            };
            Game game = new Game(gSettings, nSettings);
            game.Run();
        }
    }
}