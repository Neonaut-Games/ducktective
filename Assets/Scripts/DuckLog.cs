using System;
using System.Diagnostics;
using System.Reflection;
using Debug = UnityEngine.Debug;

public static class DuckLog
{
    public static void Normal(string message)
    {
        Debug.Log("[" + NameOfCallingClass() + "] " + message);
    }
    
    private static string NameOfCallingClass()
    {
        string simpleName;
        Type declaringType;
        int skipFrames = 2;
        do
        {
            MethodBase method = new StackFrame(skipFrames, false).GetMethod();
            declaringType = method.DeclaringType;
            if (declaringType == null)
            {
                return method.Name;
            }
            skipFrames++;
            simpleName = declaringType.Name;
        }
        while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

        return simpleName;
    }
}