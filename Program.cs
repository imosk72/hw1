using System.Diagnostics;


Process.GetCurrentProcess().ProcessorAffinity = (IntPtr) (1 << 4);
var stopWatch = new Stopwatch();

var thread1 = new Thread(() =>
{
    stopWatch.Start();
    while (stopWatch.IsRunning)
    {
    }
})
{
    Priority = ThreadPriority.Highest
};
var thread2 = new Thread(() =>
{
    while (true)
    {
        if (!stopWatch.IsRunning) continue;
        stopWatch.Stop();
        Console.WriteLine($"Tread1 was working {stopWatch.Elapsed.Milliseconds} milliseconds");
        return;
    }
})
{
    Priority = ThreadPriority.Highest
};
thread2.Start();
thread1.Start();
thread2.Join();
thread1.Join();
