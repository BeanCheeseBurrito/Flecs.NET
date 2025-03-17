namespace Flecs.NET.Core.Invokers;

internal interface IJobInvoker
{
    static abstract void Invoke(JobState jobState);
}
