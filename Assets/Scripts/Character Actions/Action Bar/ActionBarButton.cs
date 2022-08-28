using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TooltipController _tooltip;



    public void SetAction(BarAction action)
    {
        _image.sprite = action.Icon;
        _tooltip.SetContents(action.Name, action.Description);
    }
}
