using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogInScreen : MonoBehaviour
{
    // Use player prefs to check if player should save prefs, if so check if data exists, if it does auto log in
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private Button _emailResetButton;
    [SerializeField] private string _emailResetUrl;

    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Button _passwordResetButton;
    [SerializeField] private string _passwordResetUrl;



    public void OpenPasswordReset()
    {
        OpenURL(_passwordResetUrl);
    }
    public void OpenEmailReset()
    {
        OpenURL(_emailResetUrl);
    }
    private void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Init()
    {

    }
}
