using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;

namespace Kursach
{
    internal class Game : GameWindow
    {
        int cellSize;
        int w;
        int h;
        int N;
        int M;
        double fi;
        double fiY;
        ImageControl imageControls;
        GameStatus gameState;
        Vector2 cursorPosition;
        List<VisualFigure> figures = new List<VisualFigure>();
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
            N = 12;  // при 800 на 600 тут 40 - ширина
            M = 22; // при 800 на 600 тут 30 - высота
            cellSize = 20;
            w = cellSize * N;
            h = cellSize * M;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 800, 0, 600, -1, 1); // 0;0 находится в левом нижнем углу. У направлена вверх, х - направо
            GL.MatrixMode(MatrixMode.Modelview);
            gameState = new GameStatus(M, N);
            imageControls = new ImageControl(M, N, cellSize, true);
            figures.Add(new Button(200,250,100,250, Color4.DarkOrange));


            //if (( (float) this.ClientSize.X / this.ClientSize.Y) < 800.0 / 600.0) { 
            
            //    fi = this.ClientSize.X / 800.0f;
            //}
            //else
            //{
            //    fi = this.ClientSize.Y / 600.0f;
            //}
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        protected override void OnResize(ResizeEventArgs e)
        {

            //if (((float)e.Width / e.Height) < 800.0 / 600.0)
            {

                fi = e.Width / 800.0f;
            }
            //else
            {
                fiY = e.Height / 600.0f;
                
            }

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
            if (gameState.GameOver)
            {
                //GL.Color3(128 / 255f, 128 / 255f, 128 / 255f);
                //GL.Rect(100, 100, 600, 370);
                //GL.Color3(1, 1, 0);
                //GL.Rect(200, 250, 500, 300);
                //GL.Rect(200, 170, 500, 220);
                foreach (VisualFigure figure in figures)
                    figure.Draw();
            }

            GL.PointSize(10f);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(cursorPosition);
            GL.End();

            SwapBuffers();
        }
        double lag = 0;
        double TIME_PER_FRAME = 0.45;
        int previouseScores;
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (!gameState.GameOver)
            {
                
                lag += args.Time;
                if (lag > TIME_PER_FRAME)
                {
                    while (lag > TIME_PER_FRAME)
                    {
                        previouseScores = gameState.Scores;
                        gameState.MoveBlockDown();
                        this.Title = "Tetris        Scores: " + gameState.Scores.ToString();
                        lag -= TIME_PER_FRAME;
                        if(previouseScores < gameState.Scores)
                        {
                            TIME_PER_FRAME -= 0.01;
                        }
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
            if (gameState.GameOver)
                return;
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
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if(gameState.GameOver)
            {
                //if(cursorPosition.X>200 && cursorPosition.X<500 && cursorPosition.Y>250&& cursorPosition.Y<300)
                //{
                //    this.Close();
                //}
                //else if()
                //{ }
                GL.Color3(1, 0, 0);
                GL.PointSize(100f);
                GL.Begin(PrimitiveType.Points);
                GL.Vertex2(cursorPosition.X,cursorPosition.Y);
                GL.End();
            }

            
            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i].IsPointInFigure(cursorPosition))
                    figures[i].OnMouseDown?.Invoke(e);
            }

        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            //cursorPosition = new Vector2((float)(e.Position.X/fi), 600-(float)(e.Position.Y*6/5));
            cursorPosition = new Vector2((float)(e.Position.X/fi), 600-(float)(e.Position.Y/fiY));
            
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

    }
}