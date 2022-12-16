using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework; //795
using System.Xml;

namespace Kursach
{
    internal class Game : GameWindow
    {
        int width, height, previouseScores, r, dr, textureId;
        double fiX, fiY, ortoWidth,ortoHeight;
        double lag = 0, TIME_PER_FRAME = 0.45;
        bool pause=false;
        GameStatus gameState;
        Vector2 cursorPosition, centerPoint;
        Button restart = new Button(1200, 800, 300, 200, Color4.Gray);
        Button close = new Button(1200, 500, 300, 200, Color4.Gray);
        List<VisualFigure> figures = new List<VisualFigure>();
        private Vector3[] ColorMass = new Vector3[]
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
        TextRenderer tr;
        public Game(GameWindowSettings gSettings, NativeWindowSettings nSettings) : base(gSettings, nSettings)
        {

        }
        protected override void OnLoad()
        {
            base.OnLoad();
            width = 12;  
            height = 22; 
            ortoHeight = 1200;
            ortoWidth = 1600;
            r = 60;
            dr = 20;
            centerPoint = new Vector2(510, 510);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, ortoWidth, 0, ortoHeight, -1, 1); // 0;0 находится в левом нижнем углу. У направлена вверх, х - направо
            GL.MatrixMode(MatrixMode.Modelview);
            gameState = new GameStatus(height, width, centerPoint, r, dr);
            textureId = ContentPipe.LoadTexture(@"Content\Consolas_Alpha_W.png");
            tr = new TextRenderer(16, 16, textureId, (float)ortoWidth, (float)ortoHeight);
            figures.Add(restart);
            figures.Add(close);
            close.OnMouseDown += CloseEvent;
            restart.OnMouseDown += Restart;
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
                GL.PointSize(5f);
                GL.Color4(Color4.Red);
                GL.Begin(PrimitiveType.Points);
                GL.Vertex2(centerPoint);
                GL.End();
            DrawAll(gameState);
            foreach (VisualFigure figure in figures)
                figure.Draw();
            GL.Color4(Color4.BlueViolet);
            GL.PointSize(10f);
            GL.Begin(PrimitiveType.Points);
                GL.Vertex2(cursorPosition);
            GL.End();
            tr.TextRender(1280, 880, 20, "RESTART", 0.9f);
            tr.TextRender(1280, 580, 25, "CLOSE", 0.9f);
            tr.TextRender(100, 1100, 25, "SCORE:" + gameState.Scores.ToString(), 0.9f);
            if(pause) { tr.TextRender(1000, 950, 10, "P", 3f); }
            if(gameState.GameOver)
            {
                GL.Color4(Color4.SlateGray);
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex2(500, 300);
                GL.Vertex2(500, 300 + 500);
                GL.Vertex2(500 + 500, 300 + 500);
                GL.Vertex2(500 + 500, 300);
                GL.End();
                tr.TextRender(640, 530, 25, "GAME OVER", 0.9f);
            }
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (!pause)
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
                                TIME_PER_FRAME -= 0.03;
                            }
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
                    case Keys.P:; pause = pause ? false : true; break;
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
        private void DrawCircleCells(GameStatus gameState) // а на кой я сюда вообще параметр передаю если могу брать глобальный?
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    GL.Color3(ColorMass[gameState.CircleCell[i, j]]);
                    gameState.CircleCell.Cells[i][j].Draw();
                }
            }
        }
        private void DrawActiveBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                GL.Color3(ColorMass[block.Id]);
                gameState.CircleCell.Cells[p.Row][p.Column].Draw();
            }
        }
        private void DrawAll(GameStatus gameState)
        {
            DrawCircleCells(gameState);
            DrawActiveBlock(gameState.CurrentBlock);
        }
        private void Restart(MouseButtonEventArgs e)
        {
            gameState = new GameStatus(height, width, centerPoint, r, dr);
        }
        private void CloseEvent(MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}