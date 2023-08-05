using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApp.Factories;

public interface IRouteParametersFactory
{
    IDictionary<string, object> Create(string key, object value);
    IDictionary<string, object> Create(string[] keys, object[] values);
}

public class RouteParametersFactory : IRouteParametersFactory
{
    public IDictionary<string, object> Create(string[] keys, object[] values)
    {
        if (keys.Length != values.Length)
        {
            throw new ArgumentException($"Keys length \"{keys.Length}\" should be equal to the Values Length \"{values.Length}\"",
                $"{nameof(keys)}, {nameof(values)}");
        }

        var routeParams = new Dictionary<string, object>();

        for (int i = 0; i < keys.Length; i++)
        {
            routeParams[keys[i]] = values[i];
        }

        return routeParams;
    }

    public IDictionary<string, object> Create(string key, object value) =>
        new Dictionary<string, object>()
        {
            [key] = value
        };
}
