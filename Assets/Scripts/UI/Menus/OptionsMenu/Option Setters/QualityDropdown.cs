using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class QualityDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private void Start()
    {
        _dropdown.ClearOptions();
        _dropdown.AddOptions(new List<string>(GetQualityTypes()));
    }

    private IEnumerable<string> GetQualityTypes()
    {
       return QualitySettings.names;
    }
}
