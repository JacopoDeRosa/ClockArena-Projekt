using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsBar : MonoBehaviour
{
    [SerializeField] private Image _fillMask, _fill;
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private int _maxValue = 1;
    [SerializeField] private int _currentValue = 1;
    

    public void SetMaxValue(int value)
    {
        _maxValue = value;
        EvaluateFill();
    }

    public void SetValue(int value)
    {
        _currentValue = value;
        EvaluateFill();
    }

    public void SetColor(Color color)
    {
        _fill.color = color;
    }

    private void EvaluateFill()
    {
        float fill = (float)_currentValue / (float)_maxValue;
        _fillMask.fillAmount = fill;
        if(_valueText != null)  _valueText.text = _currentValue.ToString() + "/" + _maxValue.ToString();
    }


    private void OnValidate()
    {
        EvaluateFill();
    }

}
