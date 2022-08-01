using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrlButton : MonoBehaviour
{
    [SerializeField] private string _url;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(OpenUrl);
    }

    public void OpenUrl()
    {
        Application.OpenURL(_url);
    }
}
