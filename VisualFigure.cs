using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
namespace Kursach
{
    delegate void MouseDelegate(MouseButtonEventArgs e);
    abstract class VisualFigure
    {
        public MouseDelegate OnMouseDown;
        abstract public void Draw();
        abstract public bool IsPointInFigure(float pointX, float pointY);
        abstract public bool IsPointInFigure(Vector2 point);
    }
}