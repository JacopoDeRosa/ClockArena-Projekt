using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarButton : MonoBehaviour, IPointerDownHandler, IClickAudio, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private TooltipController _tooltip;
    [SerializeField] private FoldingBar _foldingBar;

    private ActionScheduler _actionScheduler;
    private int _actionIndex;
    private bool _locked;

    public ClickTypes ClickType { get => ClickTypes.Heavy; }

    public event Action onAudio;

    private void Awake()
    {
        _actionScheduler = FindObjectOfType<ActionScheduler>();
        _actionScheduler.onActionEndedOrCanceled += OnActionCanceled;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onAudio?.Invoke();
        _actionScheduler.StartAction(_actionIndex);
        _locked = true;
    }

    public void SetAction(BarAction action, int index)
    {
        _actionIndex = index;
        _image.sprite = action.Icon;
        _tooltip.SetContents(action.Name, action.Description);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_locked) _locked = false;
        _foldingBar.Toggle(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_locked) return;
        _foldingBar.Toggle(false);
    }

    private void OnActionCanceled()
    {
        _foldingBar.Toggle(false);
        _locked = false;
    }

    private void OnEnable()
    {
        _locked = false;
        _foldingBar.transform.localPosition = Vector3.zero;
        _foldingBar.ToggleNoMove(false);
    }
}
