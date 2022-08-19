﻿using UnityEngine;
using System.Collections;

public class AbilityTier
{
    [SerializeField] private string _name;
    [SerializeField] private Ability[] _abilities;

    public string Name { get => _name; }
    
    public Ability GetAbility(int index)
    {
        return _abilities[index];
    }

}
