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
    }

    [Button]
    public void UpdateCharacterMesh()
    {
        SkinnedMeshRenderer[] renderers = _meshesContainer.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        MergeSkinnedMeshesWithSeparate(renderers);
    }

    private void MergeSkinnedMeshesWithSeparate(SkinnedMeshRenderer[] targets)
    {
        #region Error Catching
        if (targets == null || targets.Length < 2)
        {
            Debug.LogError("Target meshes array is null or too short");
            return;
        }
        #endregion

        #region Create and merge the new mesh

        Mesh finalMesh = new Mesh();

        finalMesh.name = "Merged Mesh Test";

        int totalSubmeshes = 0;

        foreach (var target in targets)
        {
            totalSubmeshes += target.sharedMesh.subMeshCount;
        }


        /*
        Use submesh index start to find where to copy the array from, use index count to know when to stop.
        Store the submesh vertex array, 

        var dest = new MyType[100];
        Array.Copy(source, 100, dest, 0, 100);

        create a new mesh with the vertex array and the submesh topology.
        Add the correct material to the mesh. will uvs be transferred? if not try to tranfer uvs.
        transfer boneweights using the array lenght and start position a boneweight array needs to be as long as the vertex count.
         */

        Mesh[] splitMeshes = new Mesh[totalSubmeshes];
        Material[] allMaterials = new Material[totalSubmeshes];

        int submeshOffset = 0;

        for (int i = 0; i < targets.Length; i ++)
        {
            Mesh baseMesh = targets[i].sharedMesh;
            for (int s = 0; s < baseMesh.subMeshCount; s++)
            {
                splitMeshes[submeshOffset] = baseMesh.GetSubmesh(s);
                splitMeshes[submeshOffset].name = "Submesh: " + i + ":" + s;
                Debug.Log(i + ":" + s);
                submeshOffset++;
            }

        }   

        _meshes = splitMeshes;

        CombineInstance[] combineInstances = new CombineInstance[totalSubmeshes];

        for (int i = 0; i < splitMeshes.Length; i++)
        {
            combineInstances[i].mesh = splitMeshes[i];
        }

        int submeshTransformOffset = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            Mesh baseMesh = targets[i].sharedMesh;
            for (int s = 0; s < baseMesh.subMeshCount; s++)
            {
                combineInstances[submeshTransformOffset].transform = targets[i].localToWorldMatrix;
            }
        }

         

        finalMesh.CombineMeshes(combineInstances, false, false);
        _finalMesh = finalMesh;

        #endregion
    
        #region Set the bindposes for the new mesh
        
        // TODO: Fix
        Matrix4x4[] bindPoses = targets[0].sharedMesh.bindposes;

        finalMesh.bindposes = bindPoses;
        
        #endregion
        
        #region Recalculate bone weights

        // TODO: fund a better method of merging bone weights
        BoneWeight[] finalBoneWeights = finalMesh.boneWeights;
        
        int offset = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            Debug.Log(targets[i].bones.Length);
            for (int v = 0; v < targets[i].sharedMesh.vertexCount; v++)
            {
               finalBoneWeights[offset + v].boneIndex0 -= _meshRenderer.bones.Length * i;
               finalBoneWeights[offset + v].boneIndex1 -= _meshRenderer.bones.Length * i;
               finalBoneWeights[offset + v].boneIndex2 -= _meshRenderer.bones.Length * i;
               finalBoneWeights[offset + v].boneIndex3 -= _meshRenderer.bones.Length * i;
            }
            offset += targets[i].sharedMesh.vertexCount;
        }

        finalMesh.boneWeights = finalBoneWeights;
        
        #endregion

        #region Finalize Mesh
        finalMesh.RecalculateBounds();
        #endregion

        #region Generate New Materials Array
        Material[] finalMats = new Material[totalSubmeshes];

        int submeshMatOffset = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            Mesh baseMesh = targets[i].sharedMesh;
            for (int s = 0; s < baseMesh.subMeshCount; s++)
            {
                finalMats[submeshMatOffset] = targets[i].sharedMaterials[s]; 
                submeshMatOffset++;
            }
        }   
        #endregion

        #region Set the final skinned mesh
        _meshRenderer.sharedMesh = finalMesh;
        _meshRenderer.materials = finalMats;
        #endregion
    }

    private void MergeSkinnedMeshes(SkinnedMeshRenderer[] targets)
    {
        #region Error Catching
        if (targets == null || targets.Length < 2)
        {
            Debug.LogError("Target meshes array is null or too short");
            return;
        }
        #endregion

        #region Create and merge the new mesh

        Mesh finalMesh = new Mesh();

        finalMesh.name = "Merged Mesh Test";

        int totalSubmeshes = 0;

        foreach (var target in targets)
        {
            totalSubmeshes += target.sharedMesh.subMeshCount;
        }


        /*
        Use submesh index start to find where to copy the array from, use index count to know when to stop.
        Store the submesh vertex array, 

        var dest = new MyType[100];
        Array.Copy(source, 100, dest, 0, 100);

        create a new mesh with the vertex array and the submesh topology.
        Add the correct material to the mesh. will uvs be transferred? if not try to tranfer uvs.
        transfer boneweights using the array lenght and start position a boneweight array needs to be as long as the vertex count.
         */

        Mesh[] splitMeshes = new Mesh[totalSubmeshes];
        Material[] allMaterials = new Material[totalSubmeshes];

        int submeshOffset = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            Mesh baseMesh = targets[i].sharedMesh;
            for (int s = 0; s < baseMesh.subMeshCount; s++)
            {
                splitMeshes[submeshOffset] = baseMesh.GetSubmesh(s);
                splitMeshes[submeshOffset].name = "Submesh: " + i + ":" + s;
                Debug.Log(i + ":" + s);
                submeshOffset++;
            }

        }

        _meshes = splitMeshes;

        CombineInstance[] combineInstances = new CombineInstance[totalSubmeshes];

        for (int i = 0; i < splitMeshes.Length; i++)
        {
            combineInstances[i].mesh = splitMeshes[i];
        }

        int submeshTransformOffset = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            Mesh baseMesh = targets[i].sharedMesh;
            for (int s = 0; s < baseMesh.subMeshCount; s++)
            {
                combineInstances[submeshTransformOffset].transform = targets[i].localToWorldMatrix;
            }
        }



        finalMesh.CombineMeshes(combineInstances, false, false);
        _finalMesh = finalMesh;

        #endregion

        #region Set the bindposes for the new mesh

        // TODO: Fix
        Matrix4x4[] bindPoses = targets[0].sharedMesh.bindposes;

        finalMesh.bindposes = bindPoses;

        #endregion

        #region Recalculate bone weights

        // TODO: fund a better method of merging bone weights
        BoneWeight[] finalBoneWeights = finalMesh.boneWeights;

        int offset = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            Debug.Log(targets[i].bones.Length);
            for (int v = 0; v < targets[i].sharedMesh.vertexCount; v++)
            {
                finalBoneWeights[offset + v].boneIndex0 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[offset + v].boneIndex1 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[offset + v].boneIndex2 -= _meshRenderer.bones.Length * i;
                finalBoneWeights[offset + v].boneIndex3 -= _meshRenderer.bones.Length * i;
            }
            offset += targets[i].sharedMesh.vertexCount;
        }

        finalMesh.boneWeights = finalBoneWeights;

        #endregion

        #region Finalize Mesh
        finalMesh.RecalculateBounds();
        #endregion

        #region Generate New Materials Array
        Material[] finalMats = new Material[totalSubmeshes];

        int submeshMatOffset = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            Mesh baseMesh = targets[i].sharedMesh;
            for (int s = 0; s < baseMesh.subMeshCount; s++)
            {
                finalMats[submeshMatOffset] = targets[i].sharedMaterials[s];
                submeshMatOffset++;
            }
        }
        #endregion

        #region Set the final skinned mesh
        _meshRenderer.sharedMesh = finalMesh;
        _meshRenderer.materials = finalMats;
        #endregion
    }
}
