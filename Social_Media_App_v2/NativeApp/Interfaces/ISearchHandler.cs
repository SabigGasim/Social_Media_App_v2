using System.Timers;

namespace NativeApp.Interfaces;

public interface ILookupHandler
{
    void ResetAndStartSearchTimer(string query);
    Func<string, Task> Handle { get; set; }
    double Interval { get; set; }
}
