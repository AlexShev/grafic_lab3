using grafic_lab3.Algorithm;
using grafic_lab3.Image;

namespace grafic_lab3.Model;

class Smilic
{
    /// <summary>
    /// Множитель расстояния
    /// </summary>
    private const double eyeScaleDistance = 0.5;

    /// <summary>
    /// положение центра смайлика
    /// </summary>
    Point _center;

    /// <summary>
    /// радиус лица
    /// </summary>
    int _faseRadius;
    /// <summary>
    /// радиус глаз
    /// </summary>
    int _eyeRadius;

    /// <summary>
    /// Радиус дуги - рот
    /// </summary>
    int _mouthRadius;
    /// <summary>
    /// Угол между 180 и левым концом дуги
    /// </summary>
    int _mouthAngleAlfa;
    /// <summary>
    /// Угол между правым концом дуги и 360
    /// </summary>
    int _mouthAngleBeta;

    /// <summary>
    /// Положение правого глаза
    /// </summary>
    Point _rightEyePosition;

    /// <summary>
    /// Положение левого глаза
    /// </summary>
    Point _leftEyePosition;


    /// <summary>
    /// Сеттер mouthAngleAlfa
    /// </summary>
    /// <param name="mouthAngleAlfa">новое значение угола между 180 и левым концом дуги</param>
    public int MouthAngleAlfa
    {
        get => _mouthAngleAlfa;
        set => _mouthAngleAlfa = value;
    }

    /// <summary>
    /// Сеттер mouthAngleBeta
    /// </summary>
    /// <param name="mouthAngleBeta">новое значение угола между правым концом дуги и 360</param>
    public int MouthAngleBeta
    {
        get => _mouthAngleBeta;
        set => _mouthAngleBeta = value;
    }

    /// <summary>
    /// Сеттер mouthRadius
    /// </summary>
    /// <param name="mouthRadius">новое значение радиуса дуги - рта</param>
    public int MouthRadius
    {
        get => _mouthRadius; 
        set
        {
            if (value <= 0)
            {
                _mouthRadius = 1;
            }
            else if (value > _faseRadius)
            {
                _mouthRadius = _faseRadius - 1;
            }
            else
            {
                _mouthRadius = value;
            }
        }
    }


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="center">положение центра смайлика</param>
    /// <param name="faseRadius">радиус лица</param>
    /// <param name="eyeRadius">радиус глаз</param>
    /// <param name="mouthRadius">Радиус дуги - рот</param>
    /// <param name="mouthAngleAlfa">Угол между 180 и левым концом дуги</param>
    /// <param name="mouthAngleBeta">Угол между правым концом дуги и 360</param>
    public Smilic(Point center, int faseRadius, int eyeRadius, int mouthRadius, int mouthAngleAlfa, int mouthAngleBeta)
    {
        _center = center;
        _faseRadius = faseRadius;
        _eyeRadius = eyeRadius;
        _mouthRadius = mouthRadius;
        MouthAngleAlfa = mouthAngleAlfa;
        MouthAngleBeta = mouthAngleBeta;

        // Отрисовка глаз - вычисляется положение глаз как положение точки на окружности под углом 30 градусов
        // Затем полученные относительные значения умножаются на множитель, чтобы глаза были внутри
        // Затем происходит смещение относительно (0;0) в центр
        _rightEyePosition = new Point((int)(_center.X + Math.Sqrt(3) * _faseRadius / 2 * eyeScaleDistance), (int)(_center.Y - _faseRadius / 2 * eyeScaleDistance));
        _leftEyePosition = new Point((int)(_center.X - Math.Sqrt(3) * _faseRadius / 2 * eyeScaleDistance), (int)(_center.Y - _faseRadius / 2 * eyeScaleDistance));
    }


