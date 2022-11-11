using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomMenuTips : MonoBehaviour
{
    [SerializeField] private TMP_Text _title, _content;
    [SerializeField] private MenuTip[] _tips;


    private void OnEnable()
    {
        SetRandomTip();
    }

    private void SetRandomTip()
    {
        MenuTip tip = _tips[Random.Range(0, _tips.Length)];

        _title.text = tip.Title;
        _content.text = tip.Content;
    }
}

[System.Serializable]
public class MenuTip
{
    [SerializeField] private string _title;
    [SerializeField] [TextArea] private string _content;

    public string Title { get => _title; }
    public string Content { get => _content; }
}
