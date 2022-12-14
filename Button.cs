using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Kursach
{
    internal class Button : VisualFigure
    {
        float x, y, width, height;
        Color4 color;
        public Button(float x, float y, float width, float height, Color4 color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }
        public override void Draw()
        {
            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(x, y);
            GL.Vertex2(x, y + height);
            GL.Vertex2(x + width, y + height);
            GL.Vertex2(x + width, y);
            GL.End();
        }
        public override bool IsPointInFigure(float pointX, float pointY)
        {
            if (pointX < x) return false;
            if (pointY < y) return false;
            if (pointX > x + width) return false;
            if (pointY > y + height) return false;
            return true;
        }
        public override bool IsPointInFigure(Vector2 point)
        {
            return IsPointInFigure(point.X, point.Y);
        }
    }
}