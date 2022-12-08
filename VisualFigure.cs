using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    delegate void MouseDelegate(MouseButtonEventArgs e);
    abstract class VisualFigure
    {
        public MouseDelegate OnMouseDown;
        abstract public void OnMouseUp(MouseButtonEventArgs e);
        abstract public void OnMouseMove(MouseMoveEventArgs e);
        abstract public void Draw();
        abstract public bool IsPointInFigure(float pointX, float pointY);
        abstract public bool IsPointInFigure(Vector2 point);

    }
}
