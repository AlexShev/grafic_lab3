namespace grafic_lab3.Image;

/// <summary>
/// Интерфейс для классов картинок
/// </summary>
public interface IBitmap
{
    /// <summary>
    /// Делегат для обновления картинки
    /// </summary>
    public delegate void CallBack(IBitmap bitmap);


    /// <summary>
    /// Ширина изображения
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Получить обычный Bitmap
    /// </summary>
    Bitmap ToBitmap();

    /// <summary>
    /// Высота изображения
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Цвет фона
    /// </summary>
    public Color BackGround { get; set; }

    /// <summary>
    /// Установить значения пикселя
    /// </summary>
    /// <param name="x">Координата по оси OX</param>
    /// <param name="y">Координата по оси OY</param>
    /// <param name="color">Значение цвета</param>
    public void SetPixel(int x, int y, Color color);

    /// <summary>
    /// Получить значения пикселя
    /// </summary>
    /// <param name="x">Координата по оси OX</param>
    /// <param name="y">Координата по оси OY</param>
    public Color GetPixel(int x, int y);

    /// <summary>
    /// Получить значения пикселя
    /// </summary>
    /// <param name="point">Рассматриваемая точка</param>
    public Color GetPixel(Point point);

}