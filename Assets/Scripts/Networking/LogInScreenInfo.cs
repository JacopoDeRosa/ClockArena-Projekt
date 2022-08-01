using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System.Net.Mail;

public class LogInScreenInfo : MonoBehaviour
{
    private const string LogInKey = "KeepLogIn";
    private const string EmailKey = "LogInMail";
    private const string PasswordKey = "LogInPass";

    [SerializeField] private Button _emailResetButton;
    [SerializeField] private string _emailResetUrl;

    [SerializeField] private Button _passwordResetButton;
    [SerializeField] private string _passwordResetUrl;


    private void Start()
    {
        _passwordResetButton.onClick.AddListener(OpenPasswordReset);
        _emailResetButton.onClick.AddListener(OpenEmailReset);
    }

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
}
