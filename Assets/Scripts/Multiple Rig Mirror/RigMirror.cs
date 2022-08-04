using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigMirror : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private List<BoneMirror> _mirrors;
    [SerializeField] private RigMirrorSource _startSource;

    private void OnValidate()
    {
        if (_root != null && _mirrors.Count < 1)
        {
           foreach(Transform bone in _root.GetComponentsInChildren<Transform>())
           {
                BoneMirror boneMirror;
                if(bone.TryGetComponent(out BoneMirror mirror))
                {
                    boneMirror = mirror;
                }
                else
                {
                    boneMirror = bone.gameObject.AddComponent<BoneMirror>();
                }

                _mirrors.Add(boneMirror);
                
           }
        }
    }

    private void Awake()
    {
        if(_startSource != null)
        {
            SetMatchingSource(_startSource);
        }
    }

    public void SetMatchingSource(RigMirrorSource source)
    {

        for (int i = 0; i < _mirrors.Count; i++)
        {
            BoneMirror mirror = _mirrors[i];
            Transform sourceBone = source.AllBones[i];
            if (source != null)
            {
                mirror.SetSourceBone(sourceBone);
            }

        }
    }
}