using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionSetter : MonoBehaviour
{
    [SerializeField] private OptionsMenu _optionsMenu;

    protected virtual void OnValidate()
    {
        _optionsMenu = GetComponentInParent<OptionsMenu>();
    }

    protected void SetOptionDirty(System.Action onSettingsConfirm)
    {
        _optionsMenu.AddSettingChange(onSettingsConfirm);
    }

    public virtual void Redraw()
    {

    }
}
