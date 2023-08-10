using NativeApp.Interfaces;
using System.Timers;

namespace NativeApp.Services;

public class LookupHandler : ILookupHandler
{
    private readonly System.Timers.Timer _timer;
    private string? _newQuery;
    private string? _oldQuery;

    public LookupHandler()
    {
        _timer = new System.Timers.Timer();
        _timer.AutoReset = false;
        _timer.Elapsed += TimerElapsedEventHandler;
    }

    public double Interval
    {
        get => _timer.Interval;
        set => _timer.Interval = value;
    }

    public Func<string, Task> Handle { get; set; } = default!;

    public void ResetAndStartSearchTimer(string query)
    {
        _timer.Stop();
        _newQuery = query;
        _timer.Start();
    }

    private async void TimerElapsedEventHandler(object? sender, ElapsedEventArgs e)
    {
        if(_oldQuery == _newQuery)
        {
            return;
        }

        _oldQuery = _newQuery;

        await Handle(_newQuery!);
    }
}
