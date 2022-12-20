using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace Kursach
{
    internal class TextRenderer : IDisposable
    {
        int choosenColumn, choosenRow, rows, columns, textureId;
        float frameWidth, frameHeight, xx = 0.1f, yy = 0.1f;
        List<int> vaoVboindex = new List<int>() { };
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
                LetterRender(x + i * dx, y, asciiCodes[i], out int a, out int b, out int vao, scale);
                vaoVboindex.Add(vao); vaoVboindex.Add(b); vaoVboindex.Add(a);
            }
            
           
        }
        private void LetterRender(float x, float y, byte target,  out int a, out int b, out int vao, float scale = 1)
        {
            choosenColumn = target % rows;
            choosenRow = target / columns;
            //GL.Enable(EnableCap.Texture2D);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            //GL.Enable(EnableCap.Blend);
            //GL.BindTexture(TextureTarget.Texture2D, textureId);
            //GL.Color4(Color4.White);
            //GL.Begin(PrimitiveType.Quads);
            //    GL.TexCoord2(0.0f + frameWidth * (choosenColumn), 0.0f + frameHeight * (choosenRow + 1));
            //    GL.Vertex2(x, y);
            //    GL.TexCoord2(0.0f + frameWidth * (choosenColumn), 0.0f + frameHeight * (choosenRow));
            //    GL.Vertex2(x, y + (yy * scale));
            //    GL.TexCoord2(0.0f + frameWidth * (choosenColumn + 1), 0.0f + frameHeight * (choosenRow));
            //    GL.Vertex2(x + (xx * scale), y + (yy * scale));
            //    GL.TexCoord2(0.0f + frameWidth * (choosenColumn + 1), 0.0f + frameHeight * (choosenRow + 1));
            //    GL.Vertex2(x + (xx * scale), y);
            //GL.End();
            //GL.Disable(EnableCap.Texture2D);
            //GL.Disable(EnableCap.Blend);

            //GL.Color4(Color4.Red);
            //GL.PointSize(2f);
            //GL.Begin(PrimitiveType.LineLoop);
            //GL.Vertex2(x, y);
            //GL.Vertex2(x, y + (0.1f * scale));
            //GL.Vertex2(x + (0.1f * scale), y + (0.1f * scale));
            //GL.Vertex2(x + (0.1f * scale), y);
            //GL.End();

            float[] texCords = { 0.0f + frameWidth * (choosenColumn) , 0.0f + frameHeight * (choosenRow + 1),
                             0.0f + frameWidth * (choosenColumn),0.0f + frameHeight * (choosenRow),
                             0.0f + frameWidth * (choosenColumn + 1), 0.0f + frameHeight * (choosenRow),
                             0.0f + frameWidth * (choosenColumn + 1),0.0f + frameHeight * (choosenRow + 1) };

            float[] letCords = { x, y, x, y + (yy * scale), x + (xx * scale), y + (yy * scale), x + (xx * scale), y };
            a = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, a);
            GL.BufferData(BufferTarget.ArrayBuffer, letCords.Length*sizeof(float),letCords,BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            b = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer,b);
            GL.BufferData(BufferTarget.ArrayBuffer, texCords.Length * sizeof(float), texCords, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, b);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, a);
            GL.VertexPointer(2, VertexPointerType.Float, 0, 0);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);

            //GL.Enable(EnableCap.Texture2D);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            //GL.Enable(EnableCap.Blend);


            //GL.EnableClientState(ArrayCap.VertexArray);
            //GL.EnableClientState(ArrayCap.TextureCoordArray);
            //GL.BindTexture(TextureTarget.Texture2D, textureId);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, b);
            //GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, 0);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, a);
            //GL.VertexPointer(2,VertexPointerType.Float,0,0);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //GL.BindTexture(TextureTarget.Texture2D, 0);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.DisableClientState(ArrayCap.VertexArray);
            //GL.DisableClientState(ArrayCap.TextureCoordArray);

            //GL.Disable(EnableCap.Texture2D);
            //GL.Disable(EnableCap.Blend);


        }

        public void Render()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);

            for (int i = 0; i < vaoVboindex.Count; i += 3)
            {
                GL.BindVertexArray(vaoVboindex[i]);
                GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            }
            //    GL.BindVertexArray(vaoVboindex[0]);
            //    GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindVertexArray(vaoVboindex[3]);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindVertexArray(vaoVboindex[6]);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindVertexArray(vaoVboindex[9]);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindVertexArray(vaoVboindex[12]);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindVertexArray(vaoVboindex[15]);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            //GL.BindVertexArray(vaoVboindex[18]);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.BindVertexArray(0);
            

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
        }
        public void Dispose()
        {
            GL.DeleteTexture(textureId);
            for(int i=0; i<vaoVboindex.Count;i++)
            {
                GL.DeleteBuffer(i);
            }
        }
    }
}