using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SkinnedMeshMerger : MonoBehaviour
{

    [SerializeField] private SkinnedMeshRenderer[] _targets;
    [SerializeField] private Material _defaultMaterial;


    [Button("Merge Meshes")]
    private void Merge()
    {
        MergeSkinnedMeshes(_targets);
    }

    /*

    First create the new mesh.
    create new meshes from the target meshes.
    calculate how many uv tiles will be needed. 
    offset each mesh on its uv tile.
    create the merged maps.
    create the merged material.
    assign bind pos to the new mesh.
    assign the bones to the new mesh.
    recalculate bounds.

    */

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

        CombineInstance[] combineInstances = new CombineInstance[targets.Length];
       

        for (int i = 0; i < targets.Length; i++)
        {
            combineInstances[i].mesh = targets[i].sharedMesh;
            combineInstances[i].transform = targets[i].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combineInstances, false, false);
        #endregion

        #region Set the bindposes for the new mesh
        Matrix4x4[] bindPoses = targets[0].sharedMesh.bindposes;

        finalMesh.bindposes = bindPoses;
        #endregion

        #region Recalculate bone weights
        Transform[] bones = targets[0].bones;

        BoneWeight[] finalBoneWeights = finalMesh.boneWeights;

        int offset = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            for (int v = 0; v < targets[i].sharedMesh.vertexCount; v++)
            {
                finalBoneWeights[offset + v].boneIndex0 -= bones.Length * i;
                finalBoneWeights[offset + v].boneIndex1 -= bones.Length * i;
                finalBoneWeights[offset + v].boneIndex2 -= bones.Length * i;
                finalBoneWeights[offset + v].boneIndex3 -= bones.Length * i;
            }
            offset += _targets[i].sharedMesh.vertexCount;
        }

        finalMesh.boneWeights = finalBoneWeights;
        #endregion

        #region Instantiate the final skinned mesh

        finalMesh.RecalculateBounds();

        var finalObject = new GameObject("Merged Mesh of " + targets[0].gameObject.name);

        finalObject.transform.position = targets[0].transform.position;
        finalObject.transform.parent = targets[0].transform.parent;

        SkinnedMeshRenderer finalRenderer = finalObject.AddComponent<SkinnedMeshRenderer>();

        finalRenderer.sharedMesh = finalMesh;


        Material[] finalMats = new Material[finalMesh.subMeshCount];
        for (int i = 0; i < targets.Length; i++)
        {
            finalMats[i] = targets[i].sharedMaterial;
        }
        finalRenderer.materials = finalMats;

        finalRenderer.bones = bones;
        finalRenderer.rootBone = targets[0].rootBone;
        #endregion
    }



}
