using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : GameItem<GadgetData>
{
    [SerializeField] new private GadgetData _data;
}
