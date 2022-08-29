using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackAction : MonoBehaviour, ISleeper, IBarAction
{
    [SerializeField] private Equipment _characterEquipment;

    public event Action onActionEnd;

    private void OnValidate()
    {
        if(_characterEquipment == null)
        {
           _characterEquipment = GetComponent<Equipment>();
        }
    }


    public void Sleep()
    {
        
    }

    public void WakeUp()
    {
        
    }

    public IEnumerable<BarAction> GetBarActions()
    {
        throw new NotImplementedException();
    }
}
