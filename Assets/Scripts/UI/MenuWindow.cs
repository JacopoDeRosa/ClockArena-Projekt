using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private RectTransform _header;



    private Vector2 _offset;
    private bool _dragging;

    public System.Action onClose;

    protected virtual void Awake()
    {
        _closeButton.onClick.AddListener(CloseWindow);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        onClose?.Invoke();
    }

    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && RectTransformUtility.RectangleContainsScreenPoint(_header, eventData.position))
        {
            _offset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
            _dragging = true;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(_dragging && eventData.button == PointerEventData.InputButton.Right)
        {
            transform.position = eventData.position + _offset;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
        _offset.Set(0, 0);
    }
}
