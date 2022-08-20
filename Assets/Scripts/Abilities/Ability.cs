using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "New Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] private bool _hasActiveUse;
    [SerializeField] private bool _hasPassiveUse;
    [SerializeField] private AbilityTypes _activeUseType;

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
