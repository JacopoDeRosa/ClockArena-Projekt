using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Ability", menuName = "Abilities/New Summon Ability")]
public class SummonAbility : Ability
{
    [SerializeField] private GameObject _summon;

    public override void ActiveUse(Character user, Vector3 position)
    {
        Instantiate(_summon, position, Quaternion.identity);
    }
}
