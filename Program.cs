using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Kursach{
    internal class Program {
        static void Main(string[] args){
            StartGame();
        }
        static void StartGame(){

            GameWindowSettings gSettings = GameWindowSettings.Default; // Устанавливаются начальные настройки. Там частота рендера апдейта и многопоточность.
            NativeWindowSettings nSettings = new NativeWindowSettings(){ // устанавливаются другие настройки. Которые касаются уже непосредственно окна
                Title = "Game",
                Size = (1280, 1024),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability
            };
            Game game = new Game(gSettings, nSettings);
            game.Run();
        }
    }
}