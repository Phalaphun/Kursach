using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
namespace Kursach
{
    internal class CircleCells
    {
        private Cell[][] grid1;
        internal Cell[][] Grid1 { get => grid1; set => grid1 = value; }
        public CircleCells(int height, int width, Vector2 Center, int r, int dr)
        {
            Grid1 = new Cell[height][];
            for (int i = height - 1; i >= 0; i--)
            {
                Grid1[i] = new Cell[width];
            }
            for(int i = height - 1; i >= 0; i--)
            {
                for(int j=0;j<width;j++)
                {
                    Grid1[i][j] = new Cell(Center, j, i, r, dr, width); //60,20
                }
            }
        }
    }
    internal class Cell
    {
        float x2, y2, x3, y3, x4, y4, x5, y5;
        int id;
        public int Id { get { return id; } set { id = value; } }
        public Cell(Vector2 Center, int j, int i, int r, int dr, int width)
        {
            float dAlpha =  2 * (float)Math.PI / width;
            x2 = Center.X + (r + i * dr) * (float)Math.Cos(j * dAlpha); // r - внутренний радиус, к-внешний(хотя скорее это ∆r, иначе говоря шаг), i- номер круга, j- число блоков в круге
            y2 = Center.Y + (r + i * dr) * (float)Math.Sin(j * dAlpha);

            x3 = Center.X + (r + i * dr + dr) * (float)Math.Cos(j * dAlpha);
            y3 = Center.Y + (r + i* dr + dr) * (float)Math.Sin(j * dAlpha);

            x4 = Center.X + (r + i * dr + dr) * (float)Math.Cos((j + 1) * dAlpha);
            y4 = Center.Y + (r + i * dr + dr) * (float)Math.Sin((j + 1) * dAlpha);

            x5 = Center.X + (r + i * dr) * (float)Math.Cos((j + 1) * dAlpha);
            y5 = Center.Y + (r + i * dr) * (float)Math.Sin((j + 1) * dAlpha);
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