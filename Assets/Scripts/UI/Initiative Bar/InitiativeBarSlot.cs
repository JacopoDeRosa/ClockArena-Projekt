using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InitiativeBarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private WorldGizmos _gizmos;

    private void OnValidate()
    {
        if (_gizmos == null) _gizmos = FindObjectOfType<WorldGizmos>();
    }

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
        _text.text = character.Name;
        _assignedCharacter = character;

    }
    private void SetImage(Sprite image)
    {
        _image.sprite = image;
    }
}
