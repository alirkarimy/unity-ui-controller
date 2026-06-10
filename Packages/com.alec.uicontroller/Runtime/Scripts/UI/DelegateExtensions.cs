using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class DelegateExtensions
{
    public static void SafeInvoke(this Delegate del, params object[] args)
    {
        var exceptions = new List<Exception>();

        foreach (Delegate handler in del.GetInvocationList())
        {
            try
            {
                handler.Method.Invoke(handler.Target, args);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }

        if (exceptions.Any())
        {
            throw new AggregateException(exceptions);
        }
    }

    public static bool Contains(this Delegate del, MethodInfo method)
    {
        foreach (Delegate handler in del.GetInvocationList())
        {
            if (handler.Method.Equals(method))
            {
                return true;
            }
        }
        return false;
    }
}
