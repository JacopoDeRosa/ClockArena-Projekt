using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gadget Data", menuName = "Items/New Gadget Data")]
public class GadgetData : ItemData
{
    [SerializeField] private int _maxUses;
    [SerializeField] private bool _needRestock;
    [SerializeField] private int _restockPrice;

    public override string GetItemClass()
    {
        return "Gadget";
    }
}
