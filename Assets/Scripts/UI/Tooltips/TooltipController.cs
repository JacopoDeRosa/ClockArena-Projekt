using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _toolTip;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _content;


    public void OnPointerEnter(PointerEventData eventData)
    {
        _toolTip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _toolTip.SetActive(false);
    }

    public void SetContents(string title, string content)
    {
        _title.text = title;
        _content.text = content;
    }
}
