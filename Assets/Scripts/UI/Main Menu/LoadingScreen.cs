using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _tipText;


    public void SetText(string text)
    {
        _tipText.text = text;
    }
  
    
}
