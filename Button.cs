using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
namespace Kursach{
    delegate void MouseDelegate();
    internal class Button{
        float x, y, width, height;
        Color4 color; float[] temp;
        int bufferId;
        public MouseDelegate OnMouseDown;
        public Button(float x, float y, float width, float height, Color4 color){
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
            temp = new float[] {x,y,x,y+height,x+width,y+height,x+width,y };
            bufferId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, temp.Length * sizeof(float), temp, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        public void Draw(){
            GL.Color4(color);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
            GL.VertexPointer(2, VertexPointerType.Float, 0, 0);
            GL.DrawArrays(PrimitiveType.Polygon, 0, 4);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
        }
        public bool IsPointInFigure(float pointX, float pointY){
            if (pointX < x) return false;
            if (pointY < y) return false;
            if (pointX > x + width) return false;
            if (pointY > y + height) return false;
            return true;
        }
        public bool IsPointInFigure(Vector2 point){
            return IsPointInFigure(point.X, point.Y);
        }
    }
}