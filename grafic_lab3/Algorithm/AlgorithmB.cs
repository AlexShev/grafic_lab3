using grafic_lab3.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafic_lab3.Algorithm;

public class AlgorithmB : IAlgorithm
{
    private static readonly Point[] pixelZigns = new Point[]
    {
            new Point(1, 1),   // ( x, y), ( y, x)
            new Point(-1, 1),  // (-x, y), (-y, x)
            new Point(-1, -1), // (-x,-y), (-y,-x)
            new Point(1, -1),  // ( x,-y), ( y,-x)
    };

    private static readonly double[] SupportAngles = new double[]
    {
        Math.PI / 4,
        3*  Math.PI / 4,
        5 * Math.PI / 4,
        7 * Math.PI / 4,
    };

    public AlgorithmExpector? AlgorithmExpector { get; set; }

    public void DrawArc(IBitmap bitmap, Color color, Point center, int R, double alfa, double beta)
    {
        AlgorithmExpector?.Start();

        int x = R, y = 0;
        int d = 3 - 2 * R;

        alfa = alfa * Math.PI / 180;
        beta = beta * Math.PI / 180;

        while (x >= y)
        {
            double phi = Math.PI / 4 - Math.Atan((double) y / x);

            int quarter = 0;

            foreach (var modification in pixelZigns)
            {
                var pixel = new Point(center.X + modification.X * x, center.Y - modification.Y * y);

                double currPhi = SupportAngles[quarter] - (quarter  % 2 == 0 ? 1 : -1) * phi;

                if (alfa <= currPhi && currPhi <= beta)
                {
                    AlgorithmExpector?.IncrimentPixelCounter();
                    bitmap.SetPixel(pixel.X, pixel.Y, color);
                }

                currPhi = SupportAngles[quarter] + (quarter % 2 == 1 ? -1 : 1) * phi;

                pixel = new Point(center.X + modification.X * y, center.Y - modification.Y * x);

                if (alfa <= currPhi && currPhi <= beta)
                {
                    AlgorithmExpector?.IncrimentPixelCounter();
                    bitmap.SetPixel(pixel.X, pixel.Y, color);
                }

                ++quarter;
            }

            if (d < 0)
            {
                d += 4 * y + 6;
                ++y;
            }
            else
            {
                d += 4 * (y - x) + 10;
                --x;
                ++y;
            }
        }

        AlgorithmExpector?.Stop();
    }

    public void FillCircle(IBitmap bitmap, Color color, Color border, Point center, int R)
    {
        AlgorithmExpector?.Start();

        int x = R, y = 0;
        int D = 2 * (1 - R);

        while (x >= 0)
        {
            draw_pixels(bitmap, color, border, center.X - x, center.X + x, center.Y + y);
            draw_pixels(bitmap, color, border, center.X - x, center.X + x, center.Y - y);

            if (D < 0 && 2 * D + 2 * x - 1 <= 0)
            {
                ++y;
                D += 2 * y + 1;
            }
            else if (D > 0 && 2 * D - 2 * y - 1 >= 0)
            {
                --x;
                D -= 2 * x - 1;
            }
            else
            {
                --x;
                ++y;
                D += 2 * y - 2 * x + 2;
            }
        }

        AlgorithmExpector?.Stop();
    }

    void draw_pixels(IBitmap bitmap, Color color, Color border, int x1, int x2, int y)
    {
        for (int x = x1; x < x2; x++)
        {
            if (border.ToArgb() != bitmap.GetPixel(x, y).ToArgb())
            {
                AlgorithmExpector?.IncrimentPixelCounter();
                bitmap.SetPixel(x, y, color);
            }
        }
    }
}
