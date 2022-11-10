using System.Diagnostics;

// привязываем к последнему ядру 
Process.GetCurrentProcess().ProcessorAffinity = (IntPtr) (1 << 4);
var stopWatch = new Stopwatch();

var thread1 = new Thread(() =>
{
    stopWatch.Start();
    while (stopWatch.IsRunning) { }
}) { Priority = ThreadPriority.Highest };

var thread2 = new Thread(() =>
{
    while (true)
    {
        if (!stopWatch.IsRunning) continue;
        stopWatch.Stop();
        var milliseconds = stopWatch.Elapsed.Milliseconds;
        Console.WriteLine($"Tread1 was working {milliseconds} milliseconds");
        return;
    }
}) { Priority = ThreadPriority.Highest };

thread2.Start();
//запускаем поток, который запускает таймер
thread1.Start();
//ждем окончания работы второго потока, когда отключится таймер
//первый поток точно будет работать свой один квант времени,
//потом в следующий квант запустится следующий поток, который отключит таймер и скажет время работы первого
thread2.Join();
thread1.Join();

//итого получилось: Tread1 was working 31 milliseconds
