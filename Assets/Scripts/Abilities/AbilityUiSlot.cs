using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class AbilityUiSlot: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private TooltipController _tooltip;
    [SerializeField] private GraphicRaycaster _raycaster;

    private Ability _ability;
    private AbilityDescriptor _abilityDescriptor;

    private Transform _originalParent;
    private Transform _moveParent;

    private Vector2 _offset;
    private bool _dragging;

    private IconsDB _iconsDB;

    private void Awake()
    {
        _iconsDB = GameItemDB.GetDbOfType<IconsDB>();
        _moveParent = transform.root;
        _originalParent = transform.parent;
    }

  

    public void SetAbility(Ability ability)
    {
        if (ability == null)
        {
            _abilityIcon.sprite = _iconsDB.DefaultSprite;
            _tooltip.SetContents("No Ability", "");
        }
        else
        {
            _abilityIcon.sprite = ability.Icon;
            _tooltip.SetContents(ability.Name, ability.Description);
        }
        _ability = ability;
    }
    public void SetAbilityDescriptor(AbilityDescriptor abilityDescriptor)
    {
        _abilityDescriptor = abilityDescriptor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            _offset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
            _dragging = true;
            transform.parent = _moveParent;
        }     
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (_dragging && eventData.button == PointerEventData.InputButton.Right)
        {
            transform.position = eventData.position + _offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AbilitySetSlot setSlot = GetItemAtPivot<AbilitySetSlot>();
        if(setSlot != null)
        {
            setSlot.SetAbility(_ability, _abilityDescriptor);
        }

        transform.parent = _originalParent;
    }

    private List<RaycastResult> GetAllElementsAtPivot()
    {
        PointerEventData fakeData = new PointerEventData(EventSystem.current);

        fakeData.position = transform.position;

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(fakeData, raycastResults);

        return raycastResults;
    }

    private T GetItemAtPivot<T>() where T : MonoBehaviour
    {
        T possibleItem = null;
        foreach (RaycastResult result in GetAllElementsAtPivot())
        {
            var slot = result.gameObject.GetComponent<T>();
            if (slot != null)
            {
                possibleItem = slot;
            }
        }
        return possibleItem;
    }
}

