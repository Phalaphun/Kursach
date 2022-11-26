using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common.Input;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kursach
{
    internal class Game : GameWindow
    {
        int cellSize;
        int w;
        int h;
        int N;
        int M;
        ImageControl imageControls;
        GameStatus gameState;

        private Vector3[] ColorImages = new Vector3[]
        {
            new Vector3(0,0,0), //black
            new Vector3(0,255/255f,255/255f), //cyan
            new Vector3(0f,0f,255/255f), //blue
            new Vector3(255/255f,165/255f,0), //orange
            new Vector3(255/255f,255/255f,0), //yellow
            new Vector3(0,128/255f,0),//green
            new Vector3(238/255f,130/255f,238/255f),//purpule
            new Vector3(255/255f,0,0),//red
        };
        

        public Game(GameWindowSettings gSettings, NativeWindowSettings nSettings) : base(gSettings, nSettings)
        {

        }
        protected override void OnLoad()
        {
            base.OnLoad();
            N = 12;
            M = 20;
            cellSize = 22;
            w = cellSize * N;
            h = cellSize * M;
            gameState = new GameStatus(M, N);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 700, 0, 500, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);

            imageControls = new ImageControl(M, N, cellSize, true);


            

            //запускается только в начале. начальные настройки можно сюда поставить
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
 

            DrawAll(gameState, imageControls);
            DrawRedLine();
            SwapBuffers();
        }


        double lag = 0;
        double TIME_PER_FRAME = 0.45;
        protected override void OnUpdateFrame(FrameEventArgs args)
        {

            if (!gameState.GameOver)
            {
                lag += args.Time;
                if (lag > TIME_PER_FRAME)
                {
                    while (lag > TIME_PER_FRAME)
                    {
                        gameState.MoveBlockDown();
                        lag -= TIME_PER_FRAME;
                    }
                }
            }


            base.OnUpdateFrame(args);
        }


        private void SetupGameCanvas(ImageControl d, Grid grid)
        {
            
            
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {

                    int id = grid[i, j];
                    GL.Color3(ColorImages[id]);
                    d.Grid1[i][j].Draw();
                }
            }
        }

        private void DrawRedLine()
        {
            GL.Color3(1.0, 0.0, 0.0); // цвет наших линий, в данном слуае - красный
            GL.Begin(PrimitiveType.Lines); // начинаем рисовать и указываем, что это линии
            for (int i = 0; i < w; i += cellSize) // отрисовываем линии в ширину
            {
                GL.Vertex2(i, 0); GL.Vertex2(i, h); // рисуем прямую
            }
            for (int j = 0; j < h; j += cellSize) //отрисовываем линии в высоту
            {
                GL.Vertex2(0, j); GL.Vertex2(w, j); // рисуем ту же самую прямую, но в другом направлении
            }

            GL.Vertex2(0, h);
            GL.Vertex2(w, h);

            GL.Vertex2(w, 0);
            GL.Vertex2(w, h);



            GL.End(); // конец отрисовки
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                GL.Color3(ColorImages[block.Id]);
                imageControls.Grid1[p.Row][p.Column].Draw();
            }
        }

        private void DrawAll(GameStatus gameState, ImageControl d)
        {
            SetupGameCanvas(imageControls, gameState._Grid);
            DrawBlock(gameState.CurrentBlock);
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if(gameState.GameOver)
            {
                return;
            }
            else
            {
                switch (e.Key)
                {
                    case Keys.Left: gameState.MoveBlockLeft(); break;
                    case Keys.Right: gameState.MoveBlockRight(); break;
                    case Keys.Up: gameState.RotateBlockCW(); break;
                    case Keys.Down: gameState.MoveBlockDown(); break;
                    case Keys.P:; break;
                }
            }


            
        }


    }
}
