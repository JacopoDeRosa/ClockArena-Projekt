using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Image _image;
    [SerializeField] private bool _status;

    private bool _primed;

    public UnityEvent<bool> onStatusChange;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_primed == true)
        {
            _status = !_status;
            onStatusChange.Invoke(_status);
            SetCorrecSprite();
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

    private void SetCorrecSprite()
    {
        if (_status)
        {
            _image.sprite = _openSprite;
        }
        else
        {
            _image.sprite = _closedSprite;
        }
    }


    private void Awake()
    {
        SetCorrecSprite();
    }
}
