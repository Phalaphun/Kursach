using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
namespace Kursach
{
    internal class ImageControl
    {
        private Cell[][] grid1;
        internal Cell[][] Grid1 { get => grid1; set => grid1 = value; }
        public ImageControl(int m, int n, int scale, Vector2 Center)
        {
            Grid1 = new Cell[m][];
            for (int i = m - 1; i >= 0; i--)
            {
                Grid1[i] = new Cell[n];
            }

            for(int i = m - 1; i >= 0; i--)
            {
                for(int j=0;j<n;j++)
                {
                    Grid1[i][j] = new Cell(Center, j, i, 60, 20);
                }
            }

        }

        public ImageControl(int m, int n, int scale, bool a)
        {
            Grid1 = new Cell[m][];
            for (int i = m - 1; i >= 0; i--)
            {
                Grid1[i] = new Cell[n];
            }
            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    Grid1[i][j] = new Cell(j, Math.Abs(i - m + 1), scale);
                }
            }
        }
    }
    internal class Cell
    {
        float x, y, x1, y1, scale;
        float x2, y2, x3, y3, x4, y4, x5, y5;
        int id;

        public int Id { get { return id; } set { id = value; } }
        public Cell(int x, int y, int scale)
        {
            this.scale = scale;
            this.x = x;
            this.y = y;
            x1 = x + 1;
            y1 = y + 1;
        }
        public Cell(Vector2 Center, int j, int i, int r, int k)
        {
            float dAlpha =  2 * (float)Math.PI / 12;
            x2 = Center.X + (r + i * k) * (float)Math.Cos(j * dAlpha);
            y2 = Center.Y + (r + i * k) * (float)Math.Sin(j * dAlpha);

            x3 = Center.X + (r + i * k + k) * (float)Math.Cos(j * dAlpha);
            y3 = Center.Y + (r + i* k + k) * (float)Math.Sin(j * dAlpha);

            x4 = Center.X + (r + i * k + k) * (float)Math.Cos((j + 1) * dAlpha);
            y4 = Center.Y + (r + i * k + k) * (float)Math.Sin((j + 1) * dAlpha);

            x5 = Center.X + (r + i * k) * (float)Math.Cos((j + 1) * dAlpha);
            y5 = Center.Y + (r + i * k) * (float)Math.Sin((j + 1) * dAlpha);

        }
        public void Draw()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(x2,y2);
            GL.Vertex2(x3,y3);
            GL.Vertex2(x4,y4);
            GL.Vertex2(x5,y5);
            GL.End();

            GL.Color4(Color4.Red);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(x2, y2);
            GL.Vertex2(x3, y3);
            GL.Vertex2(x4, y4);
            GL.Vertex2(x5, y5);
            GL.End();
        }
    }
}