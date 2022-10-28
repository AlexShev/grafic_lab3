using static grafic_lab3.Image.IBitmap;

namespace grafic_lab3.Image;


/// <summary>
/// Класс отвечающий за расстягивание изображения
/// </summary>
public class ScaledBitmap : IBitmap
{
    /// <summary>
    /// Картинка
    /// </summary>
    private Bitmap _bitmap;
    /// <summary>
    /// Множитель  увеличения
    /// </summary>
    private int _scale;

    public CallBack? Function { get; set; }

    public ScaledBitmap(int width, int height, int scale, Color backGround)
    {
        _bitmap = new Bitmap(width - width % scale, height - height % scale);
        
        _scale = scale;

        BackGround = backGround;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                SetPixel(x, y, backGround);
            }
        }
    }

    public ScaledBitmap(Bitmap bitmap, int scale)
    {
        _bitmap = new Bitmap(bitmap.Width * scale, bitmap.Height * scale);

        _scale = scale;

        BackGround = Color.White;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                SetPixel(x, y, bitmap.GetPixel(x, y));
            }
        }
    }

    public Bitmap ToBitmap()
    {
        lock(_bitmap)
            return new Bitmap(_bitmap);
    }

    public int Width => _bitmap.Width / _scale;
    public int Height => _bitmap.Height / _scale;

    public Color BackGround { get; set; }

    public void SetPixel(int x, int y, Color color)
    {
        if (IsCorrectPixel(x, y))
        {
            int offsetX = x * _scale;
            int offsetY = y * _scale;

            lock (_bitmap)
            {
                for (int i = 0; i < _scale; i++)
                {
                    for (int j = 0; j < _scale; j++)
                    {
                        _bitmap.SetPixel(i + offsetX, j + offsetY, color);
                    }
                }
            }
            // Вызов функкции для обновления
            if (Function != null)
            {
                Function(this);
            }
        }
    }

    public Color GetPixel(int x, int y)
    {
        int offsetX = x * _scale;
        int offsetY = y * _scale;

        return _bitmap.GetPixel(offsetX, offsetY);
    }

    public Color GetPixel(Point point)
    {
        return GetPixel(point.X, point.Y);
    }

    /// <summary>
    /// Проверить пиксель на принадлежность обоасти
    /// </summary>
    /// <param name="point">Рассматриваемая точка</param>
    private bool IsCorrectPixel(int x, int y)
    {
        return x > -1 && y > -1 && x < Width && y < Height;
    }
}
