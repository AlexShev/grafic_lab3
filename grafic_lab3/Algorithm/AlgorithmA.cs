using grafic_lab3.Image;

namespace grafic_lab3.Algorithm;

public class AlgorithmA : IAlgorithm
{
    private static readonly Point[] aroundPixelModifications = new Point[]
    {
        new Point(-1, 0),
        new Point(0, 1),
        new Point(1, 0),
        new Point(0, -1),
    };

    public AlgorithmExpector? AlgorithmExpector { get; set; }

    public void DrawArc(IBitmap bitmap, Color color, Point center, int R, double alfa, double beta)
    {
        AlgorithmExpector?.Start();

        // эврестический подсчёт шага
        double step = Math.Acos(1 - 1.0 / (2 * R * R)) * 180 / Math.PI;

        double angle = alfa;

        // Проверка на то что правый конец всегда правее
        if (alfa > beta)
        {
            beta += 360;
        }

        while (angle < beta)
        {
            // вычисляем координаты очередной точки дуги
            double x = center.X + R * Math.Cos(angle * Math.PI / 180);
            double y = center.Y - R * Math.Sin(angle * Math.PI / 180);

            // отрисовываем пиксиль
            bitmap.SetPixel((int)Math.Round(x, MidpointRounding.AwayFromZero)
                            , (int)Math.Round(y, MidpointRounding.AwayFromZero)
                            , color);

            // увеличиваем шаг
            angle += step;

            AlgorithmExpector?.IncrimentPixelCounter();
        }

        AlgorithmExpector?.Stop();
    }

    public void FillCircle(IBitmap bitmap, Color color, Color border, Point center, int R)
    {
        FillArea(bitmap, color, border, center);
    }

    public void FillArea(IBitmap bitmap, Color color, Color border, Point center)
    {
        AlgorithmExpector?.Start();

        Stack <Point> points = new Stack<Point>();

        bitmap.SetPixel(center.X, center.Y, color);

        points.Push(center);

        while (points.Count > 0)
        {
            var currPixel = points.Pop();

            foreach (var modification in aroundPixelModifications)
            {
                var neighbour = new Point(currPixel.X + modification.X, currPixel.Y + modification.Y);

                Color currPixelColor = bitmap.GetPixel(neighbour);

                if (currPixelColor.ToArgb() != color.ToArgb()
                    && currPixelColor.ToArgb() != border.ToArgb())
                {
                    bitmap.SetPixel(neighbour.X, neighbour.Y, color);

                    points.Push(neighbour);

                    AlgorithmExpector?.IncrimentPixelCounter();
                }
            }
        }

        AlgorithmExpector?.Stop();
    }
}
