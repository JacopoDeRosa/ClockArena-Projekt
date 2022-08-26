using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class ItemUiSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IClickAudio
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private bool _canBeCleared;

    private PointerEventData.InputButton _downButton;

    public ClickTypes ClickType { get => ClickTypes.Heavy; }

    public event Action onClick;
    public event Action onClear;
    public event Action onAudio;

    public void OnPointerDown(PointerEventData eventData)
    {
        _downButton = eventData.button;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != _downButton) return;

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            onClick?.Invoke();
        }
        else if(eventData.button == PointerEventData.InputButton.Right && _canBeCleared)
        {
            onClear?.Invoke();
        }
        onAudio?.Invoke();
    }

    public void SetItem(GameItem item)
    {
        if(item == null)
        {
            _iconImage.sprite = _emptySprite;
            _nameText.text = "Empty";
        }
        else
        {
            _iconImage.sprite = item.Data.Sprite;
            _nameText.text = item.Data.Name;
        }
    }

    

}
