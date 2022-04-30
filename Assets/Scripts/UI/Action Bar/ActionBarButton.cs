using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TooltipController _tooltip;

    private bool _primed;
    private CharacterAction _action;


    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Left && _action != null && _primed)
       {
            _action.Begin();
       }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _primed = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _primed = false;
    }
    public void Init(CharacterAction action)
    {
        if (action == null) return;
        _action = action;
        if(_tooltip)  _tooltip.SetContents(action.GetName(), action.GetDescription());
        _image.sprite = action.GetActionSprite();
    }

    public ActionBarButton ResetButton()
    {
        _action = null;
        gameObject.SetActive(false);
        return this;
    }
}
