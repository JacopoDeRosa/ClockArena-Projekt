using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Ability", menuName = "Abilities/New Summon Ability")]
public class SummonAbility : Ability
{
    [SerializeField] private GameObject _summon, _userFX;

    public override void ActiveUse(Character user, Vector3 position)
    {
        user.Animator.SetTrigger("Summon Attack");
        Instantiate(_summon, position, Quaternion.identity);
        Instantiate(_userFX, user.transform.position, user.transform.rotation);
    }
}
