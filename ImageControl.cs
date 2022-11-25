using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    enum BlockColor
    {
        Cyan,
        Blue,
        Orange,
        Yellow,
        Green,
        Purpule,
        Red,
    }

    internal class ImageControl
    {
        private Cell[][] grid1;
        internal Cell[][] Grid1 { get => grid1; set => grid1 = value; }
        public ImageControl(int m, int n, int scale, int a)
        {
            Grid1 = new Cell[n][];
            for (int i = 0; i < n; i++)
            {
                Grid1[i] = new Cell[m];
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Grid1[i][j] = new Cell(j, i, scale);
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
        int x, y, x1, y1, scale;
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

        public void Draw()
        {
            //GL.Rect(x * scale, y * scale, x1 * scale, y1 * scale);
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(x * (scale), y * (scale));
            GL.Vertex2(x1 * (scale), y * (scale));
            GL.Vertex2(x1 * (scale), y1 * (scale));
            GL.Vertex2(x * (scale), y1 * (scale));
            GL.End();
        }

        public void ChangeColor(Vector3 c)
        {
            GL.Color3(c);
            GL.Rect(x * scale, y * scale, x1 * scale, y1 * scale);
        }



    }
}
