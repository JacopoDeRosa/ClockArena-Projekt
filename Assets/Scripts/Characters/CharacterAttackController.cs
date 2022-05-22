using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour, ISleeper
{
    [SerializeField] private Equipment _characterEquipment;

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
}
