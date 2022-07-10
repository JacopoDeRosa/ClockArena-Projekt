using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armour : GameItem<ArmourData>
{
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;

    public SkinnedMeshRenderer[] MeshRenderers { get => _meshRenderers; }
}
