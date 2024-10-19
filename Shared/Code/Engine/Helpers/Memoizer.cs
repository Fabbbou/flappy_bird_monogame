using System;
using System.Collections.Generic;

public class Memoizer
{
    private static Memoizer _instance;
    public static Memoizer I
    {
        get
        {
            _instance ??= new Memoizer();
            return _instance;
        }
    }

    private readonly Dictionary<string, Dictionary<object[], object>> _cache = new Dictionary<string, Dictionary<object[], object>>();

    public T Memoize<T>(Func<T> method, params object[] parameters)
    {
        string methodName = method.Method.Name;

        if (!_cache.ContainsKey(methodName))
        {
            _cache[methodName] = new Dictionary<object[], object>(new ObjectArrayComparer());
        }

        if (_cache[methodName].ContainsKey(parameters))
        {
            return (T)_cache[methodName][parameters];
        }

        T result = method();
        _cache[methodName][parameters] = result;

        return result;
    }

    private class ObjectArrayComparer : IEqualityComparer<object[]>
    {
        public bool Equals(object[] x, object[] y)
        {
            if (x.Length != y.Length)
                return false;

            for (int i = 0; i < x.Length; i++)
            {
                if (!x[i].Equals(y[i]))
                    return false;
            }

            return true;
        }

        public int GetHashCode(object[] obj)
        {
            int hash = 17;
            foreach (var o in obj)
            {
                hash = hash * 31 + (o?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
