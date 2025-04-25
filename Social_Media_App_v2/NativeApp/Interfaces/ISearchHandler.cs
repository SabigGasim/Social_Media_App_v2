using System.Timers;

namespace NativeApp.Interfaces;

public interface ILookupHandler
{
    void Reset(string query);
    Func<string, Task> Handle { get; set; }
    double Interval { get; set; }
}
