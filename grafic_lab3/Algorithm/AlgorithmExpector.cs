using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafic_lab3.Algorithm;

public class AlgorithmExpector
{
    Stopwatch stopwatch = new Stopwatch();

    private long pixelCounter = 0;

    public void IncrimentPixelCounter()
    {
        ++pixelCounter;
    }

    public long PixelCounter { get { return pixelCounter; } }

    public long Time { get { return stopwatch.ElapsedMilliseconds; } }

    public void Start()
    {
        stopwatch.Start();
    }

    public void Stop()
    {
        stopwatch.Stop();
    }
}
