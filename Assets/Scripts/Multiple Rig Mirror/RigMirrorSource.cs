using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigMirrorSource : MonoBehaviour
{
    [SerializeField] private List<Transform> _allRigBones;
    [SerializeField] private Transform _root;


    public List<Transform> AllBones { get => _allRigBones; }

    private void OnValidate()
    {
        if (_root != null)
        {
            _allRigBones = new List<Transform>(_root.GetComponentsInChildren<Transform>());
        }
    }
}
