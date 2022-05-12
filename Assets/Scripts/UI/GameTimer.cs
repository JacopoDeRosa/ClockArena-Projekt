using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ExtendedUI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private void Update()
    {
        string time = Time.timeSinceLevelLoad.ToClockFormat();
        _text.text = time;  
    }
}
