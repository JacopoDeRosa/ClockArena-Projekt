using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Ability", menuName = "Abilities/New Healing Ability")]
public class HealingAbility : Ability
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private int _healing;

    public override void ActiveUse(Character user)
    {
        user.Stats.HealDamage(_healing);
        user.Animator.SetTrigger("Summon Buff");
        Instantiate(_effect, user.transform.position, user.transform.rotation);
    }
}
