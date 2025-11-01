using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// 1. GARANTA QUE O NAMESPACE É O PRINCIPAL:
namespace Gerenciamento_De_Chamados
{
    public class RoundedPanel : Panel
    {
        public RoundedPanel()
        {
            this.BackColor = Color.White;
            // Impede que o painel base pinte o fundo para evitar bordas quadradas
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }
        // (O resto do seu código permanece o mesmo)
        public float CornerRadius { get; set; } = 15f;
        public Color BorderColor { get; set; } = Color.White;
        public float BorderWidth { get; set; } = 1f;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Não chame base.OnPaint(e) para ter controle total sobre o desenho
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, CornerRadius))
            {
                // Define a região de recorte do painel para os cantos arredondados
                this.Region = new Region(path);

                // Preenche o fundo do painel
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Desenha a borda
                if (BorderWidth > 0)
                {
                    using (Pen pen = new Pen(BorderColor, BorderWidth))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2;

            // Garante que o diâmetro não seja maior que o retângulo
            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            // Cria um retângulo um pouco menor para a borda não ser cortada
            RectangleF innerRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            if (BorderWidth > 1)
            {
                innerRect = new RectangleF(rect.X + BorderWidth / 2, rect.Y + BorderWidth / 2, rect.Width - BorderWidth, rect.Height - BorderWidth);
                diameter = (radius - (BorderWidth / 2)) * 2;
                if (diameter > innerRect.Width) diameter = innerRect.Width;
                if (diameter > innerRect.Height) diameter = innerRect.Height;
            }
            
            if (diameter <= 0)
            {
                path.AddRectangle(innerRect);
                return path;
            }

            // Adiciona os arcos dos cantos
            path.AddArc(innerRect.X, innerRect.Y, diameter, diameter, 180, 90); // Canto superior esquerdo
            path.AddArc(innerRect.Right - diameter, innerRect.Y, diameter, diameter, 270, 90); // Canto superior direito
            path.AddArc(innerRect.Right - diameter, innerRect.Bottom - diameter, diameter, diameter, 0, 90); // Canto inferior direito
            path.AddArc(innerRect.X, innerRect.Bottom - diameter, diameter, diameter, 90, 90); // Canto inferior esquerdo
            path.CloseFigure(); // Fecha o caminho

            return path;
        }
    }
}