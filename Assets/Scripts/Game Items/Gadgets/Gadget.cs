using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : GameItem
{
    new public GadgetData Data { get => (GadgetData) _data; }

    private void OnValidate()
    {
        ForceDatatype<GadgetData>();
    }
}
