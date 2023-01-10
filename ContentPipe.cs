using System.Drawing.Imaging;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace Kursach
{
    internal class ContentPipe{
        static public int LoadTexture(string path){
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found at '{path}'");
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            Bitmap bmp = new Bitmap(path); //Превращаем всё в пиксели
            BitmapData data = bmp.LockBits( new Rectangle(0, 0, bmp.Width, bmp.Height), // блокируем биты и создаём прямоугольник с размерами.
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb); // Ставим режим картинки только чтение, а тамже даём понять какой битности картинка - 32
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,data.Width, data.Height, 0, // выбираем формати пикселя или его уветовые компаненты, в конструкторе класса было сказано сделать 0.
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, // Ещё раз указываем формат уже передаваемых данных
                PixelType.UnsignedByte, // выделяем 1 байт под цвет
                data.Scan0); // отправляем битмапдату и выбираем там первый пиксель
            bmp.UnlockBits(data); // освобождаем биты
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp); // указываем что делать если текстуры не хватило на объект. В данном случае прижимаеи текстуру к посленему пикчелю на границе.по разным осям
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear); //Функция уменьшения текстуры при масштабировании
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear); //Функция увеличения текстуры при масштабировании. на основе 5 точек рядом с рассматрвиаемой точкой
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return id;
        }

    }
}