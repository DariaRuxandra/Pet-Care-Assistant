using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Linq;
using Pet_Care_Assistant.Models;

namespace Pet_Care_Assistant.Views
{
    public class PriceChartDrawable : IDrawable
    {
        public List<Service> Services { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Services == null || !Services.Any()) return;

            // Setări generale
            double maxPrice = Services.Max(s => s.Price);

            // 1. DECLARAREA și ATRIBUIREA valorilor de bază:
            float padding = 20;
            float chartBottom = dirtyRect.Height - 30;
            float maxHeight = dirtyRect.Height - 60;

            // 2. CALCULUL lățimii dinamice:
            float numberOfBars = Services.Count;
            float availableWidth = dirtyRect.Width - (2 * padding);

            float totalSpacePerBar = availableWidth / numberOfBars;

            // Lățimea barei și spațierea sunt calculate din spațiul total alocat
            float barWidth = totalSpacePerBar * 0.7f;
            float barSpacing = totalSpacePerBar * 0.3f;

            float currentX = padding;

            canvas.FontColor = Colors.Black;
            canvas.FontSize = 10;

            // Desenarea barelor
            for (int i = 0; i < Services.Count; i++)
            {
                var service = Services[i];

                // 1. Calcul înălțime
                float barHeight = (float)(service.Price / maxPrice) * maxHeight;
                float barY = chartBottom - barHeight;

                // 2. Setare culoare (Paletă fixă)
                Color barColor;
                switch (i % 6)
                {
                    case 0: barColor = Colors.SteelBlue; break;
                    case 1: barColor = Colors.MediumSeaGreen; break;
                    case 2: barColor = Colors.DarkOrange; break;
                    case 3: barColor = Colors.MediumPurple; break;
                    case 4: barColor = Colors.IndianRed; break;
                    case 5: barColor = Colors.Teal; break;
                    default: barColor = Colors.Gray; break;
                }
                canvas.FillColor = barColor;

                // 3. Desenare bară
                canvas.FillRectangle(currentX, barY, barWidth, barHeight);

                // 4. Desenare label preț (deasupra barei)
                canvas.DrawString($"{service.Price:F0}", currentX, barY - 18, barWidth, 15, HorizontalAlignment.Center, VerticalAlignment.Top);

                // 5. Desenare label nume (sub axa X) - Rotit și Scurtat
                string fullName = service.Name;
                string shortName;

                var words = fullName.Split(' ');

                // Logică de scurtare: ia primele două cuvinte + "..." dacă sunt mai mult de două
                if (words.Length > 2)
                {
                    shortName = $"{words[0]} {words[1]}...";
                }
                else
                {
                    shortName = fullName;
                }

                // Rotirea textului
                canvas.SaveState();
                canvas.Translate(currentX + barWidth / 2, chartBottom + 5);
                canvas.Rotate(45);
                canvas.DrawString(shortName, -barWidth / 2, 0, barWidth, 30, HorizontalAlignment.Left, VerticalAlignment.Top);
                canvas.RestoreState();

                // Trecere la următoarea poziție X
                currentX += barWidth + barSpacing;
            }

            // 6. Desenare linie de bază (Axa X)
            canvas.StrokeColor = Colors.DarkGray;
            canvas.DrawLine(0, chartBottom, dirtyRect.Width, chartBottom);
        }
    }
}