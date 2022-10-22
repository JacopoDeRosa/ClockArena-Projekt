using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGUI : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _friendlyLocator, _enemyLocator;
    [SerializeField] private Image _icon;
    [SerializeField] private StatsBar _healthBar;
    [SerializeField] private Color _friendlyColor, _enemyColor;


    private CharacterStats _stats;

    private void Awake()
    {
        _stats = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        _healthBar.SetMaxValue(_stats.MaxHp);
        _healthBar.SetValue(_stats.HP);
        _stats.onHpChange += _healthBar.SetValue;     
    }
    public void ShowGui(bool show)
    {
        _container.SetActive(show);
    }
    public void SetGuiFriendly(bool friendly)
    {
        if(friendly)
        {
            _healthBar.SetColor(_friendlyColor);
            _friendlyLocator.SetActive(true);
        }
        else
        {
            _healthBar.SetColor(_enemyColor);
            _enemyLocator.SetActive(true);
        }
    }

    public void SetImage(Sprite image)
    {
        _icon.sprite = image;
    }
}
