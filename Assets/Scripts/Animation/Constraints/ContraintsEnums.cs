using System;

[Flags]
public enum Axis
{
    X = 2,
    Y = 4,
    Z = 8
}

public enum Space
{
    World,
    Local
}

public enum ConstrainMode
{
    Add,
    Replace
}