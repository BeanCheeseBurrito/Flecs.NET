using System.Threading;

namespace Flecs.NET.Utilities;

internal struct JobState(InvokerCallback callback, CountdownEvent countdown, WorkerIterable worker)
{
    public InvokerCallback Callback = callback;
    public CountdownEvent Countdown = countdown;
    public WorkerIterable Worker = worker;
}
