using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : GameOptionSetter
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private Resolution[] _resolutions;
    private List<string> _dropdownOptions;

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _dropdownOptions = new List<string>(GetResFromArray(_resolutions));
        _dropdown.ClearOptions();
        _dropdown.AddOptions(_dropdownOptions);
    }

    private IEnumerable<string> GetResFromArray(Resolution[] resolutions)
    {
        foreach (Resolution resolution in resolutions)
        {
            yield return resolution.ToString();
        }
    }
}
