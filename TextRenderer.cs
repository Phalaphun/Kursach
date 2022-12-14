using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Text;
namespace Kursach
{
    internal class TextRenderer
    {
        int choosenColumn, choosenRow, rows, columns, textureId;
        float frameWidth, frameHeight, xx = 0.1f, yy = 0.1f;
        public TextRenderer(int rows, int columns, int textureId, float canvaWidth, float canvaHeight)
        {
            this.rows = rows;
            this.columns = columns;
            frameHeight = 1.0f / rows;
            frameWidth = 1.0f / columns;
            this.textureId = textureId;
            xx *= canvaWidth / 4;
            yy *= canvaHeight / 4;
        }
        public TextRenderer(int rows, int columns, int textureId)
        {
            this.rows = rows;
            this.columns = columns;
            frameHeight = 1.0f / rows;
            frameWidth = 1.0f / columns;
            this.textureId = textureId;
        }
        public void TextRender(float x, float y, float dx, string text, float scale = 1)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            byte[] asciiCodes = encoding.GetBytes(text);
            for (int i = 0; i < text.Length; i++)
            {
                LetterRender(x + i * dx, y, asciiCodes[i], scale);
            }
        }
        private void LetterRender(float x, float y, byte target, float scale = 1)
        {
            choosenColumn = target % rows;
            choosenRow = target / columns;
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0.0f + frameWidth * (choosenColumn), 0.0f + frameHeight * (choosenRow + 1));
                GL.Vertex2(x, y);
                GL.TexCoord2(0.0f + frameWidth * (choosenColumn), 0.0f + frameHeight * (choosenRow));
                GL.Vertex2(x, y + (yy * scale));
                GL.TexCoord2(0.0f + frameWidth * (choosenColumn + 1), 0.0f + frameHeight * (choosenRow));
                GL.Vertex2(x + (xx * scale), y + (yy * scale));
                GL.TexCoord2(0.0f + frameWidth * (choosenColumn + 1), 0.0f + frameHeight * (choosenRow + 1));
                GL.Vertex2(x + (xx * scale), y);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
            //GL.Color4(Color4.Red);
            //GL.PointSize(2f);
            //GL.Begin(PrimitiveType.LineLoop);
            //GL.Vertex2(x, y);
            //GL.Vertex2(x, y + (0.1f * scale));
            //GL.Vertex2(x + (0.1f * scale), y + (0.1f * scale));
            //GL.Vertex2(x + (0.1f * scale), y);
            //GL.End();
        }
    }
}