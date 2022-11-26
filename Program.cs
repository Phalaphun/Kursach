using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Kursach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }
        static void StartGame()
        {
            GameWindowSettings gSettings = GameWindowSettings.Default;
            NativeWindowSettings nSettings = new NativeWindowSettings()
            {
                Title = "Game",
                Size = (700, 500),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability
            };
            Game game = new Game(gSettings, nSettings);
            game.Run();
        }
    }
}