    /// <summary>
    /// Отрисовать смайлик с помощью алгоритма
    /// </summary>
    /// <param name="algorithm">Алгоритм рисования</param>
    /// <param name="bitmap">Место рисования</param>
    /// <param name="lineColor">Цвет линии</param>
    public SmileDrawInfo Draw(IAlgorithm algorithm, IBitmap bitmap, Color lineColor)
    {
        SmileDrawInfo res = new SmileDrawInfo();

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // отрисовка контуров смайлика
        algorithm.DrawArc(bitmap, lineColor, _center, _faseRadius, 0, 360);
        res.faseConturePixels = algorithm.AlgorithmExpector.PixelCounter;
        res.faseContureTime = algorithm.AlgorithmExpector.Time;

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // Заливка Лица
        algorithm.FillCircle(bitmap, Color.Yellow, lineColor, _center, _faseRadius);
        res.faseFillPixels = algorithm.AlgorithmExpector.PixelCounter;
        res.faseFillTime = algorithm.AlgorithmExpector.Time;

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // Отрисовка левого глаза
        algorithm.DrawArc(bitmap, lineColor, _leftEyePosition, _eyeRadius, 0, 360);
        res.leftEyeConturePixels = algorithm.AlgorithmExpector.PixelCounter;
        res.leftEyeContureTime = algorithm.AlgorithmExpector.Time;

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // Отрисовка правого глаза
        algorithm.DrawArc(bitmap, lineColor, _rightEyePosition, _eyeRadius, 0, 360);
        res.rightEyeConturePixels = algorithm.AlgorithmExpector.PixelCounter;
        res.rightEyeContureTime = algorithm.AlgorithmExpector.Time;

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // Заполнение левого глаза
        algorithm.FillCircle(bitmap, Color.SkyBlue, lineColor, _leftEyePosition, _eyeRadius);
        res.leftEyeFillPixels = algorithm.AlgorithmExpector.PixelCounter;
        res.leftEyeFillTime = algorithm.AlgorithmExpector.Time;

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // Заполнение правого глаза
        algorithm.FillCircle(bitmap, Color.SkyBlue, lineColor, _rightEyePosition, _eyeRadius);
        res.rightEyeFillPixels = algorithm.AlgorithmExpector.PixelCounter;
        res.rightEyeFillTime = algorithm.AlgorithmExpector.Time;

        algorithm.AlgorithmExpector = new AlgorithmExpector();
        // Отрисовка дуги. Углы переводяться относительно дуги (-1;0) и (1;0) на един окружности 
        algorithm.DrawArc(bitmap, lineColor, _center, _mouthRadius, 180 + _mouthAngleAlfa, 360 - _mouthAngleBeta);
        res.mouthConturePixels = algorithm.AlgorithmExpector.PixelCounter;
        res.mouthContureTime = algorithm.AlgorithmExpector.Time;

        return res;
    }

    public class SmileDrawInfo
    {
        public long faseConturePixels;
        public long faseContureTime;

        public long faseFillPixels;
        public long faseFillTime;

        public long leftEyeConturePixels;
        public long leftEyeContureTime;

        public long leftEyeFillPixels;
        public long leftEyeFillTime;

        public long rightEyeConturePixels;
        public long rightEyeContureTime;

        public long rightEyeFillPixels;
        public long rightEyeFillTime;

        public long mouthConturePixels;
        public long mouthContureTime;

        public override string ToString()
        {
            return "                                         Пиксели     Время \n"
                + $"Контур лица                        {faseConturePixels}           {faseContureTime} \n"
                + $"Заполение лица                  {faseFillPixels}         {faseFillTime} \n"
                + $"Контур левого глаза            {leftEyeConturePixels}           {leftEyeContureTime} \n"
                + $"Заполнение левого глаза   {leftEyeFillPixels}           {leftEyeFillTime} \n"
                + $"Контур правого глаза         {rightEyeConturePixels}           {rightEyeContureTime} \n"
                + $"Заполение правого глаза   {rightEyeFillPixels}           {rightEyeFillTime} \n"
                + $"Контур рта                            {mouthConturePixels}           {mouthContureTime} \n";
        }
    }
}