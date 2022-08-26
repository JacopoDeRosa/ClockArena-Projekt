using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemWindowSlot : MonoBehaviour, IPointerDownHandler 
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _infoText;

    private ItemWindow _parent;
    private int _index;

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_EDITOR
        if(_parent == null)
        {
            Debug.LogError("Parent is null");

        }
#endif
        _parent.SelectItem(_index);

    }

    public void SetSlotItem(ItemDescriptor descriptor)
    {
        _index = descriptor.item;
        _icon.sprite = descriptor.sprite;
        _infoText.text = descriptor.info;
    }

    public void SetParent(ItemWindow parent)
    {
        if (_parent == null)
        {
            _parent = parent;
        }
    }
}
