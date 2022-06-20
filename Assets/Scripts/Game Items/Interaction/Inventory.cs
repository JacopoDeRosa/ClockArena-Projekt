using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _allSlots;

    public IEnumerable<ItemSlot> GetSlotsByType<T>() where T: GameItem
    {
        foreach (ItemSlot slot in _allSlots)
        {
            if(slot.Item is T)
            {
                yield return slot;
            }
        }
    }

    public IEnumerable<ItemSlot> GetAllArmour()
    {
        return GetSlotsByType<Armour>();
    }

    public IEnumerable<ItemSlot> GetAllWeapons()
    {
        return GetSlotsByType<Weapon>();
    }

    public IEnumerable<ItemSlot> GetAllGadgets()
    {
        return GetSlotsByType<Gadget>();
    }
}
