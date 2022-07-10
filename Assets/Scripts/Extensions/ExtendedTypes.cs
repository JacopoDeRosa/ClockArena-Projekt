using System.Collections;
using System;

public static class ExtendedTypes
{
    public static string GetName(this Type type)
    {
        string[] split = type.FullName.Split('.');

        return split[split.Length - 1];
    }

}
