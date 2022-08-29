using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RangeGizmo : MonoBehaviour
{
    [SerializeField] private TMP_Text _sizeText;

    public void SetRange(float range)
    {
        transform.localScale.Set(range, range, range);
        _sizeText.text = range.ToString();
    }
}
