using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armour : GameItem<ArmourData>
{
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;

    public void AssignBones(Transform[] bones)
    {
        foreach (SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.bones = bones;
        }
    }
}
