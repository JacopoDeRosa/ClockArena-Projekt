using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class LightingUtilities
{
    
   
    [MenuItem("Probe Declutter", menuItem = "Utilities/Lighting/Probe Declutter")]
    private static void LightProbeDeclutter()
    {
        Transform transform = Selection.activeTransform;

        if(transform == null)
        {
            Debug.LogWarning("Please select an object");
            return;
        } 
            

        LightProbeGroup lightProbeGroup = transform.GetComponent<LightProbeGroup>();

        if (lightProbeGroup == null)
        {
            Debug.LogWarning("Please Select a valid lightprobe group");
            return;
        }

        List<Vector3> positions = new List<Vector3>(lightProbeGroup.probePositions);

        positions = positions.Distinct().ToList();

        lightProbeGroup.probePositions = positions.ToArray();
    }


}
