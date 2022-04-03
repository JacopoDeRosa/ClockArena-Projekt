using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InitiativeBarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _image;

    private Character _assignedCharacter;
    private bool _pointerIn = false;

    public void OnPointerClick(PointerEventData eventData)
    {
       if(_pointerIn && eventData.button == PointerEventData.InputButton.Left)
       {
            Debug.Log("Should focus on: " + _assignedCharacter.Name);
       }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerIn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerIn = false;
    }

    public void SetCharacter(Character character)
    {
        if (character.CustomIcon != null)
        {
            SetImage(character.CustomIcon);
        }
        else
        {
            SetImage(character.Data.DefaulIcon);
        }
        _assignedCharacter = character;
    }
    private void SetImage(Sprite image)
    {
        _image.sprite = image;
    }
}
