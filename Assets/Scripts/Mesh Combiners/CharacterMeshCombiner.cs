using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System;

public class CharacterMeshCombiner : MonoBehaviour
{
    [SerializeField] private Equipment _characterEquipment;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Transform _meshesContainer;
    [SerializeField] private Mesh[] _meshes;
    [SerializeField] private Mesh _finalMesh;

    private void Awake()
    {
        _characterEquipment.onArmourChanged += UpdateCharacterMesh;
        UpdateCharacterMesh();
    }

    [Button]
    public void UpdateCharacterMesh()
    {
        SkinnedMeshRenderer[] renderers = _meshesContainer.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        MergeSkinnedMeshesEfficent(renderers);
    }

    private void MergeSkinnedMeshesEfficent(SkinnedMeshRenderer[] targets)
    {
        #region Error Catching
        if (targets == null || targets.Length < 1)
        {
            Debug.LogError("Target meshes array is null or too short");
            return;
        }
        #endregion

        #region Create and merge the new mesh

        Mesh finalMesh = new Mesh();

        finalMesh.name = "Merged Mesh Test";


        // Get The total number of submeshes
        int totalSubmeshes = 0;

        foreach (var target in targets)
        {
            totalSubmeshes += target.sharedMesh.subMeshCount;
        }


        // Get Material Array
        Material[] allMaterials = new Material[totalSubmeshes];
        
        int submeshOffset = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            for (int s = 0; s < targets[i].sharedMesh.subMeshCount; s++)
            {
                allMaterials[submeshOffset] = targets[i].sharedMaterials[s];
                submeshOffset++;
            }

        }

        // Get Meshes Array
        Mesh[] allMeshes = new Mesh[targets.Length];
        _meshes = allMeshes;

        for (int i = 0; i < targets.Length; i++)
        {
            allMeshes[i] = targets[i].sharedMesh;
        }

        // Create the combine instances
        CombineInstance[] combineInstances = new CombineInstance[totalSubmeshes];

        int combineOffset = 0;
        for (int i = 0; i < allMeshes.Length; i++)
        {
            for (int s = 0; s < allMeshes[i].subMeshCount; s++)
            {
                combineInstances[combineOffset] = new CombineInstance();
                combineInstances[combineOffset].mesh = allMeshes[i];
                combineInstances[combineOffset].subMeshIndex = s;
                combineInstances[combineOffset].transform = targets[i].localToWorldMatrix;
                combineOffset++;
            }
        }

        finalMesh.CombineMeshes(combineInstances, false, true);

        _finalMesh = finalMesh;
      //  Debug.Log("Finalmesh Vertex Count: " + _finalMesh.vertices.Length);
        #endregion

        #region Set the bindposes for the new mesh
        // TODO: Fix
           Matrix4x4[] bindPoses = targets[0].sharedMesh.bindposes;
           finalMesh.bindposes = bindPoses;
      //   Debug.Log("Finalmesh Bindposes Count: " + finalMesh.bindposes.Length);
        #endregion

        #region Recalculate bone weights

        // TODO: find a better method of merging bone weights
        BoneWeight[] finalBoneWeights = finalMesh.boneWeights;

     //   Debug.Log("Finalmesh Boneweights Count: " + finalMesh.boneWeights.Length);

        int offset = 0;

        for (int i = 0; i < allMeshes.Length; i++)
        {
            Mesh currentMesh = allMeshes[i];
            for (int v = 0; v < currentMesh.vertexCount; v++)
            {            
                finalBoneWeights[offset + v].boneIndex0 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[offset + v].boneIndex1 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[offset + v].boneIndex2 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[offset + v].boneIndex3 -= _meshRenderer.bones.Length * i;
            }
            offset += currentMesh.vertexCount;
       //     Debug.Log("Adding offset: " + allMeshes[i].vertexCount);
        }
      //  Debug.Log("Offset: " + offset + " Final Mesh Vertex count: " + finalMesh.vertices.Length);
        finalMesh.boneWeights = finalBoneWeights;

        #endregion

        #region Finalize Mesh
        finalMesh.RecalculateBounds();
        #endregion

        #region Generate New Materials Array
        
        #endregion

        #region Set the final skinned mesh
        _meshRenderer.sharedMesh = finalMesh;
        _meshRenderer.materials = allMaterials;
        #endregion
    }

  
}
