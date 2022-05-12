using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonitorDropdown : GameOptionSetter
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private void Start()
    {
        _dropdown.ClearOptions();
        _dropdown.AddOptions(new List<string>(GetDisplays()));
    }
    private IEnumerable<string> GetDisplays()
    {
        foreach (var display in Display.displays)
        {
            yield return display.ToString();
        }
    }
}
