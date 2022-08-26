using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemWindow : MenuWindow
{
    [SerializeField] private GridLayoutGroup _gridLayout;
    [SerializeField] private ItemWindowSlot[] _allSlots;

    private Action<int> _itemSelectedCallback;

    private Queue<ItemWindowSlot> _freeSlots;


    protected override void Awake()
    {
        base.Awake();
        ResetWindowSlots();
    }
    private void ResetWindowSlots()
    {
        if(_freeSlots == null)
        {
            _freeSlots = new Queue<ItemWindowSlot>();
        }
        else
        {
            _freeSlots.Clear();
        }

        foreach(ItemWindowSlot slot in _allSlots)
        {
            _freeSlots.Enqueue(slot);
            slot.SetParent(this);
            slot.gameObject.SetActive(false);
        }
    }

    public void ShowItems(IEnumerable<ItemDescriptor> items)
    {
        ResetWindowSlots();


        foreach (ItemDescriptor item in items)
        {
            ItemWindowSlot slot = _freeSlots.Dequeue();
            slot.gameObject.SetActive(true);
            slot.SetSlotItem(item);
        }
    }

    public void SetItemsSize(int x, int y)
    {
        _gridLayout.cellSize.Set(x, y);
    }

    public void SetCallback(Action<int> callback)
    {
        _itemSelectedCallback = callback;
    }

    public void SelectItem(int selection)
    {
        _itemSelectedCallback?.Invoke(selection);
        CloseWindow();
    }
}

public class ItemDescriptor
{
   public int item;
   public Sprite sprite;
    public string info;

    public ItemDescriptor(int item, Sprite sprite)
    {
        this.item = item;
        this.sprite = sprite;
        info = "";
    }
    public ItemDescriptor(int item, Sprite sprite, string description)
    {
        this.item = item;
        this.sprite = sprite;
        this.info = description;
    }
}