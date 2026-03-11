using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace LibraryAIS
{
    public class CaptchaGenerator
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string CaptchaText { get; private set; }

        public Bitmap GenerateCaptchaImage(int width = 200, int height = 70)
        {
            // Генерация случайной строки из 4 символов
            CaptchaText = GenerateRandomText(4);

            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Заливка фона
            graphics.Clear(Color.White);

            // Добавление шума - точки
            AddNoise(graphics, width, height);

            // Рисование символов на разных уровнях
            DrawText(graphics, width, height);

            // Добавление линий помех
            AddNoiseLines(graphics, width, height);

            graphics.Dispose();

            return bitmap;
        }

        private string GenerateRandomText(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void DrawText(Graphics graphics, int width, int height)
        {
            Font[] fonts = new Font[]
            {
                new Font("Arial", 20, FontStyle.Bold | FontStyle.Italic),
                new Font("Verdana", 20, FontStyle.Bold),
                new Font("Times New Roman", 20, FontStyle.Bold | FontStyle.Italic),
                new Font("Georgia", 20, FontStyle.Bold)
            };

            Brush[] brushes = new Brush[]
            {
                new SolidBrush(Color.FromArgb(0, 0, 139)),
                new SolidBrush(Color.FromArgb(139, 0, 0)),
                new SolidBrush(Color.FromArgb(0, 100, 0)),
                new SolidBrush(Color.FromArgb(139, 69, 19))
            };

            float charWidth = width / (CaptchaText.Length + 1);

            for (int i = 0; i < CaptchaText.Length; i++)
            {
                // Случайная позиция по вертикали для каждого символа
                float x = charWidth * (i + 0.5f);
                float y = random.Next(height / 4, height / 2);

                // Поворот символа
                graphics.TranslateTransform(x, y);
                graphics.RotateTransform(random.Next(-30, 30));

                // Рисуем символ
                graphics.DrawString(
                    CaptchaText[i].ToString(),
                    fonts[random.Next(fonts.Length)],
                    brushes[random.Next(brushes.Length)],
                    0, 0
                );

                graphics.ResetTransform();
            }

            // Освобождение ресурсов
            foreach (var font in fonts) font.Dispose();
            foreach (var brush in brushes) brush.Dispose();
        }

        private void AddNoise(Graphics graphics, int width, int height)
        {
            // Добавление случайных точек
            for (int i = 0; i < width * height / 50; i++)
            {
                int x = random.Next(width);
                int y = random.Next(height);
                int w = random.Next(3);
                int h = random.Next(3);

                graphics.FillEllipse(
                    new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))),
                    x, y, w, h
                );
            }
        }

        private void AddNoiseLines(Graphics graphics, int width, int height)
        {
            // Добавление перечеркивающих линий
            Pen pen = new Pen(Color.Gray, 2);

            for (int i = 0; i < 5; i++)
            {
                Point point1 = new Point(random.Next(width), random.Next(height));
                Point point2 = new Point(random.Next(width), random.Next(height));

                graphics.DrawLine(pen, point1, point2);
            }

            // Добавление волнистых линий
            pen = new Pen(Color.LightGray, 1);
            for (int i = 0; i < 3; i++)
            {
                Point[] points = new Point[width / 10];
                for (int j = 0; j < points.Length; j++)
                {
                    points[j] = new Point(j * 10, random.Next(height));
                }
                graphics.DrawCurve(pen, points);
            }

            pen.Dispose();
        }
    }
}