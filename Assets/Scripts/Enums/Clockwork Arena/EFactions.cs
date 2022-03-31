using System;

[Flags]
public enum FactionsBitmask
{
    None = 0,
    DeathRow = 1 << 1,
    Slummers = 1 << 2,
    Traitors = 1 << 3,
    All = DeathRow | Slummers | Traitors,
}

public enum Factions
{
    DeathRow = 1 << 1,
    Slummers = 1 << 2,
    Traitors = 1 << 3,
}
