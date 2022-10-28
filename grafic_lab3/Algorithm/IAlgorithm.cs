using grafic_lab3.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafic_lab3.Algorithm;

public interface IAlgorithm
{
    /// <summary>
    /// Нарисовать дугу
    /// </summary>
    /// <param name="bitmap">Место рисования</param>
    /// <param name="color">Цвет линии</param>
    /// <param name="center">Центр окружности</param>
    /// <param name="R">Радиус окружности</param>
    /// <param name="alfa">Угол левого конца</param>
    /// <param name="beta">Угол правого конца</param>
    public void DrawArc(IBitmap bitmap, Color color, Point center, int R, double alfa, double beta);

    /// <summary>
    /// Заполнить круг
    /// </summary>
    /// <param name="bitmap">Место рисования</param>
    /// <param name="color">Цвет линии</param>
    /// <param name="border">Цвет границы</param>
    /// <param name="center">Центр окружности</param>
    /// <param name="R">Радиус окружности</param>
    public void FillCircle(IBitmap bitmap, Color color, Color border, Point center, int R);

    public AlgorithmExpector? AlgorithmExpector { set; get; }
}
