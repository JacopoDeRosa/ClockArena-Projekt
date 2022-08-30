using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackAction : MonoBehaviour, ISleeper, IBarAction
{
    [SerializeField] private Character _user;


    public event Action onActionEnd;

    private IconsDB _iconsDB;

    private void Awake()
    {
        _iconsDB = GameItemDB.GetDbOfType<IconsDB>();
    }

    public void Sleep()
    {
        
    }

    public void WakeUp()
    {
        
    }

    public void Punch()
    {
        _user.Animator.SetTrigger("Melee");
        _user.Voice.PlayAttack();
        StartCoroutine(EndActionDelayed(2));
    }

    public bool NoCancel()
    {
        return false;
    }

    private IEnumerator EndActionDelayed(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        onActionEnd?.Invoke();
    }


    public IEnumerable<BarAction> GetBarActions()
    {
        if(_user.Equipment.Weapon is RangedWeapon)
        {
            yield return new BarAction(null, null, this, "Not Implemented", "Not Implemented", null);
        }
        else if (_user.Equipment.Weapon is MeleeWeapon)
        {
            yield return new BarAction(null, null, this, "Not Implemented", "Not Implemented", null);
        }
        else if(_user.Equipment.Weapon == null)
        {
            yield return new BarAction(Punch, NoCancel, this, "Punch", "Punch anything in front of you", _iconsDB.MeleeSprite);
        }
    }
}
