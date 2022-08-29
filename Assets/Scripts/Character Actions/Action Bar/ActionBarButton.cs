using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarButton : MonoBehaviour, IPointerDownHandler, IClickAudio
{
    [SerializeField] private Image _image;
    [SerializeField] private TooltipController _tooltip;

    private ActionScheduler _actionScheduler;
    private int _actionIndex;

    public ClickTypes ClickType { get => ClickTypes.Heavy; }

    public event Action onAudio;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onAudio?.Invoke();
        _actionScheduler.StartAction(_actionIndex);
    }

    public void SetAction(BarAction action, int index)
    {
        _actionIndex = index;
        _image.sprite = action.Icon;
        _tooltip.SetContents(action.Name, action.Description);
    }
}
