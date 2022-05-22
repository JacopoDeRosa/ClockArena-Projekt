using System;

[Flags]
public enum Factions
{
    None = 1,
    DeathRow = 2,
    Slummers = 4,
    Traitors = 8,
    All = DeathRow | Slummers | Traitors,
}