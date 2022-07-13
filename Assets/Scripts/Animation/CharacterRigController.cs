using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRigController : MonoBehaviour
{
   [SerializeField] private Rig _aimRig;


   public void SetAimWeight(float value)
   {
        value = Mathf.Clamp01(value);
        _aimRig.weight = value;
   }
}


