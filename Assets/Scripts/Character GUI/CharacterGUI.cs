using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGUI : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Image _icon;
    [SerializeField] private StatsBar _healthBar;

    private CharacterStats _stats;

    private void Awake()
    {
        _stats = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        _healthBar.SetMaxValue(_stats.MaxHp);
        _stats.onHpChange += _healthBar.SetValue;
    }
    public void ShowGui(bool show)
    {
        _container.SetActive(show);
    }

    public void SetImage(Sprite image)
    {
        _icon.sprite = image;
    }
}
