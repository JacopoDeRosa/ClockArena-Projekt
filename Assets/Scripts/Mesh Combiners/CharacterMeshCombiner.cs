using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System;

public class CharacterMeshCombiner : MonoBehaviour
{
    [SerializeField] private string _finalMeshName;
    [SerializeField] private Equipment _characterEquipment;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Transform _meshesContainer;

    private void Awake()
    {
        _characterEquipment.onArmourChanged += UpdateCharacterMesh;
        UpdateCharacterMesh();
    }

    [Button]
    public void UpdateCharacterMesh()
    {
        StartCoroutine(MergeMeshesRoutine());
    }

    public void UpdateCharacterMeshImmediate()
    {
        SkinnedMeshRenderer[] renderers = _meshesContainer.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        MergeSkinnedMeshes(renderers);
    }

    private IEnumerator MergeMeshesRoutine()
    {
        yield return new WaitForEndOfFrame();
        UpdateCharacterMeshImmediate();
    }

    private void MergeSkinnedMeshes(SkinnedMeshRenderer[] targets)
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

        finalMesh.name = _finalMeshName;


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

        finalMesh.CombineMeshes(combineInstances, false, false);

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

        /*
          Bones in the submeshes have and index of i + submesh index when the mesh is merged
          This is the old way of offsetting the bone weights, it does not take into account submeshes

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
        }
        */

        for (int i = 0; i < finalMesh.subMeshCount; i++)
        {
            SubMeshDescriptor currentSubmesh = finalMesh.GetSubMesh(i);
        //    Debug.Log("Base Vertex: " + currentSubmesh.firstVertex);
        //    Debug.Log("Vertex Count: " + currentSubmesh.vertexCount);
            for (int v = 0; v < currentSubmesh.vertexCount; v++)
            {
                finalBoneWeights[v + currentSubmesh.firstVertex].boneIndex0 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[v + currentSubmesh.firstVertex].boneIndex1 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[v + currentSubmesh.firstVertex].boneIndex2 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[v + currentSubmesh.firstVertex].boneIndex3 -= _meshRenderer.bones.Length * i;
            }
        }

 /* 
        USE THIS TO DEBUG THE INDEX OF EACH BONE IF THE CONSOLE (WARNING: THIS IS SLOW)
        for (int i = 0; i < finalBoneWeights.Length; i++)
        {
            BoneWeight weight = finalBoneWeights[i];
            Debug.Log("Index 0 of Vertex " + i + " : " + weight.boneIndex0);
        }
 */

        

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
