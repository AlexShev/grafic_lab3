using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafic_lab3.Image;

public class BitmapComporator
{
    public static Bitmap Compare(IBitmap first, IBitmap second)
    {
        Bitmap bitmap = new Bitmap(first.Width, first.Height);

        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                Color pixelFirst = first.GetPixel(x, y);
                Color pixelSecond = second.GetPixel(x, y);

                if (pixelFirst.ToArgb() == pixelSecond.ToArgb())
                {
                    bitmap.SetPixel(x, y, pixelFirst);
                }
                //else
                //{
                //    int r = (pixelFirst.R + pixelSecond.R) / 2;
                //    int g = (pixelFirst.G + pixelSecond.G) / 2;
                //    int b = (pixelFirst.B + pixelSecond.B) / 2;
                //    int a = (pixelFirst.A + pixelSecond.A) / 2;

                //    bitmap.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                //}
                else if (pixelFirst.ToArgb() != first.BackGround.ToArgb())
                {
                    bitmap.SetPixel(x, y, Color.Blue);
                }
                else if (pixelSecond.ToArgb() != second.BackGround.ToArgb())
                {
                    bitmap.SetPixel(x, y, Color.Red);
                }
            }
        }

        return bitmap;
    }
}
