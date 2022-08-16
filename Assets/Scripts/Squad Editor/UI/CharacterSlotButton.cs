using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSlotButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IClickAudio
{
    [SerializeField] private Graphic _graphic;
    [SerializeField] private Color _normalColor, _highlightedColor, _clickColor;
    [SerializeField] private SquadEditor _editor;
    [SerializeField] private int _index;
    [SerializeField] private GameObject _filledView, _emptyView;
    [SerializeField] private TMP_Text _nameText, _levelText;
    [SerializeField] private Image _characterIcon;

    public event Action onAudio;

    public ClickTypes ClickType { get => ClickTypes.Heavy; }


    public void OnPointerDown(PointerEventData eventData)
    {
        _graphic.color = _clickColor;
        onAudio?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _graphic.color = _normalColor;
        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _graphic.color = _highlightedColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _graphic.color = _normalColor;
    }

    private void OnClick()
    {
        if (_editor.SquadLoaded == false) return;

        Character character = _editor.GetCharacter(_index);
        if(character == null)
        {
             character = _editor.SpawnCharacterAtIndex(_index);
            _emptyView.SetActive(false);
            _filledView.SetActive(true);
            _nameText.text = character.Name;
            _levelText.text = "Lvl " + character.Level;
        }
        else
        {
            _editor.FocusOnCharacter(_index);
        }

    }

}
