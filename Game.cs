using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kursach
{
    internal class Game : GameWindow
    {
        int width, height, previouseScores, r, dr;
        double fiX, fiY, ortoWidth,ortoHeight;
        double lag = 0;
        double TIME_PER_FRAME = 0.45;
        CircleCells circleCells;
        GameStatus gameState;
        Vector2 cursorPosition, Center;
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
            new Vector3(1,1,1),//white
        };
        public Game(GameWindowSettings gSettings, NativeWindowSettings nSettings) : base(gSettings, nSettings)
        {

        }
        protected override void OnLoad()
        {
            base.OnLoad();
            width = 12;  // при 800 на 600 тут 40 - ширина
            height = 22; // при 800 на 600 тут 30 - высота
            ortoHeight = 1200;
            ortoWidth = 1600;
            r = 60;
            dr = 20;
            Center = new Vector2(510, 510);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, ortoWidth, 0, ortoHeight, -1, 1); // 0;0 находится в левом нижнем углу. У направлена вверх, х - направо
            GL.MatrixMode(MatrixMode.Modelview);
            gameState = new GameStatus(height, width);
            circleCells = new CircleCells(height, width, Center, r,dr);
            figures.Add(new Button(200,250,100,250, Color4.DarkOrange));
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            {
                GL.PointSize(5f);
                GL.Color4(Color4.Red);
                GL.Begin(PrimitiveType.Points);
                GL.Vertex2(Center);
                GL.End();
            }

            DrawAll(gameState, circleCells);

            if (gameState.GameOver)
            {
                foreach (VisualFigure figure in figures)
                    figure.Draw();
            }

            GL.Color4(Color4.BlueViolet);
            GL.PointSize(10f);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(cursorPosition);
            GL.End();

            SwapBuffers();
        }
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
                        if (previouseScores < gameState.Scores)
                        {
                            TIME_PER_FRAME -= 0.01;
                        }
                    }
                }

            }
            base.OnUpdateFrame(args);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            fiX = e.Width / ortoWidth;
            fiY = e.Height / ortoHeight;
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
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
            for (int i = 0; i < figures.Count; i++)
            {
                if (figures[i].IsPointInFigure(cursorPosition))
                    figures[i].OnMouseDown?.Invoke(e);
            }

        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            cursorPosition = new Vector2((float)(e.Position.X/fiX), (float)ortoHeight - (float)(e.Position.Y/fiY));
            
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }
        private void DrawCircleCells(CircleCells d, Grid grid)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int id = grid[i, j];
                    GL.Color3(ColorImages[id]);
                    d.Grid1[i][j].Draw();
                }
            }
        }
        private void DrawActiveBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                GL.Color3(ColorImages[block.Id]);
                circleCells.Grid1[p.Row][p.Column].Draw();
            }
        }
        private void DrawAll(GameStatus gameState, CircleCells d)
        {
            DrawCircleCells(circleCells, gameState._Grid);
            DrawActiveBlock(gameState.CurrentBlock);
        }
    }
}