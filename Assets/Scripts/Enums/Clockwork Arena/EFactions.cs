using System;

[Flags]
public enum Factions
{
    DeathRow = 2,
    Slummers = 4,
    Traitors = 8,
    Robots = 16,
    Humans = DeathRow | Slummers | Traitors,
}

public enum Faction
{ 
    DeathRow = 2,
    Slummers = 4,
    Traitors = 8,
    Robots = 16
}
