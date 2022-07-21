using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterMeshCombiner : MonoBehaviour
{
    [SerializeField] private Equipment _characterEquipment;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Transform _meshesContainer;

    private void Awake()
    {
        _characterEquipment.onArmourChanged += UpdateCharacterMesh;
    }

    [Button]
    public void UpdateCharacterMesh()
    {
        SkinnedMeshRenderer[] renderers = _meshesContainer.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        MergeSkinnedMeshes(renderers);
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

        BoneWeight[] finalBoneWeights = finalMesh.boneWeights;

        int offset = 0;

        for (int i = 0; i < targets.Length; i++)
        {
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
        Material[] finalMats = new Material[finalMesh.subMeshCount];
        for (int i = 0; i < targets.Length; i++)
        {
            finalMats[i] = targets[i].sharedMaterial;
        }
        #endregion

        #region Set the final skinned mesh
        _meshRenderer.sharedMesh = finalMesh;
        _meshRenderer.materials = finalMats;
        #endregion
    }
}
