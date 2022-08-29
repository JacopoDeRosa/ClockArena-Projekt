using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _range;
    [SerializeField] private float _duration;
    [SerializeField] private bool _hasActiveUse;
    [SerializeField] private bool _hasPassiveUse;
    [SerializeField] private AbilityTypes _activeUseType;



    public string Name { get => _name; }
    public string Description { get => _description; }
    public Sprite Icon { get => _icon; }
    public AbilityTypes AbilityType { get => _activeUseType; }
    public bool HasActiveUse { get => _hasActiveUse; }
    public bool HasPassiveUser { get => _hasPassiveUse; }
    public float Range { get => _range; }
    public float Duration { get => _duration; }

    public virtual void ActiveUse(Character user)
    {

    }
    public virtual void ActiveUse(Character user, Character target)
    {

    }
    public virtual void ActiveUse(Character user, Vector3 position)
    {

    }

    public virtual void PassiveUse(Character user)
    {

    }
}
