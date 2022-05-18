using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : GameOptionSetter
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private List<Resolution> _resolutions;
    private List<string> _dropdownOptions;

    private void Start()
    {
        _resolutions = new List<Resolution>(Screen.resolutions);
        
        _dropdownOptions = new List<string>(GetResFromArray(_resolutions));

        _dropdown.ClearOptions();

        _dropdown.AddOptions(_dropdownOptions);

        string currentResolution = Screen.currentResolution.ToString();

        int currentResIndex = _dropdownOptions.FindIndex(x => x.Equals(currentResolution));

        _dropdown.SetValueWithoutNotify(currentResIndex);
    }

    private IEnumerable<string> GetResFromArray(IEnumerable<Resolution> resolutions)
    {
        foreach (Resolution resolution in resolutions)
        {
            yield return resolution.ToString();
        }
    }
}
