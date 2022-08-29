using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InitiativeBarSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private WorldGizmos _gizmos;

    private void OnValidate()
    {
        if (_gizmos == null) _gizmos = FindObjectOfType<WorldGizmos>();
    }

    private Character _assignedCharacter;

    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Left)
       {
            Debug.Log("Should focus on: " + _assignedCharacter.Name);
       }
    }
 

    public void SetCharacter(Character character)
    {
        SetImage(character.Icon);   
        _text.text = character.Name;
        _assignedCharacter = character;
    }
    private void SetImage(Sprite image)
    {
        _image.sprite = image;
    }
}
